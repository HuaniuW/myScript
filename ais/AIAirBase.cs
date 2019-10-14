using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAirBase : AIBase {

	// Use this for initialization
	void Start () {
        GetStart();
	}


    protected override void Patrol()
    {
        if (isPatrolRest)
        {
            PatrolResting();
            return;
        }


        if (isRunLeft)
        {
            gameBody.RunLeft(-0.4f);
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
            gameBody.RunRight(0.4f);
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
            print("是否进来 ！！！");
            restTimes = UnityEngine.Random.Range(1, 2);
        }

        GetComponent<AIRest>().GetRestByTimes(restTimes);
        gameBody.GetStand(false);

    }



    // Update is called once per frame
    void Update () {
        GetUpdate2();
	}

    protected override void AIBeHit()
    {
        if (aisx != null) aisx.ReSet();
        isFindEnemy = true;
        isPatrolRest = false;
        isNearAtkEnemy = true;
        SlowYSpeed(0);
        AIReSet();
        if (aiFanji != null) aiFanji.GetFanji();
    }

    protected override void AIReSet()
    {
        isAction = false;
        isActioning = false;
        choseNearType = false;
        nearTypeNums = 0;
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


    public float flySpeed = 0.9f;
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


    bool choseNearType = false;
    int nearTypeNums = 0;


    public override bool NearRoleInDistance(float distance)
    {
        //预判策略  是否用策略 （如果 角色和怪之间最近距离有障碍 启用策略   这里只考虑最短路径）
        //判断X方向是否 有障碍 有的话 下移动 x线没有碰到 y线没碰到
        //靠近的击中方式  1.xy同时进行  2.先X后Y  3.先Y后X  4.绕后   
        
        if (!choseNearType)
        {
            choseNearType = true;
            nearTypeNums = (int)UnityEngine.Random.Range(0, 100);
        }
        
        if (nearTypeNums <= 100)
        {
            return NearByYThanX(distance);
            //同时进行
            return NearByXAndY(distance);
        }
        else if(nearTypeNums <= 50)
        {
            return NearByXThanY(distance);
        }
        return false;
    }


    void SlowXSpeed()
    {
        gameBody.GetPlayerRigidbody2D().velocity = new Vector2(gameBody.GetPlayerRigidbody2D().velocity.x*0.4f, gameBody.GetPlayerRigidbody2D().velocity.y);
    }

    void SlowYSpeed(float scaleNum = 0.2f) {
        gameBody.GetPlayerRigidbody2D().velocity = new Vector2(gameBody.GetPlayerRigidbody2D().velocity.x , gameBody.GetPlayerRigidbody2D().velocity.y * scaleNum);
    }

    //先x后y
    private bool NearByXThanY(float distance)
    {
        if (gameObj.transform.position.x - transform.position.x > distance)
        {
            //目标在右
            gameBody.RunRight(flySpeed);
            return false;
        }
        else if (gameObj.transform.position.x - transform.position.x < -distance)
        {
            //目标在左
            gameBody.RunLeft(-flySpeed);
            return false;
        }
        else
        {
            SlowXSpeed();
            if (Mathf.Abs(gameObj.transform.position.y - transform.position.y) >= 0.3f) {
                float speedY = GetSpeedY(transform, gameObj.transform);
                this.GetComponent<AirGameBody>().RunY(speedY);
            }
            else
            {
                SlowYSpeed();
                choseNearType = false;
                nearTypeNums = 0;
                return true;
            }
           
        }
        return false;
    }

    //先y后x
    private bool NearByYThanX(float distance)
    {
        if (Mathf.Abs(gameObj.transform.position.y - transform.position.y) >= 0.3f)
        {
            float speedY = GetSpeedY(transform, gameObj.transform);
            this.GetComponent<AirGameBody>().RunY(speedY);
            return false;
        }

        SlowYSpeed();

        if (gameObj.transform.position.x - transform.position.x > distance)
        {
            //目标在右
            gameBody.RunRight(flySpeed);
            return false;
        }
        else if (gameObj.transform.position.x - transform.position.x < -distance)
        {
            //目标在左
            gameBody.RunLeft(-flySpeed);
            return false;
        }

        SlowXSpeed();
        choseNearType = false;
        nearTypeNums = 0;
        return true;
    }

    //x y同时
    bool NearByXAndY(float distance)
    {
        //print("  ------------------------------------------------- ");

        float speedX = GetSpeedX(transform, gameObj.transform);
        float speedY = GetSpeedY(transform, gameObj.transform);

        if(Mathf.Abs(gameObj.transform.position.y - transform.position.y) >= 0.3f) this.GetComponent<AirGameBody>().RunY(speedY);
        if (gameObj.transform.position.x - transform.position.x > distance)
        {
            //目标在右
            gameBody.RunRight(speedX);
        }
        else if (gameObj.transform.position.x - transform.position.x < -distance)
        {
            //目标在左
            gameBody.RunLeft(-speedX);
        }
        else
        {
            SlowXSpeed();
            //print("  xOK "+ speedY);
            if (Mathf.Abs(gameObj.transform.position.y - transform.position.y) < 0.3f)
            {
                SlowYSpeed();
                choseNearType = false;
                nearTypeNums = 0;
                return true;
            }
        }

        return false;
    }

    bool isRaohou = false;
    bool isMoveY = false;
    bool isMoveX = false;

    bool RaoHou(float distance)
    {
        //到一定高度 取到高度的点y  如果顶到墙就直接前进

        //渠道角色背后的点x  如果碰到墙就下降
        if (!isRaohou)
        {
            isRaohou = true;
            //确定Y点
        }


        return false;
    }

    float GetSpeedX(Transform myP,Transform targetP)
    {
        float distanceX = targetP.position.x - myP.position.x;
        return Mathf.Abs(distanceX/Vector2.Distance(myP.position,targetP.position)*flySpeed);
    }

    float GetSpeedY(Transform myP, Transform targetP)
    {
        float distanceY = targetP.position.y - myP.position.y;
        return distanceY / Vector2.Distance(myP.position, targetP.position) * flySpeed;
    }





    protected override void GetAtkFS()
    {
        if (!isAction)
        {
            isAction = true;
            acName = GetZS();

            //print(atkNum + "    name " + acName);
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
                aisx = GetComponent<AIShanxian>();
                atkDistance = aisx.sxDistance;
                return;
            }

            if (acName == "runCut")
            {
                acName = "runCut";
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


        PtAtk();
        if (acName != "shanxian")
        {

        }
    }
}
