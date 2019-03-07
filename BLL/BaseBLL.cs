using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BaseBLL<T> where T : class, new()
    {
        private IBaseDAL<T> dal;

        public void SetDal(IBaseDAL<T> dal) {
            this.dal = dal;
        }

        public void Add(T t)
        {
            dal.Add(t);
        }

        public void DeleteById(int id)
        {
            dal.DeleteById(id);
        }

        public void DeleteBySituation(Expression<Func<T, bool>> deleteWhere)
        {
            dal.DeleteBySituation(deleteWhere);
        }


        public void Modify(T entity, params string[] propName)
        {
            dal.Modify(entity, propName);
        }

        public IQueryable<T> GetAll()
        {
            return dal.GetAll();
        }

        public int GetCount()
        {
            return dal.GetAll().Count();
        }


        public int GetCount(Expression<Func<T, bool>> whereExpression)
        {
            return dal.GetCount();
        }

        public T GetById(int? id)
        {
            return dal.GetById(id);
        }

        public IQueryable<T> GetBySituation(Expression<Func<T, bool>> whereExpression)
        {
            return dal.GetBySituation(whereExpression);
        }

        public IQueryable<T> GetByPage<type>(int pageSize, int? currentPage, bool isAsc, Expression<Func<T, type>> orderExpression, Expression<Func<T, bool>> whereExpression) {
            return dal.GetByPage(pageSize, currentPage, isAsc, orderExpression, whereExpression);
        }


        public IQueryable<T> GetByPage(int pageSize, int? currentPage, bool isAsc, string orderFieldName, Expression<Func<T, bool>> whereExpression)
        {
            return dal.GetByPage(pageSize, currentPage, isAsc, orderFieldName, whereExpression);
        }
    }
}
