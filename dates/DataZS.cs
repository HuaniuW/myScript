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




    static public Dictionary<string, string> atk_1 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_001" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" } ,{"atkDistance","6" } };
    //攻击相应数值
    public static Dictionary<string, float> atk_1_v = new Dictionary<string, float> { { "atkPower", 10 }, { "_xdx", -1f }, { "_xdy", 0f }, { "_scaleW", 2f}, { "_scaleH", 1.8f }, { "_disTime", 1 } };
    static public Dictionary<string, string> atk_2 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "9" }, { "txName", "dg_002" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    
    static public Dictionary<string, string> atk_3 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_003" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> atk_4 = new Dictionary<string, string> { { "atkName", "atk_4" }, { "xF", "9" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_004" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "25" }, { "atkDistance", "7" } };
    //稻草人高个子
    static public Dictionary<string, string> atk_101 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_101" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "8" } };
    static public Dictionary<string, string> atk_102 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_102" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "8" } };
    static public Dictionary<string, string> atk_103 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "8" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_103" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "8.5" } };
    static public Dictionary<string, string> atk_104 = new Dictionary<string, string> { { "atkName", "atk_4" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_101" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "8" } };
    static public Dictionary<string, string> atk_105 = new Dictionary<string, string> { { "atkName", "atk_5" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_105" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "8" } };
    static public Dictionary<string, string> jumpAtk_1 = new Dictionary<string, string> { { "atkName", "jumpAtk_1" }, { "xF", "3" }, { "yF", "2" }, { "showTXFrame", "10" }, { "txName", "dg_001" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }};
    static public Dictionary<string, string> jumpAtk_2 = new Dictionary<string, string> { { "atkName", "jumpAtk_2" }, { "xF", "3" }, { "yF", "0" }, { "showTXFrame", "10" }, { "txName", "dg_002" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "5" }};
    static public Dictionary<string, string> jumpAtk_21 = new Dictionary<string, string> { { "atkName", "jumpAtk_21" }, { "xF", "3" }, { "yF", "0" }, { "showTXFrame", "10" }, { "txName", "dg_002" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "5" } };
    //烟怪
    static public Dictionary<string, string> atk_201 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "3" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_201" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "8" } };
    static public Dictionary<string, string> atk_202 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "3" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_202" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "8" } };
    static public Dictionary<string, string> atk_203 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "3" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_203" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "8" } };

    //不死重甲长枪兵
    static public Dictionary<string, string> atk_301 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_ci_301" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> atk_302 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "4" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_302" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> atk_303 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "12" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_ci_303" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "10" }, { "atkDistance", "8" } };

    //白面武士
    static public Dictionary<string, string> atk_401 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_401" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_402 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_402" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_403 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_403" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };
    //刺
    static public Dictionary<string, string> atk_404 = new Dictionary<string, string> { { "atkName", "atk_4" }, { "xF", "12" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_ci_401" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "10" }, { "atkDistance", "8" } };
    //横削
    static public Dictionary<string, string> atk_405 = new Dictionary<string, string> { { "atkName", "atk_5" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_404" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "10" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_406 = new Dictionary<string, string> { { "atkName", "atk_6" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_405" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "10" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_407 = new Dictionary<string, string> { { "atkName", "atk_7" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_403" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };
    //幽灵刀
    static public Dictionary<string, string> atk_501 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_501" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_502 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_502" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };


    static public Dictionary<string, string> atk_21 = new Dictionary<string, string> { { "atkName", "atk_21" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "9" }, { "txName", "dg_601" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> atk_22 = new Dictionary<string, string> { { "atkName", "atk_22" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "9" }, { "txName", "dg_001" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> atk_23 = new Dictionary<string, string> { { "atkName", "atk_23" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "9" }, { "txName", "dg_ci_401" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> atk_24 = new Dictionary<string, string> { { "atkName", "atk_24" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "9" }, { "txName", "dg_004" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" } };

    static public Dictionary<string, string> atk_31 = new Dictionary<string, string> { { "atkName", "atk_31" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "9" }, { "txName", "dg_602" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> jumpAtk_31 = new Dictionary<string, string> { { "atkName", "jumpAtk_31" }, { "xF", "7" }, { "yF", "0" }, { "showTXFrame", "9" }, { "txName", "dg_602" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> jumpAtk_32 = new Dictionary<string, string> { { "atkName", "jumpAtk_32" }, { "xF", "7" }, { "yF", "0" }, { "showTXFrame", "9" }, { "txName", "dg_603" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" },{ "fantan","5"} };

    static public Dictionary<string, string>[] atkZS = new Dictionary<string, string>[] {atk_21,atk_22,atk_24};
    static public Dictionary<string, string>[] atkUpZS = new Dictionary<string, string>[] { atk_31};
    static public Dictionary<string, string>[] jumpAtkZS = new Dictionary<string, string>[] {jumpAtk_21,jumpAtk_21};
    static public Dictionary<string, string>[] jumpAtkDownZS = new Dictionary<string, string>[] { jumpAtk_32};
    static public Dictionary<string, string>[] jumpAtkUpZS = new Dictionary<string, string>[] { jumpAtk_31};
    static public Dictionary<string, string> jn_ci_1 = new Dictionary<string, string> { { "atkName", "atk_4" },{ "skillBeginEffect", "tt"} ,{ "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "dg_ci_1" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "10" } };
    static public Dictionary<string, string> jn_ci_2 = new Dictionary<string, string> { { "atkName", "atk_6" }, { "skillBeginEffect", "tt" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "jn_cizu" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "20" } };
    static public Dictionary<string, string> jn_chongjijian_2 = new Dictionary<string, string> { { "atkName", "atk_5" }, { "skillBeginEffect", "tt" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "jn_chongjijian_2" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "20" } };

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
