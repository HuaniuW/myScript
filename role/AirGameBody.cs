﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class AirGameBody : GameBody {

	// Use this for initialization
	//void Start () {
        //print(this.DBBody == null);
        //if(DBBody == null) DBBody = GetComponentInChildren<UnityArmatureComponent>();
        //roleDate = GetComponent<RoleDate>();
        //print(this.rigidbody2D)
        //this.TestsI();
        //GetStart();
    //}
	
	// Update is called once per frame
	void Update () {
        this.GetUpdate();
	}

    public override void Testss()
    {
        print("AirGamebody!");
    }

    public void Test22()
    {
        print("222");
    }

    public override void ResetAll()
    {
        //isRun = false;
        isRunLefting = false;
        isRunRighting = false;
        //isInAiring = false;
        isDowning = false;
        //在空中被击中 如果关闭跳跃bool会有落地bug
        //isJumping = false;
        // isJumping2 = false;
        //isJump2 = false;
        //isQiTiao = false;
        isAtk = false;
        isAtking = false;
        atkNums = 0;
        isAtkYc = false;
        isYanchi = false;
        isBackUp = false;
        isBackUping = false;
        isQianhua = false;
        isQianhuaing = false;
        isAcing = false;
        isYanchi = false;
        isSkilling = false;
        isSkillOut = false;
        if (roleDate) roleDate.isBeHiting = false;
    }

    protected override void Stand()
    {
        if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        //print(">  "+DBBody.animation.lastAnimationName+"   atking "+isAtking);
        if (DBBody.animation.lastAnimationName != STAND || (DBBody.animation.lastAnimationName == STAND && DBBody.animation.isCompleted))
        {
            DBBody.animation.GotoAndPlayByFrame(STAND, 0, 1);
            //print("--");
           
        }

        isDowning = false;
        if (newSpeed.x > slideNum)
        {
            newSpeed.x = slideNum - 1;
        }
        else if (newSpeed.x < -slideNum)
        {
            newSpeed.x = -slideNum + 1;
        }

        playerRigidbody2D.velocity = newSpeed;
    }

    protected override void Run()
    {
        //print("isJumping   "+ isJumping+ "    isDowning  "+ isDowning+ "   isBeHiting  " + roleDate.isBeHiting+ "isInAiring" + isInAiring+ "   isDodgeing  " + isDodgeing);
        //if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        if (roleDate.isBeHiting) return;
        if (DBBody.animation.lastAnimationName != RUN)
        {
           // print("?????run");
            DBBody.animation.GotoAndPlayByFrame(RUN);
        }
      
    }

    protected override void InAir()
    {
        //print("hi");
    }

    public override bool IsGround
    {
        get
        {
            return true;
        }
    }

    public override bool IsEndGround
    {
        get
        {
            return false;
        }
    }

    protected override void Yanmu()
    {
        
    }

    public override void GetDie()
    {
        playerRigidbody2D.velocity = Vector2.zero;
        if (DBBody.animation.lastAnimationName != DIE) DBBody.animation.GotoAndPlayByFrame(DIE, 0, 1);
        if (isDieRemove) StartCoroutine(IEDieDestory(2f));
    }

    public override void RunLeft(float horizontalDirection, bool isWalk = false)
    {
        //print("r "+isAtking);
        isBackUping = false;
        if (roleDate.isBeHiting) return;
        if (isAcing) return;
        if (isDodgeing) return;
        if (!DBBody.animation.HasAnimation(WALK)) isWalk = false;
        if (!isWalk && bodyScale.x == -1)
        {
            bodyScale.x = 1;
            this.transform.localScale = bodyScale;
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x * 0.01f, playerRigidbody2D.velocity.y);
            AtkReSet();
        }

        if (isAtking) return;

        //resetAll();
        isAtkYc = false;
        isRunLefting = true;
        isRunRighting = false;

        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        //print("hihihi");
        Run();

    }

    public override void RunRight(float horizontalDirection, bool isWalk = false)
    {
        //print("l " + isAtking);
        isBackUping = false;
        if (roleDate.isBeHiting) return;
        if (isAcing) return;
        if (isDodgeing) return;
        if (!DBBody.animation.HasAnimation(WALK)) isWalk = false;
        if (!isWalk && bodyScale.x == 1)
        {
            bodyScale.x = -1;
            this.transform.localScale = bodyScale;
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x * 0.01f, playerRigidbody2D.velocity.y);
            AtkReSet();
        }

        if (isAtking) return;
        //resetAll();
        isAtkYc = false;
        isRunRighting = true;
        isRunLefting = false;

        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        Run();
    }

    public virtual void RunY(float horizontalDirection)
    {
        isBackUping = false;
        if (roleDate.isBeHiting) return;
        if (isAcing) return;
        if (isDodgeing) return;
        if (isAtking) return;
        //resetAll();
        isAtkYc = false;

        playerRigidbody2D.AddForce(new Vector2(0,yForce * horizontalDirection));
        Run();
    }

    public virtual void GetStop()
    {
        playerRigidbody2D.velocity = Vector2.zero;
    }

}
