using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameBody : GameBody {

	// Use this for initialization
	//void Start () {
 //       GetStart();
 //   }
	
	// Update is called once per frame
	void Update () {
        LJJiSHu();
        if (isFighting)
        {
            OutFighting();
        }
        this.GetUpdate();
    }

    public override void GameOver()
    {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
    }

    void RemoveSelf(UEvent e)
    {
        if(this == null)
        {
            ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
            return;
        }
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
        DestroyImmediate(this.gameObject, true);
    }

    bool isFighting = false;
    //切换站立姿势 切换跑动姿势
    void ChangeStandAndRunAC()
    {
        int nums = Random.Range(1, 3);
        nums += 1;
        STAND = "stand_" + nums;
        if(nums == 3||nums == 1)
        {
            RUN = "run_3";
        }
        else
        {
            //RUN = "run_" + nums;
            RUN = "run_1";
        }
        CheckIsHasAC();
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
            AtkReSet();
        }

        if (isAtk) return;
        //resetAll();
        isAtkYc = false;
        isRunRighting = true;
        isRunLefting = false;

        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        //print("right "+ horizontalDirection + "  xForce "+xForce);
        Run();
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
            AtkReSet();
        }

        if (isAtk) return;

        //resetAll();
        isAtkYc = false;
        isRunLefting = true;
        isRunRighting = false;

        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        //print("hihihi");
        Run();

    }




    bool isLJ = false;
    int jishiNum = 0;
    void LJJiSHu()
    {
        if (isLJ)
        {
            jishiNum++;
            if (jishiNum >= 30)
            {
                jishiNum = 0;
                isLJ = false;
                atkNums = 0;
                yanchi = 0;
            }
        }
    }

    protected override void Atk()
    {
        if (DBBody.animation.lastAnimationName == vOAtk.atkName && DBBody.animation.isPlaying)
        {
            //print(DBBody.animation.lastAnimationState);
        }
        if (DBBody.animation.lastAnimationName == vOAtk.atkName && DBBody.animation.isCompleted)
        {
            jisuqi = 0;
            yanchi++;
            //ljTime.GetStopByTime(0.5f);
            if (yanchi > vOAtk.yanchi - canMoveNums)
            {
                isAtk = false;
                if (this.transform.tag != "Player") isAtking = false;
            }

           
            

            if (yanchi == 1)
            {
                if (atkNums <= atkZS.Length)
                {
                    atkNums++;
                    jishiNum = 0;
                    if(atkNums == atkZS.Length) atkNums = 0;
                    isLJ = true;
                }
            }

            if (yanchi >= vOAtk.yanchi)
            {
                //超过延迟时间 失去连击
                isAtkYc = false;
                AtkLJOver();
                //playerRigidbody2D.gravityScale = 4.5f;
            }
            InFightAtk();

        }

        //保险措施 
        if (DBBody.animation.lastAnimationName != vOAtk.atkName)
        {
            AtkLJOver();
            //playerRigidbody2D.gravityScale = 4.5f;
        }

    
    }


    public override void GetAtk(string atkName = null)
    {
        if (roleDate.isBeHiting) return;
        if (isDodgeing || isAcing) return;
        //阻止了快落地攻击时候的bug
        //这里会导致AI回跳 进入落地动作而不能进入atk动作 所以回跳的跳起在动画里面做 不在程序里面给Y方向推力
        if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        if (!isAtk)
        {
            isAtk = true;
            isAtking = true;
            isAtkYc = true;
            yanchi = 0;
            jisuqi = 0;
            isLJ = false;
            jishiNum = 0;

            if (isInAiring)
            {
                if (Globals.isKeyUp)
                {
                    atkZS = DataZS.jumpAtkUpZS;
                }
                else if (Globals.isKeyDown)
                {
                    atkZS = DataZS.jumpAtkDownZS;
                }
                else
                {
                    atkZS = DataZS.jumpAtkZS;
                }


                if (vOAtk.yF == 0)
                {
                    newSpeed.y = 1;
                    playerRigidbody2D.velocity = newSpeed;
                }
                else
                {
                    MoveVY(vOAtk.yF);
                }
            }
            else
            {
                if (Globals.isKeyUp)
                {
                    atkZS = DataZS.atkUpZS;
                }
                else
                {
                    atkZS = DataZS.atkZS;
                }
                
            }

            if (atkName == null)
            {
                if (atkNums >= atkZS.Length) atkNums = 0;
                vOAtk.GetVO(atkZS[(int)atkNums]);
                DBBody.animation.GotoAndPlayByFrame(vOAtk.atkName, 0, 1);
            }
            else
            {
                //GetPause(0.5f,0.5f);
                string _atkName = atkName;
                if (atkName.Split('|').Length != 1)
                {
                    _atkName = atkName.Split('|')[0];
                    float times = float.Parse(atkName.Split('|')[1].Split('-')[0]);
                    float scales = 0.5f;
                    if (atkName.Split('|')[1].Split('-').Length != 1) scales = float.Parse(atkName.Split('|')[1].Split('-')[1]);
                    GetPause(times, scales);

                }
                vOAtk.GetVO(GetDateByName.GetInstance().GetDicSSByName(_atkName, DataZS.GetInstance()));
                DBBody.animation.GotoAndPlayByFrame(vOAtk.atkName, 0, 1);
            }

            MoveVX(vOAtk.xF, true);
            
            //print(newSpeed.y);
            //获取XY方向的推力 
            //print(DBBody.animation.animations);

        }

    }



    void AtkLJOver()
    {
        isAtking = false;
        yanchi = 0;
        //atkNums = 0;
    }


    TheTimer ljTime = new TheTimer();


    void ChangeACNum(int nums)
    {
        if(nums == 1)
        {
            STAND = "stand_3";
            RUN = "run_3";
        }
        else if(nums == 2)
        {
            STAND = "stand_2";
            RUN = "run_1";
        }else if (nums == 3)
        {
            STAND = "stand_2";
            RUN = "run_2";
        }else if (nums == 4)
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
        if (roleDate.isBeHiting) return;
        if (!DBBody.animation.HasAnimation(STAND)) STAND = "stand_1";
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
        Time.timeScale = 0.5f;
        //print("speedX   "+ speedX);
        //print("22--->  "+ playerRigidbody2D.velocity.x);
        if (chongjili > 700)
        {
            if(DBBody.animation.HasAnimation("beHit_3")) BEHIT = "beHit_3";
            ChangeACNum(2);
        }
        else
        {
            //判断是哪种被击中 改变被击中的动作
            //判断是否包含动作 
            float rnum = Random.Range(0, 2);
            if (DBBody.animation.HasAnimation("beHit_2")) BEHIT = rnum >= 1 ? "beHit_1" : "beHit_2";
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


    int mnum = 0;
    override protected void GetBeHit()
    {
        if (DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == BEHITINAIR)
        {
            mnum++;
            if (mnum > 5)
            {
                Time.timeScale = 1;
            }
        }


        if ((DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == BEHITINAIR) && DBBody.animation.isCompleted)
        {
            roleDate.isBeHiting = false;
            mnum = 0;
            Time.timeScale = 1;
            if (IsGround) GetStand();
        }
    }

    override public void GetDie()
    {
        if (DBBody.animation.lastAnimationName != DIE) {
            if (!isInAiring)
            {
                int nums = Random.Range(1, 3);
                DIE = "die_" + nums;
            }
            if (!DBBody.animation.HasAnimation(DIE)) DIE = "die_1";
            DBBody.animation.GotoAndPlayByFrame(DIE, 0, 1);
        }
        //Time.timeScale = 0.5f;
        //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.DIE_OUT), this);
        if (isDieRemove) StartCoroutine(IEDieDestory(1f));
    }


    protected override void InAir()
    {
        // print(DBBody.animation.lastAnimationName+"   speedy  "+ newSpeed.y);

        if (isDodgeing && IsHitMQWall && isInAiring)
        {
            //碰到墙
            if (DBBody.animation.lastAnimationName != JUMPHITWALL) DBBody.animation.GotoAndPlayByFrame(JUMPHITWALL, 0, 1);
            //isJump2 = false;
            isJumping2 = false;
            isDowning = false;
            isDodgeing = false;
            isDodge = false;
            playerRigidbody2D.gravityScale = gravityScaleNums;
            return;
        }


        if (isDodgeing || isAcing) return;
        isInAiring = !IsGround;
        if (IsGround && DBBody.animation.lastAnimationName == DOWNONGROUND)
        {
            //print("???????????");
            if (DBBody.animation.isCompleted)
            {
                //print("luodidongzuo zuowan");
                isDowning = false;
                isJumping = false;
                isJumping2 = false;
                isQiTiao = false;
                isJump2 = false;
                isAtkYc = false;
                //落地还原 不然 地上攻击会累加
                atkNums = 0;
                DBBody.animation.GotoAndPlayByFrame(STAND, 0, 1);
                //GetStand();
            }
            return;
        }


        //print("isqitiao  "+isQiTiao);

        if (IsGround && !isBackUping && (isQiTiao || DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == JUMPDOWN || DBBody.animation.lastAnimationName == JUMP2DUAN || DBBody.animation.lastAnimationName == JUMPHITWALL))
        {
            //落地动作
            if (DBBody.animation.lastAnimationName != DOWNONGROUND)
            {
                DBBody.animation.GotoAndPlayByFrame(DOWNONGROUND, 0, 1);
                isAtkYc = false;
                isAtking = false;
                isAtk = false;
                MoveVX(0);
            }
        }

        if (isAtking)
        {
            if (isInAiring)
            {
                //playerRigidbody2D.gravityScale = 2f;
                playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x * 0.9f, playerRigidbody2D.velocity.y);
            }
            return;
        }
        else
        {
            //playerRigidbody2D.gravityScale = 4.5f;
        }


        if (IsHitMQWall && isInAiring)
        {
            //碰到墙
            if (DBBody.animation.lastAnimationName != JUMPHITWALL) DBBody.animation.GotoAndPlayByFrame(JUMPHITWALL, 0, 1);
            //isJump2 = false;
            isJumping2 = false;
            isDowning = false;
            return;
        }



        if (isInAiring)
        {
            if (roleDate.isBeHiting || DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == BEHITINAIR) return;
            if (newSpeed.y <= 0)
            {
                if (!isDowning)
                {
                    //下降
                    isDowning = true;
                    //还原落地攻击的BUG
                    isQiTiao = true;
                    DBBody.animation.GotoAndPlayByFrame(JUMPDOWN, 0, 1);
                }
            }
            else
            {
                if (isJumping2 && (DBBody.animation.lastAnimationName == JUMP2DUAN || DBBody.animation.lastAnimationName == JUMPHITWALL || DBBody.animation.lastAnimationName == RUNBEGIN) && !DBBody.animation.isCompleted) return;
                if (DBBody.animation.lastAnimationName != JUMPDOWN)
                {
                    //上升
                    //print("shangsheng");
                    //newSpeed.y >0 的时候是上升  这个是起跳动作完成后 上升的时候 停留在下降的最后一帧 
                    //做动画的时候  下落动画第一帧就是 起跳最后一帧
                    DBBody.animation.GotoAndPlayByFrame(JUMPDOWN, 0, 1);
                    DBBody.animation.Stop();
                    isDowning = false;
                }
            }
        }
    }

    int maxJumpNums = 1;
    int jumpNums = 1;
    public override void GetJump()
    {
        if (roleDate.isBeHiting) return;
        if (isDodgeing) return;
        Jump();
    }



    protected override void Jump()
    {
        if (isDodgeing || isAtk || roleDate.isBeHiting) return;

        if (jumpNums < 0) return;
        if (jumpNums == 0)
        {
            if (DBBody.animation.lastAnimationName != JUMP2DUAN)
            {
                if (DBBody.animation.lastAnimationName == JUMPHITWALL)
                {
                    newPosition = this.transform.localPosition;
                    if (bodyScale.x == 1)
                    {
                        MoveXByPosition(0.1f);
                        playerRigidbody2D.AddForce(Vector2.right * wallJumpXNum);
                    }
                    else
                    {
                        MoveXByPosition(-0.1f);
                        playerRigidbody2D.AddForce(Vector2.left * wallJumpXNum);
                    }
                }
                DBBody.animation.GotoAndPlayByFrame(JUMP2DUAN, 0, 1);
                newSpeed.y = 0.1f;
                isJumping2 = true;
                playerRigidbody2D.velocity = newSpeed;
                playerRigidbody2D.AddForce(Vector2.up * yForce);
            }
        }
        else
        {
            if (!roleDate.isBeHiting && DBBody.animation.lastAnimationName != JUMPHITWALL &&
                DBBody.animation.lastAnimationName != JUMP2DUAN &&
                DBBody.animation.lastAnimationName != DOWNONGROUND &&
                DBBody.animation.lastAnimationName != JUMPUP)
            {
                playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, 0);
                DBBody.animation.GotoAndPlayByFrame(JUMPUP, 0, 1);
                playerRigidbody2D.AddForce(Vector2.up * yForce);
            }
        }
        jumpNums--;

    }

    protected override void IsCanShanjinAndJump()
    {
        if (IsGround || IsHitMQWall)
        {
            isCanShanjin = true;
        }

        if (IsGround&& DBBody.animation.lastAnimationName == DOWNONGROUND) jumpNums = maxJumpNums;
        if (IsHitMQWall) jumpNums = 0;

    }
}
