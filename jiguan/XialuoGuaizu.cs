using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XialuoGuaizu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        objList = new List<GameObject> { obj1, obj2, obj3, obj4, obj5, obj6, obj7 };
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.DIE_OUT, this.DieOutDo);
    }

    private void OnDisable()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DIE_OUT, this.DieOutDo);
    }

    [Header("控制的门的 数组字符  门开启时候的状态")]
    public string DoorStr = "";

    void DieOutDo(UEvent e)
    {
        foreach (GameObject o in objList)
        {
            if (o != null && !o.GetComponent<RoleDate>().isDie) return;
        }

        string[] DoorArr = DoorStr.Split('|');
        if (DoorArr.Length == 0)return ;

        //开门
        if (!isOpen)
        {
            for (int i = 0;i<DoorArr.Length;i++)
            {
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, DoorArr[i]), this);
            }
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "Men_1-1"), this);
        }
       
    }



    bool isOpen = false;

    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject obj6;
    public GameObject obj7;
    List<GameObject> objList = new List<GameObject> { };

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool IsNeedHitStart = true;

    bool isStartXialuo = false;
    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (!IsNeedHitStart) return;
        if (Coll.tag == "Player")
        {
            if (!isStartXialuo)
            {
                isStartXialuo = true;
                print("敌军出现 开始");
                foreach(GameObject o in objList)
                {
                    if (o!=null &&!o.GetComponent<AIBase>().isNearAtkEnemy) o.GetComponent<AIBase>().isNearAtkEnemy = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D Coll)
    {
        //print("Trigger - B");
        if (Coll.tag == "Player")
        {

        }
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        //print("Trigger - C");

    }
}
