using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.Util;
using Ranorex;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;

namespace EpointAutomationHelper
{
	/// <summary>
	/// Excel表格工具类
	/// </summary>
	public class ExcelHelper : IDisposable
	{
		private string fileName = null; //文件名
		private IWorkbook workbook = null;
		private FileStream fs = null;
		private bool disposed;

		/// <summary>
		/// 初始化Excel
		/// </summary>
		/// <param name="fileName">Excel文件名</param>
		public ExcelHelper(string fileName)
		{
			this.fileName = fileName;
			disposed = false;
		}

		/// <summary>
		/// 将DataTable数据导入到excel中
		/// </summary>
		/// <param name="data">要导入的数据</param>
		/// <param name="isColumnWritten">DataTable的列名是否要导入</param>
		/// <param name="sheetName">要导入的excel的sheet的名称</param>
		/// <returns>导入数据行数(包含列名那一行)</returns>
		public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
		{
			int i = 0;
			int j = 0;
			int count = 0;
			ISheet sheet = null;

			fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			if (fileName.IndexOf(".xlsx") > 0) // 2007版本
				workbook = new XSSFWorkbook();
			else if (fileName.IndexOf(".xls") > 0) // 2003版本
				workbook = new HSSFWorkbook();

			try
			{
				if (workbook != null)
				{
					sheet = workbook.CreateSheet(sheetName);
				}
				else
				{
					return -1;
				}

				if (isColumnWritten == true) //写入DataTable的列名
				{
					IRow row = sheet.CreateRow(0);
					for (j = 0; j < data.Columns.Count; ++j)
					{
						row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
					}
					count = 1;
				}
				else
				{
					count = 0;
				}

				for (i = 0; i < data.Rows.Count; ++i)
				{
					IRow row = sheet.CreateRow(count);
					for (j = 0; j < data.Columns.Count; ++j)
					{
						//row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
						row = SetFontStyle(workbook,row,j,data.Rows[i][j].ToString());
						//设置单元格宽度
						workbook.GetSheet(sheetName).SetColumnWidth(j, 15 * 256);
					}
					++count;
				}
				workbook.Write(fs); //写入到excel
				return count;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.Message);
				return -1;
			}
		}
		
		/// <summary>
		/// 设置标记为[Error][Success][Block]的数据为指定的颜色--zjy
		/// </summary>
		/// <param name="workbook">工作表</param>
		/// <param name="row">新增行</param>
		/// <param name="cellIndex">列索引</param>
		/// <param name="cellValue">列设置值</param>
		/// <returns>该新增行</returns>
		public IRow SetFontStyle(IWorkbook workbook,IRow row,int cellIndex,string cellValue)
		{
			ICell cell =row.CreateCell(cellIndex);
			ICellStyle style = workbook.CreateCellStyle();
			IFont font = workbook.CreateFont();
			
			//设置字体
			font.FontName="宋体";//字体
			font.FontHeightInPoints = 11;//字号
			//取出文本前的标记
			string flag = Regex.Match(cellValue,@"\[*.*]").Value;
			switch(flag)
			{
				case "[Error]":
					font.Color = HSSFColor.Red.Index;
					font.IsBold = true;//粗体
					//去掉错误标记
					cellValue = cellValue.Replace("[Error]","");
					break;
				case "[Success]":
					font.Color = HSSFColor.Green.Index;
					font.IsBold = true;//粗体
					//去掉成功标记
					cellValue = cellValue.Replace("[Success]","");
					break;
				case "[Block]":
					font.Color = HSSFColor.Grey50Percent.Index;
					font.IsBold = true;//粗体
					//去掉阻塞标记
					cellValue = cellValue.Replace("[Block]","");
					break;
				default:
					//无标记普通正文，黑色不加粗
					font.Color = HSSFColor.Black.Index;
					font.IsBold = false;//粗体
					break;
			}
			style.SetFont(font);
			
			//设置边框
			style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
			style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
			style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
			style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
			
			//自动换行
			style.WrapText = true;
			cell.CellStyle = style;
			cell.SetCellValue(cellValue);
			return row;
		}

		/// <summary>
		/// 将excel中的数据导入到DataTable中
		/// </summary>
		/// <param name="sheetName">excel工作薄sheet的名称</param>
		/// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
		/// <returns>返回的DataTable</returns>
		public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
		{
			ISheet sheet = null;
			DataTable data = new DataTable();
			int startRow = 0;
			try
			{
				fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				if (fileName.IndexOf(".xlsx") > 0) // 2007版本
					workbook = new XSSFWorkbook(fs);
				else if (fileName.IndexOf(".xls") > 0) // 2003版本
					workbook = new HSSFWorkbook(fs);

				if (sheetName != null)
				{
					sheet = workbook.GetSheet(sheetName);
					if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
					{
						sheet = workbook.GetSheetAt(0);
					}
				}
				else
				{
					sheet = workbook.GetSheetAt(0);
				}
				if (sheet != null)
				{
					IRow firstRow = sheet.GetRow(0);
					int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

					if (isFirstRowColumn)
					{
						for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
						{
							ICell cell = firstRow.GetCell(i);
							if (cell != null)
							{
								string cellValue = cell.StringCellValue;
								if (cellValue != null)
								{
									DataColumn column = new DataColumn(cellValue);
									data.Columns.Add(column);
								}
							}
						}
						startRow = sheet.FirstRowNum + 1;
					}
					else
					{
						startRow = sheet.FirstRowNum;
					}

					//最后一列的标号
					int rowCount = sheet.LastRowNum;
					for (int i = startRow; i <= rowCount; ++i)
					{
						IRow row = sheet.GetRow(i);
						if (row == null) continue; //没有数据的行默认是null

						DataRow dataRow = data.NewRow();
						for (int j = row.FirstCellNum; j < cellCount; ++j)
						{
							if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
								dataRow[j] = row.GetCell(j).ToString();
						}
						data.Rows.Add(dataRow);
					}
				}

				return data;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.Message);
				return null;
			}
		}

		/// <summary>
		/// 写入Excel
		/// </summary>
		/// <param name="excelPath">Excel文件路径</param>
		/// <param name="dataTable">Data表</param>
		/// <param name="sheetName">写入的Sheet名，默认：MySheet</param>
		public static void WriteExcel(string excelPath, DataTable dataTable, string sheetName = "MySheet")
		{
			try
			{
				using (ExcelHelper excelUtil = new ExcelHelper(excelPath))
				{
					DirHelper.CreateFolder(Path.GetDirectoryName(excelPath));
					excelUtil.DataTableToExcel(dataTable, sheetName, true);
				}
			}
			catch (Exception ex)
			{
				Report.Error("WriteExcelException: " + ex.Message);
			}
		}



		//TODO:打印格式优化
		/// <summary>
		/// 打印数据
		/// </summary>
		/// <param name="data">DataTable</param>
		public static void PrintData(DataTable data)
		{
			if (data == null) return;
			for (int i = 0; i < data.Rows.Count; ++i)
			{
				for (int j = 0; j < data.Columns.Count; ++j)
					Console.Write("{0} ", data.Rows[i][j]);
				//Report.Log(ReportLevel.None, string.Format("{0} ", data.Rows[i][j]));
				Console.Write("\n");
			}
		}

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 释放资源
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (fs != null)
						fs.Close();
				}

				fs = null;
				disposed = true;
			}
		}


		#region 读取Excel

		///   <summary>
		///   将Excel中的数据读入DataSet
		///   </summary>
		///   <param   name="excelPath">文件路径和文件名</param>
		/// <param name="sheetName">工作表名称</param>
		/// <returns>DataSet</returns>
		public static DataSet ReadExcel(string excelPath, string sheetName)
		{
			string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + excelPath + ";" + "Extended Properties = 'Excel 8.0;IMEX=1'";
			OleDbConnection conn = new OleDbConnection(strConn);
			conn.Open();
			string strExcel = "";
			OleDbDataAdapter myCommand = null;
			strExcel = "select * from [" + sheetName + "$]";
			myCommand = new OleDbDataAdapter(strExcel, strConn);
			DataSet ds = new DataSet();
			myCommand.Fill(ds, "Data");
			conn.Close();
			return ds;
		}
		
		#endregion
	}
}
