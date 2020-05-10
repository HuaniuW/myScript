using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChange : MonoBehaviour {
    [Header("去的场景名字")]
    public string GoScreenName = "";
    [Header("位置")]
    public string PlayerPosition = "";
    [Header("是否碰撞触发")]
    public bool IsHitGo = false;
    [Header("朝向")]
    public float PlayerScaleX = 1;
    [Header("门的名字")]
    public string doorName = "";

    [Header("门的朝向")]
    public string MenFX = "";


    //判断 是否 生成 新的 地图
    [Header("随机地图名字")]
    public string ReMapName = "";



    [Header("当前门的位置 保留到 进入门位置 来判断出口是哪个门")]
    public string DangQianMenWeizhi = "";

    //查找全局 数据 中是否 有 地图 数据     没有的 话 要生成  并且 保存入 全局数据


    //当前 是 map1  还是map2  都不是的话 就转map1


	// Use this for initialization
	void Start () {
		
	}

    void SetPlayerPositionAndScreen()
    {
        GlobalSetDate.instance.playerPosition = PlayerPosition;
        GlobalSetDate.instance.CReMapName = ReMapName;
        if (ReMapName != "")
        {
            //这里是进入 随机 地图
            if (SceneManager.GetActiveScene().name != "RMap_1")
            {
                GlobalSetDate.instance.screenName = "RMap_1";
            }
            else
            {
                GlobalSetDate.instance.screenName = "RMap_2";
            }
        }
        else
        {
            GlobalSetDate.instance.screenName = GoScreenName;
        }
        
        GlobalSetDate.instance.IsChangeScreening = true;
        GlobalSetDate.instance.doorName = doorName;
    }

    GameObject playerUI;
    void ChangeScreen()
    {
        playerUI = GlobalTools.FindObjByName("PlayerUI");
        playerUI.GetComponent<PlayerUI>().skill_bar.GetComponent<UI_ShowPanel>().saveAllHZDate();
        //获取角色当前数据 当前血量 当前蓝量  发给GlobalSetDate  什么格式 以后再说  cLive=1000,cLan=1000
        GlobalSetDate.instance.ScreenChangeDateRecord();
        GlobalSetDate.instance.HowToInGame = GlobalSetDate.CHANGE_SCREEN;
        if (GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1") != null) GlobalSetDate.instance.bagDate = GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1").GetComponent<Mianban1>().saveDate();
        
        //通知储存关卡变化的数据
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_SCREEN, null), this);
        //ObjectPools.GetInstance().delAll();//解决切换场景时候特效没回收被销毁导致再取取不出来的问题  但是这样销毁会导致卡顿很长的切换速度 已用重新创建解决
        SetPlayerPositionAndScreen();
        //SceneManager.LoadScene("loads");
        StartCoroutine(IEStartLoading(GlobalSetDate.instance.screenName));
        playerUI.GetComponent<PlayerUI>().GetScreenZZChange(1);

    }

	// Update is called once per frame
	void Update () {
		
	}




    void OnTriggerEnter2D(Collider2D Coll)
    {
        //print(Coll.tag + "  --  " + Coll.transform.tag);
        if(Coll.tag == "Player")
        {


            print("hit!!!!   ");
            if (ReMapName != "")
            {
                GlobalSetDate.instance.GetMapMsgByName(ReMapName,MenFX,DangQianMenWeizhi);
            }

            if (Coll.transform.GetComponent<RoleDate>().isDie) return;
            ChangeScreen();
            Coll.GetComponent<GameBody>().GetStand();

            //查找地图数据

            //if (!IsHitGo) return;


        }
    }











    private IEnumerator IEStartLoading(string scene)
    {
        int displayProgress = 0;
        int toProgress = 0;
        //if (Globals.isDebug) print("screen>  " + scene);
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
        //是否允许自动跳场景 （如果设为false 只会加载到90 不会继续加载）
        op.allowSceneActivation = false;
        //print("hi!!!");
        while (op.progress < 0.9f)
        {
            //print("in");
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                //SetLoadingPercentage(displayProgress);
                //loadingBar.rectTransform.se
                playerUI.GetComponent<PlayerUI>().ShowLoadProgressNums(displayProgress);
                print("screenChange progress: "+ displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            //SetLoadingPercentage(displayProgress);
            playerUI.GetComponent<PlayerUI>().ShowLoadProgressNums(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        //设置为true 后 加载到100后直接自动跳转
        op.allowSceneActivation = true;

    }
}
