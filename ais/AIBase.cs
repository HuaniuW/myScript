using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AIBase : MonoBehaviour {

    protected GameBody gameBody;
    public GameObject gameObj;
    protected AIQiShou aiQishou;
    protected AIFanji aiFanji;

    [Header("是否是不能动的 怪物")]
    public bool IsCanNotMove = false;

	// Use this for initialization
	void Start () {
        GetStart();
    }

    protected void GetStart()
    {
        
        GetGameBody();
        if (GetComponent<AIQiShou>()) aiQishou = GetComponent<AIQiShou>();
        if (!aiFanji) aiFanji = GetComponent<AIFanji>();
        
         //Type myType = typeof(DataZS);
         myPosition = this.transform.position;
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GET_ENEMY, GetEnemyObj);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.DIE_OUT, playerDie);

    }

    bool isPlayerDie = false;
    
    private void playerDie(UEvent evt)
    {
        //print("dieout---------------------------------------------@@   "+evt.eventParams.ToString());
        if(evt.eventParams!=null&&evt.eventParams.ToString() == "Player")
        {
            isPlayerDie = true;
            Globals.isPlayerDie = true;
            //GetComponent<GameBody>().ResetAll();
            if(GetComponent<GameBody>().GetDB().animation.lastAnimationName!="stand_1")GetComponent<GameBody>().GetStand();
            //print("palyer is die !!!!!!!!!");
            //GetComponent<GameBody>().GetStand();
        }
        
    }

    protected virtual void GetGameBody()
    {
        gameBody = GetComponent<GameBody>();
    }
    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GET_ENEMY, GetEnemyObj);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DIE_OUT, playerDie);
    }

    public void GetEnemyObj(UEvent e)
    {
        
        gameObj = GlobalTools.FindObjByName("player");
    }

    [Header("是否发现敌人")]
    public bool isFindEnemy = false;
    [Header("发现敌人的距离")]
    public float findEnemyDistance = 10;

    [Header("发现敌人 是否采取攻击")]
    public bool isNearAtkEnemy = true;
    protected bool IsFindEnemy()
    {
        if (!isNearAtkEnemy) return false;
        if (isFindEnemy) return true;
        if(Mathf.Abs(gameObj.transform.position.x - transform.position.x)< findEnemyDistance&& Mathf.Abs(gameObj.transform.position.y - transform.position.y) < findEnemyDistance)
        {
            isFindEnemy = true;
            //isPatrol = false;
            GetComponent<GameBody>().InFightAtk();
            return true;
        }
        return false;
    }

    public float outDistance = 15;
    protected void IsEnemyOutAtkDistance()
    {
        if (!isActioning&&!aiQishou.isQishouAtk&&(Mathf.Abs(gameObj.transform.position.x - transform.position.x) > outDistance|| Mathf.Abs(gameObj.transform.position.y - transform.position.y) > findEnemyDistance))
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
        if (Mathf.Abs(gameObj.transform.position.x - transform.position.x) > outDistance || Mathf.Abs(gameObj.transform.position.y - transform.position.y) > findEnemyDistance) return true;
        return false;
    }

    //是巡逻 还是站地警戒  


    protected bool isRunLeft = true;
    protected bool isRunRight = false;
    public float patrolDistance = 6;
    protected Vector3 myPosition;
   

    [Header("巡逻")]
    public bool isPatrol = false;
    protected virtual void Patrol()
    {
        if (isPatrolRest) {
            PatrolResting();
            return;
        }


        if (isRunLeft)
        {
            gameBody.RunLeft(-0.4f);
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
            gameBody.RunRight(0.4f);
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
        
        if (isPlayerDie) return;
        GetUpdate();
    }

    protected virtual void GetUpdate()
    {
        if (GetComponent<RoleDate>().isDie)
        {
            return;
        }
        

        if (!gameObj)
        {
            gameObj = GlobalTools.FindObjByName("player");
            return;
        }

        if (!gameObj&&gameObj.GetComponent<RoleDate>().isDie)
        {
            gameBody.ResetAll();
            //gameBody.Stand();
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
        //print("_name   "+_name);
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

    //3 靠近 达到攻击距离
    public virtual bool NearRoleInDistance(float distance,float nertSpeed  =0.9f)
    {
        
        if (DontNear) return true;
        if (gameObj.transform.position.x - transform.position.x > distance)
        {
            //目标在右
            gameBody.RunRight(nertSpeed);
            return false;
        }
        else if (gameObj.transform.position.x - transform.position.x < -distance)
        {
            //目标在左
            gameBody.RunLeft(-nertSpeed);
            return false;
        }
        else
        {
            return true;
        }
    }

    
    public bool IsInAtkDistance(float distance)
    {
        if (Mathf.Abs(gameObj.transform.position.x - transform.position.x) <= distance) return true;
        return false;
    }

    //转向
    public virtual void ZhuanXiang()
    {
        if (gameObj.transform.position.x - transform.position.x > 0)
        {
            //目标在右
            gameBody.RunRight(0.3f);
        }
        else
        {
            gameBody.RunLeft(-0.3f);
        }
    }

    protected bool isActioning = false;
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
        //isActioning = true;
    }


    //动作名称
    protected string acName = "";
    protected string jn_effect = "";
    //加强AI  会判断每次攻击是否在攻击范围内
    //6.开始下一个攻击
    protected virtual void GetAtkFS()
    {
        //print("atkFS!!!");
        if (isPlayerDie) return;
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


            if (acName == "yishan")
            {
                return;
            }

            if (strArr[0] == "jn") {
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

            if (acName == "yueguangzhan"|| acName == "zhongzhan")
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

            atkDistance = GetAtkVOByName(acName, DataZS.GetInstance()).atkDistance;
		}

        if(aiQishou&&aiQishou.isQishouAtk&&!aiQishou.isFirstAtked)
        {
            //print("---------------------------------------------------》起手攻击  "+ atkDistance);
            if (atkDistance == 0f)
            {
                aiQishou.isFirstAtked = true;
            }
            else
            {
                if(Mathf.Abs(gameObj.transform.position.x - transform.position.x) <= atkDistance) aiQishou.isFirstAtked = true;
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
            if(GetComponent<AIRest>().isZhuanXiang) ZhuanXiang();
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

    
    protected virtual void AIBeHit()
    {
        //if (aisx != null) aisx.ReSet();
        isFindEnemy = true;
        isPatrolRest = false;
        isNearAtkEnemy = true;
        //IsGetAtkFSByName = false;
        AIReSet();
        if(aiFanji!=null) aiFanji.GetFanji();
    }

    protected virtual void AIReSet()
    {
        isAction = false;
        isActioning = false;
        
        lie = -1;
        atkNum = 0;
        acName = "";
        //IsGetAtkFSByName = false;
        //print("起手2   " + aiQishou.isQishouAtk);
        if (aiQishou) aiQishou.isQishouAtk = false;

        //isZSOver = false;
    }

    //用于连招的时候 可以连续动作打完再做其他普通攻击动作 注意前面+"lz"  攻击动作只能写成 "lz_xxx_x"或者"xxx_xx"
    protected bool DontNear = false;

    //一般攻击
    protected virtual void PtAtk(){

        //判断 是否hi不能动的怪物
        if (!IsCanNotMove)
        {
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
            if (!DontNear&&!IsInAtkDistance(atkDistance))
            {
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
            GetComponent<AIRunCut>().GetStart(gameObj);
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
            GetComponent<AI_Chongci>().GetStart(gameObj);
            atkNum++;
            //GetAtkNumReSet();
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


    void GetZhongZhan()
    {

        if (!isActioning)
        {
            //print("111   zhongzhan!!!!!!! ");
            isActioning = true;
            ZhuanXiang();
            GetComponent<JN_zhongzhan>().GetStart(gameObj);
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



    void GetLuanRen()
    {

        if (!isActioning)
        {
            //print("111   zhongzhan!!!!!!! ");
            isActioning = true;
            ZhuanXiang();
            GetComponent<JN_LuanRen2>().GetStart(gameObj);
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
            GetComponent<AIYiShan>().GetStart();
            atkNum++;
            //GetAtkNumReSet();
        }

    }

    protected void JNAtk()
    {
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
            GetComponent<AIYuanLiHuiXue>().GetStart();
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
