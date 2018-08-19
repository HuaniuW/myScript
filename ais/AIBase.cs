using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AIBase : MonoBehaviour {

    GameBody gameBody;
    public GameObject gameObj;
	// Use this for initialization
	void Start () {
        gameBody = GetComponent<GameBody>();
      
        //DataZS d = DataZS.GetInstance();
        Type myType = typeof(DataZS);
        PropertyInfo myPropInfo = myType.GetProperty("tt");
        //print("-------------------->  "+ (myPropInfo == null)+"  >  "+ myPropInfo+"   length "+ arrays.Length);

    }

    // Update is called once per frame
    void Update () {
        //NearRoleInDistance(4);
        //被攻击没有重置 isAction所以不能继续攻击了
        if (GetComponent<RoleDate>().isBeHiting)
        {
            AIBeHit();
            return;
        }
        
        GetAtkFS();
    }
    //选中行为 攻击招式 攻击距离范围 移动 攻击 攻击完成   
    //动画的对照 是否播完 来判断下一步  全局化 攻击动作的 静态常量？
    

    //   切换招式组
    //string[] zsarr1 = { "atk_1", "atk_2", "atk_1", "atk_3" };
    //string[] zsarr2 = { "atk_1", "atk_2", "atk_3", "atk_3" };
    //string[] zsarr3 = { "atk_1", "atk_1", "atk_1", "atk_3" };
    string[][] arrays = { new string[]{"shanxian","atk_1", "atk_2", "atk_3"}, new string[] { "atk_1", "atk_2", "atk_3", "atk_3" }, new string[] { "atk_1", "atk_1", "atk_1", "atk_1", "atk_1", "atk_1", "atk_3" } };

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
		int i = (int)UnityEngine.Random.Range(0, arrays.Length);
        i = 0;
		//print("随机 " + i);
		return i;//(int)UnityEngine.Random.Range(0, arrays.Rank-1); 
    }

    //获取招式
    string GetZS(bool isAddN = false)
    {
        //if (lie == 1000) return null;
        if (lie == -1) lie = GetLie();
        string[] carr = arrays[lie];
        string zs = "";
        //isZSOver = false;
        //print("atkName " + acName + "     atkNum  " + atkNum );
        if (atkNum < carr.Length)
        {
            zs = carr[atkNum];
            //print(">>>?????????????   " + atkNum + "  ---   " + zs);
            //print(lie+"     "+zs);
            if (isAddN)
            {
                atkNum++;
                if(atkNum >= carr.Length)
                {
                    atkNum = 0;
                    lie = -1;
                }
            }
           
        }
        return zs;
    }

    //2.招式组第一个攻击动作的攻击距离  位移技能也可以直接取距离
    float atkDistance;

    //3 靠近 达到攻击距离
    bool NearRoleInDistance(float distance)
    {
        
        if (gameObj.transform.position.x - transform.position.x > distance)
        {
            //目标在右
            //print("??????1111111");
            gameBody.RunRight(0.9f);
            return false;
        }
        else if (gameObj.transform.position.x - transform.position.x < -distance)
        {
            //print("??????22222");
            //目标在左
            gameBody.RunLeft(-0.9f);
            return false;
        }
        else
        {
           
            return true;
        }
    }

    bool isActioning = false;
    bool isAction = false;

    //4.攻击
    void GetAtk()
    {
        string zs = GetZS(true);
		//print("zs   "+zs);
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
            //Debug.Log("atkName "+acName);
            //Console.WriteLine("atkName " + acName);
            
            if (acName == "shanxian")
            {
                aisx = GetComponent<AIShanxian>();
                atkDistance = aisx.sxDistance;
                return;
            }
			atkDistance = GetAtkVOByName(GetZS(), DataZS.GetInstance()).atkDistance;
		}

		if(acName == "shanxian"){
			GetShanXian();
			return;
		}

		if(acName !="shanxian"){
			//print("???????????????");
			PtAtk();	
		}      
    }

	AIShanxian aisx;
	void GetShanXian(){
		//print("? "+NearRoleInDistance(atkDistance)+"   >  "+aisx.isStart);

		if(!isActioning && NearRoleInDistance(atkDistance)){
            isActioning = true;
            aisx.ReSet();
			aisx.isStart = true;
			//print("oooooo");
			GetComponent<AIShanxian>().GetTheEnemyPos(gameObj);
            atkNum++;
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
        AIReSet();
    }

    void AIReSet()
    {
        isAction = false;
        isActioning = false;
        lie = -1;
        atkNum = 0;
        //isZSOver = false;
    }

    //一般攻击
	void PtAtk(){

        //这种如果再次超出攻击距离会再追踪
        if (!isActioning && NearRoleInDistance(atkDistance))
        {
            isActioning = true;
            //print("??? atking");
            GetAtk();
        }

        if (isActioning)
        {
            if (IsAtkOver())
            {
               // print("??>>>");
                isActioning = false;
                isAction = false;
            }
        }
	}

}
