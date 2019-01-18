using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dap;

namespace DrugTest.Controllers
{
    public class UserController : ApiController
    {
        /// <summary>
        /// 检查登录是否成功
        /// </summary>
        /// <param name="phoneNumber"></param>g
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public RESULT CheckLogin(string phoneNumber, string key)
        {
            RESULT result = new RESULT();
            try
            {
                result.result = Dap.user.checkLogin(phoneNumber, key);
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
