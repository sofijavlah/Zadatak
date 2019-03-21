using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using Zadatak.Attributes;
using Zadatak.DTOs.Device;
using Zadatak.Exceptions;
using Zadatak.Expressions;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.Repositories
{
    /// <summary>
    /// Device Repository
    /// </summary>
    /// <seealso>
    ///     <cref>Repositories.Repository{Models.Device}</cref>
    /// </seealso>
    /// <seealso cref="Zadatak.Interfaces.IDeviceRepository" />
    [DefineScopeType]
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DeviceRepository(WorkContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [NoUnitOfWork]
        public override IEnumerable<Device> GetAll()
        {
            var devices = Context.Devices.Include(x => x.Employee).ToList();

            return devices;
        }

        /// <summary>
        /// Gets the device use history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">Doesn't exist</exception>
        [NoUnitOfWork]
        public Device GetDeviceUseHistory(long id)
        {
            var device = Context.Devices.Include(x => x.UsageList).ThenInclude(x => x.Employee).FirstOrDefault(x => x.Id == id);

            if(device == null) throw new CustomException("Doesn't exist");

            return device;

        }
        
        /// <summary>
        /// Gets the device current information.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">Doesn't exist</exception>
        [NoUnitOfWork]
        public Device GetDeviceCurrentInfo(long id)
        {
            var device = Context.Devices.Include(x => x.Employee).Include(x => x.UsageList)
                .FirstOrDefault(x => x.Id == id);

            if (device == null) throw new CustomException("Doesn't exist");

            return device;
        }

        /// <summary>
        /// Gets the name of the device by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [NoUnitOfWork]
        public Device GetDeviceByName(string name)
        {
            var device = Context.Devices.Include(x => x.Employee).Include(x => x.UsageList)
                .FirstOrDefault(x => x.Name == name);

            return device;
        }

        /// <summary>
        /// Adds the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        public void Add(DeviceDto dto)
        {
            var user = Context.Employees.Find(dto.Employee.EmployeeId);

            Context.Devices.Add(new Device
            {
                Name = dto.Name,
                Employee = user
            });

            Context.SaveChanges();
        }

        /// <summary>
        /// Changes the device name or user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <exception cref="Exception">Device doesn't exist</exception>
        public void ChangeDeviceNameOrUser(long id, DeviceDto dto)
        {
            var device = Context.Devices.Include(x => x.Employee).FirstOrDefault(x => x.Id == id);

            if (device == null) throw new CustomException("Device doesn't exist");

            if (dto.Name != null && dto.Name != device.Name)
            {
                device.Name = dto.Name;
            }

            var newUser = Context.Employees.Find(dto.Employee.EmployeeId);

            device.Employee = newUser;

            Context.SaveChanges();
        }


        public BinaryExpression GetOneSelectExpression<TEntity>(RuleInfo rule)
        {
            //objekat
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");

            //property
            var propExpression = Expression.Property(parameter, rule.Property);

            //tip propertija
            var type = propExpression.Type;

            var convertedValue = Convert.ChangeType(rule.Value, type);

            //konstanta
            var constant = Expression.Constant(convertedValue);

            BinaryExpression binary;

            switch (convertedValue)
            {
                case string _:
                    binary = GetBinaryExpressionForString(rule.Operator, propExpression, constant);
                    break;

                case int _:
                    binary = GetBinaryExpressionForInt(rule.Operator, propExpression, constant);
                    break;

                default:
                    throw new CustomException($"Unexpected value type {type.Name}");
            }

            return binary;
        }


        public BinaryExpression GetBinaryExpressionForInt(string operand, MemberExpression propExpression,
            ConstantExpression constant)
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


        public Expression GetAndOrExpression<TEntity>(FilterInfo filterInfo)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");

            var trueExpression = Expression.Constant(true, typeof(bool));

            var falseExpression = Expression.Constant(false, typeof(bool));

            BinaryExpression and = Expression.AndAlso(trueExpression, trueExpression);

            BinaryExpression or = Expression.AndAlso(trueExpression, falseExpression);

            if (filterInfo.Condition == "and")
            {
                foreach (var rule in filterInfo.Rules)
                {
                    var oneSelect = GetOneSelectExpression<TEntity>(rule);
                    and = Expression.AndAlso(and, oneSelect);
                }

                return Expression.Lambda<Func<TEntity, bool>>(and, parameter);
            }

            if (filterInfo.Condition == "or")
            {
                foreach (var rule in filterInfo.Rules)
                {
                    var oneSelect = GetOneSelectExpression<TEntity>(rule);
                    or = Expression.OrElse(or, oneSelect);
                }

                return Expression.Lambda<Func<TEntity, bool>>(or, parameter);
            }

            throw new CustomException("NoFilterCalled");
        }

        public BinaryExpression GetBinaryExpressionForString(string operand, MemberExpression propExpression,
            ConstantExpression constant)
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

        public BinaryExpression GetOneWhereExpression<TEntity>(SortInfo sortInfo)
        {
            //objekat
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");

            //property
            var propExpression = Expression.Property(parameter, sortInfo.Property);

            //tip propertija
            var type = propExpression.Type;

            var convertedValue = Convert.ChangeType(sortInfo.SortDirection, type);

            //konstanta
            var constant = Expression.Constant(convertedValue);

            BinaryExpression binary;

            switch (convertedValue)
            {
                case string _:
                    binary = GetBinaryExpressionForString(rule.Operator, propExpression, constant);
                    break;

                case int _:
                    binary = GetBinaryExpressionForInt(rule.Operator, propExpression, constant);
                    break;

                default:
                    throw new CustomException($"Unexpected value type {type.Name}");
            }

            return binary;
        }












    }
}
