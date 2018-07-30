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
       // print("-------------------->  "+ (myPropInfo == null)+"  >  "+ myPropInfo+"   length "+ arrays.Length);

    }

    // Update is called once per frame
    void Update () {
        //NearRoleInDistance(4);
        GetAtkFS();
    }
    //选中行为 攻击招式 攻击距离范围 移动 攻击 攻击完成   
    //动画的对照 是否播完 来判断下一步  全局化 攻击动作的 静态常量？
    

    //   切换招式组
    //string[] zsarr1 = { "atk_1", "atk_2", "atk_1", "atk_3" };
    //string[] zsarr2 = { "atk_1", "atk_2", "atk_3", "atk_3" };
    //string[] zsarr3 = { "atk_1", "atk_1", "atk_1", "atk_3" };
    string[][] arrays = { new string[]{ "atk_1", "atk_2", "atk_1", "atk_3"}, new string[] { "atk_1", "atk_2", "atk_3", "atk_3" }, new string[] { "atk_1", "atk_1", "atk_1", "atk_3" } };

    //string[,] arrays = { { "atk_1", "atk_2", "atk_1", "atk_3" } };

    //1 找到招式组
    
    
   
    //根据招式名称 获取招式数据
    VOAtk atkvo;
    VOAtk GetAtkVOByName(string _name, System.Object obj)
    {
        Dictionary<string, string> dic = GetDateByName.GetInstance().GetDicSSByName(_name, obj);
        atkvo = new VOAtk();
        atkvo.GetVO(dic);
        return atkvo;
    }

    
    int atkNum = 0;
    //随机获取列
    int lie = -1;
    int GetLie()
    {
        return (int)UnityEngine.Random.Range(0, arrays.Rank-1); 
    }

    string GetZS(bool isAddN = false)
    {
        if (lie == -1) lie = GetLie();
        string[] carr = arrays[lie];
        string zs = ""; 
        //isZSOver = false;
        if (atkNum < carr.Length)
        {
            zs = carr[atkNum];
            if (isAddN) atkNum++;
        }
        else
        {
            //归零
            atkNum = 0;
            //isZSOver = true;
            isAtk = false;
            isAtking = false;
            //重新开始
            lie = -1;
        }
        //print("zs  "+zs);
        return zs;
    }

    bool isZSOver = false;


    //2.招式组第一个攻击动作的攻击距离  位移技能也可以直接取距离
    float atkDistance;

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
            //print(gameBody.GetDB().animation.lastAnimationName);

            //进入范围
            //gameBody.GetAtk();
            return true;
        }
    }

    bool isAtking = false;
    bool isAtk = false;

    //4.攻击
    void GetAtk()
    {
        string zs = GetZS(true);
        if(zs == "")
        {
            isAtk = false;
            return;
        }
        gameBody.GetAtk(zs);
    }
    //5判断攻击是否完成
    bool IsAtkOver()
    {
        if (gameBody.isAtking)
        {
            if (gameBody.GetDB().animation.isCompleted) return true;
        }
        return false;
    }
    //加强AI  会判断每次攻击是否在攻击范围内
    //6.开始下一个攻击
    void GetAtkFS()
    {
        //int r = Random.Range(0, 100);
        //print(r);

        if (!isAtk)
        {
            isAtk = true;
            
            //print("zs  "+GetZS());
            atkDistance = GetAtkVOByName(GetZS(), DataZS.GetInstance()).atkDistance;
        }

        //这种如果再次超出攻击距离会再追踪
        if (!isAtking && NearRoleInDistance(atkDistance))
        {
            isAtking = true;
            //print("??? atking");
            GetAtk();
        }

        if (isAtking)
        {
            if (IsAtkOver())
            {
                isAtking = false;
            }
        }

    }

 


}
