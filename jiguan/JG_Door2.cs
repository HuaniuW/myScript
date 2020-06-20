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

    public void AddDoorListener()
    {
        //print("  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ ");
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.OPEN_DOOR, this.GetDoorEvent);
        GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().CheckGuaiDoor();
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.OPEN_DOOR, this.GetDoorEvent);
    }

    void GetDoorEvent(UEvent e)
    {
        //print("  //////////////////////////@@@   "+e.eventParams.ToString());
        if(e.eventParams.ToString() == "meiguai")
        {
            //没怪 不允许碰撞关门
            IsCanCloseDoorMap = false;
            //print("iiiiiiii  没怪 不许关门");
        }else if (e.eventParams.ToString() == "kyguanmen")
        {
            IsCanCloseDoorMap = true;
        }else if(e.eventParams.ToString() == "open")
        {
            //print(" >>>>>>>...,,,,//////    已经开门了 不要2次关门  ");
            IsCanCloseDoorMap = false;
            IsCloseDoor = false;
        }
        else
        {
            //print("  wokao!!!!  ");
            IsCloseDoor = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
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


    bool IsCloseDoor = false;

    void OnTriggerEnter2D(Collider2D Coll)
    {
        //判断 是否是 精英 怪  是的话就关门
        //判断是否有怪物  没有的话 就不关门

        //多个怪的时候 怎么处理？  只要有精英怪就关门 那么 每个小关卡 都有可能 随机出精英怪？
        //print("  ///////////////////我碰到关门机关了  IsCanCloseDoorMap  "+ IsCanCloseDoorMap);
        if (!IsCanCloseDoorMap) return;
        

        if (Coll.tag == "Player")
        {
            //关门
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "close"), this);
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
