using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_Date : MonoBehaviour {

    [Header("技能名字")]
    public string atkSkillName ="jineng";

    [Header("攻击力")]
    public float atkPower;
    [Header("相对位置x 注意这个值是负值就是 角色正面（当时角色都是朝左做的）")]
    public float _xdx = 0;
    [Header("相对位置y")]
    public float _xdy = 0;

    [Header("hitKuai相对位置x 越大越靠前")]
    public float hitKuai_xdx = 0;
    [Header("hitKuai相对位置y")]
    public float hitKuai_xdy = 0;

    [Header("是否是纵向位攻击，确定特效的朝向")]
    public bool isAtkZongXiang = false;

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
    [Header("特效类型 1.下一帧或者时间内消失 2.持续型 3.碰到消失 ")]
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

    [Header("反推力 一般用在 身体碰撞")]
    public float FanTuili = 0;


    [Header("击中硬直时间")]
    public float yingzhishijian = 0.1f;

    [Header("攻击方向 上下左右")]
    public string atkDirection = "";
    [Header("空中攻击的反推力")]
    public float fasntuili = 0;
    [Header("击中特效类型 5是撞击 7是毒")]
    public int HitInSpecialEffectsType = 1;

    [Header("击中特效偏移位置X")]
    public float HitInSpecialEffectsX = 0;
    [Header("击中特效偏移位置Y")]
    public float HitInSpecialEffectsY = 0;

    [Header("击中特效显示位置点")]
    public Vector2 TXPos = Vector2.zero;


    //ranshao_4_100  效果名字 持续时间 每秒伤害
    [Header("击中目标的 附带 效果 麻痹 燃烧 等")]
    public string JiZhongFDXiaoguo = "";


    [Header("非粒子特效 根据释放者改变方向")]
    public bool IsChanFXBYAtkObj = false;

    [Header("毒持续伤害")]
    public float DuChixuShanghai = 0;
    [Header("毒持续伤害时间")]
    public float DuChixuShanghaiTime = 0;

    [Header("火持续伤害")]
    public float HuoChixuShanghai = 0;
    [Header("火持续伤害时间")]
    public float HuoChixuShanghaiTime = 0;




    [Header("技能的 硬值 如果不等于0 优先用技能硬直")]
    public float JNYingzhi = 0;



    //public Dictionary<string, float> atk_date = new Dictionary<string, float> { { "atkPower", 100 },{ "_xdx", -1.5f }, { "_xdy", 0f },{ "_scaleW", 2f }, { "_scaleH", 1.8f }, { "_disTime", 1 }};
    // Use this for initialization
    void Start () {
        //this.transform.localScale = new Vector3(xzScaleX, transform.localScale.y, transform.localScale.z);
	}

    public void SetDateVO(Dictionary<string,string> dict) {
        
    }


    //public delegate void callback(float nums = 0);
    //callback _call;

    //public void GetCallBackStart()
    //{
    //    if (_call!=null) _call();
    //}

    //public void GetcallBackFuc(callback call)
    //{
    //    _call = call;
    //}



    // Update is called once per frame
    void Update () {
		
	}
}
