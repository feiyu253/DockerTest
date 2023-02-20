// See https://aka.ms/new-console-template for more information
using DockerConsole;
using DockerDataModel;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text.Json;
using System.Threading.Channels;
using System.Xml.Linq;

CommonHelper helper = new CommonHelper();
Console.WriteLine(helper.TestMethod());
Console.WriteLine("Hello, World!");

string myTest = JsonConvert.SerializeObject(new Test { UserID = "michael", UserName = "李玉强" });
Console.WriteLine(myTest);
var objTest = JsonConvert.DeserializeObject<Test>(myTest);
Console.WriteLine(objTest.UserID);
//RabbitMQ publish
string mqQueueName = "RabbitMQ'Test";
Console.WriteLine("开始发送信息.....");
Console.WriteLine();
Task.Factory.StartNew(async () => {
    var publishCnn = helper.GetMQConnection("localhost", 5672, "/", "michael", "137690");
    var channel = publishCnn.CreateModel();
    channel.QueueDeclare(mqQueueName,false,false,false,null);
    for(int i = 1;i < 12; i++)
    {
        string publishMS = $"message {i}";
        Console.WriteLine($"开始发送消息：{publishMS}");
        channel.BasicPublish("",mqQueueName,false,null, System.Text.Encoding.UTF8.GetBytes(publishMS));
        Console.WriteLine($"成功发送消息：{publishMS}");
        Console.WriteLine();
        await Task.Delay(20);
    }
    channel.Dispose();
    publishCnn.Close();
    Console.WriteLine();
    Console.WriteLine($"全部发送成功。");
    //Console.WriteLine($"please press enter key to recerive messages");
});


//Console.ReadLine();
//Console.WriteLine("开始接收信息.....");
//Console.WriteLine();
//RabbitMQ consume
Task.Factory.StartNew(async () => { 
    var consumeCnn = helper.GetMQConnection("localhost", 5672, "/", "michael", "137690");
    var channel = consumeCnn.CreateModel();
    channel.QueueDeclare(mqQueueName,false,false,false,null);
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model,e)=> {
        
        var ms = System.Text.Encoding.UTF8.GetString(e.Body.ToArray());
        Console.WriteLine($"消费者1 收到消息：{ms}");
        Console.WriteLine();
    };
    channel.BasicConsume(mqQueueName, true, consumer.ToString(), false, false, null, consumer);
    Console.ReadLine();
    channel.Dispose();
    consumeCnn.Close();
});

Task.Factory.StartNew(async () => {
    var consumeCnn = helper.GetMQConnection("localhost", 5672, "/", "michael", "137690");
    var channel = consumeCnn.CreateModel();
    channel.QueueDeclare(mqQueueName, false, false, false, null);
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, e) => {

        var ms = System.Text.Encoding.UTF8.GetString(e.Body.ToArray());
        Console.WriteLine($"消费者2 收到消息：{ms}");
        Console.WriteLine();
    };
    channel.BasicConsume(mqQueueName, true, consumer.ToString(), false, false, null, consumer);
    Console.ReadLine();
    channel.Dispose();
    consumeCnn.Close();
});

Task.Factory.StartNew(async () => {
    var consumeCnn = helper.GetMQConnection("localhost", 5672, "/", "michael", "137690");
    var channel = consumeCnn.CreateModel();
    channel.QueueDeclare(mqQueueName, false, false, false, null);
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, e) => {

        var ms = System.Text.Encoding.UTF8.GetString(e.Body.ToArray());
        Console.WriteLine($"消费者3 收到消息：{ms}");
        Console.WriteLine();
    };
    channel.BasicConsume(mqQueueName, true, consumer.ToString(), false, false, null, consumer);
    Console.ReadLine();
    channel.Dispose();
    consumeCnn.Close();
});

Task.Factory.StartNew(async () => {
    var consumeCnn = helper.GetMQConnection("localhost", 5672, "/", "michael", "137690");
    var channel = consumeCnn.CreateModel();
    channel.QueueDeclare(mqQueueName, false, false, false, null);
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, e) => {

        var ms = System.Text.Encoding.UTF8.GetString(e.Body.ToArray());
        Console.WriteLine($"消费者4 收到消息：{ms}");
        Console.WriteLine();
    };
    channel.BasicConsume(mqQueueName, true, consumer.ToString(), false, false, null, consumer);
    Console.ReadLine();
    channel.Dispose();
    consumeCnn.Close();
});

Console.ReadLine();


public class Test
{
    public string UserID { get; set; }

    public string UserName { get; set; }
}


