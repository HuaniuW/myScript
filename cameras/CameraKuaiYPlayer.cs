using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKuaiYPlayer : MonoBehaviour {
    [Header("与玩家 Y 的距离")]
    public float distanceWithPlayerY = 4;
	// Use this for initialization
	void Start () {
        cm = GameObject.Find("/MainCamera").GetComponent<CameraController>();
    }
    CameraController cm;
    // Update is called once per frame
    void Update () {
        
    }

    void OnTriggerEnter2D(Collider2D Coll)
    {
        
    }

    void OnTriggerExit2D(Collider2D Coll)
    {

        cm.OutHitCameraKuaiY();
        //print("Trigger - B");
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        //print(Coll.tag);

        //print("Trigger - C");

        if (Coll.tag == "Player")
        {
            //print("In");
            //print(GlobalTools.FindObjByName("MainCamera"));
            //print(cameraY.transform.position.y);
            //if (isChangeCam) return;
            //isChangeCam = true;
            cm.GetHitCameraKuaiY(Coll.transform.position.y + distanceWithPlayerY);
        }
    }
}
