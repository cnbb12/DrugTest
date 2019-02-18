using cn.jpush.api;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace DrugTest
{
    public class Jpush
    {
        /// <summary>
        /// 固定时间间隔进行推送
        /// </summary>
        public static void startTimer()
        {
            sendPush();
            //设置定时间隔(毫秒为单位)
            int interval = 24 * 60 * 60 * 1000;
            Timer timer = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            timer.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp);

        }

        private static void TimerUp(object sender, System.Timers.ElapsedEventArgs e)
        {
            sendPush();
        }
        public static void sendPush()
        {

            string AppKey = "6d39f5bdddfe1143a3d55895";
            string MasterSecret = "74758e5fd26d7bb2faa74488";
            JPushClient client = new JPushClient(AppKey, MasterSecret);//根据app信息生成推送client
            List<PushPayload> pushPayLoads = JPushObjecWithExtrasAndMessage();//设置推送的具体参数
            foreach (PushPayload pushPayLoad in pushPayLoads)
            {
                try
                {
                    var reslut = client.SendPush(pushPayLoad);//推送
                    var apiReslut = client.getReceivedApi(reslut.msg_id.ToString());//接收推送结果
                }catch(Exception e)
                {

                }
            }




        }




        public static List<PushPayload> JPushObjecWithExtrasAndMessage()
        {
            List<string> alias = new List<string>();
            List<string> names = new List<string>();
            List<PushPayload> pushes = new List<PushPayload>();

            //添加别名实现个推
            List<Models.Drugs> list = Dap.drugs.getDrugsList("1");//1:RemainDay==0

            foreach (Models.Drugs drug in list)
            {
                PushPayload pushPayload = new PushPayload();
                pushPayload.platform = Platform.all();
                string s = drug.OwnerId.ToString().Replace("-", "");//将userId去掉间隔符作为别名
                pushPayload.audience = Audience.s_alias(s);
                //通知
                string alert = "你的药品" + drug.Name.ToString() + "已经过期";
                pushPayload.notification = new Notification().setAndroid(new AndroidNotification().setAlert(alert));
                pushes.Add(pushPayload);

            }

            return pushes;


        }
    }
}