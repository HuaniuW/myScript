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
    internal void ShowOutSkillByName(string hzSkillName,bool isSkill = false)
    {

        //if(Globals.isDebug)print(" hzSkillName  "+ hzSkillName);

        //print("-------------------------------------------------------------------？？？？？？？？？hzSkillName   "+ hzSkillName);
        GameObject skillObj = Resources.Load(hzSkillName) as GameObject;
        
        if (skillObj == null)
        {
            print("  skillObj = null  ");
            //Time.timeScale = 0;
            return;
        }
        GameObject skill = ObjectPools.GetInstance().SwpanObject2(skillObj);
        
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
        skill.GetComponent<JN_base>().GetPositionAndTeam(this.transform.position, this.transform.GetComponent<RoleDate>().team,this.transform.localScale.x,this.gameObject, isSkill);
        //skill.GetComponent<JN_Date>().GetCallBackStart();
        
        //TXPlay2(skill);
        //取到技能vo
        //如果是技能VO  按技能方法 直接在技能特效上写攻击力
        //如果是普通攻击 按DateZS里面来算
        //AtkAttributesVO _atkVVo = skill.GetComponent<AtkAttributesVO>();
    }

    public void ShowOutSkillBeginEffectByName(string effectName)
    {
        GameObject beginEffect = ObjectPools.GetInstance().SwpanObject2(Resources.Load(effectName) as GameObject);
        Transform groundCheck = this.GetComponent<GameBody>().groundCheck;
        //Vector3 ePosition = new Vector3(this.transform.position.x+groundCheck.position.x, this.transform.position.y + groundCheck.position.y, groundCheck.position.z);
        beginEffect.transform.parent = this.transform;
        beginEffect.transform.position = groundCheck.position;
        //print("effectName   "+ effectName);
        //print("beginEffect     "+ beginEffect);
        //beginEffect.transform.position = this.transform.position;
        //scaleX的控制

        //后面可以加入特殊位置的特效显示 比如武器上 等等

        //销毁
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(beginEffect, 2));
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
