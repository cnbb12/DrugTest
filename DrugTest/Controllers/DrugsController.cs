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
        /// 根据过期状态（已过期、即将过期、安全）返回对应的药品列表（分页）
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
                string  page;

                status = HttpContext.Current.Request.Form["status"];
                ownerId = HttpContext.Current.Request.Form["ownerId"];
                page = HttpContext.Current.Request.Form["page"];
                result.result = Dap.drugs.getDrugsList(status,ownerId,int.Parse(page));               
                
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
