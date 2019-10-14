using System.Collections;
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
        //float spU = horizontalDirection > 0 ? 0.04f : -0.04f;
        //this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + spU);
        Run();
    }

    public virtual void GetStop()
    {
        playerRigidbody2D.velocity = Vector2.zero;
    }

    protected override void GetBeHit()
    {

        if ((DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == BEHITINAIR) && DBBody.animation.isCompleted)
        {
            roleDate.isBeHiting = false;
            GetPause(0.7f);
            if (IsGround) GetStand();
        }
        else
        {
            print(" isBeHiting! 但是没有进入 behit 动作 ");
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
            isTXShow = true;
            isAtkYc = true;
            yanchi = 0;
            jisuqi = 0;

            if (isInAiring)
            {
                atkZS = DataZS.jumpAtkZS;
            }
            else
            {
                atkZS = DataZS.atkZS;
            }

            if (atkName == null)
            {
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
        }

    }

}
