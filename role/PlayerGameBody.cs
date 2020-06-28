using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameBody : GameBody {

    // Use this for initialization
    //void Start () {
    //       GetStart();
    //   }

    // Update is called once per frame
    void Update()
    {

        DimianAtkHuanYuan();
        //攻击修正 防卡死
        AtkXiuZheng();
        //外挂飘动骨骼修正
        BoneOrderXiuzheng();
        //DBTest();
        //连击 如果出问题 就打开 连击计数  作用已经不记得了  延迟不要超过30吧  好像是连击重复什么的
        //LJJiSHu();
        if (Globals.isInPlot) return;
        if (isFighting)
        {
            //脱离战斗 随机 站立姿势 跑步动作什么的
            OutFighting();
        }

        if (GetComponent<RoleDate>().isDie)
        {
            GetPlayerRigidbody2D().velocity = new Vector2(GetPlayerRigidbody2D().velocity.x * 0.5f, GetPlayerRigidbody2D().velocity.y);
        }

        

        this.GetUpdate();

        //print("------  IsAtkDown " + IsAtkDown + "  IsGround   " + IsGround+" isAtk "+isAtk+"   isAtking "+isAtking);

    }

    protected override void FirstStart()
    {
        STAND = "stand_5";
        DIE = "die_4";
        Die_dian.Stop();
        hongdian.Stop();
    }



    public GameObject img_bianziz;
    public GameObject img_bianziy;
    public GameObject img_toufaz;
    public GameObject img_toufazhong;
    public GameObject img_toufay;

    [Header("黑电")]
    public ParticleSystem heidian;
    [Header("红电")]
    public ParticleSystem hongdian;
    public ParticleSystem Hongyan;
    public ParticleSystem Die_dian;

    public void Bianhei()
    {
        img_bianziz.GetComponent<SpriteRenderer>().color = Color.black;
        img_bianziy.GetComponent<SpriteRenderer>().color = Color.black;
        img_toufaz.GetComponent<SpriteRenderer>().color = Color.black;
        img_toufazhong.GetComponent<SpriteRenderer>().color = Color.black;
        img_toufay.GetComponent<SpriteRenderer>().color = Color.black;
        //print(" >>>>>>>>>>>>> 播放特效！！！！！！！  ");


        //if (!thePlayerUI) thePlayerUI = GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>();
        if (!roleDate.isDie && thePlayerUI.ui_shanbi.GetComponent<UI_Shanbi>().GetUseTimes() == 0)
        {
            hongdian.Play();
        }
        else
        {
            heidian.Play();
        }
        
        Hongyan.Play();
    }

    public void Bianbai()
    {
        img_bianziz.GetComponent<SpriteRenderer>().color = Color.white;
        img_bianziy.GetComponent<SpriteRenderer>().color = Color.white;
        img_toufaz.GetComponent<SpriteRenderer>().color = Color.white;
        img_toufazhong.GetComponent<SpriteRenderer>().color = Color.white;
        img_toufay.GetComponent<SpriteRenderer>().color = Color.white;
        //print(">>>>>>>>>>>>> ting---------特效！！！！！！！  ");
        heidian.Stop();
        hongdian.Stop();
        //Hongyan.Stop();
    }


    protected override void ShanjinTX()
    {
        Bianhei();
    }

    protected override void ShanjinOverTX()
    {
        Bianbai();
    }







    bool isTest = false;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="SoltName">修正的骨骼插槽名字</param>
    /// <param name="ACName">当是什么动作的时候生效</param>
    void BoneOrderXiuzheng(string SoltName = "",string ACName = "")
    {
        if (!isTest)
        {
            isTest = true;
            GetDB().armature.GetSlot("辫子右")._SetDisplayIndex(1);
            GetDB().armature.GetSlot("辫子左_1")._SetDisplayIndex(1);
            GetDB().armature.GetSlot("刘海左")._SetDisplayIndex(1);
            GetDB().armature.GetSlot("刘海中")._SetDisplayIndex(1);
            GetDB().armature.GetSlot("刘海右")._SetDisplayIndex(1);



        }
        
        if (GetDB().animation.lastAnimationName == ACName)
        {

        }
        //if (bianzi1) print("辫子1    "+bianzi1.GetComponent<Renderer>().sortingOrder);
        //print(" 头部插槽  "+ (GetDB().armature.GetSlot("头").display as GameObject).GetComponent<Renderer>().sortingOrder);
        //print(" 辫子右  " + (GetDB().armature.GetSlot("辫子右").display as GameObject).GetComponent<Renderer>().sortingOrder);
        //print(" 刘海  " + (GetDB().armature.GetSlot("刘海左").display as GameObject).GetComponent<Renderer>().sortingOrder);

        //(GetDB().armature.GetSlot("头").display as GameObject).GetComponent<Renderer>().sortingOrder = -200;

    }

    void DBTest()
    {

        //if (GetDB().armature.GetBone("辫子右").visible) GetDB().armature.GetBone("辫子右").visible = false;

        //print("  order   "+(GetDB().armature.GetSlot("头").display as GameObject).GetComponent<Renderer>().sortingOrder);
        //(GetDB().armature.GetSlot("头").display as GameObject).GetComponent<Renderer>().sortingOrder = -10;
        //print("> " + (GetDB().armature.GetSlot("头").display as GameObject).GetComponent<Renderer>().sortingOrder);
        //(GetDB().armature.GetSlot("头").display as GameObject).GetComponent<Renderer>().sortingOrder = 2;
        //print((GetDB().armature.GetSlot("头").display as GameObject).GetComponent<Renderer>().sortingOrder);

            //(GetDB().armature.GetSlot("刘海左").display as GameObject).SetActive(false);
            //GetDB().armature.GetBone("刘海左").visible = false;

            //if (o.slot.display != null) (o.slot.display as GameObject).GetComponent<Renderer>().material.color = color;
    }

    public override void GameOver()
    {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);

        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.SHOU_FEIDAO, this.Shoufeidao);

        //TurnRight();
    }

    void Shoufeidao(UEvent e)
    {
        if (!Globals.feidao||GetComponent<RoleDate>().isDie) return;
        //判断一些不能收飞刀的情况  比如  攻击 放技能之类的 被攻击

        //print("收飞刀！");
        //变黑 暂停人物动作 无敌时间
        GetComponent<RoleDate>().isCanBeHit = false;
        //GetComponent<GameBody>().GetBoneColorChange(Color.black);
        //飞刀是否碰墙
        /**if (Globals.feidao.GetComponent<JN_FD>().IsHitWall)
        {

        }*/



        //判断 飞刀相对有人物的方位 
        //根据飞刀方位  选择收刀动作 和人物朝向
        //人物飞刀飞刀位置
        //GlobalTools.FindObjByName("MainCamera").GetComponent<CameraController>().CameraFollow(Globals.feidao);

        //print("Globals.feidao.transform.position   "+ Globals.feidao.transform.position);
        this.transform.position = Globals.feidao.transform.position;
        Globals.cameraIsFeidaoGS = true;

        //人物速度归零
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //人物变白
        //收飞刀动作
        //收刀动作播完 恢复
        //发送 销毁飞刀事件
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.DISTORY_FEIDAO, null), this);
        //Globals.isShouFD = false;

        GetComponent<RoleDate>().isCanBeHit = true;
        //GetComponent<GameBody>().GetBoneColorChange(Color.white);
    }

    void RemoveSelf(UEvent e)
    {
        if(this == null)
        {
            ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
            return;
        }
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.SHOU_FEIDAO, this.Shoufeidao);
        DestroyImmediate(this.gameObject, true);
    }

    private void OnDisable()
    {
        if (this == null)
        {
            ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
            return;
        }
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.SHOU_FEIDAO, this.Shoufeidao);
        //DestroyImmediate(this.gameObject, true);
    }

    bool isFighting = false;
    //切换站立姿势 切换跑动姿势
    void ChangeStandAndRunAC()
    {
        int nums = Random.Range(1, 3);
        nums += 1;
        nums = 5;
        STAND = "stand_" + nums;
        if(nums == 3||nums == 1)
        {
            RUN = "run_3";
        }
        else
        {
            //RUN = "run_" + nums;
            RUN = "run_3";
        }
        CheckIsHasAC();
    }



    public GameObject bianziy;
    public GameObject bianziz;
    [Header("骨骼参数修正")]
    public GameObject XzBone1;


    public GameObject toufaz;
    public GameObject toufazhong;
    public GameObject toufay;


    void ChangeBoneScaleX()
    {
        //print("修正！！！");
        ChangeBoneScaleXByObject(bianziy);
        ChangeBoneScaleXByObject(bianziz);
        ChangeBoneScaleXByObject(toufaz);
        ChangeBoneScaleXByObject(toufazhong);
        ChangeBoneScaleXByObject(toufay);
        Xiuzheng();
    }


    void Xiuzheng()
    {
        Vector3 v3 = XzBone1.GetComponent<SpringBone>().springForce;
        if (this.transform.localScale.x == 1)
        {
            XzBone1.GetComponent<SpringBone>().SpringForceChangeByScaleX(new Vector3(-Mathf.Abs(v3.x),v3.y,v3.z));
        }
        else
        {
            XzBone1.GetComponent<SpringBone>().SpringForceChangeByScaleX(new Vector3(Mathf.Abs(v3.x), v3.y, v3.z));
        }
    }


    //给个参数 可以多个 用
    void ChangeBoneScaleXByObject(GameObject obj)
    {
        //print("角色 的 朝向  "+this.transform.localScale);
        //print("   scale  "+bianzi1.transform.localScale);
        //print("rotation   "+bianzi1.transform.localRotation);
        //print("localEulerAngles   " + bianzi1.transform.localEulerAngles);
        Vector3 v3 = obj.transform.localScale;
        if (this.transform.localScale.x == 1)
        {
            obj.transform.localScale = new Vector3(Mathf.Abs(v3.x), v3.y, v3.z);
            obj.transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            obj.transform.localScale = new Vector3(-Mathf.Abs(v3.x), v3.y, v3.z);
            obj.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }


    public override void RunRight(float horizontalDirection, bool isWalk = false)
    {
        //print("l " + isAtking);
        if (DBBody.animation.lastAnimationName == STAND) DodgeOver();
        isBackUping = false;
        if (roleDate.isBeHiting) return;
        if (isAcing) return;
        if (isDodgeing) return;
        if (!DBBody.animation.HasAnimation(WALK)) isWalk = false;

        if (isTXShow) return;

        if (!isWalk && bodyScale.x == 1)
        {
            AtkReSet();
            
        }

        if (!isWalk){
            bodyScale.x = -1;
            this.transform.localScale = bodyScale;
            ChangeBoneScaleX();
        }

        

        if (isAtk) return;
        //resetAll();
        isAtkYc = false;
        isRunRighting = true;
        isRunLefting = false;

        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        //print("right "+ horizontalDirection + "  xForce "+xForce +"    sudu  "+playerRigidbody2D.velocity.x+"   max   "+maxSpeedX);
        if (playerRigidbody2D.velocity.x > 10) playerRigidbody2D.velocity = new Vector2(maxSpeedX, playerRigidbody2D.velocity.y);
        Run();
    }


    public override void RunLeft(float horizontalDirection, bool isWalk = false)
    {
        if (DBBody.animation.lastAnimationName == STAND) DodgeOver();
        //print("r "+isAtking);
        isBackUping = false;
        if (roleDate.isBeHiting) return;
        if (isAcing) return;
        if (isDodgeing) return;
        if (!DBBody.animation.HasAnimation(WALK)) isWalk = false;
        if (isTXShow) return;

        if (!isWalk && bodyScale.x == -1)
        {
            AtkReSet();
            
        }

        if (!isWalk)
        {
            bodyScale.x = 1;
            this.transform.localScale = bodyScale;
            ChangeBoneScaleX();
        }

        

        if (isAtk) return;

        //resetAll();
        isAtkYc = false;
        isRunLefting = true;
        isRunRighting = false;

        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        //print("left " + horizontalDirection + "  xForce " + xForce + "    sudu  " + playerRigidbody2D.velocity.x + "   max   " + maxSpeedX);
        if (playerRigidbody2D.velocity.x < -10) playerRigidbody2D.velocity = new Vector2(-maxSpeedX, playerRigidbody2D.velocity.y);
        //print("hihihi");
        Run();

    }

    public override void Run()
    {

        if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        //print("isJumping   " + isJumping + "    isDowning  " + isDowning + "   isBeHiting  " + roleDate.isBeHiting + "isInAiring" + isInAiring + "   isDodgeing  " + isDodgeing);
        if (isJumping || isInAiring || isDowning || isDodgeing || roleDate.isBeHiting) return;
        //print("??????   "+isRunLefting +"    "+isRunRighting);
        //if (DBBody.animation.lastAnimationName == RUN|| DBBody.animation.lastAnimationName == STAND) return;

        if (DBBody.animation.lastAnimationName != RUN)
        {
            Hongyan.Stop();
            DBBody.animation.GotoAndPlayByFrame(RUN);
        }
    }




    bool isLJ = false;
    int jishiNum = 0;
    //连击清零
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

    bool IsAtkDown = false;
    bool IsAtkDowning = false;

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
            //print(yanchi+"   >>   "+ (vOAtk.yanchi - canMoveNums));
            //ljTime.GetStopByTime(0.5f);
            if (yanchi > vOAtk.yanchi - canMoveNums)
            {
                isAtk = false;
                if (this.transform.tag != "Player") isAtking = false;
                //print("   -------->isAtk  "+isAtk);
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
                //print("---------------------------------> ok!!  " + yanchi);
                //超过延迟时间 失去连击
                isAtkYc = false;
                IsAtkDown = false;
                IsAtkDowning = false;
                AtkLJOver();
                
                //playerRigidbody2D.gravityScale = 4.5f;
            }

            //print("IsAtkDown " + IsAtkDown + "  IsGround   " + IsGround);
            //如果hi像下攻击 并且 已经落地
            DimianAtkHuanYuan();



            InFightAtk();

        }

     

    
    }


    void AtkXiuZheng()
    {
        if (vOAtk != null)
        {
            //保险措施

            if (DBBody.animation.lastAnimationName != vOAtk.atkName|| yanchi >= vOAtk.yanchi)
            {
                AtkLJOver();

                IsAtkDown = false;
                IsAtkDowning = false;
                isAtkYc = false;
                isTXShow = false;
                //if(Globals.isDebug) print("-------------------修正--------------> ok!!  " + yanchi);
            }


            //if (DBBody.animation.lastAnimationName != vOAtk.atkName)
            //{
            //    AtkLJOver();

            //    IsAtkDown = false;
            //    IsAtkDowning = false;
            //    isAtkYc = false;
            //    isTXShow = false;
            //    print("----------修正！！！！！！");
            //    //playerRigidbody2D.gravityScale = 4.5f;
            //}
            //else
            //{
            //    if (yanchi >= vOAtk.yanchi)
            //    {
            //        print("-------------------修正--------------> ok!!  " + yanchi);
            //        //超过延迟时间 失去连击
            //        isAtkYc = false;
            //        IsAtkDown = false;
            //        IsAtkDowning = false;
            //        isTXShow = false;
            //        AtkLJOver();

            //        //playerRigidbody2D.gravityScale = 4.5f;
            //    }
            //}
        }
       
    }

    //地面攻击还原
    void DimianAtkHuanYuan()
    {
        if (IsAtkDown && IsGround)
        {
            IsAtkDown = false;
            IsAtkDowning = false;
            isAtk = false;
            isAtkYc = false;
            AtkLJOver();
            yanchi = 0;
            //print("  ----------->in! ");
        }
    }



    //取消空中闪进
    void ShanjinStop()
    {
        isShanjin = false;
        isCanShanjin = true;
        isDodgeing = false;
        isDodge = false;
        roleDate.isCanBeHit = true;
        playerRigidbody2D.gravityScale = gravityScaleNums;
        Bianbai();
    }


   


    public override void GetAtk(string atkName = null)
    {
        if (roleDate.isBeHiting) return;
        if (isAcing) return;

        if (isDodgeing) {
            if (!isShanjin) return;
            ShanjinStop();
        }

        //阻止了快落地攻击时候的bug
        //这里会导致AI回跳 进入落地动作而不能进入atk动作 所以回跳的跳起在动画里面做 不在程序里面给Y方向推力
        if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        //if (IsAtkDown) return;
        if (!isAtk)
        {
            if (Globals.isKeyDown)
            {
                if (IsAtkDowning) return;
            }

            isAtk = true;
            isAtking = true;
            isTXShow = true;
            isAtkYc = true;
            yanchi = 0;
            jisuqi = 0;
            isLJ = false;
            jishiNum = 0;
            //print("  dianji gongji ");

            if (isInAiring)
            {
                if (Globals.isKeyUp)
                {
                    atkZS = DataZS.jumpAtkUpZS;
                    IsAtkDowning = false;
                }
                else if (Globals.isKeyDown)
                {
                   
                    atkZS = DataZS.jumpAtkDownZS;
                    IsAtkDown = true;
                    //print("  theTimer     " + _theTimer);
                    //_theTimer.TimesAdd(0.5f, CanAtkDownCallBack);
                    //GetPause(0.2f, 0.5f);
                }
                else
                {
                    atkZS = DataZS.jumpAtkZS;
                    IsAtkDowning = false;
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
                IsAtkDowning = false;
                if (Globals.isKeyUp)
                {
                    atkZS = DataZS.atkUpZS;
                }
                else
                {
                    atkZS = DataZS.atkZS;
                }
                
            }

            if (IsAtkDown && IsAtkDowning) return;

            if (IsAtkDown&&!IsAtkDowning)
            {
                IsAtkDowning = true;
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
            //MoveVX(0, true);

            //print(newSpeed.y);
            //获取XY方向的推力 
            //print(DBBody.animation.animations);

        }

    }



    void AtkLJOver()
    {
        isAtking = false;
        isAtk = false;
        isTXShow = false;
        yanchi = 0;
        //atkNums = 0;
    }


    //TheTimer ljTime = new TheTimer();


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
        if (!DBBody.animation.HasAnimation(STAND)) STAND = "stand_5";
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
        //print("  >>>>>>>>>>>>>>>>>>>?????   Behit  ");
        if (DBBody.animation.lastAnimationName == DODGE1) return;
        ResetAll();
        Bianbai();
        if (IsAtkDowning)
        {
            IsAtkDowning = false;
            IsAtkDown = false;
        }
        roleDate.isBeHiting = true;
        inFightNums = 0;
        mnum = 0;
        InFightAtk();
        ChangeStandAndRunAC();
        Time.timeScale = 0.5f;
        //print(" Time.timeScale  "+ Time.timeScale);
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

        isGetJump = false;
        isGetJumpOnWall = false;
        if (!IsGround)
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
      
        //print("   getbehit>>>????????>1111111 "+ DBBody.animation.lastAnimationName+"  ?  "+BEHIT+"   pro  "+ roleDate.isBeHiting);
        if (DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == BEHITINAIR)
        {
            

            float jindu = DBBody.animation.lastAnimationState.currentTime / DBBody.animation.lastAnimationState.totalTime;
            //print("jindu    "+ jindu+"    inair "+isInAiring+"   是否着地  "+IsGround+"    动作  "+ DBBody.animation.lastAnimationName);

            if (!IsGround && DBBody.animation.lastAnimationName == BEHIT) {
                //print("地上进入 空中   "+ DBBody.animation.lastAnimationName +"   jindu  "+ jindu);
                DBBody.animation.GotoAndPlayByProgress(BEHITINAIR, jindu, 1);
            }

            if (IsGround && DBBody.animation.lastAnimationName == BEHITINAIR)
            {
                DBBody.animation.GotoAndPlayByProgress(BEHIT, jindu, 1);
            }
            


            mnum++;
            if (mnum > 30)
            {
                mnum = 0;
                Time.timeScale = 1;
            }

        }

       
        if ((DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == BEHITINAIR) && DBBody.animation.isCompleted)
        {
            roleDate.isBeHiting = false;
            mnum = 0;
            Time.timeScale = 1;
            //print("???  ----behit!!!!!  ");
            if (IsGround) GetStand();
        }
    }

    bool IsBianhei = false;
    override public void GetDie()
    {
        if (DBBody.animation.lastAnimationName != DIE) {
            //if (!isInAiring)
            //{
            //    int nums = Random.Range(1, 3);
            //    DIE = "die_" + nums;
            //}
            //if (!DBBody.animation.HasAnimation(DIE)) DIE = "die_1";
            Die_dian.Play();
            
            GetPlayerRigidbody2D().gravityScale = 0;
            GetPlayerRigidbody2D().velocity = new Vector2(0,1f);
            DBBody.animation.GotoAndPlayByFrame(DIE, 0, 1);
            DBBody.animation.Play(DIE,1);
            Time.timeScale = 0.2f;
        }

        //print(DBBody.animation.lastAnimationState.fadeTotalTime+"  ------->  "+ DBBody.animation.lastAnimationState.currentPlayTimes+"  @@  "+ DBBody.animation.lastAnimationState.currentTime);
        //print(" progress   "+DBBody.animation.lastAnimationState);
        if (!IsBianhei&&DBBody.animation.lastAnimationState.currentTime >= 0.31f)
        {
            IsBianhei = true;
            
            Bianhei();
        }

        //Time.timeScale = 0.5f;
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.DIE_OUT,this.tag), this);
        if (isDieRemove) StartCoroutine(IEDieDestory(1f));
    }


    protected override void InAir()
    {
        //print(DBBody.animation.lastAnimationName+"   speedy  "+ newSpeed.y);

        if (playerRigidbody2D.velocity.y < -MaxSpeedY) {
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, -MaxSpeedY);
        }
        

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
            Hongyan.Stop();
            //print("jinlai  mei         ");
            return;
        }


        if (isDodgeing || isAcing) return;
        isInAiring = !IsGround;
        if (IsGround && DBBody.animation.lastAnimationName == DOWNONGROUND)
        {
            if (DBBody.animation.isCompleted)
            {
                print("luodidongzuo over!!!!!!!");
                isDowning = false;
                isJumping = false;
                isJumping2 = false;
                isQiTiao = false;
                isJump2 = false;
                isAtkYc = false;
                //落地还原 不然 地上攻击会累加
                atkNums = 0;
                //print("jinlai  mei     222222222    ");
                DBBody.animation.GotoAndPlayByFrame(STAND, 0, 1);
                //GetStand();
            }
            return;
        }


        //print("isqitiao  "+isQiTiao);

        if (IsGround && !isBackUping && (DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == JUMPDOWN || DBBody.animation.lastAnimationName == JUMP2DUAN || DBBody.animation.lastAnimationName == JUMPHITWALL))
        {
            //落地动作
            if (DBBody.animation.lastAnimationName != DOWNONGROUND)
            {
                DBBody.animation.GotoAndPlayByFrame(DOWNONGROUND, 0, 1);
                isAtkYc = false;
                isAtking = false;
                isAtk = false;
                Hongyan.Stop();
                print(" --------------------------------------------////////////////////  ----------------? ?????  ");
                //AtkLJOver(); 
                //isGetJump = false;
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


        if (newSpeed.y < -20f)
        {
            //print("newSpeed.y >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>   " + newSpeed.y);
            if (Hongyan.isStopped)
            {
                
                Hongyan.Play();
            } 
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
                if ((DBBody.animation.lastAnimationName == JUMP2DUAN || DBBody.animation.lastAnimationName == JUMPHITWALL || DBBody.animation.lastAnimationName == RUNBEGIN) && !DBBody.animation.isCompleted) return;
                if (DBBody.animation.lastAnimationName != JUMPDOWN)
                {
                    //上升
                    //print("shangsheng");
                    //newSpeed.y >0 的时候是上升  这个是起跳动作完成后 上升的时候 停留在下降的最后一帧 
                    //做动画的时候  下落动画第一帧就是 起跳最后一帧
                    DBBody.animation.GotoAndPlayByFrame(JUMPDOWN, 0, 1);
                    DBBody.animation.Stop();
                    isDowning = false;
                    //Hongyan.Stop();
                }
            }
        }
    }


    bool isGetJump = false;
    int maxJumpNums = 2;
    int jumpNums = 1;
    public override void GetJump()
    {
        if (roleDate.isBeHiting) return;
        if (isDodgeing) return;
        if (isAcing) return;
       /* print("jump "+jumpNums+"   jump2 "+isJumping2);
        print(roleDate.isBeHiting + "      " + DBBody.animation.lastAnimationName + "   isOn " + IsGround+" atk "+isAtking+" isAtk "+isAtk);*/
        Jump();
    }

    
    protected override void Jump()
    {
        if (isDodgeing || isAtk || roleDate.isBeHiting) return;
        
        if (jumpNums < 1) return;
        jumpNums--;
        //print("jump num "+jumpNums+ "  isjump "+isJumping+ "  IsGround?  " + IsGround);
        isGetJump = true;
        isGetJumpOnWall = true;
        //return;
        if (jumpNums == 0)
        {
            //print("zhe shi 0  "+jumpNums+"  好像第一次还是 onGround状态 被拉回来跳了jump1了  "+IsGround);
            if (DBBody.animation.lastAnimationName != JUMP2DUAN)
            {
                JumpHitWall();
                

                DBBody.animation.GotoAndPlayByFrame(JUMP2DUAN, 0, 1);

                newSpeed.y = 0.1f;
                //isJumping2 = true;
                playerRigidbody2D.velocity = newSpeed;
                playerRigidbody2D.AddForce(Vector2.up * yForce);
            }
        }
        else
        {
            //print("JINGLAIMEI  " + jumpNums);
            if (!roleDate.isBeHiting &&
                DBBody.animation.lastAnimationName != JUMP2DUAN &&
                DBBody.animation.lastAnimationName != JUMPUP)
            {
                JumpHitWall();

                newPosition = this.transform.localPosition;
                MoveYByPosition(0.4f);
                //this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 10f);

                playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, 0);
                DBBody.animation.GotoAndPlayByFrame(JUMPUP, 0, 1);
                playerRigidbody2D.AddForce(Vector2.up * yForce);
            }
        }
    }

    void JumpHitWall()
    {
        if (DBBody.animation.lastAnimationName == JUMPHITWALL)
        {
            newPosition = this.transform.localPosition;
            
            if (bodyScale.x == 1)
            {
                MoveXByPosition(0.4f);
                playerRigidbody2D.AddForce(Vector2.right * wallJumpXNum);
            }
            else
            {
                MoveXByPosition(-0.4f);
                playerRigidbody2D.AddForce(Vector2.left * wallJumpXNum);
                
            }
        }
    }





    protected override void IsCanShanjinAndJump()
    {
        if (IsGround || IsHitMQWall)
        {
            isCanShanjin = true;
        }



        if (IsGround)
        {
            //print("isStand ");
            //print("isAcing> " + isAcing + " isAtk> " + isAtk + " roleDate.isBeHiting " + roleDate.isBeHiting + " jumpNums> " + jumpNums + " isGetJump> " + isGetJump);
            //跳之前 位置先移出判断区  解决方法2
            /*if (isAcing || isAtk || roleDate.isBeHiting) isGetJump = false;
            if (isGetJump && isDowning) isGetJump = false;
            isDowning = false;
            if (!isGetJump) jumpNums = maxJumpNums;*/

            isDowning = false;
            jumpNums = maxJumpNums;
            //print("isGetJump> " + isGetJump + " jumpNums>" + jumpNums);
        }


        if (IsHitMQWall)
        {
            //print(IsGround);
            if (isAcing || isAtking || roleDate.isBeHiting) isGetJumpOnWall = false;
            if (isGetJumpOnWall) isGetJumpOnWall = false;
            if (!isGetJumpOnWall) jumpNums = maxJumpNums;
            if (!isHitMQWall)
            {
                isHitMQWall = true;
                ClearBodySpeedY();
            }
        }
        else
        {
            isHitMQWall = false;
        }

    }

    bool isHitMQWall = false;


    protected void ClearBodySpeedY()
    {
        GetPlayerRigidbody2D().velocity = Vector2.zero;
    }

    bool isGetJumpOnWall = false;

    protected override void InStand()
    {
        //print("?????????????");
        //print("回到 stand   isatking> " + isAtking + "  isAtk " + isAtk + "  isDown " + isDowning + "  isJump2  " + isJump2 + "  isjumping2 " + isJumping2 + "  isjumping " + isJumping);
        if (!roleDate.isBeHiting && !isQianhuaing && !isInAiring && !isDowning && !isRunLefting && !isRunRighting && !isJumping && !isJumping2 && !isAtking && !isDodgeing && !isAtkYc && !isBackUping)
        {
            //if (this.tag != "diren") print("stand" + "  ? " + isRunLefting + "   " + DBBody.animation.lastAnimationName);

            Stand();
        }
    }


    public override void TurnRight()
    {
        if (GetComponent<RoleDate>().isDie) return;

        //gameControl 里面 awake 调用的  player里面没有初始化 所以会报错
        //RunRight(1);
        this.transform.localScale = new Vector3(-1, 1, 1);
        ChangeBoneScaleX();
    }

    public override void TurnLeft()
    {
        if (GetComponent<RoleDate>().isDie) return;
        //RunLeft(1);
        this.transform.localScale = new Vector3(1, 1, 1);
        
    }


    string turnFX = "";
    public void TrunFXStrRight()
    {
        turnFX = "r";
    }

    public void TrunFXStrLeft()
    {
        turnFX = "l";
    }



    protected override void Stand()
    {
        //print("  进门GlobalSetDate.instance.roleDirection     " + GlobalSetDate.instance.roleDirection);
       

        if(GlobalSetDate.instance.roleDirection == "l"|| turnFX == "l")
        {
            TurnLeft();
            print(" stand 中 左转？？？？？    ");
            GlobalSetDate.instance.roleDirection = "";
            turnFX = "";
        }
        else if(GlobalSetDate.instance.roleDirection == "r"|| turnFX == "r")
        {
            TurnRight();
            print(" stand -----右转？？？？？    ");
            ChangeBoneScaleX();
            GlobalSetDate.instance.roleDirection = "";
            turnFX = "";
        }
        
        //TurnRight();
        //if (DBBody.animation.lastAnimationName == DODGE2|| DBBody.animation.lastAnimationName == DODGE1) return;
        if (roleDate.isBeHiting) return;
        if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        //print(">  "+DBBody.animation.lastAnimationName+"   atking "+isAtking);


        if (DBBody.animation.lastAnimationName != STAND || (DBBody.animation.lastAnimationName == STAND && DBBody.animation.isCompleted))
        {
            if (this.tag == "player")
            {
                DBBody.animation.GotoAndPlayByFrame(STAND, 0, 1);
                isGetJump = false;
            }
            else
            {
                //DBBody.animation.GotoAndPlayByFrame(STAND, 0, 1);
                //时间0.01f  0.1秒 慢了会报错（位置错误）
                DBBody.animation.FadeIn(STAND, 0.01f, 1);
            }

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

    public override bool IsGround
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            grounded = Physics2D.Linecast(start, end, groundLayer);

            

            /*Vector2 start2;
            Vector2 end2;
            if (!grounded && groundCheck3 != null)
            {

                start2 = groundCheck3.position;
                end2 = new Vector2(start2.x, start2.y - distance);
                Debug.DrawLine(start2, end2, Color.blue);
                grounded = Physics2D.Linecast(start2, end2, groundLayer);
                //print("??????????????????????????????>>  "+ grounded);
            }*/

            return grounded;
        }
    }


    public override void GetSit2()
    {
        if (!Globals.isInPlot)
        {
            Globals.isInPlot = true;
            if(DBBody.animation.lastAnimationName!= "sit_2")
            {
                DBBody.animation.GotoAndPlayByFrame("sit_2", 0, -1);
            }
        }
        else
        {
            Globals.isInPlot = false;
        }
    }
}
