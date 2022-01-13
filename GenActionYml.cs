using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using YamlDotNet.Serialization;
using System.Xml;
using System.Diagnostics;

namespace EpointAutomationHelper
{
	/// <summary>
	/// 生成action.yml工具类
	/// </summary>
	public class GenActionYml
	{
		string dirPath;
		/// <summary>
		/// 文件夹路径
		/// </summary>
		public string DirPath
		{
			get { return dirPath; }
			set { dirPath = value; }
		}
		
		string to;
		/// <summary>
		/// 收件人邮箱
		/// </summary>
		public string To
		{
			get { return to; }
			set { to = value; }
		}
		
		
		/// <summary>
		/// 生成action.yml工具类
		/// </summary>
		/// <param name="dirPath">文件夹路径</param>
		/// <param name="to">收件人邮箱，支持多邮箱，用英文逗号隔开</param>
		public GenActionYml(string dirPath, string to)
		{
			this.dirPath = dirPath;
			this.to = to;
		}
		

		/// <summary>
		/// 生成ActionYML
		/// </summary>
		public void GenYml()
		{
			//主题字段
			var strSubject = string.Format("{0} has completed {1} test at the time of {2}.",
			                               Environment.MachineName,
			                               Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.FileName),
			                               DateTime.Now.ToString());

			//新建文件夹
			var path = dirPath + @"\.github\workflows\";
			DirHelper.CreateFolder(path);

			//action结构
			var atcion = new ActionYML
			{
				name = "Send Email",
				on = new On
				{
					push = new Push
					{
						branches = "master"
					}
				},

				jobs = new Jobs
				{
					build = new Build
					{
						runs_on = "ubuntu-latest",
						steps = new FatherStep[]
						{
							new StepA
							{
								uses  = "actions/checkout@master",
							},

							new StepB
							{
								uses = "dawidd6/action-send-mail@v2",

								with = new With
								{
									server_address = "smtp.gmail.com",
									server_port = "465",
									username = "${{secrets.MAIL_USERNAME}}",
									password = "${{secrets.MAIL_PASSWORD}}",
									subject =  strSubject,
									body = @"file://README.md",
									to = this.to,
									from = "Epoint Automation Test Team"
								}
							}
						}
					}
				}


			};

			//写yml
			StringWriter strWriter = new StringWriter();
			var serializer = new SerializerBuilder().Build();
			serializer.Serialize(strWriter, atcion);
			var ymlPath = path + @".action.yml";
			using (TextWriter writer = File.CreateText(path + @".action.yml"))
			{
				writer.Write(strWriter.ToString());
			}

			//处理runs-on为runs_on
			File.WriteAllText(ymlPath, File.ReadAllText(ymlPath).Replace("runs_on", "runs-on"));
		}

		/// <summary>
		/// ActionYML结构类
		/// </summary>
		public class ActionYML
		{
			/// <summary>
			/// Action名称
			/// </summary>
			public string name { get; set; }

			/// <summary>
			/// On属性
			/// </summary>
			public On on { get; set; }

			/// <summary>
			/// 任务属性
			/// </summary>
			public Jobs jobs { get; set; }
		}

		/// <summary>
		/// On结构类
		/// </summary>
		public class On
		{
			/// <summary>
			/// 推送属性
			/// </summary>
			public Push push { get; set; }
		}

		/// <summary>
		/// 推送结构类
		/// </summary>
		public class Push
		{
			/// <summary>
			/// 分支名称
			/// </summary>
			public string branches { get; set; }
		}

		/// <summary>
		/// 任务结构类
		/// </summary>
		public class Jobs
		{
			/// <summary>
			/// 构建属性
			/// </summary>
			public Build build { get; set; }
		}

		/// <summary>
		/// Build结构类
		/// </summary>
		public class Build
		{
			/// <summary>
			/// 运行平台
			/// </summary>
			public string runs_on { get; set; }

			/// <summary>
			/// 步骤
			/// </summary>
			public FatherStep[] steps { get; set; }
		}
		
		/// <summary>
		/// 步骤基类
		/// </summary>
		public class FatherStep
		{
			/// <summary>
			/// 用法属性
			/// </summary>
			public string uses { get; set; }
		}
		
		/// <summary>
		/// 步骤A结构类
		/// </summary>
		public class StepA : FatherStep
		{
		}

		/// <summary>
		/// 步骤B结构类
		/// </summary>
		public class StepB : FatherStep
		{
			/// <summary>
			/// 用法属性
			/// </summary>
			new public string uses { get; set; }
			/// <summary>
			/// With属性
			/// </summary>
			public With with { get; set; }
		}

		/// <summary>
		/// With结构的类
		/// </summary>
		public class With
		{
			/// <summary>
			/// 服务器地址
			/// </summary>
			public string server_address { get; set; }
			
			/// <summary>
			/// 服务器端口
			/// </summary>
			public string server_port { get; set; }
			/// <summary>
			/// 用户名（发送方邮箱地址）
			/// </summary>
			public string username { get; set; }
			/// <summary>
			/// 密码（发送方授权码）
			/// </summary>
			public string password { get; set; }
			/// <summary>
			/// 邮件主题
			/// </summary>
			public string subject { get; set; }
			/// <summary>
			/// 邮件内容
			/// </summary>
			public string body { get; set; }
			/// <summary>
			/// 接收方邮件地址
			/// </summary>
			public string to { get; set; }
			/// <summary>
			/// 发送方信息
			/// </summary>
			public string from { get; set; }
		}
	}
}
