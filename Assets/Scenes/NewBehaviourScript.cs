using Sxer.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //获取当前文件编码：
        Debug.LogError("当前文件编码：" + GetFileEncodeType(@"C:\Users\DS\Desktop\tet1.ini"));

        //当前系统默认编码
        Debug.LogError("系统默认编码："+System.Text.Encoding.Default);


        Debug.Log(INIHelper.ReadIniString(@"C:\Users\DS\Desktop\tet1.ini", "汉字1", "1", "0"));

        //Encoding end = new ASCIIEncoding();
        //string str = string.Empty;
        //using (StreamReader sr = new StreamReader(@"C:\Users\DS\Desktop\tet1.ini", Encoding.ASCII))
        //{
        //    str = sr.ReadToEnd();
        //    Debug.LogError(str);
        //}

    }
    public static Encoding GetFileEncodeType(string filename)
    {
        FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        Byte[] buffer = br.ReadBytes(3);
        Debug.Log(String.Format("{0:X2}", buffer[0]));
        Debug.Log(String.Format("{0:X2}", buffer[1]));
        Debug.Log(String.Format("{0:X2}", buffer[2]));
        //Debug.Log(String.Format("{0:X2}", buffer[3]));
        if (buffer.Length < 2) { return null; }
        if (buffer[0] >= 0xEF)
        {
            if (buffer[0] == 0xEF && buffer[1] == 0xBB)
            {
                return Encoding.UTF8;
            }
            else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
            {
                return Encoding.BigEndianUnicode;
            }
            else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
            {
                return Encoding.Unicode;
            }
            else
            {
                return Encoding.Default;
            }
        }
        else
        {
            return Encoding.Default;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
