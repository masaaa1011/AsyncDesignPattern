using AsyncDesignPattern.Repository.Repository;
using AsyncDesignPattern.TaskFamily.Controller;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ここからIPアドレスやポートの設定
            // 着信データ用のデータバッファー。
            byte[] bytes = new byte[1024];
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            //ここまでIPアドレスやポートの設定

            //ソケットの作成
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //通信の受け入れ準備
            listener.Bind(localEndPoint);
            listener.Listen(10);

            //通信の確率
            Socket handler = listener.Accept();


            // 任意の処理
            //データの受取をReceiveで行う。
            int bytesRec = handler.Receive(bytes);
            string data1 = Encoding.UTF8.GetString(bytes, 0, bytesRec);
            Console.WriteLine(data1);

            //大文字に変更
            data1 = data1.ToUpper();

            //クライアントにSendで返す。
            byte[] msg = Encoding.UTF8.GetBytes(data1);
            handler.Send(msg);

            //ソケットの終了
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

            //CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>()
                            .AddTransient<ITaskHandler, TaskHandler>()
                            .AddTransient<IRepository, MockRecordRepository>();
                });
    }
}
