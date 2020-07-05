using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_GuaiAllDieOpen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.OPEN_DOOR, this.GetDoorEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.OPEN_DOOR, this.GetDoorEvent);
    }

    void GetDoorEvent(UEvent e)
    {
        ChackGuaiNums();
    }


    public List<GameObject> GuaiList = new List<GameObject>() { };

    bool IsOpenDoor = false;

    void ChackGuaiNums()
    {
        
        foreach (GameObject o in GuaiList)
        {
            if(o!=null && !o.GetComponent<RoleDate>().isDie)return;
        }
        print("  ----------die all   "+ IsOpenDoor);
        if (IsOpenDoor) return;
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.All_DIE_OPEN_DOOR, "open"), this);
        IsOpenDoor = true;
    }

}
