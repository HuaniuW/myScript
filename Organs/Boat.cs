using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {

    [Header("水平速度")]
    public float speedX;

    Rigidbody2D playerRigidbody2D;
    Vector3 boatSpeed;
    float speedY = 0.002f;
    bool isGo = false;
    bool isStop = false;
    bool isPlayerOnBoat = false;
    
    // Use this for initialization
    void Start () {
        boatSpeed = new Vector3(1, 0, 0);
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        //var cubeF = GameObject.Find("/MainCamera");
        //print(cubeF);

    }

    bool isChangeCam = false;
    private void Move()
    {
       

        if (isStop) return;
        if (!isGo) return;
        
        boatSpeed.y -= speedY;
        if (boatSpeed.y >= 0.06)
        {
            speedY *= -1;
        }
        else if(boatSpeed.y <= -0.06)
        {
            speedY *= -1;
        }
        //print(boatSpeed.y);
        playerRigidbody2D.velocity = boatSpeed;

        MoveCamera();
    }

    void MoveCamera()
    {
        if (transform.position.x > 10)
        {
            //2.37 -13.33
            if (isChangeCam) return;
            var cm = GameObject.Find("/MainCamera").GetComponent<CameraController>();
            cm.SetNewPosition(new Vector3(cm.transform.position.x, player.transform.position.y + 2.8f, -13.33f));
            isChangeCam = true;
        }
    }

    // Update is called once per frame
    void Update () {
        Move();

        if (Globals.isInPlot &&isDaoAn)
        {
            if (player.GetComponent<PlotControl>().GetStand())
            {
                //print(">>?????");
                Globals.isInPlot = false;
            }
        }

        if (!isGo&&!isDaoAn &&Globals.isInPlot)
        {
            if (player.GetComponent<PlotControl>().GetToPosition(this.transform.position.x+0.5f))
            {
                if (player.GetComponent<PlotControl>().TurnRight())
                {
                    if (player.GetComponent<PlotControl>().GetSit())
                    {
                        isGo = true;
                    }
                    
                }
               
            }
        }


        Controls();
	}

    private void Controls()
    {
        if (!isPlayerOnBoat||isDaoAn) return;
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //isPlayerOnBoat = true;
            //print(">>>????????????????");
            Globals.isInPlot = true;
            //剧情模式的行动 走到 指定位置 坐下 开船  到点起来

            //isGo = true;
        }
    }

    bool isDaoAn = false;
    void OnTriggerEnter2D(Collider2D Coll)
    {
        //print(Coll.transform.name);
        if (Coll.gameObject.name == "CJ1_youan")
        {
            isDaoAn = true;
            isGo = false;
            isStop = true;
            //Globals.isInPlot = false;
            playerRigidbody2D.velocity = Vector3.zero;
            return;
        }
        
    }

    GameObject player;

    void OnCollisionEnter2D(Collision2D Coll)
    {
        if (isDaoAn) return;
        if (Coll.gameObject.name == "player")
        {
            //isGo = true;
            //Globals.isInPlot = true;
            player = Coll.gameObject;
            isPlayerOnBoat = true;
        }
    }

    private void OnCollisionExit2D(Collision2D Coll)
    {
        if (Coll.gameObject.name == "player")
        {
            //isGo = true;
            Globals.isInPlot = false;
            isPlayerOnBoat = false;
        }
    }
}
