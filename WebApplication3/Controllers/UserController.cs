using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using System.Drawing;
using WebApplication3.ViewModels;
using IBLL;
using BLL;

namespace WebApplication3.Controllers
{
    public class UserController : Controller
    {
        public IUserBLL userBLL = new UserBLL();

        #region 用户注册
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        //写入数据库
        [HttpPost]
        public ActionResult Register(ViewModels.User user) {
            Model.User u = new Model.User
            {
                Email = user.Email,
                Password = Common.Encrypt.Md5(user.Password),
                Nickname = user.NickName
            };
            userBLL.Add(u);
            return Content("");
        }

        //通过Ajax验证表单是否正确
        [HttpPost]
        public ActionResult AjaxRegister(ViewModels.User user)
        {
            //验证验证码
            if (user.Captcha == "" || user.Captcha.ToLower() != Session["Captcha"].ToString().ToLower())
            {
                return Json(new Common.JsonResult
                {
                    Result = ResultType.Error,
                    ErrorTips = "验证码错误"
                });
            }

            //判断用户名是否存在
            int userCount = userBLL.GetBySituation(u => u.Email == user.Email).Count();

            if (userCount == 1)
            {
                return Json(new Common.JsonResult
                {
                    Result = ResultType.Error,
                    ErrorTips = "用户名已存在"
                });
            }

            return Json(new Common.JsonResult
            {
                Result = ResultType.Success,
                ErrorTips = string.Empty
            });

        }

        //验证码
        public ActionResult CheckCaptcha() {
            Captcha c = new Captcha();
            string CaptchaCode = c.GetCaptchaCode(4);
            Session.Add("Captcha", CaptchaCode);
            Image img = c.GetCaptchaImg(CaptchaCode);

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return File(ms.ToArray(),"image/jpeg");
        }
        #endregion

        #region 用户登陆
        [HttpGet]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Model.User user)
        {
            user.Password = Common.Encrypt.Md5(user.Password);
            
            var users = userBLL.GetBySituation(u => u.Email == user.Email &&
           u.Password == user.Password);
            var loginResult = users.Count() == 1;
            if (loginResult)
            {
                //登陆成功
                Session.Add("user", users.FirstOrDefault());
                return RedirectToAction("Index", "Index");
            }
            else
            {
                return RedirectToAction("List", "Product");
            }
        }

        public ActionResult AjaxLogin(Model.User user) {
            user.Password = Common.Encrypt.Md5(user.Password);
            int userCount = userBLL.GetBySituation(u => u.Password == user.Password).Count();
            if (userCount == 0) {
                return Json(new Common.JsonResult
                {
                    Result = ResultType.Error,
                    ErrorTips = "用户名或密码错误"
                });
            }
            else
            {
                return Json(new Common.JsonResult
                {
                    Result = ResultType.Success,
                    ErrorTips = string.Empty
                });
            }
        }

        //退出登陆
        public ActionResult Logout() {
            Session.Remove("user");
            return RedirectToAction("Index", "Index");
        }
        #endregion

        #region 个人概况
        public ActionResult MyAccount() {
            if (Session["user"] == null)
            {
                return RedirectToAction("login", "User");
            }
            Model.User u = Session["user"] as Model.User;

            Model.User user =  userBLL.GetById(u.Id);
            return View(user);
        }
        #endregion

        #region 个人信息
        public ActionResult MyProfile() {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            Model.User u = Session["user"] as Model.User;
            Model.User user = userBLL.GetById(u.Id);
            return View(user);
        }

        //个人信息修改
        public ActionResult Edit(Model.User user) {
            Model.User u = Session["user"] as Model.User;

            user.Id = u.Id;
            user.Password = u.Password;
            userBLL.Modify(user,"Id", "Email","Password", "Nickname");

            return RedirectToAction("MyProfile");
        }
        #endregion


    }
}