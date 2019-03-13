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
    
    public class CartController : Controller
    {
        private ICartBLL cartBLL = new CartBLL();
        private IUserBLL userBLL = new UserBLL();
        //加入购物车
        [HttpGet]
        public ActionResult Add() {
            return View();
        }
        //加入购物车
        [HttpPost]
        public ActionResult Add(int ProductColorId, int ProductId, int ProductSizeId)
        {
            if (Session["user"] == null)
            {
                //未登录
                return Redirect("~/User/Login");
            }

            User currentUser = (Session["user"] as User);

            //判断购物车是否已有相同商品
            var result = cartBLL.GetBySituation(c => c.UserId == currentUser.Id && c.ProductId == ProductId && c.ProductColorId == ProductColorId && c.ProductSizeId == ProductSizeId);

            if (result.Count() == 1)
            {
                Cart currentCart = result.FirstOrDefault();
                currentCart.Qty++;
                cartBLL.Modify(currentCart, "Qty");
            }
            else
            {
                Cart c = new Cart
                {
                    UserId = currentUser.Id,
                    ProductId = ProductId,
                    ProductSizeId = ProductSizeId,
                    ProductColorId = ProductColorId,
                    Qty = 1
                };
                cartBLL.Add(c);
            }

            //添加购物车数量cookie
            if (cartBLL.GetBySituation(c => c.UserId == currentUser.Id).Count() == 1)
            {
                HttpCookie cartQty = new HttpCookie("cartQty", "1");
                Response.Cookies.Add(cartQty);
            }
            else {
                HttpCookie newCartQty = Request.Cookies["cartQty"];
                string newQty = (Convert.ToInt32(newCartQty.Value) + 1).ToString();
                Response.Cookies.Add(new HttpCookie("cartQty", newQty));
            }

            return Redirect("/Product/Detail/" + ProductId);
        }


        // 获取用户购物车信息
        public ActionResult List()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            User user = Session["user"] as User;
            
            var list = cartBLL.GetBySituation(c => c.UserId == user.Id);
            return View(list.ToList());
        }

        //修改购物车信息
        public ActionResult AjaxEditQty(int ProductId,int Qty , Cart cart,int Id) {
            User user = Session["user"] as User;
            cart.Id = Id;
            cart.UserId = user.Id;
            cart.ProductId = ProductId;
            cart.Qty = Qty;
            cartBLL.Modify(cart, "Id", "UserId", "ProductId", "Qty");
            return RedirectToAction("List","Cart");
        }

        //删除购物车商品
        public ActionResult AjaxDeleteItem(int Id) {
            User user = Session["user"] as User;
            cartBLL.DeleteById(Id);

            if (Request.Cookies["cartQty"].Value == "0")
            {
                Request.Cookies["cartQty"].Expires.AddDays(-1);
                Response.Cookies.Add(Request.Cookies["cartQty"]);
            }
            else {
                HttpCookie cartQty = Request.Cookies["cartQty"];
                string newQty = (Convert.ToInt32(cartQty.Value) - 1).ToString();
                Response.Cookies.Add(new HttpCookie("cartQty", newQty));
            }

            return RedirectToAction("List");
        }
    }
}