using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B2CWebTemplate.Models
{
    public partial class ExceptionLogInfo:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            LogService.LogHelper.WriteLog(filterContext.Exception.ToString());
        }
    }
}