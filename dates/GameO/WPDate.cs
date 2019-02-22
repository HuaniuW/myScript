using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPDate : MonoBehaviour {
    public int id;
    public string _name;
    public string type;
    public string icon;
    public int level;
    public float addAtk;
    //倍数
    public float multipleAtk;
    public float addDef;
    public float multipleDef;
    //暴击
    [Header("暴击几率")]
    public float crit;
    public float addLive;
    public float multipleLive;
    //抗性
    [Header("抗火")]
    public float kanghuo;
    [Header("抗冰")]
    public float kangbing;
    [Header("抗电")]
    public float kangdian;
    [Header("抗毒")]
    public float kangdu;

    //描述
    public string description;


    //----------------------------------技能型
    public string skillName;//技能属性写在技能特效内

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
