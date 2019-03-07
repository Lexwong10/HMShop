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
    public class CartBLL : BaseBLL<Cart>,ICartBLL
    {
        ICartDAL carDAL = new CartDAL();

        public CartBLL() {
            this.SetDal(carDAL);
        }
    }
}
