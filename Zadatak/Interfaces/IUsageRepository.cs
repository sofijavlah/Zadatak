using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Zadatak.DTOs.Device;
using Zadatak.Expressions;
using Zadatak.Models;

namespace Zadatak.Interfaces
{
    /// <summary>
    /// Interface for Usage Repository
    /// </summary>
    ///
    ///<seealso>
    ///     <cref>Repositories.IRepository{Models.DeviceUsage}</cref>
    /// </seealso>
    public interface IUsageRepository : IRepository<DeviceUsage>
    {
        /// <summary>
        /// Adds the specified device.
        /// </summary>
        /// <param name="device">The device.</param>
        void Add(Device device);

        BinaryExpression GetBinaryExpressionForString(string operand, MemberExpression propExpression,
            ConstantExpression constant);

        BinaryExpression GetBinaryExpressionForInt(string operand, MemberExpression propExpression,
            ConstantExpression constant);

        Expression<Func<TEntity, bool>> GetFilterExpression<TEntity>(ParameterExpression parameter,FilterInfo filterInfo);

        Expression<Func<TEntity, object>> GetOrderExpression<TEntity>(ParameterExpression parameter, SortInfo sortInfo);

        IQueryable<TEntity> GetSorted<TEntity>(QueryInfo queryInfo, List<TEntity> list);
    }
}
