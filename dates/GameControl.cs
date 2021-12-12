using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using XLua;

//[LuaCallCSharp]
public class GameControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //TimeTest();

    }

    void TimeTest()
    {
        //Application.targetFrameRate = 30;
        //float s = 8 * 365/7*6;
        //float t = 1000000;
        //print("--------------------------------------------->每个月的收入  " + t / s * 8 * 31+"元"+"  每天价值 "+t/s*8+"  每小时价值 "+t/s);

        //float dj = 39*0.7f*0.55f;
        //float zong = 10000000;
        //print("1000万收入 需要卖出多少份   "+zong/dj);

        for (int i=0;i<20;i++)
        {
            float jl = GlobalTools.GetRandomDistanceNums(1000);
            print(i+ "  ----   jl >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>   " + jl);

            if (jl <= 8)
            {
                print(i+   "  zhong!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }
       

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

        Globals.IsInCameraKuai = false;
        //GlobalSetDate.instance.GetGuanKaStr();
        //GlobalSetDate.instance;
        //GlobalSetDate.instance.Init();
        GuankaName = SceneManager.GetActiveScene().name;
        if (IsInPlot) return;
        Globals.IsHitDoorStop = false;
        GetPlayer();
        //print("取到 player!!!");
        if (player == null)
        {
            //print("没有找到主角 游戏中断！");
            return;
        }
        GetPlayerUI();
        GetPlayBag();
        GetCamersTargetToPlayer();

        InitGuanKaDate();
        GetPlayerStatus();
        GetPlayerPosByFX();
        //print("游戏 控制完成");
        
    }

    private void OnEnable()
    {
        //if (Globals.isDebug) print("游戏关卡控制类OnEnable 启动");
        //CheckGuaiDoor();
    }


    public GameObject GetPlayerObj()
    {
        return player;
    }



    //-----------------------------------------------------------------------------门数据---------------
    public List<GameObject> ListDoor = new List<GameObject>() { };

    public void ClearListDoor()
    {
        ListDoor.Clear();
    }


    public void GetDoorInList(GameObject door)
    {
        ListDoor.Add(door);
    }

    //通过方向 来判断 门 和放置玩家的位置
    public void GetPlayerPosByFX()
    {
        print(" 玩家位置 和方向！！   "+ GlobalSetDate.instance.HowToInGame);
        player.GetComponent<GameBody>().IsNeedDieOutDownY = true;
        if (GlobalSetDate.instance.HowToInGame == GlobalSetDate.LOAD_GAME) return;

        SetPlayerPos();

        //print("---------------------  在 GameControl 中控制 玩家位置！！！ ");
    }

    public void TestPlayerDoorPos()
    {
        SetPlayerPos();
    }


    void SetPlayerPos()
    {
        foreach (GameObject door in ListDoor)
        {
            print("  玩家所在门位置---  " + door.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().DangQianMenWeizhi + "   :   " + GetFanFX(GlobalSetDate.instance.DanqianMenweizhi));

            if (door.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().DangQianMenWeizhi == GetFanFX(GlobalSetDate.instance.DanqianMenweizhi))
            {
                print("--------------------------------------------------进入的门信息--------------------------------------------------------------- ");
                //print("储存的当前门 位置  "+ GlobalSetDate.instance.DanqianMenweizhi);
                //print("找到的门 的位置 "+ door.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().DangQianMenWeizhi);


                print("  创建地图时候 玩家的位置 " + player.transform.position);
                print("玩家 出现点位置   " + door.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().OutPosition.position);
                player.transform.position = door.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().OutPosition.position;

                print("玩家位置  " + player.transform.position);

                player.GetComponent<GameBody>().IsNeedDieOutDownY = true;
                SetPlayerFXAndCameraPos();
                return;
            }
        }
    }

    public void SetPlayerFXAndCameraPos()
    {
        //print("------》  "+player.transform.localScale);

        //print("朝向--> "+ GetFanFX(GlobalSetDate.instance.DanqianMenweizhi));
        //string whatYouWanted = str.Substring(0, 1);
        string chaoxiang = GetFanFX(GlobalSetDate.instance.DanqianMenweizhi).Substring(0,1);
        if (chaoxiang == "l" || chaoxiang == "d")
        {
            //print(" 右转！！！！ ");

            player.GetComponent<PlayerGameBody>().TrunFXStrRight();
        }
        else
        {
            //print(" ----》左转！！！！ ");
            player.GetComponent<PlayerGameBody>().TrunFXStrLeft();
        }
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+2.6f, this.transform.position.z);
        
        //print("------222222》  " + player.transform.localScale);
    }

    public string GetFanFX(string fx) {
        if (fx == "l")
        {
            fx = "r";
        }
        else if (fx == "r")
        {
            fx = "l";
        }
        else if (fx == "u")
        {
            fx = "d";
        }
        else if (fx == "d")
        {
            fx = "u";
        }
        else if (fx == "r2") {
            fx = "l2";
        }else if (fx == "l2")
        {
            fx = "r2";
        }
        return fx;
    }


    //---------------------------------------------------------------------------------------------------





    //-----------------------------------------------------------------------------关卡数据的存储---------------------------------------------------------

    [Header("当前关卡名字")]
    public string GuankaName = "";

    [Header("记录关卡的动态数据")]
    public string GuanKaStr = "";
    
    //string TempCurrentGKDate;
    //初始化关卡数据
    public void InitGuanKaDate()
    {
        print(" 初始化  关卡数据！！！！！！  ");
        //进游戏 才能调用 
        //GlobalSetDate.instance.GetGameAllCunstomStr();
        GlobalDateControl.GetGameAllCunstomStr();
        //查找存档中是否有本关卡的关卡记录  通过本关卡名字获取本关卡数据
        string TempCurrentGKDate = GlobalDateControl.GetCurrentGKDate();  //GlobalSetDate.instance.GetGuanKaStrByGKNameAndRemoveIt(SceneManager.GetActiveScene().name);
        //print("关于本关卡的关卡记录>>   " + TempCurrentGKDate);
        if (TempCurrentGKDate == null) {
            //print(" 记录 null   "+ TempCurrentGKDate);
            TempCurrentGKDate = ""; //GuanKaStr;
        }
        else
        {
            //print("  有关卡记录  草！！！！！！！  ");
            //如果全局数据中有本关卡数据  清除掉原数据中本关卡数据   匹配本关卡数据
            if (GlobalDateControl.GetCGKName().Split('@').Length == 1) GetPiPei();
            //GetPiPei();
        }
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_SCREEN, SetChangeThisGKInZGKTempDate);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.RECORDOBJ_CHANGE, GKDateChange);
    }


    //这个和自动地图区别开 防止混合调用了
    public bool IsCanBeCheckGuai = true;

    
    public List<GameObject> GuaiList = new List<GameObject> { };
    public void CheckGuaiDoor()
    {
        if (!IsCanBeCheckGuai) return;
        GameObject maps = GlobalTools.FindObjByName("maps");
        //print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>****************************@@@@@");
        if (maps&& maps.GetComponent<GetReMap2>()) {
            if (maps.GetComponent<GetReMap2>().GuaiList.Count != 0) GuaiList = maps.GetComponent<GetReMap2>().GuaiList;
            if (maps.GetComponent<GetReMap2>().GetGuanKaType() == GlobalMapDate.BOSS_PINGDI ||
                maps.GetComponent<GetReMap2>().GetGuanKaType() == GlobalMapDate.JINGYING_PINGDI) {
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "kyguanmen"), this);
            }
        } 
        if (GuaiList.Count == 0)
        {
            //开门
            //print("、、、、、、、、、、、、、、、、、、、、 开门  ");
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "meiguai"), this);
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "open"), this);
        }
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
        print("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% 关卡数据匹配 当前 关卡名  "+ GlobalDateControl.GetCurrentGKDate().Split(':')[0]);
        string changeDate = e.eventParams.ToString();//Men_1-1
        //print(" GKDateChange 事件  "+ changeDate);
        string changeDateName = changeDate.Split('-')[0];//men_1
        string type = changeDateName.Split('_')[0];//men
        string changezt;
        if (changeDate.Split('-').Length>1) changezt = changeDate.Split('-')[1];

        string TempCurrentGKDate = GlobalDateControl.GetCurrentGKDate().Split(':')[1];
        print("临时 关卡 数据 ：   " + TempCurrentGKDate);
        if (TempCurrentGKDate == "") {
            TempCurrentGKDate = changeDate;
            print("临时 关卡 数据 没有数据的时候  TempCurrentGKDate>>>> ： " + TempCurrentGKDate);
        }
        else
        {
            //print(" >>????? TempCurrentGKDate     "+ TempCurrentGKDate+ "  ??changeDate   "+ changeDate);
            //men_1-0,men_2-0,boss_1-0 当前关卡数据长这样
            string[] currentGKDateArr = TempCurrentGKDate.Split(',');
            //先查找 关卡数据中是否有该数据 有的就变化状态 没有的话就在后面加
            if (!GlobalTools.IsHasDate(changeDate, currentGKDateArr))
            {
                TempCurrentGKDate += "," + changeDate;
                print(" 如果 原始数据 没有新数据 直接 加载后面? 生成数据是：    "+ TempCurrentGKDate+"  /////// 新数据：   "+ changeDate);
                //return;
            }
            else
            {
                print("如果 变化数据 原始数据中有的 话 进入这里 开始 更新 数据状态 @@@@@@");
                string newGKDate = "";
                for (var i = 0; i < currentGKDateArr.Length; i++)
                {
                    string theGKDate = currentGKDateArr[i];
                    string dateName = theGKDate.Split('-')[0];
                    //if (Globals.isDebug) print("theGKDate  >  " + theGKDate);
                    //string zt = theGKDate.Split('-')[1];
                    //这里 替换了 名字相同的内容  也就是替换了状态
                    if (dateName == changeDateName)
                    {
                        if (i != currentGKDateArr.Length - 1)
                        {
                            newGKDate += changeDate + ",";
                        }
                        else
                        {
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
                print("  改变状态后的 数据 TempCurrentGKDate   "+ TempCurrentGKDate);
            }
        }



        //string TempGKDateStr = GuankaName + ":" + TempCurrentGKDate;
        string TempGKDateStr = GlobalDateControl.GetCGKName() + ":" + TempCurrentGKDate;

        if (TempCurrentGKDate != null) GlobalDateControl.SetCurrentGKDateInZGKTempDate(TempGKDateStr);
        print("记录数据   TempCurrentGKDate        " + TempGKDateStr);
        //print("TempCurrentGKDate:    " + TempCurrentGKDate);
        if (type == "boss"||type == "G"|| type == "B"|| type == "WP")
        {
            //将关卡数据写入全局临时数据 存档
            GlobalDateControl.SaveMapDate();
        }
    }
    //将本关卡存入全局临时数据
    public void SetChangeThisGKInZGKTempDate(UEvent e)
    {
        //将关卡数据写入全局临时数据
        
        //if (TempCurrentGKDate != null) GlobalSetDate.instance.SetChangeThisGKInZGKTempDate(GuankaName+":"+TempCurrentGKDate);
        //print(" 将临时数据 写入 全局变量：  " + GlobalSetDate.instance.TempZGuanKaStr);
    }

  

    //匹配记录的机关状态 门是开的还是关的 BOSS有没有杀掉
    public void GetPiPei()
    {
        string TempCurrentGKDate = GlobalDateControl.GetCurrentGKDate().Split(':')[1];
        print("*****************************************************************开始进行 当前关卡的数据匹配 TempCurrentGKDate:  " + TempCurrentGKDate);
        if (TempCurrentGKDate == "") return;
        //开始匹配关卡数据
        string[] strArr = TempCurrentGKDate.Split(',');
        //if (Globals.isDebug) print("匹配！！TempCurrentGKDate：   "+ TempCurrentGKDate);
        //print(" 匹配关卡记录数据------------------------ >  "+ strArr);
        for (var i = 0; i < strArr.Length; i++)
        {
            if (strArr[i] == "") continue;
            string s = strArr[i].Split('-')[0];
            s = GlobalTools.GetNewStrQuDiaoClone(s);
            //s = GlobalTools.GetNewStrQuDiaoKuohao(s);
            string zt = "0";
            if (strArr[i].Split('-').Length>1) zt = strArr[i].Split('-')[1].Split('@')[0];

            string sName = s.Split('_')[0];

            //print("字母头 sName：    " + sName+" zt  "+zt);


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
            else if (sName == "BOSS"|| sName == "B")
            {
                print("匹配到 boss名字 :   "+s);
                GameObject boss = GlobalTools.FindObjByName(s);
                if (boss != null) {
                    print(">>>进来没！！！");
                    boss.SetActive(false);
                    DestroyImmediate(boss, true);
                }
                
            } else if (sName == "WP") {
                //GlobalTools.FindObjByName(s).SetActive(false);
                if (zt == "1")
                {
                    print("s   ------>   "+s);
                    //生成一个
                    GameObject o =  GlobalTools.GetGameObjectByName(s);
                    if (o == null) return;
                    string posStr = strArr[i].Split('@')[1];
                    Vector2 pos = new Vector2(float.Parse(posStr.Split('#')[0]), float.Parse(posStr.Split('#')[1]));
                    o.transform.position = pos;
                }
                else
                {
                    //状态=0 的时候 删除
                    if(GlobalTools.FindObjByName(s) != null)GlobalTools.FindObjByName(s).GetComponent<Wupinlan>().DistorySelf();
                }
              
            }else if (sName == "G")
            {

                GameObject guai;
                if (GlobalDateControl.GetCGKName().Split('@').Length!=1)
                {
                    print(">>1");
                    guai = GlobalTools.FindObjByNameInRMaps(s);
                }
                else
                {
                    print(">>2");
                    //非生成地图
                    guai = GlobalTools.FindObjByName(s);
                }


                if(guai == null)
                {
                    print("******************************怪物名字    "+s);
                    guai = GlobalTools.FindObjByNameInGuais(s);
                    GlobalTools.FindObjByName("MainCamera").GetComponent<ScreenDoorGuaiControl>().TheGuaiList.Remove(guai);
                    if(guai)guai.SetActive(false);
                    return;
                }


                //要在 maps 里面找
                print("  >////////////////////guai sname   " + s+"   是否匹配到怪  "+ guai);
                if (GuaiList.Count!=0 && guai != null) {
                    GuaiList.Remove(guai);
                    if (GlobalTools.FindObjByName("maps") && GlobalTools.FindObjByName("maps").GetComponent<GetReMap2>().GuaiList.Remove(guai)) {
                        guai.SetActive(false);
                    }
                    else
                    {
                        guai.SetActive(false);
                    }
                    
                    CheckGuaiDoor();
                }
                else
                {
                    guai.SetActive(false); 
                }
            }
            else if (sName == "JG")
            {
                //机关记录
                //JG_screenName-nums(数组位置)
                string ScreenName = strArr[i].Split('_')[1]+"_"+strArr[i].Split('_')[2].Split('-')[0];
                //print("ScreenName   "+ ScreenName);
                if(ScreenName == SceneManager.GetActiveScene().name)
                {
                    JGNum = int.Parse(strArr[i].Split('-')[1]);
                    //print("----------------------------  匹配！！！");
                }
            }
            else
            {
                print("其他记录    "+s);
                if (GlobalTools.FindObjByName(s) != null) GlobalTools.FindObjByName(s).SetActive(false);
            }
        }
    }

    public int JGNum = 0;
    //-------------------------------------------------------------------------------------------------------------------------------------------------







    bool IsTetsPos = false;

    // Update is called once per frame
    void Update () {
        //print(GlobalTools.GetRandomNum(1)+"   ----  "+ GlobalTools.GetRandomNum(2)+"   -  "+ GlobalTools.GetRandomNum(0));

        //print(player.transform.position);
        //if(player) player.GetComponent<GameBody>().TurnRight();
        //if (!IsTetsPos)
        //{
        //    IsTetsPos = true;
        //    print("摄像机--------");
        //    print("摄像机  :::::::::  " + this.transform.position);
        //    this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 15.6f, this.transform.position.z);
        //}


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
            //print("   获取玩家信息1  没有角色 生成玩家player  ");
            player = InstancePrefabByName("player");
        }
        else
        {
            //print("   获取玩家信息2  有角色   @@@@@@@@@@@@@@@@@@ 直接调用player  ");
            player = GlobalTools.FindObjByName("player");
        }
        //player.transform.position = new Vector2(0,0);
        player.GetComponent<GameBody>().IsNeedDieOutDownY = false;
        if (player!=null) player.GetComponent<PlayerRoleDate>().GetStart();
    }

    

    void GetPlayerStatus()
    {
        //print("玩家状态******************************************************************************************************************************》》》》》》》》       ");
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
            if(GlobalSetDate.instance.CurrentUserDate.curLive!=null) player.GetComponent<RoleDate>().live = float.Parse(GlobalSetDate.instance.CurrentUserDate.curLive);
        }
        else if (GlobalSetDate.instance.HowToInGame == GlobalSetDate.CHANGE_SCREEN)
        {
            if (Globals.isDebug) print("转场进入游戏");
            //背包更新

            //player.transform.position = GlobalSetDate.instance.GetPlayerInScreenPosition();
            //指定当前数值
            //GlobalSetDate.instance.GetScreenChangeDate();
        } else if (GlobalSetDate.instance.HowToInGame == GlobalSetDate.TEMP_SCREEN) {
            if (Globals.isDebug) print("临时 直接重场景进入的游戏");
            GlobalSetDate.instance.GetSave();
            GlobalSetDate.instance.HowToInGame = GlobalSetDate.LOAD_GAME;
        }
        
        //摄像机位置跟随
        GameObject mainCamera = GlobalTools.FindObjByName("MainCamera");
        if (mainCamera) mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3.5f, mainCamera.transform.position.z);
        //玩家站立
        player.GetComponent<GameBody>().SetV0();
        //当前是什么状态 新进游戏（不管是取档 还是新游戏）
        print("玩家的 血量 是多少    "+player.GetComponent<RoleDate>().live+"   玩家的 最大血量是多少   "+ player.GetComponent<RoleDate>().maxLive);
        print(" 玩家的 数据CurrentUserDate   " + GlobalSetDate.instance.CurrentUserDate);
        print(" CurrentMapMsgDate 数据     " + GlobalSetDate.instance.CurrentMapMsgDate);
        //GlobalSetDate.instance.GetScreenChangeDate();
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
        //print(">>>>>GetPlayerUI  ");
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
        playerUI.GetComponent<UI_lantiao>().GetTargetObj(GlobalTools.FindObjByName("player"));
        //GlobalTools.FindObjByName("player").GetComponent<RoleDate>().Lan = float.Parse(GlobalSetDate.instance.CurrentUserDate.curLan);
        //playerUI.GetComponent<PlayerUI>().ui_hun.GetComponent<UI_Hun>().SetHun();
        //print("*******************************************************************************************************************************************************徽章更新***");
        //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_HZ, ""), this);
    }


    //摄像头定焦 和找到敌人
    public void GetCamersTargetToPlayer()
    {
        //print("hi");
        this.GetComponent<CameraController>().GetTargetObj(GlobalTools.FindObjByName("player").transform);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_ENEMY), null);
        //print("摄像机  kaishi zuobiao  "+ this.transform.position);
        //this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 15.6f, this.transform.position.z);
        //print(" 控制 摄像机 位置！！！！！！！！ ");
        //print("摄像机  houlai------ zuobiao  " + this.transform.position);
    }


  

}
