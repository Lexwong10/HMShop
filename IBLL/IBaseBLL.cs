using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IBaseBLL<T> where T : class, new()
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Add(T entity);

        /// <summary>
        /// 根据主键删
        /// </summary>
        /// <param name="id"></param>
        void DeleteById(int id);

        /// <summary>
        /// 根据条件lambda表达式删除
        /// </summary>
        /// <param name="deleteWhere"></param>
        void DeleteBySituation(Expression<Func<T, bool>> deleteWhere);

        /// <summary>
        /// 根据属性名称进行修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propName"></param>
        void Modify(T entity, params string[] propName);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// 获取数据总条数
        /// </summary>
        /// <returns></returns>
        int GetCount();

        /// <summary>
        /// 根据条件获取总条数
        /// </summary>
        /// <param name="whereExpression">筛选条件</param>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 根据主键获取商品数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        T GetById(int? id);

        /// <summary>
        /// 根据筛选条件拿出说有数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        IQueryable<T> GetBySituation(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 分页，筛选，排序，查询
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetByPage<type>(int pageSize, int? currentPage, bool isAsc, Expression<Func<T, type>> orderExpression, Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 扩展中的方法
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="isAsc"></param>
        /// <param name="orderFieldName"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        IQueryable<T> GetByPage(int pageSize, int? currentPage, bool isAsc, string orderFieldName, Expression<Func<T, bool>> whereExpression);
    }
    
    public partial interface IUserBLL : IBaseBLL<User> { }
    public partial interface IProductBLL : IBaseBLL<Product> { }
    public partial interface ICartBLL : IBaseBLL<Cart> { }
    public partial interface IProductTypeBLL : IBaseBLL<ProductType> { }
    public partial interface IAddressBLL : IBaseBLL<Address> { }
    public partial interface IOrderBLL : IBaseBLL<Order> { }
    public partial interface IAreaBLL : IBaseBLL<Area> { }
    public partial interface IOrderDetailBLL : IBaseBLL<OrderDetail> { }
}

