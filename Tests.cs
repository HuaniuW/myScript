using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Tests : MonoBehaviour {
    private string[] dict;

    // Use this for initialization
    void Start () {
        MyClass m = new MyClass();
        Type type1 = m.GetType();
        Type type2 = typeof(MyClass);
        string s = type1.BaseType.ToString();//获得父类名
        FieldInfo[] fields = type1.GetFields();//获得所有非私有字段信息
       // print("???   "+ fields.Length+"  i  "+fields[0]);
        foreach (FieldInfo item in fields)
        {
            //print("?>>>>>>>  "+item.Name+"    "+item.FieldType);
            //Console.WriteLine(item.Name);
        }

        PropertyInfo[] propertys = type1.GetProperties();//获得所有非私有属性信息
        foreach (PropertyInfo item in propertys)
        {
            //print(item.Name+"   length  "+propertys.Length+"    "+item.GetType());
        }

        FieldInfo fieldInfo = type1.GetField("arr1");
        dict = fieldInfo.GetValue(m) as string[];

        //DataZS sss = DataZS.GetInstance();
        DataZS sss = new DataZS();
        print("------------------>   "+ dict.Length+"   m   "+sss);


        string[] arrt = GetArr("arr1", m );
        print("xx  "+ arrt[1]);

       
        //this["Tests1"]();

        //Console.ReadKey();
    }

    string[] GetArr(string _name, System.Object obj)
    {
        Type type = obj.GetType();
        FieldInfo fieldInfo = type.GetField(_name);
        if (fieldInfo == null) {
            return null;
        }
        else
        {
            return fieldInfo.GetValue(obj) as string[];
        }
    }

    string Tests1()
    {
        return "test!";
    }

    // Update is called once per frame
    void Update () {
		
	}

}

class MyClass
{
    public string[] abc = null;

    public string a = "hi";

    public static string[] arr1 = { "a", "b", "3" };
    public int MyProperty { get; set; }
    private string Name
    {
        get; set;
    }
    public void Say()
    {
        Console.WriteLine("hello world");
    }
}
