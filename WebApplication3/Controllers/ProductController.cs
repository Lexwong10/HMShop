using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 4;

        public IProductBLL productBLL = new ProductBLL();

        public IProductTypeBLL ProductType = new ProductTypeBLL();

        // GET: Product
        public ActionResult List(int? TypeId, int? OrderId, int? Page)
        {
            string orderField = null;
            bool asc = true;
            if (Page == null)
            {
                Page = 1;
            }
            if (OrderId == null)
            {
                OrderId = 1;
            }

            switch (OrderId)
            {
                case 1:
                    orderField = "Id";
                    asc = true;
                    break;
                case 2:
                    orderField = "Sold";
                    asc = false;
                    break;
                case 3:
                    orderField = "OldPrice";
                    asc = true;
                    break;
                case 4:
                    orderField = "OldPrice";
                    asc = false;
                    break;
            }

            Expression<Func<Product, bool>> whereExpression = p => p.OnSale ==1;
            if (TypeId != null)
            {
                whereExpression = p => p.TypeId == TypeId || p.ProductType.ProductType2.Id == TypeId &&p.OnSale == 1;
                ViewBag.CurrentProductType = ProductType.GetById(TypeId) ;
            }

            var ProductCount = productBLL.GetBySituation(whereExpression).Count();
            var totalPages =  Math.Ceiling(ProductCount * 1.0 / PAGE_SIZE);
            ViewBag.TotalPages = totalPages;
            


            //输入商品一级类型
            ViewBag.ProductType = ProductType.GetBySituation(t => t.Pid == null);
            ViewBag.OrderId = OrderId;
            ViewBag.PageSize = PAGE_SIZE;
            ViewBag.Page = Page;

            var list = productBLL.GetByPage(
                PAGE_SIZE,
                Page,
                asc,
                orderField,
                whereExpression
                );

            return View(list.ToList());
        }

        //商品详情
        public ActionResult Detail(int id)
        {
            Product p = productBLL.GetById(id);
            return View(p);
        }

    }
}