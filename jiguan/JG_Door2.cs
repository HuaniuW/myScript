using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_Door2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "open"), this);
        AddDoorListener();
        
    }


    //----------------------------------检测 boss die后的记录 门关闭  判断的 是 boss的 activeSelf

    [Header("没有boss或者精英怪 的时候 是否需要关门 并且组织碰撞关门")]
    public bool IsNoBossNeedCloseDoor = false;
    public GameObject BossOrJY;
    bool IsStopHitJG = false;
    void CheckIsNoBossCloseDoor()
    {
        if (!IsNoBossNeedCloseDoor|| IsStopHitJG) return;
        IsNoBossNeedCloseDoor = false;
        //print(" >>>   "+(BossOrJY == null));
        if (!BossOrJY.activeSelf)
        {
            //print("???????   wokao  guanmena  a  a a a a  ");
            IsStopHitJG = true;
            
            IsCloseDoor = true;
            Door.transform.position = new Vector3(Door.transform.position.x, DownPos.transform.position.y, Door.transform.position.z);
            SetPlayerPos();
        }
    }

    bool IsHasSetPlayerPos = false;
    void SetPlayerPos()
    {
        if (IsHasSetPlayerPos) return;
        IsHasSetPlayerPos = true;
        GameObject player = GlobalTools.FindObjByName("player");
        if (player) player.transform.position = new Vector2(86.82f, 24.1f);
        //print(" ------>>>>>    "+player.transform.position);
        GameObject cm = GlobalTools.FindObjByName("MainCamera");

        cm.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, cm.transform.position.z); 
    }

    //------------------------------------------------------

    [Header("是否自能碰撞一次")]
    public bool IsOnlyCanHitOne = false;

    //-------------------------------------触发关门 同时 触发其他机关的 设定
    public bool IsOtherJG1 = false;
    public BoxCollider2D Kuai;
    bool IsJGOver = false;
    void GetOtherJG1()
    {
        if (!IsOtherJG1) return;
        if (IsJGOver) return;
        IsJGOver = true;
        GameObject cm = GlobalTools.FindObjByName("MainCamera");
        cm.GetComponent<CameraController>().GetBounds(Kuai, true);
        cm.GetComponent<CameraController>().SetNewPosition(new Vector3(cm.transform.position.x, cm.transform.position.y, -14));
    }
    //----------------------------------------------------------------------


    [Header("检测 摄像机 怪组是否有怪 没有的话 开门并且不许关门")]
    public bool IsNeedCheck = true;
    public void AddDoorListener()
    {
        //print("  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ ");
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.OPEN_DOOR, this.GetDoorEvent);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.All_DIE_OPEN_DOOR, this.GetDoorEvent);
        if (IsNeedCheck) GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().CheckGuaiDoor();
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.OPEN_DOOR, this.GetDoorEvent);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.All_DIE_OPEN_DOOR, this.GetDoorEvent);
    }

    void GetDoorEvent(UEvent e)
    {
        //print("  //////////////////////////@@@   "+e.eventParams.ToString());
        if (e.eventParams.ToString() == "allDie") return;


        if(e.eventParams.ToString() == "meiguai")
        {
            //没怪 不允许碰撞关门
            IsCanCloseDoorMap = false;
            //print("iiiiiiii  没怪 不许关门");
        }else if (e.eventParams.ToString() == "kyguanmen")
        {
            IsCanCloseDoorMap = true;
            print(">>>>>>>>>>>>>>>>>>*****  可以关门！ ");
        }
        else if(e.eventParams.ToString() == "open")
        {
            //print(" >>>>>>>...,,,,//////    已经开门了 不要2次关门  "+this.transform.parent.name);
            IsCanCloseDoorMap = false;
            IsCloseDoor = false;
        }
        else
        {
            //print("  wokao!!!! 关门啊 啊啊啊啊啊啊啊啊啊啊啊！！！  ");
            IsCloseDoor = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsNoBossCloseDoor();
        if (IsCloseDoor)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    [Header("控制的门")]
    public GameObject Door;

    public GameObject UpPos;
    public GameObject DownPos;

    [Header("是否是 需要关门的 地图")]
    public bool IsCanCloseDoorMap = false;

    [Header("门移动速度")]
    public float DoorSpeed = 0.4f;


    public bool IsCloseDoor = false;

    void OnTriggerEnter2D(Collider2D Coll)
    {
        //判断 是否是 精英 怪  是的话就关门
        //判断是否有怪物  没有的话 就不关门

        //多个怪的时候 怎么处理？  只要有精英怪就关门 那么 每个小关卡 都有可能 随机出精英怪？
        //print("  ///////////////////我碰到关门机关了  IsCanCloseDoorMap  "+ IsCanCloseDoorMap);
        if (!IsCanCloseDoorMap) return;
        if (IsStopHitJG) return;

        if (IsOnlyCanHitOne) IsStopHitJG = true;

        if (Coll.tag == "Player")
        {
            
            //关门
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "close"), this);
            GetOtherJG1();

            print(" **************  碰撞 关门机关！！！！   ");
        }
    }


    private void OnDisable()
    {
        //print("我被消除了！？？？？？？");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.OPEN_DOOR, this.GetDoorEvent);
    }

    void CloseDoor()
    {
        if (IsCloseDoor && Door.transform.position.y > DownPos.transform.position.y)
        {
            //print("***************  关门！！！！！！**** ");
            Door.transform.position = new Vector3(Door.transform.position.x, Door.transform.position.y - DoorSpeed, +Door.transform.position.z);
        }
    }


    void OpenDoor()
    {
        //print("kaimena !    "+ IsCloseDoor +"  ??  "+ Door.transform.position.y+"   ---uppos "+ UpPos.transform.position.y);
        if (!IsCloseDoor&&Door.transform.position.y < UpPos.transform.position.y)
        {
            Door.transform.position = new Vector3(Door.transform.position.x, Door.transform.position.y+DoorSpeed,+Door.transform.position.z);
        }
    }




    public void DistorySelf()
    {
        StartCoroutine(IEDestoryByEnd(this.gameObject));
    }
    public IEnumerator IEDestoryByEnd(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(obj, true);
    }

}
