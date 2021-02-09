using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using XLua;

public class GlobalSetDate : MonoBehaviour {
    public static GlobalSetDate instance;
    public const string NEW_GAME = "NewGame";
    public const string LOAD_GAME = "LoadGame";
    public const string CHANGE_SCREEN = "ChangeScreen";
    /// <summary>
    /// 临时场景
    /// </summary>
    public const string TEMP_SCREEN = "TempScreen";
    public string HowToInGame = TEMP_SCREEN;

   

    // Use this for initialization
    static GlobalSetDate()
    {
        //这个好像只要调用就会触发 类同名函数
        if (instance) return;
        GameObject go = new GameObject("GlobalDates");
        DontDestroyOnLoad(go);
        instance = go.AddComponent<GlobalSetDate>();
    }

    public string TempSkillUseRecord = "";


    //是否初次开启游戏
    public bool isFirstInGame = true;

    //是否是从取档进入
    public bool isInFromSave = false;
    //现在的全局玩家数据
    public UserDate CurrentUserDate;

    public UserDate CurrentMapMsgDate;

    //玩家的 选择数据
    public OtherSaveDate OtherDate;


    public bool isGameOver = false;
    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_OVER, this.GameOver);
    }




    void Start()
    {
        //LuaEnv luaenv = new LuaEnv();
        //luaenv.DoString("require 'gameMain'");
        //XLuaTest("lua test  测试");
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_OVER, this.GameOver);
        OtherDate = GetOtherDate();
    }

    //***********************************************other数据获取区*************************************************************
    public OtherSaveDate GetOtherDate()
    {
        print("  获取了 otherdate!! ");
        if(OtherDate == null)
        {
            OtherDate = new OtherSaveDate();
            OtherDate = GameSaveDate.GetInstance().GetOtherSaveDateByName();
        }
        return OtherDate;
    }

    public string GetOtherDateValueByKey(string key)
    {
        string[] otherDateArr = OtherDate.GlobalOtherDate.Split('|');
        List<string> otherDateList = new List<string>(otherDateArr);
        foreach (string str in otherDateList)
        {
            if (str.Split('*')[0] == key) return str.Split('*')[1];
        }
        return "";
    }


    /// <summary>
    /// 存入 或者改变 other 数据
    /// </summary>
    /// <param name="str"></param>
    public void SaveInOtherDate(string str)
    {
        string key = str.Split('*')[0];
        string value = str.Split('*')[1];
        if (GetOtherDateValueByKey(key) != "")
        {
            string _dateStr = "";
            string[] otherDateArr = OtherDate.GlobalOtherDate.Split('|');
            List<string> otherDateList = new List<string>(otherDateArr);

            for (int i = 0; i < otherDateList.Count; i++)
            {
                string strs = otherDateList[i];
                if (strs.Split('*')[0] == key)
                {
                    strs = key + "*" + value;
                }
                _dateStr += "|" + strs;
            }
            OtherDate.GlobalOtherDate = _dateStr;
        }
        else
        {
            OtherDate.GlobalOtherDate += "|" + str;
        }
        GameSaveDate.GetInstance().SaveOtherDateByURLName(OtherDate);
    }

    //************************************************************************************************************


    void XLuaTest(string str)
    {
        print(str);
    }

    GameObject dieScreen;
    void GameOver(UEvent e)
    {
        isGameOver = true;
        if (dieScreen == null) {
            dieScreen = GlobalTools.GetGameObjectByName("DieScreen");
            dieScreen.GetComponent<DieScreen>().StartAC();
            isInFromSave = true;
        }
        
        //GetGameDateStart();
    }

    public void CloseDieScreen()
    {
        if (dieScreen != null)
        {
            dieScreen.GetComponent<DieScreen>().StopAC();
            dieScreen.gameObject.SetActive(false);
            DestroyImmediate(dieScreen.gameObject, true);
        }
    }

    //这个是  用来 存取 自动地图的 数据  不要搞混了
    //public GameMapDate gameMapDate;

    private void Awake()
    {
        //Debug.Log("Start");
        if (Globals.isDebug) print("全局数据GlobalSetDate 启动");
        if (CurrentUserDate == null) CurrentUserDate = new UserDate();
        if (CurrentMapMsgDate == null) CurrentMapMsgDate = new UserDate();
        if (OtherDate == null) OtherDate = new OtherSaveDate();

        //if (gameMapDate == null) gameMapDate = new GameMapDate();
        //CurrentUserDate = new UserDate(); //外部调用居然比启动更快 这里就不要new了 会导致数据消失
        //GetGuanKaStr();

            //Application.targetFrameRate = 60;
            //Time.fixedDeltaTime = 0.033f;
    }
    
    public void GetGameDateStart(bool isSYS = false)
    {
        //获取角色的信息 位置 摄像机位置 背包数据 小地图数据 收集物数据 角色状态
        if (isInFromSave)
        {
            print("读档游戏");
            HowToInGame = LOAD_GAME;
            CurrentUserDate = GameSaveDate.GetInstance().GetSaveDateByName(saveDateName);
            GetSaveGame();
        }
        else
        {
            print("新游戏");
            HowToInGame = NEW_GAME;
            if (isSYS)
            {
                //实验室
                InNewGame("0.59_-0.54","shiyanshi_1");
            }
            else
            {
                InNewGame();
            }
            
        }
        isGameOver = false;
        SceneManager.LoadScene("loads");
    }



    public bool IsScreenNameIsSpeMap()
    {
        string _screenName = screenName;
        if (_screenName.Split('_').Length == 1) return false;
        string zhongjianName = _screenName.Split('_')[1].Split('-')[0];

        return false;
    }


    //第一次启动 掉用这里面存储的数据系统
    public string playerPosition = "6_-2_r";
    public string screenName = "g2_1";
    public string cameraPosition = "";
    public string bagDate = "";//背包数据
    public string mapDate = "";//小地图数据
    public int xp_nums = 0;
    //切换场景的时候不让 控制  MyController.cs
    public bool IsChangeScreening = false;

    public string doorName = "";
    //存档的名字
    public string saveDateName = "myGame";
    //public GameObject player;

    void InNewGame(string pos = "-90.47_-4.46", string scrName = "nga0_1")
    {
        if (CurrentUserDate == null) CurrentUserDate = new UserDate();
        if (CurrentMapMsgDate == null) CurrentMapMsgDate = new UserDate();
        playerPosition = pos; 
        screenName = scrName;
        cameraPosition = "";
        bagDate = "";//背包数据
        mapDate = "";//小地图数据
        xp_nums = 0;//血瓶数量
        CurrentUserDate.screenName = SceneManager.GetActiveScene().name;
        CurrentUserDate.playerPosition = playerPosition;
        CurrentUserDate.cameraPosition = playerPosition;
        CurrentUserDate.mapDate = mapDate;
        CurrentUserDate.bagDate = bagDate;
        //血瓶数量
        CurrentUserDate.xp_nums = xp_nums;
        CurrentUserDate.userName = "myGame";
        CurrentUserDate.onlyId = 2;
        GameSaveDate.GetInstance().SaveDateByURLName(saveDateName, CurrentUserDate);
        GameSaveDate.GetInstance().SaveDateByURLName(saveDateName + "Map", CurrentUserDate);
        IsChangeScreening = false;
        //print("hi!!!!");


        //清除 选择的 other数据
        OtherDate = new OtherSaveDate();
        GameSaveDate.GetInstance().SaveOtherDateByURLName(OtherDate);
    }

    

    void GetSaveGame()
    {
        playerPosition = CurrentUserDate.playerPosition;
        screenName = CurrentUserDate.screenName;
        cameraPosition = CurrentUserDate.cameraPosition;
        //print(" *** screenName" + screenName);
        //print(" *** playerPosition" + playerPosition);
    }



    Vector3 playerInScreenPosition;
    public string roleDirection = "r";

    public Vector2 GetPlayerInScreenPosition()
    {
        print("doorName    "+ doorName);
        if (doorName != "")
        {
            GameObject door = GlobalTools.FindObjByName(doorName);
            print(" ----------->>>   "+door.name);
            playerInScreenPosition = door.transform.position;
            if (door.transform.localRotation.z < 0)
            {
                roleDirection = "r";
                playerInScreenPosition = new Vector2(door.transform.position.x - 1, door.transform.position.y - 1f);
            }
            else
            {
                roleDirection = "l";
                playerInScreenPosition = new Vector2(door.transform.position.x + 1, door.transform.position.y - 1f);
            }
            doorName = "";
        }
        else
        {
            string[] sArray;
            print("  CurrentUserDate.playerPosition  "+ CurrentUserDate.playerPosition);
            if (CurrentUserDate.playerPosition!=null)
            {
                sArray = CurrentUserDate.playerPosition.Split('_');
            }
            else
            {
                sArray = playerPosition.Split('_');
            }
            
            //print("-----playerPosition>   " + playerPosition);
            playerInScreenPosition = new Vector2(float.Parse(sArray[0]), float.Parse(sArray[1]));
            if (sArray.Length > 2) roleDirection = sArray[2];
            //if (Globals.isDebug) print("位置   "+ playerInScreenPosition);
        }



        print("角色的位置   "+ playerInScreenPosition);


        return playerInScreenPosition;
    }


  


    //声音调控
    public float SoundEffect = 0.6f;
    public float GetSoundEffectValue()
    {
        return SoundEffect;
    }
    public void GetUpSoundEffectValue(float v = 0.1f)
    {
        SoundEffect += v;
        SoundEffect = SoundEffect <= 1 ? SoundEffect: 1;
    }
    public void GetDownSoundEffectValue(float v = 0.1f)
    {
        SoundEffect -= v;
        SoundEffect = SoundEffect >= 0 ? SoundEffect : 0;
    }


    string _GuankaStr;
    //关卡记录
    public string GuanKaStr()
    {
        
        return _GuankaStr;
    }

	//当前存档位
	public string CurrentSaveDateName = "UnityUserData";
    //全局总关卡的临时数据
    public string TempZGuanKaStr = "";
    //启动游戏的时候 调用存档数据先  对比当前关卡数据是否有变动 
    //每关都要对比加到临时数据





    //GameObject UI_screenChangeZZ;

    //public void Show_UIZZ()
    //{
    //    if (!UI_screenChangeZZ)
    //    {
    //        UI_screenChangeZZ = GlobalTools.GetGameObjectByName("ZZ_screenChange");
    //    }
    //    UI_screenChangeZZ.GetComponent<UI_screenChangeZZ>().Show_UIZZ();
    //}

    //public void Hide_UIZZ()
    //{
    //    if (!UI_screenChangeZZ)
    //    {
    //        UI_screenChangeZZ = GlobalTools.GetGameObjectByName("ZZ_screenChange");
    //    }
    //    UI_screenChangeZZ.GetComponent<UI_screenChangeZZ>().Hide_UIZZ();
    //}

    //public void ShowLoadProgressNums(float nums, bool isHasChangeScreen = false)
    //{
    //    UI_screenChangeZZ.GetComponent<UI_screenChangeZZ>().ShowLoadProgressNums(nums);
    //}







    public void GetSave()
    {
        //储存玩家所有数据
        print("SAVE.........");
        //if (Globals.isDebug) print("save临时总关卡数据  " + TempZGuanKaStr);
        GameObject player = GlobalTools.FindObjByName("player");
        if (player == null) return;
        if (CurrentUserDate == null) CurrentUserDate = new UserDate();
        CurrentUserDate.curLive = player.GetComponent<RoleDate>().live.ToString();
        print("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@CurrentUserDate.curLive存档时血量    "+CurrentUserDate.curLive);
        CurrentUserDate.curLan = player.GetComponent<RoleDate>().lan.ToString();
        //CurrentUserDate.screenName = SceneManager.GetActiveScene().name;
        CurrentUserDate.screenName = GlobalDateControl.GetCGKName();
        if (HowToInGame == TEMP_SCREEN)
        {
            //print("临时进的游戏吗？？？？");
            CurrentUserDate.playerPosition = player.transform.position.x + "_" + player.transform.position.y;
        }
        else
        {
            CurrentUserDate.playerPosition = player.transform.position.x + 2 + "_" + player.transform.position.y;
        }
        //print("储存的 玩家位置    "+ CurrentUserDate.playerPosition);
        //存档时候 技能CD满
        GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().skill_bar.GetComponent<UI_ShowPanel>().AllSkillCDFull();

        CurrentUserDate.cameraPosition = GlobalTools.FindObjByName("MainCamera").transform.position.x + "_" + GlobalTools.FindObjByName("MainCamera").transform.position.y;
        //CurrentUserDate.mapDate = GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetSaveZGKDate();
        GlobalDateControl.SaveMapDate();
        //print(" @@@@---  CurrentUserDate.mapDate     "+ CurrentUserDate.mapDate);
        if (GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1") != null) CurrentUserDate.bagDate = CurrentMapMsgDate.bagDate;  //GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1").GetComponent<Mianban1>().HZSaveDate();
        //print(" 储存进来的徽章数据   "+ CurrentUserDate.bagDate);
        //血瓶数量
        if(GlobalTools.FindObjByName("PlayerUI(Clone)/xueping")!=null) CurrentUserDate.xp_nums = GlobalTools.FindObjByName("PlayerUI(Clone)/xueping").GetComponent<XuePingBox>().GetXPNums();
        CurrentUserDate.userName = "myGame";
        CurrentUserDate.onlyId = 2;
        ScreenChangeDateRecord();

        //print(CurrentUserDate.curLan);
        //print(CurrentUserDate.screenName);
        //print(CurrentUserDate.playerPosition);
        //print(CurrentUserDate.cameraPosition);
        //print("mapdate---------------------------------------------------------------->   "+CurrentUserDate.mapDate);
        //print(CurrentUserDate.userName);




        GameSaveDate.GetInstance().SaveDateByURLName(saveDateName, CurrentUserDate);

        //print("////////////////////////////////////////////////////////////////////////////////////////////////////////===================================记录完成！！！ ");

    }

    string screenChangeDate;
    public void ScreenChangeDateRecord()
    {
        RoleDate _roleDate = GlobalTools.FindObjByName("player").GetComponent<RoleDate>();
        screenChangeDate = "cLive=" +_roleDate.live+","+"cL="+_roleDate.lan+",cYingzhi="+_roleDate.yingzhi+",cFangyu="+_roleDate.def;
        print("  角色转场前 存储的 角色状态信息  》》》》》》   "+ screenChangeDate);
    }

    public void GetScreenChangeDate()
    {
        print("获取角色 转场信息  ******************************************************************************************     " + screenChangeDate);
        //GlobalTools.FindObjByName("player")
        if (screenChangeDate == null) return;
        string[] roleDateArr = screenChangeDate.Split(',');
        for(var i = 0; i < roleDateArr.Length; i++) {
            string _date = roleDateArr[i];
            if (_date.Split('=')[0] == "cLive") GlobalTools.FindObjByName("player").GetComponent<RoleDate>().live = float.Parse(_date.Split('=')[1]);
            if (_date.Split('=')[0] == "cLan") GlobalTools.FindObjByName("player").GetComponent<PlayerRoleDate>().Lan = float.Parse(_date.Split('=')[1]);
            //print(_date.Split('=')[0]+"   --->   "+ _date.Split('=')[1]);
            if(_date.Split('=')[0] == "cYingzhi") GlobalTools.FindObjByName("player").GetComponent<PlayerRoleDate>().yingzhi = float.Parse(_date.Split('=')[1]);
            if (_date.Split('=')[0] == "cFangyu") GlobalTools.FindObjByName("player").GetComponent<PlayerRoleDate>().def = float.Parse(_date.Split('=')[1]);
        }
        //screenChangeDate = null;
    }

    
	//public void RemoveCurrentGKDateByName(string GKName){
	//	if (TempZGuanKaStr == null) return;
	//	string gkStr = "";
	//	string[] arr = TempZGuanKaStr.Split('|');
	//	for (var i = 0; i < TempZGuanKaStr.Length; i++)
	//	{
	//		string[] arr2 = arr[i].Split(':');
	//		if (arr2[0] == GKName) {
	//			continue;
	//		}
	//		gkStr += arr[i]+'|';
	//	}
 //       TempZGuanKaStr =  gkStr;
	//}


   

    public void DoSomeThings()
    {
        //Debug.Log("DoSomeThings");
    }


    int SpaceNums = 0;
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //音量+
            GetUpSoundEffectValue();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.AUDIO_VALUE, null), this);//   addEventListener(EventTypeName.AUDIO_VALUE, audioValue);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            //音量-
            GetDownSoundEffectValue();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.AUDIO_VALUE, null), this);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR,"3"),this);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //存档测试
            GetSave();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //消除存档数据
            print("---> 清除 存档 数据！");
            InNewGame();
            //新游戏的选项
            //GetSave();

        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            //消除存档数据
            //InNewGame();
            //新游戏的选项
            //GetSave();
            print("储存所有地图数据！！！！");
            if (GlobalTools.FindObjByName("maps"))
            {
                GlobalTools.FindObjByName("maps").GetComponent<GetReMap>().SetMapMsgDateInStr(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            print("清除所有地图数据");
            GameMapDate.ClearMapSaveDate();
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //暂停游戏
            SpaceNums++;
            if (SpaceNums % 2 == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
           
        }

        //一键保存 小地图

    }



























    //-----------------------------------全局 地图 生成

    //大地图信息  由哪些小地图组成
    public List<string> BigMapMsgList = new List<string> { };

    //小地图 信息
    public string MapMsgList = "";

    //当前 跳入 的随机地图名字
    public string CReMapName = "s";
    public bool IsCMapHasCreated = false;


    //当前的map头
    public string CMapTou = "";

    //当前大关卡 数  用来控制 生成地图和怪的标记
    public int DaGuanKaNum = 1;
    //这里可以根据 关卡 头 中间 字母 来判断  大关卡数字    根据数字 来生成 怪物搭配


    public void GetInMenWeizhi(string menWeiZhi)
    {
        DanqianMenweizhi = menWeiZhi;
    }




    public string DanqianMenweizhi = "";
    //public string CRMapName = "";
    public CreateMaps _createMap;
    public void GetMapMsgByName(string mapName,string baoliumenFX,string menweizhi)
    {
        if (!_createMap) _createMap = new CreateMaps();
        //_createMap.Test();
        //是否有大地图
        //map_r+map_r-1!0#0!r:map_r-2^u:map_r-3|map_r-2!1#0!r:map_r-4@map_u+map_u-1!0#0!r:map_u-2|map_u-2!1#0!map_u-3

        //CRMapName = mapName;
        //print("*** mapName " + mapName+ "  baoliumenFX  "+ baoliumenFX+ "  menweizhi "+ menweizhi);

        DanqianMenweizhi = menweizhi;
        //地图头 就是大地图的 头部标识你  eg:  map_r-1  -->  map_r 就是地图 头标记
        CMapTou = mapName.Split('-')[0];

        print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>当前随机地图的 标识     "+ CMapTou);

        //判断 如果 没有该大地图 就生成 大地图 并储存
        if(!IsHasBigMapByName(CMapTou, GameMapDate.BigMapDate))
        {
            print("没有大地图 生成数据");
            //这里要 设置 保留门 存入地图数据 存入本地
            string baoliuFX = baoliumenFX;
            print("进入生成 地图 保留门 的方向   "+ baoliuFX);
            //入场的 地图名字
            string ruchangMap = SceneManager.GetActiveScene().name+"$p";

            print("  这个是保留的场景名字 从这个 场景 进入的随机地图 *** ruchangMap "+ ruchangMap);
            // 设置 地图参数 中间名字  方向几率  最大地图数
            _createMap.SetMapCenterName(CMapTou.Split('_')[1]);
            List<string> TempMapList = _createMap.GetMaps();
            string mapListMsgStr = CMapTou + "+";
            for(var i=0;i< TempMapList.Count; i++)
            {

                if (TempMapList[i].Split('!')[0].Split('-')[1] == "1")
                {
                    if(i== TempMapList.Count - 1)
                    {
                        mapListMsgStr += TempMapList[i] + "^" + baoliuFX + ":" + ruchangMap;
                    }
                    else
                    {
                        mapListMsgStr += TempMapList[i] + "^" + baoliuFX + ":" + ruchangMap+"|";
                    }
                    
                    continue;
                }

                if (i == TempMapList.Count-1)
                {
                    mapListMsgStr += TempMapList[i];
                }
                else
                {
                    mapListMsgStr += TempMapList[i]+"|";
                }
            }

            //生成的 地图数据    map_r+map_r-1!0#0!r:map_r-2^l:mapR_1$p|map_r-2!1#0!l:map_r-1^r:map_r-3|map_r-4!2#1!d:map_r-3|map_r-5!2#-1!u:map_r-3|map_r-3!2#0!l:map_r-2^u:map_r-4^d:map_r-5^r:map_r-6|map_r-7!3#1!d:map_r-6|map_r-8!3#-1!u:map_r-6|map_r-6!3#0!l:map_r-3^u:map_r-7^d:map_r-8^r:map_r-9|map_r-9!4#0!l:map_r-6^u:map_r-10|map_r-10!4#1!d:map_r-9
            //print("生成的 地图数据:   " + gameMapDate.BigMapDate);

            print("生成的地图数据  "+ mapListMsgStr);

            //如果大地图中 没有该大地图   生成大地图
            if (GameMapDate.BigMapDate == "")
            {
                GameMapDate.BigMapDate = mapListMsgStr;
            }
            else
            {
                GameMapDate.BigMapDate += "@" + mapListMsgStr;
            }

            //print("生成的 地图数据    "+ gameMapDate.BigMapDate);

            //这里要存入 数据
            //return;
        }



        //这里 只需要知道 loading哪个 地图 记录 地图名字   进入场景 后 再生成 地形   --   场景中药有地形 生成代码


        // map_1-1
        //先找 小地图  如果 小地图 没有 说明  没有生成    -->生成大地图  再生成小地图
        if (IsHasMapByName(mapName, GameMapDate.MapDate) != "")
        {
            //根据 存储数据 生成地图
            //print("*** 根据地形数据 生成地图");
        }
        else
        {
            //print("*** 没有地图数据 生成一地图地形数据 ！进入哪个地图？   "+mapName);
            //生成大地图
            //BigMapMsgList = _createMap.GetMaps();
            //将地图名字 传入 下个 场景 下个场景 来生成地图

        }

        //print("***  进入了 生成地图判断 ");
        //生成 大地图 和小地图 都要 保存到本地
        screenName = "R@" + screenName;
    }

    bool IsHasBigMapByName(string mapTouName, string bigMapDateList)
    {

        //这里要根据 mapTou 来判断 是否存在该 大地图数据


        print("   bigMapDateList>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  "+ bigMapDateList);
        string[] bigMapArr = bigMapDateList.Split('@');
        for(var i=0;i< bigMapArr.Length; i++)
        {
            if (bigMapArr[i].Split('+')[0] == mapTouName) {
                Globals.mapZBArr = new List<System.String>(bigMapArr[i].Split('+')[1].Split('|'));
                return true;
            }
            
        }
        return false;
    }


    string IsHasMapByName(string mapName,string mapDateList)
    {
        if (mapDateList == "") return "";
        string[] mapArr = mapDateList.Split('@');
        for(var i=0;i< mapArr.Length; i++)
        {
            if (mapArr[i].Split(':')[0] == mapName) return mapArr[i].Split(':')[1];
        }

        //foreach(string str in mapList)
        //{
        //    if (str.Split(':')[0] == mapName)
        //    {
        //        return str;
        //    }
        //}
        return "";
    }

}
