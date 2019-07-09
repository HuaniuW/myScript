﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;

using System;

public class GlobalTools : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    ///根据在场景根目录中位置寻找对象
    /// </summary>
    /// <param name="_urlName">对象路径名</param>
    /// <returns></returns>
    public static GameObject FindObjByName(string _urlName)
    {
        GameObject obj = GameObject.Find("/" + _urlName) as GameObject;
        if (obj == null) obj = GameObject.Find("/" + _urlName + "(Clone)") as GameObject;
        return obj;
    }

    //
    /// <summary>
    ///根据名字动态加载 并实例化 GameObject  （预制对象必须在resources 文件夹下） 
    /// </summary>
    /// <param name="ObjName"></param>
    /// <returns></returns>
    public static GameObject GetGameObjectByName(string ObjName)
    {
        //print("ObjName    "+ ObjName);
        GameObject obj = Resources.Load(ObjName) as GameObject;
        obj = Instantiate(obj);
        return obj;
    }

    //Text text = btn.transform.Find("Text").GetComponent<Text>();

    //public static GameObject FindObjInTransformByName(GameObject obj,string _name) {
    //    GameObject _obj = obj.Find(_name) as GameObject;
    //    return _obj;
    //}

    /// <summary>
    /// 控制对象的 透明度
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="num"></param>
    public static void CanvasGroupAlpha(CanvasGroup CGroup,float num)
    {
        if (num > 1) num = 1;
        if (num < 0) num = 0;
        bool isTrue = num == 0 ? false : true;
        CGroup.alpha = num;
        CGroup.interactable = isTrue;
        CGroup.blocksRaycasts = isTrue;
    }



    public static void PlayAudio(string AudioName, System.Object obj,float _value = 1)
    {
        //(this[AudioName] as AudioSource)
        //print(AudioName);
        AudioSource cAudio = GetDicSSByName(AudioName, obj);
        if (cAudio)
        {
            cAudio.volume = _value * GlobalSetDate.instance.GetSoundEffectValue();
            cAudio.Play();
        }

    }

    public static AudioSource GetDicSSByName(string _name, System.Object obj)
    {
        Type type = obj.GetType();
        FieldInfo fieldInfo = type.GetField(_name);
        //print("  type " + type+ "    fieldInfo  "+ fieldInfo);
        if (fieldInfo == null) return null;
        return fieldInfo.GetValue(obj) as AudioSource;
    }


    public static bool IsHasDate(string changeDate, string[] currentGKDateArr)
    {
        string name = changeDate.Split('-')[0];
        for (var i = 0; i < currentGKDateArr.Length; i++)
        {
            string date = currentGKDateArr[i];
            string dateName = date.Split('-')[0];
            if (name == dateName) return true;
        }
        return false;
    }

    public static string GetHasDate(string changeDate, string[] currentGKDateArr)
    {
        string getDate = "";
        string name = changeDate.Split('-')[0];
        for (var i = 0; i < currentGKDateArr.Length; i++)
        {
            string date = currentGKDateArr[i];
            string dateName = date.Split('-')[0];
            if (name == dateName) {
                getDate = date;
                return getDate;
            }
        }
        return getDate;
    }
}
