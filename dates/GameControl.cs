using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }

    private void Awake()
    {
        //GameSaveDate.GetInstance().GetTestSave();
        if (Globals.isDebug) print("游戏关卡控制类 启动    "+ SceneManager.GetActiveScene().name);
        //GlobalSetDate.instance.GetGuanKaStr();
        //GlobalSetDate.instance;
        //GlobalSetDate.instance.Init();
        GuankaName = SceneManager.GetActiveScene().name;
        GetPlayer();
        GetPlayerUI();
        GetPlayBag();
        GetTargetPlayer();
        InitGuanKaDate();
        if (GlobalSetDate.instance.IsNewGame)
        {
            GlobalSetDate.instance.IsNewGame = false;
            GlobalSetDate.instance.GetSave();
        }
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
        GlobalSetDate.instance.GetGuanKaStr();
        //查找存档中是否有本关卡的关卡记录
        TempCurrentGKDate = GlobalSetDate.instance.GetGuanKaStrByGKNameAndRemoveIt(SceneManager.GetActiveScene().name);//返回总关卡 或者""
        //print("关于本关卡的关卡记录>>   " + TempCurrentGKDate);
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
    bool IsHasDate(string changeDate, string[] currentGKDateArr)
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
        if (Globals.isDebug) print(">>>  " + e.eventParams + "  > " + TempCurrentGKDate);
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
        if (!IsHasDate(changeDate, currentGKDateArr)) {
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
		//print("GuanKaStr  >>>  "+GuanKaStr);
		//print(GuanKaStr);
        for (var i = 0; i < strArr.Length; i++)
        {
			//print(strArr[0] + " ?  ");
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
                GlobalTools.FindObjByName(s).SetActive(false);
            } else if (sName == "WP") {
                //GlobalTools.FindObjByName(s).SetActive(false);
                GlobalTools.FindObjByName(s).GetComponent<Wupinlan>().DistorySelf();
            }else if (sName == "guai")
            {

            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------









    // Update is called once per frame
    void Update () {
        //print(player.transform.position);
	}

    GameObject FindObjByName(string _name)
    {
        GameObject obj = GameObject.Find("/" + _name) as GameObject;
        if(obj == null)obj = GameObject.Find("/"+_name+"(Clone)") as GameObject;
        return obj;
    }


    GameObject InstancePrefabByName(string _name) {
        GameObject obj = Resources.Load(_name) as GameObject;
        GameObject.Instantiate(obj);
        return obj;
    }

    GameObject player;
    //找到主角 获取主角坐标
    void GetPlayer()
    {
        //if (GlobalSetDate.instance.player != null)
        //{
        //    GlobalSetDate.instance.player.SetActive(true);
        //    player = GlobalSetDate.instance.player;
        //    //player.SetActive(true);
        //    print("player   "+player);
        //}
        //else
        //{

        //    GlobalSetDate.instance.player = player;
        //}



        if (FindObjByName("player") == null)
        {
            player = InstancePrefabByName("player");
        }
        else
        {
            player = FindObjByName("player");
        }


        //设置玩家进场位置
        if (GlobalSetDate.instance.playerPosition!="") player.transform.position = GlobalSetDate.instance.GetPlayerInScreenPosition();
        GameObject mainCamera = GlobalTools.FindObjByName("MainCamera");
        if (mainCamera) mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, mainCamera.transform.position.z);
        //玩家站立
        player.GetComponent<GameBody>().SetV0();
        //玩家状态 气血 护甲 
        GlobalSetDate.instance.IsChangeScreening = false;
        //print("p "+player.GetComponent<GameBody>().GetBodyScale());
        //player.transform.localScale = new Vector3(1, 1, 1);
        FirstInGame();

        //print("玩家位置   " + player.transform.position);
    }




    void FirstInGame()
    {
        if (GlobalSetDate.instance.isFirstInGame)
        {
            GlobalSetDate.instance.isFirstInGame = false;
            
        }
        //获取玩家状态数据
        if (GlobalSetDate.instance.isInFromSave)
        {
            GlobalSetDate.instance.isInFromSave = false;


            //加载地图
            //加载收藏品
            //刷新角色数据
            //print("???     "+ GlobalSetDate.instance.CurrentUserDate.curLive);
            player.GetComponent<RoleDate>().live = float.Parse(GlobalSetDate.instance.CurrentUserDate.curLive);
        }
    }


    void GetPlayBag()
    {
        if (FindObjByName("UI_Bag") == null)
        {
            //加载背包
            GlobalTools.GetGameObjectByName("UI_Bag");
        }
    }



    //生成UI
    void GetPlayerUI()
    {
        GameObject playerUI;
        if (FindObjByName("PlayerUI") == null)
        {
            playerUI = InstancePrefabByName("PlayerUI");
        }
        else
        {
            playerUI = FindObjByName("PlayerUI");
        }
        playerUI.GetComponent<DontDistoryObj>().ShowSelf();
        playerUI.GetComponent<XueTiao>().GetTargetObj(FindObjByName("player"));
    }


    //摄像头定焦 和找到敌人
    void GetTargetPlayer()
    {
        //print("hi");
        this.GetComponent<CameraController>().GetTargetObj(FindObjByName("player").transform);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_ENEMY), null);
    }

}
