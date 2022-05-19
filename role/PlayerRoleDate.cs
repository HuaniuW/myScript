using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoleDate : RoleDate
{
    float _atk;
    float _def;
    float _theMaxLive;
    float _theMaxLan;
    float _yingzhi;
    float _live;
    float _lan;
    float _shanghaijianmianLv;

    float _kangDuJilv;

    float _kangDuShanghaijilv;

    float _kangMabiJilv;



    float _kangDianJilv;
    float _KangDianMabiJilv;


    float _kangHuoJilv;
    float _kangHuoShanghaiJilv;

    public override float lan
    {
        get
        {
            return Lan;
        }
        set
        {
            Lan = value;
            if (Lan > maxLan) Lan = maxLan;
            if (Lan < 0) Lan = 0;
            //print("蓝 量改变  "+Lan);
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_HUN, Lan), this);
        }
    }

    [Header("是否 初始化 血量等 基础信息")]
    public bool IsStart = false;
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


    protected GameObject tx;
    public void GetTX(string txName)
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
        //if(this.gameObject!=null && this.gameObject.activeSelf) print("this  player "+this.gameObject.name+"    "+ this.gameObject.activeSelf);
        //if(this.gameObject.activeInHierarchy)
        if (this.gameObject != null && this.gameObject.activeSelf) {
            if (this.gameObject.activeSelf) StartCoroutine(IEDestory2ByTime(tx, 0.8f));
        }
        
    }

    protected IEnumerator IEDestory2ByTime(GameObject obj, float time)
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
            this.lan += 5000;

            //存档点 加不加蓝 看设计
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
        print("    初始化  血量是多少  "+ _theMaxLive);
        _theMaxLan = this.maxLan;
        _live = this.live;
        _lan = this.lan;
        _yingzhi = this.yingzhi;
        _shanghaijianmianLv = this.shanghaijianmianLv;

        _kangDuJilv = this.KangDuJilv;
        _kangDuShanghaijilv = this.KangDuShanghaijilv;

        _kangDianJilv = this.KangDianJilv;
        _KangDianMabiJilv = this.KangDianMabiJilv;



        _kangHuoJilv = this.KangHuoJilv;
        _kangHuoShanghaiJilv = this.KangHuoShanghaijilv;

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
        maxLan = _theMaxLan;
        if (lan > maxLan) lan = maxLan;
        yingzhi = _yingzhi;
        shanghaijianmianLv = _shanghaijianmianLv;

        this.KangDuJilv = _kangDuJilv;
        this.KangDuShanghaijilv = _kangDuShanghaijilv;


        this.KangHuoJilv = _kangHuoJilv;
        this.KangHuoShanghaijilv = _kangHuoShanghaiJilv;

        this.KangDianJilv = _kangDianJilv;
        this.KangDianMabiJilv = _KangDianMabiJilv;
        this.BaoJiShangHaiBeiLv = 0;
        this.BaoJiLv = 0;
        //this.KangMabiJilv = _kangMabiJilv;

    }

    bool IsInScreen = false;

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
        print("------------------------------------------------------------气血徽章- 事件发送！！！！！"+ "  当前血量是多少????    " + live);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANEG_LIVE, this.maxLive+"_max"), this);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANEG_LAN, this.maxLan), this);
        GetPlayerMsg();
        //print("  当前血量是多少????    "+live);

        if (!IsInScreen)
        {
            IsInScreen = true;
            //切换场景时候 调用
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.XUETIAO_ZHIJIEGENSUI, null), this);
        }


      

        print("   ********* 徽章事件 roledate 血量   怎么进的游戏"+ GlobalSetDate.instance.HowToInGame);

    }


    string playerMsg = "角色信息";
    public string PLAYERMSG
    {
        get { return playerMsg; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    playerMsg = "角色信息";
                    break;
                case Globals.JAPAN:
                    playerMsg = "役割情報";
                    break;
                case Globals.ENGLISH:
                    playerMsg = "role information";
                    break;
                case Globals.Portugal:
                    playerMsg = "información del rol";
                    break;
                case Globals.KOREAN:
                    playerMsg = "역할 정보";
                    break;
                case Globals.German:
                    playerMsg = "Rolleninformationen";
                    break;
                case Globals.Italy:
                    playerMsg = "informazioni sul ruolo";
                    break;
                case Globals.French:
                    playerMsg = "informations sur le rôle";
                    break;
                case Globals.CHINESEF:
                    playerMsg = "角色信息";
                    break;
            }
        }
    }



    string liveZ = "生命值: ";
    public string LIVEZ
    {
        get { return liveZ; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    liveZ = "生命值: ";
                    break;
                case Globals.JAPAN:
                    liveZ = "生命価値: ";
                    break;
                case Globals.ENGLISH:
                    liveZ = "live: ";
                    break;
                case Globals.Portugal:
                    liveZ = "valor de vida: ";
                    break;
                case Globals.KOREAN:
                    liveZ = "삶의 가치: ";
                    break;
                case Globals.German:
                    liveZ = "Lebenswert: ";
                    break;
                case Globals.Italy:
                    liveZ = "valore della vita: ";
                    break;
                case Globals.French:
                    liveZ = "valeur de la vie: ";
                    break;
                case Globals.CHINESEF:
                    liveZ = "生命值: ";
                    break;
            }
        }
    }


    string lanZ = "蓝量: ";
    public string LANZ
    {
        get { return lanZ; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    lanZ = "蓝量: ";
                    break;
                case Globals.JAPAN:
                    lanZ = "エネルギー: ";
                    break;
                case Globals.ENGLISH:
                    lanZ = "energy: ";
                    break;
                case Globals.Portugal:
                    lanZ = "energía: ";
                    break;
                case Globals.KOREAN:
                    lanZ = "에너지: ";
                    break;
                case Globals.German:
                    lanZ = "energy: ";
                    break;
                case Globals.Italy:
                    lanZ = "energy: ";
                    break;
                case Globals.French:
                    lanZ = "energy: ";
                    break;
                case Globals.CHINESEF:
                    lanZ = "藍量: ";
                    break;
            }
        }
    }


    string atkZ = "攻击力: ";
    public string ATKZ
    {
        get { return atkZ; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    atkZ = "攻击力: ";
                    break;
                case Globals.JAPAN:
                    atkZ = "攻撃力: ";
                    break;
                case Globals.ENGLISH:
                    atkZ = "atk: ";
                    break;
                case Globals.Portugal:
                    atkZ = "atk: ";
                    break;
                case Globals.KOREAN:
                    atkZ = "공격력: ";
                    break;
                case Globals.German:
                    atkZ = "atk: ";
                    break;
                case Globals.Italy:
                    atkZ = "atk: ";
                    break;
                case Globals.French:
                    atkZ = "atk: ";
                    break;
                case Globals.CHINESEF:
                    atkZ = "攻撃力: ";
                    break;
            }
        }
    }




    string defZ = "防御力: ";
    public string DEFZ
    {
        get { return defZ; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    defZ = "防御力: ";
                    break;
                case Globals.JAPAN:
                    defZ = "防衛: ";
                    break;
                case Globals.ENGLISH:
                    defZ = "def: ";
                    break;
                case Globals.Portugal:
                    defZ = "Defensa: ";
                    break;
                case Globals.KOREAN:
                    defZ = "방어: ";
                    break;
                case Globals.German:
                    defZ = "def: ";
                    break;
                case Globals.Italy:
                    defZ = "def: ";
                    break;
                case Globals.French:
                    defZ = "def: ";
                    break;
                case Globals.CHINESEF:
                    defZ = "防衛力: ";
                    break;
            }
        }
    }


    string yzZ = "硬直: ";
    public string YZZ
    {
        get { return yzZ; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    yzZ = "硬直: ";
                    break;
                case Globals.JAPAN:
                    yzZ = "硬度: ";
                    break;
                case Globals.ENGLISH:
                    yzZ = "hardness: ";
                    break;
                case Globals.Portugal:
                    yzZ = "dureza: ";
                    break;
                case Globals.KOREAN:
                    yzZ = "경도: ";
                    break;
                case Globals.German:
                    yzZ = "hardness: ";
                    break;
                case Globals.Italy:
                    yzZ = "hardness: ";
                    break;
                case Globals.French:
                    yzZ = "hardness: ";
                    break;
                case Globals.CHINESEF:
                    yzZ = "硬度: ";
                    break;
            }
        }
    }


    string baojiZ = "暴击几率: ";
    public string BAOJIZ
    {
        get { return baojiZ; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    baojiZ = "暴击率: ";
                    break;
                case Globals.JAPAN:
                    baojiZ = "クリティカ: ";
                    break;
                case Globals.ENGLISH:
                    baojiZ = "crit: ";
                    break;
                case Globals.Portugal:
                    baojiZ = "crit: ";
                    break;
                case Globals.KOREAN:
                    baojiZ = "중요한 기회: ";
                    break;
                case Globals.German:
                    baojiZ = "crit: ";
                    break;
                case Globals.Italy:
                    baojiZ = "crit: ";
                    break;
                case Globals.French:
                    baojiZ = "crit: ";
                    break;
                case Globals.CHINESEF:
                    baojiZ = "暴击率: ";
                    break;
            }
        }
    }



    string baojiZBL = "暴击倍率: ";
    public string BAOJIZBL
    {
        get { return baojiZ; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    baojiZBL = "暴击倍率: ";
                    break;
                case Globals.JAPAN:
                    baojiZBL = "クリティカル乗数: ";
                    break;
                case Globals.ENGLISH:
                    baojiZBL = "Crit multiplier: ";
                    break;
                case Globals.Portugal:
                    baojiZBL = "Crit multiplier: ";
                    break;
                case Globals.KOREAN:
                    baojiZBL = "치명타 배율: ";
                    break;
                case Globals.German:
                    baojiZBL = "Crit multiplier: ";
                    break;
                case Globals.Italy:
                    baojiZBL = "Crit multiplier: ";
                    break;
                case Globals.French:
                    baojiZBL = "Crit multiplier: ";
                    break;
                case Globals.CHINESEF:
                    baojiZBL = "暴击倍率: ";
                    break;
            }
        }
    }



    string shanghaiJML = "伤害减免率: ";
    public string SHANGHAIJML
    {
        get { return shanghaiJML; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    shanghaiJML = "伤害减免率: ";
                    break;
                case Globals.JAPAN:
                    shanghaiJML = "ダメージ軽減率: ";
                    break;
                case Globals.ENGLISH:
                    shanghaiJML = "Damage Reduction Rate: ";
                    break;
                case Globals.Portugal:
                    shanghaiJML = "Tasa de reducción de daños: ";
                    break;
                case Globals.KOREAN:
                    shanghaiJML = "데미지 감소율: ";
                    break;
                case Globals.German:
                    shanghaiJML = "Schadensreduktionsrate: ";
                    break;
                case Globals.Italy:
                    shanghaiJML = "Tasso di riduzione del danno: ";
                    break;
                case Globals.French:
                    shanghaiJML = "Taux de réduction des dégâts: ";
                    break;
                case Globals.CHINESEF:
                    shanghaiJML = "傷害減免率: ";
                    break;
            }
        }
    }

    string kangduJL = "抗毒几率: ";
    public string KANGDUJL
    {
        get { return kangduJL; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    kangduJL = "抗毒几率: ";
                    break;
                case Globals.JAPAN:
                    kangduJL = "アンチウイルス: ";
                    break;
                case Globals.ENGLISH:
                    kangduJL = "Antivirus: ";
                    break;
                case Globals.Portugal:
                    kangduJL = "antivirus: ";
                    break;
                case Globals.KOREAN:
                    kangduJL = "바이러스 백신: ";
                    break;
                case Globals.German:
                    kangduJL = "Anti Gift Chance: ";
                    break;
                case Globals.Italy:
                    kangduJL = "Possibilità anti veleno: ";
                    break;
                case Globals.French:
                    kangduJL = "Chance anti poison: ";
                    break;
                case Globals.CHINESEF:
                    kangduJL = "抗毒機率: ";
                    break;
            }
        }
    }


    string kangduSHBL = "抗毒伤害比率: ";
    public string KANGDUSHBL
    {
        get { return kangduSHBL; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    kangduSHBL = "抗毒伤害比率: ";
                    break;
                case Globals.JAPAN:
                    kangduSHBL = "毒ダメージ: ";
                    break;
                case Globals.ENGLISH:
                    kangduSHBL = "Poison Damage: ";
                    break;
                case Globals.Portugal:
                    kangduSHBL = "Daño por veneno: ";
                    break;
                case Globals.KOREAN:
                    kangduSHBL = "독 피해: ";
                    break;
                case Globals.German:
                    kangduSHBL = "Giftschadensverhältnis: ";
                    break;
                case Globals.Italy:
                    kangduSHBL = "Rapporto danni da veleno: ";
                    break;
                case Globals.French:
                    kangduSHBL = "Taux de dégâts de poison: ";
                    break;
                case Globals.CHINESEF:
                    kangduSHBL = "抗毒傷害比率: ";
                    break;
            }
        }
    }

    string kanghuoJL = "抗火点燃几率: ";
    public string KANGHUJL
    {
        get { return kanghuoJL; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    kanghuoJL = "抗火点燃几率: ";
                    break;
                case Globals.JAPAN:
                    kanghuoJL = "難燃性: ";
                    break;
                case Globals.ENGLISH:
                    kanghuoJL = "fire resistant: ";
                    break;
                case Globals.Portugal:
                    kanghuoJL = "resistente al fuego: ";
                    break;
                case Globals.KOREAN:
                    kanghuoJL = "내화성: ";
                    break;
                case Globals.German:
                    kanghuoJL = "Feuer Beständigkeit: ";
                    break;
                case Globals.Italy:
                    kanghuoJL = "resistenza al fuoco: ";
                    break;
                case Globals.French:
                    kanghuoJL = "résistance au feu: ";
                    break;
                case Globals.CHINESEF:
                    kanghuoJL = "抗火點燃機率: ";
                    break;
            }
        }
    }

    string kanghuoSHBL = "抗火伤害比率: ";
    public string KANGHUSHBL
    {
        get { return kanghuoSHBL; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    kanghuoSHBL = "抗火伤害: ";
                    break;
                case Globals.JAPAN:
                    kanghuoSHBL = "火災によるダメージ: ";
                    break;
                case Globals.ENGLISH:
                    kanghuoSHBL = "Fire damage: ";
                    break;
                case Globals.Portugal:
                    kanghuoSHBL = "Daño por fuego: ";
                    break;
                case Globals.KOREAN:
                    kanghuoSHBL = "화재 피해: ";
                    break;
                case Globals.German:
                    kanghuoSHBL = "Feuer Schaden: ";
                    break;
                case Globals.Italy:
                    kanghuoSHBL = "Danni da fuoco: ";
                    break;
                case Globals.French:
                    kanghuoSHBL = "Dégâts du feu: ";
                    break;
                case Globals.CHINESEF:
                    kanghuoSHBL = "抗火傷害: ";
                    break;
            }
        }
    }

    string kangdianJL = "抗电几率: ";
    public string KANGDIANJL
    {
        get { return kangdianJL; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    kangdianJL = "抗电几率: ";
                    break;
                case Globals.JAPAN:
                    kangdianJL = "反電気: ";
                    break;
                case Globals.ENGLISH:
                    kangdianJL = "Anti electric: ";
                    break;
                case Globals.Portugal:
                    kangdianJL = "Anti eléctrico: ";
                    break;
                case Globals.KOREAN:
                    kangdianJL = "정전기 방지: ";
                    break;
                case Globals.German:
                    kangdianJL = "Anti elektrische Wahrscheinlichkeit: ";
                    break;
                case Globals.Italy:
                    kangdianJL = "Probabilità antielettrica: ";
                    break;
                case Globals.French:
                    kangdianJL = "Probabilité anti électrique: ";
                    break;
                case Globals.CHINESEF:
                    kangdianJL = "抗電機率: ";
                    break;
            }
        }
    }

    string kangdianMBJL = "抗电麻痹几率: ";
    public string KANGDIANMBJL
    {
        get { return kangdianMBJL; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    kangdianMBJL = "抗电伤害: ";
                    break;
                case Globals.JAPAN:
                    kangdianMBJL = "反電気的損傷: ";
                    break;
                case Globals.ENGLISH:
                    kangdianMBJL = "nti electrical damage: ";
                    break;
                case Globals.Portugal:
                    kangdianMBJL = "Daño anti eléctrico: ";
                    break;
                case Globals.KOREAN:
                    kangdianMBJL = "정전기 방지 손상: ";
                    break;
                case Globals.German:
                    kangdianMBJL = "Anti elektrische Schäden: ";
                    break;
                case Globals.Italy:
                    kangdianMBJL = "Danno antielettrico: ";
                    break;
                case Globals.French:
                    kangdianMBJL = "Dommages anti électriques: ";
                    break;
                case Globals.CHINESEF:
                    kangdianMBJL = "抗電傷害: ";
                    break;
            }
        }
    }

    //获取玩家数据信息
    public string GetPlayerMsg()
    {
        PLAYERMSG = "";
        string str ="<color=#FF3399>"+ PLAYERMSG + "</color>\n\n";

        LIVEZ = "";
        string _liveStr = "<color=#76D7C4>"+ LIVEZ + this.live+"/"+this.maxLive + "</color>\n" ;

        LANZ = "";
        string _lanStr = "<color=#5DADE2>"+ LANZ + this.lan + "/" + this.maxLan + "</color>\n";

        ATKZ = "";
        //print("显示 攻击力：     "+ATKZ);
        string _atkStr = "<color=#E74C3C>"+ ATKZ + this.atk + "</color>\n";
        //print("攻击力 字符  _atkStr    "+ _atkStr);

        DEFZ = "";
        string _defStr = "<color=#5DADE2>"+ DEFZ + this.def + "</color>\n";

        YZZ = "";
        string _yingzhiStr = "<color=#5DADE2>"+ YZZ + this.yingzhi + "</color>\n";

        BAOJIZ = "";
        string _baojijilv = "<color=#5D0DE2>"+ BAOJIZ + this.BaoJiLv + "</color>\n";

        BAOJIZBL = "";
        string _baojishanghaibeilv ="<color=#0DADE2>"+ BAOJIZBL + this.BaoJiShangHaiBeiLv + "</color>\n";

        SHANGHAIJML = "";
        string _shanghaijianmianlv= "<color=#E74C3C>"+ SHANGHAIJML + this.shanghaijianmianLv + "</color>\n";
        //抗毒几率
        KANGDUJL = "";
        string _kangdujilv = "<color=#E74C3C>"+ KANGDUJL + this.KangDuJilv + "</color>\n";

        KANGDUSHBL = "";
        string _kangduShanghaiJilv ="<color=#E00CfC>"+ KANGDUSHBL + this.KangDuShanghaijilv + "</color>\n";
        //抗火点燃几率
        KANGHUJL = "";
        
        string _kanghuojilv = "<color=#5DADE2>"+ KANGHUJL + this.KangHuoJilv + "</color>\n";
        //抗火伤害比率
        KANGHUSHBL = "";
        string _kanghuoshanghaijilv ="<color=#E00CfC>"+ KANGHUSHBL + this.KangHuoShanghaijilv + "</color>\n";
        //抗电几率
        KANGDIANJL = "";
        string _kangdianjilv = "<color=#5DADE2>"+ KANGDIANJL + this.KangDianJilv + "</color>\n";
        //抗电麻痹几率
        KANGDIANMBJL = "";
        string _kangdianmabijilv = "<color=#5DADE2>"+ KANGDIANMBJL + this.KangDianMabiJilv + "</color>\n";

        
        str += _liveStr+ _lanStr + _atkStr + _defStr+ _yingzhiStr+_baojijilv+_baojishanghaibeilv+ _shanghaijianmianlv + _kangdujilv+ _kangduShanghaiJilv+ _kanghuojilv+ _kanghuoshanghaijilv+ _kangdianjilv+ _kangdianmabijilv;
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

    //float _CurrentBaojiBeishu = 0;

    //徽章的数据加成
    void GetHZDate(HZDate hzdate)
    {
        //print(hzdate.HZName);
        
        if (hzdate.def != 0) this.def += hzdate.def;
        if (hzdate.atk != 0) this.atk += hzdate.atk;
        if (hzdate.BaoJiLv != 0) this.BaoJiLv += hzdate.BaoJiLv;
        if (hzdate.BaoJiShangHaiBeiLv != 0) {
            if(this.BaoJiShangHaiBeiLv< hzdate.BaoJiShangHaiBeiLv)
            {
                this.BaoJiShangHaiBeiLv = hzdate.BaoJiShangHaiBeiLv;
                //_CurrentBaojiBeishu = this.BaoJiShangHaiBeiLv;
            }
          
        }
        
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

        if (hzdate.addLan != 0)
        {
            this.maxLan += hzdate.addLan;
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


        if (hzdate.KangDuJilv != 0) KangDuJilv += hzdate.KangDuJilv;
        if (hzdate.KangDuShanghaiJilv != 0) KangDuShanghaijilv += hzdate.KangDuShanghaiJilv;



        if (hzdate.KangHuoJilv != 0) KangHuoJilv += hzdate.KangHuoJilv;
        if (hzdate.KangHuoShanghaiJilv != 0) KangHuoShanghaijilv += hzdate.KangHuoShanghaiJilv;



        if (hzdate.KangDianJilv != 0) KangDianJilv += hzdate.KangDianJilv;
        if (hzdate.KangDianMabiJilv != 0) KangDianMabiJilv = hzdate.KangDianMabiJilv;

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
