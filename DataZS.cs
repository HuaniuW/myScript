using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataZS : MonoBehaviour {

    //攻击动作 x移动力量 y移动力量  特效  特效相对于角色位置x 特效相对于橘色位置y 


    //public Dictionary<string, string> atkZS = new Dictionary<string, string>[{"atk_1":"??"},{"atk_2":"o"}];

    //Object[] atkZS = new Object[] {10,100,10,10,100,100};

    static public string[] atk_1 = { "atk_1", "100", "100" ,"tx_1","10","10"};
    static public string[] atk_2 = { "atk_2", "100", "100", "tx_2", "10", "10" };
    static public string[] atk_3 = { "atk_3", "100", "100", "tx_3", "10", "10" };
    static public string[][] atkZS = new string[][]{ atk_1, atk_2, atk_3 };
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
