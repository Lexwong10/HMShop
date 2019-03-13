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
    public class OrderDetailController : Controller
    {
        public IOrderDetailBLL orderDetailBLL = new OrderDetailBLL();
        public ICartBLL cartBLL = new CartBLL();
        public IOrderBLL orderBLL = new OrderBLL();

        //添加订单详情，清除购物车，改变订单状态
        public ActionResult Add(int[] ProductId , int OrderId , int[] Qty , decimal[] Price,OrderDetail orderDetail)
        {
            User user = Session["user"] as User;

            //添加订单详情
            orderDetail.OrderId = OrderId;
            for (int i = 0; i < ProductId.Length; i++)
            {
                orderDetail.ProductId = ProductId[i];
                orderDetail.Qty = Qty[i];
                orderDetail.Price = Price[i];
                orderDetailBLL.Add(orderDetail);
            }

            //删除购物车
            cartBLL.DeleteBySituation(c => c.UserId == user.Id);

            //改变订单状态
            var orderToChange = orderBLL.GetBySituation(o => o.OrderStatus == 1&& o.UserId == user.Id).FirstOrDefault();
            orderToChange.OrderStatus = 2;
            orderBLL.Modify(orderToChange, "OrderStatus");

            //清除购物车数量
            Request.Cookies["cartQty"].Expires.AddDays(-1);
            Response.Cookies.Add(Request.Cookies["cartQty"]);

            return RedirectToAction("PaySucess");
        }

        public ActionResult PaySucess() {
            return View();
        }
    }
}