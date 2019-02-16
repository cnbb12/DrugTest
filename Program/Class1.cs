using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api;
using cn.jpush.api.common;
using cn.jpush.api.push;
using cn.jpush.api.report;
using cn.jpush.api.util;

namespace Program
{
    public class Class1
    {
        static void Main(string[] args)
        {
         
            JPushClient client = new JPushClient("6d39f5bdddfe1143a3d55895", "74758e5fd26d7bb2faa74488");
            MessageResult result = client.sendNotificationAll("7777777aaaaaa");//现在开始测试极光推送，检测消息是否到达
        
        }

    }
}
