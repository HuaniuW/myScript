﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

using System.Security.Cryptography;


public class GameSaveDate{
    //存档时间
    public string GameSaveTime = "";
    //玩家信息  当前生命值   当前能量值
    //玩家装备 已经装备的道具 道具id
    //全局关卡信息 哪些机关已经触发 哪些关卡（有控制流程的）已经走完流程


    static GameSaveDate instance;
    static public GameSaveDate GetInstance()
    {
        if (instance==null) instance = new GameSaveDate();
        return instance;
    }
    
    string _fileURL = Application.persistentDataPath + "/date1";


    // Use this for initialization
    public GameSaveDate()
    {
        
    }

    public void GetTestSave()
    {
        UserDate user = new UserDate();
        user.userName = "乐逍遥";
        user.onlyId = 1;
        string s = SerializeObject(user, typeof(UserDate));
        //创建XML文件且写入数据
        CreateTextFile(_fileURL, s, false);
    }

    public void GetNewGameSave()
    {

    }


    string StrURL = "SaveDate";
    //************************************ 地图数据的 存储 ******************************************************************************
    //记录生成地图 地形 等数据
    public void SaveMapDateByURLName(MapSaveDate date,string URL = "MAPDATE")
    {
        //string _fileURL2 = Application.persistentDataPath + "/" + URL;
        string _fileURL2 = Application.dataPath +"/"+ StrURL + "/" + URL;
        
        //if (Globals.isDebug) Debug.Log("_fileURL2> "+ _fileURL2);
        string s = SerializeObject(date, typeof(MapSaveDate));
        //创建XML文件且写入数据
        CreateTextFile(_fileURL2, s, false);
    }

    public MapSaveDate GetMapSaveDateByName(string cGKDateName = "MAPDATE")
    {
        // _fileName = Application.persistentDataPath + "/" + _fileName;
        //string str = Application.persistentDataPath + "/" + cGKDateName;
        string str = Application.dataPath + "/" + StrURL + "/" + cGKDateName;
        //if(Globals.isDebug)Debug.Log(str);
        try
        {
            string strTemp = LoadTextFile(str, false);
            //反序列化对象
            MapSaveDate userD = DeserializeObject(strTemp, typeof(MapSaveDate)) as MapSaveDate;
            //if (Globals.isDebug) Debug.Log(userD.userName);
            return userD;
        }
        catch
        {
            if (Globals.isDebug) Debug.Log("系统读取 地图 XML出现错误，请检查");
        }
        return null;
    }

    //******************************************************************************************************************











    //************************************ 选择数据 other 存储 ******************************************************************************
    //记录 剧情 选择 改变的 数据   这数据可以改变游戏方向
    public void SaveOtherDateByURLName(OtherSaveDate date, string URL = "OTHER")
    {
        //string _fileURL2 = Application.persistentDataPath + "/" + URL;
        string _fileURL2 = Application.dataPath + "/" + StrURL + "/" + URL;
        //if (Globals.isDebug) Debug.Log("_fileURL2> "+ _fileURL2);
        string s = SerializeObject(date, typeof(OtherSaveDate));
        //创建XML文件且写入数据
        CreateTextFile(_fileURL2, s, false);
        Debug.Log("ot 其他数据写入  "+s+"    路径> "+ _fileURL2+"   date内容  "+date.GlobalOtherDate);
    }



    public OtherSaveDate GetOtherSaveDateByName(string cGKDateName = "OTHER")
    {
        // _fileName = Application.persistentDataPath + "/" + _fileName;
        //string str = Application.persistentDataPath + "/" + cGKDateName;
        string str = Application.dataPath + "/" + StrURL + "/" + cGKDateName;
        Debug.Log("ot 取other数据路径  "+ str);
        //if(Globals.isDebug)Debug.Log(str);
        try
        {
            string strTemp = LoadTextFile(str, false);
            //反序列化对象
            OtherSaveDate userD = DeserializeObject(strTemp, typeof(OtherSaveDate)) as OtherSaveDate;
            if (Globals.isDebug) Debug.Log("  ot 取到other数据  "+userD );
            return userD;
        }
        catch
        {
            if (Globals.isDebug) Debug.Log("系统读取 地图 XML出现错误，请检查");
        }
        return null;
    }
    //******************************************************************************************************************














    //************************************ 一般数据 存储 ******************************************************************************
    public void SaveDateByURLName(string URL,UserDate date)
    {
        //string _fileURL2 = Application.persistentDataPath + "/" + URL;
        string _fileURL2 = Application.dataPath + "/" + StrURL + "/" + URL;
        //if (Globals.isDebug) Debug.Log("_fileURL2> "+ _fileURL2);
        string s = SerializeObject(date, typeof(UserDate));
        //创建XML文件且写入数据
        CreateTextFile(_fileURL2, s, false);
    }

    public UserDate GetSaveDateByName(string cGKDateName)
    {
        // _fileName = Application.persistentDataPath + "/" + _fileName;
        //string str = Application.persistentDataPath + "/" + cGKDateName;
        string str = Application.dataPath + "/" + StrURL + "/" + cGKDateName;
        if (Globals.isDebug)Debug.Log("存档位置---： "+str);
        try
        {
            string strTemp = LoadTextFile(str, false);
            //反序列化对象
            UserDate userD = DeserializeObject(strTemp, typeof(UserDate)) as UserDate;
            //if (Globals.isDebug) Debug.Log(userD.userName);
            return userD;
        }
        catch
        {
            if (Globals.isDebug) Debug.Log("系统读取XML出现错误，请检查");
        }
        return null;
    }
    //******************************************************************************************************************

    public bool IsHasSaveDateByName(string SaveDateName)
    {
        //string str = Application.persistentDataPath + "/" + SaveDateName;

        if (GetSaveDateByName(SaveDateName) != null) return true;
        return false;
    }

    string[] dateZu = { "date1", "date2", "date3" };
    public bool IsHasSaveDate()
    {
        if (IsHasSaveDateByName(GlobalSetDate.instance.saveDateName)) return true;
        return false;
    }

    public bool IsHasMapSaveDate()
    {
        if (IsHasSaveDateByName(GlobalSetDate.instance.saveDateName+"Map")) return true;
        return false;
    }



    public string SerializeObject(object pObject, System.Type ty)
    {
        string XmlizedString = null;
        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer xs = new XmlSerializer(ty);
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        xs.Serialize(xmlTextWriter, pObject);
        memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
        return XmlizedString;
    }

    /// xml字符串转换数据对象
    public object DeserializeObject(string pXmlizedString, System.Type ty)
    {
        XmlSerializer xs = new XmlSerializer(ty);
        MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        return xs.Deserialize(memoryStream);
    }
    //UTF8字节数组转字符串
    public string UTF8ByteArrayToString(byte[] characters)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        string constructedString = encoding.GetString(characters);
        return (constructedString);
    }

    //字符串转UTF8字节数组
    public byte[] StringToUTF8ByteArray(String pXmlString)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] byteArray = encoding.GetBytes(pXmlString);
        return byteArray;
    }

    public void CreateTextFile(string fileName, string strFileData, bool isEncryption)
    {
        StreamWriter writer;                               //写文件流
        string strWriteFileData;
        if (isEncryption)
        {
            strWriteFileData = Encrypt(strFileData);  //是否加密处理
        }
        else
        {
            strWriteFileData = strFileData;             //写入的文件数据
        }

        writer = File.CreateText(fileName);
        writer.Write(strWriteFileData);
        writer.Close();                                    //关闭文件流
    }

    public string LoadTextFile(string fileName, bool isEncryption)
    {
        StreamReader sReader;                              //读文件流
        string dataString;                                 //读出的数据字符串

        sReader = File.OpenText(fileName);
        dataString = sReader.ReadToEnd();
        sReader.Close();                                   //关闭读文件流

        if (isEncryption)
        {
            return Decrypt(dataString);                      //是否解密处理
        }
        else
        {
            return dataString;
        }

    }
    /// 加密方法
    /// 描述： 加密和解密采用相同的key,具体值自己填，但是必须为32位
    public string Encrypt(string toE)
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789012");
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateEncryptor();
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toE);
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    /// 解密方法
    /// 描述： 加密和解密采用相同的key,具体值自己填，但是必须为32位
    public string Decrypt(string toD)
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789012");
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateDecryptor();
        byte[] toEncryptArray = Convert.FromBase64String(toD);
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return UTF8Encoding.UTF8.GetString(resultArray);
    }
    

}
