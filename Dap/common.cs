using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dap
{
    class common
    {
        public static String conn
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            }
        }
        /// <summary>
        /// Http方式下载文件
        /// </summary>
        /// <param name="url">http地址</param>
        /// <param name="savePath">本地存放地址</param>
        /// <returns></returns>
        public static void Download(string url, string savePath)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();

            //创建本地文件写入流

            if (!System.IO.File.Exists(savePath))
            {

                //文件不存在
                System.IO.File.Create(savePath);//创建该文件

            }
            Stream stream = new FileStream(savePath, FileMode.Create);
            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            stream.Close();
            responseStream.Close();

        }
    }

    public class RESULT
    {
        public bool state { get; set; }
        public List<object> result { get; set; }
        public string msg { get; set; }
        public RESULT(string token = "")
        {
            this.state = true;
            this.msg = "succeed";
            //在这里可以添加有关token等验证操作
        }
    }
}
