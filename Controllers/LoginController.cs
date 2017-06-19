using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Validate;

namespace B2CWebTemplate.Controllers
{
    public class LoginController :Controller
    {
        public IUserInfoService UserService { get; set; }
        public LoginController(IUserInfoService UserService)
        {
            this.UserService = UserService;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowValidateCode()
        {
            ValidateCode validate = new ValidateCode();
            string Code = validate.CreateValidateCode(4);
            Session["VCode"] = Code;
            byte[] imgBytes = validate.CreateValidateGraphic(Code);
            return File(imgBytes,@"image/jpeg");
        }
        public async Task<ActionResult>   BaseLogin()
        {
           string valtext= Request["validatecode"];
            string VCode = Session["VCode"] as string;
            Session["VCode"] = null;
            if (!string.IsNullOrEmpty(valtext) && !string.IsNullOrEmpty(VCode))
            {
                if (valtext != VCode)
                {
                    return Content("验证码错误");

                }
                else
                {
                    //验证用户名密码
                    string username = Request["username"];
                    string pwd = Request["pwd"];
                    string userLoginId = null;
                    int res = await UserService.Login(username, pwd, userLoginId);
                    if (res > 0)
                    {
                        userLoginId = Guid.NewGuid().ToString();
                        await ValidateCache.GetValidate().AddCache(userLoginId,username, pwd, DateTime.Now.AddMinutes(20));
                        Response.Cookies["userLoginId"].Value = userLoginId;
                        return Content("ok");
                    }
                    else {
                        return Content("用户名密码错误");
                    }
                }
            }
            else {
                return Content("验证码错误");
            }
        }
    }
}