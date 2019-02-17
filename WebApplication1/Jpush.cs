using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Timers;
using System.Web;
using cn.jpush.api;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;
using Dap;
using Models;

namespace WebApplication1
{
    public class Jpush
    {
        public static void Main()
        {
       
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 10; //执行间隔时间,单位为毫秒; 这里实际间隔为10分钟  
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimedEvent);
            
        }

        private static void TimedEvent(object source, ElapsedEventArgs e)
        {
            sendPush();
       
        }
        public static void sendPush()
        {
            string AppKey = ConfigurationManager.AppSettings["AppKey"];
            string MasterSecret = ConfigurationManager.AppSettings["MasterSecret"];
            JPushClient client = new JPushClient(AppKey, MasterSecret);//根据app信息生成推送client
            List<PushPayload> pushPayLoads = JPushObjecWithExtrasAndMessage();//设置推送的具体参数
            foreach(PushPayload pushPayLoad in pushPayLoads) {
                var reslut = client.SendPush(pushPayLoad);//推送
                var apiReslut = client.getReceivedApi(reslut.msg_id.ToString());//接收推送结果
            }
               
         
       

        }




        public static List<PushPayload> JPushObjecWithExtrasAndMessage()
        {
            List<string> alias = new List<string>();
            List<string> names = new List<string>();
            List<PushPayload> pushes = new List<PushPayload>();

            //添加别名实现个推
            List<Models.Drugs> list =  Dap.drugs.getDrugsList("1");//1:RemainDay==0
           
            foreach(Models.Drugs drug in list)
            {
                PushPayload pushPayload = new PushPayload();
                pushPayload.platform = Platform.all();
                string s = drug.OwnerId.ToString().Replace("-", "");//将userId去掉间隔符作为别名
                pushPayload.audience = Audience.s_alias(s);
                //通知
                string alert = "你的药品" + drug.Name.ToString()+"已经过期";
                pushPayload.notification = new Notification().setAndroid(new AndroidNotification().setAlert(alert));
                pushes.Add(pushPayload);
                
            }
          
            return pushes;


        }
    }



}