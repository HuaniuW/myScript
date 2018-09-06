using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class PlotControl : MonoBehaviour {
    public bool isInPlot = false;
    Vector3 bodyScale;
    Rigidbody2D playerRigidbody2D;
    UnityArmatureComponent DBBody;
    // Use this for initialization
    void Start () {
        bodyScale = Vector3.one;  //this.GetComponent<GameBody>().GetBodyScale();
        playerRigidbody2D = this.GetComponent<GameBody>().GetPlayerRigidbody2D();
        //print("----------------> "+playerRigidbody2D);
        DBBody = this.GetComponent<GameBody>().GetDB();
    }

    bool isWalk = false;
    bool isWalking = false;

    private void WalkLeft()
    {
        //print(2);
        print(this.transform.localScale);
        bodyScale.x = 1;
        this.transform.localScale = bodyScale;
        playerRigidbody2D.AddForce(new Vector2(-20, 0));
        Walk();
    }
    private void WalkRight()
    {
        //print(1);
        bodyScale.x = -1;
        this.transform.localScale = bodyScale;
        playerRigidbody2D.AddForce(new Vector2(20, 0));
        Walk();
    }

    private void Walk()
    {
        if(DBBody.animation.lastAnimationName!= "walk_1")
        {
            DBBody.animation.GotoAndPlayByFrame("walk_1");
        }
        
        //throw new NotImplementedException();
    }

    public bool TurnLeft()
    {
        if (this.transform.localScale.x == -1)
        {
            bodyScale.x = 1;
            this.transform.localScale = bodyScale;
            return true;
        }
        return false;
    }

    public bool TurnRight()
    {
        if (this.transform.localScale.x == 1)
        {
            bodyScale.x = -1;
            this.transform.localScale = bodyScale;
            return true;
        }
        else
        {
            return true;
        }
        return false;
    }

   

    public bool GetToPosition(float __x)
    {
        //print("---juli   "+(__x - this.transform.position.x));
        if(__x - this.transform.position.x > 0.5)
        {
            WalkRight();
        }else if(__x - this.transform.position.x < -0.5)
        {
            WalkLeft();
        }
        else
        {
            return true;
        }
        return false;
    }

    public bool GetSit()
    {
        //print("getSit>>>>>>>>>>>>>>>>>");
        if(DBBody.animation.lastAnimationName == "getSit_1"&& DBBody.animation.isCompleted)
        {
            DBBody.animation.GotoAndPlayByFrame("sit_1");
            return true;
        }

        if (DBBody.animation.lastAnimationName != "sit_1"&&DBBody.animation.lastAnimationName != "getSit_1")
        {
            DBBody.animation.GotoAndPlayByFrame("getSit_1",0,1);
            return false;
        }
        return false;
    }

    public bool GetStand()
    {
        if (DBBody.animation.lastAnimationName == "getStand_1" && DBBody.animation.isCompleted)
        {
            DBBody.animation.GotoAndPlayByFrame("stand_1");
            return true;
        }
        
        if (DBBody.animation.lastAnimationName != "stand_1"&&DBBody.animation.lastAnimationName != "getStand_1")
        {
            DBBody.animation.GotoAndPlayByFrame("getStand_1",0,1);
            return false;
        }

        return false;
    }

    // Update is called once per frame
    void Update () {
        if (!isInPlot) return;
        
	}
}
