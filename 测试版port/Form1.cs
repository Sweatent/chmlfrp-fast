using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata;
using Microsoft.VisualBasic.ApplicationServices;

namespace 测试版port
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // 获取当前进程的名称
            string processName = Process.GetCurrentProcess().ProcessName;
            // 获取同名进程的数量
            int count = Process.GetProcessesByName(processName).Length;
            // 如果数量大于1，关闭程序
            if (count > 1)
            {
                Process.GetCurrentProcess().Kill();
            }
            MessageBox.Show("警告！本程序具有超不稳定性，请保证以下情况运行程序：1.打开了mc的java版并且打开了局域网2.联网成功" +
                "不然将报未知错误并引发无法预测的事情，如隧道被多次创建等");
            MessageBox.Show("本程序可以一键启动映射，不需要你建隧道和运行隧道，本程序点击按钮后自动运行");
        }
        public static string GenerateRandomName()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new StringBuilder(8);
            for (int i = 0; i < 8; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return result.ToString();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            // 判断输入框是否为空
            if (string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                // 弹出消息框提示用户输入
                MessageBox.Show("token不能为空!", "系统提示");
                button1.Enabled = true;
            }
            else
            {
                richTextBox1.Clear();
                // 发送api get请求
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // 在url字符串中加上输入框的值
                string url = "https://panel.chmlfrp.cn/api/userinfo.php?usertoken=" + this.textBox1.Text;
                HttpResponseMessage response = client.GetAsync(url).Result;
                string content = response.Content.ReadAsStringAsync().Result;
                JsonElement data = JsonSerializer.Deserialize<JsonElement>(content);
                var jsonObject = JObject.Parse(content);
                if (response.IsSuccessStatusCode)
                {
                    var userId = jsonObject["userid"];
                    if (data.TryGetProperty("error", out JsonElement error))
                    {
                        // token不对
                        MessageBox.Show("Token错误！", "系统提示");
                        richTextBox1.AppendText("[info]token错误" + Environment.NewLine);
                        button1.Enabled = true;
                    }
                    else
                    {
                        richTextBox1.AppendText("[info]登录成功" + Environment.NewLine);
                        // 创建一个Process对象，用于执行cmd命令
                        Process pro = new Process();
                        pro.StartInfo.FileName = "cmd.exe";
                        pro.StartInfo.UseShellExecute = false;
                        pro.StartInfo.RedirectStandardInput = true;
                        pro.StartInfo.RedirectStandardOutput = true;
                        pro.StartInfo.RedirectStandardError = true;
                        pro.StartInfo.CreateNoWindow = true;
                        pro.Start();

                        // 向cmd输入netstat -ano命令，获取所有连接和端口信息
                        pro.StandardInput.WriteLine("netstat -ano");

                        // 退出cmd
                        pro.StandardInput.WriteLine("exit");

                        // 用正则表达式匹配空白字符
                        Regex reg = new Regex("\\s+", RegexOptions.Compiled);

                        // 定义一个字符串变量，用于存储端口号
                        string port = null;

                        // 逐行读取cmd的输出结果
                        string line = null;
                        bool isport = false;
                        while ((line = pro.StandardOutput.ReadLine()) != null)
                        {
                            // 去除两端的空白字符
                            line = line.Trim();

                            // 如果不是以TCP开头的行，跳过
                            if (!line.StartsWith("TCP"))
                            {
                                continue;
                            }

                            // 用逗号替换空白字符，分割字符串
                            line = reg.Replace(line, ",");
                            string[] arr = line.Split(',');

                            // 获取本地地址和端口号
                            string local = arr[1];
                            int pos = local.LastIndexOf(':');
                            port = local.Substring(pos + 1);

                            // 获取进程ID
                            string pid = arr[4];

                            // 根据进程ID获取进程名称
                            string name = Process.GetProcessById(Convert.ToInt32(pid)).ProcessName;

                            // 如果进程名称是java，输出端口号并跳出循环
                            if (name == "java")
                            {
                                isport = true;
                                richTextBox1.AppendText("[info]获取端口成功" + Environment.NewLine);
                                break;
                            }
                        }
                        if (isport == true)
                        {
                            MessageBox.Show(port);
                            System.Random aa = new Random();
                            int j = aa.Next(25565, 65535);

                            string url1 = "https://panel.chmlfrp.cn/api/tunnel.php"; // 替换为你的目标网页地址
                            string name123 = GenerateRandomName();
                            // 构建要发送的数据
                            string postData = @"{
    ""token"": """ + this.textBox1.Text + @""",
    ""userid"":" + userId + @",
    ""localip"": ""127.0.0.1"",
    ""name"":" + name123 + @",
    ""node"": ""广州移动-2"",
    ""type"": ""tcp"",
    ""nport"": " + port + @",
    ""dorp"": " + j + @",
    ""ap"": """",
    ""encryption"": ""false"",
    ""compression"": ""false""
}";

                            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                            // 创建一个WebRequest对象
                            WebRequest request = WebRequest.Create(url1);

                            // 设置请求的方法为POST
                            request.Method = "POST";

                            // 设置请求的内容类型为application/json
                            request.ContentType = "application/json";

                            // 设置请求的内容长度
                            request.ContentLength = byteArray.Length;

                            // 获取请求的输入流
                            using (var dataStream = request.GetRequestStream())
                            {
                                // 将数据写入请求的输入流
                                dataStream.Write(byteArray, 0, byteArray.Length);
                            }

                            // 获取请求的响应
                            using (var response1 = request.GetResponse())
                            {
                                // 获取响应流
                                using (var responseStream = response1.GetResponseStream())
                                {
                                    // 读取响应流
                                    using (var reader = new System.IO.StreamReader(responseStream))
                                    {
                                        string responseText = reader.ReadToEnd();
                                        // 处理响应结果
                                        MessageBox.Show(responseText);
                                        richTextBox1.Text = responseText;
                                        button1.Text = "停止";
                                        button1.Enabled = true;
                                        // 或者根据需要进行其他操作
                                    }

                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("未开启mc局域网，请检查再试", "系统提示");

                            button1.Enabled = true;
                        }
                    }
                }
                else
                {
                    // 处理错误情况
                    MessageBox.Show("请求失败!请检查是否联网", "系统提示");
                    button1.Enabled = true;
                }
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

}


