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
    public class ProductBLL : BaseBLL<Product> , IProductBLL
    {
        IProductDAL productDAL = new ProductDAL();

        public ProductBLL() {
            this.SetDal(productDAL);
        }
    }
}
