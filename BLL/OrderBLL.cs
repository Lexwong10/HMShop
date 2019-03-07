using DAL;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderBLL : BaseBLL<Order>, IOrderBLL
    {
        IOrderDAL orderDAL = new OrderDAL();

        public OrderBLL()
        {
            this.SetDal(orderDAL);
        }
    }
}
