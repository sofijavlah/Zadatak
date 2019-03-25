using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Zadatak.Exceptions;

namespace Zadatak.Expressions
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryInfo
    {
        /// <summary>
        /// Number of entities in the list to skip when sorting.
        /// </summary>
        /// <value>
        /// The skip.
        /// </value>
        public int Skip { get; set; }

        /// <summary>
        /// Number of elements to return when sorting the list. (if skip = x, and take = y, it returns elements from index [x+1] to [x+y])
        /// </summary>
        /// <value>
        /// The take.
        /// </value>
        public int Take { get; set; }

        /// <summary>
        /// Sort the list by values defined in each "SortInfo" in this list of sorters.
        /// </summary>
        public List<SortInfo> Sorters = new List<SortInfo>();

        /// <summary>
        /// Filter the list depending on the values defined for this entity.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        public FilterInfo Filter { get; set; }




        // ---------------------------=----------------EKSPRESIJE ------------------------------------------------------ //

        /// <summary>
        /// Gets the sorted.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="queryInfo">The query information.</param>
        /// <param name="list">The list.</param>
        /// <returns>
        /// List gone through every sort criteria and filtered through with each Rule defined in Filter/Rules. 
        /// </returns>
        public IQueryable<TEntity> GetSorted<TEntity>(QueryInfo queryInfo, List<TEntity> list)
        {
            var sortInfo = queryInfo.Sorters;

            var filterInfo = queryInfo.Filter;

            IQueryable<TEntity> myList = list.AsQueryable();

            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");

            var binary = GetAndOrExpression(parameter, filterInfo);

            myList = myList.Where(GetFilterExpression<TEntity>(parameter, binary));

            bool sorted = false;

            foreach (var sort in sortInfo)
            {
                switch (sort.SortDirection)
                {
                    case 'a':
                        if (sorted == false)
                        {
                            myList = myList.OrderBy(GetOrderExpression<TEntity>(parameter, sort));
                            sorted = true;
                            break;
                        }

                        var sortedList1 = myList as IOrderedQueryable<TEntity>;
                        myList = sortedList1.ThenBy(GetOrderExpression<TEntity>(parameter, sort));
                        break;

                    case 'd':
                        if (sorted == false)
                        {
                            myList = myList.OrderByDescending(GetOrderExpression<TEntity>(parameter, sort));
                            sorted = true;
                            break;
                        }

                        var sortedList2 = myList as IOrderedQueryable<TEntity>;
                        myList = sortedList2.ThenBy(GetOrderExpression<TEntity>(parameter, sort));
                        break;
                }
            }

            myList = myList.Skip(queryInfo.Skip).Take(queryInfo.Take);

            return myList;
        }


        /// <summary>
        /// Gets Lambda Expression to order list by certain properties.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="parameter">The parameter.</param>
        /// <param name="sortInfo">The sort information.</param>
        /// <returns>
        /// Lambda expression for property we want to sort our list by. The property is defined in SortInfo argument.
        /// </returns>
        public Expression<Func<TEntity, object>> GetOrderExpression<TEntity>(ParameterExpression parameter, SortInfo sortInfo)
        {
            var propExpression = Expression.Property(parameter, sortInfo.Property);

            var convertExp = Expression.Convert(propExpression, typeof(object));

            return Expression.Lambda<Func<TEntity, object>>(convertExp, parameter);
        }



        /// <summary>
        /// True or false expression intended to be sent to GetFilterExpression
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="filterInfo">The filter information.</param>
        /// <returns>
        /// True or false expression dependent on each object satisfying certain condition defined in each filter rule.
        /// </returns>
        /// <exception cref="CustomException">
        /// Property {property}
        /// or
        /// Unexpected value type {currentType.Name}
        /// </exception>
        public Expression GetAndOrExpression(ParameterExpression parameter, FilterInfo filterInfo)
        {
            //true & false expr
            var trueExpression = Expression.Constant(true, typeof(bool));
            var falseExpression = Expression.Constant(false, typeof(bool));

            Expression result;

            result = filterInfo.Condition == "and" ? trueExpression : falseExpression;
            
            BinaryExpression binary;

            foreach (var rule in filterInfo.Rules)
            {
                Expression propExpression = parameter;

                Expression currentParameter = parameter;

                Type currentType = parameter.Type;

                var allProperties = rule.Property.Split(".");

                foreach (var property in allProperties)
                {
                    PropertyInfo[] propertyTypes = currentType.GetProperties();

                    bool propertyExists = false;

                    foreach (var propType in propertyTypes)
                    {
                        if (propType.Name == property)
                        {
                            propertyExists = true;
                        }
                    }

                    if (propertyExists == false) throw new CustomException($"Property {property} doesn't exist");

                    currentParameter = Expression.Property(currentParameter, property);
                    
                    currentType = currentParameter.Type;

                    propExpression = Expression.Property(propExpression, property);
                }
                
                var convertedValue = Convert.ChangeType(rule.Value, currentType);

                var constant = Expression.Constant(convertedValue);

                switch (convertedValue)
                {
                    case string _:
                        binary = GetBinaryExpressionForString(rule.Operator, propExpression, constant);
                        break;

                    case long _:
                        binary = GetBinaryExpressionForInt(rule.Operator, propExpression, constant);
                        break;

                    default:
                        throw new CustomException($"Unexpected value type {currentType.Name}");
                }
                
                if (filterInfo.Condition == "and")
                    result = Expression.AndAlso(result, binary);

                else
                    result = Expression.OrElse(result, binary);

            }

            return result;

        }


        /// <summary>
        /// Expression to put as argument for "Where" method.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="parameter">The parameter.</param>
        /// <param name="andOr">The and or.</param>
        /// <returns></returns>
        public Expression<Func<TEntity, bool>> GetFilterExpression<TEntity>(ParameterExpression parameter,
            Expression andOr)
        {
            return Expression.Lambda<Func<TEntity, bool>>(andOr, parameter);
        }


        /// <summary>
        /// Gets the binary expression for int.
        /// </summary>
        /// <param name="operand">The operand.</param>
        /// <param name="propExpression">The property expression.</param>
        /// <param name="constant">The constant.</param>
        /// <returns>
        /// Binary Expression
        /// </returns>
        /// <exception cref="InvalidOperationException">Neocekivani operator {operand}</exception>
        public BinaryExpression GetBinaryExpressionForInt(string operand, Expression propExpression, ConstantExpression constant)
        {
            switch (operand)
            {
                case "gt":
                    return Expression.GreaterThan(propExpression, constant);
                case "lt":
                    return Expression.LessThan(propExpression, constant);
                case "gte":
                    return Expression.GreaterThanOrEqual(propExpression, constant);
                case "lte":
                    return Expression.LessThanOrEqual(propExpression, constant);
                case "eq":
                    return Expression.Equal(propExpression, constant);

                default:
                    throw new InvalidOperationException($"Neocekivani operator {operand}");
            }
        }


        /// <summary>
        /// Gets the binary expression for string.
        /// </summary>
        /// <param name="operand">The operand.</param>
        /// <param name="propExpression">The property expression.</param>
        /// <param name="constant">The constant.</param>
        /// <returns>
        /// BinaryExpression
        /// </returns>
        /// <exception cref="InvalidOperationException">Neocekivani operator {operand}</exception>
        public BinaryExpression GetBinaryExpressionForString(string operand, Expression propExpression, ConstantExpression constant)
        {
            var trueExpression = Expression.Constant(true, typeof(bool));

            BinaryExpression bin;

            //MethodInfo compareToMethod = typeof(string).GetMethod("CompareTo", new[] {typeof(string)});

            //var compareCall = Expression.Call(propExpression, compareToMethod, constant);

            //var zero = Expression.Constant(0);

            //var comparison = Expression.Equal(compareCall, zero);

            switch (operand)
            {
                case "eq":
                    return Expression.Equal(propExpression, constant);

                case "ct":
                    MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var contains = Expression.Call(propExpression, containsMethod, constant);
                    bin = Expression.Equal(contains, trueExpression);
                    break;

                case "sw":
                    MethodInfo startsMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                    var startsWith = Expression.Call(propExpression, startsMethod, constant);
                    bin = Expression.Equal(startsWith, trueExpression);
                    break;

                case "ew":
                    MethodInfo endsMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                    var endsWith = Expression.Call(propExpression, endsMethod, constant);
                    bin = Expression.Equal(endsWith, trueExpression);
                    break;

                default:
                    throw new InvalidOperationException($"Neocekivani operator {operand}");
            }

            return bin;
        }
    }
}
