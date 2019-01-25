using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [Header("移动到的y位置的标点")]
    public UnityEngine.Transform moveToY;
    public GameObject men;

    [Header("开门声音")]
    public AudioSource openDoor;

    // Use this for initialization
    void Start () {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.OPEN_DOOR, OpenDoor);
	}

    bool isOpen = false;
    public void OpenDoor(UEvent uEvent)
    {
        isOpen = true;
        if (openDoor) openDoor.Play();
    }
	// Update is called once per frame
	void Update () {
        if (isOpen)
        {
            if (men&&men.transform.position.y>moveToY.transform.position.y)
            {
                float moveY = men.transform.position.y - 0.1f;
                men.transform.position = new Vector3(men.transform.position.x,moveY, men.transform.position.z);
            }
            else
            {
                isOpen = false;
                if (openDoor) openDoor.Stop();
            }
        }
	}
}
