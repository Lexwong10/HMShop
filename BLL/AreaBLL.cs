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
    public class AreaBLL : BaseBLL<Area>, IAreaBLL
    {
        IAreaDAL areaDAL = new AreaDAL();

        public AreaBLL()
        {
            this.SetDal(areaDAL);
        }
    }
}
