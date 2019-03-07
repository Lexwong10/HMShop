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
    public class UserBLL : BaseBLL<User>, IUserBLL
    {
        IUserDAL userDAL = new UserDAL();

        public UserBLL() {
            this.SetDal(userDAL);
        }


    }
}
