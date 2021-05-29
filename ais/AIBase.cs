using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AIBase : MonoBehaviour {

    protected GameBody gameBody;
    public GameObject thePlayer;
    protected AIQiShou aiQishou;
    protected AIFanji aiFanji;
    protected RoleDate _roleDate;


    [Header("是否是不能动的 怪物")]
    public bool IsCanNotMove = false;

	// Use this for initialization
	void Start () {
        GetStart();
    }

   




    [Header("碰到开战块 打开 boss行动 直接打开的话boss行动-测试用")]
    public bool IsBossStop = false;

    [Header("是否能退回防守区")]
    public bool IsCanTuihuiFSQ = true;

    //初始的  x 位置 用来 退回防守区
    float _ChuSshiX = 0;

    public bool IsTuihuiFangshouquing = false;

    float tuihuiSpeed = 2;

    void TuihuiFangshouqu()
    {
        if (!IsCanTuihuiFSQ) return;
        if (!IsTuihuiFangshouquing) return;

        if (Mathf.Abs(this.transform.position.x - _ChuSshiX)<1|| gameBody.IsHitWall)
        {
            IsTuihuiFangshouquing = false;
            isActioning = false;
            gameBody.ResetAll();
            gameBody.SpeedXStop();
            gameBody.GetStand();
            return;
        }

        if(this.transform.position.x< _ChuSshiX)
        {
            //右移动
            gameBody.RunRight(tuihuiSpeed);
        }
        else
        {
            //左移动
            gameBody.RunLeft(-tuihuiSpeed);
        }



        if (IsTuihuiFangshouquing)
        {
            return;
        }
    }



    protected void GetStart()
    {
        //GetKuangDownY();//获取 kuang 底部的 y
        GetGameBody();
        _roleDate = GetComponent<RoleDate>();
        if (GetComponent<AIQiShou>()) aiQishou = GetComponent<AIQiShou>();
        if (!aiFanji) aiFanji = GetComponent<AIFanji>();
        _ChuSshiX = this.transform.position.x;
        //Type myType = typeof(DataZS);
        myPosition = this.transform.position;
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GET_ENEMY, GetEnemyObj);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.BOSS_IS_OUT, BossFight);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.JINGSHI, this.JingshiEvent);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHOSE_EVENT, this.ChoseEvent);
        //print("   start jinlai mei!!!! "+thePlayer);
        //if (thePlayer == null)
        //{
        //    IsCanTuihuiFSQ = false;
        //    thePlayer = GlobalTools.FindObjByName("player");
        //    isAction = true;
        //}

        if(GetComponent<RoleDate>().enemyType == "boss")
        {
            GetComponent<RoleDate>().isCanBeHit = false;
        }
    }


  


    void ChoseEvent(UEvent e)
    {
        string EStr = e.eventParams.ToString();
        if (EStr == EventTypeName.STATR_FIGHT)
        {
            //可以做延迟 几秒 开始
            StartCoroutine(IPoltPlay(0.8f));
        }
    }

    public IEnumerator IPoltPlay(float time)
    {
        //Debug.Log("time   "+time);
        //yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(time);
        IsBossStop = false;
    }

    void BossFight(UEvent e)
    {
        IsBossStop = false;
        GetComponent<RoleDate>().isCanBeHit = true;
    }

    public bool isPlayerDie = false;


  

    protected virtual void GetGameBody()
    {
        gameBody = GetComponent<GameBody>();
    }
    private void OnDestroy()
    {
        //print(" ************************************************************** die  移除 侦听 ");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GET_ENEMY, GetEnemyObj);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.JINGSHI, this.JingshiEvent);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.BOSS_IS_OUT, BossFight);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHOSE_EVENT, this.ChoseEvent);
    }

    protected void OnDisable()
    {
        //print(" OnDisable   ************************************************************** die  移除 侦听 ");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GET_ENEMY, GetEnemyObj);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.JINGSHI, this.JingshiEvent);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.BOSS_IS_OUT, BossFight);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHOSE_EVENT, this.ChoseEvent);
    }


    [Header("****是否能警示 通知  怪物出现")]
    public List<string> JingshiOutGuaiNameList = new List<string>() { };
    protected void ListGuaiOut()
    {
        if (JingshiOutGuaiNameList.Count == 0) return;
        //关门吗？
        foreach(string _guaiName in JingshiOutGuaiNameList)
        {
            GameObject guai = GlobalTools.GetGameObjectByName(_guaiName);
            //guai.GetComponent<AIBase>().isAction = true;

            float __x = GlobalTools.GetRandomNum() > 50 ? this.transform.position.x + GlobalTools.GetRandomDistanceNums(4) : this.transform.position.x - GlobalTools.GetRandomDistanceNums(4);

            guai.transform.position = new Vector2(__x, this.transform.position.y+10+GlobalTools.GetRandomDistanceNums(5));

            if (guai.GetComponent<AIAirBase>())
            {
                guai.GetComponent<AIAirBase>().isFindEnemy = true;
            }

        }
    }


    [Header("****是否能 警示 通知")]
    public bool IsCanJingshi = false;
    bool IsJingshiing = false;
    protected void GetJingshi()
    {
        if (!IsCanJingshi) return;
        if (IsJingshiEvent) return;
        if (!IsJingshiing && GetComponent<RoleDate>().isBeHiting)
        {
            IsJingshiing = true;
            ListGuaiOut();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.JINGSHI), this);
        }
    }

    [Header("**空中掉落的怪 重力设定 是0的话就没变化")]
    public float XialuoZhongliang = 0;
    [Header("**空中掉落的怪 X距离检测 否则 超出范围也检测")]
    public bool IsXJulijiance = false;
    [Header("**X警示距离")]
    public float XJinshijuli = 8;

    [Header("****是否能警示 接受通知")]
    public bool IsCanJieShouJingshi = false;
    bool IsJingshiEvent = false;
    void JingshiEvent(UEvent e)
    {
        if (!IsCanJieShouJingshi) return;
        if (!IsJingshiEvent)
        {
            if (IsXJulijiance)
            {
                GameObject playerObj = GlobalTools.FindObjByName("player");
                float juli = Mathf.Abs(this.transform.position.x - playerObj.transform.position.x);
                print("jinshi 1   XialuoZhongliang " + XialuoZhongliang+"  juli   "+ juli);
                if (XialuoZhongliang != 0 && juli < XJinshijuli)
                {
                    print("jinshi 2");
                    GetComponent<GameBody>().GetPlayerRigidbody2D().gravityScale = XialuoZhongliang;
                    IsJingshiEvent = true;
                    SetFindEnemyDistance(40);
                }
            }
            else
            {
                IsJingshiEvent = true;
                SetFindEnemyDistance(40);
            }
        }

    }









    public void GetEnemyObj(UEvent e)
    {
        thePlayer = GlobalTools.FindObjByName("player");
    }

    [Header("是否发现敌人")]
    public bool isFindEnemy = false;
    [Header("发现敌人的距离")]
    public float findEnemyDistance = 10;

    [Header("发现敌人 是否采取攻击")]
    public bool isNearAtkEnemy = true;


    bool IsInDubai = false;
    float dubaiTimes = 1;
    float dubaiJiShi = 0;
    bool IsInDuBai()
    {
        if (IsInDubai)
        {
            dubaiJiShi += Time.deltaTime;
            if (dubaiJiShi >= dubaiTimes) IsInDubai = false;
        }
        return IsInDubai;
    }



    protected bool IsFindEnemy()
    {
        if (!isNearAtkEnemy) return false;
        if (isFindEnemy) return true;
        //if (!thePlayer)
        //{
        //    thePlayer = GlobalTools.FindObjByName("Player");
        //}

        //print("**********************************************************************************************************************");
        //print(Mathf.Abs(thePlayer.transform.position.x - transform.position.x)+ "   findEnemyDistance  "+ findEnemyDistance);
        //print(Mathf.Abs(thePlayer.transform.position.y - transform.position.y) + "   findEnemyDistance  " + findEnemyDistance);

        if (Mathf.Abs(thePlayer.transform.position.x - transform.position.x)< findEnemyDistance&& Mathf.Abs(thePlayer.transform.position.y - transform.position.y) < findEnemyDistance)
        {
            print("发现敌人！！！");
            string _msg = GetComponent<RoleDate>().DuBai;
            if (GetComponent<RoleDate>().enemyType == "boss"&& _msg!="")
            {
                
                GameObject _cBar = ObjectPools.GetInstance().SwpanObject2(Resources.Load("TalkBar2") as GameObject);
                Vector2 _talkPos = GetComponent<GameBody>().GetTalkPos();
               
                print("我是boss！！！！  boss 的 独白  是什么？？       "+_msg);
                _cBar.GetComponent<UI_talkBar>().ShowTalkText(_msg, _talkPos, dubaiTimes);
                IsInDubai = true;
            }


            isFindEnemy = true;
            //isPatrol = false;
            GetComponent<GameBody>().InFightAtk();
            FightingDistanceAdd();
            return true;
        }
        return false;
    }

    //战斗后 感知距离增加
    void FightingDistanceAdd()
    {
        if(findEnemyDistance<30) findEnemyDistance = 30;
        if(outDistance<30) outDistance = 30;
    }

    public float outDistance = 15;
    protected void IsEnemyOutAtkDistance()
    {
        if (thePlayer&&!isActioning&&!aiQishou.isQishouAtk&&(Mathf.Abs(thePlayer.transform.position.x - transform.position.x) > outDistance|| Mathf.Abs(thePlayer.transform.position.y - transform.position.y) > findEnemyDistance))
        {
            isFindEnemy = false;
            //print("起手3   " + aiQishou.isQishouAtk);
            if (aiQishou) aiQishou.isQishouAtk = false;
            if (GetComponent<AIYuanLiHuiXue>()!=null)
            {
                print("-------------------------------远离 回血！！！！！");
                YLHuiXue();
               
            }
            else
            {
                gameBody.GetStand();
            }

            
        }
    }


    public bool IsEnemyObjOutAtkDistance()
    {
        if (Mathf.Abs(thePlayer.transform.position.x - transform.position.x) > outDistance || Mathf.Abs(thePlayer.transform.position.y - transform.position.y) > findEnemyDistance) return true;
        return false;
    }

    //是巡逻 还是站地警戒  


    protected bool isRunLeft = true;
    protected bool isRunRight = false;

    [Header("巡逻 距离")]
    public float patrolDistance = 6;
    protected Vector3 myPosition;
   

    [Header("巡逻")]
    public bool isPatrol = false;
    [Header("巡逻几率")]
    public bool IsPatrolRandom = true;
    [Header("必须巡逻")]
    public bool IsMastPatrol = false;

    [Header("巡逻的移动速度")]
    public float PatrolSpeed = 0.4f;



    protected virtual void Patrol()
    {
        if (_roleDate.isDie) return;

        if (isPatrolRest) {
            PatrolResting();
            return;
        }

        if (IsPatrolRandom)
        {
            IsPatrolRandom = false;
            PatrolSpeed = PatrolSpeed + GlobalTools.GetRandomDistanceNums(0.6f);
            patrolDistance =4 + GlobalTools.GetRandomDistanceNums(4);
            if (GlobalTools.GetRandomNum() > 50)
            {
                isRunLeft = false;
                isRunRight = true;
            }


            //print("随机的 巡逻推力    "+ PatrolSpeed);
            if (!IsMastPatrol && GlobalTools.GetRandomNum() >= 60)
            {
                isPatrol = false;
                return;
            }
        }
        //print(this.name+"  我在巡逻？？？？？？？ ");

        if (isRunLeft)
        {
            //print("------>isRunLeft");
            gameBody.RunLeft(-PatrolSpeed);
            if (this.transform.position.x - myPosition.x<-patrolDistance|| gameBody.IsEndGround||gameBody.IsHitWall)
            {
                if (isRunLeft) {
                    isPatrolRest = true;
                    PatrolRest(-1);
                }
                isRunLeft = false;
                isRunRight = true;
            }
        }else if (isRunRight)
        {
            //print("------>isRunRight");
            gameBody.RunRight(PatrolSpeed);
            if (this.transform.position.x - myPosition.x > patrolDistance || gameBody.IsEndGround || gameBody.IsHitWall)
            {
                if (isRunRight) {
                    isPatrolRest = true;
                    PatrolRest(-1);
                }
                
                isRunLeft = true;
                isRunRight = false;
            }
        }
    }

    protected bool isPatrolRest = false;
    protected virtual void PatrolRest(float restTimes = 1)
    {
        if (restTimes == -1)
        {
            restTimes = UnityEngine.Random.Range(1, 2);
        }

        GetComponent<AIRest>().GetRestByTimes(restTimes);
        //gameBody.ResetAll();
        gameBody.GetStand();

    }

    protected void PatrolResting() {
        if (GetComponent<AIRest>().IsOver())
        {
            isPatrolRest = false;
        }
    }



    public bool isEndXXXXX;
    protected bool IsHitWallOrNoWay
    {
        get
        {
            isEndXXXXX = gameBody.IsEndGround;
            return gameBody.IsEndGround;
        }
        
    }





    
    // Update is called once per frame
    void Update () {
        if (isPlayerDie)
        {
            //如果玩家 die
            if (GetComponent<AirGameBody>() && GetComponent<AirGameBody>().IsPlayerDieLaugh) GetComponent<AirGameBody>().Laughing();
            return;
        }
        //print(this.name+"  1  !!!!!isAction     "+ isAction);
        //if (!isAction) return;
        //print("**************************************************  "+this.name);
        GetUpdate();
    }

    float _ycNums = 0;
    float _theYcNums = 1;


    bool IsJumpFX = false;
    public float VXPower = 200;

    protected virtual void GetUpdate()
    {

        


        if (GetComponent<RoleDate>().isDie||Globals.IsHitDoorStop)
        {
            return;
        }
        
        if (_ycNums< _theYcNums)
        {
            _ycNums += Time.deltaTime;
            return;
        }

        


        if (!thePlayer)
        {
            thePlayer = GlobalTools.FindObjByName("player");
        }

        //print("   哦！！ "+gameBody);
        if (!thePlayer || thePlayer.GetComponent<RoleDate>().isDie)
        {
            //print(" -------- ///////////////////////////////   什么哦   " + GetComponent<GameBody>().GetDB().animation.lastAnimationName);
            if (gameBody.GetDB().animation.lastAnimationName.Split('_')[0] == "run")
            {
                isActioning = false;
                gameBody.SetV0();
                gameBody.GetStand();
            }

            return;
        }


       

        if (!isAction&&isPlayerDie)
        {
            //print(" vx "+gameBody.GetPlayerRigidbody2D().velocity.x);
            //gameBody.TSACControl = true;
            isActioning = false;
            IsJumpFX = false;
            gameBody.ResetAll();
            gameBody.SpeedXStop();
            gameBody.GetStand();
            return;
        }


       

        

        if (isYLHuiXue)
        {
            //远离回血的时候 不被打断
            YLHuiXue();
            return;
        }

        //被攻击没有重置 isAction所以不能继续攻击了
        if (GetComponent<RoleDate>().isBeHiting)
        {
            AIBeHit();
            gameBody.ResetAll();
            GetJingshi();
            return;
        }

        if (IsJump)
        {
            //print("   isJumping???    " + gameBody.isJumping + "     ");


            if (gameBody.grounded && GetComponent<GameBody>().GetPlayerRigidbody2D().velocity.y <= 0)
            {
                IsJump = false;
                IsJumpFX = false;
                //print("跳跃完成！！！！");
                gameBody.ResetAll();
                gameBody.SpeedXStop();
                gameBody.GetStand();
            }



            if (gameBody.isJumping)
            {
                gameBody.InAir();
                if (gameBody.isJumping && gameBody.IsGround && gameBody.GetDB().animation.lastAnimationName == gameBody.GetJumpUpACName()&&gameBody.grounded&&GetComponent<GameBody>().GetPlayerRigidbody2D().velocity.y<=0)
                {
                    //print("  ??>>>> " + gameBody.IsGround + "   gameBody ACName " + gameBody.GetDB().animation.lastAnimationName + "   >>>  ");
                    gameBody.isJumping = false;
                    IsJumpFX = false;
                    //IsJump = false;
                    gameBody.ResetAll();
                    //gameBody.SpeedXStop();
                    //gameBody.GetStand();
                    gameBody.GetDB().animation.GotoAndPlayByFrame(gameBody.GetJumpDownACName(), 0, 1);
                }
                else
                {
                    //print("   空中推力________________  ");
                    //float powerX = Mathf.Abs(this.transform.position.x - _playerX) / DistanceXDW * 80;
                    //float vx = this.transform.position.x > thePlayer.transform.position.x ? -VXPower : VXPower;
                    if (!IsJumpFX)
                    {
                        IsJumpFX = true;
                        VXPower = Mathf.Abs(VXPower);
                        VXPower = this.transform.position.x > thePlayer.transform.position.x ? -VXPower : VXPower;
                    }



                    //if (IsJumpToPlayerPostion) vx = this.transform.position.x > _playerX ? -powerX : powerX;
                    if (!gameBody.IsHitWall && !GetComponent<RoleDate>().isBeHiting&&
                        (gameBody.GetDB().animation.lastAnimationName == gameBody.GetJumpUpACName()|| gameBody.GetDB().animation.lastAnimationName == gameBody.GetJumpDownACName())
                        ) gameBody.GetZongTuili(new Vector2(VXPower, 0)); 
                }
                return;

            }

          

        }






        TuihuiFangshouqu();
        if (IsTuihuiFangshouquing) return;


        if (gameBody.tag != "AirEnemy" && !gameBody.IsGround)
        {
            return;
        }



        if (isPatrol && !IsFindEnemy()&&!gameBody._IsBeHitSlowing)
        {
            AIReSet();
            Patrol();
            return;
        }


        //if (!isActioning && IsHitWallOrNoWay)
        //{
        //    isFindEnemy = false;
        //    AIReSet();
        //    gameBody.GetStand();
        //    //isAction = true;
        //    //isActioning = true;
        //    //acName = "backUp";
        //    //gameBody.GetBackUp(14);
        //    //print("in--------->");
        //    return;
        //}


        //超出追击范围
        IsEnemyOutAtkDistance();

        if (!IsFindEnemy()) return;

        
        if (aiFanji != null && aiFanji.IsFanjiing()) return;

        GetAtkFS();
    }

    //根据招式名称 获取招式数据
    protected VOAtk atkvo;
    protected VOAtk GetAtkVOByName(string _name, System.Object obj)
    {
        print("_name   "+_name);
        Dictionary<string, string> dict = GetDateByName.GetInstance().GetDicSSByName(_name, obj);
        atkvo = GetComponent<VOAtk>();//new VOAtk();
        if (dict == null) return null;
        atkvo.GetVO(dict);
        return atkvo;
    }


    protected int atkNum = 0;
    //随机获取列
    protected int lie = -1;
    protected int GetLie()
    {
        int i = (int)UnityEngine.Random.Range(0, GetComponent<AITheWay_dcr>().GetZSArrLength());
        //调试用
        //i = 1;
       // print(aiQishou+"  起手   "+ aiQishou.isQishouAtk);
        if (aiQishou && aiQishou.isQishouAtk)
        {
            aiQishou.isQishouAtk = false;
            carr = aiQishou.qishouAtkArr;
         //   print("-------------------------------------------------------进入起手攻击");
            return -1;
            
        }
        else
        {
            carr = GetComponent<AITheWay_dcr>().GetZSArrays(i);
            if (carr.Length == 0) i = GetLie();//防止空数组
        }
        return i;
    }


    public void SetFindEnemyDistance(float distances)
    {
        findEnemyDistance = distances;
        outDistance = distances + 10;
    }

    protected string[] carr;
    //获取招式
    public string GetZS(bool isAddN = false)
    {
        if (lie == -1) lie = GetLie();
        string zs = "";
        //print("atkNum   "+ atkNum+"  length  "+ carr.Length);
        if(atkNum >= carr.Length) GetAtkNumReSet();
        if (atkNum < carr.Length)
        {
            zs = carr[atkNum];
            if (isAddN)
            {
                string[] _zs = zs.Split('_');
                if (_zs[0] == "lz") zs = _zs[1] + "_" + _zs[2];
                atkNum++;
                GetAtkNumReSet();
            }
           
        }
        moretimes = 0;
        IsChongqi = false;
        return zs;
    }

    //重置攻击招式的次数位置  非进攻招式放最后面会不重置导致错误
    protected void GetAtkNumReSet()
    {
        if (atkNum >= carr.Length)
        {
            //print("起手1   "+ aiQishou.isQishouAtk);
            if (aiQishou && aiQishou.isQishouAtk) aiQishou.isQishouAtk = false;
            atkNum = 0;
            lie = -1;
        }
    }

    //2.招式组第一个攻击动作的攻击距离  位移技能也可以直接取距离
    public float atkDistance = 0;
    public float atkDistanceY = 0;

    bool IsPlayerDieStand = false;
    //3 靠近 达到攻击距离
    public virtual bool NearRoleInDistance(float distance,float nearSpeed  =1.9f)
    {

        if (GlobalTools.FindObjByName("player").GetComponent<RoleDate>().isDie)
        {
            //print("  ////  --------------------- -------------getStand!!!!!!");
            
            if (!IsPlayerDieStand)
            {
                IsPlayerDieStand = true;
                GetComponent<GameBody>().GetStand();
            }

            return false;
        }


        //print("AI 靠近敌人！！！！！");

        if (DontNear) return true;
        //print("??????  靠近敌人？？  敌人是否在攻击  "+gameBody.isAtking  +"   是否能跳跃  "+!IsJump);


        if (IsJump) return false;

        if (
            !gameBody.isJumping&&
            (gameBody.IsEndGround||gameBody.IsHitWall)&&
            (this.transform.localScale.x>0&&(thePlayer.transform.position.x<this.transform.position.x)
            ||
            this.transform.localScale.x < 0 && (thePlayer.transform.position.x > this.transform.position.x))
            )
        {
            //print("撞墙 或者 没路了   撞墙  "+ gameBody.IsHitWall+"    没路  "+ gameBody.IsEndGround);



            if (gameBody.IsHitWall)
            {
                //print("***** 前面撞高墙没有路了");
                if (gameBody.IsCanJump2())
                {
                    //print("****跳高");
                    GetJump(1200);
                    return false;
                }
            }



            if (gameBody.IsCanMoveDown()) {
                //print("下面有路 可以直接 下去！！！！！！");
                //如果下面是 机关 就直接退回去

                return GetMove(distance, nearSpeed);
            }
           

            //先找 能不能 跳过去
            if (gameBody.IsEndGround)
            {
                //print("***** 前面没有路了");
                //探测 前面是否有地板  有的 话跳跃
                if (gameBody.IsCanJump1()&&Mathf.Abs(this.transform.position.y - thePlayer.transform.position.y)>=0.6f)
                {
                    //跳跃1 跳远
                    //print("跳远");
                    GetJump(1000);
                    return false;
                }
                else
                {
                    //print("没有路 还不能跳！！");
                }
            }

         

          

            //没路了 怎么办
            if (IsCanTuihuiFSQ&&!IsTuihuiFangshouquing)
            {
                IsTuihuiFangshouquing = true;
                //print("没路了！！！！！！！");
                return false;
            }
        }
        //print(" 靠近 目标！！！！ ");
        return GetMove(distance, nearSpeed);


    }


    bool GetMove(float distance,float nearSpeed)
    {
        if (thePlayer.transform.position.x - transform.position.x > distance)
        {
            //目标在右
            //print("  向右跑！！??   "+ nearSpeed+"    ACName    "+gameBody.GetDB().animation.lastAnimationName );
            gameBody.RunRight(nearSpeed);
            //print("    ACName    " + gameBody.GetDB().animation.lastAnimationName);
            return false;
        }
        else if (thePlayer.transform.position.x - transform.position.x < -distance)
        {
            //目标在左
            //print("  向 左跑！！ ***  "+nearSpeed + "    ACName    " + gameBody.GetDB().animation.lastAnimationName);
            gameBody.RunLeft(-nearSpeed);
            //print("    ACName    " + gameBody.GetDB().animation.lastAnimationName);
            return false;
        }
        else
        {
            return true;
        }
    }


   
    protected bool IsJump = false;
    public void GetJump(float jumpPower = 1200)
    {
        if (!IsJump)
        {
            IsJump = true;
            GetComponent<GameBody>().yForce = jumpPower;
            GetComponent<GameBody>().GetJump();
            GetComponent<GameBody>().Jump();
        }
    }

    
    public bool IsInAtkDistance(float distance)
    {
        if (Mathf.Abs(thePlayer.transform.position.x - transform.position.x) <= distance) return true;
        return false;
    }

    //转向
    public virtual void ZhuanXiang()
    {
        if (!thePlayer) return;
        if (thePlayer.transform.position.x - transform.position.x > 0)
        {
            //目标在右
            gameBody.RunRight(0.3f);
        }
        else
        {
            gameBody.RunLeft(-0.3f);
        }
    }

    public bool isActioning = false;
    protected bool isAction = false;

    //4.攻击
    protected void GetAtk()
    {
        string zs = GetZS(true);
        if(zs == "")
        {
            isAction = false;
            return;
        }
        //print("  zs "+zs);
        gameBody.GetAtk(zs);
    }
    //5判断攻击是否完成
    protected bool IsAtkOver()
    {
        return gameBody.IsAtkOver();
    }

    //protected bool IsGetAtkFSByName = false;
    public virtual void GetAtkFSByName(string atkFSName)
    {
       

        //IsGetAtkFSByName = true;
        isAction = true;
        acName = atkFSName;
        string[] strArr = atkFSName.Split('_');
        if (strArr.Length >= 2)
        {
            acName = strArr[0];
            if (acName == "AIZiDans")
            {
                GetComponent<AI_ZiDans>().SetZiDanType(int.Parse(strArr[1]));
            }
        }

        //isActioning = true;
    }

    public bool IsCanAtk = true;
    //动作名称
    protected string acName = "";
    protected string jn_effect = "";
    //加强AI  会判断每次攻击是否在攻击范围内
    //6.开始下一个攻击
    protected virtual void GetAtkFS()
    {
        //print("atkFS!!!");
        if (!IsCanAtk) return;
        if (!isNearAtkEnemy) return;
        if (gameBody.IsGuDingTuili) return;
        if (!isAction &&isPlayerDie) return;
        if (IsBossStop) return;
        if (GetComponent<GameBody>() && GetComponent<GameBody>().GetDB().animation.lastAnimationName == "downOnGround_1") return;
        if (gameBody.isJumping) return;
        if (IsIfStopMoreTime())return;


        if (IsInDuBai()) return;

        if (!isAction){
			isAction = true;
			acName = GetZS();
            //IsGetAtkFSByName = false;
           
            
            print(atkNum + "????------------------------------------------------------------->    name " + acName);
            string[] strArr = acName.Split('_');
            if (acName == "walkBack") return;

            if (strArr[0] == "lz")
            {
                //不需要转向
                DontNear = true;
                return;
            }
            else
            {
                DontNear = false;
            }

            if(acName == "jumpCut")
            {
                return;
            }


            if (acName == "yishan")
            {
                return;
            }

            if (strArr[0] == "jn") {
                jn_effect = acName;
                if(GetAtkVOByName(acName, DataZS.GetInstance()))
                {
                    atkDistance = GetAtkVOByName(acName, DataZS.GetInstance()).atkDistance;
                }
               
                acName = "jn";
                return;
            }


            if (strArr[0] == "qianhua")
            {
                acName = "qianhua";
                gameBody.Qianhua(float.Parse(strArr[1]));
                return;
            }
            else if (strArr[0] == "backUp")
            {
                acName = "backUp";

                gameBody.GetBackUp(float.Parse(strArr[1]));
                return;
            }

            
            
            if (strArr[0]=="rest")
            {
                acName = "rest";
                GetComponent<AIRest>().GetRestByTimes(float.Parse(strArr[1]));
                return;
            }
               
            if (acName == "shanxian")
            {
                aisx = GetComponent<AIShanxian>();
                atkDistance = aisx.sxDistance;
                return;
            }

            if (acName == "zidan")
            {
                acName = "zidan";
                return;
            }

            if (acName == "runCut")
            {
                acName = "runCut";
                return;
            }

            //print("acName:   "+acName+"     "+GetZS());
            if (acName == "chongci1")
            {
                acName = "chongci1";
                return;
            }

            if (acName == "yueguangzhan"|| acName == "zhongzhan"|| acName == "lianzhuaci")
            {
                return;
            }

            //print("   ??? acNmae  "+ acName);

            if (acName == "luanren")
            {
                //print(" luanren!!! ");
                return;
            }

            //if(acName == "zhongzhan") 
            //{
            //    return;
            //}
            //print("     acName  "+ acName);
            if (acName.Split('|').Length > 1)
            {
                atkDistance = GetAtkVOByName(acName.Split('|')[0], DataZS.GetInstance()).atkDistance;
            }
            else
            {
                atkDistance = GetAtkVOByName(acName, DataZS.GetInstance()).atkDistance;
            }
            
		}

       

        if (aiQishou&&aiQishou.isQishouAtk&&!aiQishou.isFirstAtked)
        {
            //print("---------------------------------------------------》起手攻击  "+ atkDistance);
            if (atkDistance == 0f)
            {
                aiQishou.isFirstAtked = true;
            }
            else
            {
                if(Mathf.Abs(thePlayer.transform.position.x - transform.position.x) <= atkDistance) aiQishou.isFirstAtked = true;
            }

            return;
        }

        if(acName == "jn")
        {
            JNAtk();
            return;
        }

        if (acName == "walkBack")
        {
            GetWalkBack();
            return;
        }

        if (acName == "qianhua")
        {
            GetQianhua();
            return;
        }

        if (acName == "backUp")
        {
            GetBackUp();
            return;
        }

        if (acName == "rest")
        {
            GetRest();
            return;
        }

		if(acName == "shanxian"){
			GetShanXian();
			return;
		}

        if (acName == "runCut")
        {
            GetRunCut();
            return;
        }

        if (acName == "chongci1")
        {
            GetChongCi1();
            return;
        }

        if (acName == "yishan")
        {
            GetYiShan();
            return;
        }

        if (acName == "zidan")
        {
            GetZiDanFire();
            return;
        }

        if (acName == "yueguangzhan")
        {
            GetYueGuangZhan();
            return;
        }

        if (acName == "lianzhuaci")
        {
            GetLianZhuaCi();
            return;
        }

        if (acName == "zhongzhan")
        {
            GetZhongZhan();
            return;
        }

        if (acName == "luanren")
        {
            GetLuanRen();
            return;
        }

        if(acName == "jumpCut")
        {
            GetJumpCut();
            return;
        }


        PtAtk();
        if (acName !="shanxian"){
				
		}

        
    }


    //public virtual void AIGetBeHit()
    //{

    //}

    public float walkDistance = 3;
    protected void GetWalkBack()
    {
        if (!isActioning)
        {
            isActioning = true;
            myPosition = this.transform.position;
            //查找自己的方向
            if (gameBody.GetBodyScale().x==1)
            {

            }
            else
            {

            }
            atkNum++;
            GetAtkNumReSet();
        }

        if (isActioning) {
            if (!gameBody.GetDB().animation.HasAnimation("walk"))
            {
                isActioning = false;
                isAction = false;
                return;
            }
            gameBody.RunLeft(0.9f, true);
            if(Mathf.Abs(this.transform.position.x - myPosition.x) > 3)
            {
                isActioning = false;
                isAction = false;
            }
        }
    }

    protected void GetBackUp()
    {
        if (!isActioning)
        {
            isActioning = true;
            atkNum++;
            GetAtkNumReSet();
        }
        if (isActioning)
        {
            if (gameBody.GetBackUpOver())
            {
                acName = "";
                isActioning = false;
                isAction = false;
            }
        }
    }


    protected void GetQianhua()
    {
        if (!isActioning)
        {
            isActioning = true;
            atkNum++;
            GetAtkNumReSet();
        }
        if (isActioning)
        {
            if (gameBody.GetQianhuaOver())
            {
                acName = "";
                isActioning = false;
                isAction = false;
            }
        }
    }

    protected void GetRest()
    {
        if (!isActioning)
        {
            isActioning = true;
            //print(gameBody.GetDB().animation.lastAnimationName);
            if (!gameBody.IsZJToStandType && gameBody.GetDB().animation.lastAnimationName != gameBody.GetStandACName())
            {
                gameBody.GetDB().animation.FadeIn(gameBody.GetStandACName(), 0.15f);
            }
            if (GetComponent<AIRest>().isZhuanXiang) ZhuanXiang();
            gameBody.GetStand();
            atkNum++;
            GetAtkNumReSet();
        }

        if (isActioning)
        {
            if (GetComponent<AIRest>().IsOver())
            {
                isActioning = false;
                isAction = false;
            }
        }
    }

    protected AIShanxian aisx;
    protected void GetShanXian(){
		if(!isActioning){
            isActioning = true;
            aisx = GetComponent<AIShanxian>();
            aisx.GetShanXian();
            atkNum++;
            //GetAtkNumReSet();
            return;
		}

		if(isActioning&&aisx.IsShanxianOver()){
            //GetComponent<GameBody>().SetACingfalse();
            //aisx.isStart = false;
            //aisx.ReSet();
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }

	}

    [Header("判断是否有  被攻击时候 需要 移除的 物体")]
    public bool IsHasBeHitDisObj = false;
    [Header("被攻击时候 需要 移除的 物体")]
    public GameObject BeHitDisObj;

    protected virtual void AIBeHit()
    {
        //if (aisx != null) aisx.ReSet();
        if (IsCanAtk)
        {
            isFindEnemy = true;
            isPatrolRest = false;
            isNearAtkEnemy = true;
        }
        IsTuihuiFangshouquing = false;
        IsBossStop = false;

        if (IsHasBeHitDisObj)
        {
            if(BeHitDisObj) Destroy(BeHitDisObj);
        }

        //IsGetAtkFSByName = false;
        AIReSet();
        if(aiFanji!=null) aiFanji.GetFanji();
    }

    float moretimes = 0;
    [Header("是否可以重启 防止 卡死")]
    public bool IsChongqi = false;

    public float ChongQiJishi = 5;

    //当怪物 卡死 长时间后 自动 重启ai
    protected bool IsIfStopMoreTime()
    {
        moretimes += Time.deltaTime;
        if (moretimes >= ChongQiJishi)
        {
            moretimes = 0;
            AIReSetAll();
            return true;
        }
        return false;
    }

    protected void AIReSetAll()
    {
        if (IsBossStop||!isFindEnemy) return;
        print("   AI重启   ！！！！！！！！！！！！！！！！！！！！！！");
        IsChongqi = true;
        AIBeHit();
        GetComponent<GameBody>().ResetAll();
    }


    protected virtual void AIReSet()
    {
        isAction = false;
        isActioning = false;
        IsJump = false;
        lie = -1;
        atkNum = 0;
        acName = "";
        //IsGetAtkFSByName = false;
        //print("起手2   " + aiQishou.isQishouAtk);
        if (aiQishou) aiQishou.isQishouAtk = false;

        //isZSOver = false;
    }

    //不能动的怪 给与招式重新启动 远离回血 或者 切换招式
    protected float DontMoveAtkWiteTimes = 0;
    protected float TheDontMoveAtkWiteTime = 3;
    void DontMoveAtkChange()
    {
        DontMoveAtkWiteTimes += Time.deltaTime;
        //print("  ????DontMoveAtkWiteTimes  " + DontMoveAtkWiteTimes);
        if (DontMoveAtkWiteTimes >= TheDontMoveAtkWiteTime)
        {
            AIReSetAll();
            if (GlobalTools.GetRandomNum() > 50)
            {
                if (GetComponent<AIYuanLiHuiXue>() != null)
                {
                    print("-------------------------------远离 回血！！！！！");
                    YLHuiXue();
                }
            }
            DontMoveAtkWiteTimes = 0;
        }
    }



    //用于连招的时候 可以连续动作打完再做其他普通攻击动作 注意前面+"lz"  攻击动作只能写成 "lz_xxx_x"或者"xxx_xx"
    protected bool DontNear = false;

    //一般攻击
    protected virtual void PtAtk(){
        if (!isNearAtkEnemy) return;
        if (gameBody.isAtking) return;

        //判断 是否hi不能动的怪物
        if (!IsCanNotMove)
        {
            //print(" 攻击招式    "+acName);
            //这种如果再次超出攻击距离会再追踪
            if (!isActioning && (NearRoleInDistance(atkDistance) || DontNear))
            {
                isActioning = true;
                
                if (!DontNear) ZhuanXiang();
                GetAtk();
            }
        }
        else
        {
            //非移动怪的 普通攻击
            //如果在攻击范围外 就直接结束  这里可以取消连击 让玩家难以猜招
            //这里 如果不是 连招的话 就会是玩家接近就 可以攻击  增加猜招难度
            //print("  移动靠近目标 DontNear  "+ DontNear);
            if (!DontNear&&!IsInAtkDistance(atkDistance))
            {
                DontMoveAtkChange();
                //print("  靠近目标！！！isActioning     " + isActioning+ "  isAction  "+ isAction);
                if (IsAtkOver())
                {
                    isActioning = false;
                    isAction = false;
                }
                return;
            }

            if (!isActioning)
            {
                isActioning = true;
                GetAtk();
            }

            
        }


        

        if (isActioning)
        {
            //print("???   "+ IsAtkOver());
            if (IsAtkOver())
            {
                isActioning = false;
                isAction = false;
            }
        }
	}

    protected void GetRunCut()
    {
        if (isActioning)
        {
            if (GetComponent<AIRunCut>().IsAcOver())
            {
                isActioning = false;
                isAction = false;
                return;
            }
        }

        if (!isActioning)
        {
            isActioning = true;
            ZhuanXiang();
            //gameBody.GetStand();
            
            GetComponent<AIRunCut>().GetStart(thePlayer);
            atkNum++;
            //GetAtkNumReSet();
        }
    }

    protected void GetChongCi1()
    {
        if (isActioning)
        {
            if (GetComponent<AI_Chongci>().IsAcOver())
            {
                isActioning = false;
                isAction = false;
                return;
            }
        }

        if (!isActioning)
        {
            isActioning = true;
            ZhuanXiang();
            //gameBody.GetStand();
            GetComponent<AI_Chongci>().GetStart(thePlayer);
            atkNum++;
            //GetAtkNumReSet();
        }
    }



    void GetZiDanFire()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIZiDan>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AIZiDan>().IsBehaviorOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }


    void GetYueGuangZhan()
    {

        if (!isActioning)
        {
            //print("111");
            isActioning = true;
            ZhuanXiang();
            GetComponent<JN_YueGuanZhan>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<JN_YueGuanZhan>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }


    //远程
    void GetLianZhuaCi()
    {
        if (!isActioning)
        {
            //print("111");
            isActioning = true;
            ZhuanXiang();
            GetComponent<JN_CizuMoves>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<JN_CizuMoves>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }



    void GetZhongZhan()
    {

        if (!isActioning)
        {
            //print("111   zhongzhan!!!!!!! ");
            isActioning = true;
            ZhuanXiang();
            GetComponent<JN_zhongzhan>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<JN_zhongzhan>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }

    void GetJumpCut()
    {
        if (!isActioning)
        {
            //print("111   zhongzhan!!!!!!! ");
            isActioning = true;
            ZhuanXiang();
            GetComponent<JN_JumpCut>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<JN_JumpCut>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }



    void GetLuanRen()
    {

        if (!isActioning)
        {
            //print("111   zhongzhan!!!!!!! ");
            isActioning = true;
            ZhuanXiang();
            GetComponent<JN_LuanRen2>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<JN_LuanRen2>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }



    protected void GetYiShan()
    {
        if (isActioning)
        {
            if (GetComponent<AIYiShan>() == null||GetComponent<AIYiShan>().IsAcOver())
            {
                //print("一闪 完成-----------------------------------------》");
                isActioning = false;
                isAction = false;
                return;
            }
        }

        if (!isActioning)
        {
            isActioning = true;
            ZhuanXiang();
            //gameBody.GetStand();
            GetComponent<AIYiShan>().GetStart(thePlayer);
            atkNum++;
            //GetAtkNumReSet();
        }

    }

    protected void JNAtk()
    {
        //print("技能攻击   距离是多少  atkDistance    " + atkDistance);
        //这种如果再次超出攻击距离会再追踪
        if (!isActioning && (NearRoleInDistance(atkDistance) || DontNear))
        {

            isActioning = true;
            if (!DontNear) ZhuanXiang();
            GetComponent<GameBody>().GetSkillBeginEffect(jn_effect);
            atkNum++;
            //GetAtk();
        }

        if (isActioning)
        {
            if (!GetComponent<GameBody>().GetIsACing())
            {
                isActioning = false;
                isAction = false;
            }
        }
    }

    bool isYLHuiXue = false;
    //远离 回血
    public void YLHuiXue()
    {
        if (!isActioning)
        {
            isActioning = true;
            //isAction = true;
            isYLHuiXue = true;
            GetComponent<AIYuanLiHuiXue>().GetStart(thePlayer);
        }

        if (isActioning) {
            if (GetComponent<AIYuanLiHuiXue>().IsGetOver())
            {
                isActioning = false;
                isAction = false;
                isYLHuiXue = false;
                print("远离 回血 over！！！！！！！");
            }
        }
    }

}
