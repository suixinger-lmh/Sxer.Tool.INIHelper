using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Sxer.Tool
{
    public static class INIHelper
    {
        #region ini win32API读取

        //声明读写INI文件的API函数
        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        /// <summary>
        /// 读取ini文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="Section">节 名称</param>
        /// <param name="Ident">key值</param>
        /// <param name="Default">读取失败返回值</param>
        /// <returns></returns>
        public static string ReadIniString(string filePath, string Section, string Ident, string Default)
        {
            try
            {
                Byte[] Buffer = new Byte[65535];
                int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), filePath);
                //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
                string s = System.Text.Encoding.UTF8.GetString(Buffer, 0, bufLen);
                return s.Trim();
            }
            catch (Exception ex)
            {
                Debug.LogError("读取ini异常！" + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 写入ini文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="Section">节 名称</param>
        /// <param name="Ident">key值</param>
        /// <param name="Value">value写入值</param>
        /// <returns></returns>
        public static bool WriteIniString(string filePath, string Section, string Ident, string Value)
        {
            try
            {
                bool bResult = WritePrivateProfileString(Section, Ident, Value, filePath);
                return bResult;
            }
            catch (Exception ex)
            {
                Debug.LogError("写入ini异常！" + ex.Message);
                return false;
            }

        }



        #endregion
    }
}

