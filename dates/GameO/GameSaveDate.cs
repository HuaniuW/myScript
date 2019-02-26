using System.Collections;
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

    string _fileName = Application.persistentDataPath + "/UnityUserData";


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
        CreateTextFile(_fileName, s, false);

      
    }

    public UserDate GetSaveDateByName(string cGKDateName)
    {
        //Debug.Log(cGKDateName);
        // _fileName = Application.persistentDataPath + "/" + _fileName;
        string str = Application.persistentDataPath + "/" + cGKDateName;
        if(Globals.isDebug)Debug.Log(str);
        try
        {
            string strTemp = LoadTextFile(str, false);
            //反序列化对象
            UserDate userD = DeserializeObject(strTemp, typeof(UserDate)) as UserDate;
            //Debug.Log(userD.guankajilu);
            return userD;
        }
        catch
        {
            if (Globals.isDebug) Debug.Log("系统读取XML出现错误，请检查");
        }
        return null;
    }

    public bool IsHasSaveDateByName(string SaveDateName)
    {
        //string str = Application.persistentDataPath + "/" + SaveDateName;
        if (GetSaveDateByName(SaveDateName) != null) return true;
        return false;
    }

    string[] dateZu = { "date1", "date2", "UnityUserData" };
    public bool IsHasSaveDate()
    {
       for(var i = 0; i < dateZu.Length; i++)
        {
            string str = dateZu[i];
            if (IsHasSaveDateByName(str)) return true;
        }
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
