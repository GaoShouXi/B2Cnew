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
            ProductionInfo pro = new ProductionInfo();
            ProductTypeInfo pt = new ProductTypeInfo();
            SellerInfo s = new SellerInfo();
            ProductionActivityInfo pa = new ProductionActivityInfo();
            IEnumerable<ProductTypeInfo> ptlist = new List<ProductTypeInfo>();
            IEnumerable<ProductionInfo> prlist = new List<ProductionInfo>();
            IEnumerable<SellerInfo> slist = new List<SellerInfo>();
            IEnumerable<ProductionActivityInfo> palist = new List<ProductionActivityInfo>();
            ptlist = await UserInfoService.SelectAllEntities<ProductTypeInfo>(pt);
            prlist = await UserInfoService.SelectAllEntities<ProductionInfo>(pro);
            slist = await UserInfoService.SelectAllEntities<SellerInfo>(s);
            palist = await UserInfoService.SelectAllEntities<ProductionActivityInfo>(pa);
            ViewBag.TypeMenu = ptlist;
            ViewBag.ProMenu = prlist;
            ViewBag.SellerMenu = slist;
            ViewBag.PaMenu = palist;
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