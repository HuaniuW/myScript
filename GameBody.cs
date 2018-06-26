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
    public float distanceMQ=0.13f;

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
        //isInAiring = false;
        isDowning = false;
        isJumping = false;
        isJumping2 = false;
        isJump2 = false;
        isQiTiao = false;
        isAtk = false;
        isAtking = false;
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
    const string ATK = "atk_";

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
        if (isAtking) return;
        isRunLefting = true;
        isRunRighting = false;
        bodyScale.x = 1;
        this.transform.localScale = bodyScale;
        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        run();

    }

    public void runRight(float horizontalDirection)
    {
        if (isAtking) return;
        //print("right:  "+Vector2.right*xForce+"  >> "+xForce);
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

    void moveXByPosition(float xDistance)
    {
        newPosition.x += xDistance;
        this.transform.localPosition = newPosition;
    }


    void moveVX(float vx)
    {
        var _vx = Mathf.Abs(vx);
        if (vx > 0)
        {
            playerRigidbody2D.AddForce(Vector2.right * _vx);
        }
        else if (vx < 0)
        {
            playerRigidbody2D.AddForce(Vector2.left * _vx);
        }
    }

    void jump()
    {
        if (isAtking) return;
        if (isInAiring)
        {
            if (isJump2 && DBBody.animation.lastAnimationName != JUMP2DUAN)
            {
                isJump2 = false;
                if (DBBody.animation.lastAnimationName == JUMPHITWALL) {
                    newPosition = this.transform.localPosition;
                    if (bodyScale.x == 1)
                    {
                        //bodyScale.x = -1;
                      
                        moveXByPosition(0.1f);
                        playerRigidbody2D.AddForce(Vector2.right * wallJumpXNum);
                    }
                    else
                    {
                        //bodyScale.x = 1;
                        moveXByPosition(-0.1f);
                        playerRigidbody2D.AddForce(Vector2.left * wallJumpXNum);
                    }
                    //this.transform.localScale = bodyScale;


                    
                    //this.transform.localPosition = newPosition;
                }
                isJumping2 = true;
                //print("???????????");
                DBBody.animation.GotoAndPlayByFrame(JUMP2DUAN, 0, 1);
                newSpeed.y = 0;
                playerRigidbody2D.velocity = newSpeed;
                playerRigidbody2D.AddForce(Vector2.up * yForce);
                return;
            }
        }
        
        if (IsGround&& DBBody.animation.lastAnimationName != JUMPHITWALL && 
            DBBody.animation.lastAnimationName!= JUMP2DUAN && 
            DBBody.animation.lastAnimationName != DOWNONGROUND && 
            DBBody.animation.lastAnimationName != JUMPUP) DBBody.animation.GotoAndPlayByFrame(JUMPUP, 0, 1);

        if (IsGround &&!isQiTiao && DBBody.animation.lastAnimationName == JUMPUP && DBBody.animation.isCompleted)
        {
            isQiTiao = true;
            playerRigidbody2D.AddForce(Vector2.up * yForce);
            return;
        }
    }

    void inAir()
    {

       // print(DBBody.animation.lastAnimationName+"   speedy  "+ newSpeed.y);
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
            }
            return;
        }


        if (IsGround&&(DBBody.animation.lastAnimationName == JUMPDOWN|| DBBody.animation.lastAnimationName == JUMP2DUAN|| DBBody.animation.lastAnimationName == JUMPHITWALL))
        {
            //落地动作
            if (DBBody.animation.lastAnimationName != DOWNONGROUND) DBBody.animation.GotoAndPlayByFrame(DOWNONGROUND, 0, 1);
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
            
            if (newSpeed.y < 0)
            {
                //isJumping = true;
                if (!isDowning)
                {
                    //下降
                    isDowning = true;
                   // print("xj");
                    //print("-------------------> " + newSpeed.y);
                    DBBody.animation.GotoAndPlayByFrame(JUMPDOWN, 0, 1);
                }
            }
            else
            {
                if (isJumping2 && (DBBody.animation.lastAnimationName == JUMP2DUAN|| DBBody.animation.lastAnimationName == JUMPHITWALL) && !DBBody.animation.isCompleted) return;
                if (DBBody.animation.lastAnimationName != JUMPDOWN)
                {
                    //print("ss");
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

    float atkNums = 1;
    bool isAtk = false;
    bool isAtking = false;
    
    public void getAtk()
    {
        if (!isAtk)
        {
            isAtk = true;
            isAtking = true;
            //print(DataZS.atk_1[0]);
            
            if (DBBody.animation.lastAnimationName != ATK + atkNums) DBBody.animation.GotoAndPlayByFrame((ATK + atkNums),0,1);
            //print(DBBody.animation.animations);
           
        }
      
    }

    void test(string type, EventObject eventObject)
    {
        print(type+" ???time  "+eventObject);
    }

    float jisuqi = 0;

    void atk()
    {
        if (DBBody.animation.lastAnimationName == (ATK + atkNums) && DBBody.animation.isPlaying) jisuqi++;
        if (DBBody.animation.lastAnimationName == (ATK + atkNums) && DBBody.animation.isCompleted)
        {
            isAtking = false;
            isAtk = false;
            print("jisuqi  "+jisuqi);
            jisuqi = 0;
            if (atkNums <= 3)
            {
                atkNums++;
            }
            if (atkNums == 4) atkNums = 1;
        }
    }

    // Use this for initialization
    void Start () {
        //Tools.timeData();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        DBBody = GetComponentInChildren<UnityArmatureComponent>();
        //DBBody.AddDBEventListener(EventObject.FRAME_EVENT, this.test);
        DBBody.AddDBEventListener("atks", this.test);
        bodyScale = new Vector3(1, 1, 1);
        this.transform.localScale = bodyScale;
    }
	
	// Update is called once per frame
	void Update () {
        //print(DBBody.animation.lastAnimationName);
        CurrentAcName = DBBody.animation.lastAnimationName;
        ControlSpeed();

        

        inAir();

        if (isAtking)
        {
            atk();
        }

        if (isJumping)
        {
            jump();
        }
        if (!isInAiring&&!isDowning && !isRunLefting && !isRunRighting&&!isJumping&&!isAtking)
        {
            stand();
        }
        
    }


}
