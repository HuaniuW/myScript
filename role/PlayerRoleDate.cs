using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoleDate : RoleDate
{
    float _atk;
    float _def;
    float _theMaxLive;
    float _yingzhi;
    float _live;
    float _lan;
    float _shanghaijianmianLv;
    

    public override float lan
    {
        get
        {
            return Lan;
        }
        set
        {
            Lan = value;
            if (Lan < 0) Lan = 0;
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_HUN, Lan), this);
        }
    }

    bool IsStart = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!IsStart) GetStart();
        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
    }


    public void GetStart()
    {
        print("角色 数据date类*********************************************************************************************************************");
        InitBaseDate();
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_HZ, changeHZ);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GET_DIAOLUOWU, this.GetDiaoLuo);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.JIAXUE, this.JiaXue);
        IsStart = true;
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANGE_HZ, changeHZ);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GET_DIAOLUOWU, this.GetDiaoLuo);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.JIAXUE, this.JiaXue);
        //ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
    }


    //void RemoveSelf(UEvent e)
    //{
    //    DestroyImmediate(this, true);
    //}



    void JiaXue(UEvent e)
    {
        this.live += 300;
        if (this.live > maxLive) live = maxLive;
        if(IsHasZZ) if (live > maxLive * 0.3f) live = maxLive * 0.3f;
        GetTX("jiaxue");
    }


    GameObject tx;
    void GetTX(string txName)
    {
        tx = null;
        tx = GlobalTools.GetGameObjectByName(txName);
        Vector2 v =  this.GetComponent<GameBody>().groundCheck.position;
        //tx.transform.parent = this.transform;
        //print(this.transform.localScale);

        tx.transform.position = new Vector2(v.x, v.y - 0.2f);
        if (tx.GetComponent<JN_TXgensui>())
        {
            tx.GetComponent<JN_TXgensui>().GetGenSuiObj(this.gameObject);
        }
        

        //if (this.transform.localScale.x == -1)
        //{
        //    tx.transform.position = new Vector2(v.x, v.y - 0.6f);
        //}
        //else {
        //    tx.transform.position = new Vector2(v.x, v.y);
        //}
        ;//new Vector2(0, -3);
        //print("tx.transform.position     "+ tx.transform.position);
        

        //tx.transform.position = new Vector2(0,-3);
        //print("position  "+tx.transform.position);
        StartCoroutine(IEDestory2ByTime(tx, 0.8f));
    }

    public IEnumerator IEDestory2ByTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        DestroyImmediate(obj, true);
    }



    //是否有诅咒
    public bool IsHasZZ = false;

    void GetDiaoLuo(UEvent e)
    {
        if (isDie) return;
        //print("------------------------------->>    "+e.eventParams.ToString());
        if(e.eventParams.ToString() == "XuehunXiao")
        {
            this.live += 100;
            GetTX("jiaxue");
        }
        else if (e.eventParams.ToString() == "XuehunDa")
        {
            this.live += 500;
            GetTX("jiaxue");
        }
        else if (e.eventParams.ToString() == "LanhunXiao")
        {
            this.lan += 100;
            GetTX("jialan");
        }
        else if (e.eventParams.ToString() == "LanhunDa")
        {
            this.lan += 500;
            GetTX("jialan");
        }else if (e.eventParams.ToString() == "C_cundangdian")
        {
            print("存档点加血");
            this.live += 5000;
            GetTX("jiaxue");
        }
        if (!IsHasZZ)
        {
            if (live > maxLive) live = maxLive;
        }
        else {
            //print(live+"  max  "+ maxLive);
            if (live > maxLive*0.3f) live = maxLive*0.3f;
            //print(live + "  max2222  " + maxLive);
        }
        
        if (lan > maxLan) lan = maxLan;
    }



    void InitBaseDate()
    {
        _atk = this.atk;
        _def = this.def;
        _theMaxLive = this.maxLive;
        _live = this.live;
        _lan = this.lan;
        _yingzhi = this.yingzhi;
        _shanghaijianmianLv = this.shanghaijianmianLv;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetCSDate()
    {
        this.atk = this._atk;
        this.def = _def;
        maxLive = _theMaxLive;
        yingzhi = _yingzhi;
        shanghaijianmianLv = _shanghaijianmianLv;
    }

    void changeHZ(UEvent e)
    {
        List<RectTransform> hzDateList = (List<RectTransform>)e.eventParams;
        //还原初始数值
        GetCSDate();
        if (hzDateList.Count != 0)
        {
            beishuArr.Clear();
            foreach (var hz in hzDateList)
            {
                GetHZDate(hz.GetComponent<HZDate>());
            }
            //诅咒宝石属性计算
            GetHZZZ(hzDateList);
            GetHZBeishu();
        }
        else
        {
            //徽章组没有徽章 没有诅咒徽章 清掉锁血
            IsHasZZ = false;
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_ZUZHOU, false), this);
        }
        print("------------------------------------------------------------气血徽章- 事件发送！！！！！");
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANEG_LIVE,this.maxLive), this);
        GetPlayerMsg();
    }

    //获取玩家数据信息
    public string GetPlayerMsg()
    {
        string str =Globals.language == Globals.CHINESE ? "<color=#FDFEFE>角色信息</color>\n\n" : "<color=#FDFEFE>Player information</color>\n\n";
        string _liveStr = Globals.language == Globals.CHINESE ? "<color=#76D7C4>生命值：+" + this.live+"/"+this.maxLive + "</color>\n" : "<color=#76D7C4>live：+" + this.live + "/" + this.maxLive + "</color>\n";
        string _atkStr = Globals.language == Globals.CHINESE ? "<color=#E74C3C>攻击力：+" + this.atk + "</color>\n" : "<color=#E74C3C>atk：+" + this.atk + "</color>\n";
        string _defStr = Globals.language == Globals.CHINESE ? "<color=#5DADE2>防御力：+" + this.def + "</color>\n" : "<color=#5DADE2>def：+" + this.def + "</color>\n";
        string _yingzhiStr = Globals.language == Globals.CHINESE ? "<color=#5DADE2>硬直：+" + this.yingzhi + "</color>\n" : "<color=#5DADE2>yz：+" + this.yingzhi + "</color>\n";
        string _baojijilv = Globals.language == Globals.CHINESE ? "<color=#5D0DE2>暴击几率：+" + this.BaoJiLv + "</color>\n" : "<color=#5DADE2>yz：+" + this.BaoJiLv + "</color>\n";
        string _baojishanghaibeilv = Globals.language == Globals.CHINESE ? "<color=#0DADE2>暴击伤害倍数：+" + this.BaoJiShangHaiBeiLv + "</color>\n" : "<color=#5DADE2>yz：+" + this.BaoJiShangHaiBeiLv + "</color>\n";
        str += _liveStr + _atkStr + _defStr+ _yingzhiStr+_baojijilv+_baojishanghaibeilv;
        return str;
    }

    //徽章的倍数变化
    void GetHZBeishu()
    {
        if (beishuArr.Count != 0)
        {
            foreach (var s in beishuArr)
            {
                string s1 = s.Split('_')[0];
                float nums = float.Parse(s.Split('_')[1]);
                string _name = s.Split('_')[2];
                if (s1 == "atkP")
                {
                    this.atk *= nums;
                }
                else if (s1 == "defP")
                {
                    this.def *= nums;
                }
                else if (s1 == "liveP")
                {
                    if(_name!="诅咒宝石") this.maxLive *= nums;
                    if(IsHasZZ) if (live > maxLive * 0.3f) live = maxLive * 0.3f;
                }else if (s1 == "yingzhiP")
                {
                    this.yingzhi *= nums;
                    csYZ = yingzhi;
                }
            }
        }
        GetPlayerMsg();
    }

    List<string> beishuArr = new List<string>();

    //徽章的数据加成
    void GetHZDate(HZDate hzdate)
    {
        //print(hzdate.HZName);
        
        if (hzdate.def != 0) this.def += hzdate.def;
        if (hzdate.atk != 0) this.atk += hzdate.atk;
        if (hzdate.BaoJiLv != 0) this.BaoJiLv += hzdate.BaoJiLv;
        if (hzdate.BaoJiShangHaiBeiLv != 0) this.BaoJiShangHaiBeiLv += hzdate.BaoJiShangHaiBeiLv;
        if (hzdate.yingzhi != 0)
        {
            this.yingzhi += hzdate.yingzhi;
            csYZ = yingzhi;
        }
        //print(this.atk + "  --   " + hzdate.atk);
        if (hzdate.live != 0)
        {
            //this.live = _live + hzdate.live;
            this.maxLive += hzdate.live;
            //print("装备最大气血装备  "+ this.maxLive);
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ADD_MAX_LIVE,maxLive), this);
        }

        if (hzdate.defP != 0) beishuArr.Add("defP_"+hzdate.defP+"_"+hzdate.HZName);
        if (hzdate.atkP != 0) beishuArr.Add("atkP_" + hzdate.atkP + "_" + hzdate.HZName);
        if (hzdate.liveP != 0) {
            beishuArr.Add("liveP_" + hzdate.liveP + "_" + hzdate.HZName);
        }

        if (hzdate.yingzhiP!=0)
        {
            beishuArr.Add("yingzhiP_" + hzdate.yingzhiP + "_" + hzdate.HZName);
        }
       

        //伤害减免比例的 使用
        if (hzdate.ShanghaiJianmianBili != 0)
        {
            if (hzdate.ShanghaiJianmianBili > this.shanghaijianmianLv)
            {
                this.shanghaijianmianLv = hzdate.ShanghaiJianmianBili;
            }
        }
       
    }



    
    //徽章诅咒
    void GetHZZZ(List<RectTransform> t)
    {
        foreach (var hz in t)
        {
            if(hz.GetComponent<HZDate>().HZName == "诅咒宝石")
            {
                IsHasZZ = true;
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_ZUZHOU,true), this);
                return;
            }
        }
        IsHasZZ = false;
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_ZUZHOU, false), this);
    }
}
