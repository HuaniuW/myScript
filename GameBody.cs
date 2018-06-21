using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class GameBody : MonoBehaviour {


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
    public float distanceMQ;

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



    Rigidbody2D playerRigidbody2D;
    UnityArmatureComponent DBBody;
    Vector3 bodyScale;

    bool isRunLefting = false;

    bool isRunRighting = false;

    bool isInAiring = false;
    bool isDowning = false;

    bool isJumping = false;
    //起跳
    bool isQiTiao = false;

    void resetAll()
    {
        //isRun = false;
        isRunLefting = false;
        isRunRighting = false;
        isInAiring = false;
        isDowning = false;
        isJumping = false;
        isJumping2 = false;
        isJump2 = false;
        isQiTiao = false;
    }


    const string RUN = "run_3";
    const string STAND = "stand_1";
    const string RUNBEGIN = "runBegin_1";
    const string RUNSTOP = "runStop_1";
    const string JUMPUP = "jumpUp_1";
    const string JUMPDOWN = "jumpDown_1";
    const string JUMPHITWALL = "jumpHitWall_1";
    const string DOWNONGROUND = "downOnGround_1";
    const string JUMP2DUAN = "jump2Duan_1";

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
            Vector2 end = new Vector2(start.x - distanceMQ*bodyScale.x, start.y);
            Debug.DrawLine(start, end, Color.red);
            hidWalled = Physics2D.Linecast(start, end, groundLayer);
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

    public void runLeft(float horizontalDirection)
    {
        isRunLefting = true;
        isRunRighting = false;
        bodyScale.x = 1;
        this.transform.localScale = bodyScale;
        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        run();

    }

    public void runRight(float horizontalDirection)
    {
        isRunRighting = true;
        isRunLefting = false;
        bodyScale.x = -1;
        this.transform.localScale = bodyScale;
        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        run();
    }

    public void reSetLR()
    {
        isRunRighting = false;
        isRunLefting = false;
    }

    void run()
    {
        if (isJumping||isInAiring|| isDowning) return;
        if (DBBody.animation.lastAnimationName == RUNBEGIN && DBBody.animation.isCompleted)
        {
            DBBody.animation.GotoAndPlayByFrame(RUN);
        }
        if (DBBody.animation.lastAnimationName != RUNBEGIN && DBBody.animation.lastAnimationName != RUN)
        {
            DBBody.animation.GotoAndPlayByFrame(RUNBEGIN, 0, 1);
        }
    }

    bool isJump2 = false;
    bool isJumping2 = false;
    public void getJump()
    {
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
    void jump()
    {
        if (isInAiring)
        {
            if (isJump2 && DBBody.animation.lastAnimationName != JUMP2DUAN)
            {
                isJump2 = false;
                DBBody.animation.GotoAndPlayByFrame(JUMP2DUAN, 0, 1);
                newSpeed.y = 0;
                playerRigidbody2D.velocity = newSpeed;
                playerRigidbody2D.AddForce(Vector2.up * yForce);
                return;
            }
        }
        
        if (IsGround&& DBBody.animation.lastAnimationName!= JUMP2DUAN&& DBBody.animation.lastAnimationName != DOWNONGROUND && DBBody.animation.lastAnimationName != JUMPUP) DBBody.animation.GotoAndPlayByFrame(JUMPUP, 0, 1);

        if (IsGround &&!isQiTiao && DBBody.animation.lastAnimationName == JUMPUP && DBBody.animation.isCompleted)
        {
            isQiTiao = true;
            playerRigidbody2D.AddForce(Vector2.up * yForce);
            return;
        }
    }

    void inAir()
    {

        
        isInAiring = !IsGround;

        if (IsHitMQWall&&isInAiring)
        {
            if (DBBody.animation.lastAnimationName != JUMPHITWALL) DBBody.animation.GotoAndPlayByFrame(JUMPHITWALL,0,1);
        }

        if (DBBody.animation.lastAnimationName == DOWNONGROUND)
        {
            if (DBBody.animation.isCompleted)
            {
                //print("luodidongzuo zuowan");
                isDowning = false;
                isJumping = false;
                isJumping2 = false;
                isQiTiao = false;
            }
            return;
        }


        if ((DBBody.animation.lastAnimationName == JUMPDOWN|| DBBody.animation.lastAnimationName == JUMP2DUAN|| DBBody.animation.lastAnimationName == JUMPHITWALL) && IsGround)
        {
            //落地动作
            if (DBBody.animation.lastAnimationName != DOWNONGROUND) DBBody.animation.GotoAndPlayByFrame(DOWNONGROUND, 0, 1);
        }

        
        if (isInAiring)
        {
            if (newSpeed.y < 0)
            {
                //isJumping = true;
                if (!isDowning)
                {
                    isDowning = true;
                    //print("-------------------> " + newSpeed.y);
                    DBBody.animation.GotoAndPlayByFrame(JUMPDOWN, 0, 1);
                }
            }
            else
            {
                if (isJumping2 && DBBody.animation.lastAnimationName == JUMP2DUAN && !DBBody.animation.isCompleted) return;
                if (DBBody.animation.lastAnimationName != JUMPDOWN)
                {
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

   
    void stand()
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

    // Use this for initialization
    void Start () {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        DBBody = GetComponentInChildren<UnityArmatureComponent>();
        bodyScale = new Vector3(1, 1, 1);
        this.transform.localScale = bodyScale;
    }
	
	// Update is called once per frame
	void Update () {
        //print(DBBody.animation.lastAnimationName);
        CurrentAcName = DBBody.animation.lastAnimationName;
        ControlSpeed();
        inAir();
        if (isJumping)
        {
            jump();
        }
        if (!isInAiring&&!isDowning && !isRunLefting && !isRunRighting&&!isJumping)
        {
            stand();
        }
        
    }


}
