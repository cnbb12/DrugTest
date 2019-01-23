using Dap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DrugTest.Controllers
{
    public class DrugsController : ApiController
    {
        /// <summary>
        /// 根据查询的过期状态返回对应的药品列表
        /// </summary>
        /// <returns></returns>
        
        [HttpPost]
        public RESULT GetDrugsList()
        {
            RESULT result = new RESULT();
            try
            {
                string status;
                string ownerId;

                status = HttpContext.Current.Request.Form["status"];
                ownerId = HttpContext.Current.Request.Form["ownerId"];
                result.result = Dap.drugs.getDrugsList(status,ownerId);               
                
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
