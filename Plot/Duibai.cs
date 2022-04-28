using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Duibai : MonoBehaviour
{
    //对白 剧情 文本 
    // Start is called before the first frame update
    void Start()
    {
        
    }


    static Duibai instance;
    static public Duibai GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("Duibai");
            //DontDestroyOnLoad(go);
            instance = go.AddComponent<Duibai>();
        } //instance = new DataZS();
        return instance;
    }

    public string GetStrinByName(string _name, System.Object obj)
    {
        Type type = obj.GetType();
        FieldInfo fieldInfo = type.GetField(_name);
        if (fieldInfo == null) return null;
        return fieldInfo.GetValue(obj) as string;
    }


    public string GetTxtById(string id)
    {
        //判断 语言类型
        id += Globals.language;
        print("  db  id "+id);
        string str = GetStrinByName(id, Duibai.GetInstance());
        print("  db  str " + str);
        return str;
    }


    public string str1chinese = "如果有机会可以重新来过，会不一样吗！";
    public string str1chineseF = "如果有機會可以重新來過，會不一樣嗎！";
    public string str1english = "If there is a chance to do it all over again, will it be different!";
    public string str1japan = "もう一度やり直す機会があれば、違うのでしょうか!";
    public string str1korean = "다시 할 수 있는 기회가 생긴다면 달라질까요!";
    public string str1portugal = "Si existe la posibilidad de hacerlo todo de nuevo, ¿será diferente!";

    public string str1German = "Wenn es eine Chance gibt, alles noch einmal zu machen, wird es anders sein!";

    public string str1French = "S'il y a une chance de tout recommencer, sera-ce différent!";

    public string str1Italy = "Se c'è la possibilità di rifare tutto da capo, sarà diverso!";


}
