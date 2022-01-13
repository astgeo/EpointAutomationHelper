using Ranorex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace EpointAutomationHelper
{
	/// <summary>
	/// FTP文件操作类，支持增删查
	/// </summary>
	public class FtpHelper
	{
		private string _Path;
		private string _IP;
		private string _UserName;
		private string _Pwd;
		
        /// <summary>
        /// 初始化FTP操作
        /// </summary>
        /// <param name="FtpServerIP">FTP IP地址</param>
        /// <param name="FtpUserName">用户名</param>
        /// <param name="FtpPassWord">密码</param>
		public FtpHelper(string FtpServerIP,string FtpUserName,string FtpPassWord)
		{
			this._Path = @"ftp://" +FtpServerIP + "/";//目标路径
			this._IP = FtpServerIP;//ftp IP地址
			this._UserName = FtpUserName;//ftp用户名
			this._Pwd = FtpPassWord; //ftp密码
		}
		
		/// <summary>
		/// 获取ftp上面的文件和文件夹
		/// </summary>
		/// <param name="dir">FTP上的路径</param>
		/// <returns>文件List</returns>
		public string[] GetFileList(string dir)
		{
			string[] downloadFiles;
			StringBuilder result = new StringBuilder();
			FtpWebRequest request;
			try
			{
				request = (FtpWebRequest)FtpWebRequest.Create(new Uri(_Path + dir));
				request.UseBinary = true;
				request.Credentials = new NetworkCredential(_UserName, _Pwd);//设置用户名和密码
				request.Method = WebRequestMethods.Ftp.ListDirectory;
				request.UseBinary = true;
				request.UsePassive = false; //选择主动还是被动模式 , 这句要加上的。
				request.KeepAlive = false;//一定要设置此属性，否则一次性下载多个文件的时候，会出现异常。
				WebResponse response = request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				
				string line = reader.ReadLine();
				while (line != null)
				{
					result.Append(line);
					result.Append("\n");
					line = reader.ReadLine();
				}
				
				result.Remove(result.ToString().LastIndexOf('\n'), 1);
				reader.Close();
				response.Close();
				return result.ToString().Split('\n');
			}
			catch (Exception ex)
			{
				Report.Error("获取ftp上面的文件和文件夹：" + ex.Message);
				downloadFiles = null;
				return downloadFiles;
			}
		}
		
		/// <summary>
		/// 从ftp服务器上获取文件并将内容全部转换成string返回
		/// </summary>
		/// <param name="fileName">文件名</param>
		/// <param name="dir">文件路径</param>
		/// <returns>文件</returns>
		public string GetFileStr(string fileName, string dir)
		{
			FtpWebRequest reqFTP;
			try
			{
				reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_Path + dir + "/" + fileName));
				reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
				reqFTP.UseBinary = true;
				reqFTP.Credentials = new NetworkCredential(_UserName, _Pwd);
				reqFTP.UsePassive = false; //选择主动还是被动模式 , 这句要加上的。
				reqFTP.KeepAlive = false;//一定要设置此属性，否则一次性下载多个文件的时候，会出现异常。
				FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
				Stream ftpStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(ftpStream);
				string fileStr = reader.ReadToEnd();
				
				reader.Close();
				ftpStream.Close();
				response.Close();
				return fileStr;
			}
			catch (Exception ex)
			{
				Report.Error("获取ftp文件并读取内容失败：" + ex.Message);
				return null;
			}
		}
		
		/// <summary>
		/// 获取文件大小
		/// </summary>
		/// <param name="file">ip服务器下的相对路径</param>
		/// <returns>文件大小</returns>
		public int GetFileSize(string file)
		{
			StringBuilder result = new StringBuilder();
			FtpWebRequest request;
			try
			{
				request = (FtpWebRequest)FtpWebRequest.Create(new Uri(_Path + file));
				request.UseBinary = true;
				request.Credentials = new NetworkCredential(_UserName, _Pwd);//设置用户名和密码
				request.Method = WebRequestMethods.Ftp.GetFileSize;
				
				int dataLength = (int)request.GetResponse().ContentLength;
				
				return dataLength;
			}
			catch (Exception ex)
			{
				Console.WriteLine("获取文件大小出错：" + ex.Message);
				return -1;
			}
		}
		
		/// <summary>
		/// 文件上传
		/// </summary>
		/// <param name="file_Path">原路径（绝对路径）包括文件名</param>
		/// <param name="obj_Path">目标文件夹：服务器下的相对路径 不填为根目录</param>
		public void FileUpLoad(string file_Path,string obj_Path="")
		{
			try
			{
				string url = _Path;
				if(obj_Path!="")
					url += obj_Path + "/";
				try
				{
					
					FtpWebRequest reqFTP = null;
					//待上传的文件 （全路径）
					try
					{
						FileInfo fileInfo = new FileInfo(file_Path);
						using (FileStream fs = fileInfo.OpenRead())
						{
							long length = fs.Length;
							reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url + fileInfo.Name));
							
							//设置连接到FTP的帐号密码
							reqFTP.Credentials = new NetworkCredential(_UserName, _Pwd);
							//设置请求完成后是否保持连接
							reqFTP.KeepAlive = false;
							//指定执行命令
							reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
							//指定数据传输类型
							reqFTP.UseBinary = true;
							
							using (Stream stream = reqFTP.GetRequestStream())
							{
								//设置缓冲大小
								int BufferLength = 5120;
								byte[] b = new byte[BufferLength];
								int i;
								while ((i = fs.Read(b, 0, BufferLength)) > 0)
								{
									stream.Write(b, 0, i);
								}
								Console.WriteLine("上传文件成功");
							}
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine("上传文件失败错误为" + ex.Message);
					}
					finally
					{
						
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("上传文件失败错误为" + ex.Message);
				}
				finally
				{
					
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("上传文件失败错误为" + ex.Message);
			}
		}
		
		/// <summary>
		/// 删除文件
		/// </summary>
		/// <param name="fileName">服务器下的相对路径 包括文件名</param>
		public void DeleteFileName(string fileName)
		{
			try
			{
				FileInfo fileInf = new FileInfo(_IP +""+ fileName);
				string uri = _Path + fileName;
				FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
				// 指定数据传输类型
				reqFTP.UseBinary = true;
				// ftp用户名和密码
				reqFTP.Credentials = new NetworkCredential(_UserName, _Pwd);
				// 默认为true，连接不会被关闭
				// 在一个命令之后被执行
				reqFTP.KeepAlive = false;
				// 指定执行什么命令
				reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
				FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
				response.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine("删除文件出错：" + ex.Message);
			}
		}
		
		/// <summary>
		/// 新建目录 上一级必须先存在
		/// </summary>
		/// <param name="dirName">服务器下的相对路径</param>
		public void MakeDir(string dirName)
		{
			try
			{
				string uri = _Path + dirName;
				FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
				// 指定数据传输类型
				reqFTP.UseBinary = true;
				// ftp用户名和密码
				reqFTP.Credentials = new NetworkCredential(_UserName, _Pwd);
				reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
				FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
				response.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine("创建目录出错：" + ex.Message);
			}
		}
		
		/// <summary>
		/// 删除目录 上一级必须先存在
		/// </summary>
		/// <param name="dirName">服务器下的相对路径</param>
		public void DelDir(string dirName)
		{
			try
			{
				string uri = _Path + dirName;
				FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
				// ftp用户名和密码
				reqFTP.Credentials = new NetworkCredential(_UserName, _Pwd);
				reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
				FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
				response.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine("删除目录出错：" + ex.Message);
			}
		}
		

		/// <summary>
		/// 从ftp服务器上获得文件夹列表
		/// </summary>
		/// <param name="Requedst_Path">服务器下的相对路径</param>
		/// <returns></returns>
		public List<string> GetDirctory(string Requedst_Path)
		{
			List<string> strs = new List<string>();
			try
			{
				string uri = _Path + Requedst_Path;  //目标路径 _Path为服务器地址
				FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
				// ftp用户名和密码
				reqFTP.Credentials = new NetworkCredential(_UserName, _Pwd);
				reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
				WebResponse response = reqFTP.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());//中文文件名
				
				string line = reader.ReadLine();
				while (line != null)
				{
					if (line.Contains("<DIR>"))
					{
						string msg = line.Substring(line.LastIndexOf("<DIR>")+5).Trim();
						strs.Add(msg);
					}
					line = reader.ReadLine();
				}
				reader.Close();
				response.Close();
				return strs;
			}
			catch (Exception ex)
			{
				Console.WriteLine("获取目录出错：" + ex.Message);
			}
			return strs;
		}

		/// <summary>
		/// 从ftp服务器上获得文件列表
		/// </summary>
		/// <param name="Requedst_Path">服务器下的相对路径</param>
		/// <returns></returns>
		public List<string> GetFile(string Requedst_Path)
		{
			List<string> strs = new List<string>();
			try
			{
				string uri = _Path + Requedst_Path;  //目标路径 _Path为服务器地址
				FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
				// ftp用户名和密码
				reqFTP.Credentials = new NetworkCredential(_UserName, _Pwd);
				reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
				WebResponse response = reqFTP.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());//中文文件名
				
				string line = reader.ReadLine();
				while (line != null)
				{
					if (!line.Contains("<DIR>"))
					{
						string msg = line.Substring(39).Trim();
						strs.Add(msg);
					}
					line = reader.ReadLine();
				}
				reader.Close();
				response.Close();
				return strs;
			}
			catch (Exception ex)
			{
				Console.WriteLine("获取文件出错：" + ex.Message);
			}
			return strs;
		}
	}
}
