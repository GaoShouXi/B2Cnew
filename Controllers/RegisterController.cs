using IBLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace B2CWebTemplate.Controllers
{
    public class RegisterController : Controller
    {
        public IUserInfoService UserService { get; set; }
        public RegisterController(IUserInfoService UserService)
        {
            this.UserService = UserService;
        }
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> RegisterMaker()
        {
            string end = "??";
           string emailname =Request["emailname"].ToString();
            string pwd = Request["pwd"].ToString();
            string pwd1 = Request["pwd1"].ToString();
            if (pwd == pwd1)
            {
                UserInfo user = new UserInfo();
                user.UserName = emailname;
                IEnumerable<UserInfo> list = new List<UserInfo>();
                list = await UserService.SelectEntityById<UserInfo>(user, "UserName");
                if (list!=null)
                {
                    Response.Write("had");
                }
                else {
                    user.UserName = emailname;
                    user.NickName = emailname;
                    user.PassWord = pwd;
                    user.JiFen = 0;
                    user.YuE = 0;
                    user.DelFlag = 0;
                    user.Modifedon = "";
                    int res = await UserService.AddEntity<UserInfo>(user);
                    if (res > 0)
                    {
                        end= "ok";
                        
                    }
                    else { end = "bad"; }
                    
                }
                
            }
            else {
                end = "dif";
                
            }
            return Content(end);
        }
    }
}