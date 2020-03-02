using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_MoveDBCIZu : MonoBehaviour
{
    
    [Header("地板数组")]
    public List<GameObject> DibanList;
    //计数 开关到哪个地板了
    int nums = 0;
    [Header("间隔触发时间")]
    public float jiangeshijian = 0.6f;
    TheTimer _theTime;
    const string MOVE_DB_ZU = "move_db_zu";
    // Start is called before the first frame update
    void Start()
    {
        //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.JG_OTHER_EVENT, otherEvent), this);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.JG_OTHER_EVENT, GetJGEvent);
        if (!_theTime) _theTime = GetComponent<TheTimer>();
    }

    private void OnDisable()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.JG_OTHER_EVENT, GetJGEvent);
    }


    void GetJGEvent(UEvent e) {
        if (e.eventParams.ToString() == MOVE_DB_ZU)
        {
            
            _theTime.TimesAdd(1, CallBack2);
            //_theTime.ContinuouslyTimesAdd(8 , 1, CallBack);
        }
    }

    void CallBack2(float n)
    {
        GetStart();
    }


    void GetStart()
    {
        DibanList[nums].GetComponent<JG_MoveDBCi>().ChangeMoveSpeed(0.4f);
        DibanList[nums].GetComponent<JG_MoveDBCi>().GetStart();
        //_theTime.ReSet();
        _theTime.TimesAdd(jiangeshijian, CallBack);
        
    }

    void CallBack(float n)
    {
        nums++;
        print("?????   "+nums+"      "+ DibanList.Count);
        if (nums < DibanList.Count)
        {
            GetStart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
