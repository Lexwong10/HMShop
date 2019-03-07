using BLL;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class AreaController : Controller
    {
        public IAreaBLL areaBLL = new AreaBLL();

        /// <summary>
        /// 获取各级别区域信息
        /// </summary>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        public ActionResult List( int? AreaId)
        {
            return Json(
                areaBLL.GetBySituation(a => a.ParentId == AreaId).Select(a => new {
                    Id = a.Id,
                    AreaName = a.AreaName,
                    ParentId = a.ParentId
                }),
                JsonRequestBehavior.AllowGet
             );
        }
    }
}