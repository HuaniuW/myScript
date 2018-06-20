using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;


public class QiYue : MonoBehaviour {
    Rigidbody2D playerRigidbody2D;

    [Header("水平速度")]
    public float speedX;

    [Header("水平方向")]
    public float horizontalDirection;

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

    [Header("垂直向上的推力")]
    public float yForce;

    [Header("感应地板的距离")]
    [Range(0, 1)]
    public float distance;

    [Header("侦测地板的射线起点")]
    public UnityEngine.Transform groundCheck;

    [Header("地面图层")]
    public LayerMask groundLayer;

    [Header("是否着地")]
    public bool grounded;



    float slideNum = 4;

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


    public bool jumpKey
    {
        get
        {
            return Input.GetKeyDown(KeyCode.UpArrow);
        }
    }

    bool isRun = false;
    bool isRuning = false;

   
    bool isInAiring = false;
    bool isDowning = false;

    bool isJump = false;
    bool isJumping = false;
    bool isQiTiao = false;

    void resetAll() {
        isRun = false;
        isRuning = false;
        isInAiring = false;
        isDowning = false;
        isJump = false;
        isJumping = false;
        isQiTiao = false;
    }


    void inAir()
    {
        isInAiring = !IsGround;
        if (isInAiring)
        {
           // print("-------------------> " + newSpeed.y);
            if (newSpeed.y < 0)
            {
                isJumping = true;
                if (!isDowning)
                {
                    isDowning = true;
                    qiyue.animation.GotoAndPlayByFrame(JUMPDOWN, 0, 1);
                }
                

            }
            else
            {
                if (qiyue.animation.lastAnimationName != JUMPDOWN)
                {
                    //newSpeed.y >0 的时候是上升  这个是起跳动作完成后 上升的时候 停留在下降的最后一帧 
                    //做动画的时候  下落动画第一帧就是 起跳最后一帧
                    qiyue.animation.GotoAndPlayByFrame(JUMPDOWN, 0, 1);
                    qiyue.animation.Stop();
                    isDowning = false;
                }
            }

           
        }
    }


    void TryJump()
    {
        if (!isJump && !isJumping && IsGround && jumpKey)
        {
            isJump = false;
            isJumping = true;
            if (qiyue.animation.lastAnimationName != JUMPUP) qiyue.animation.GotoAndPlayByFrame(JUMPUP, 0, 1);

        }

    }
    Vector3 tt;

    UnityArmatureComponent qiyue;
    // Use this for initialization
    void Start() {
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        qiyue = GetComponentInChildren<UnityArmatureComponent>();
        //print("qiyue"   +qiyue);
        //qiyue.animation.GotoAndPlayByFrame("run_3_复制1");

        //dbTest.animation.timeScale = 2f;
        tt = new Vector3(1, 1, 1);
        this.transform.localScale = tt;
        //print(this.transform.localScale.x);



    }


    const string RUN = "run_3";
    const string STAND = "stand_1";
    const string RUNBEGIN = "runBegin_1";
    const string RUNSTOP = "runStop_1";
    const string JUMPUP = "jumpUp_1";
    const string JUMPDOWN = "jumpDown_1";
    const string JUMPHITWALL = "jumpHitWall_1";
    const string DOWNONGROUND = "downOnGround_1";



    void run()
    {

        if (isJumping && qiyue.animation.lastAnimationName == DOWNONGROUND) return;

        if (horizontalDirection > 0)
        {
            tt.x = -1;
        }
        else if (horizontalDirection < 0)
        {
            tt.x = 1;
        }

        this.transform.localScale = tt;
        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));

        if (isInAiring || (qiyue.animation.lastAnimationName == JUMPUP || 
            qiyue.animation.lastAnimationName == JUMPDOWN
            )) {
            return;
        } ;
        if(qiyue.animation.lastAnimationName == RUNBEGIN&& qiyue.animation.isCompleted)
        {
            qiyue.animation.GotoAndPlayByFrame(RUN);
        }
        if (qiyue.animation.lastAnimationName != RUNBEGIN
            && qiyue.animation.lastAnimationName != RUN)
        {
            qiyue.animation.GotoAndPlayByFrame(RUNBEGIN,0,1);
        }

        
    }


    void jump()
    {
        if (qiyue.animation.lastAnimationName == DOWNONGROUND)
        {
            if (qiyue.animation.isCompleted)
            {
                isDowning = false;
                isJumping = false;
                isQiTiao = false;
                
            }
            return;
        }


        if (qiyue.animation.lastAnimationName == JUMPDOWN&&IsGround)
        {
            //落地动作
            if (qiyue.animation.lastAnimationName != DOWNONGROUND)qiyue.animation.GotoAndPlayByFrame(DOWNONGROUND, 0, 1);
        }


       

        if (!isQiTiao&& IsGround && qiyue.animation.lastAnimationName == JUMPUP&& qiyue.animation.isCompleted)
        {
            isQiTiao = true;
            playerRigidbody2D.AddForce(Vector2.up * yForce);
        }
    }

    void stand()
    {
        
        if (qiyue.animation.lastAnimationName != STAND) qiyue.animation.GotoAndPlayByFrame(STAND);
        if (qiyue.animation.lastAnimationName == DOWNONGROUND) return;
        if (newSpeed.x > slideNum)
        {
            newSpeed.x = slideNum-1;
        }
        else if (newSpeed.x < -slideNum)
        {
            newSpeed.x = -slideNum+1;
        }

        playerRigidbody2D.velocity = newSpeed;

    }

   
    void action()
    {
        horizontalDirection = Input.GetAxis(HORIZONTAL);
        if (horizontalDirection != 0)
        {
            isRuning = true;
        }
        else
        {
            isRuning = false;
            
        }

        inAir();
        //tt.x = horizontalDirection;

        //print("horizontalDirection>   " + horizontalDirection);
        if (isRuning)
        {
            run();
        }

        if (isJumping)
        {
            jump();
            return;
        }




        if (!isRuning && !isJumping &&!isInAiring)
        {
            stand();
        }
        


        
    }

    // Update is called once per frame
    void Update()
    {
        //		speedX = playerRigidbody2D.velocity.x;
        ControlSpeed();
        action();
        TryJump();
      
    }

}
