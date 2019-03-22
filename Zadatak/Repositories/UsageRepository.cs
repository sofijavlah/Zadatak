using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Zadatak.Attributes;
using Zadatak.DTOs.Device;
using Zadatak.Exceptions;
using Zadatak.Expressions;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso>
    ///     <cref>Repositories.Repository{Models.DeviceUsage}</cref>
    /// </seealso>
    /// <seealso cref="Zadatak.Interfaces.IUsageRepository" />
    [DefineScopeType]
    public class UsageRepository : Repository<DeviceUsage>, IUsageRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsageRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UsageRepository(WorkContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [NoUnitOfWork]
        public override IEnumerable<DeviceUsage> GetAll()
        {
            var usages = Context.DeviceUsages.Include(x => x.Employee).Include(x => x.Device).ToList();

            return usages;
        }

        /// <summary>
        /// Adds the specified device.
        /// </summary>
        /// <param name="device">The device.</param>
        public void Add(Device device)
        {
            Context.DeviceUsages.Add(new DeviceUsage
            {
                From = DateTime.Now,
                To = null,
                Device = device,
                Employee = device.Employee
            });

            Context.SaveChanges();
        }

        /// <summary>
        /// Updates the specified device usage.
        /// </summary>
        /// <param name="deviceUsage">The device usage.</param>
        public override void Update(DeviceUsage deviceUsage)
        {
            var usage = Context.DeviceUsages.Find(deviceUsage.Id);

            usage.To = DateTime.Now;

            Context.SaveChanges();
        }


        // ---------------------------=----------------EKSPRESIJE ------------------------------------------------------ //

        public IQueryable<TEntity> GetSorted<TEntity>(QueryInfo queryInfo, List<TEntity> list)
        {
            var sortInfo = queryInfo.Sorters;

            var filterInfo = queryInfo.Filter;

            IQueryable<TEntity> myList = list.AsQueryable();

            //objekat
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");

            myList.Where(GetFilterExpression<TEntity>(parameter, filterInfo));

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
                            myList = myList.OrderBy(GetOrderExpression<TEntity>(parameter, sort));
                            sorted = true;
                            break;
                        }

                        var sortedList2 = myList as IOrderedQueryable<TEntity>;
                        myList = sortedList2.ThenBy(GetOrderExpression<TEntity>(parameter, sort));
                        break;
                }
            }

            return myList;
        }


        public Expression<Func<TEntity, object>> GetOrderExpression<TEntity>(ParameterExpression parameter, SortInfo sortInfo)
        {
            var propExpression = Expression.Property(parameter, sortInfo.Property);

            var convertExp = Expression.Convert(propExpression, typeof(object));

            return Expression.Lambda<Func<TEntity, object>>(convertExp, parameter);
        }


        public Expression<Func<TEntity, bool>> GetFilterExpression<TEntity>(ParameterExpression parameter, FilterInfo filterInfo)
        {
            //true & false expr
            var trueExpression = Expression.Constant(true, typeof(bool));
            var falseExpression = Expression.Constant(false, typeof(bool));

            //& || expr
            BinaryExpression and = Expression.AndAlso(trueExpression, trueExpression);
            BinaryExpression or = Expression.AndAlso(trueExpression, falseExpression);
            Expression select = null;

            //binary result
            BinaryExpression binary;

            foreach (var rule in filterInfo.Rules)
            {
                //property
                var propExpression = Expression.Property(parameter, rule.Property);

                //tip propertija
                var type = propExpression.Type;

                var convertedValue = Convert.ChangeType(rule.Value, type);

                //konstanta
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
                        throw new CustomException($"Unexpected value type {type.Name}");
                }

                select = binary;
                or = Expression.OrElse(or, binary);            //x.ID > 8 !!!!!!!!!!
                and = Expression.AndAlso(and, binary);
            }

            if (filterInfo.Condition == "and")
                return Expression.Lambda<Func<TEntity, bool>>(and, parameter);

            if (filterInfo.Condition == "or")
                return Expression.Lambda<Func<TEntity, bool>>(or, parameter);

            return Expression.Lambda<Func<TEntity, bool>>(select, parameter);
        }


        public BinaryExpression GetBinaryExpressionForInt(string operand, MemberExpression propExpression, ConstantExpression constant)
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

        public BinaryExpression GetBinaryExpressionForString(string operand, MemberExpression propExpression, ConstantExpression constant)
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
