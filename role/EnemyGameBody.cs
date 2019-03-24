using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGameBody : GameBody {

	// Use this for initialization
	void Start () {
        GetStart();
    }
	
	// Update is called once per frame
	void Update () {
        if (isFighting)
        {
            OutFighting();
        }
        this.GetUpdate();
    }

    bool isFighting = false;
    //切换站立姿势 切换跑动姿势
    void ChangeStandAndRunAC()
    {
        STAND = "stand_2";
        CheckIsHasAC();
    }

    void ChangeACNum(int nums)
    {
        if (nums == 1)
        {
            STAND = "stand_3";
            RUN = "run_3";
        }
        else if (nums == 2)
        {
            STAND = "stand_2";
            RUN = "run_1";
        }
        else if (nums == 3)
        {
            STAND = "stand_2";
            RUN = "run_2";
        }
        else if (nums == 4)
        {
            STAND = "stand_1";
            RUN = "run_3";
        }
        CheckIsHasAC();
    }

    /// <summary>
    /// 检测是否包含 站立和跑动动作 不包含就还愿初始动作
    /// </summary>
    void CheckIsHasAC()
    {
        if (!DBBody.animation.HasAnimation(STAND)) BEHIT = "stand_1";
        if (!DBBody.animation.HasAnimation(RUN)) RUN = "run_3";
    }

    //void ChangeRunAC()
    //{
    //    int nums = Random.Range(0, 3);
    //    nums += 1;
    //    RUN = "run_" + nums;
    //    if (!DBBody.animation.HasAnimation(RUN)) RUN = "run_3";
    //}

    int inFightNums = 0;
    //脱离战斗状态
    void OutFighting()
    {
        inFightNums++;

        if (inFightNums >= 1000)
        {
            //print(inFightNums);
            ChangeACNum(4);
            DBBody.animation.GotoAndPlayByFrame(STAND);
            isFighting = false;
        }
    }


    override public void InFightAtk()
    {
        //print("hellooooooooooooooooooooooooooooooooooo!!");
        inFightNums = 0;
        isFighting = true;
        //print("atkNums   " + atkNums);
        ChangeStandAndRunAC();
        //if (atkNums == 2)
        //{
        //    //ChangeACNum(3);
        //}else if (atkNums == 3)
        //{
        //    //ChangeACNum(2);
        //}
    }

    override public void HasBeHit(float chongjili = 0)
    {
        if (DBBody.animation.lastAnimationName == DODGE1) return;
        ResetAll();
        roleDate.isBeHiting = true;
        inFightNums = 0;
        InFightAtk();
        ChangeStandAndRunAC();
        //print("speedX   "+ speedX);
        //print("22--->  "+ playerRigidbody2D.velocity.x);
        if (chongjili > 700)
        {
            BEHIT = "beHit_3";
            ChangeACNum(2);
        }
        else
        {
            //判断是哪种被击中 改变被击中的动作
            //判断是否包含动作 
            float rnum = Random.Range(0, 2);
            BEHIT = rnum >= 1 ? "beHit_1" : "beHit_2";
        }
        if (!DBBody.animation.HasAnimation(BEHITINAIR)) BEHIT = "beHit_1";

        //print(speedX);


        if (isInAiring)
        {
            if (DBBody.animation.HasAnimation(BEHITINAIR))
            {
                DBBody.animation.GotoAndPlayByFrame(BEHITINAIR, 0, 1);
                return;
            }
        }
        DBBody.animation.GotoAndPlayByFrame(BEHIT, 0, 1);
    }

    override protected void GetBeHit()
    {
        if ((DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == BEHITINAIR) && DBBody.animation.isCompleted)
        {
            roleDate.isBeHiting = false;
            if (IsGround) GetStand();
        }
    }
}
