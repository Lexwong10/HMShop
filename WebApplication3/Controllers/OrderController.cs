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
    public class OrderController : Controller
    {
        public IOrderBLL orderBLL = new OrderBLL();
        public ICartBLL cartBLL = new CartBLL();
        
        #region 我的订单
        public ActionResult MyOrder()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("login", "User");
            }
            Model.User u = Session["user"] as Model.User;

            return View(orderBLL.GetBySituation(o => o.UserId == u.Id).ToList());
        }
        #endregion

        #region 添加订单
        public ActionResult Add(int AddressId , decimal Price,Order order) {
            User user = Session["user"] as User;

            var result =  orderBLL.GetBySituation(o => o.OrderStatus == 1 && o.UserId == user.Id);
           
            if (result.Count() == 1)
            {
                result.FirstOrDefault().OrderTime = DateTime.Now;
                result.FirstOrDefault().AddressId = AddressId;
                result.FirstOrDefault().Price = Price;
                orderBLL.Modify(result.FirstOrDefault(), "OrderTime", "AddressId", "Price");
            }
            else {
                order.UserId = user.Id;
                order.OrderTime = DateTime.Now;
                order.AddressId = AddressId;
                order.Price = Price;
                order.OrderStatus = 1;
                orderBLL.Add(order);
            }
            return RedirectToAction("Checkout");
        }
        #endregion


        //确认订单
        public ActionResult Checkout() {
            User user = Session["user"] as User;
            var list = orderBLL.GetBySituation(c => c.OrderStatus == 1 && c.UserId == user.Id);
            ViewBag.currentCart = cartBLL.GetBySituation(c => c.UserId == user.Id).ToList();
            return View(list.ToList());
        }

    }
}