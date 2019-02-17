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
        /// <returns></returns>
        public static List<Models.Drugs> getDrugsList(string status)
        {
            var list_drugs = new List<Models.Drugs>();
            using (DataContext dc = new DataContext(common.conn))
            {
                if ("1".Equals(status))//已过期
                {
                    var _list_drugs = from x in dc.GetTable<Drugs>()
                                     where x.RemainDay == 0
                                     select x;
                    list_drugs = _list_drugs.ToList();
                    
                }
                else if ("2".Equals(status))//即将过期（标准为过期前两个月）
                {
                    var _list_drugs = from x in dc.GetTable<Drugs>()
                                     where x.RemainDay == 30
                                     select x;
                    list_drugs = _list_drugs.ToList();
                    
                }
                //else if("3".Equals(status))//其他药品
                //{
                //    var _list_drugs = from x in dc.GetTable<Drugs>()
                //                     where x.OwnerId.ToString() == ownerId && x.RemainDay > 60
                //                     select x;
                //    list_drugs = _list_drugs.ToList();

                //}
            }
            if(list_drugs != null)
            {
              
                //IOrderedEnumerable<Drugs> query;

                //query = list_drugs.OrderBy(c => c.RemainDay);
                //int pageSize = 8;
                //var results = query
                //              .Skip(pageSize * (page-1))
                //              .Take(pageSize)
                //              .ToList();
                return list_drugs;
            }
            else throw (new Exception("没有找过对应药品"));

        }
        /// <summary>
        /// 返回所有药品
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<Models.Drugs> getDrugsList(string ownerId, int page)
        {
            var list_drugs = new List<Models.Drugs>();
            using (DataContext dc = new DataContext(common.conn))
            {

                var _list_drugs = from x in dc.GetTable<Drugs>()
                                  where x.OwnerId.ToString() == ownerId
                                  select x;
                list_drugs = _list_drugs.ToList();
            }
            if (list_drugs != null)
            {

                IOrderedEnumerable<Drugs> query;

                query = list_drugs.OrderBy(c => c.RemainDay);
                int pageSize = 8;
                var results = query
                              .Skip(pageSize * (page - 1))
                              .Take(pageSize)
                              .ToList();
                return results;
            }
            else throw (new Exception("没有找过对应药品"));

        }
        /// <summary>
        /// 根据关键字推荐药品
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<string> drugSuggestion(string key,string ownerId)
        {
            var list_drugs_name = new List<string>();
            using (DataContext dc = new DataContext(common.conn))
            {

                var _list_drugs_name = from x in dc.GetTable<Drugs>()
                                  where x.Name.Contains(key) && x.OwnerId.ToString() == ownerId
                                  select x.Name;
                list_drugs_name = _list_drugs_name.ToList();
            }
            if (list_drugs_name != null)
            {
                return list_drugs_name;
            }
            else throw (new Exception("没有找过对应药品"));

        }
        /// <summary>
        /// 搜索药品
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Models.Drugs> searchDrug(string name,string ownerId)
        {
            var list_drugs = new List<Models.Drugs>();
            using (DataContext dc = new DataContext(common.conn))
            {

                var _list_drugs = from x in dc.GetTable<Drugs>()
                                       where x.Name.Contains(name) && x.OwnerId.ToString() == ownerId
                                  select x;
                list_drugs = _list_drugs.ToList();
            }
            if (list_drugs != null)
            {
                IOrderedEnumerable<Drugs> query;

                query = list_drugs.OrderBy(c => c.RemainDay);
                int pageSize = 8;
                var results = query
                              .Take(pageSize)
                              .ToList();
                return results;
            }
            else throw (new Exception("没有找过对应药品"));

        }

        /// <summary>
        /// 删除药品
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Models.Drugs> deleteDrug(string ownerId, string id)
        {
            if (null == ownerId || null == id) {
                throw (new Exception("参数缺失"));
            }
            else {
                using (DataContext dc = new DataContext(common.conn))
                {
                    var tb = dc.GetTable<Models.Drugs>();
                    var _drug = (from x in tb
                                 where x.ID.Equals(id) && x.OwnerId.ToString().Equals(ownerId)
                                 select x).First();
                    tb.DeleteOnSubmit(_drug);
                    dc.SubmitChanges();
                    List<Models.Drugs> list_drug = new List<Drugs>();
                    list_drug.Add(_drug);
                    return list_drug;
                }
            }
        }


        /// <summary>
        /// 添加药品
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<Models.Drugs> addDrug(string name, string effect, string data,string remark,string ownerId)
        {
            if (null == effect|| null == name|| null == data || null == ownerId)
            {
                throw (new Exception("请将药品信息填写完整"));
            }
            else
            {
                using (DataContext dc = new DataContext(Dap.common.conn))
                {
                    var tb = dc.GetTable<Models.Drugs>();
                    Models.Drugs drug = new Models.Drugs();
                    drug.Name = name;
                    drug.Effect = effect;
                    drug.ExpirationDate = data;
                    if (null != remark)
                    {
                        drug.Remark = remark;
                        
                    }
                    drug.OwnerId = new Guid(ownerId);
                    drug.ID = System.Guid.NewGuid();
                    DateTime t = DateTime.Now;
                    DateTime t1 = Convert.ToDateTime(data);
                    TimeSpan ts = t1 - t;
                    int d = ts.Days;
                    drug.RemainDay = d;
                    tb.InsertOnSubmit(drug);
                    dc.SubmitChanges();//后台自动生成用户ID
                    List<Models.Drugs> list_drug = new List<Drugs>();
                    list_drug.Add(drug);
                    return list_drug;
                }
            }
        }
    }

}
