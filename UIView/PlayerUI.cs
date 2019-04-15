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
    // Use this for initialization
    void Start () {
        // GetTypePC();
        //GetTypeMobile();
        if (player == null)
        {
            player = GlobalTools.FindObjByName("player");
            playerObj = player.GetComponent<GameBody>();
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

        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_SAVEING, this.GetSaveing);
        GetMainCamera();
    }


    void GetMainCamera()
    {
        print(GlobalTools.FindObjByName("MainCamera"));
        //print(this.transform.GetComponent<Canvas>().gameObject.camera);
    }

    private void OnDestroy()
    {
        if(Globals.isDebug)print("PlayerUI");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_SAVEING, this.GetSaveing);
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

    void RemoveSelf(UEvent e) {
        DestroyImmediate(this.gameObject,true);
    }

    GameObject setUI;
    void GetSetUI()
    {
        if(setUI == null)
        {
            setUI = GlobalTools.GetGameObjectByName("UI_shezhi");
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
}
