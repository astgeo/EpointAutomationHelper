using BumpKit;
using Ranorex;
using Ranorex.Core.Testing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Timers;

namespace EpointAutomationHelper
{
	/// <summary>
	/// 测试报告工具类
	/// </summary>
	internal class ReportHelper
	{
		internal ReportHelper()
		{
		}

		/// <summary>
		/// System.Threading.Timer回调函数
		/// </summary>
		/// <param name="state"></param>
		internal static void TakeScreenshot(object state)
		{
			Report.Info("Enter TakeScreenshot2");
			string screenshotPath = AppDomain.CurrentDomain.BaseDirectory + "\\scr";
			if (!Directory.Exists(screenshotPath))
			{
				Directory.CreateDirectory(screenshotPath);
			}
			System.DateTime dt = new System.DateTime();
			dt = System.DateTime.Now;
			//Report.Info(dt.ToLongTimeString());
			Bitmap img = null;
			img = Imaging.CaptureDesktopImage(null);
			img = (Bitmap)img.ScaleToFit(new Size(800, 600), false);
			img.Save(screenshotPath + "\\" + dt.ToString("yyyyMMddHHmmssfff") + ".png", ImageFormat.Png);
			img.Dispose();
		}

		/// <summary>
		/// 截取桌面图片，存放进当前运行目录的scr文件夹内
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		internal static void CaptureScreenshot(object sender, ElapsedEventArgs e)
		{
			//Report.Info("Enter TakeScreenshot");
			string screenshotPath = AppDomain.CurrentDomain.BaseDirectory + "\\scr";
			if (!Directory.Exists(screenshotPath))
			{
				Directory.CreateDirectory(screenshotPath);
			}
			System.DateTime dt = new System.DateTime();
			dt = System.DateTime.Now;
			//Report.Info(dt.ToLongTimeString());
			Bitmap img = null;
			img = Imaging.CaptureDesktopImage(null);
			img = (Bitmap)img.ScaleToFit(new Size(800, 600), false);
			//img.Save(screenshotPath + "\\" + dt.ToString("yyyyMMddHHmmssfff") + ".png", ImageFormat.Png);
			GetPicThumbnail(img, screenshotPath + "\\" + dt.ToString("yyyyMMddHHmmssfff") + ".png", 600, 800, 30);
			img.Dispose();
		}

		/// <summary>
		/// 生成测试出错前2分钟内的屏幕录像,保存当前运行目录的ReportGIF文件夹内
		/// </summary>
		internal static void GenerateScreencast()
		{
			if (TestSuite.CurrentTestContainer.Status == Ranorex.Core.Reporting.ActivityStatus.Failed)
			{
				Report.Info("TestCase Failed, Generating screencast of error...");
				string screenshotPath = AppDomain.CurrentDomain.BaseDirectory + "\\scr";
				if (!Directory.Exists(screenshotPath))
				{
					Report.Warn("Screenshot folder not found.");
					return;
				}
				System.DateTime dt = System.DateTime.Now;
				DirectoryInfo di = new DirectoryInfo(screenshotPath);
				FileInfo[] fi = di.GetFiles();
				fi.OrderBy(o => o.CreationTime);
				List<string> pngFilePath = new List<string>();
				foreach (var item in fi)
				{
					//获取2分钟之前的所有图片
					if ((dt - item.CreationTime) < TimeSpan.FromMinutes(2))
					{
						pngFilePath.Add(item.FullName);
					}
				}
				GenerateGif(pngFilePath, TestSuite.CurrentTestContainer.Name + dt.ToString("yyyyMMddHHmmssfff"));
				Report.Info("Finished.");
			}
			else
			{
				Report.Warn("TestCase not failed.");
			}
		}

		/// <summary>
		/// 清理10分钟以前的截图
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		internal static void CleanUp(object sender, ElapsedEventArgs e)
		{
			string screenshotPath = AppDomain.CurrentDomain.BaseDirectory + "\\scr";
			if (!Directory.Exists(screenshotPath))
			{
				Report.Warn("Screenshot folder not found.");
				return;
			}
			System.DateTime dt = System.DateTime.Now;
			DirectoryInfo di = new DirectoryInfo(screenshotPath);
			FileInfo[] fi = di.GetFiles();
			//fi.OrderBy(o => o.CreationTime);
			Report.Info("Cleaning screenshots captured 10min ago...");
			foreach (var item in fi)
			{
				//获取10分钟之前的所有图片
				if ((dt - item.CreationTime) > TimeSpan.FromMinutes(10))
				{
					item.Delete();
				}
			}
		}

		/// <summary>
		/// 生成gif
		/// </summary>
		/// <param name="files">文件列表</param>
		/// <param name="gifName">gif文件名</param>
		private static void GenerateGif(List<string> files, string gifName)
		{
			string gifPath = AppDomain.CurrentDomain.BaseDirectory + "\\ReportGIF";
			if (!Directory.Exists(gifPath))
			{
				Directory.CreateDirectory(gifPath);
			}
			using (var gif = File.OpenWrite(gifPath + "\\" + gifName + ".gif"))
			{
				using (var encoder = new GifEncoder(gif, null, null, null))
				{
					for (var i = 0; i < files.Count; i++)
					{
						using (var frame = Image.FromStream(new FileStream(files[i], FileMode.Open)))
						{
							//encoder.AddFrame(frame.ScaleToFit(new Size(540, 405), false), 0, 0, TimeSpan.FromMilliseconds(200));
							encoder.AddFrame(frame, 0, 0, TimeSpan.FromMilliseconds(250));
						}
					}
				}
			}
		}

		/// <summary>
		/// 无损压缩图片 抄网上
		/// </summary>
		/// <param name="iSource">原图片</param>
		/// <param name="dFile">压缩后保存位置</param>
		/// <param name="dHeight">压缩后高度</param>
		/// <param name="dWidth">压缩后宽度</param>
		/// <param name="flag">压缩质量(数字越小压缩率越高) 1-100</param>
		/// <returns></returns>
		private static bool GetPicThumbnail(Bitmap iSource, string dFile, int dHeight, int dWidth, int flag)
		{
			//System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
			ImageFormat tFormat = iSource.RawFormat;
			Bitmap ob = new Bitmap(dWidth, dHeight);
			Graphics g = Graphics.FromImage(ob);
			g.Clear(Color.WhiteSmoke);
			g.CompositingQuality = CompositingQuality.HighQuality;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			//g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);
			g.DrawImage(iSource, new Rectangle(0, 0, iSource.Width, iSource.Height), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);
			g.Dispose();

			//以下代码为保存图片时，设置压缩质量
			EncoderParameters ep = new EncoderParameters();
			long[] qy = new long[1];
			qy[0] = flag;//设置压缩的比例1-100
			EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
			ep.Param[0] = eParam;
			try
			{
				ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
				ImageCodecInfo jpegICIinfo = null;
				for (int x = 0; x < arrayICI.Length; x++)
				{
					if (arrayICI[x].FormatDescription.Equals("JPEG"))
					{
						jpegICIinfo = arrayICI[x];
						break;
					}
				}
				if (jpegICIinfo != null)
				{
					ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
				}
				else
				{
					ob.Save(dFile, tFormat);
				}
				return true;
			}
			catch
			{
				return false;
			}
			finally
			{
				iSource.Dispose();
				ob.Dispose();
			}
		}

		/// <summary>
		/// 未实现
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void FailureGif(object sender, ElapsedEventArgs e)
		{
			Report.Info("gif");
			Report.Info(TestSuite.Current.CurrentTestContainer.Name);
			Report.Info(TestModuleLeaf.Current.Name);
			if (TestSuite.CurrentTestContainer.Status == Ranorex.Core.Reporting.ActivityStatus.Success)
			{
				System.DateTime dt = System.DateTime.Now;
				DirectoryInfo di = new DirectoryInfo(@"C:\Users\kissKurisu\Desktop\scr");
				FileInfo[] fi = di.GetFiles();
				fi.OrderBy(o => o.CreationTime);
				List<string> pngFilePath = new List<string>();
				foreach (var item in fi)
				{
					//获取5分钟之前的所有图片
					if ((dt - item.CreationTime) < TimeSpan.FromMinutes(5))
					{
						pngFilePath.Add(item.FullName);
					}
				}
				GenerateGif(pngFilePath, TestSuite.CurrentTestContainer.Name);
			}
		}
	}
}
