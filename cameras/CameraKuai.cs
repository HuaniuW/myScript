using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKuai : MonoBehaviour {

    [Header("碰撞后摄像机的Y距离点")]
    public UnityEngine.Transform cameraY;

    CameraController cm;
    // Use this for initialization
    void Start () {
        cm = GameObject.Find("/MainCamera").GetComponent<CameraController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.tag == "Player")
        {
            print("In");
            print(GlobalTools.FindObjByName("MainCamera"));
            //print(cameraY.transform.position.y);
            cm.GetHitCameraKuaiY(cameraY.transform.position.y);
        }
    }

    void OnTriggerExit2D(Collider2D Coll)
    {

        if (Coll.tag == "Player")
        {
            print("?????????????????????????????????????????out");
            cm.OutHitCameraKuaiY();
        }
        //print("Trigger - B");
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        //print(Coll.tag);
       
        //print("Trigger - C");
    }
}
