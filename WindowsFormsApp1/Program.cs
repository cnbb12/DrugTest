using cn.jpush.api;
using cn.jpush.api.push;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            JPushClient client = new JPushClient("6d39f5bdddfe1143a3d55895", "74758e5fd26d7bb2faa74488");
            MessageResult result = client.sendNotificationAll("7777777aaaaaa");//现在开始测试极光推送，检测消息是否到达
        }
    }
}
