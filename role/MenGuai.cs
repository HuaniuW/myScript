using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenGuai : MonoBehaviour {

    // Use this for initialization
    [Header("门记号")]
    public string DoorNum = "";
	void Start () {
		
	}

    bool isOpen = false;
	// Update is called once per frame
	void Update () {
        if (!isOpen && this.GetComponent<RoleDate>().isDie)
        {
            isOpen = true;
            var strArr = DoorNum.Split(',');
            if (strArr.Length > 1)
            {
                for(var i=0; i< strArr.Length; i++)
                {
                    ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, strArr[i]), this);
                }
                return;
            }
            //var s = "sss";
            //print(s.Split(',')[0]);
            //print("men?  "+this.DoorNum);
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, DoorNum), this);
        }
    }
}
