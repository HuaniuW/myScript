﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GetDateByName{


    static GetDateByName instance;
    public static GetDateByName GetInstance()
    {
        if (instance == null) {
            //GameObject go = new GameObject("GetDateByName");
            //DontDestroyOnLoad(go);
            //instance = go.AddComponent<GetDateByName>();
            instance = new GetDateByName();
        }
        //instance = new GetDateByName();
        return instance;
    }

	
	

    public Dictionary<string, string> GetDicSSByName(string _name, System.Object obj)
    {
        Type type = obj.GetType();
        FieldInfo fieldInfo = type.GetField(_name);
        //print("  type " + type+ "    fieldInfo  "+ fieldInfo);
        if (fieldInfo == null) return null;
        return fieldInfo.GetValue(obj) as Dictionary<string, string>;
    }

    public Dictionary<string, float> GetDicSFByName(string _name, System.Object obj)
    {
        Type type = obj.GetType();
        FieldInfo fieldInfo = type.GetField(_name);
        if (fieldInfo == null) return null;
        return fieldInfo.GetValue(obj) as Dictionary<string, float>;
    }

	public List<string> GetListByName(string _name, System.Object obj)
    {
        Type type = obj.GetType();
        FieldInfo fieldInfo = type.GetField(_name);
        if (fieldInfo == null) return null;
		return fieldInfo.GetValue(obj) as List<string>;
    }


    public ParticleSystem GetTXByName(string _name, System.Object obj)
    {
        Type type = obj.GetType();
        FieldInfo fieldInfo = type.GetField(_name);
        if (fieldInfo == null) return null;
        return fieldInfo.GetValue(obj) as ParticleSystem;
    }

    public Transform GetTransformByName(string _name, System.Object obj)
    {
        Type type = obj.GetType();
        FieldInfo fieldInfo = type.GetField(_name);
        if (fieldInfo == null) return null;
        return fieldInfo.GetValue(obj) as Transform;
    }

    public string GetStrinByName(string _name, System.Object obj)
    {
        Type type = obj.GetType();
        FieldInfo fieldInfo = type.GetField(_name);
        if (fieldInfo == null) return null;
        return fieldInfo.GetValue(obj) as string;
    }
}
