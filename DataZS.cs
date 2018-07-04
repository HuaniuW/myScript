using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataZS : MonoBehaviour {

    //攻击动作 x移动力量 y移动力量  特效  特效相对于角色位置x 特效相对于橘色位置y 


    //public Dictionary<string, string> atkZS = new Dictionary<string, string>[{"atk_1":"??"},{"atk_2":"o"}];

    //Object[] atkZS = new Object[] {10,100,10,10,100,100};

        //名字 x移动力量 y移动力量 特效出现帧 特效名字 特效相对x位置 特效相对y位置 攻击完后延迟帧数 
   
    


    static public Dictionary<string, string> atk_1 = new Dictionary<string, string> { { "atkName", "atk_1" }, { "xF", "300" }, { "yF", "100" }, { "showTXFrame", "8" }, { "txName", "tx_1" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" } };
    static public Dictionary<string, string> atk_2 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "300" }, { "yF", "100" }, { "showTXFrame", "9" }, { "txName", "tx_2" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "20" } };
    static public Dictionary<string, string> atk_3 = new Dictionary<string, string> { { "atkName", "atk_3" }, { "xF", "300" }, { "yF", "100" }, { "showTXFrame", "8" }, { "txName", "tx_3" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" } };
    static public Dictionary<string, string> jumpAtk_1 = new Dictionary<string, string> { { "atkName", "jumpAtk_1" }, { "xF", "100" }, { "yF", "200" }, { "showTXFrame", "10" }, { "txName", "tx_3" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "15" } };
    static public Dictionary<string, string> jumpAtk_2 = new Dictionary<string, string> { { "atkName", "jumpAtk_2" }, { "xF", "100" }, { "yF", "200" }, { "showTXFrame", "10" }, { "txName", "tx_4" }, { "ox", "10" }, { "oy", "10" }, { "yanchi", "5" } };

    static public Dictionary<string, string>[] atkZS = new Dictionary<string, string>[] { atk_1, atk_2, atk_3 };
    static public Dictionary<string, string>[] jumpAtkZS = new Dictionary<string, string>[] { jumpAtk_1, jumpAtk_2};

    // Use this for initialization
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
