using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDiban : MonoBehaviour {
    [Header("目标点1")]
    public UnityEngine.Transform d1;
    [Header("目标点2")]
    public UnityEngine.Transform d2;

    [Header("移动的地板")]
    public GameObject diban;

    [Header("是否优先上或者左移动")]
    public bool IsFristUpOrLeft = true;

    [Header("移动速度")]
    public float mspeed = 0.1f;

    [Header("是否来回移动")]
    public bool IsMoveBack = true;

    [Header("是否Y移动")]
    public bool IsMoveY = true;

    // Use this for initialization
    void Start () {
        if (!IsFristUpOrLeft) mspeed *= -1;
        lastSpeedm = mspeed;

    }

    [Header("停顿时间差 用于错开各个地板移动时间")]
    public float stopTime = 0;
	
	// Update is called once per frame
	void Update () {
        if (!IsStopOver()) return;
        if (!diban) return;
        if (!IsMoveY)
        {
            MoveX();
            return;
        }
        MoveY();

    }

    bool IsGetTop = false;
    //停顿计时
    float stopTiming = 0;
    bool IsStopOver()
    {
        if (stopTime == 0) return true;
        if (IsGetTop)
        {
            stopTiming += Time.deltaTime;
            //print(" stopTiming  "+ stopTiming);
            if(stopTiming>= stopTime)
            {
                IsGetTop = false;
                stopTiming = 0;
                return true;
            }
            return false;
        }
        return true;
    }

    float lastSpeedm = 1;
    private void MoveY()
    {
        float moveY = diban.transform.position.y + mspeed;
        diban.transform.position = new Vector3(diban.transform.position.x, moveY, diban.transform.position.z);
        var cubeF = GameObject.Find("/MainCamera");
        foreach (Transform t in objList)
        {
            //print(t.tag);
            if(t.tag == "Player")
            {
                
                //print("cccc   "+cubeF);
                if (cubeF.GetComponent<CameraController>().IsOutY)
                {
                    //print("hiiiiiii");
                    //cubeF.GetComponent<CameraController>().IsOutY2 = true;
                    float cmy = cubeF.transform.position.y + mspeed;
                    cubeF.transform.position = new Vector3(cubeF.transform.position.x, cmy, cubeF.transform.position.z);
                }
                else
                {
                    //cubeF.GetComponent<CameraController>().IsOutY2 = false;
                }
                
            }
            float cy = t.transform.position.y + mspeed;
            t.transform.position = new Vector3(t.transform.position.x,cy, t.transform.position.z);
        }

        if (diban.transform.position.y >= d1.position.y || diban.transform.position.y <= d2.position.y) {
            mspeed *= -1;
            if(lastSpeedm!= mspeed)
            {
                IsGetTop = true;
            }
            lastSpeedm = mspeed;
        }
        
    }

    private void MoveX()
    {
        float moveX = diban.transform.position.x + mspeed;
        diban.transform.position = new Vector3( moveX, diban.transform.position.y, diban.transform.position.z);
        foreach (Transform t in objList)
        {
            if (t == null || t.transform == null) return;
            float cx = t.transform.position.x + mspeed;
            t.transform.position = new Vector3(cx, t.transform.position.y, t.transform.position.z);
        }

        //print(objList.Count);

        if (diban.transform.position.x <= d1.position.x || diban.transform.position.x >= d2.position.x) {
            mspeed *= -1;
            if (lastSpeedm != mspeed)
            {
                IsGetTop = true;
            }
            lastSpeedm = mspeed;
        }
       
    }

    //Transform obj2;

    Transform[] tarr = { };
    List<Transform> objList = new List<Transform>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print("Trigger - A");
        //obj2 = collision.collider.transform;
        if(!objList.Contains(collision.collider.transform)) objList.Add(collision.collider.transform);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //print("Trigger - B");
        if (objList.Contains(collision.collider.transform)) objList.Remove(collision.collider.transform);
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        //print("Trigger - C  " + collision.collider.name);
        //float my = collision.collider.transform.position.x + mspeed;
        //collision.collider.transform.position = new Vector3(my, collision.collider.transform.position.y, collision.collider.transform.position.z);
        //collision.collider.transform.position = new Vector3(collision.collider.transform.position.x, my, collision.collider.transform.position.z);
        
    }
}
