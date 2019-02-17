using Dap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
                //if (null != status)
                //{
                //    result.result = Dap.drugs.getDrugsList(status, ownerId, int.Parse(page));
                //}
                //else
                //{
                    result.result = Dap.drugs.getDrugsList(ownerId, int.Parse(page));

                //}
                
            }
            catch (Exception e)
            {
                result.state = false;
                result.msg = e.Message;
            }
            return result;
        }
        /// <summary>
        /// 根据关键字推荐药品
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        public RESULT DrugSuggestion()
        {
            RESULT result = new RESULT();
            try
            {
                string key,id;

                key = HttpContext.Current.Request.Form["key"];
                id = HttpContext.Current.Request.Form["id"];
                if (null != key)
                {
                    result.result = Dap.drugs.drugSuggestion(key,id);
                }
                else
                {
                    throw new Exception("请输入关键字");
                }

            }
            catch (Exception e)
            {
                result.state = false;
                result.msg = e.Message;
            }
            return result;

        }
        /// <summary>
        /// 查找药品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public RESULT SearchDrug()
        {
            RESULT result = new RESULT();
            try
            {
                string name,id;

                name = HttpContext.Current.Request. Form["name"];
                id = HttpContext.Current.Request.Form["id"];
                if (null != name)
                {
                    result.result = Dap.drugs.searchDrug(name,id);
                }
                else
                {
                    throw new Exception("请输入关键字");
                }

            }
            catch (Exception e)
            {
                result.state = false;
                result.msg = e.Message;
            }
            return result;

        }

        /// <summary>
        /// 删除药品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public RESULT DeleteDrug()
        {
            RESULT result = new RESULT();
            try
            {
                string ownerId, id;

                id = HttpContext.Current.Request.Form["id"];
                ownerId = HttpContext.Current.Request.Form["ownerId"];
                result.result = Dap.drugs.deleteDrug(ownerId, id);
            }
            catch (Exception e)
            {
                result.state = false;
                result.msg = e.Message;
            }
            return result;

        }
        /// <summary>
        /// 添加药品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public RESULT AddDrug()
        {
            RESULT result = new RESULT();
            try
            {
                string name, effect, data, remark, ownerId;

                name = HttpContext.Current.Request.Form["name"];
                ownerId = HttpContext.Current.Request.Form["ownerId"];
                effect = HttpContext.Current.Request.Form["effect"];
                data = HttpContext.Current.Request.Form["data"];
                remark = HttpContext.Current.Request.Form["instruction"];
                result.result = Dap.drugs.addDrug(name, effect,data,remark,ownerId);
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
