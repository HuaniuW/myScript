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
    [Header("附带技能动作  空中")]
    public string skillACNameInAir;
    [Header("技能动作起始延迟")]
    public float ACyanchi;

    [Header("抗火")]
    public float kanghuo;
    [Header("是被动技能还是主动技能")]
    public string type = "bd";

    [Header("主动技能 名字")]
    public string zd_skill_ui_Name = "";
    [Header("技能CD")]
    public int cd = 0;
    [Header("技能可以连续使用次数")]
    public int usenums = 1;

    [Header("消耗蓝")]
    public float xyLan = 0;

    [Header("消耗血")]
    public float xyXue = 0;

    [Header("徽章介绍")]
    public string HZ_information = "";

    [Header("主动技能图片介绍")]
    public string imgName = "";

    public string RQName = "";

    [Header("被动防御技能-触发几率")]
    public float Chance_of_Passive_Skills = 0;

    [Header("防御效果")]
    public string def_effect = "";


    //增加硬直
    //临时增加硬直



    void Start () {
        
    }

    int _cd = 0;
    
    public bool IsCDOver()
    {
        if (_cd > 0)
        {
            //_cd--;
            //print("cd  "+_cd);
            return false;
        }
        return true;
    }


    public string GetHZ_information_str()
    {
        string str = "";
        string _name = Globals.language == Globals.CHINESE? "<color=#FDFEFE>徽章名字：" + this.HZName+"</color>\n": "<color=#FDFEFE>Badge name：" + this.HZName + "</color>\n";
        str += _name;
        string _atkStr = "";
        if (atk != 0)
        {
            _atkStr = Globals.language == Globals.CHINESE ? "<color=#E74C3C>攻击力：+" + this.atk + "</color>\n" : "<color=#E74C3C>atk：+" + this.atk + "</color>\n";
        }
        str += _atkStr;

        string _atkPStr = "";
        if (atkP != 0)
        {
            _atkPStr = Globals.language == Globals.CHINESE ? "<color=#E74C3C>攻击力倍数：" + this.atkP + "</color>\n" : "<color=#E74C3C>atk P：" + this.atkP + "</color>\n";
        }
        str += _atkPStr;

        string _defStr = "";
        if (def != 0)
        {
            _defStr = Globals.language == Globals.CHINESE ? "<color=#5DADE2>防御力：+" + this.def + "</color>\n" : "<color=#5DADE2>def：+" + this.def + "</color>\n";
        }
        str += _defStr;

        string _defPStr = "";
        if (defP != 0)
        {
            _defPStr = Globals.language == Globals.CHINESE ? "<color=#5DADE2>防御力倍数：" + this.defP + "</color>\n" : "<color=#5DADE2>def P：" + this.defP + "</color>\n";
        }
        str += _defPStr;

        string _liveStr = "";
        if (live != 0)
        {
            _liveStr = Globals.language == Globals.CHINESE ? "<color=#76D7C4>生命值：+" + this.live + "</color>\n" : "<color=#76D7C4>live：+" + this.live + "</color>\n";
        }
        str += _liveStr;

        string _livePStr = "";
        if (liveP != 0)
        {
            _livePStr = Globals.language == Globals.CHINESE ? "<color=#76D7C4>生命值倍数：" + this.liveP + "</color>\n" : "<color=#76D7C4>live P：" + this.liveP + "</color>\n";
        }
        str += _livePStr;


        string _information = "";
        if (HZ_information != "")
        {
            _information = Globals.language == Globals.CHINESE ? "\n<color=#CCD1D1>" + this.HZ_information + "</color>\n" : "<color=#CCD1D1>" + this.HZ_information + "</color>\n";
        }
        str += _information;

        return str;
    }


    public int GetCdNums()
    {
        return _cd;
    }

    public void StartCD()
    {
        _cd = cd;
        Jishi();
    }

    void Jishi()
    {
        if (_cd > 0) StartCoroutine(IETimeBySeconds());
    }

    public void OnDestroy()
    {
        StopCoroutine(IETimeBySeconds());
    }



    public IEnumerator IETimeBySeconds()
    {
        yield return new WaitForSeconds(1);
        if (_cd > 0) {
            _cd--;
            Jishi();
        }
    }


    // Update is called once per frame
    void Update () {
        //if(_cd>0)IsCDOver();
    }
}
