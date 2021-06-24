using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOutSkill : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 释放特效
    /// </summary>
    /// <param name="hzSkillName">特效名字</param>
    /// <param name="isSkill">是否是技能</param>
    internal void ShowOutSkillByName(string hzSkillName,bool isSkill = false,VOAtk vOAtk = null)
    {

        //if(Globals.isDebug)print(" hzSkillName  "+ hzSkillName);

        //print("-------------------------------------------------------------------？？？？？？？？？hzSkillName   "+ hzSkillName);
        GameObject skillObj;
        GameObject skill;
        //int nums = 0;
        //string[] _strArr = hzSkillName.Split('|');
        //if (_strArr.Length > 1)
        //{
        //    skillObj = Resources.Load(_strArr[0]) as GameObject;
        //    //nums = int.Parse(_strArr[1]);
        //    //间隔时间 这里就不多做了  直接在动画里面 多几个 事件就可以了
        //}
        //else
        //{
        //    skillObj = Resources.Load(hzSkillName) as GameObject;
        //}

        skillObj = Resources.Load(hzSkillName) as GameObject;
      

        if (skillObj == null)
        {
            print("  skillObj = null  ");
            //Time.timeScale = 0;
            return;
        }
        skill = ObjectPools.GetInstance().SwpanObject2(skillObj);
        //skill.transform.parent = this.transform.parent;

        //print("parent  >>>   " + skill.transform.parent+"    playerpARENT   "+GlobalTools.FindObjByName("player").transform.position);


        if (skill.GetComponent<TX_Dianqiang>())
        {
            //***以后 所有 可以直接丢出去的 技能 在这里 做  统一接口************
            //电墙  
            skill.transform.position =  new Vector2(this.transform.position.x, skill.GetComponent<TX_Dianqiang>().PosY);
            skill.GetComponent<JN_base>().atkObj = this.gameObject;
            skill.GetComponent<JN_Date>().team = GetComponent<RoleDate>().team;
            skill.GetComponent<TX_Dianqiang>().GetStart();
            skill.GetComponent<TX_Dianqiang>().SetSpeedFX(-this.transform.localScale.x);
            return;
        }




        if (skill.GetComponent<TX_zidan>()) {
            if (this.GetComponent<GameBody>().zidanPos != null)
            {
                GameObject shanGuang = ObjectPools.GetInstance().SwpanObject2(Resources.Load("TX_zidan1shan") as GameObject);  //GlobalTools.GetGameObjectByName("TX_zidan1shan");
                shanGuang.transform.position = this.GetComponent<GameBody>().zidanPos.position;
                skill.transform.position = this.GetComponent<GameBody>().zidanPos.position;
                skill.GetComponent<TX_zidan>().team = GetComponent<RoleDate>().team;
            }

            skill.transform.localScale = this.transform.localScale;
            return;
        }


        if (vOAtk != null)
        {
            //print("vOAtk.chongjili       " + vOAtk.chongjili);
            if (vOAtk.chongjili != 0) skill.GetComponent<JN_Date>().chongjili = vOAtk.chongjili;
            if (vOAtk.hitKuaiOX != 0) skill.GetComponent<JN_Date>().hitKuai_xdx = vOAtk.hitKuaiOX;
            if (vOAtk.hitKuaiOY != 0) skill.GetComponent<JN_Date>().hitKuai_xdy = vOAtk.hitKuaiOY;
            if (vOAtk.hitKuaiSX != 0) skill.GetComponent<JN_Date>().hitKuaiSW = vOAtk.hitKuaiSX;
            if (vOAtk.hitKuaiSY != 0) skill.GetComponent<JN_Date>().hitKuaiSH = vOAtk.hitKuaiSY;
            if (vOAtk.atkPower != 0) skill.GetComponent<JN_Date>().atkPower = vOAtk.atkPower;
            //print("  POWER!!!???    "+skill.GetComponent<JN_Date>().atkPower+"   >>?????     "+ vOAtk.atkPower);
            if (vOAtk.TXOX != 0) skill.GetComponent<JN_Date>()._xdx = vOAtk.TXOX;
            //print(" __xdx  "+ skill.GetComponent<JN_Date>()._xdx+"   ------->>>>>>  "+ vOAtk.TXOX);
            if (vOAtk.TXOY != 0) skill.GetComponent<JN_Date>()._xdy = vOAtk.TXOY;
            if (vOAtk.TXSX != 0) skill.GetComponent<JN_Date>()._scaleW = vOAtk.TXSX;
        }







        //if (Globals.isDebug)
        //{
        //    print("skill  "+ skill);
        //    print("this.transform.position  " + this.transform.position);
        //    print("this.transform.GetComponent<RoleDate>().team  " + this.transform.GetComponent<RoleDate>().team);
        //    print("this.transform.localScale.x  " + this.transform.localScale.x);
        //    print("this.gameObject  " + this.gameObject);
        //}

        //print("攻击时玩家的x周速度  "+this.GetComponent<Rigidbody2D>().velocity.x);
        //print("----------------------------------->????  " + skill.GetComponent<JN_base>());
        if(skill && skill.GetComponent<JN_base>()) skill.GetComponent<JN_base>().GetPositionAndTeam(this.transform.position, this.transform.GetComponent<RoleDate>().team,this.transform.localScale.x,this.gameObject, isSkill);
        //skill.GetComponent<JN_Date>().GetCallBackStart();
        
        //TXPlay2(skill);
        //取到技能vo
        //如果是技能VO  按技能方法 直接在技能特效上写攻击力
        //如果是普通攻击 按DateZS里面来算
        //AtkAttributesVO _atkVVo = skill.GetComponent<AtkAttributesVO>();
    }

    public void ShowOutSkillBeginEffectByName(string effectName)
    {
        return;
        //GameObject beginEffect = ObjectPools.GetInstance().SwpanObject2(Resources.Load(effectName) as GameObject);
        //Transform groundCheck = this.GetComponent<GameBody>().groundCheck;
        
        //beginEffect.transform.parent = this.transform;
        //beginEffect.transform.position = groundCheck.position;
        
        ////scaleX的控制

        ////后面可以加入特殊位置的特效显示 比如武器上 等等

        ////销毁
        //StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(beginEffect, 2));
    }

    void TXPlay2(GameObject tx)
    {
        //tx.transform.localScale = this.transform.localScale;
        //print(tx.Find("tt"));
        //print(tx.transform.localScale);
        
        Vector3 ttt = new Vector3(0, 0, 0);
        ttt = tx.transform.localScale;
        ttt.x = Mathf.Abs(tx.transform.localScale.x);
        ttt.x *= this.transform.localScale.x;
        tx.transform.localScale = ttt;
        
    }
}
