using Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dap
{
    public class user
    {
        /// <summary>
        /// 检测登录
        /// </summary>
        /// <param name="phoneNumber">用户名就是手机号</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<Models.User> checkLogin(string phoneNumber, string key)
        {
            using (DataContext dc = new DataContext(common.conn))
            {
                if (phoneNumber.Equals("") || key == "")
                {
                    throw (new Exception("用户名或密码不能为空"));
                }
                else
                {
                    var user = from x in dc.GetTable<User>()
                               where x.PhoneNumber == phoneNumber && x.Password == key
                               select x;
                    if (user.Count() >= 1)
                    {
                        return user.ToList();
                        
                    }
                    else throw (new Exception("用户名或密码错误"));
                }
            }
        }

        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<Models.User> registerUser(string phoneNumber, string key)
        {
            using (DataContext dc = new DataContext(common.conn))
            {
                int phoneNumbers = (from x in dc.GetTable<User>()
                                   where x.PhoneNumber == phoneNumber
                                   select x).Count();
                if(phoneNumbers != 0)
                {
                    throw new Exception("该手机号已被注册");
                }
                else
                {
                    int count = (from x in dc.GetTable<User>()
                                       select x).Count();

                    
                    User user = new User();
                    user.PhoneNumber = phoneNumber;
                    user.Password = key;
                    user.ID = Guid.NewGuid();
                    user.xUtils_id = count + 1;

                    var tb = dc.GetTable<User>();
                    tb.InsertOnSubmit(user);
                    dc.SubmitChanges();//后台自动生成用户ID
                    List<Models.User> list_user= new List<User>();
                    list_user.Add(user);
                    return list_user;
                }
                    
               
                
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="key"></param>
        /// <param name="newKey"></param>
        /// <returns></returns>
        public static List<Models.User> changePsd(string id, string key, string newKey)
        {
            if (null == id || null == key || null == newKey)
            {
                throw (new Exception("请将信息填写完整"));
            }
            else
            {
                using (DataContext dc = new DataContext(Dap.common.conn))
                {
                    var _user = (from x in dc.GetTable<User>()
                                 where x.ID.ToString() == id && x.Password.ToString() == key
                                 select x).First();
                    Models.User user = _user;

                    user.Password = newKey;
                   
                    dc.SubmitChanges();//后台自动生成用户ID
                    List<Models.User> list_user = new List<User>();
                    list_user.Add(user);
                    return list_user;
                }
            }

        }
    }
}
