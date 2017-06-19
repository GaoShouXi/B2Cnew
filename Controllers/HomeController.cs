using IBLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B2CWebTemplate.Controllers
{
    public class HomeController : Controller
    {
        public IUserInfoService UserInfoService;
        public HomeController(IUserInfoService UserService)
        {
            this.UserInfoService = UserService;
        }
        public ActionResult Index()
        {
            //拿到cookie的值传进来
            //UserInfoService.Login();
            UserInfo userTest = new UserInfo();
            UserInfoService.AddEntity<UserInfo>(userTest);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}