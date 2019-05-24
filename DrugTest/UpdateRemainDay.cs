using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace DrugTest
{
    public class UpdateRemainDay
    {
        public static void startTimer()
        {
            Dap.drugs.upDateRemainDay();
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
            Dap.drugs.upDateRemainDay();
        }
    }
}