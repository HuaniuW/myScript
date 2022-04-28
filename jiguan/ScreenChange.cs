using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
//using System.Runtime.CompilerServices;
//using Object = UnityEngine.Object;


public class ScreenChange : MonoBehaviour {
    [Header("去的场景名字")]
    public string GoScreenName = "";
    [Header("位置")]
    public string PlayerPosition = "";
    [Header("是否碰撞触发")]
    public bool IsHitGo = false;
    [Header("朝向")]
    public float PlayerScaleX = 1;
    [Header("转去场景 查找 门 的名字")]
    public string doorName = "";

    [Header("门的朝向")]
    public string MenFX = "";

    //[Header("是否进入随机地图 ")]
    //public bool IsInReMap = false;

    //判断 是否 生成 新的 地图
    [Header("随机地图名字")]
    public string ReMapName = "";


    [Header("玩家出来的 位置")]
    public Transform OutPosition;



    [Header("当前门的位置 保留到 进入门位置 来判断出口是哪个门")]
    public string DangQianMenWeizhi = "";


    [Header(" 自动地图碰撞触发 全局的 地图类型 取向  默认是0(不改变) 森林")]
    public int HitChangeGlobalMapType = 0;

    //查找全局 数据 中是否 有 地图 数据     没有的 话 要生成  并且 保存入 全局数据

    //public bool IsYuJiazai = false;
    //当前 是 map1  还是map2  都不是的话 就转map1


	// Use this for initialization
	void Start () {
        //StartCoroutine(IEStartLoading(GoScreenName));
        //if(IsYuJiazai)LoadScreen(GoScreenName);
    }


    public void SetMenMsg(string fx,string _GoScreenName)
    {
        DangQianMenWeizhi = fx;
        //判断是否是特殊地图
        if (_GoScreenName.Split('$').Length == 2)
        {
            GoScreenName = _GoScreenName.Split('$')[0];
        }
        else
        {
            ReMapName = _GoScreenName;
        }
        //GoScreenName = _GoScreenName;
    }


    public void SetMenMsg2(string goSc,string fx,string goRMap)
    {
        GoScreenName = goSc;
        DangQianMenWeizhi = fx;
        ReMapName = goRMap;
    }


    void SetPlayerPositionAndScreen()
    {
        GlobalSetDate.instance.playerPosition = PlayerPosition;
        GlobalSetDate.instance.CReMapName = ReMapName;

        //print("?????  ReMapName  " + ReMapName);

        if (ReMapName != "")
        {
            //这里是进入 随机 地图   只有这两个地图 容器
            if (SceneManager.GetActiveScene().name != "RMap_1")
            {
                GlobalSetDate.instance.screenName = "RMap_1";
            }
            else
            {
                GlobalSetDate.instance.screenName = "RMap_2";
            }

            //print("-------------------------------------------------------------------------------------------------------- 进入的地图名字 "+ ReMapName);
        }
        else
        {
            GlobalSetDate.instance.screenName = GoScreenName;
            print(" 数据TempZGuanKaStr  >:   " + GlobalSetDate.instance.TempZGuanKaStr);
            //print("--------------------------------？？？？？？？？？------------------------------------------------------------------------ 进入的地图名字 " + GoScreenName);
        }
        
        GlobalSetDate.instance.IsChangeScreening = true;
        GlobalSetDate.instance.doorName = doorName;
    }

    GameObject playerUI;
    void ChangeScreen()
    {
        print(" 场景 变化   screenChange ");
        playerUI = GlobalTools.FindObjByName("PlayerUI");
        if(playerUI) playerUI.GetComponent<PlayerUI>().skill_bar.GetComponent<UI_ShowPanel>().SaveAllHZDate();

        //用来判断 地图地形 是否生成 过 如果生成过 就不用再生成了 直接根据数据生成就可以（控制 地图块内的 自动生成 粒子 草之类的）
        GlobalSetDate.instance.IsCMapHasCreated = false;

        //获取角色当前数据 当前血量 当前蓝量  发给GlobalSetDate  什么格式 以后再说  cLive=1000,cLan=1000
        GlobalSetDate.instance.ScreenChangeDateRecord();
        GlobalSetDate.instance.HowToInGame = GlobalSetDate.CHANGE_SCREEN;
        if (GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1") != null) {
            //GlobalSetDate.instance.bagDate = GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1").GetComponent<Mianban1>().HZSaveDate();
            //GlobalSetDate.instance.CurrentMapMsgDate.bagDate = GlobalSetDate.instance.CurrentMapMsgDate.bagDate = GlobalSetDate.instance.bagDate;
            GlobalSetDate.instance.CurrentMapMsgDate.bagDate = GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1").GetComponent<Mianban1>().HZSaveDate();
            print("切换到新 地图时  总地图数据  " + GlobalSetDate.instance.CurrentMapMsgDate.mapDate);
            print("GlobalSetDate.instance.CurrentMapMsgDate.bagDate： " + GlobalSetDate.instance.CurrentMapMsgDate.bagDate);
            GlobalDateControl.SaveMapDate();
        }
       
        
        //通知储存关卡变化的数据
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_SCREEN, null), this);
        //ObjectPools.GetInstance().delAll();//解决切换场景时候特效没回收被销毁导致再取取不出来的问题  但是这样销毁会导致卡顿很长的切换速度 已用重新创建解决
        SetPlayerPositionAndScreen();
        //SceneManager.LoadScene("loads");
        //print("***  GlobalSetDate.instance.screenName   "+ GlobalSetDate.instance.screenName);
        //StartCoroutine(IEStartLoading(GlobalSetDate.instance.screenName));
        //StartCoroutine(IEStartLoading(GoScreenName));
        //if (IsYuJiazai)
        //{
        //    op.allowSceneActivation = true;
        //}
        //else
        //{
            
        //}

        StartCoroutine(IEStartLoading(GlobalSetDate.instance.screenName));

        //启动遮罩
        playerUI.GetComponent<PlayerUI>().GetScreenZZChange(1);

        //GlobalSetDate.instance.Show_UIZZ();
    }

	// Update is called once per frame
	void Update () {
		
	}




    void OnTriggerEnter2D(Collider2D Coll)
    {
        //print(Coll.tag + "  --  " + Coll.transform.tag);
        if(Coll.tag == "Player")
        {
            GetGC();
            //这里判断 是否要进随机的大地图  小地图

            //这里判断 地图类型 如果 特殊地图类型是 Boss  CD  或者JY   就改为 自动地图（自动地图的，特殊地图） 

            //Boss 有boss名 直接用boss名 没有 直接随机  全局记录 当前关卡 有boss  过关再次进入 要删除boss

            //另外生成一个 特殊地图的 信息列表 来匹配 特殊地图 是否有  有的话生成 特殊地图
            //eg   12!boss^boss_test!db^2!qj^2!jqj^2!bg^2 等11!cundang    9!juqing^juqingjueseming

            print("***  ReMapName????? "+ ReMapName  + "  GoScreenName  "+ GoScreenName);

            Globals.IsHitDoorStop = true;
            if (HitChangeGlobalMapType != 0) Globals.mapTypeNums = HitChangeGlobalMapType;
            GlobalMapDate.ClearGlobalCurrentMapMsg();

            if (ReMapName == "" && IsSpeMap(GoScreenName))
            {
                ReMapName = GlobalMapDate.CurrentSpelMapName;
                GlobalSetDate.instance.GetMapMsgByName(ReMapName, MenFX, DangQianMenWeizhi);
                //print("*** ReMapName "+ ReMapName);
            }else if (ReMapName != "")
            {
                print("说明是 从 特殊地图 跳到 随机里面   "+ ReMapName+ "  MenFX   " + MenFX);
                GlobalSetDate.instance.GetMapMsgByName(ReMapName,MenFX,DangQianMenWeizhi);
                //return;
            }
            else
            {
                GlobalSetDate.instance.GetInMenWeizhi(DangQianMenWeizhi);
            }

            

            if (Coll.transform.GetComponent<RoleDate>().isDie) return;
            ChangeScreen();
            Coll.GetComponent<GameBody>().GetStand();

            //查找地图数据

            //if (!IsHitGo) return;


        }
    }



    bool IsSpeMap(string goScreenName)
    {
        print("判断是否是 特殊地图！！！！   "+ goScreenName);
        foreach(string s in GlobalMapDate.TeShuShengchengDiTuList)
        {
            
            string TeShuShengchengDituName = "map_"+s.Split(':')[0] + "-" + s.Split(':')[1].Split('!')[0];
            //print("***  TeShuShengchengDituName  " + TeShuShengchengDituName+ "  goScreenName  "+ goScreenName);
            if (TeShuShengchengDituName == goScreenName) {
                GlobalMapDate.CurrentSpelMapName = TeShuShengchengDituName;
                //s 的数据结构 eg:  "i:2!boss^1!db^1,bg^1,jyj^1,yj1^1,yj2^1,qj1^1,qj2^1,xs1^1,xs2^1",

                string gkMsg = s.Split(':')[1];
                //print("gkMsg   " + gkMsg);
                string[] msgArr = gkMsg.Split('!');
                //print("msgArr.Length   "+ msgArr.Length);
                GlobalMapDate.CurrentSpelMapType = msgArr[1];
                if (msgArr.Length>=3) GlobalMapDate.CurrentSpeMapDiXingMsg = msgArr[2];


                //print("*** TeShuShengchengDituName   "+ TeShuShengchengDituName);
                //print("*** GlobalMapDate.CurrentSpelMapName   " + GlobalMapDate.CurrentSpelMapName);
                //print("*** GlobalMapDate.CurrentSpelMapType --  " + GlobalMapDate.CurrentSpelMapType);


                return true;
            } 
        }
        return false;
    }









    AsyncOperation op;
    private IEnumerator IEStartLoading(string scene)
    {
        int displayProgress = 0;
        int toProgress = 0;
        //if (Globals.isDebug) print("*** screen>  " + scene);
        //op = null;

        //Scene scene = SceneManager.GetSceneByName(sceneName);
        //SceneManager.UnloadSceneAsync(0);
        op = SceneManager.LoadSceneAsync(scene);
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
                //GlobalSetDate.instance.ShowLoadProgressNums(displayProgress);
                //print("screenChange progress: "+ displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            //SetLoadingPercentage(displayProgress);
            playerUI.GetComponent<PlayerUI>().ShowLoadProgressNums(displayProgress);
            //print("screenChange progress: " + displayProgress);
            yield return new WaitForEndOfFrame();
        }
        //设置为true 后 加载到100后直接自动跳转
        op.allowSceneActivation = true;

    }


    void LoadScreen(string scene)
    {
        //int displayProgress = 0;
        //int toProgress = 0;
        //if (Globals.isDebug) print("*** screen>  " + scene);
        op = SceneManager.LoadSceneAsync(scene);
        //是否允许自动跳场景 （如果设为false 只会加载到90 不会继续加载）
        op.allowSceneActivation = false;

       
    }

    public bool IsNeedGC = false;
    bool IsHasGC = false;
    void GetGC()
    {
        if (IsNeedGC&&!IsHasGC)
        {
            IsHasGC = true;
            ObjectPools.GetInstance().delAll();
            //卸载没有被引用的资源  不要用  会卡住
            //Resources.UnloadUnusedAssets();

            //立即进行垃圾回收
            GC.Collect();
            GC.WaitForPendingFinalizers();//挂起当前线程，直到处理终结器队列的线程清空该队列为止
            //GC.Collect();
        }
    }


}
