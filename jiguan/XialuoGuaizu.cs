using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XialuoGuaizu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GUAI_DIE, this.DieOutDo);
        //GuaiList = GuaiTrans.GetComponentsInChildren;

        if (GuaiTrans != null)
        {
            foreach (Transform child in GuaiTrans)
            {
                string objName = child.gameObject.name;
                print("景是什么 ？ " + objName);
                GuaiList.Add(child.gameObject);
            }
        }
       
    }


    public List<GameObject> GuaiList = new List<GameObject>() { };
    public Transform GuaiTrans;

    //private void OnEnable()
    //{
    //    objList = new List<GameObject> { obj1, obj2, obj3, obj4, obj5, obj6, obj7 };
    //    ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.DIE_OUT, this.DieOutDo);
    //}

    private void OnDisable()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GUAI_DIE, this.DieOutDo);
    }


    void DieOutDo(UEvent e)
    {
        if (GuaiList.Count != 0)
        {
            for(int i= GuaiList.Count-1; i >= 0; i--)
            {
                if(GuaiList[i] == (e.eventParams as GameObject))
                {
                    GuaiList.Remove((e.eventParams as GameObject));
                }
            }
        }

        print("sj "+ GuaiList.Count+"  ---- "+(e.eventParams as GameObject).name+"   obj    "+ e.eventParams);

        if(GuaiList.Count == 0)
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.NEW_OPEN_DOOR,"open"),this);
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "open"), this);
        }

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
                foreach(GameObject o in GuaiList)
                {
                    if (o!=null &&!o.GetComponent<AIBase>().isNearAtkEnemy) o.GetComponent<AIBase>().isNearAtkEnemy = true;
                }
            }
        }
    }

    //void OnTriggerExit2D(Collider2D Coll)
    //{
    //    //print("Trigger - B");
    //    if (Coll.tag == "Player")
    //    {

    //    }
    //}
    //void OnTriggerStay2D(Collider2D Coll)
    //{
    //    //print("Trigger - C");

    //}
}
