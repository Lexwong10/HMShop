using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class AddressController : Controller
    {
        public IAddressBLL addressBLL = new AddressBLL();
        public IAreaBLL adreaBLL = new AreaBLL();

        //显示我的地址簿
        public ActionResult MyAddress()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            User u = Session["user"] as User;
            ViewBag.Areas = adreaBLL.GetBySituation(a => a.ParentId == null).ToList();

            return View(addressBLL.GetBySituation(a=>a.UserId == u.Id).ToList());
        }

        //修改地址
        [HttpGet]
        public ActionResult Edit(Address address) {

            User user = Session["user"] as User;

           var currentAddress =  addressBLL.GetById(address.Id);
            currentAddress.Receiver = address.Receiver;
            currentAddress.Phone = address.Phone;
            currentAddress.AreaId = address.AreaId;
            currentAddress.Address1 = address.Address1;

            if (address.Default == false)
            {
                addressBLL.Modify(currentAddress, "UserId", "Receiver", "Phone", "AreaId", "Address1");
            }
            else {
                var defaultAddress = addressBLL.GetBySituation(a => a.UserId == user.Id).ToList();
                foreach (var a in defaultAddress)
                {
                    a.Default = false;
                    addressBLL.Modify(a, "Default");
                }

                currentAddress.Default = true;
                addressBLL.Modify(currentAddress, "UserId", "Receiver", "Phone", "AreaId", "Address1", "Default");
            }

            return RedirectToAction("MyAddress");
        }

        //增加地址
        [HttpPost]
        public ActionResult Add(Address address) {

            User user = Session["user"] as User;

            address.UserId = user.Id;

            if (address.Default == null)
            {
                address.Default = false;
            }

            addressBLL.Add(address);

            return RedirectToAction("MyAddress");
        }

        //删除地址
        [HttpGet]
        public ActionResult DeleteAddress(Address address) {
            addressBLL.DeleteById(address.Id);
            return RedirectToAction("MyAddress");
        }
    }
}