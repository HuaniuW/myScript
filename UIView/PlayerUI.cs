using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public RectTransform btn_skill1;
    public RectTransform btn_skill2;
    public RectTransform btn_yaoping;
    public RectTransform btn_fx;
    public Button btn_a;
    public Button btn_b;
    public Button btn_c;
    public Button btn_zt;

    public Image b1;
    public Image b2;
    public Image b3;

    public Image b4;
    public Image b5;
    public Image b6;

    public RectTransform saveing;


    [Header("----boss名字 数量")]
    public Text Txt_BossName;


    public GameObject player;
    public GameBody playerObj;

    public RectTransform screenChangeZZ;
    public Text screenLoadTxt;
    public Image LoadImgBar;

    [Header("干扰弹 数量")]
    public Text Txt_ganraodan;
    [Header("机炮 数量")]
    public Text Txt_jipao;

    [Header("导弹 数量")]
    public Text Txt_daodan;

    [Header("***子弹")]
    public Image img_zidan;

    [Header("***导弹")]
    public Image img_daodan;

    [Header("***干扰弹")]
    public Image img_ganraodan;



    public GameObject skill_bar;
    public GameObject ui_hun;
    public GameObject ui_feidao;
    public GameObject ui_shanbi;



    public void HideFeiJijiaUI()
    {
        ui_shanbi.gameObject.SetActive(false);
        skill_bar.gameObject.SetActive(false);
    }



    // Use this for initialization
    void Start () {
        // GetTypePC();
        //GetTypeMobile();
        if (player == null)
        {
            player = GlobalTools.FindObjByName("player");
            if(player)playerObj = player.GetComponent<GameBody>();
        }

        //this.GetComponent<Canvas>().Get = GlobalTools.FindObjByName("Main Camera"); 

        if (Globals.isPC)
        {
            GetTypePC();
        }
        else
        {
            GetTypeMobile();
        }

        btn_zt.onClick.AddListener(GetSetUI);
        Txt_BossName.GetComponent<CanvasGroup>().alpha = 0;
        saveing.GetComponent<CanvasGroup>().alpha = 0;
        
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_SAVEING, this.GetSaveing);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_RESTART, this.RemoveSelfAndRestart);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.BOSS_NAME, this.ShowBossNameTxt);
        GetMainCamera();

        //screenChangeZZ.gameObject.SetActive(false);
        //这里是控制 开始遮罩的 
        this.screenChangeZZ.GetComponent<CanvasGroup>().alpha = 1;
        screenLoadTxt.text = "";
        //StartCoroutine(IHideZZByTimes(0.5f));
        if(GlobalTools.FindObjByName("maps")&& GlobalTools.FindObjByName("maps").GetComponent<GetReMap2>())
        {
            StartCoroutine(IHideZZByTimes(0.2f));
        }
        else
        {
            HideUIZZ();
        }

        print("   playerUI***************************************** ");
        //GlobalSetDate.instance.Hide_UIZZ();

    }

    public IEnumerator IHideZZByTimes(float time)
    {
        yield return new WaitForSeconds(time);
        //print("延迟 显示 ZZ 延迟时间    "+time);
        HideZZ();
    }



    public void HideUIZZ(float times = 0)
    {
        //print("延迟 显示 ZZ 延迟时间2    " + times);
       if(times == 0)
        {
            HideZZ();
        }
        else
        {
            StartCoroutine(IHideZZByTimes(times));
        }
    }




    void HideZZ()
    {
        this.screenChangeZZ.GetComponent<CanvasGroup>().alpha = 0;
        //不禁止 会阻挡 鼠标点击事件
        screenChangeZZ.gameObject.SetActive(false);
    }




    public void HideSelf()
    {
        isShowUI = false;
        GetComponent<CanvasGroup>().alpha = 0;
    }

    public void GetUIShow()
    {
        isShowUI = true;
    }


    bool isShowUI = false;
    public void ShowSelf()
    {
        if (!isShowUI) return;
        if (GetComponent<CanvasGroup>().alpha < 0.92)
        {
            GetComponent<CanvasGroup>().alpha += (1 - GetComponent<CanvasGroup>().alpha) * 0.2f;
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = 1;
            isShowUI = false;
        }
    }



    //void InScreen()
    //{
    //    if (this.screenChangeZZ != null)
    //    {
    //        this.screenChangeZZ.GetComponent<CanvasGroup>().alpha += (ZZAlphaNums - this.screenChangeZZ.GetComponent<CanvasGroup>().alpha) * 0.1f;
    //        if (ZZAlphaNums == 0 && this.screenChangeZZ.GetComponent<CanvasGroup>().alpha <= 0.2)
    //        {
    //            this.screenChangeZZ.GetComponent<CanvasGroup>().alpha = 0;
    //            IsScreenChangeing = false;
    //            //防止遮挡鼠标事件
    //            screenChangeZZ.gameObject.SetActive(false);
    //        }
    //    }
    //}

    public GameObject UI_PlayerLive;
    public GameObject UI_BossLive;


    //隐藏掉 除了 ZZ之外的UI
    public void HideUIs()
    {
        skill_bar.SetActive(false);
        ui_shanbi.SetActive(false);
        UI_PlayerLive.SetActive(false);
        UI_BossLive.SetActive(false);

    }

  


    void GetScreenChange()
    {
        if (!IsScreenChangeing) return;
        //print("场景切换 遮罩调用  ZZAlphaNums： "+ ZZAlphaNums);
        if (this.screenChangeZZ != null)
        {
            this.screenChangeZZ.GetComponent<CanvasGroup>().alpha += (ZZAlphaNums - this.screenChangeZZ.GetComponent<CanvasGroup>().alpha) * 0.1f;
            if(ZZAlphaNums == 0&&this.screenChangeZZ.GetComponent<CanvasGroup>().alpha <= 0.2)
            {
                //print("??????????????????????????????????????   >>>>>>>>>>>>>>  "+ ZZAlphaNums);
                this.screenChangeZZ.GetComponent<CanvasGroup>().alpha = 0;
                IsScreenChangeing = false;
                //防止遮挡鼠标事件
                screenChangeZZ.gameObject.SetActive(false);
            }
        }
    }

    float ZZAlphaNums = 0;
    bool IsScreenChangeing = false;
    public void GetScreenZZChange(float alphaNum)
    {
        screenChangeZZ.gameObject.SetActive(true);
        ZZAlphaNums = 1;
        IsScreenChangeing = true;
    }

    //RectTransform rt = LoadImgBar.GetComponent<RectTransform>();
    public void ShowLoadProgressNums(float nums, bool isHasChangeScreen = false)
    {
        
        if(nums == 100)
        {
            //IsScreenChangeing = false;
            //this.screenChangeZZ.GetComponent<CanvasGroup>().alpha = 0;
            //ZZAlphaNums = 0;
            
        }
        if(LoadImgBar) LoadImgBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, nums * 1.5f);
        if (screenLoadTxt != null) screenLoadTxt.text = "LOADING... "+nums + "%";
    }


    void GetMainCamera()
    {
        //print(GlobalTools.FindObjByName("MainCamera"));
        //print(this.transform.GetComponent<Canvas>().gameObject.camera);
    }

    private void OnDestroy()
    {
        skill_bar.GetComponent<UI_ShowPanel>().OnDistory2();
        ui_hun.GetComponent<UI_Hun>().OnDistory2();
        ui_feidao.GetComponent<UI_FeiDao>().GetDistory();
        
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_SAVEING, this.GetSaveing);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_RESTART, this.RemoveSelfAndRestart);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.BOSS_NAME, this.ShowBossNameTxt);
    }

    void RemoveSelfAndRestart(UEvent e)
    {
        //UI内的 其他子组件  侦听  没有把代码写到 主PlayerUI组件上 不会自动 调用销毁  OnDistory   这里就必须手动调用  或者 改写到主UI上********************注意
       
        //DestroyImmediate 立即销毁  Destroy是异步销毁
        DestroyImmediate(this.gameObject, true);
        //Destroy(this.gameObject);
        GlobalSetDate.instance.GetGameDateStart();
    }



    bool isSaveing = false;
    void GetSaveing(UEvent e)
    {
        isSaveing = true;
    }

    float alphaNums = 0.01f;
    int showSaveTxtTimeNums = 0;
    void ShowSaveing()
    {
        showSaveTxtTimeNums++;
        if (showSaveTxtTimeNums>=200)
        {
            HideSaveTxt();
            return;
        }

        if (saveing.GetComponent<CanvasGroup>().alpha == 1)
        {
            alphaNums = -0.02f;
        }
        else if (saveing.GetComponent<CanvasGroup>().alpha <= 0.4f)
        {
            alphaNums = 0.02f;
        }

        saveing.GetComponent<CanvasGroup>().alpha += alphaNums;
        //if (alphaNums > 1) alphaNums = 1;
        //if (alphaNums < 0) alphaNums = 0;
    }

    void HideSaveTxt()
    {
        showSaveTxtTimeNums = 0;
        saveing.GetComponent<CanvasGroup>().alpha = 0;
        isSaveing = false;
    }

    

    GameObject setUI;
    public void GetSetUI()
    {
        print("   shezhi!!!! ");
        if(setUI == null)
        {
            setUI = GlobalTools.GetGameObjectByName("UI_set");
            GlobalSetDate.instance.IsChangeScreening = true;
            Globals.IsInSetBar = true;
            print("开启 设置");
            if(GlobalTools.FindObjByName("UI_Bag")) GlobalTools.FindObjByName("UI_Bag").GetComponent<UIBag>().HideMianban();
            player.GetComponent<GameBody>().GetStand();
        }
        else
        {
            print("关闭 ^^^^^^ 设置");
            GlobalSetDate.instance.IsChangeScreening = false;
            Globals.IsInSetBar = false;
            DestroyImmediate(setUI, true);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isSaveing)
        {
            ShowSaveing();
        }
        ShowSelf();
        GetScreenChange();
        JianZheng();
        ShowBossName();

    }





    void ShowBossNameTxt(UEvent e)
    {
        string s = e.eventParams.ToString();
        Txt_BossName.text = s;
        IsShowBossName = true;
    }



    bool IsShowBossName = false;
    float BossNameJishi = 0;
    float BossNameNums = 2;
    void ShowBossName()
    {
        if (IsShowBossName)
        {
            BossNameJishi += Time.deltaTime;
            if (BossNameJishi>= BossNameNums)
            {
                Txt_BossName.GetComponent<CanvasGroup>().alpha += (0 - Txt_BossName.GetComponent<CanvasGroup>().alpha) * 0.2f;
                if (Txt_BossName.GetComponent<CanvasGroup>().alpha <= 0.1f)
                {
                    Txt_BossName.GetComponent<CanvasGroup>().alpha = 0;
                    IsShowBossName = false;
                }
                return;
            }
            Txt_BossName.GetComponent<CanvasGroup>().alpha += (1 - Txt_BossName.GetComponent<CanvasGroup>().alpha) * 0.2f;
            
        }
    }

    
    void GetTypePC()
    {
        btn_a.gameObject.SetActive(false);
        btn_b.gameObject.SetActive(false);
        btn_c.gameObject.SetActive(false);
        btn_yaoping.position = b3.transform.position;
        btn_skill1.transform.position = b1.transform.position;
        btn_skill2.transform.position = b2.transform.position;

        btn_fx.gameObject.SetActive(false);
    }
    
    void GetTypeMobile()
    {
        btn_a.gameObject.SetActive(true);
        btn_b.gameObject.SetActive(true);
        btn_c.gameObject.SetActive(true);

        btn_fx.gameObject.SetActive(true);

        btn_skill1.transform.position = b4.transform.position;
        btn_skill2.transform.position = b5.transform.position;
        btn_yaoping.transform.position = b6.transform.position;

        btn_a.onClick.AddListener(GetAtk);
        btn_b.onClick.AddListener(GetJump);
        btn_c.onClick.AddListener(GetDodge1);
    }

    void GetAtk()
    {
        playerObj.GetAtk();
    }

    void GetJump()
    {
        playerObj.GetJump();
    }

    void GetDodge1()
    {
        playerObj.GetDodge1();
    }



    float _jianzhengNums = 0;
    float _jianzhengNumsJishi = 0;
    bool IsJianZheng = false;
    void JianZheng()
    {
        if (IsJianZheng)
        {
            _jianzhengNumsJishi += Time.deltaTime;
            if(_jianzhengNumsJishi>= _jianzhengNums)
            {
                IsJianZheng = false;
                _jianzhengNumsJishi = 0;
                Time.timeScale = 1;
            }
        }
    }


    //全局减速
    public void GetSlowByTimes(float times = 1,float TimeScaleNums = 0.5f)
    {
        //print(" 减慢 时间速度！！！ times  "+times+ "   TimeScaleNums "+ TimeScaleNums);
        _jianzhengNums = times;
        Time.timeScale = TimeScaleNums;
        IsJianZheng = true;
        _jianzhengNumsJishi = 0;
    }
}
