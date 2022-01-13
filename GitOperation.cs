using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibGit2Sharp;
using Ranorex;

namespace EpointAutomationHelper
{
	/// <summary>
	/// Git操作
	/// </summary>
	public class GitOperation
	{
		string dirPath;
		/// <summary>
		/// 仓库路径
		/// </summary>
		public string DirPath
		{
			get { return dirPath; }
			set { dirPath = value; }
		}
		
		/// <summary>
		/// Git操作
		/// </summary>
		/// <param name="dirPath">仓库路径</param>
		public GitOperation(string dirPath)
		{
			this.dirPath = dirPath;
			
			//验证二进制文件是否复制到制定位置
		}

		/// <summary>
		/// 初始化仓库
		/// </summary>
		public void InitRepo()
		{
			DirHelper.CreateFolder(dirPath);

			Repository.Init(dirPath);
		}

		/// <summary>
		/// 推送本地仓库到远程仓库
		/// </summary>
		public void PushRepo()
		{
			using (var repo = new Repository(dirPath))
			{
				repo.Stage(dirPath + @"\README.md");
				repo.Stage(dirPath + @"\.github\workflows\.action.yml");
				var remote =  repo.Network.Remotes.Add("original", "https://github.com/EpointAutomation/Email2Ranorex.git");
				repo.Commit("Epoint Automation Auto Commit at" + System.DateTime.Now.ToString());
				var options = new PushOptions
				{
					CredentialsProvider = (_url, _user, _cred) =>
						new UsernamePasswordCredentials { Username = "EpointAutomation", Password = "At@epoint123456" }
				};
				string pushRefSpec = string.Format("+{0}:{0}", repo.Head.CanonicalName);
				repo.Network.Push(remote, pushRefSpec, options);
			}
		}
	}
}
