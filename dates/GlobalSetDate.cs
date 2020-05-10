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
    //是否初次开启游戏
    public bool isFirstInGame = true;

    //是否是从取档进入
    public bool isInFromSave = false;
    //现在的全局玩家数据
    public UserDate CurrentUserDate;


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
    }

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

    public GameMapDate gameMapDate;

    private void Awake()
    {
        //Debug.Log("Start");
        if (Globals.isDebug) print("全局数据GlobalSetDate 启动");
        if (CurrentUserDate == null) CurrentUserDate = new UserDate();
        if (gameMapDate == null) gameMapDate = new GameMapDate();
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

    void InNewGame(string pos = "5.67_1.21",string scrName = "g2_1")
    {
        if (CurrentUserDate == null) CurrentUserDate = new UserDate();
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
        IsChangeScreening = false;
        //print("hi!!!!");
    }

    

    void GetSaveGame()
    {
        playerPosition = CurrentUserDate.playerPosition;
        screenName = CurrentUserDate.screenName;
        cameraPosition = CurrentUserDate.cameraPosition;
    }


    void TempScreenGame()
    {

    }



    Vector3 playerInScreenPosition;
    public string roleDirection = "r";

    public Vector2 GetPlayerInScreenPosition()
    {
        print("doorName    "+ doorName);
        if (doorName != "")
        {
            GameObject door = GlobalTools.FindObjByName(doorName);
            playerInScreenPosition = door.transform.position;
            if (door.transform.localRotation.z < 0)
            {
                roleDirection = "r";
                playerInScreenPosition = new Vector2(door.transform.position.x-1, door.transform.position.y - 1f);
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
            string[] sArray = playerPosition.Split('_');
            //print("-----playerPosition>   " + playerPosition);
            playerInScreenPosition = new Vector3(float.Parse(sArray[0]), float.Parse(sArray[1]));
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
    public string TempZGuanKaStr;
    //启动游戏的时候 调用存档数据先  对比当前关卡数据是否有变动 
    //每关都要对比加到临时数据

    //匹配存档中的全局关卡记录
    public void GetGameAllCunstomStr()
    {
        //获取当前关卡的数据

        //是否有存档
        if (GameSaveDate.GetInstance().IsHasSaveDate())
        {
            //if (Globals.isDebug) print("!!!!!!!!!!!!!!!!!!有存档记录");
            //找到总的关卡记录
            if (GameSaveDate.GetInstance().IsHasSaveDateByName(saveDateName)) {
                //print(GameSaveDate.GetInstance().GetSaveDateByName(CurrentSaveDateName));
                TempZGuanKaStr = GameSaveDate.GetInstance().GetSaveDateByName(saveDateName).mapDate;
                //print("TempZGuanKaStr  "+ TempZGuanKaStr);
            }
            else
            {
                //print("没有当前存档的记录");
            }
            
        }
        else
        {
            if (Globals.isDebug) print("没有存档记录");
			//记录当前关卡的记录  这里一般是新开游戏
			//TempZGuanKaStr = "";
        }
        //获取存档的关卡记录
    }

    public string currentGKDate;
    //获取关卡名的关卡数据 并且在原数据中删除
    public string GetGuanKaStrByGKNameAndRemoveIt(string GKName)
    {
        if (TempZGuanKaStr == null) return null;
        currentGKDate = null;
        //if (Globals.isDebug) print("  GKName " + GKName + "  TempZGuanKaStr " + TempZGuanKaStr);
        // |g1_1:men_1-1,men_2-1|
        string gkStr = "";
        string[] arr = TempZGuanKaStr.Split('|');
        for(var i = 0; i < arr.Length; i++)
        {
            string[] arr2 = arr[i].Split(':');
            string curGKName = arr2[0];
            //if (Globals.isDebug) print("curGKName  "+ curGKName);
            if (curGKName != ""&& curGKName == GKName)
            {
                
                currentGKDate = arr2[1];
                //if (Globals.isDebug) print("当前关卡的数据  "+ currentGKDate);
            }
            else
            {
                if(arr[i]!="") gkStr += arr[i] + '|';
            }
           
        }
        if (Globals.isDebug) print("进场景取数据  "+ gkStr); //gkStr是去掉当前关卡后的数据
        TempZGuanKaStr = gkStr;
        if (Globals.isDebug) print("取完数据后的全局数据  "+ TempZGuanKaStr);
        return currentGKDate;
    }

    //将当前关卡数据 加到总关卡数据中
    public string SetChangeThisGKInZGKTempDate(string GKDate)
    {
        //if (Globals.isDebug) print(" >>>>>>>>>>>>>>>>>>>>>>>>  "+GKDate);
        //if (Globals.isDebug) print(" 当前全局的数据  "+ TempZGuanKaStr);
        TempZGuanKaStr += GKDate+"|";
        //if (Globals.isDebug) print("加完后的全局数据！！！ " + TempZGuanKaStr);
        return TempZGuanKaStr;
    }

    public void SaveDoor()
    {
        if (CurrentUserDate == null) CurrentUserDate = new UserDate();
        CurrentUserDate.mapDate = GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetSaveZGKDate();
        if (Globals.isDebug) print("CurrentUserDate.mapDate    " + CurrentUserDate.mapDate);
        GameSaveDate.GetInstance().SaveDateByURLName(saveDateName, CurrentUserDate);
    }

    public void GetSave()
    {
        //储存玩家所有数据
        print("SAVE.........");
        //if (Globals.isDebug) print("save " + TempZGuanKaStr);
        GameObject player = GlobalTools.FindObjByName("player");
        if (CurrentUserDate == null) CurrentUserDate = new UserDate();
        CurrentUserDate.curLive = player.GetComponent<RoleDate>().live.ToString();
        //print("CurrentUserDate.curLive存档时血量    "+CurrentUserDate.curLive);
        CurrentUserDate.curLan = player.GetComponent<RoleDate>().lan.ToString();
        CurrentUserDate.screenName = SceneManager.GetActiveScene().name;
        if (HowToInGame == TEMP_SCREEN)
        {
            //print("临时进的游戏吗？？？？");
            CurrentUserDate.playerPosition = player.transform.position.x + "_" + player.transform.position.y;
        }
        else
        {
            CurrentUserDate.playerPosition = player.transform.position.x + 2 + "_" + player.transform.position.y;
        }

        //存档时候 技能CD满
        GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().skill_bar.GetComponent<UI_ShowPanel>().AllSkillCDFull();

        CurrentUserDate.cameraPosition = GlobalTools.FindObjByName("MainCamera").transform.position.x + "_" + GlobalTools.FindObjByName("MainCamera").transform.position.y;
        CurrentUserDate.mapDate = GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetSaveZGKDate();
        if(GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1")!=null) CurrentUserDate.bagDate = GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1").GetComponent<Mianban1>().saveDate();
        //血瓶数量
        if(GlobalTools.FindObjByName("PlayerUI(Clone)/xueping")!=null) CurrentUserDate.xp_nums = GlobalTools.FindObjByName("PlayerUI(Clone)/xueping").GetComponent<XuePingBox>().GetXPNums();
        CurrentUserDate.userName = "myGame";
        CurrentUserDate.onlyId = 2;


        //print(CurrentUserDate.curLan);
        //print(CurrentUserDate.screenName);
        //print(CurrentUserDate.playerPosition);
        //print(CurrentUserDate.cameraPosition);
        //print("mapdate---------------------------------------------------------------->   "+CurrentUserDate.mapDate);
        //print(CurrentUserDate.userName);




        GameSaveDate.GetInstance().SaveDateByURLName(saveDateName, CurrentUserDate);
    }

    string screenChangeDate;
    public void ScreenChangeDateRecord()
    {
        RoleDate _roleDate = GlobalTools.FindObjByName("player").GetComponent<RoleDate>();
        screenChangeDate = "cLive=" +_roleDate.live+","+"cL="+_roleDate.lan;
    }

    public void GetScreenChangeDate()
    {
        //GlobalTools.FindObjByName("player")
        if (screenChangeDate == null) return;
        string[] roleDateArr = screenChangeDate.Split(',');
        for(var i = 0; i < roleDateArr.Length; i++) {
            string _date = roleDateArr[i];
            if (_date.Split('=')[0] == "cLive") GlobalTools.FindObjByName("player").GetComponent<RoleDate>().live = float.Parse(_date.Split('=')[1]);
            if (_date.Split('=')[0] == "cLan") GlobalTools.FindObjByName("player").GetComponent<PlayerRoleDate>().Lan = float.Parse(_date.Split('=')[1]);
        }
        screenChangeDate = null;
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
            InNewGame();
            //新游戏的选项
            //GetSave();
        }


        //一键保存 小地图

    }





    //全局 地图 生成

    //大地图信息  由哪些小地图组成
    public List<string> BigMapMsgList = new List<string> { };

    //小地图 信息
    public string MapMsgList = "";

    //当前 跳入 的随机地图名字
    public string CReMapName = "";

    public string CMapTou = "";





    public string DanqianMenweizhi = "";

    public CreateMaps _createMap;
    public void GetMapMsgByName(string mapName,string baoliumenFX,string menweizhi)
    {
        if (!_createMap) _createMap = new CreateMaps();
        _createMap.Test();
        //是否有大地图
        //map_r+map_r-1!0#0!r:map_r-2^u:map_r-3|map_r-2!1#0!r:map_r-4@map_u+map_u-1!0#0!r:map_u-2|map_u-2!1#0!map_u-3


        DanqianMenweizhi = menweizhi;
        //地图头 就是大地图的 头部标识
        CMapTou = mapName.Split('-')[0];
        //判断 如果 没有该大地图 就生成 大地图 并储存
        if(!IsHasBigMapByName(CMapTou, gameMapDate.BigMapDate))
        {

            //这里要 设置 保留门 存入地图数据 存入本地
            string baoliuFX = baoliumenFX;
            //入场的 地图名字
            string ruchangMap = SceneManager.GetActiveScene().name;

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

          
            print(gameMapDate.BigMapDate);
            
           


            //如果大地图中 没有该大地图   生成大地图
            if (gameMapDate.BigMapDate == "")
            {
                gameMapDate.BigMapDate = mapListMsgStr;
            }
            else
            {
                gameMapDate.BigMapDate += "@" + mapListMsgStr;
            }


            print("生成的 地图数据    "+ gameMapDate.BigMapDate);

            //这里要存入 数据
            //return;



        }



        //这里 只需要知道 loading哪个 地图 记录 地图名字   进入场景 后 再生成 地形   --   场景中药有地形 生成代码


        // map_1-1
        //先找 小地图  如果 小地图 没有 说明  没有生成    -->生成大地图  再生成小地图
        if (IsHasMapByName(mapName, gameMapDate.MapDate) != "")
        {
            //根据 存储数据 生成地图
            print("根据地形数据 生成地图");
        }
        else
        {
            print("没有地图数据 生成一地图地形数据 ！进入哪个地图？   "+mapName);
            //生成大地图
            //BigMapMsgList = _createMap.GetMaps();
            //将地图名字 传入 下个 场景 下个场景 来生成地图

        } 


        //生成 大地图 和小地图 都要 保存到本地

    }

    bool IsHasBigMapByName(string mapTouName, string bigMapDateList)
    {
        print("   bigMapDateList>>>  "+ bigMapDateList);
        string[] bigMapArr = bigMapDateList.Split('@');
        for(var i=0;i< bigMapArr.Length; i++)
        {
            if (bigMapArr[i].Split('+')[0] == mapTouName) return true;
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
