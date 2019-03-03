using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HZDate : MonoBehaviour {

    // Use this for initialization
    public string id = "";
    [Header("名字")]
    public string HZName;
    [Header("出现特效的名字")]
    public string TXName;
    [Header("调用的名字")]
    public string objName;
    [Header("增加攻击力")]
    public float atk;
    [Header("增加攻击力比例")]
    public float atkP;
    [Header("增加防御力")]
    public float def;
    [Header("增加防御力比例")]
    public float defP;
    [Header("增加生命")]
    public float live;
    [Header("增加生命的比例")]
    public float liveP;
    [Header("附带技能")]
    public string skill;

    [Header("附带技能动作")]
    public string skillACName;
    [Header("技能动作起始延迟")]
    public float ACyanchi;

    [Header("抗火")]
    public float kanghuo;
    [Header("是被动技能还是主动技能")]
    public string type = "bd";
    [Header("技能CD")]
    public float cd = 0;

    [Header("消耗蓝")]
    public float xyLan = 0;

    void Start () {
        
    }

    public float _cd = 0;
    
    public bool IsCDOver()
    {
        if (_cd > 0)
        {
            _cd--;
            //print("cd  "+_cd);
            return false;
        }
        return true;
    }


    public void isHasCD()
    {

    }

    public void StartCD()
    {
        _cd = cd;
    }


	// Update is called once per frame
	void Update () {
        if(_cd>0)IsCDOver();
    }
}
