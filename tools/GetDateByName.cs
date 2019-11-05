using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GetDateByName : MonoBehaviour {


    static GetDateByName instance;
    public static GetDateByName GetInstance()
    {
        if (instance == null) instance = new GetDateByName();
        return instance;
    }

	// Use this for initialization
	void Start () {
		
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


}
