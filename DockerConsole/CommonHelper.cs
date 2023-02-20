using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerConsole
{
    public class CommonHelper
    {
        public string TestMethod()
        {
            return "Michael";
        }

        public string GetUserName()
        {
            return "Michael";
        }

        public IConnection GetMQConnection(string hostIP, int hostPort, string virtualHost, string userID, string pwd)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = hostIP;
            factory.VirtualHost = virtualHost;
            factory.Port = hostPort;
            factory.UserName = userID;
            factory.Password = pwd;
            return factory.CreateConnection();
        }
    }

    
}
