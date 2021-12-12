using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDoorGuaiControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AddEvent();
    }

    private void AddEvent()
    {
        //throw new NotImplementedException();
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.ALLDIE_OPEN_DOOR, this.IsAllDieCheck);
    }

    private void IsAllDieCheck(UEvent evt)
    {
        //throw new NotImplementedException();
        print("  guaidie -------> "+ GlobalTools.FindObjByName("MainCamera").GetComponent<ScreenDoorGuaiControl>().TheGuaiList.Count);


        if (TheGuaiList.Count == 0)
        {
            //开门
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.NEW_OPEN_DOOR, "open"), this);

            //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.NEW_OPEN_DOOR, this.GetDoorEvent);
        }
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.ALLDIE_OPEN_DOOR, this.IsAllDieCheck);
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    
    

    [Header("是否能关门")]
    public bool IsCanCloseDoor = true;

    [Header("记录 怪物 最大数量 少于不能自动关门 ")]
    public int TheMaxGuaiNums = -1;

    //对比是否是满怪 满怪的话 可以关门
    public bool IsManGuai()
    {
        if (!IsCanCloseDoor) return false;
        if (TheGuaiList.Count == TheMaxGuaiNums) return true;
        return false;
    }

  
    public List<GameObject> TheGuaiList = new List<GameObject> { };

    //public static int MaxGuaiNums = -1;
    //public static List<GameObject> GuaiList = new List<GameObject> { };

    public static List<GameObject> DBList = new List<GameObject> { };

    [Header("出现机关 几率")]
    public static int IsHasJGJL = 0;

    public static float LeftPosX = 0;
    public static float LeftPosY = 0;
    public static float RightPosX = 0;

    public static bool IsOneColorRandom { get; set; }
    public static Color LightColor { get; set; }
    public static bool IsColorRandom { get; set; }
}
