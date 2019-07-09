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

	// Use this for initialization
	void Start () {
        GetStart();
    }

    protected void GetStart()
    {
        gameBody = GetComponent<GameBody>();
        if (GetComponent<AIQiShou>()) aiQishou = GetComponent<AIQiShou>();
        if (!aiFanji) aiFanji = GetComponent<AIFanji>();
        //Type myType = typeof(DataZS);
        myPosition = this.transform.position;
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GET_ENEMY, GetEnemyObj);
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GET_ENEMY, GetEnemyObj);
    }

    public void GetEnemyObj(UEvent e)
    {
        
        gameObj = GlobalTools.FindObjByName("player");
    }

    [Header("是否发现敌人")]
    public bool isFindEnemy = false;
    [Header("发现敌人的距离")]
    public float findEnemyDistance = 10;

    protected bool IsFindEnemy()
    {
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
        if (!isActioning&&(Mathf.Abs(gameObj.transform.position.x - transform.position.x) > outDistance|| Mathf.Abs(gameObj.transform.position.y - transform.position.y) > findEnemyDistance))
        {
            isFindEnemy = false;
            if (aiQishou) aiQishou.isQishouAtk = false;
            gameBody.GetStand();
        }
    }

    //是巡逻 还是站地警戒  


    protected bool isRunLeft = true;
    protected bool isRunRight = false;
    public float patrolDistance = 6;
    Vector3 myPosition;
   

    [Header("巡逻")]
    public bool isPatrol = false;
    protected void Patrol()
    {
        //print("hi");
        //print("hi    " + this.transform.position.x + "    "+myPosition.x+"     "+ patrolDistance);
        //gameBody.RunLeft(-0.4f);
        //return;

        //print(gameBody.IsEndGround +"    "+ gameBody.IsHitWall);

        if (isRunLeft)
        {
            gameBody.RunLeft(-0.4f);
            if (this.transform.position.x - myPosition.x<-patrolDistance|| gameBody.IsEndGround||gameBody.IsHitWall)
            {
                isRunLeft = false;
                isRunRight = true;
            }
        }else if (isRunRight)
        {
            gameBody.RunRight(0.4f);
            if (this.transform.position.x - myPosition.x > patrolDistance || gameBody.IsEndGround || gameBody.IsHitWall)
            {
                isRunLeft = true;
                isRunRight = false;
            }
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
            //isAction = true;
            //isActioning = true;
            //acName = "backUp";
            //gameBody.GetBackUp(14);
            //print("in--------->");
            return;
        }


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
        Dictionary<string, string> dict = GetDateByName.GetInstance().GetDicSSByName(_name, obj);
        atkvo = new VOAtk();
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
        if (aiQishou && aiQishou.isQishouAtk)
        {
            carr = aiQishou.qishouAtkArr;
        }
        else
        {
            carr = GetComponent<AITheWay_dcr>().GetZSArrays(i);
        }
        return i;
    }


    protected string[] carr;
    //获取招式
    protected string GetZS(bool isAddN = false)
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
            if (aiQishou && aiQishou.isQishouAtk) aiQishou.isQishouAtk = false;
            atkNum = 0;
            lie = -1;
        }
    }

    //2.招式组第一个攻击动作的攻击距离  位移技能也可以直接取距离
    protected float atkDistance = 0;

    //3 靠近 达到攻击距离
    protected virtual bool NearRoleInDistance(float distance)
    {
        
        if (DontNear) return true;
        if (gameObj.transform.position.x - transform.position.x > distance)
        {
            //目标在右
            gameBody.RunRight(0.9f);
            return false;
        }
        else if (gameObj.transform.position.x - transform.position.x < -distance)
        {
            //目标在左
            gameBody.RunLeft(-0.9f);
            return false;
        }
        else
        {
            return true;
        }
    }

    //转向
    protected void ZhuanXiang()
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
        gameBody.GetAtk(zs);
    }
    //5判断攻击是否完成
    protected bool IsAtkOver()
    {
        return gameBody.IsAtkOver();
    }


    //动作名称
    protected string acName = "";
    protected string jn_effect = "";
    //加强AI  会判断每次攻击是否在攻击范围内
    //6.开始下一个攻击
    protected void GetAtkFS()
    {
		if(!isAction){
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
			atkDistance = GetAtkVOByName(GetZS(), DataZS.GetInstance()).atkDistance;
		}

        if(aiQishou&&aiQishou.isQishouAtk&&!aiQishou.isFirstAtked)
        {
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


        PtAtk();
        if (acName !="shanxian"){
				
		}      
    }

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
            ZhuanXiang();
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
		if(!isActioning && NearRoleInDistance(atkDistance)){
            isActioning = true;
            aisx.ReSet();
			aisx.isStart = true;
			GetComponent<AIShanxian>().GetTheEnemyPos(gameObj);
            atkNum++;
            GetAtkNumReSet();
            return;
		}

		if(aisx.isOver){
			GetComponent<GameBody>().SetACingfalse();
            //aisx.isStart = false;
            aisx.ReSet();
            isAction = false;
            isActioning = false;
        }

	}

    
    protected void AIBeHit()
    {
        if (aisx != null) aisx.ReSet();
        isFindEnemy = true;
        AIReSet();
        if(aiFanji!=null) aiFanji.GetFanji();
    }

    protected void AIReSet()
    {
        isAction = false;
        isActioning = false;
        lie = -1;
        atkNum = 0;
        acName = "";
        if (aiQishou) aiQishou.isQishouAtk = false;

        //isZSOver = false;
    }

    //用于连招的时候 可以连续动作打完再做其他普通攻击动作 注意前面+"lz"  攻击动作只能写成 "lz_xxx_x"或者"xxx_xx"
    protected bool DontNear = false;

    //一般攻击
    protected void PtAtk(){
        //这种如果再次超出攻击距离会再追踪
        if (!isActioning && (NearRoleInDistance(atkDistance) || DontNear))
        {

            isActioning = true;
            if(!DontNear) ZhuanXiang();
            GetAtk();
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

}
