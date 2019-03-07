using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common;
using IDAL;
using System.Data.Entity;

namespace DAL
{
    public class BaseDAL<T> where T : class, new()
    {
        protected HM2Entities db = new HM2Entities();

        public T Add(T entity) {
            db.Set<T>().Add(entity);
            db.SaveChanges();
            return db.Set<T>().Attach(entity);
        }

        public void DeleteById(int id) {
            db.Set<T>().Remove(db.Set<T>().Find(id));
            db.SaveChanges();
        }

        public void DeleteBySituation(Expression<Func<T, bool>> deleteWhere) {
            db.Set<T>().RemoveRange(db.Set<T>().Where(deleteWhere));
            db.SaveChanges();
        }


        public void Modify(T entity, params string[] propName) {
            db.Set<T>().Attach(entity);
            for (int i = 0; i < propName.Length; i++)
            {
                db.Entry(entity).Property(propName[i]).IsModified = true;
            }
            db.SaveChanges();
        }

        public IQueryable<T> GetAll() {
            return db.Set<T>();
        }


        
        public int GetCount() {
            return this.GetAll().Count();
        }


        public int GetCount(Expression<Func<T, bool>> whereExpression) {
            return db.Set<T>().Where(whereExpression).Count();
        }


        public T GetById(int? id) {
            return db.Set<T>().Find(id);
        }

        public IQueryable<T> GetBySituation(Expression<Func<T, bool>> whereExpression) {
            return db.Set<T>().Where(whereExpression);
        }

        /// <summary>
        /// 分页，筛选，排序，查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetByPage<type>(int pageSize, int? currentPage, bool isAsc, Expression<Func<T, type>> orderExpression, Expression<Func<T, bool>> whereExpression) {
            if (currentPage == null)
            {
                currentPage = 1;

            }
            if (isAsc)
            {
                return db.Set<T>().Where(whereExpression).OrderBy(orderExpression).Skip(Convert.ToInt32((currentPage - 1) * pageSize)).Take(pageSize);
            }
            else {
                return db.Set<T>().Where(whereExpression).OrderByDescending(orderExpression).Skip(Convert.ToInt32((currentPage - 1) * pageSize)).Take(pageSize);
            }
        }

        public IQueryable<T> GetByPage(int pageSize, int? currentPage, bool isAsc, string orderFieldName, Expression<Func<T, bool>> whereExpression) {
            if (currentPage == null)
            {
                currentPage = 1;
                
            }
                return db.Set<T>().Where(whereExpression).OrderBy(orderFieldName, isAsc).Skip(Convert.ToInt32((currentPage - 1) * pageSize)).Take(pageSize);
        }
    }

    public partial class UserDAL : BaseDAL<User>, IUserDAL { }
    public partial class ProductDAL : BaseDAL<Product> , IProductDAL { }
    public partial class CartDAL : BaseDAL<Cart> , ICartDAL { }
    public partial class ProductTypeDAL : BaseDAL<ProductType>, IProductTypeDAL { }
    public partial class AddressDAL : BaseDAL<Address>, IAddressDAL { }
    public partial class OrderDAL : BaseDAL<Order>, IOrderDAL { }
    public partial class AreaDAL : BaseDAL<Area>, IAreaDAL { }
    public partial class OrderDetailDAL : BaseDAL<OrderDetail>, IOrderDetailDAL { }
}
