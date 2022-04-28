using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIAirBase : AIBase
{

    // Use this for initialization
    void Start()
    {
       
        GetStart();
        if (!_AIYinshen) _AIYinshen = GetComponent<AIYinshen>();
        air_aiNear = GetComponent<AIAirRunNear>();
    }
    AIAirRunNear air_aiNear;

    bool IsHasGetPlayer = false;
    float LongNotMoveJishi = 0;
    float DieTimes = 120;
    void GetPlayerLongNotMoveDie()
    {
        if (_roleDate && _roleDate.enemyType == "boss") return;
        if (isFindEnemy) IsHasGetPlayer = true;
        if (IsHasGetPlayer)
        {
            LongNotMoveJishi += Time.deltaTime;
            if(LongNotMoveJishi>= DieTimes)
            {
                _roleDate.live = 0;
            }
        }
    }

    [Header("巡逻停顿休息 时间长度 -1为默认")]
    public float PatrolRestTimes = 0.1f;
    protected override void Patrol()
    {
        //print("AI 巡逻！！！！！！！！！！！！！！！！！！！！！！！！！！！！"+ isPatrol);

        if (_roleDate.isDie) return;


        if(GetComponent<AIYidongZidan>()&& GetComponent<AIYidongZidan>().IsCanStart)
        {
            GetComponent<AIYidongZidan>().MoveZiDan();
            return;
        }



        if (isPatrolRest)
        {
            //print("rest!!!!!!!");
            PatrolResting();
            return;
        }


        if (IsPatrolRandom)
        {
            IsPatrolRandom = false;
            flyXSpeed = flyXSpeed + GlobalTools.GetRandomDistanceNums(0.6f);
            //print("随机的 巡逻推力    " + flyXSpeed);
            if (!IsMastPatrol && GlobalTools.GetRandomNum() >= 60)
            {
                isPatrol = false;
            }
        }


        isNearing = false;
        //print("巡逻-----------gameBody.IsHitWall    " + gameBody.IsHitWall + "  " + patrolDistance + "     " + this.transform.position.x+ "   myPosition  "+ myPosition.x);
        //if (gameBody.IsHitWall) print("巡逻-----------gameBody.IsHitWall    "+ gameBody.IsHitWall+"  "+ patrolDistance+"     "+this.transform.position.x+"  速度  " +flyXSpeed+"   左？   " +isRunLeft+"  右?  "+isRunRight);
        if (isRunLeft)
        {
            gameBody.RunLeft(flyXSpeed);
            //print("   左 "+flySpeed);
            if (this.transform.position.x - myPosition.x < -patrolDistance || gameBody.IsEndGround || gameBody.IsHitWall)
            {
                if (isRunLeft)
                {
                    isPatrolRest = true;
                    PatrolRest(PatrolRestTimes);
                }
                isRunLeft = false;
                isRunRight = true;
                
            }
        }
        else if (isRunRight)
        {
            gameBody.RunRight(flyXSpeed);
            //print("   右！！！ " + flySpeed);
            if (this.transform.position.x - myPosition.x > patrolDistance || gameBody.IsEndGround || gameBody.IsHitWall)
            {
                if (isRunRight)
                {
                    isPatrolRest = true;
                    PatrolRest(PatrolRestTimes);
                }

                isRunLeft = true;
                isRunRight = false;
                
            }
        }
    }


    protected override void PatrolRest(float restTimes = 1)
    {
        if (restTimes == -1)
        {
            //print("休息停顿 rest 是否进来 ！！！");
            restTimes = UnityEngine.Random.Range(1, 2);
        }

        GetComponent<AIRest>().GetRestByTimes(restTimes);
        gameBody.GetStand(false);

    }


    protected bool IsBeHitRunAwaying = false;
    protected void BeHitRunAway()
    {
      
        if (!IsBeHitRunAwaying &&GetComponent<AirGameBody>()&& GetComponent<AirGameBody>().IsBehitJingkong)
        {
            IsBeHitRunAwaying = true;
            flyXSpeed += 4;
            if (!thePlayer) return;
            if (this.transform.position.x >= thePlayer.transform.position.x)
            {
                isRunLeft = false;
                isRunRight = true;
                gameBody.RunRight(flyXSpeed);
            }
            else
            {
                isRunLeft = true;
                isRunRight = false;
                gameBody.RunLeft(flyXSpeed);
            }
        }
    }

    protected void BeHitRunAwaying()
    {
        if (isRunLeft)
        {
            gameBody.RunLeft(flyXSpeed);
            if (GetComponent<GameBody>().IsHitWall)
            {
                isRunLeft = false;
                isRunRight = true;
            }
        }
        else if (isRunRight)
        {
            gameBody.RunRight(flyXSpeed);
            if (GetComponent<GameBody>().IsHitWall)
            {
                isRunLeft = true;
                isRunRight = false;
            }
        }


    }



    // Update is called once per frame
    void Update()
    {
        GetPlayerLongNotMoveDie();
        GetUpdate2();
    }

    AIYinshen _AIYinshen;

    public override void AIBeHit()
    {
        //AI闪现
        if (aisx != null) aisx.ReSet();
        isFindEnemy = true;
        isPatrolRest = false;
        isNearAtkEnemy = true;
        //print("AI BE HITTTTTTTTT!!!");
        AIReSet();
        if (aiFanji != null) {
            aiFanji.GetFanji();
            return;
        }

        if (_AIYinshen != null) {
            _AIYinshen.BeHitHide();
            return;
        }
        
        BeHitRunAway();
    }

    
    //public override void AIGetBeHit()
    //{
    //    //base.AIGetBeHit();
    //    print(" aiGetBehit!! ");
        
    //}


    public void GetReSet()
    {
        //print("   getAIReset  ");
        AIReSet();
    }

    public override void AIReSet()
    {
        isAction = false;
        isActioning = false;
        GetComponent<AIAirRunNear>().ResetAll();
        //myPosition = this.transform.position;
        //IsGetAtkFSByName = false;
        lie = -1;
        atkNum = 0;
        acName = "";
        if (aiQishou) aiQishou.isQishouAtk = false;

        //isZSOver = false;
    }

    public override void GetAtkFSByName(string atkFSName)
    {


        //IsGetAtkFSByName = true;
        QuXiaoAC();
        isAction = true;
        acName = atkFSName;
        print(" ********************************************************** acName  "+acName);
        
        string[] strArr = atkFSName.Split('_');
        if (strArr.Length >= 2)
        {
            acName = strArr[0];
            if (acName == "AIZiDans")
            {
                GetComponent<AI_ZiDans>().SetZiDanType(int.Parse(strArr[1]));
            }else if (acName == "atk")
            {
                print("  普通攻击！！！ "+ acName);
                atkDistance = GetAtkVOByName(atkFSName.Split('|')[0], DataZS.GetInstance()).atkDistance;
                atkDistanceY = GetAtkVOByName(atkFSName.Split('|')[0], DataZS.GetInstance()).atkDistanceY;
            }
        }
        else
        {
            //isActioning = true;
        }


    }


    public void ReSetAll2()
    {
        //print("   getAIReset  2222222222222  ");
        isAction = false;
        isActioning = false;
        GetComponent<AIAirRunNear>().ResetAll();
        acName = "";
        if (aiQishou) aiQishou.isQishouAtk = false;
    }


    protected virtual void GetUpdate2()
    {
        //print("----1");
        if (!thePlayer)
        {
            thePlayer = GlobalTools.FindObjByName("player");
            if (_roleDate.enemyType == GlobalTag.BOSS) ZhuanXiang();
            return;
        }
        //print("----2");
        if (thePlayer.GetComponent<RoleDate>().isDie||Globals.IsHitDoorStop)
        {
            gameBody.ResetAll();
            //gameBody.Stand();
            return;
        }
        //print("----3");
        //被攻击没有重置 isAction所以不能继续攻击了
        if (GetComponent<RoleDate>().isBeHiting)
        {
            AIBeHit();
            GetJingshi();
            return;
        }
        //print("----4");
        if (gameBody.tag != "AirEnemy" && !gameBody.IsGround)
        {
            return;
        }
        //print("----5");
        if (IsBeHitRunAwaying) {
            //print("?????????????behit ranaway");
            BeHitRunAwaying();
            return;
        }
        //print("----6");
        //print(" vx " + gameBody.GetPlayerRigidbody2D().velocity.x);

        if (isPatrol && !IsFindEnemy())
        {
            AIReSet();
            Patrol();
            return;
        }
        //print("----7");
        //print(">>>??????  进来没！！ ");


        if (!isActioning && IsHitWallOrNoWay)
        {
            isFindEnemy = false;
            AIReSet();
            gameBody.GetStand();
            return;
        }
        //print("----8");

        //超出追击范围
        IsEnemyOutAtkDistance();
        //print("----9");
        if (!IsFindEnemy()) return;
        //print("----10");
        GetAtkFS();
    }


    public float flySpeed = 0.02f;
    public float flyXSpeed = 0;
    public float flyYSpeed = 0;

    protected virtual bool TongY(float distance)
    {
        if (thePlayer.transform.position.y - transform.position.y > distance)
        {
            //目标在上面  这里做角度计算 来控制XY 速度
            this.GetComponent<AirGameBody>().RunY(0.9f);
            return false;
        }
        else if (thePlayer.transform.position.y - transform.position.y < -distance)
        {
            //目标在下面
            this.GetComponent<AirGameBody>().RunY(-0.9f);
            return false;
        }
        else
        {
            return true;
        }
    }

    // 4方探测器

    //行为
    //远离  X远离  Y远离
    //闪现远离 闪现靠近

    //鱼怪 的撞击


    public float nearDastanceX = 1f;
    public float nearDastanceY = 0.3f;

    //X靠近
    public void GetMoveNearX(float _moveDistance)
    {

    }
    //Y靠近
    public bool GetMoveNearY(float _moveDistance,float speedY = 0)
    {
        if (DontNear) return true;
        if (speedY == 0) speedY = flyYSpeed;
        if (thePlayer.transform.position.y - transform.position.y > _moveDistance)
        {
            //目标在上 向上移动
            
            this.GetComponent<AirGameBody>().RunY(speedY);
            return false;
        }
        else if (thePlayer.transform.position.y - transform.position.y < -_moveDistance)
        {
            //目标在下 向下移动
            this.GetComponent<AirGameBody>().RunY(-speedY);
            return false;
        }
        else
        {

            return true;
        }
    }
    //X远离
    //Y远离
    //同Y
    //绕后



    public override bool NearRoleInDistance(float distance,float nearSpeed = 0.9f)
    {

        if (DontNear) return true;
        print("----------???????????");
        if (thePlayer.transform.position.x - transform.position.x > distance)
        {
            //目标在右
            gameBody.RunRight(flyXSpeed);
            return false;
        }
        else if (thePlayer.transform.position.x - transform.position.x < -distance)
        {
            //目标在左
            gameBody.RunLeft(flyXSpeed);
            return false;
        }
        else
        {

            return true;
        }
    }



    public bool IsCanZhuangXiang = true;
    public override void ZhuanXiang()
    {
        if (!IsCanZhuangXiang|| thePlayer == null) return;
        if (thePlayer.transform.position.x - transform.position.x > 0)
        {
            //print("  ****** right ");
            //目标在右
            //gameBody.RunRight(flyXSpeed);
            gameBody.TurnRight();
        }
        else
        {
            //print("  ****** left ");
            //gameBody.RunLeft(flyXSpeed);
            gameBody.TurnLeft();
        }
    }


    bool XianYhouX()
    {
        if (!GetMoveNearY(flyYSpeed)) return false;
        if (!NearRoleInDistance(atkDistance)) return false;
        return true;
    }


    bool XianXhouY()
    {
        if (!NearRoleInDistance(atkDistance)) return false;
        if (!GetMoveNearY(flyYSpeed)) return false;
        return true;
    }

    bool Tongshi()
    {
        if (!NearRoleInDistance(atkDistance) || !GetMoveNearY(flyYSpeed)) return false;
        return true;
    }


    int nearRanNum = 0;
    bool isNearing = false;
    protected override void PtAtk()
    {
        //空怪靠近 
        //x y靠近方式 有几种
        //GetComponent<Xunlu>().GetListZB(this.gameObject, thePlayer.transform.position, Vector2.zero);
        /* if (!isActioning&&!isNearing)
         {
             isNearing = true;
             nearRanNum = GlobalTools.GetRandomNum();

         }
         if (nearRanNum < 30)
         {
             print("先Y 后X！！！");
             if (!XianYhouX()) return;
         }else if(nearRanNum < 60)
         {
             if (!XianXhouY()) return;
         }
         else
         {
             if (!Tongshi()) return;
         }*/
        //print(this.name+"???????????????????????????????????????????????????????????????????????普通攻击  isActioning    "   + isActioning + "  atkDistance   " + atkDistance + "  atkDistanceY  " + atkDistanceY+ " DontNear    "+ DontNear);
     



        if (!isActioning && !(air_aiNear.ZhuijiXY(atkDistance,1,atkDistanceY)||DontNear)) return;

        print(" ?? >>  普通攻击 ！！！ ");
        if (!isActioning)
        {
            isActioning = true;
            isNearing = false;
            print("   >>#3 "+ DontNear);
            if (!DontNear) ZhuanXiang();
            GetAtk();
        }

        //先Y后X
        //先X后Y
        //XY同时
        //绕后
        //最优路线 如果碰到墙就找最优路线
        //print("空中怪 ptatk!!!");
        //这种如果再次超出攻击距离会再追踪
        //if (!isActioning && (NearRoleInDistance(atkDistance) || DontNear))
        //{
        //    isActioning = true;
        //    if (!DontNear) ZhuanXiang();
        //    GetAtk();
        //}

        if (isActioning)
        {
            if (IsAtkOver())
            {
                print("   普通 攻击 结束！！！！！！！ ");
                //isActioning = false;
                //isAction = false;
                ReSetAll2();
            }
            return;
        }


    }

    protected override void GetAtkFS()
    {
        //print(1);
        if (GetComponent<RoleDate>().isDie||!thePlayer|| thePlayer.GetComponent<RoleDate>().isDie) {
            GetComponent<AIDestinationSetter>().ReSetAll();
            GetComponent<AIPath>().canMove = false;
            return;
        }
        //print(2);
        if (IsIfStopMoreTime()) return;
        //print(3);
        //print("IsBossStop   "+ IsBossStop);
        if (IsBossStop) return;
        //print(4);

        if (IsInDuBai()) return;
        //print(5);
        if (IsInZDAcing) return;
        //print(6);

        if (!isAction)
        {
            isAction = true;
            acName = GetZS();
            //gameBody.isAcing = false;
            //IsGetAtkFSByName = false;

            print(" atkNum:  " + atkNum + " ----------------------------------------------------------------------------------------->   name " + acName + "  isACing " + isActioning);
            string[] strArr = acName.Split('_');
            LongNotMoveJishi = 0;

            CurrentAIName = strArr[0];

            if (CurrentAIName == "ZD")
            {
                //进入自动攻击流程
                print("***自动攻击的 AI 技能   " + acName);
                if (!isActioning)
                {
                    isActioning = true;
                    IsInZDAcing = true;
                    atkNum++;
                }

                moretimes = 0;
                string str = this.gameObject.GetInstanceID() + "@" + acName;

                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ZD_SKILL_SHOW, str), this);

                return;
            }



            if (strArr.Length > 1&& strArr[0]!="atk")
            {
                acName = strArr[0];
            }

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


            if(strArr[0] == "AIZiDans")
            {
                acName = strArr[0];
                GetComponent<AI_ZiDans>().SetZiDanType(int.Parse(strArr[1]));
                return;
            }

            if (strArr[0] == "AIZiDanDingxiang")
            {
                acName = strArr[0];
                GetComponent<AI_ZidanDingxiang>().SetZiDanType(int.Parse(strArr[1]));
                return;
            }

            if (strArr[0] == "AIFlyPenhuo")
            {
                acName = strArr[0];
                if (strArr.Length > 1) {
                    GetComponent<AIFlyPenghuo>().SetTimeAndMoveSpeed(strArr[1]);
                }
                else
                {
                    GetComponent<AIFlyPenghuo>().SetTimeAndMoveSpeed("");
                }
                
                return;
            }

            if (acName == "hengXiangChongZhuang")
            {
                return;
            }

            //连弹攻击
            if (acName == "liandan")
            {
                return;
            }

            if (acName == "hengXiangCZKuaiSu")
            {
                return;
            }

            if (acName == "zongYa")
            {
                return;
            }

            if (acName == "gddZongYa")
            {
                //固定点 重压
                return;
            }


            if (acName == "yishan")
            {
                return;
            }

            if (strArr[0] == "jn")
            {
                jn_effect = acName;
                acName = "jn";
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



            if (strArr[0] == "rest")
            {
                acName = "rest";
                GetComponent<AIRest>().GetRestByTimes(float.Parse(strArr[1]));
                return;
            }

            if (acName == "shanxian")
            {
                //aisx = GetComponent<AIShanxian>();
                //atkDistance = aisx.sxDistance;
                acName = "shanxian";
                return;
            }

            if (strArr[0] == "flyToPot1")
            {
                //取到哪个点
                acName = "flyToPot1";
                if (strArr.Length > 1) {
                    GetComponent<AIFlyToPot1>().SetPotType(int.Parse(strArr[1]));
                }
                else
                {
                    GetComponent<AIFlyToPot1>().SetPotType();
                }
                
                return;
            }

            if (strArr[0] == "flyToPot2GD")
            {
                //取到哪个 点 固定位置
                if (strArr.Length > 1) {
                    GetComponent<AIFlyToPot2GD>().SetPotType(int.Parse(strArr[1]));
                }
                else
                {
                    GetComponent<AIFlyToPot2GD>().SetPotType();
                }
                acName = "flyToPot2GD";
                return;
            }

            if (acName == "runCut")
            {
                //冲砍
                acName = "runCut";
                return;
            }

            if (acName == "chongji")
            {
                acName = "chongji";
                return;
            }

            if (acName == "chongjiHX")
            {
                acName = "chongjiHX";
                return;
            }

            if (acName == "chongjiHX2")
            {
                acName = "chongjiHX2";
                return;
            }



            if (acName == "zidan")
            {
                acName = "zidan";
                return;
            }

            if(acName == "yuanli")
            {
                acName = "yuanli";
                return;
            }

            if (strArr[0] == "yinshen")
            {
                acName = strArr[0];
                if (strArr.Length > 1)
                {
                    GetComponent<AIYinshen>().SetYinShenPosBiaoJi(strArr[1]);
                }
                else
                {
                    GetComponent<AIYinshen>().SetYinShenPosBiaoJi();
                }
                
                return;
            }

            if (acName == "yueguangzhan" || acName == "zhongzhan"|| acName == "gotoAtk"|| acName == "dazhan"|| acName == "3dianqiu" || acName == "dianqiang"|| acName == "diandings")
            {
                return;
            }


            //if (acName.Split('|').Length > 1)
            //{

            //}
            //else
            //{
            //    atkDistance = GetAtkVOByName(acName, DataZS.GetInstance()).atkDistance;
            //    atkDistanceY = GetAtkVOByName(acName, DataZS.GetInstance()).atkDistanceY;
            //}

            print("  acName "+ acName);
            atkDistance = GetAtkVOByName(acName.Split('|')[0], DataZS.GetInstance()).atkDistance;
            atkDistanceY = GetAtkVOByName(acName.Split('|')[0], DataZS.GetInstance()).atkDistanceY;

            print("  acName " + acName + "  atkDistanceY:  " + atkDistanceY);
        }

        //print("acName    "+ acName+"  shange  dongzuo ACName   "+GetComponent<AirGameBody>().GetDB().animation.lastAnimationName);

        if (aiQishou && aiQishou.isQishouAtk && !aiQishou.isFirstAtked)
        {
            if (atkDistance == 0f)
            {
                aiQishou.isFirstAtked = true;
            }
            else
            {
                if (Mathf.Abs(thePlayer.transform.position.x - transform.position.x) <= atkDistance) aiQishou.isFirstAtked = true;
            }

            return;
        }

        if (acName == "jn")
        {
            JNAtk();
            return;
        }


        if (acName == "hengXiangChongZhuang")
        {
            GetHengXiangChongZhuang();
            return;
        }

        if (acName == "hengXiangCZKuaiSu")
        {
            GetHengXiangCZKuaiSu();
            return;
        }


        if (acName == "zongYa")
        {
            GetZongYa();
            return;
        }

        if (acName == "gddZongYa")
        {
            //固定点 重压
            GetGDDZongYa();
            return;
        }

        if (acName == "liandan")
        {
            GetLianDan();
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

        if (acName == "shanxian")
        {
            GetShanXian();
            return;
        }

        if (acName == "runCut")
        {
            GetRunCut();
            return;
        }

        if (acName == "yishan")
        {
            GetYiShan();
            return;
        }
        if (acName == "shanxian")
        {
            GetShanXian();
            return;
        }

        if (acName == "chongji")
        {
            //print("------------------------------------------> chognji!!! ");
            GetChongji();
            return;
        }

        if (acName == "chongjiHX")
        {
            //print("------------------------------------------> chognji!!! ");
            GetChongJiHX();
            return;
        }

        if (acName == "chongjiHX2")
        {
            GetChongJiHX2();
            return;
        }

        if (acName == "zidan")
        {
            GetZiDanFire();
            return;
        }

        if (acName == "yuanli")
        {
            GetYuanli();
            return;
        }

        if (acName == "flyToPot1")
        {
            GetFlyToPot1();
            return;
        }

        if (acName == "flyToPot2GD")
        {
            GetFlyToPot2GD();
            return;
        }



        if (acName == "yueguangzhan")
        {
            GetYueGuangZhan();
            return;
        }

        if (acName == "dianqiang")
        {
            //电墙
            GetDianqiang();
            return;
        }

        if (acName == "diandings")
        {
            //电钉
            GetDianding();
            return;
        }


        


        if (acName == "3dianqiu")
        {
            Get3dianqiu();
            return;
        }

        if (acName == "yinshen")
        {
            GetYinshen();
            return;
        }

        if(acName == "gotoAtk")
        {
            GetGotoAtk();
            return;
        }

        if (acName == "dazhan")
        {
            GetDaZhan();
            return;
        }

        if (acName == "AIZiDans")
        {
            GetAIZiDans();
            return;
        }

        if (acName == "AIZiDanDingxiang")
        {
            //固定方向子弹
            GetAIZiDanDingxiang();
            return;
        }

        if (acName == "AIFlyPenhuo")
        {
            GetAIFlyPenhuo();
            return;
        }



        //print("    -------->acName "+ acName);
        PtAtk();
       
    }

    private void GetDianding()
    {
        if (!isActioning)
        {
            //print("111");
            isActioning = true;
            ZhuanXiang();
            GetComponent<JN_Diandings>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<JN_Diandings>().IsGetOver())
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

    void GetDianqiang()
    {

        if (!isActioning)
        {
            //print("111");
            isActioning = true;
            ZhuanXiang();
            GetComponent<JN_Dianqiang>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<JN_Dianqiang>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }



    void GetDaZhan() {
        if (!isActioning)
        {
            //print("111");
            isActioning = true;
            ZhuanXiang();
            GetComponent<JN_Dazhan>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<JN_Dazhan>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }



    public override void QuXiaoAC()
    {
        //这里还要 清除掉 招式里面的 resetAll
        //print("直接取消！！！！！！");

        isAction = false;
        isActioning = false;
        //acName = "";
        if (GetComponent<JN_YueGuanZhan>()) GetComponent<JN_YueGuanZhan>().ReSetAll();
        if (GetComponent<AIChongji>()) GetComponent<AIChongji>().ReSetAll();
        if (GetComponent<AIAirRunAway>()) GetComponent<AIAirRunAway>().ReSetAll();
        if (GetComponent<AIZiDan>()) GetComponent<AIZiDan>().ReSetAll();
        if (GetComponent<AIAirGoToAndAC>()) GetComponent<AIAirGoToAndAC>().ReSetAll();
        if (GetComponent<JN_Dazhan>()) GetComponent<JN_Dazhan>().ReSetAll();
        
        if (GetComponent<AIZongYa>()) GetComponent<AIZongYa>().ReSetAll();
        if (GetComponent<AIHengXiangChongZhuang>()) GetComponent<AIHengXiangChongZhuang>().ReSetAll();
        if (GetComponent<AIChongJiHX>()) GetComponent<AIChongJiHX>().ReSetAll();
        if (GetComponent<AIYinshen>()) GetComponent<AIYinshen>().ReSetAll();
        if (GetComponent<JN_3Dianqiu>()) GetComponent<JN_3Dianqiu>().ReSetAll();
        if (GetComponent<AIFlyPenghuo>()) GetComponent<AIFlyPenghuo>().ReSetAll();
        if (GetComponent<AI_ZidanDingxiang>()) GetComponent<AI_ZidanDingxiang>().ReSetAll();
        if (GetComponent<AI_ZiDans>()) GetComponent<AI_ZiDans>().ReSetAll();

        if (GetComponent<AI_HXChongji2>()) GetComponent<AI_HXChongji2>().ReSetAll();
        if (GetComponent<AI_GDPotZhongya>()) GetComponent<AI_GDPotZhongya>().ReSetAll();
    }

    void Get3dianqiu()
    {
        if (!isActioning)
        {
            //print("111");
            isActioning = true;
            ZhuanXiang();
            GetComponent<JN_3Dianqiu>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<JN_3Dianqiu>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }


    void GetChongji()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIChongji>().GetStart(thePlayer);
            atkNum++;
            //GetAtkNumReSet();
            return;
        }

        if (isActioning && GetComponent<AIChongji>().IsChongjiOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }

    void GetLianDan()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AI_LianDan>().GetStart(thePlayer);
            atkNum++;
            //GetAtkNumReSet();
            return;
        }

        if (isActioning && GetComponent<AI_LianDan>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }


    void GetChongJiHX()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIChongJiHX>().GetStart(thePlayer);
            atkNum++;
            //GetAtkNumReSet();
            return;
        }

        if (isActioning && GetComponent<AIChongJiHX>().IsChongjiOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }

    void GetChongJiHX2()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AI_HXChongji2>().GetStart(thePlayer);
            atkNum++;
            //GetAtkNumReSet();
            return;
        }

        if (isActioning && GetComponent<AI_HXChongji2>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }

    void GetZiDanFire()
    {
        //print("ffffff");
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIZiDan>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        //print("ac  "+acName+"   isActioning   " + isActioning+ "    -------- IsBehaviorOver   "+ GetComponent<AIZiDan>().IsBehaviorOver());

        if (isActioning && GetComponent<AIZiDan>().IsBehaviorOver())
        {
            //print("   zidan  wanjie????? ");
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }

    void GetYuanli()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIAirRunAway>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AIAirRunAway>().GetYuanliOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }

    
     void GetFlyToPot1()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIFlyToPot1>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AIFlyToPot1>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }

    //飞到2个固定点
    void GetFlyToPot2GD()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIFlyToPot2GD>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AIFlyToPot2GD>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }



    void GetYinshen()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIYinshen>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AIYinshen>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            
            isActioning = false;

            //print(" ------------------------------->  隐身结束   ");
            
            GetComponent<AIYinshen>().BeHitFanjiAC();

        }
    }


    void GetGotoAtk()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIAirGoToAndAC>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AIAirGoToAndAC>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;


        }
    }


    void GetHengXiangChongZhuang()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIHengXiangChongZhuang>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AIHengXiangChongZhuang>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;


        }
    }

    void GetHengXiangCZKuaiSu()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIHengXiangCZKuaiSu>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AIHengXiangCZKuaiSu>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;


        }
    }

    

    void GetZongYa()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIZongYa>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AIZongYa>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }

    void GetGDDZongYa()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AI_GDPotZhongya>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AI_GDPotZhongya>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }



    void GetAIZiDans()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AI_ZiDans>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AI_ZiDans>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }

    void GetAIZiDanDingxiang()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AI_ZidanDingxiang>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AI_ZidanDingxiang>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }

    

    void GetAIFlyPenhuo()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIFlyPenghuo>().GetStart(thePlayer);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AIFlyPenghuo>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            isActioning = false;
        }
    }


}
