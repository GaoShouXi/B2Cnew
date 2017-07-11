using IBLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        public async  Task<ActionResult> Index()
        {
            //拿到cookie的值传进来
            //UserInfoService.Login();
            ProductionInfo pro = new ProductionInfo();
            ProductTypeInfo pt = new ProductTypeInfo();
            IEnumerable<ProductTypeInfo> ptlist = new List<ProductTypeInfo>();
            IEnumerable<ProductionInfo> prlist = new List<ProductionInfo>();

            ptlist =await UserInfoService.SelectAllEntities<ProductTypeInfo>(pt);
            
            prlist =await UserInfoService.SelectAllEntities<ProductionInfo>(pro);
          
            ViewBag.TypeMenu = ptlist;
            ViewBag.ProMenu = prlist;
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