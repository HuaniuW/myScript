using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_NewDoor : MonoBehaviour
{
    [Header("门 上位置点")]
    public Transform UpPos;
    [Header("门 下位置点")]
    public Transform DownPos;
    [Header("门")]
    public Transform Door;

    [Header("门的 移动速度")]
    public float DoorMoveSpeed = 0.4f;

    [Header("门的 声音")]
    public AudioSource DoorSource;

    [Header("是否能 碰撞到机关块 关门")]
    public bool IsCanDoorClose = true;


    // Start is called before the first frame update
    void Start()
    {
        AddEventListeners();
        IsOpenOrClose();
        CheckDoorByDate();
        CheckAllGuaiIsDie();
        ZDDTScreenGuaiCheck();
    }

    GameObject _MainCamera;
    //自动 生成地图 的 门检测
    private void ZDDTScreenGuaiCheck()
    {
        if (BossOrJY!=null) return;
        //print(" oo  自动 工具生成 地图 检测 门 ");
        //throw new NotImplementedException();
        if (!_MainCamera) _MainCamera = GlobalTools.FindObjByName("MainCamera");
        if (_MainCamera.GetComponent<ScreenDoorGuaiControl>().TheMaxGuaiNums == -1) return;
        if (_MainCamera.GetComponent<ScreenDoorGuaiControl>().IsManGuai())
        {
            //print("oo 是否可以 关门  "+ IsCanDoorClose);
            IsCanDoorClose = true;
        }
        else
        {
            IsCanDoorClose = false;
        }
    }

    void AddEventListeners()
    {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.NEW_OPEN_DOOR, this.GetDoorEvent);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GUAI_DIE, this.GuaiListAllDieOpenDoor);
        //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.BOSS_IS_DIE, this.name), this);

        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.BOSS_IS_DIE, this.GuaiListAllDieOpenDoor);

        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.All_DIE_OPEN_DOOR, this.GetDoorEvent);
        //if (IsNeedCheck) GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().CheckGuaiDoor();
    }

    private void OnDisable()
    {
        //print("我被消除了！？？？？？？");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.NEW_OPEN_DOOR, this.GetDoorEvent);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GUAI_DIE, this.GuaiListAllDieOpenDoor);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.BOSS_IS_DIE, this.GuaiListAllDieOpenDoor);
    }

    private void GetDoorEvent(UEvent evt)
    {
        //throw new NotImplementedException();
        String str = evt.eventParams.ToString();
        //print("sj  men test");

        if (str == "open")
        {
            OpenTheDoor();
        }

        if (!IsCanDoorClose) return;
        print(" sj "+ str);
        if(str == "close")
        {
            CloseTheDoor();
        }
    }

    void OpenTheDoor()
    {
        IsCloseing = false;
        IsOpening = true;
        DoorSource.Play();
        if(!IsAllGuaiDieOpenDoorAfterHasJYorBossCanCloseDoor&&BossOrJY!=null) IsCanDoorClose = false;
    }


    void CloseTheDoor()
    {
        IsOpening = false;
        IsCloseing = true;
        DoorSource.Play();
    }



    //----------------------------------检测 boss die后的记录 门关闭  判断的 是 boss的 activeSelf------这里不要乱勾  不知道是哪用到的了

    [Header("**不要乱勾***没有boss或者精英怪 的时候 是否需要关门 并且组织碰撞关门")]
    public bool IsNoBossNeedCloseDoor = false;
    [Header("检测 boss 或者 精英怪")]
    public GameObject BossOrJY;

    //[Header("检测 boss 或者 精英怪 是开启 还是关闭 状态")]
    //public string NoGuaiOpenOrClose = "open";

    void CheckIsNoBossCloseDoor()
    {
        if (!IsNoBossNeedCloseDoor || !IsCanDoorClose) return;
        //print("****************************!!!!");
        IsNoBossNeedCloseDoor = false;
        //print(" >>>   "+(BossOrJY == null));
        if (BossOrJY&&!BossOrJY.activeSelf)
        {
            IsCanDoorClose = false;
            IsDoorClosed = false;
            IsOpenOrClose();
        }
    }





    void OnTriggerEnter2D(Collider2D Coll)
    {
        print(" IsCanDoorClose --------------->   " + IsCanDoorClose);

        if (!IsCanDoorClose) return;
        if (Coll.tag == "Player")
        {
            //关门
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.NEW_OPEN_DOOR, "close"), this);
            //IsCloseing = true;
            print(" **************  碰撞 关门机关！！！！   ");
            
        }
    }



    //读取记录来 判断 是开启 还是 关闭
    void CheckDoorByDate()
    {
        

    }



    //默认 开启 还是关闭
    [Header("默认开启 还是 关闭")]
    public bool IsDoorClosed = false;

    void IsOpenOrClose()
    {
        if (IsDoorClosed)
        {
            Door.transform.position = new Vector2(Door.transform.position.x, DownPos.transform.position.y);
        }
        else
        {
            Door.transform.position = new Vector2(Door.transform.position.x, UpPos.transform.position.y);
        }
    }


    public bool IsOpening = false;
    public bool IsCloseing = false;


    void OpenDoor()
    {
        //向上移动 开启
        if (!IsOpening) return;
        float __y = Door.transform.position.y;
        __y += DoorMoveSpeed;
        if (__y >= UpPos.transform.position.y)
        {
            IsDoorClosed = false;
            IsOpenOrClose();
            IsOpening = false;
            DoorSource.Stop();
            return;
        }

        Door.transform.position = new Vector2(Door.transform.position.x, __y);
    }



    void CloseDoor()
    {
        if (!IsCanDoorClose) return;
        //向下移动 关闭
        if (!IsCloseing) return;

        float __y = Door.transform.position.y;
        __y -= DoorMoveSpeed;
        if (__y <= DownPos.transform.position.y)
        {
            IsDoorClosed = true;
            IsOpenOrClose();
            IsCloseing = false;
            DoorSource.Stop();
            IsCanDoorClose = false;
            print(" IsCanDoorClose?  已经关门！！！！！ ");
            return;
        }

        Door.transform.position = new Vector2(Door.transform.position.x, __y);
    }


    [Header("是否是 全部怪物 die后 开门")]
    public bool IsGuaiListAllDieOpenDoor = false;

    [Header("怪物数组 如果有怪物 则是 怪物全die 开门")]
    public List<GameObject> GuaiList = new List<GameObject>() { };


    void GuaiListAllDieOpenDoor(UEvent e)
    {
        
        if (!IsGuaiListAllDieOpenDoor) return;
        GameObject o = e.eventParams as GameObject;
        //print(" guaidie------------   "+o.name);
        print(e.eventParams);

        //print("guaidie  GuaiList.Count    " + GuaiList.Count);
        for(int i= GuaiList.Count - 1; i >= 0; i--)
        {
            print(GuaiList[i]);
            if(GuaiList[i] == o|| GuaiList[i] == null)
            {
                //print("guaidie   " + o.name);
                GuaiList.RemoveAt(i);
            }
        }
        print("guaidie  Guailist >>>>>>  " + GuaiList.Count);
        if(GuaiList.Count == 0) OpenTheDoor();
    }

    [Header("怪都 清理后 是否还能 关闭 门 还有精英或者boss时候能再次关门")]
    public bool IsAllGuaiDieOpenDoorAfterHasJYorBossCanCloseDoor = false;
    void CheckAllGuaiIsDie()
    {
        if (IsNoBossNeedCloseDoor) return;

        for (int i = GuaiList.Count - 1; i >= 0; i--)
        {
            if(GuaiList[i]!=null) print("  检查 是否 有怪 么怪就 不能关门  "+GuaiList[i]+"    ---  "+ GuaiList[i].activeSelf);
            if (GuaiList[i] == null||GuaiList[i].activeSelf == false)
            {
                GuaiList.RemoveAt(i);
            }
        }

        print("检查 怪物数量   " + GuaiList.Count);

        if (GuaiList.Count == 0) {
            //print(" oo   没有 怪 不许关门！！！ ");
            IsCanDoorClose = false;
            OpenTheDoor();
        }
        
        
    }


    // Update is called once per frame
    void Update()
    {
        //GuaiListAllDieOpenDoor();
        CheckIsNoBossCloseDoor();
        OpenDoor();
        
        CloseDoor();
    }
}
