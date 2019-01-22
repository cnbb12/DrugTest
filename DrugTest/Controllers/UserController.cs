using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Dap;

namespace DrugTest.Controllers
{
    public class UserController : ApiController
    {
        /// <summary>
        /// 检查登录是否成功
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public RESULT CheckLogin()
        {
                
            RESULT result = new RESULT();
            List<object> list = new List<object>();
            try
            {
                string phoneNumber, key;

                phoneNumber = HttpContext.Current.Request.Headers["phoneNumber"];
                key = HttpContext.Current.Request.Headers["key"];
                
                if (phoneNumber == null && key == null)
                {
                    phoneNumber = HttpContext.Current.Request.Form["phoneNumber"];
                    key = HttpContext.Current.Request.Form["key"];
                }
                object ob = Dap.user.checkLogin(phoneNumber, key);
                list.Add(ob);
                
                result.result = list;
            }
            catch (Exception e)
            {
                result.state = false;
                result.msg = e.Message;
            }
            return result;
        }
    }
}
