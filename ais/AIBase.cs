using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AIBase : MonoBehaviour {

    GameBody gameBody;
    public GameObject gameObj;
    AIQiShou aiQishou;
	// Use this for initialization
	void Start () {
        gameBody = GetComponent<GameBody>();
        if (GetComponent<AIQiShou>()) aiQishou = GetComponent<AIQiShou>();
         Type myType = typeof(DataZS);
        PropertyInfo myPropInfo = myType.GetProperty("tt");
        myPosition = this.transform.position;
    }

    [Header("是否发现敌人")]
    public bool isFindEnemy = false;
    [Header("发现敌人的距离")]
    public float findEnemyDistance = 10;

    bool IsFindEnemy()
    {
        if (isFindEnemy) return true;
        if(Mathf.Abs(gameObj.transform.position.x - transform.position.x)< findEnemyDistance&& Mathf.Abs(gameObj.transform.position.y - transform.position.y) < findEnemyDistance)
        {
            isFindEnemy = true;
            //isPatrol = false;
            return true;
        }
        return false;
    }

    public float outDistance = 15;
    void IsEnemyOutAtkDistance()
    {
        if (!isActioning&&(Mathf.Abs(gameObj.transform.position.x - transform.position.x) > outDistance|| Mathf.Abs(gameObj.transform.position.y - transform.position.y) > findEnemyDistance))
        {
            isFindEnemy = false;
            if (aiQishou) aiQishou.isQishouAtk = false;
            //gameBody.GetStand();
        }
    }

    //是巡逻 还是站地警戒  


    bool isRunLeft = true;
    bool isRunRight = false;
    public float patrolDistance = 6;
    Vector3 myPosition;
   

    [Header("巡逻")]
    public bool isPatrol = false;
    void Patrol()
    {
        //print("hi");
        if (isRunLeft)
        {
            gameBody.RunLeft(-0.2f);
            if (this.transform.position.x - myPosition.x<-patrolDistance|| gameBody.IsEndGround||gameBody.IsHitWall)
            {
                isRunLeft = false;
                isRunRight = true;
            }
        }else if (isRunRight)
        {
            gameBody.RunRight(0.2f);
            if (this.transform.position.x - myPosition.x > patrolDistance || gameBody.IsEndGround || gameBody.IsHitWall)
            {
                isRunLeft = true;
                isRunRight = false;
            }
        }
    }

    public bool isEndXXXXX;
    bool IsHitWallOrNoWay
    {
        get
        {
            isEndXXXXX = gameBody.IsEndGround;
            return gameBody.IsEndGround;
        }
        
    }

    
    // Update is called once per frame
    void Update () {

        //print("myPosition    "+ myPosition);
        if (!gameObj) return;

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

        if (!gameBody.IsGround)
        {
            return;
        }



        if (isPatrol&& !IsFindEnemy())
        {
            AIReSet();
            Patrol();
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

        //NearRoleInDistance(4);
        
        
        GetAtkFS();
    }
    //选中行为 攻击招式 攻击距离范围 移动 攻击 攻击完成   
    //动画的对照 是否播完 来判断下一步  全局化 攻击动作的 静态常量？


    //   切换招式组
    //string[] zsarr1 = { "atk_1", "atk_2", "atk_1", "atk_3" };
    //string[] zsarr2 = { "atk_1", "atk_2", "atk_3", "atk_3" };
    //string[] zsarr3 = { "atk_1", "atk_1", "atk_1", "atk_3","atk_3" };
    //string[,] az2 = { zsarr1,zsarr2 };
    /**
    Array[] atkArrs = new Array[] {};
    string[] GetZSArrays()
    {
        Array[] atkArrs = { zsarr1, zsarr2, zsarr3 };
        //print("a: "+ atkArrs.Length);
        string[] zss = (string[])atkArrs[0];
        print(zss[0]);
        return zss;
    }
    */

    //string[][] arrays = { new string[]{"shanxian","atk_1", "atk_2", "atk_3"}, new string[] { "walkBack", "atk_1","backUp","atk_2", "atk_3", "rest_1", "atk_3" }, new string[] { "atk_1", "atk_1", "atk_1", "atk_1", "atk_1", "atk_1", "atk_3" } };

    //string[,] arrays = { { "atk_1", "atk_2", "atk_1", "atk_3" } };

    //1 找到招式组
    
    
   
    //根据招式名称 获取招式数据
    VOAtk atkvo;
    VOAtk GetAtkVOByName(string _name, System.Object obj)
    {
        Dictionary<string, string> dict = GetDateByName.GetInstance().GetDicSSByName(_name, obj);
        atkvo = new VOAtk();
        atkvo.GetVO(dict);
        return atkvo;
    }

    
    int atkNum = 0;
    //随机获取列
    int lie = -1;
    int GetLie()
    {
        int i = (int)UnityEngine.Random.Range(0, GetComponent<AITheWay_dcr>().GetZSArrLength());
        //调试用
        i = 1;
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


    string[] carr;
    //获取招式
    string GetZS(bool isAddN = false)
    {
        if (lie == -1) lie = GetLie();
        string zs = "";
        
        if (atkNum < carr.Length)
        {
            zs = carr[atkNum];
            if (isAddN)
            {
                atkNum++;
                GetAtkNumReSet();
            }
           
        }
        return zs;
    }

    //重置攻击招式的次数位置  非进攻招式放最后面会不重置导致错误
    void GetAtkNumReSet()
    {
        if (atkNum >= carr.Length)
        {
            if (aiQishou && aiQishou.isQishouAtk) aiQishou.isQishouAtk = false;
            atkNum = 0;
            lie = -1;
        }
    }

    //2.招式组第一个攻击动作的攻击距离  位移技能也可以直接取距离
    float atkDistance = 0;

    //3 靠近 达到攻击距离
    bool NearRoleInDistance(float distance)
    {
        
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
    void ZhuanXiang()
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

    bool isActioning = false;
    bool isAction = false;

    //4.攻击
    void GetAtk()
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
    bool IsAtkOver()
    {
        return gameBody.IsAtkOver();
    }


    //动作名称
	string acName = "";
    //加强AI  会判断每次攻击是否在攻击范围内
    //6.开始下一个攻击
    void GetAtkFS()
    {
		if(!isAction){
			isAction = true;
			acName = GetZS();

            //print(atkNum + "    name " + acName);
            string[] strArr = acName.Split('_');
            if (acName == "walkBack") return;
           


            if (acName == "backUp")
            {
                gameBody.GetBackUp();
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

        if (acName == "walkBack")
        {
            GetWalkBack();
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
    void GetWalkBack()
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

    void GetBackUp()
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

    void GetRest()
    {
        if (!isActioning)
        {
            isActioning = true;
            gameBody.GetStand();
            atkNum++;
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

	AIShanxian aisx;
	void GetShanXian(){
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

    void AIBeHit()
    {
        if (aisx != null) aisx.ReSet();
        isFindEnemy = true;
        AIReSet();
    }

    void AIReSet()
    {
        isAction = false;
        isActioning = false;
        lie = -1;
        atkNum = 0;
        acName = "";
        if (aiQishou) aiQishou.isQishouAtk = false;

        //isZSOver = false;
    }

    //一般攻击
	void PtAtk(){

        //这种如果再次超出攻击距离会再追踪
        if (!isActioning && NearRoleInDistance(atkDistance))
        {
            isActioning = true;
            ZhuanXiang();
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

}
