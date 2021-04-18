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




    static public Dictionary<string, string> atk_1 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_001" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" } ,{"atkDistance","4" } };
    //攻击相应数值
    public static Dictionary<string, float> atk_1_v = new Dictionary<string, float> { { "atkPower", 10 }, { "_xdx", -1f }, { "_xdy", 0f }, { "_scaleW", 2f}, { "_scaleH", 1.8f }, { "_disTime", 1 } };
    static public Dictionary<string, string> atk_2 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "10" }, { "yF", "1" }, { "showTXFrame", "9" }, { "txName", "dg_002" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    
    static public Dictionary<string, string> atk_3 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "13" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_003" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "1" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_4 = new Dictionary<string, string> { { "atkName", "atk_4" }, { "xF", "9" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_004" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "25" }, { "atkDistance", "4" } };
    //稻草人精英怪 横砍 招式
    static public Dictionary<string, string> atk_42 = new Dictionary<string, string> { { "atkName", "atk_5" }, { "xF", "9" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_002" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    //稻草人高个子
    static public Dictionary<string, string> atk_101 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_101" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "5" } };
    static public Dictionary<string, string> atk_102 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_102" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "5" } };
    static public Dictionary<string, string> atk_103 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "8" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_103" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "5.5" } };
    static public Dictionary<string, string> atk_104 = new Dictionary<string, string> { { "atkName", "atk_4" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_101" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "5" } };
    static public Dictionary<string, string> atk_105 = new Dictionary<string, string> { { "atkName", "atk_5" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_105" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "5" } };
    static public Dictionary<string, string> jumpAtk_1 = new Dictionary<string, string> { { "atkName", "jumpAtk_1" }, { "xF", "3" }, { "yF", "2" }, { "showTXFrame", "10" }, { "txName", "dg_001" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }};
    static public Dictionary<string, string> jumpAtk_2 = new Dictionary<string, string> { { "atkName", "jumpAtk_2" }, { "xF", "3" }, { "yF", "0" }, { "showTXFrame", "10" }, { "txName", "dg_002" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "5" }};
    static public Dictionary<string, string> jumpAtk_21 = new Dictionary<string, string> { { "atkName", "jumpAtk_21" }, { "xF", "3" }, { "yF", "0" }, { "showTXFrame", "10" }, { "txName", "dg_601" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "5" } };

    //小七
    static public Dictionary<string, string> atk_5001 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_5001" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "5" } };
    static public Dictionary<string, string> atk_5002 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_5002" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "5" } };
    static public Dictionary<string, string> atk_5003 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "8" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_5003" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "5.5" } };
    static public Dictionary<string, string> atk_5004 = new Dictionary<string, string> { { "atkName", "atk_4" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_5001" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "5" } };
    static public Dictionary<string, string> atk_5005 = new Dictionary<string, string> { { "atkName", "atk_5" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_5005" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "5" } };





    //精英大剑
    static public Dictionary<string, string> atk_107 = new Dictionary<string, string> { { "atkName", "atk_7" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_102" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "5" } };
    static public Dictionary<string, string> atk_108 = new Dictionary<string, string> { { "atkName", "atk_8" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_101" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "35" }, { "atkDistance", "5" } };


    //烟怪
    static public Dictionary<string, string> atk_201 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "3" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_201" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } ,{ "qishouYC","3"} };
    static public Dictionary<string, string> atk_202 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "3" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_202" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" }, { "qishouYC", "3" } };
    static public Dictionary<string, string> atk_203 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "3" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_203" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" }, { "qishouYC", "3" } };

    //不死重甲长枪兵
    static public Dictionary<string, string> atk_301 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_ci_301" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> atk_302 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "4" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_302" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> atk_303 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "12" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_ci_303" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "10" }, { "atkDistance", "8" } };

    //白面武士
    static public Dictionary<string, string> atk_401 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_401" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_402 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_402" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_403 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_403" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };
    //刺
    static public Dictionary<string, string> atk_404 = new Dictionary<string, string> { { "atkName", "atk_4" }, { "xF", "20" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_ci_401" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "10" }, { "atkDistance", "8" } };
    //横削 特效dg_404是横砍
    static public Dictionary<string, string> atk_405 = new Dictionary<string, string> { { "atkName", "atk_5" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_404" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "10" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_406 = new Dictionary<string, string> { { "atkName", "atk_6" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_405" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "10" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_407 = new Dictionary<string, string> { { "atkName", "atk_7" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_403" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };
    //幽灵刀
    static public Dictionary<string, string> atk_501 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_501" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_502 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_502" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };


    //精英长枪怪
    static public Dictionary<string, string> atk_701 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_701" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };



    static public Dictionary<string, string> atk_21 = new Dictionary<string, string> { { "atkName", "atk_21" }, { "xF", "4" }, { "yF", "1" }, { "showTXFrame", "9" }, { "txName", "dg_601" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> atk_22 = new Dictionary<string, string> { { "atkName", "atk_22" }, { "xF", "6" }, { "yF", "1" }, { "showTXFrame", "9" }, { "txName", "dg_001" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> atk_23 = new Dictionary<string, string> { { "atkName", "atk_23" }, { "xF", "4" }, { "yF", "1" }, { "showTXFrame", "9" }, { "txName", "dg_ci_401" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> atk_24 = new Dictionary<string, string> { { "atkName", "atk_24" }, { "xF", "8" }, { "yF", "1" }, { "showTXFrame", "9" }, { "txName", "dg_004" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" }, { "chongjili", "800" } };
    static public Dictionary<string, string> atk_25 = new Dictionary<string, string> { { "atkName", "atk_23" }, { "xF", "16" }, { "yF", "1" }, { "showTXFrame", "9" }, { "txName", "dg_ci_401" }, { "ox", "14" }, { "oy", "6" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    //记得23 改25
    static public Dictionary<string, string> atk_31 = new Dictionary<string, string> { { "atkName", "atk_31" }, { "xF", "0" }, { "yF", "1" }, { "showTXFrame", "9" }, { "txName", "dg_602" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> jumpAtk_31 = new Dictionary<string, string> { { "atkName", "jumpAtk_31" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "9" }, { "txName", "dg_602" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "1" }, { "atkDistance", "6" } };
    static public Dictionary<string, string> jumpAtk_32 = new Dictionary<string, string> { { "atkName", "jumpAtk_32" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "9" }, { "txName", "dg_603" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "18" }, { "atkDistance", "6" },{ "fantan","5"} };

    static public Dictionary<string, string>[] atkZS = new Dictionary<string, string>[] {atk_21,atk_22,atk_25,atk_24};
    static public Dictionary<string, string>[] atkUpZS = new Dictionary<string, string>[] { atk_31};
    static public Dictionary<string, string>[] jumpAtkZS = new Dictionary<string, string>[] {jumpAtk_21,jumpAtk_21};
    static public Dictionary<string, string>[] jumpAtkDownZS = new Dictionary<string, string>[] { jumpAtk_32};
    static public Dictionary<string, string>[] jumpAtkUpZS = new Dictionary<string, string>[] { jumpAtk_31};
    static public Dictionary<string, string> jn_ci_1 = new Dictionary<string, string> { { "atkName", "atk_4" },{ "skillBeginEffect", ""} ,{ "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "dg_ci_1" }, { "ox", "10" }, { "oy", "10" },{ "qishouYC","15" }, { "yanchi", "15" }, { "atkDistance", "18" }, { "chongjili", "1800" } };
    static public Dictionary<string, string> jn_ci_2 = new Dictionary<string, string> { { "atkName", "atk_6" }, { "skillBeginEffect", "" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "jn_cizu" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "20" } };
    static public Dictionary<string, string> jn_chongjijian_2 = new Dictionary<string, string> { { "atkName", "atk_5" }, { "skillBeginEffect", "" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "jn_chongjijian_2" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "20" } };



    //boss 花妖
    static public Dictionary<string, string> atk_801 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "0" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_801" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "14" }, { "AudioName", "AudioAtk_2" } };
    static public Dictionary<string, string> atk_802 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "0" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_801" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "14" } };
    static public Dictionary<string, string> atk_803 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "0" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_803" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "14" } };
    static public Dictionary<string, string> atk_804 = new Dictionary<string, string> { { "atkName", "atk_4" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "JN_pensandan" }, { "ox", "0" }, { "oy", "0" }, { "yanchi", "15" }, { "atkDistance", "24" },{ "AudioName","AudioAtk_1"} };
    static public Dictionary<string, string> atk_807 = new Dictionary<string, string> { { "atkName", "atk_4" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "JN_pensandanGZ" }, { "ox", "0" }, { "oy", "0" }, { "yanchi", "15" }, { "atkDistance", "24" }, { "AudioName", "AudioAtk_1" } };
    static public Dictionary<string, string> atk_806 = new Dictionary<string, string> { { "atkName", "atk_4" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "JN_pensandan2" }, { "ox", "0" }, { "oy", "0" }, { "yanchi", "15" }, { "atkDistance", "24" }, { "AudioName", "AudioAtk_1" } };
    static public Dictionary<string, string> atk_805 = new Dictionary<string, string> { { "atkName", "atk_4" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "JN_pensandan_zxm" }, { "ox", "0" }, { "oy", "0" }, { "yanchi", "15" }, { "atkDistance", "24" }, { "AudioName", "AudioAtk_1" } };


    //boss 斗笠武士 
    static public Dictionary<string, string> atk_901 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_901" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_902 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_902" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };
    static public Dictionary<string, string> atk_903 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "5" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_903" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "4" } };


    //幽灵boss
    static public Dictionary<string, string> atk_1001 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "1" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "dg_1001" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "6" },{"qishouYC","0" } };
    //这个是没有 起手动作的 快速砍击
    static public Dictionary<string, string> atk_1001s = new Dictionary<string, string> { { "atkName", "atk_11" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_1001" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "6" }, { "qishouYC", "0.3" } };
    static public Dictionary<string, string> atk_1002 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "1" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "dg_1002" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "25" }, { "atkDistance", "6" }, { "qishouYC", "0.3" } };
    static public Dictionary<string, string> atk_1004 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "7" }, { "yF", "1" }, { "showTXFrame", "8" }, { "txName", "dg_1001" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" }, { "atkDistance", "6" }, { "qishouYC", "0.3" } };

    //电球恶魔boss
    static public Dictionary<string, string> atk_1104 = new Dictionary<string, string> { { "atkName", "skill_genzong1" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "TX_dianqiu" }, { "ox", "0" }, { "oy", "0" }, { "yanchi", "15" }, { "atkDistance", "30" }, { "atkDistanceY", "4" } };
    static public Dictionary<string, string> atk_1105 = new Dictionary<string, string> { { "atkName", "skill_juji" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "TX_jujiqiu" }, { "ox", "0" }, { "oy", "0" }, { "yanchi", "15" }, { "atkDistance", "50" } };


    //重甲精英斧头怪
    static public Dictionary<string, string> atk_1201 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "dg_1011" }, { "ox", "0" }, { "oy", "0" }, { "yanchi", "35" }, { "atkDistance", "6" }, { "atkDistanceY", "0" } ,{ "chongjili","3000"} ,{"hitKuaiOX","1.5"},{"hitKuaiOY","0" },{"hitKuaiSX","5"},{"hitKuaiSY","0"},{"atkPower","400"},{"TXSX","0"},{"TXOX","-1.4" },{"TXOY","0" } };
    static public Dictionary<string, string> atk_1202 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "dg_1011" }, { "ox", "0" }, { "oy", "0" }, { "yanchi", "35" }, { "atkDistance", "6" }, { "atkDistanceY", "0" }, { "chongjili", "3000" }, { "hitKuaiOX", "1.5" }, { "hitKuaiOY", "0" }, { "hitKuaiSX", "5" }, { "hitKuaiSY", "0" }, { "atkPower", "400" }, { "TXSX", "0" }, { "TXOX", "-1.4" }, { "TXOY", "0" } };
    static public Dictionary<string, string> atk_1203 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "0" }, { "yF", "0" }, { "showTXFrame", "8" }, { "txName", "dg_1011" }, { "ox", "0" }, { "oy", "0" }, { "yanchi", "15" }, { "atkDistance", "8" }, { "atkDistanceY", "0" }, { "chongjili", "500" } };

    //public string Tt
    //{
    //    get { return Tt; }
    //    set { Tt = value; }
    //}

    public string Qw = "qw";

    // Use this for initialization
    // Use this for initialization
    void Start () {
        //Tt = "100";
       //print(this["atk_1_v"]);
	}

    static DataZS instance;
    static public DataZS GetInstance() {
        if (instance == null) {
            GameObject go = new GameObject("DataZS");
            //DontDestroyOnLoad(go);
            instance = go.AddComponent<DataZS>();


          
        } //instance = new DataZS();
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
