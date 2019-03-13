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
    public class IndexController : Controller
    {
        private ICartBLL carBLL = new CartBLL();
        private IProductTypeBLL productTypeBLL = new ProductTypeBLL();
        // GET: Index
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                User u = Session["user"] as User;
                var myCart = carBLL.GetBySituation(c => c.UserId == u.Id); 
                //int? cartCount = 0;

                //foreach (var item in myCart)
                //{
                //    cartCount += item.Qty;
                //}
                //HttpCookie cartQty = new HttpCookie("cartQty", cartCount.ToString());
                //Response.Cookies.Add(cartQty);
                //ViewBag.cartCount = cartCount;
            }
            
            return View(productTypeBLL.GetBySituation(t => t.Pid == null).ToList());
        }
    }
}