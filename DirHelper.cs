using Ranorex;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EpointAutomationHelper
{
	/// <summary>
	/// 目录工具类
	/// </summary>
	public class DirHelper
	{
		/// <summary>
		/// 当前程序运行目录,最后没有“\”
		/// </summary>
		public static string BaseDirectory
		{
			get
			{
				return System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
			}
		}

		/// <summary>
		/// 创建文件夹
		/// </summary>
		/// <param name="path">文件夹绝对路径</param>
		/// <param name="isPrintLog">是否打印日志</param>
		public static void CreateFolder(string path, bool isPrintLog = false)
		{
			if (!Directory.Exists(path))
			{
				if (isPrintLog)
					Report.Info("创建的文件夹路径=" + path);
				Directory.CreateDirectory(path);
			}
			else
			{
				if (isPrintLog)
					Report.Info(string.Format("文件夹={0}已存在，不再创建", path));
			}
		}

		/// <summary>
		/// 复制文件（包括文件夹下文件和子文件夹及其文件）到指定目录
		/// </summary>
		/// <param name="srcFolderPath">源文件夹</param>
		/// <param name="targetFolderPath">目标文件夹</param>
		public static void CopyDirectory(string srcFolderPath, string targetFolderPath)
		{
			var info = new DirectoryInfo(srcFolderPath);
			CreateFolder(targetFolderPath);

			foreach (var item in info.GetFileSystemInfos())
			{
				var targetName = Path.Combine(targetFolderPath, item.Name);

				if (item is FileInfo)
				{
					File.Copy(item.FullName, targetName, true);
				}
				else
				{
					Directory.CreateDirectory(targetName);
					CopyDirectory(item.FullName, targetName);
				}
			}
		}
		
		/// <summary>
		/// 删除文件夹目录及文件
		/// </summary>
		/// <param name="dirPath">文件夹路径</param>
		public static void DeleteFolder(string dirPath)
		{
			if (Directory.Exists(dirPath))
			{
				string[] files = Directory.GetFiles(dirPath);
				string[] dirs = Directory.GetDirectories(dirPath);

				foreach (string file in files)
				{
					File.SetAttributes(file, FileAttributes.Normal);
					File.Delete(file);
				}

				foreach (string dir in dirs)
				{
					DeleteFolder(dir);
				}

				Directory.Delete(dirPath, false);
			}
		}
		
		/// <summary>
		///杀资源管理器进程，解除文件被占用导致权限无法访问的情况
		/// </summary>
		/// <remarks>资源管理器会自己重启</remarks>
		public static void KillExplorer()
		{
			if (Process.GetProcessesByName("explorer").Length > 0)
			{
				Delay.Milliseconds(500);
				Process[] killpro = Process.GetProcessesByName("explorer");
				foreach (var item in killpro)
				{
					item.Kill();
				}
			}
		}
		
		/// <summary>
		/// 通过程序名称去注册表寻找程序安装路径
		/// </summary>
		/// <param name="softName">软件名称</param>
		/// <param name="path">安装路径</param>
		/// <returns>如果找到指定软件返回true，否则false</returns>
		public static bool TryGetSoftwarePath(Softwares softName, out string path)
		{
			return TryGetSoftwarePath(softName.ToString(), out path);
		}

		/// <summary>
		/// 通过程序名称去注册表寻找程序安装路径
		/// </summary>
		/// <param name="softName">软件名称</param>
		/// <param name="path">安装路径</param>
		/// <returns>如果找到指定软件返回true，否则false</returns>
		public static bool TryGetSoftwarePath(string softName, out string path)
		{
			////代码网上复制
			string strPathResult = string.Empty;
			string strKeyName = "";     //"(Default)" key, which contains the intalled path
			object objResult = null;

			Microsoft.Win32.RegistryValueKind regValueKind;
			Microsoft.Win32.RegistryKey regKey = null;
			Microsoft.Win32.RegistryKey regSubKey = null;

			try
			{
				//Read the key
				regKey = Microsoft.Win32.Registry.LocalMachine;
				regSubKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\" + softName.ToString() + ".exe", false);

				//Read the path
				objResult = regSubKey.GetValue(strKeyName);
				regValueKind = regSubKey.GetValueKind(strKeyName);

				//Set the path
				if (regValueKind == Microsoft.Win32.RegistryValueKind.String)
				{
					strPathResult = objResult.ToString();
				}
			}
			catch (System.Security.SecurityException ex)
			{
				throw new System.Security.SecurityException("You have no right to read the registry!", ex);
			}
			catch (Exception ex)
			{
				throw new Exception("Reading registry error!", ex);
			}
			finally
			{

				if (regKey != null)
				{
					regKey.Close();
					regKey = null;
				}

				if (regSubKey != null)
				{
					regSubKey.Close();
					regSubKey = null;
				}
			}

			if (strPathResult != string.Empty)
			{
				//Found
				path = strPathResult;
				return true;
			}
			else
			{
				//Not found
				path = null;
				return false;
			}
		}

	}

	/// <summary>
	/// 软件名称
	/// </summary>
	public enum Softwares
	{
		//代码网上复制
		//The names are the same with the registry names.
		//You can add any software exists in the "regedit" path:
		//HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths

		/// <summary>
		/// Office Excel
		/// </summary>
		EXCEL,
		/// <summary>
		/// Office Word
		/// </summary>
		WINWORD,
		/// <summary>
		/// Office Access
		/// </summary>
		MSACCESS,
		/// <summary>
		/// Office PowerPoint
		/// </summary>
		POWERPNT,
		/// <summary>
		/// Office Outlook
		/// </summary>
		OUTLOOK,
		/// <summary>
		/// Office InfoPath
		/// </summary>
		INFOPATH,
		/// <summary>
		/// Office Publisher
		/// </summary>
		MSPUB,
		/// <summary>
		/// Office Visio
		/// </summary>
		VISIO,
		/// <summary>
		/// IE
		/// </summary>
		IEXPLORE,
		/// <summary>
		/// Apple ITunes
		/// </summary>
		ITUNES
	}

}