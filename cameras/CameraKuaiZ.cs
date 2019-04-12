using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKuaiZ : MonoBehaviour {
    [Header("碰撞后摄像机的Y距离点")]
    public UnityEngine.Transform cameraY;

    [Header("碰撞后摄像机的Z距离点")]
    public float cameraZ = -13;

    public Vector3 cameraPosition;

    CameraController cm;
    //bool isChangeCam = false;
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
            //print("In");
            //print(GlobalTools.FindObjByName("MainCamera"));
            //print(cameraY.transform.position.y);
            //if (isChangeCam) return;
            //isChangeCam = true;
            cameraPosition = GameObject.Find("/MainCamera").transform.position;
            cm.GetHitCameraKuaiY(cameraY.transform.position.y);
            cm.SetNewPosition(new Vector3(cm.transform.position.x, Coll.transform.position.y + 2.8f, cameraZ));
        }
    }

    void OnTriggerExit2D(Collider2D Coll)
    {

        if (Coll.tag == "Player")
        {
            //print("out");
            cm.GetComponent<CameraController>().OutHitCameraKuaiY();
            //cm.SetNewPosition(cameraPosition);
            cm.SetNewPosition(new Vector3(cameraPosition.x, cameraPosition.y,-13));
        }
        //print("Trigger - B");
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        //print(Coll.tag);

        //print("Trigger - C");
    }
}
