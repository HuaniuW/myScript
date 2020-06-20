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


    public GameObject player;
    public GameBody playerObj;

    public RectTransform screenChangeZZ;
    public Text screenLoadTxt;

    public GameObject skill_bar;
    public GameObject ui_hun;
    public GameObject ui_feidao;
    public GameObject ui_shanbi;

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

        saveing.GetComponent<CanvasGroup>().alpha = 0;
        
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_SAVEING, this.GetSaveing);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_RESTART, this.RemoveSelfAndRestart);
        GetMainCamera();

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


    void GetScreenChange()
    {
        if (!IsScreenChangeing) return;
        if (this.screenChangeZZ != null)
        {
            this.screenChangeZZ.GetComponent<CanvasGroup>().alpha += (ZZAlphaNums - this.screenChangeZZ.GetComponent<CanvasGroup>().alpha) * 0.1f;
            if(ZZAlphaNums == 0&&this.screenChangeZZ.GetComponent<CanvasGroup>().alpha <= 0.2)
            {
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

    public void ShowLoadProgressNums(float nums, bool isHasChangeScreen = false)
    {
        if(nums == 100)
        {
            //IsScreenChangeing = false;
            //this.screenChangeZZ.GetComponent<CanvasGroup>().alpha = 0;
            ZZAlphaNums = 0;
            
        }
        if (screenLoadTxt != null) screenLoadTxt.text = nums + "%";
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
    void GetSetUI()
    {
        if(setUI == null)
        {
            setUI = GlobalTools.GetGameObjectByName("UI_set");
            GlobalSetDate.instance.IsChangeScreening = true;
        }
        else
        {
            GlobalSetDate.instance.IsChangeScreening = false;
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




    //全局减速
    public void GetSlowByTimes(float times = 1)
    {
        GetComponent<TheTimer>().TimesAdd(times, TimeCallBack);
        Time.timeScale = 0.5f;
    }

    public void TimeCallBack(float nums)
    {
        //print("---------------------------------------------------------------->>>>>>>>>>>>>>>>>??????????????????????");
        Time.timeScale = 1;
    }
}
