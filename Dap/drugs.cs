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
                DateTime t = DateTime.Now;
                if ("1".Equals(status))//已过期
                {
                    var _list_drugs = from x in dc.GetTable<Drugs>()
                                     where (Convert.ToDateTime(x.ExpirationDate) - t).Days < 1
                                      select x;
                    list_drugs = _list_drugs.ToList();
                    
                }
            }
            if(list_drugs != null)
            {
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
                                       where (x.Name.Contains(name) || x.Effect.Contains(name)) && x.OwnerId.ToString() == ownerId
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
        /// 更新药品信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="effect"></param>
        /// <param name="data"></param>
        /// <param name="remark"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public static dynamic updateDrug(string id, string name, string effect, string data, string remark, string ownerId)
        {
            if (null == effect || null == name || null == data || null == ownerId)
            {
                throw (new Exception("请将药品信息填写完整"));
            }
            else
            {
                using (DataContext dc = new DataContext(Dap.common.conn))
                {
                    var _drug = (from x in dc.GetTable<Drugs>()
                                      where x.ID.ToString() == id
                                      select x).First();
                    Models.Drugs drug = _drug;

         
                    drug.Name = name;
                    drug.Effect = effect;
                    drug.ExpirationDate = data;
                    if (null != remark)
                    {
                        drug.Remark = remark;

                    }
                    DateTime t = DateTime.Now;
                    DateTime t1 = Convert.ToDateTime(data);
                    TimeSpan ts = t1 - t;
                    int d = ts.Days;
                    drug.RemainDay = d;
                    dc.SubmitChanges();//后台自动生成用户ID
                    List<Models.Drugs> list_drug = new List<Drugs>();
                    list_drug.Add(drug);
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


        /// <summary>
        /// 获取药品RemainDay
        /// </summary>
        /// <returns></returns>
        public static void upDateRemainDay()
        {
            using (DataContext dc = new DataContext(Dap.common.conn))
            {

                var _drugs = from x in dc.GetTable<Drugs>()
                            select x;
                List<Drugs> list_drug = _drugs.ToList();
                DateTime t = DateTime.Now;
                
                foreach (Drugs drug in list_drug)
                {
                    DateTime t1 = Convert.ToDateTime(drug.ExpirationDate);
                    TimeSpan ts = t1 - t;
                    int d = ts.Days;
                    drug.RemainDay = d;
                    
                }
                dc.SubmitChanges();

            }
            
        }
    }

}
