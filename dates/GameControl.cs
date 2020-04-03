using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using XLua;

//[LuaCallCSharp]
public class GameControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TimeTest();

    }

    void TimeTest()
    {
        Application.targetFrameRate = 30;
        //float s = 8 * 365/7*6;
        //float t = 1000000;
        //print("--------------------------------------------->每个月的收入  " + t / s * 8 * 31+"元"+"  每天价值 "+t/s*8+"  每小时价值 "+t/s);

        //float dj = 39*0.7f*0.55f;
        //float zong = 10000000;
        //print("1000万收入 需要卖出多少份   "+zong/dj);
    }

    public bool IsInPlot = false;

    //public void LuaTest(string str)
    //{
    //    //print("hello 你好 XLua!"+str);
    //}

    private void Awake()
    {


        //GameSaveDate.GetInstance().GetTestSave();
        if (Globals.isDebug) print("游戏关卡控制类 启动 当前场景名字   "+ SceneManager.GetActiveScene().name);
        //GlobalSetDate.instance.GetGuanKaStr();
        //GlobalSetDate.instance;
        //GlobalSetDate.instance.Init();
        GuankaName = SceneManager.GetActiveScene().name;
        if (IsInPlot) return;
        GetPlayer();
        if (player == null)
        {
            print("没有找到主角 游戏中断！");
            return;
        }
        GetPlayerUI();
        GetPlayBag();
        GetCamersTargetToPlayer();
        InitGuanKaDate();
        GetPlayerStatus();

        
    }

    private void OnEnable()
    {
        //if (Globals.isDebug) print("游戏关卡控制类OnEnable 启动");
        
    }










    //-----------------------------------------------------------------------------关卡数据的存储---------------------------------------------------------

    [Header("当前关卡名字")]
    public string GuankaName = "";

    [Header("记录关卡的动态数据")]
    public string GuanKaStr = "";
    
    string TempCurrentGKDate;
    //初始化关卡数据
    public void InitGuanKaDate()
    {
        GlobalSetDate.instance.GetGameAllCunstomStr();
        //查找存档中是否有本关卡的关卡记录  通过本关卡名字获取本关卡数据
        TempCurrentGKDate = GlobalSetDate.instance.GetGuanKaStrByGKNameAndRemoveIt(SceneManager.GetActiveScene().name);
        print("关于本关卡的关卡记录>>   " + TempCurrentGKDate);
        if (TempCurrentGKDate == null) {
            TempCurrentGKDate = ""; //GuanKaStr;
        }
        else
        {
            //如果全局数据中有本关卡数据  清除掉原数据中本关卡数据   匹配本关卡数据
            GetPiPei();
        }
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_SCREEN, SetChangeThisGKInZGKTempDate);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.RECORDOBJ_CHANGE, GKDateChange);
    }



    private void OnDestroy()
    {
        //print("OnDestroy!!!!!!");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANGE_SCREEN, SetChangeThisGKInZGKTempDate);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.RECORDOBJ_CHANGE, GKDateChange);
    }

    //是否包含查找的数据
    public bool IsHasDate(string changeDate, string[] currentGKDateArr)
    {
        string name = changeDate.Split('-')[0];
        for (var i = 0; i < currentGKDateArr.Length; i++)
        {
            string date = currentGKDateArr[i];
            string dateName = date.Split('-')[0];
            if (name == dateName) return true;
        }
        return false;
    }


    //记录数据的改变 存入全局临时变量  如果是BOSS死掉 存入文档 捡到boss装备也要自动存档
    public void GKDateChange(UEvent e)
    {
        //men_1-1 改变门状态
        //if (Globals.isDebug) print(">>>  " + e.eventParams + "  > " + TempCurrentGKDate);
        //if (TempCurrentGKDate == "") return;
        string changeDate = e.eventParams.ToString();//men_1-1
        string changeDateName = changeDate.Split('-')[0];//men_1
        string type = changeDateName.Split('_')[0];//men
        //print("type  "+type);
        string changezt;
        if (changeDate.Split('-').Length>1) changezt = changeDate.Split('-')[1];

        if (TempCurrentGKDate == "") {
            TempCurrentGKDate += ","+changeDate;
            //print("1  "+ TempCurrentGKDate);
            return;
        }

        //men_1-0,men_2-0,boss_1-0 当前关卡数据长这样
        string[] currentGKDateArr = TempCurrentGKDate.Split(',');
        //先查找 关卡数据中是否有该数据 有的就变化状态 没有的话就在后面加
        if (!GlobalTools.IsHasDate(changeDate, currentGKDateArr)) {
            TempCurrentGKDate += ","+changeDate;
            return;
        }

        string newGKDate = "";
        for(var i = 0; i < currentGKDateArr.Length; i++)
        {
            string theGKDate = currentGKDateArr[i];
            string dateName = theGKDate.Split('-')[0];
            if (Globals.isDebug) print("theGKDate  >  " + theGKDate);
            //string zt = theGKDate.Split('-')[1];
            if(dateName == changeDateName)
            {
                if (i != currentGKDateArr.Length - 1)
                {
                    newGKDate += changeDate + ",";
                }
                else {
                    newGKDate += changeDate;
                }
            }
            else
            {
                if (i != currentGKDateArr.Length - 1)
                {
                    newGKDate += theGKDate + ",";
                }
                else
                {
                    newGKDate += theGKDate;
                }
            }
        }
        TempCurrentGKDate = newGKDate;

        print(TempCurrentGKDate);
        //if(type == "boss")
        //{
        //    //将关卡数据写入全局临时数据
        //    if(TempCurrentGKDate!=null) GlobalSetDate.instance.SetChangeThisGKInZGKTempDate(GuankaName + ":" + TempCurrentGKDate);
        //    //存档
        //    GlobalSetDate.instance.GetSave();
        //}
    }
    //将本关卡存入全局临时数据
    public void SetChangeThisGKInZGKTempDate(UEvent e)
    {
        //将关卡数据写入全局临时数据
        if (TempCurrentGKDate != null) GlobalSetDate.instance.SetChangeThisGKInZGKTempDate(GuankaName+":"+TempCurrentGKDate);
    }

    public string GetSaveZGKDate()
    {
        return GlobalSetDate.instance.SetChangeThisGKInZGKTempDate(GuankaName + ":" + TempCurrentGKDate);
    }


    //匹配记录的机关状态 门是开的还是关的 BOSS有没有杀掉
    void GetPiPei()
    {
		if (TempCurrentGKDate == "") return;
        //开始匹配关卡数据
        string[] strArr = TempCurrentGKDate.Split(',');
        if (Globals.isDebug) print("TempCurrentGKDate   "+ TempCurrentGKDate);
        for (var i = 0; i < strArr.Length; i++)
        {
            if (strArr[i] == "") continue;
            string s = strArr[i].Split('-')[0];
            string zt = "0";
            if (strArr[i].Split('-').Length>1) zt = strArr[i].Split('-')[1];

            string sName = s.Split('_')[0];
            //找名字 好像部分大小写
            if (sName == "Men")
            {
                if (zt == "0")
                {
                    //print("ssss  "+s);
                    GlobalTools.FindObjByName(s).GetComponent<Door>().Chushi();
                }
                else if (zt == "1")
                {
                    //print("s   " + s);
                    GlobalTools.FindObjByName(s).GetComponent<Door>().HasOpen();
                }
            }
            else if (sName == "BOSS")
            {
                if(GlobalTools.FindObjByName(s)!=null) GlobalTools.FindObjByName(s).SetActive(false);
            } else if (sName == "WP") {
                //GlobalTools.FindObjByName(s).SetActive(false);
                if(GlobalTools.FindObjByName(s)!=null) GlobalTools.FindObjByName(s).GetComponent<Wupinlan>().DistorySelf();
            }else if (sName == "guai")
            {

            }else if (sName == "JG")
            {
                //机关记录
                //JG_screenName-nums(数组位置)
                string ScreenName = strArr[i].Split('_')[1]+"_"+strArr[i].Split('_')[2].Split('-')[0];
                print("ScreenName   "+ ScreenName);
                if(ScreenName == SceneManager.GetActiveScene().name)
                {
                    JGNum = int.Parse(strArr[i].Split('-')[1]);
                    print("----------------------------  匹配！！！");
                }
            }
        }
    }

    public int JGNum = 0;
    //-------------------------------------------------------------------------------------------------------------------------------------------------









    // Update is called once per frame
    void Update () {
        //print(player.transform.position);
        //if(player) player.GetComponent<GameBody>().TurnRight();


        //print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>  "+Time.time);
        //if(Time.time>=10) print("Time.realtimeSinceStartup    " + Time.realtimeSinceStartup);

    }

    //GameObject FindObjByName(string _name)
    //{
    //    GameObject obj = GameObject.Find("/" + _name) as GameObject;
    //    if(obj == null)obj = GameObject.Find("/"+_name+"(Clone)") as GameObject;
    //    return obj;
    //}


    GameObject InstancePrefabByName(string _name) {
        GameObject obj = Resources.Load(_name) as GameObject;
        GameObject.Instantiate(obj);
        return obj;
    }

    GameObject player;
    //找到主角
    void GetPlayer()
    {
        
        if (GlobalTools.FindObjByName("player") == null)
        {
            player = InstancePrefabByName("player");
        }
        else
        {
            player = GlobalTools.FindObjByName("player");
        }
        
    }

    

    void GetPlayerStatus()
    {
        if(GlobalSetDate.instance.HowToInGame != GlobalSetDate.TEMP_SCREEN) player.transform.position = GlobalSetDate.instance.GetPlayerInScreenPosition();
       
        // 是怎么进入游戏的 1.新游戏 2.取档 3.过场 4.传送（待定）5.临时场景直接进入游戏
        if (GlobalSetDate.instance.HowToInGame == GlobalSetDate.NEW_GAME)
        {
            if(Globals.isDebug)print("新游戏！");
            GlobalSetDate.instance.GetSave();
            GlobalSetDate.instance.HowToInGame = GlobalSetDate.LOAD_GAME;
        }
        else if (GlobalSetDate.instance.HowToInGame == GlobalSetDate.LOAD_GAME)
        {
            if (Globals.isDebug) print("取档进入游戏！");
            //player.transform.position = GlobalSetDate.instance.GetPlayerInScreenPosition();
            player.GetComponent<RoleDate>().live = float.Parse(GlobalSetDate.instance.CurrentUserDate.curLive);
        }
        else if (GlobalSetDate.instance.HowToInGame == GlobalSetDate.CHANGE_SCREEN)
        {
            if (Globals.isDebug) print("转场进入游戏");
            //背包更新

            //player.transform.position = GlobalSetDate.instance.GetPlayerInScreenPosition();
            //指定当前数值
            GlobalSetDate.instance.GetScreenChangeDate();
        } else if (GlobalSetDate.instance.HowToInGame == GlobalSetDate.TEMP_SCREEN) {
            if (Globals.isDebug) print("临时 直接重场景进入的游戏");
            GlobalSetDate.instance.GetSave();
            GlobalSetDate.instance.HowToInGame = GlobalSetDate.LOAD_GAME;
        }
        
        //摄像机位置跟随
        GameObject mainCamera = GlobalTools.FindObjByName("MainCamera");
        if (mainCamera) mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, mainCamera.transform.position.z);
        //玩家站立
        player.GetComponent<GameBody>().SetV0();
        //当前是什么状态 新进游戏（不管是取档 还是新游戏）
        //或者是 跳场景
        //玩家状态 当前气血 护甲 （比如玩家身上是什么 徽章的特效 持续事件   负面特效时间）  跳转场景的时候需要用到这些
        //玩家状态 记录玩家持续的 增益减益等等的 乱七八糟的各种状态
        

        GlobalSetDate.instance.IsChangeScreening = false;
        //print("玩家位置   " + player.transform.position);

        //print("player.transform.localScale     " + player.transform.localScale);
        Time.timeScale = 1;
    }


    void GetPlayBag()
    {
        if (GlobalTools.FindObjByName("UI_Bag") == null)
        {
            //加载背包
            GlobalTools.GetGameObjectByName("UI_Bag");
        }

        //GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().skill_bar.GetComponent<UI_ShowPanel>().GetAllHZDate();
    }



    //生成UI
    void GetPlayerUI()
    {
        GameObject playerUI;
        if (GlobalTools.FindObjByName("PlayerUI") == null)
        {
            playerUI = InstancePrefabByName("PlayerUI");
        }
        else
        {
            playerUI = GlobalTools.FindObjByName("PlayerUI");
        }
        //playerUI.GetComponent<DontDistoryObj>().ShowSelf();
        playerUI.GetComponent<XueTiao>().GetTargetObj(GlobalTools.FindObjByName("player"));
        //GlobalTools.FindObjByName("player").GetComponent<RoleDate>().Lan = float.Parse(GlobalSetDate.instance.CurrentUserDate.curLan);
        //playerUI.GetComponent<PlayerUI>().ui_hun.GetComponent<UI_Hun>().SetHun();
    }


    //摄像头定焦 和找到敌人
    void GetCamersTargetToPlayer()
    {
        //print("hi");
        this.GetComponent<CameraController>().GetTargetObj(GlobalTools.FindObjByName("player").transform);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_ENEMY), null);
    }

}
