using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using Models;

namespace Dap
{
    public class drugs
    {
     
        /// <summary>
        /// 根据过期状态返回对应药品列表
        /// </summary>
        /// <param name="status">1：已过期 2：即将过期（过期时间<=60) 3:其他药品</param>
        /// <param name="ownerId">上传者userId</param>
        /// <param name="page">查询第几页（默认每页有8个数据）</param>
        /// <returns></returns>
        public static List<Models.Drugs> getDrugsList(string status,string ownerId,int page)
        {
            var list_drugs = new List<Models.Drugs>();
            using (DataContext dc = new DataContext(common.conn))
            {
                if ("1".Equals(status))//已过期
                {
                    var _list_drugs = from x in dc.GetTable<Drugs>()
                                     where x.OwnerId.ToString() == ownerId && x.RemainDay <= 0
                                     select x;
                    list_drugs = _list_drugs.ToList();
                    
                }
                else if ("2".Equals(status))//即将过期（标准为过期前两个月）
                {
                    var _list_drugs = from x in dc.GetTable<Drugs>()
                                     where x.OwnerId.ToString() == ownerId && x.RemainDay > 0 && x.RemainDay <= 60
                                     select x;
                    list_drugs = _list_drugs.ToList();
                    
                }
                else if("3".Equals(status))//其他药品
                {
                    var _list_drugs = from x in dc.GetTable<Drugs>()
                                     where x.OwnerId.ToString() == ownerId && x.RemainDay > 60
                                     select x;
                    list_drugs = _list_drugs.ToList();

                }
            }
            if(list_drugs != null)
            {
              
                IOrderedEnumerable<Drugs> query;

                query = list_drugs.OrderBy(c => c.RemainDay);
                int pageSize = 8;
                var results = query
                              .Skip(pageSize * (page-1))
                              .Take(pageSize)
                              .ToList();
                return results;
            }
            else throw (new Exception("没有找过对应药品"));

        }
    }
}
