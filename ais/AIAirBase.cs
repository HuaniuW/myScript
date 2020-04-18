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

    protected override void Patrol()
    {
        //print("AI 巡逻！！！！！！！！！！！！！！！！！！！！！！！！！！！！");
        if (isPatrolRest)
        {
            //print("rest!!!!!!!");
            PatrolResting();
            return;
        }
        isNearing = false;
        //print("巡逻-----------gameBody.IsHitWall    " + gameBody.IsHitWall + "  " + patrolDistance + "     " + this.transform.position.x+ "   myPosition  "+ myPosition.x);
        //if (gameBody.IsHitWall) print("巡逻-----------gameBody.IsHitWall    "+ gameBody.IsHitWall+"  "+ patrolDistance+"     "+this.transform.position.x);
        if (isRunLeft)
        {
            gameBody.RunLeft(flyXSpeed);
            if (this.transform.position.x - myPosition.x < -patrolDistance || gameBody.IsEndGround || gameBody.IsHitWall)
            {
                if (isRunLeft)
                {
                    isPatrolRest = true;
                    PatrolRest(-1);
                }
                isRunLeft = false;
                isRunRight = true;
            }
        }
        else if (isRunRight)
        {
            gameBody.RunRight(flyXSpeed);
            if (this.transform.position.x - myPosition.x > patrolDistance || gameBody.IsEndGround || gameBody.IsHitWall)
            {
                if (isRunRight)
                {
                    isPatrolRest = true;
                    PatrolRest(-1);
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



    // Update is called once per frame
    void Update()
    {
        GetUpdate2();
    }

    AIYinshen _AIYinshen;

    protected override void AIBeHit()
    {
        //AI闪现
        if (aisx != null) aisx.ReSet();
        isFindEnemy = true;
        isPatrolRest = false;
        isNearAtkEnemy = true;
       
        AIReSet();
        if (aiFanji != null) aiFanji.GetFanji();
        if (_AIYinshen != null) _AIYinshen.BeHitHide();
    }


    public void GetReSet()
    {
        AIReSet();
    }

    protected override void AIReSet()
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


    protected virtual void GetUpdate2()
    {
        if (!gameObj)
        {
            gameObj = GlobalTools.FindObjByName("player");
            return;
        }

        if (gameObj.GetComponent<RoleDate>().isDie)
        {
            gameBody.ResetAll();
            //gameBody.Stand();
            return;
        }

        //被攻击没有重置 isAction所以不能继续攻击了
        if (GetComponent<RoleDate>().isBeHiting)
        {
            AIBeHit();
            return;
        }

        if (gameBody.tag != "AirEnemy" && !gameBody.IsGround)
        {
            return;
        }



        if (isPatrol && !IsFindEnemy())
        {
            AIReSet();
            Patrol();
            return;
        }


        if (!isActioning && IsHitWallOrNoWay)
        {
            isFindEnemy = false;
            AIReSet();
            gameBody.GetStand();
            return;
        }


        //超出追击范围
        IsEnemyOutAtkDistance();

        if (!IsFindEnemy()) return;
        GetAtkFS();
    }


    public float flySpeed = 0.02f;
    public float flyXSpeed = 0;
    public float flyYSpeed = 0;

    protected virtual bool TongY(float distance)
    {
        if (gameObj.transform.position.y - transform.position.y > distance)
        {
            //目标在上面  这里做角度计算 来控制XY 速度
            this.GetComponent<AirGameBody>().RunY(0.9f);
            return false;
        }
        else if (gameObj.transform.position.y - transform.position.y < -distance)
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
        if (gameObj.transform.position.y - transform.position.y > _moveDistance)
        {
            //目标在上 向上移动
            
            this.GetComponent<AirGameBody>().RunY(speedY);
            return false;
        }
        else if (gameObj.transform.position.y - transform.position.y < -_moveDistance)
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
        if (gameObj.transform.position.x - transform.position.x > distance)
        {
            //目标在右
            gameBody.RunRight(flyXSpeed);
            return false;
        }
        else if (gameObj.transform.position.x - transform.position.x < -distance)
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

    


    public override void ZhuanXiang()
    {
        if (gameObj.transform.position.x - transform.position.x > 0)
        {
            //目标在右
            gameBody.RunRight(flyXSpeed);
        }
        else
        {
            gameBody.RunLeft(flyXSpeed);
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
        //GetComponent<Xunlu>().GetListZB(this.gameObject, gameObj.transform.position, Vector2.zero);
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
        //print("???????????????????????????????????????????????????????????????????????普通攻击");

        if (!isActioning && !(air_aiNear.ZhuijiXY(atkDistance)||DontNear)) return;


        if (!isActioning)
        {
            isActioning = true;
            isNearing = false;
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
                isActioning = false;
                isAction = false;
            }
        }
    }

    protected override void GetAtkFS()
    {
        if (GetComponent<RoleDate>().isDie||!gameObj|| gameObj.GetComponent<RoleDate>().isDie) {
            GetComponent<AIDestinationSetter>().ReSetAll();
            GetComponent<AIPath>().canMove = false;
            return;
        }
        
        if (!isAction)
        {
            isAction = true;
            acName = GetZS();
            //IsGetAtkFSByName = false;

            print(atkNum + " ----------------------------------------------------------------------------------------->   name " + acName);
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

            if (acName == "yueguangzhan" || acName == "zhongzhan"||acName == "yinshen")
            {
                return;
            }

            atkDistance = GetAtkVOByName(GetZS(), DataZS.GetInstance()).atkDistance;
        }

        if (aiQishou && aiQishou.isQishouAtk && !aiQishou.isFirstAtked)
        {
            if (atkDistance == 0f)
            {
                aiQishou.isFirstAtked = true;
            }
            else
            {
                if (Mathf.Abs(gameObj.transform.position.x - transform.position.x) <= atkDistance) aiQishou.isFirstAtked = true;
            }

            return;
        }

        if (acName == "jn")
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

        if(acName == "zidan")
        {
            GetZiDanFire();
            return;
        }

        if (acName == "yuanli")
        {
            GetYuanli();
            return;
        }

     

        if (acName == "yueguangzhan")
        {
            GetYueGuangZhan();
            return;
        }

        if (acName == "yinshen")
        {
            GetYinshen();
            return;
        }

        PtAtk();
       
    }

    void GetYueGuangZhan()
    {

        if (!isActioning)
        {
            //print("111");
            isActioning = true;
            ZhuanXiang();
            GetComponent<JN_YueGuanZhan>().GetStart(gameObj);
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

    public void QuXiaoAC()
    {
        //这里还要 清除掉 招式里面的 resetAll
        isAction = false;
        isActioning = false;
        if (GetComponent<JN_YueGuanZhan>()) GetComponent<JN_YueGuanZhan>().ReSetAll();
        if (GetComponent<AIChongji>()) GetComponent<AIChongji>().ReSetAll();
        if (GetComponent<AIAirRunAway>()) GetComponent<AIAirRunAway>().ReSetAll();
        if (GetComponent<AIZiDan>()) GetComponent<AIZiDan>().ReSetAll();

    }

    void GetChongji()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIChongji>().GetStart(gameObj.transform);
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

    void GetZiDanFire()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIZiDan>().GetStart(gameObj);
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

    void GetYuanli()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIAirRunAway>().GetStart(gameObj);
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

    void GetYinshen()
    {
        if (!isActioning)
        {
            isActioning = true;
            GetComponent<AIYinshen>().GetStart(gameObj);
            atkNum++;
            return;
        }

        if (isActioning && GetComponent<AIYinshen>().IsGetOver())
        {
            ZhuanXiang();
            isAction = false;
            
            isActioning = false;

            print(" ------------------------------->  隐身结束   ");
            
            GetComponent<AIYinshen>().BeHitFanjiAC();

        }
    }
}
