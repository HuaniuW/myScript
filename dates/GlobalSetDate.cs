using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSetDate : MonoBehaviour {
    public static GlobalSetDate instance;
    
    // Use this for initialization
    static GlobalSetDate()
    {
        //这个好像只要调用就会触发 类同名函数
        if (instance) return;
        GameObject go = new GameObject("GlobalDates");
        DontDestroyOnLoad(go);
        instance = go.AddComponent<GlobalSetDate>();
        //print("?????");
    }

    private void OnDestroy()
    {
        //print("??????????????????销毁了？");
    }

    public string playerPosition = "-9.6_-1.9";
    public string screenName = "";
    public string cameraPosition = "";
    public bool IsChangeScreening = false;

    Vector3 playerInScreenPosition;

    public Vector3 GetPlayerInScreenPosition()
    {
        string[] sArray = playerPosition.Split('_');
        playerInScreenPosition = new Vector3(float.Parse(sArray[0]), float.Parse(sArray[1]), 0);
        //print("位置   "+ playerInScreenPosition);
        return playerInScreenPosition;
    }


    //声音调控
    float SoundEffect = 1f;
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

    //匹配存档中的关卡记录
    public void GetGuanKaStr()
    {
        //获取当前关卡的数据

        //是否有存档
        if (GameSaveDate.GetInstance().IsHasSaveDate())
        {
            if (Globals.isDebug) print("!!!!!!!!!!!!!!!!!!有存档记录");
            //找到总的关卡记录
            if (GameSaveDate.GetInstance().IsHasSaveDateByName(CurrentSaveDateName)) {
                //print(GameSaveDate.GetInstance().GetSaveDateByName(CurrentSaveDateName));
                TempZGuanKaStr = GameSaveDate.GetInstance().GetSaveDateByName(CurrentSaveDateName).guankajilu;
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
        if (Globals.isDebug) print("  GKName " + GKName+ "  TempZGuanKaStr " + TempZGuanKaStr);
        string gkStr = "";
        string[] arr = TempZGuanKaStr.Split('|');
        for(var i = 0; i < arr.Length; i++)
        {
            string[] arr2 = arr[i].Split(':');
            string curGKName = arr2[0];
            if (Globals.isDebug) print("curGKName  "+ curGKName);
            if (curGKName != ""&& curGKName == GKName)
            {
                
                currentGKDate = arr2[1];
                if (Globals.isDebug) print("当前关卡的数据  "+ currentGKDate);
            }
            else
            {
                if(arr[i]!="") gkStr += arr[i] + '|';
            }
           
        }
        //print("进场景取数据  "+ gkStr);
        TempZGuanKaStr = gkStr;
        //print("取完数据后的全局数据  "+ TempZGuanKaStr);
        return currentGKDate;
    }

    //将当前关卡数据 加到总关卡数据中
    public void SetChangeThisGKInZGKTempDate(string GKDate)
    {
        if (Globals.isDebug) print(" >>>>>>>>>>>>>>>>>>>>>>>>  "+GKDate);
        if (Globals.isDebug) print(" 当前全局的数据  "+ TempZGuanKaStr);
        TempZGuanKaStr += GKDate+"|";
        if (Globals.isDebug) print("加完后的全局数据！！！ " + TempZGuanKaStr);
    }

    public void GetSave()
    {
        //储存玩家所有数据
        if (Globals.isDebug) print("save " + TempZGuanKaStr);
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


    void Start()
    {
        //Debug.Log("Start");
        if (Globals.isDebug) print("全局数据GlobalSetDate 启动");
        GetGuanKaStr();
    }

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
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            //音量-
            GetDownSoundEffectValue();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR,"3"),this);
        }

    }
}
