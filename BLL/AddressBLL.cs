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
    public class AddressBLL : BaseBLL<Address>, IAddressBLL
    {
        IAddressDAL addressDAL = new AddressDAL();

        public AddressBLL()
        {
            this.SetDal(addressDAL);
        }
    }
}
