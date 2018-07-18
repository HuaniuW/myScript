using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using System;

public class GameBody : MonoBehaviour {

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

    void ResetAll()
    {
        //isRun = false;
        isRunLefting = false;
        isRunRighting = false;
        //isInAiring = false;
        //isDowning = false;
        isJumping = false;
        isJumping2 = false;
        isJump2 = false;
        //isQiTiao = false;
        isAtk = false;
        isAtking = false;
        atkNums = 0;
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
    const string DODGE1 = "dodge_1";
    const string DODGE2 = "dodge_2";


    bool isSkill = false;
    bool isSkilling = false;
    internal void GetSkill1()
    {
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
        if (isInAiring||DBBody.animation.lastAnimationName== DOWNONGROUND) return;
        if (!isDodge)
        {
            ResetAll();
            isDodge = true;
            isDodgeing = true;
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

    public void RunLeft(float horizontalDirection)
    {
        if (isAtking || isDodgeing) return;
        //resetAll();
        isRunLefting = true;
        isRunRighting = false;
        bodyScale.x = 1;
        this.transform.localScale = bodyScale;
        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        Run();

    }

    public void RunRight(float horizontalDirection)
    {
        if (isAtking || isDodgeing) return;
        //resetAll();
        isRunRighting = true;
        isRunLefting = false;
        bodyScale.x = -1;
        this.transform.localScale = bodyScale;
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
        if (isJumping || isInAiring || isDowning || isDodgeing) return;
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
    //bool isQiTiao = false;
    public void GetJump()
    {
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
        if (isAtking|| isDodgeing) return;
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
                newSpeed.y = 0;
                playerRigidbody2D.velocity = newSpeed;
                playerRigidbody2D.AddForce(Vector2.up * yForce);
                return;
            }
        }

       
        if (IsGround &&!isAtking&& DBBody.animation.lastAnimationName != JUMPHITWALL &&
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
                //落地还原 不然 地上攻击会累加
                atkNums = 0;
            }
            return;
        }

       


        if (IsGround&&(isQiTiao || DBBody.animation.lastAnimationName == JUMPDOWN|| DBBody.animation.lastAnimationName == JUMP2DUAN|| DBBody.animation.lastAnimationName == JUMPHITWALL))
        {
            //落地动作
            if (DBBody.animation.lastAnimationName != DOWNONGROUND)
            {
                DBBody.animation.GotoAndPlayByFrame(DOWNONGROUND, 0, 1);
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

    float atkNums = 0;
    bool isAtk = false;
    bool isAtking = false;
    string[] atkMsg;
    VOAtk vOAtk;
    Dictionary<string, string>[] atkZS;

    public void GetAtk()
    {
        if (isDodgeing) return;
        if (!isAtk)
        {
            //resetAll();
            isAtk = true;
            isAtking = true;
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
            
            vOAtk.getVO(atkZS[(int)atkNums]);
            if (DBBody.animation.lastAnimationName != vOAtk.atkName)
            {
                
                DBBody.animation.GotoAndPlayByFrame(vOAtk.atkName, 0, 1);
                MoveVX(vOAtk.xF);
                if (newSpeed.y < 0)
                {
                    newSpeed.y = 0;
                    playerRigidbody2D.velocity = newSpeed;
                    MoveVY(vOAtk.yF);
                }
            }
            //获取XY方向的推力 
            //print(DBBody.animation.animations);
           
        }
      
    }

    void Test(string type, EventObject eventObject)
    {
        print(type+" ???time  "+eventObject);
    }

    //特效方向
    void TXPlay(ParticleSystem tx) {
        tx.Stop();
        Vector3 ttt = new Vector3(0, 0, 0);
        ttt = tx.transform.localScale;
        ttt.x = Mathf.Abs(tx.transform.localScale.x);
        ttt.x *= this.transform.localScale.x;
        tx.transform.localScale = ttt;
        tx.Play();
    }

    float jisuqi = 0;
    float yanchi = 0;

    public object GetPropertyValue(Dictionary<string, object> _values,string propertyName)
    {
       
        if (_values.ContainsKey(propertyName) == true)
        {
            return _values[propertyName];
        }
        return null;
    }
    /// <summary

    void Atk()
    {
        if (DBBody.animation.lastAnimationName == vOAtk.atkName && DBBody.animation.isPlaying)
        {
            jisuqi++;
            //print("jisuqi "+jisuqi+"    ??    "+ vOAtk.showTXFrame);
            //特效出现时间
            if(jisuqi == vOAtk.showTXFrame){
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

                if (vOAtk.txName == "tx_1")
                {
                    TXPlay(tx_1);
                } else if (vOAtk.txName == "tx_2") {
                    TXPlay(tx_2);
                } else if (vOAtk.txName == "tx_3") {
                    TXPlay(tx_3);
                } else if (vOAtk.txName == "tx_4") {
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
            if(yanchi>= vOAtk.yanchi)
            {
                //超过延迟时间 失去连击
                isAtking = false;
                yanchi = 0;
                atkNums = 0;
            }
        }
    }

    // Use this for initialization
    void Start () {
        //Tools.timeData();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        DBBody = GetComponentInChildren<UnityArmatureComponent>();
        //DBBody.AddDBEventListener(EventObject.FRAME_EVENT, this.test);
        DBBody.AddDBEventListener("atks", this.Test);
        bodyScale = new Vector3(1, 1, 1);
        vOAtk = GetComponent<VOAtk>();
        this.transform.localScale = bodyScale;
    }
	
	// Update is called once per frame
	void Update () {
        CurrentAcName = DBBody.animation.lastAnimationName;
        ControlSpeed();
        InAir();

        if (isDodgeing)
        {
            Dodge1();
            return;
        }

        if (isAtking)
        {
            Atk();
        }

        if (isJumping)
        {
            Jump();
        }
        if (!isInAiring&&!isDowning && !isRunLefting && !isRunRighting&&!isJumping&&!isAtking&&!isDodgeing)
        {
            Stand();
        }
        
    }

}

