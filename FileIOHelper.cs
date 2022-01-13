using Ranorex;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EpointAutomationHelper
{
    /// <summary>
    /// 文件读写工具类
    /// </summary>
    public class FileIOHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="obj">对象</param>
        public static void Serializable(string fileName, object obj)
        {
            fileName = fileName.EndsWith(".db") ? fileName : fileName + ".db";
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            DirHelper.CreateFolder(Path.GetDirectoryName(fileName));
            BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
            binFormat.Serialize(fStream, obj);
            fStream.Close();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>对象</returns>
        public static object UnSerializable(string fileName)
        {
            try
            {
                fileName = fileName.EndsWith(".db") ? fileName : fileName + ".db";
                Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
                var obj = binFormat.Deserialize(fStream);//反序列化对象
                fStream.Close();
                return obj;
            }
            catch (Exception e)
            {
                Report.Info("UnSerializableException" + e.ToString());
                return null;
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="obj">被复制的对象</param>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <returns>复制的对象</returns>
        public static T CopyObject<T>(T obj)
        {
            Stream fStream = new FileStream("tmp", FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
            binFormat.Serialize(fStream, obj);
            fStream.Position = 0;//重置流位置
            T result = (T)binFormat.Deserialize(fStream);//反序列化对象
            fStream.Close();
            return result;
        }
    }
}
