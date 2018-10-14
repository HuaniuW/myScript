using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using System;

public class GameBody : MonoBehaviour,IRole{

    public ParticleSystem tx_1;
    public ParticleSystem tx_2;
    public ParticleSystem tx_3;
    public ParticleSystem tx_4;

    [Header("水平速度")]
    public float speedX;

    //水平方向的按键响应 Input.GetAxis
    const string HORIZONTAL = "Horizontal";

    [Header("水平推力")]
    [Range(0, 150)]//数值滑杆 限定最大最小值
    public float xForce;

    //目前垂直速度
    float speedY;

    [Header("水平最大速度")]
    public float maxSpeedX;

    Vector2 newSpeed;

    [Header("垂直向上的推力")]
    public float yForce;

    [Header("感应地板的距离")]
    [Range(0, 1)]
    public float distance;

    [Header("侦测地板的射线起点")]
    public UnityEngine.Transform groundCheck;


    [Header("感应与面前墙的距离")]
    [Range(0, 1)]
    public float distanceMQ = 0.13f;

    [Header("侦测面前墙的射线起点")]
    public UnityEngine.Transform qianmian;


    [Header("当前动作名字")]
    public string CurrentAcName;

    [Header("地面图层")]
    public LayerMask groundLayer;

    [Header("是否着地")]
    public bool grounded;

    [Header("是否碰到面前的墙")]
    public bool hidWalled;

    [Header("停下后X方向的剩余滑动速度")]
    public float slideNum = 3;

    [Header("反弹跳X方向的力")]
    public float wallJumpXNum = 800;

    Vector3 newPosition;

    [Header("是否die回收")]
    public bool isDieRemove = true;


    Rigidbody2D playerRigidbody2D;
    public Rigidbody2D GetPlayerRigidbody2D()
    {
        if(!playerRigidbody2D) playerRigidbody2D = GetComponent<Rigidbody2D>();
        return playerRigidbody2D;
    }

    UnityArmatureComponent DBBody;
    public UnityArmatureComponent GetDB()
    {
        if(!DBBody) DBBody = GetComponentInChildren<UnityArmatureComponent>();
        return DBBody;
    }

    Vector3 bodyScale;
    public Vector3 GetBodyScale()
    {
        return bodyScale;
    }

    bool isRunLefting = false;

    bool isRunRighting = false;

    bool isInAiring = false;
    bool isDowning = false;

    bool isJumping = false;
    //起跳
    bool isQiTiao = false;

    public void ResetAll()
    {
        //isRun = false;
        isRunLefting = false;
        isRunRighting = false;
        //isInAiring = false;
        //isDowning = false;
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
        isBackUping = false;
    }

    RoleDate roleDate;


    const string RUN = "run_3";
    const string STAND = "stand_1";
    const string RUNBEGIN = "runBegin_1";
    const string RUNSTOP = "runStop_1";
    const string JUMPUP = "jumpUp_1";
    const string JUMPDOWN = "jumpDown_1";
    const string JUMPHITWALL = "jumpHitWall_1";
    const string DOWNONGROUND = "downOnGround_1";
    const string JUMP2DUAN = "jump2Duan_1";
    const string ATK = "atk_";
    const string DODGE1 = "dodge_1";
    const string DODGE2 = "dodge_2";
    const string BEHIT = "beHit_1";
    const string DIE = "die_1";
    //回跳跃动作
    const string BACKUP = "backUp_1";
    const string WALK = "walk_1";
    const string BEHITINAIR = "beHitInAir_1";


    bool isBackUping = false;
    public bool GetBackUpOver()
    {
        if (roleDate.isBeHiting) return true;
        if (!DBBody.animation.HasAnimation(BACKUP)) return true;
        if(DBBody.animation.lastAnimationName != BACKUP)
        {
            DBBody.animation.GotoAndPlayByFrame(BACKUP, 0, 1);
            isBackUping = true;
            roleDate.isCanBeHit = false;
            BackJumpVX(500);
            MoveVY(300);

        }
        if (DBBody.animation.lastAnimationName == BACKUP&& DBBody.animation.isCompleted)
        {
            isBackUping = false;
            roleDate.isCanBeHit = true;
            return true;
        }
        return false;
    }




    bool isSkill = false;
    bool isSkilling = false;
    internal void GetSkill1()
    {
        if (roleDate.isBeHiting) return;
        if (!isSkill)
        {
            isSkill = false;
        }
        print("释放技能");
        //this.GetComponent<GetHitKuai>().GetKuai("jn_yueguang","1");
        GetComponent<ShowOutSkill>().ShowOutSkillByName("jn_yueguang");
    }


    internal void GetSkill2()
    {
        if (roleDate.isBeHiting) return;
        print("释放技能2");
        ShowSkillByNum(1);
    }

    public void ShowSkillByNum(int n=1) {
        //根据技能槽 安装的徽章技能来释放 相应技能
        // 取得技能槽 徽章技能名称
        string hzSkillName =  GetHZSkillName(n);
        GetComponent<ShowOutSkill>().ShowOutSkillByName(hzSkillName);
        //GameObject t = Resources.Load("ttt") as GameObject;
        //t = GameObject.Instantiate(t);
        //t.transform.position = this.transform.position;
    }

    private string GetHZSkillName(int n)
    {
        return "jn_shan";
    }

    bool isDodge = false;
    bool isDodgeing = false;

    public void GetDodge1()
    {
        if (roleDate.isBeHiting) return;
        if (isInAiring||DBBody.animation.lastAnimationName== DOWNONGROUND|| DBBody.animation.lastAnimationName == JUMPUP) return;
        if (!isDodge)
        {
            ResetAll();
            isDodge = true;
            isDodgeing = true;
            roleDate.isCanBeHit = false;
            //print("-->x  " + playerRigidbody2D.velocity.x);
            if (playerRigidbody2D.velocity.x >= 0)
            {
                if (DBBody.animation.lastAnimationName != DODGE2)
                {
                    DBBody.animation.GotoAndPlayByFrame(DODGE2, 0, 1);
                    MoveVX(2200);
                }
            }
            else if (playerRigidbody2D.velocity.x < 0)
            {
                if (DBBody.animation.lastAnimationName != DODGE2)
                {
                    DBBody.animation.GotoAndPlayByFrame(DODGE2, 0, 1);
                    MoveVX(2200);
                }
            }
        }
    }


    void Dodge1()
    {
        if ((DBBody.animation.lastAnimationName == DODGE1|| DBBody.animation.lastAnimationName == DODGE2) && DBBody.animation.isCompleted)
        {
            isDodge = false;
            isDodgeing = false;
            roleDate.isCanBeHit = true;
        }
    }


    [Header("侦测地板的射线起点2")]
    public UnityEngine.Transform groundCheck2;
    //路面是否尽头
    bool isEndGround = false;
    public bool IsEndGround
    {
        get
        {
            if (groundCheck2 == null) return false;
            Vector2 start = groundCheck2.position;
            Vector2 end = new Vector2(start.x, start.y - 0.7f);
            Debug.DrawLine(start, end, Color.red);
            isEndGround = Physics2D.Linecast(start, end, groundLayer);
            return !isEndGround;
        }
    }

    bool isHitWall = false;
    public bool IsHitWall
    {
        get
        {
            if (groundCheck2 == null) return false;
            Vector2 start = groundCheck2.position;
            Vector2 end = new Vector2(start.x, start.y + 0.8f);
            Debug.DrawLine(start, end, Color.yellow);
            isHitWall = Physics2D.Linecast(start, end, groundLayer);
            return isHitWall;
        }
    }


    //在玩家底部是一条短射线 碰到地板说明落到地面 
    bool IsGround
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            grounded = Physics2D.Linecast(start, end, groundLayer);
            return grounded;
        }
    }


    bool IsHitMQWall
    {
        get
        {
            Vector2 start = qianmian.position;
            Vector2 end = new Vector2(start.x - distanceMQ * bodyScale.x, start.y);
            Debug.DrawLine(start, end, Color.red);
            hidWalled = Physics2D.Linecast(start, end, groundLayer);
            if (IsGround) hidWalled = false;
            return hidWalled;
        }
    }


    public void ControlSpeed()
    {
        speedX = playerRigidbody2D.velocity.x;
        speedY = playerRigidbody2D.velocity.y;
        //钳制 speedX 被限制在 -maxSpeedX  maxSpeedX 之间
        float newSpeedX = Mathf.Clamp(speedX, -maxSpeedX, maxSpeedX);
        //if (horizontalDirection == 0) newSpeedX/=10;
        newSpeed.x = newSpeedX;
        newSpeed.y = speedY;
        //获取向量速度
        playerRigidbody2D.velocity = newSpeed;
    }

    public void RunLeft(float horizontalDirection,bool isWalk = false)
    {
        //print("r "+isAtking);
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
        
        if (isAtking) return;
        
        //resetAll();
        isAtkYc = false;
        isRunLefting = true;
        isRunRighting = false;
       
        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        Run();

    }

    public void RunRight(float horizontalDirection, bool isWalk = false)
    {
        //print("l " + isAtking);
        if (roleDate.isBeHiting) return;
		if (isAcing) return;
        if (isDodgeing) return;
        if (!DBBody.animation.HasAnimation(WALK)) isWalk = false;
        if (!isWalk &&bodyScale.x == 1)
        {
            bodyScale.x = -1;
            this.transform.localScale = bodyScale;
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

    public void ReSetLR()
    {
        isRunRighting = false;
        isRunLefting = false;
    }

    void Run()
    {
        //print("isJumping   "+ isJumping+ "    isDowning  "+ isDowning+ "   isBeHiting  " + roleDate.isBeHiting+ "isInAiring" + isInAiring+ "   isDodgeing  " + isDodgeing);
        if (isJumping || isInAiring || isDowning || isDodgeing ||roleDate.isBeHiting) return;
        if (DBBody.animation.lastAnimationName == RUNBEGIN && DBBody.animation.isCompleted)
        {
            DBBody.animation.GotoAndPlayByFrame(RUN);
        }
        if (DBBody.animation.lastAnimationName != RUNBEGIN && DBBody.animation.lastAnimationName != RUN)
        {
            DBBody.animation.GotoAndPlayByFrame(RUNBEGIN, 0, 1);
        }
    }

    void Walk()
    {
        if (isJumping || isInAiring || isDowning || isDodgeing || roleDate.isBeHiting) return;
        if (DBBody.animation.lastAnimationName != WALK && DBBody.animation.isCompleted)
        {
            DBBody.animation.GotoAndPlayByFrame(WALK);
        }
    }

    bool isJump2 = false;
    bool isJumping2 = false;
    //bool isQiTiao = false;
    public void GetJump()
    {
        if (roleDate.isBeHiting) return;
        if (isDodgeing) return;
        if (!isJumping)
        {
            isJumping = true;
        }
        else
        {
            if (!isJumping2)
            {
                isJump2 = true;
                isJumping2 = true;
            }
        }
    }

    void MoveXByPosition(float xDistance)
    {
        newPosition.x += xDistance;
        this.transform.localPosition = newPosition;
    }


    void BackJumpVX(float vx)
    {
        var _vx = Mathf.Abs(vx);
        if (bodyScale.x < 0)
        {
            playerRigidbody2D.AddForce(Vector2.left * _vx);
        }
        else if (bodyScale.x > 0)
        {
            playerRigidbody2D.AddForce(Vector2.right * _vx);
        }
        playerRigidbody2D.velocity = newSpeed;
    }

    void MoveVX(float vx,bool isNoAbs = false)
    {
        
        var _vx = Mathf.Abs(vx);
        if (isNoAbs) _vx = vx;
        //newSpeed.x = 0;
        if (bodyScale.x < 0)
        {
            playerRigidbody2D.AddForce(Vector2.right * _vx);
        }
        else if (bodyScale.x > 0)
        {
            playerRigidbody2D.AddForce(Vector2.left * _vx);
        }
        playerRigidbody2D.velocity = newSpeed;
    }

    void MoveVY(float vy)
    {
        playerRigidbody2D.AddForce(Vector2.up * vy);
        playerRigidbody2D.velocity = newSpeed;
    }

    void Jump()
    {
        if (isAtking|| isDodgeing||roleDate.isBeHiting) return;
        if (isInAiring)
        {
            if (isJump2 && DBBody.animation.lastAnimationName != JUMP2DUAN)
            {
                isJump2 = false;
                if (DBBody.animation.lastAnimationName == JUMPHITWALL) {
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
                isJumping2 = true;
                DBBody.animation.GotoAndPlayByFrame(JUMP2DUAN, 0, 1);
                newSpeed.y = 0.1f;
                playerRigidbody2D.velocity = newSpeed;
                playerRigidbody2D.AddForce(Vector2.up * yForce);
                return;
            }
        }

       
        if (IsGround &&!roleDate.isBeHiting&&!isAtking&& DBBody.animation.lastAnimationName != JUMPHITWALL &&
            DBBody.animation.lastAnimationName != JUMP2DUAN &&
            DBBody.animation.lastAnimationName != DOWNONGROUND &&
            DBBody.animation.lastAnimationName != JUMPUP)
        {
           // print("?????>>>   "+DBBody.animation.lastAnimationName+"     "+isQiTiao);
            isQiTiao = false;
            DBBody.animation.GotoAndPlayByFrame(JUMPUP, 0, 1);
        }

        if (IsGround &&!isQiTiao && DBBody.animation.lastAnimationName == JUMPUP && DBBody.animation.isCompleted)
        {
            isQiTiao = true;
            playerRigidbody2D.AddForce(Vector2.up * yForce);
            return;
        }
    }

    void InAir()
    {
        // print(DBBody.animation.lastAnimationName+"   speedy  "+ newSpeed.y);
        if (isDodgeing) return;
        isInAiring = !IsGround;
        if (IsGround&&DBBody.animation.lastAnimationName == DOWNONGROUND)
        {
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
            }
            return;
        }


        //print("isqitiao  "+isQiTiao);

        if (IsGround&&(isQiTiao || DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == JUMPDOWN|| DBBody.animation.lastAnimationName == JUMP2DUAN|| DBBody.animation.lastAnimationName == JUMPHITWALL))
        {
            //落地动作
            if (DBBody.animation.lastAnimationName != DOWNONGROUND)
            {
                DBBody.animation.GotoAndPlayByFrame(DOWNONGROUND, 0, 1);
                isAtkYc = false;
                isAtking = false;
                isAtk = false;
            }
        }

        if (isAtking) return;

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
            if(roleDate.isBeHiting)return;
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
                if (isJumping2 && (DBBody.animation.lastAnimationName == JUMP2DUAN|| DBBody.animation.lastAnimationName == JUMPHITWALL) && !DBBody.animation.isCompleted) return;
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

   
    void Stand()
    {
        if (DBBody.animation.lastAnimationName != STAND) DBBody.animation.GotoAndPlayByFrame(STAND);
        if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
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

    public void GetStand()
    {
        ResetAll();
        Stand();
    }

    

    // Use this for initialization
    protected void Start () {
        //Tools.timeData();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        DBBody = GetComponentInChildren<UnityArmatureComponent>();
		//DBBody.AddEventListener(DragonBones.FrameEvent.MOVEMENT_FRAME_EVENT, this.onMOVEMENTBoneEvent, this);
		DBBody.AddDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
        DBBody.AddDBEventListener(DragonBones.EventObject.SOUND_EVENT, this.ShowACTX);
        roleDate = GetComponent<RoleDate>();
        //DBBody.AddDBEventListener(EventObject.FRAME_EVENT, this.test);
        DBBody.AddDBEventListener("atks", this.Test);
        bodyScale = new Vector3(1, 1, 1);
        vOAtk = GetComponent<VOAtk>();
        this.transform.localScale = bodyScale;
    }

	public void GetDie()
    {
        //print("indie!!!!!!!!!!!!!!!!");
        if(DBBody.animation.lastAnimationName != DIE) DBBody.animation.GotoAndPlayByFrame(DIE, 0, 1);
        //print("回收");
        //对象池无法移除对象 原因不明
        //ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject, 1f);
        //this.gameObject.SetActive(false);

        if(isDieRemove)StartCoroutine(IEDieDestory(2f));
    }

    public IEnumerator IEDieDestory(float time)
    {
        //Debug.Log("time   "+time);
        //yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }



    // Update is called once per frame
    void Update () {
        CurrentAcName = DBBody.animation.lastAnimationName;

       //脚下的烟幕
        Yanmu();
        if (roleDate.isDie)
        {
            GetDie();
            return;
        }
        if (roleDate.live<=0)
        {
            roleDate.isDie = true;
        }
       

        if (Globals.isInPlot) return;
        
        if (roleDate.isBeHiting)
        {
            GetBeHit();
            return;
        }

        if (isBackUping)
        {
            GetBackUpOver();
            return;
        }

        ControlSpeed();
        InAir();

		if(isAcing){
			GetAcMsg(_acName);
			return;
		}

        if (isDodgeing)
        {
            Dodge1();
            return;
        }

        if (isJumping)
        {
            Jump();
        }

        if (isAtking)
        {
            Atk();
        }

      
        if (!roleDate.isBeHiting&&!isInAiring&&!isDowning && !isRunLefting && !isRunRighting&&!isJumping&&!isAtking&&!isDodgeing&&!isAtkYc)
        {
            Stand();
        }
        
    }

    public void HasBeHit()
    {
        if (DBBody.animation.lastAnimationName == DODGE1) return;
        ResetAll();
        roleDate.isBeHiting = true;
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

    private void GetBeHit()
    {
        if((DBBody.animation.lastAnimationName == BEHIT|| DBBody.animation.lastAnimationName == BEHITINAIR) && DBBody.animation.isCompleted) {
            roleDate.isBeHiting = false;
        }
    }

    
    public ParticleSystem _yanmu;
    public ParticleSystem _yanmu2;
    void Yanmu()
    {
        if (IsGround && !isQiTiao && DBBody.animation.lastAnimationName == JUMPUP && DBBody.animation.isCompleted)
        {
            _yanmu.Play();
            return;
        }


        if(IsGround && DBBody.animation.lastAnimationName == DOWNONGROUND)
        {
            _yanmu.Play();
            return;
        }

        if (IsGround && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 3)
        {
            _yanmu.Play();
        }
        else
        {
            _yanmu.Stop();
        }


        //if (isJump2 && DBBody.animation.lastAnimationName != JUMP2DUAN)
        //{
          //  _yanmu2.Play();
            //return;
        //}

        if (IsHitMQWall && isInAiring&& DBBody.animation.lastAnimationName == JUMPHITWALL&& !DBBody.animation.isCompleted) {
            //碰到墙
            _yanmu2.Play();
            return;
        }


       // print("y " + GetComponent<Rigidbody2D>().velocity.y);

        if (IsHitMQWall && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > 3) {
            
            _yanmu2.Play();
        }
        else
        {
            _yanmu2.Stop();
        }
    }



    //1.动作名 调用 函数  动作名 和 特效名字
    //2.获取VO信息  包含 位置 相对位置（给每个角色基本信息加宽高补偿 缩放补偿） 是否需要补偿释放（有些技能特效不需要 寻找相对位置和缩放）
    //2.判断是否包含动作 有一类技能无动作可以直接释放
    //3.




    //一个是延迟 一个是连击的给出时间  连击给出时间还是得在延迟内   给个时间差做减法 在减法内 不能控制 超过减法可以接连招 跑完回复站立


    //动作控制流程
    int acNums = 0;
    //是否在动作延迟
    bool isYanchi = false;
    //动作滞留延迟
    int yanchiNum = 10;
    int yanchiMaxNum = 20;
    //延迟行动尺度
    public int canMoveNums = 100;
    bool isAcing = false;
	string _acName;
    public string GetAcMsg(string acName)
    {

        //获取技能VO
        //GetSkillVOByName(acName);
        //print("------------------------------------------------------------------  "+acName);
        if (!DBBody.animation.HasAnimation(acName)) return null;
        //print("------------------------------------------------------------------22  " + acName);
        
        if (DBBody.animation.lastAnimationName == acName && DBBody.animation.isCompleted)
        {
            acNums++;
            isYanchi = true;
            if (acNums > yanchiNum)
            {
                //可以切换招式
            }

            //这里的延迟 将来会改到 vo里面直接取  招式切换也是 这个Ac主要针对 非攻击Ac
            if (acNums > yanchiMaxNum) {
                isAcing = false;
                isYanchi = false;
                return "completed";
            }
            return "acYanChi";
        }

        if (DBBody.animation.HasAnimation(acName)&&DBBody.animation.lastAnimationName!=acName)
        {
            DBBody.animation.GotoAndPlayByFrame(acName, 0, 1);
            acNums = 0;
            isAcing = true;
			_acName = acName;
            return "start";
        }
        
        if(DBBody.animation.lastAnimationName == acName && DBBody.animation.isPlaying)
        {
			//print("acName "+_acName);
			return "playing_"+acNums;
        }
        
        return null;
    }

    private void GetSkillVOByName(string acName)
    {
        //throw new NotImplementedException();
    }

    

    public void SpeedXStop()
	{
		if (playerRigidbody2D != null) playerRigidbody2D.velocity = Vector3.zero;
		//throw new NotImplementedException();
	}

	public void SetACingfalse()
	{
		isAcing = false;
		//throw new NotImplementedException();
	}


    float atkNums = 0;
    bool isAtk = false;
    public bool isAtking = false;
    string[] atkMsg;
    VOAtk vOAtk;
    Dictionary<string, string>[] atkZS;

    public void GetAtk(string atkName = null)
    {

        //print(" atkName " + atkName+"    "+isAtking);
        if (roleDate.isBeHiting) return;
        if (isDodgeing) return;
        if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        if (!isAtk)
        {
            isAtk = true;
            isAtking = true;
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
                vOAtk.GetVO(GetDateByName.GetInstance().GetDicSSByName(atkName, DataZS.GetInstance()));
               
                DBBody.animation.GotoAndPlayByFrame(vOAtk.atkName, 0, 1);
            }

            MoveVX(vOAtk.xF);
            if (newSpeed.y < 0)
            {
                newSpeed.y = 1;
                playerRigidbody2D.velocity = newSpeed;
                MoveVY(vOAtk.yF);
            }

            //获取XY方向的推力 
            //print(DBBody.animation.animations);

        }

    }

    void Test(string type, EventObject eventObject)
    {
        //print(type+" ???time  "+eventObject);
    }

    //特效方向
    void TXPlay(ParticleSystem tx)
    {
        tx.Stop();
        Vector3 ttt = new Vector3(0, 0, 0);
        ttt = tx.transform.localScale;
        ttt.x = Mathf.Abs(tx.transform.localScale.x);
        ttt.x *= this.transform.localScale.x;
        tx.transform.localScale = ttt;
        tx.Play();
    }


    //显示动作特效
    private void ShowACTX(string type, EventObject eventObject)
    {
        //print("type:  "+type);
        if (type == EventObject.SOUND_EVENT)
        {

        }


        if (type == EventObject.FRAME_EVENT)
        {
            if (eventObject.name == "ac")
            {
                GetComponent<ShowOutSkill>().ShowOutSkillByName("dg_fk");
            }
        }
        
    }

    float jisuqi = 0;
    float yanchi = 0;

    //动作延迟  在延迟内 将isAtk=false 可以控制人物 而不会一直在动作尾不受控制
    bool isAtkYc = false;
    

    void AtkReSet()
    {
        jisuqi = 0;
        isAtk = false;
        isAtking = false;
        isAtkYc = false;
        yanchi = 0;
        atkNums = 0;
    }

    void Atk()
    {
        if (DBBody.animation.lastAnimationName == vOAtk.atkName && DBBody.animation.isPlaying)
        {

        }

        if (DBBody.animation.lastAnimationName == vOAtk.atkName && DBBody.animation.isCompleted)
        {
            jisuqi = 0;
            yanchi++;
            if(yanchi> vOAtk.yanchi - canMoveNums) isAtk = false;

            if (yanchi == 1)
            {
                if (atkNums <= atkZS.Length)
                {
                    atkNums++;
                }
                if (atkNums == atkZS.Length) atkNums = 0;
            }
            if (yanchi >= vOAtk.yanchi)
            {
                //超过延迟时间 失去连击
                isAtking = false;
                isAtkYc = false;
                yanchi = 0;
                atkNums = 0;
            }
        }

        //保险措施 
        if(DBBody.animation.lastAnimationName != vOAtk.atkName)
        {
            isAtking = false;
            yanchi = 0;
            atkNums = 0;
        }
    }

    public bool IsAtkOver()
    {
        return !isAtk;
    }

    /// <summary

    void Atk2()
    {
        if (DBBody.animation.lastAnimationName == vOAtk.atkName && DBBody.animation.isPlaying)
        {
            jisuqi++;
            //print("jisuqi "+jisuqi+"    ??    "+ vOAtk.showTXFrame);
            //特效出现时间
            if (jisuqi == vOAtk.showTXFrame)
            {
                //this["tx_1"].play();
                //(ParticleSystem)this.vOAtk.txName.Play();
                //print("vOAtk.txName  "+ vOAtk.txName);
                //[vOAtk.atkName+"_v"]

                //print("gongjishuzhi "+DataZS.getInstance().Test("atk_1_v"));
                //DataZS.getInstance().getTest();
                //AtkAttributesVO atkVVo = AtkAttributesVO.getInstance();
                //atkVVo.getValue(DataZS.atk_1_v);
                //atkVVo.team = this.GetComponent<RoleDate>().team;
                //this.GetComponent<GetHitKuai>().GetKuai();
                GetComponent<ShowOutSkill>().ShowOutSkillByName("dg_fk");
                //如果直接按名称来 这里改为 拿技能的 资源 pro资源  下面就可以不要了

                if (vOAtk.txName == "tx_1")
                {
                    TXPlay(tx_1);
                }
                else if (vOAtk.txName == "tx_2")
                {
                    TXPlay(tx_2);
                }
                else if (vOAtk.txName == "tx_3")
                {
                    TXPlay(tx_3);
                }
                else if (vOAtk.txName == "tx_4")
                {
                    TXPlay(tx_4);
                }

                //print("sx " + dg1.transform.localScale.x + " --   " + this.transform.localScale.x);
            }
        }
        if (DBBody.animation.lastAnimationName == vOAtk.atkName && DBBody.animation.isCompleted)
        {
            jisuqi = 0;
            isAtk = false;
            yanchi++;
            if (yanchi == 1)
            {
                if (atkNums <= atkZS.Length)
                {
                    atkNums++;
                }
                if (atkNums == atkZS.Length) atkNums = 0;
            }
            if (yanchi >= vOAtk.yanchi)
            {
                //超过延迟时间 失去连击
                isAtking = false;
                yanchi = 0;
                atkNums = 0;
            }
        }
    }
}

