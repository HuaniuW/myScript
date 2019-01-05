using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DataZS : MonoBehaviour
{

    //攻击动作 x移动力量 y移动力量  特效  特效相对于角色位置x 特效相对于角色位置y 


    //public Dictionary<string, string> atkZS = new Dictionary<string, string>[{"atk_1":"??"},{"atk_2":"o"}];

    //Object[] atkZS = new Object[] {10,100,10,10,100,100};

    //名字 x移动力量 y移动力量 特效出现帧 特效名字 特效相对x位置 特效相对y位置 攻击完后延迟帧数 




    static public Dictionary<string, string> atk_1 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "300" }, { "yF", "100" }, { "showTXFrame", "8" }, { "txName", "dg_001" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" } ,{"atkDistance","4.3" } };
    //攻击相应数值
    public static Dictionary<string, float> atk_1_v = new Dictionary<string, float> { { "atkPower", 10 }, { "_xdx", -1f }, { "_xdy", 0f }, { "_scaleW", 2f}, { "_scaleH", 1.8f }, { "_disTime", 1 } };
    static public Dictionary<string, string> atk_2 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "300" }, { "yF", "100" }, { "showTXFrame", "9" }, { "txName", "dg_002" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "4.3" } };
    static public Dictionary<string, string> atk_3 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "300" }, { "yF", "100" }, { "showTXFrame", "8" }, { "txName", "dg_003" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "20" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_4 = new Dictionary<string, string> { { "atkName", "atk_4" }, { "xF", "400" }, { "yF", "100" }, { "showTXFrame", "8" }, { "txName", "dg_004" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "25" }, { "atkDistance", "5" } };
    static public Dictionary<string, string> jumpAtk_1 = new Dictionary<string, string> { { "atkName", "jumpAtk_1" }, { "xF", "100" }, { "yF", "200" }, { "showTXFrame", "10" }, { "txName", "dg_001" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }};
    static public Dictionary<string, string> jumpAtk_2 = new Dictionary<string, string> { { "atkName", "jumpAtk_2" }, { "xF", "100" }, { "yF", "200" }, { "showTXFrame", "10" }, { "txName", "dg_002" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "5" }};

    static public Dictionary<string, string>[] atkZS = new Dictionary<string, string>[] {atk_1,atk_2,atk_3,atk_4};
    static public Dictionary<string, string>[] jumpAtkZS = new Dictionary<string, string>[] {jumpAtk_1,jumpAtk_2,atk_4};

    public string Tt
    {
        get { return Tt; }
        set { Tt = value; }
    }

    public string Qw = "qw";

    // Use this for initialization
    // Use this for initialization
    void Start () {
        Tt = "100";
       //print(this["atk_1_v"]);
	}

    static DataZS instance;
    static public DataZS GetInstance() {
        if (instance == null) instance = new DataZS();
        return instance;
    }


    static public string GetValueByName(string _name) {
        //print(DataZS.GetInstance()["tt"]);
        return DataZS.GetInstance()["tt"] as string;        
    }

    public object this[string propertyName]
    {
        get
        {
            // probably faster without reflection:
            // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
            // instead of the following
            //print("name "+propertyName);
            Type myType = typeof(DataZS);
            //print(myType);
            PropertyInfo myPropInfo = myType.GetProperty(propertyName);
            //print("p> "+myPropInfo);
            //print(" propertyName     " + GetType().GetProperty(propertyName));
            return null;//this.GetType().GetProperty(propertyName).GetValue(this, null);
        }
        set
        {
            Type myType = typeof(DataZS);
            PropertyInfo myPropInfo = myType.GetProperty(propertyName);
            myPropInfo.SetValue(this, value, null);

        }

    }

    public void GetTest() {
       // print(""+this["atk_1_v"]);
    }
    


    // Update is called once per frame
    void Update () {
		
	}
}
