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
        public static Models.User checkLogin(string phoneNumber, string key)
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
                    if (user.Count() == 1)
                    {
                        return user.First();
                    }
                    else throw (new Exception("用户名或密码错误"));
                }
            }
        }
    }
}
