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
            //��������IP�A�h���X��|�[�g�̐ݒ�
            // ���M�f�[�^�p�̃f�[�^�o�b�t�@�[�B
            byte[] bytes = new byte[1024];
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            //�����܂�IP�A�h���X��|�[�g�̐ݒ�

            //�\�P�b�g�̍쐬
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //�ʐM�̎󂯓��ꏀ��
            listener.Bind(localEndPoint);
            listener.Listen(10);

            //�ʐM�̊m��
            Socket handler = listener.Accept();


            // �C�ӂ̏���
            //�f�[�^�̎���Receive�ōs���B
            int bytesRec = handler.Receive(bytes);
            string data1 = Encoding.UTF8.GetString(bytes, 0, bytesRec);
            Console.WriteLine(data1);

            //�啶���ɕύX
            data1 = data1.ToUpper();

            //�N���C�A���g��Send�ŕԂ��B
            byte[] msg = Encoding.UTF8.GetBytes(data1);
            handler.Send(msg);

            //�\�P�b�g�̏I��
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
