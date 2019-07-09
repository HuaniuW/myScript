using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_Date : MonoBehaviour {
    [Header("攻击力")]
    public float atkPower;
    [Header("相对位置x")]
    public float _xdx = 0;
    [Header("相对位置y")]
    public float _xdy = 0;

    [Header("hitKuai相对位置x")]
    public float hitKuai_xdx = 0;
    [Header("hitKuai相对位置y")]
    public float hitKuai_xdy = 0;


    [Header("宽")]
    public float _scaleW = 1;
    [Header("普通攻击碰撞快的宽的缩放")]
    public float hitKuaiSW = 1;
    [Header("普通攻击碰撞快的高的缩放")]
    public float hitKuaiSH = 1;
    //[Header("高")]
    //public float _scaleH = 1;
    [Header("特效消失时间为下一帧消失")]
    public float TXDisTime = 0.5f;
    [Header("消失时间 -1 为下一帧消失")]
    public float _disTime = -1;
    [Header("队伍")]
    public float team;
    [Header("X移动速度 为0则不移动")]
    public float moveXSpeed = 0;
    [Header("Y移动速度 为0则不移动")]
    public float moveYSpeed = 0;
    [Header("sceleX修正 有时候资源正反不一")]
    public int xzScaleX = 1;
    [Header("特效类型")]
    public string _type = "1";
    [Header("技能动作")]
    public string _skillAC = "";

    [Header("是否延迟")]
    public bool isYanchi = false;
    [Header("延迟时间")]
    public int yanchiNum = 0;
    [Header("变招计数尺度")]
    public int bianzhaochidu = 0;
    [Header("技能咏唱时间")]
    public float yongchangshijian = 0;
    [Header("冲击力 击退力量")]
    public float chongjili = 0;
    [Header("击中硬直时间")]
    public float yingzhishijian = 0.1f;


    //public Dictionary<string, float> atk_date = new Dictionary<string, float> { { "atkPower", 100 },{ "_xdx", -1.5f }, { "_xdy", 0f },{ "_scaleW", 2f }, { "_scaleH", 1.8f }, { "_disTime", 1 }};
    // Use this for initialization
    void Start () {
        //this.transform.localScale = new Vector3(xzScaleX, transform.localScale.y, transform.localScale.z);
	}

    public void SetDateVO(Dictionary<string,string> dict) {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
