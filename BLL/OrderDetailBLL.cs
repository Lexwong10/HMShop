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
    public class OrderDetailBLL : BaseBLL<OrderDetail>, IOrderDetailBLL
    {
        IOrderDetailDAL orderDetailDAL = new OrderDetailDAL();

        public OrderDetailBLL()
        {
            this.SetDal(orderDetailDAL);
        }
    }
}
