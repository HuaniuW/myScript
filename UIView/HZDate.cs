using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HZDate : MonoBehaviour {

    // Use this for initialization
    public string id = "";
    [Header("名字")]
    public string HZName;
    [Header("徽章装配特效的名字(用于徽章显示特效自动调用)")]
    public string HZZBTXName = "";
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

    [Header("增加蓝量")]
    public float addLan;


    [Header("附带技能")]
    public string skill;

    [Header("附带技能动作")]
    public string skillACName;


    [Header("技能动作的 喊声")]
    public string AudioName;

    [Header("附带技能动作  空中")]
    public string skillACNameInAir;
    [Header("空中释放技能 悬停时间")]
    public float AirXTTimes = 0.5f;
    [Header("技能动作起始延迟")]
    public float ACyanchi = 0;

    [Header("技能动作 结束延迟")]
    public float ACOveryanchi = 0;

    [Header("抗火")]
    public float kanghuo;
    [Header("是被动技能还是主动技能")]
    public string type = "bd";

    [Header("有UI的技能 **名字(显示UI的名字)")]
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

    [Header("临时提高的硬直")]
    public float TempAddYingZhi = 0;

    [Header("临时提高的硬直的持续时间")]
    public float TempAddYingZhiTimes = 1;


    [Header("伤害减免比例")]
    public float ShanghaiJianmianBili = 0;


    [Header(" >> 临时伤害减免比例（0-100）")]
    public float TempShanghaiJianmianBili = 0;
    [Header(" -- 临时伤害减免比例 持续时间")]
    public float tempJSTimes = 0;

    //增加硬直
    [Header("*增加硬直*")]
    public float yingzhi = 0;
    [Header("*增加硬直百分比增加*")]
    public float yingzhiP = 0;
    //临时增加硬直

    [Header("暴击率")]
    public float BaoJiLv = 0;

    [Header("暴击伤害倍数")]
    public float BaoJiShangHaiBeiLv = 0;



    [Header("抗毒几率")]
    public float KangDuJilv = 0;
    [Header("抗中毒伤害几率")]
    public float KangDuShanghaiJilv = 0;


    [Header("抗 *火点燃* 几率")]
    public float KangHuoJilv = 0;
    [Header("*火* 伤害抵抗率")]
    public float KangHuoShanghaiJilv = 0;


    [Header("抗 *电* 几率")]
    public float KangDianJilv = 0;
    [Header("*电* 麻痹抵抗率")]
    public float KangDianMabiJilv = 0;


    [Header("*技能 恢复血量**")]
    public float HuifuXue = 0;



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



    string hz_name = "徽章名字：";
    string hz_information = "徽章名字：";
    string hz_atk = "攻击力：";
    string hz_atkP = "攻击力倍数：";
    string hz_def = "防御力：";
    string hz_defP = "防御力倍数：";
    string hz_live = "生命值：";
    string hz_liveP = "生命值：";
    string hz_baojilv = "暴击率：";
    string hz_baojishanghaibeishu = "暴击伤害倍数：";


    string hz_kangdujilv = "抗毒几率：";
    string hz_kangdushanghaijilv = "毒伤害抵抗率：";

    string hz_kanghuojilv = "抗火几率：";
    string hz_kanghuoshanghaijilv = "火伤害抵抗率：";

    string hz_kangdianjilv = "抗电几率：";
    string hz_kangdianmabijilv = "抗电麻痹几率：";

    public string GetHZ_information_str()
    {
        //做一次 语言判断


        string str = "";
        string _name = Globals.language == Globals.CHINESE? "<color=#FDFEFE>"+ hz_name + this.HZName+"</color>\n": "<color=#FDFEFE>Badge name：" + this.HZName + "</color>\n";
        str += _name;




        string _atkStr = "";
        if (atk != 0)
        {
            _atkStr = Globals.language == Globals.CHINESE ? "<color=#E74C3C>"+ hz_atk + "+" + this.atk + "</color>\n" : "<color=#E74C3C>atk：+" + this.atk + "</color>\n";
        }
        str += _atkStr;

        string _atkPStr = "";
        if (atkP != 0)
        {
            _atkPStr = Globals.language == Globals.CHINESE ? "<color=#E74C3C>"+ hz_atkP + this.atkP + "</color>\n" : "<color=#E74C3C>atk P：" + this.atkP + "</color>\n";
        }
        str += _atkPStr;

        string _defStr = "";
        if (def != 0)
        {
            _defStr = Globals.language == Globals.CHINESE ? "<color=#5DADE2>"+ hz_def + "+" + this.def + "</color>\n" : "<color=#5DADE2>def：+" + this.def + "</color>\n";
        }
        str += _defStr;

        string _defPStr = "";
        if (defP != 0)
        {
            _defPStr = Globals.language == Globals.CHINESE ? "<color=#5DADE2>"+ hz_defP + this.defP + "</color>\n" : "<color=#5DADE2>def P：" + this.defP + "</color>\n";
        }
        str += _defPStr;

        string _liveStr = "";
        if (live != 0)
        {
            _liveStr = Globals.language == Globals.CHINESE ? "<color=#76D7C4>"+ hz_live + "+"+this.live + "</color>\n" : "<color=#76D7C4>live：+" + this.live + "</color>\n";
        }
        str += _liveStr;

        string _livePStr = "";
        if (liveP != 0)
        {
            _livePStr = Globals.language == Globals.CHINESE ? "<color=#76D7C4>"+ hz_liveP + this.liveP + "</color>\n" : "<color=#76D7C4>live P：" + this.liveP + "</color>\n";
        }
        str += _livePStr;


        string _baojilv = "";
        if (BaoJiLv != 0)
        {
            _baojilv = Globals.language == Globals.CHINESE ? "<color=#06D7C4>" + hz_baojilv + this.BaoJiLv + "</color>\n" : "<color=#76D7C4>live P：" + this.BaoJiLv + "</color>\n";
        }
        str += _baojilv;


        string _baojishanghaibeishu = "";
        if (BaoJiShangHaiBeiLv != 0)
        {
            _baojishanghaibeishu = Globals.language == Globals.CHINESE ? "<color=#76D704>" + hz_baojishanghaibeishu + this.BaoJiShangHaiBeiLv + "</color>\n" : "<color=#76D7C4>live P：" + this.BaoJiShangHaiBeiLv + "</color>\n";
        }
        str += _baojishanghaibeishu;




        string _kanghuojilv = "";
        if (KangHuoJilv!=0)
        {
            _kanghuojilv = Globals.language == Globals.CHINESE ? "<color=#BBFFFF>" + hz_kanghuojilv + this.KangHuoJilv + "</color>\n" : "<color=#BBFFFF>live P：" + this.KangHuoJilv + "</color>\n";
        }
        str += _kanghuojilv;

        string _kanghuoshanghaijilv = "";
        if (KangHuoShanghaiJilv != 0)
        {
            _kanghuoshanghaijilv = Globals.language == Globals.CHINESE ? "<color=#BBFFFF>" + this.hz_kanghuoshanghaijilv + this.KangHuoShanghaiJilv + "</color>\n" : "<color=#BBFFFF>live P：" + this.KangHuoShanghaiJilv + "</color>\n";
        }
        str += _kanghuoshanghaijilv;






        string _kangdujilv = "";
        if (this.KangDuJilv != 0)
        {
            _kangdujilv = Globals.language == Globals.CHINESE ? "<color=#8EE5EE>" + this.hz_kangdujilv + this.KangDuJilv + "</color>\n" : "<color=#8EE5EE>live P：" + this.KangDuJilv + "</color>\n";
        }
        str += _kangdujilv;

        string _kangdushanghaijiv = "";
        if (this.KangDuShanghaiJilv != 0)
        {
            _kangdushanghaijiv = Globals.language == Globals.CHINESE ? "<color=#8EE5EE>" + this.hz_kangdushanghaijilv + this.KangDuShanghaiJilv + "</color>\n" : "<color=#8EE5EE>live P：" + this.KangDuShanghaiJilv + "</color>\n";
        }
        str += _kangdushanghaijiv;






        string _kangdianjilv = "";
        if (this.KangDianJilv != 0)
        {
            _kangdianjilv = Globals.language == Globals.CHINESE ? "<color=#00E5EE>" + this.hz_kangdianjilv + this.KangDianJilv + "</color>\n" : "<color=#00E5EE>live P：" + this.KangDianJilv + "</color>\n";
        }
        str += _kangdianjilv;


        string _kangdianmabijilv = "";
        if (this.KangDianMabiJilv != 0)
        {
            _kangdianmabijilv = Globals.language == Globals.CHINESE ? "<color=#00E5EE>" + this.hz_kangdianmabijilv + this.KangDianMabiJilv + "</color>\n" : "<color=#00E5EE>live P：" + this.KangDianMabiJilv + "</color>\n";
        }
        str += _kangdianmabijilv;




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
