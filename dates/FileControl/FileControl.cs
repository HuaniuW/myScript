using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  //操作文件夹时需引用该命名空间
using System.Text;


public class FileControl
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    static FileControl instance;
    public static FileControl GetInstance()
    {
        if (instance == null)
        {
            instance = new FileControl();
        }
        return instance;
    }

    string __url = "/TxtFile/";

    public void CreateTxt(string TxtName = "test", bool IsReCreate = false)
    {
        //先判断 是否有 txt了


        string path = Application.dataPath + __url+ TxtName+".txt";
        FileStream file = new FileStream(path, FileMode.Create);

        byte[] bts = System.Text.Encoding.UTF8.GetBytes("");
        // 文件写入数据流
        file.Write(bts, 0, bts.Length);
        if (file != null)
        {
            //清空缓存
            file.Flush();
            // 关闭流
            file.Close();
            //销毁资源
            file.Dispose();
        }
    }


    public void AddTxtTextByFileStream(string txtText)
    {
        string path = Application.dataPath + "/Json/MyTxtByFileStream.txt";
        // 文件流创建一个文本文件
        FileStream file = new FileStream(path, FileMode.Create);
        //得到字符串的UTF8 数据流
        byte[] bts = System.Text.Encoding.UTF8.GetBytes(txtText);
        // 文件写入数据流
        file.Write(bts, 0, bts.Length);
        if (file != null)
        {
            //清空缓存
            file.Flush();
            // 关闭流
            file.Close();
            //销毁资源
            file.Dispose();
        }
    }

    public void ReWriteTxt(string TxtName = "test")
    {
        string path = Application.dataPath + __url + TxtName + ".txt";

        string[] strs = { "123", "321", "234" };

        File.WriteAllLines(path, strs);

        /*
         文本内容为：（逐行显示的）
         123
         321
         234
         原文本内容为：
         第一种方法添加text文本
         */

        //如果想在某一行中添加一行新的，可以将原txt文本读取下来，保存到数组里，
        //然后新建一个数组，载将原数组文本与新加入的行数据同时写入到新数组里
        //然后用新数组数据替换（重写）原来的数据
    }




    //创建txt 方法2
    public void AddTxtTextByFileInfo(string TxtName = "test")
    {
        string path = Application.dataPath + __url + TxtName + ".txt";
        StreamWriter sw;
        FileInfo fi = new FileInfo(path);

        if (!File.Exists(path))
        {
            Debug.Log("没有 改文件 创建一个 新文件！！！");
            sw = fi.CreateText();
        }
        else
        {
            sw = fi.AppendText();   //在原文件后面追加内容      
        }
        sw.WriteLine("ttedtedte");
        sw.Close();
        sw.Dispose();
    }

    //逐行读取 txt
    public void ReadTxtSecond(string TxtName = "test")
    {
        string path = Application.dataPath + __url + TxtName + ".txt";
        //逐行读取返回的为数组数据
        string[] strs = File.ReadAllLines(path);

        foreach (string item in strs)
        {
            Debug.Log(item); //第二种方法添加txt文本
                         //第二种方法添加txt文本  
        }
    }



    //根据 键值写入
    public void WriteInByKey(string key,string value, string TxtName = "test")
    {
        string path = Application.dataPath + __url + TxtName + ".txt";
        //逐行读取返回的为数组数据
        string[] strs = File.ReadAllLines(path);

        for (int i=0;i<strs.Length;i++)
        {
            if(key == strs[i].Split(':')[0])
            {
                strs[i] = key + ":" + value;
                break;
            }
        }
        //重新写入文本
        File.WriteAllLines(path, strs);
    }



    //格局键值 读取
    public string GetValueByKey(string key, string TxtName = "test")
    {
        string path = Application.dataPath + __url + TxtName + ".txt";
        //逐行读取返回的为数组数据
        string[] strs = File.ReadAllLines(path);
        for (int i = 0; i < strs.Length; i++)
        {
            if (key == strs[i].Split(':')[0])
            {
                return strs[i].Split(':')[1];
            }
        }
        return "";
    }


}
