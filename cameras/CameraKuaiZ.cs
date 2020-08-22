using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKuaiZ : MonoBehaviour {
    [Header("碰撞后摄像机的Y距离点")]
    public UnityEngine.Transform cameraY;

    [Header("碰撞后摄像机的Z距离点")]
    public float cameraZ = -13;

    [Header("离开碰撞点后 的Z距离点")]
    public float OutKuaiCameraZ = -13;

    public Vector3 cameraPosition;

    CameraController cm;
    //bool isChangeCam = false;
    // Use this for initialization
    void Start () {
        cm = GameObject.Find("/MainCamera").GetComponent<CameraController>();
    }


    public bool IsSuoDingY = false;
	// Update is called once per frame
	void Update () {
        if (IsSetYGSPlayer)
        {
            if(_player&& IsOutKuai) cm.GetHitCameraKuaiY(_player.transform.position.y + DistanceY, IsSuoDingY);
        }
	}

    [Header("是否控制 摄像机的y")]
    public bool IsSetY = true;

    public bool IsSetYGSPlayer = false;

    [Header("控制 摄像机的y 相对于玩家的距离")]
    public float DistanceY = 2.8f;

    bool IsOutKuai = false;

    GameObject _player;
    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.tag == "Player")
        {
            _player = Coll.gameObject;
            IsOutKuai = true;
            //print("In");
            //print(GlobalTools.FindObjByName("MainCamera"));
            //print(cameraY.transform.position.y);
            //if (isChangeCam) return;
            //isChangeCam = true;
            cameraPosition = GameObject.Find("/MainCamera").transform.position;
            if (IsSetY) {
                cm.GetHitCameraKuaiY(cameraY.transform.position.y);
            }
            
            cm.SetNewPosition(new Vector3(cm.transform.position.x, Coll.transform.position.y + DistanceY, cameraZ));
        }
    }

    void OnTriggerExit2D(Collider2D Coll)
    {

        if (Coll.tag == "Player")
        {
            //print("out");
            cm.GetComponent<CameraController>().OutHitCameraKuaiY();
            //cm.SetNewPosition(cameraPosition);
            cm.SetNewPosition(new Vector3(cameraPosition.x, cameraPosition.y, OutKuaiCameraZ));
            IsOutKuai = false;
            if (IsSuoDingY) cm.JieKaiSuoDingY();
        }
        //print("Trigger - B");
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        //print(Coll.tag);

        //print("Trigger - C");
    }
}
