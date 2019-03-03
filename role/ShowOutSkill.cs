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

    internal void ShowOutSkillByName(string hzSkillName,bool isSkill = false)
    {

        //if(Globals.isDebug)print(" hzSkillName  "+ hzSkillName);
        GameObject skill = ObjectPools.GetInstance().SwpanObject2(Resources.Load(hzSkillName) as GameObject);
        //if (Globals.isDebug)
        //{
        //    print("skill  "+ skill);
        //    print("this.transform.position  " + this.transform.position);
        //    print("this.transform.GetComponent<RoleDate>().team  " + this.transform.GetComponent<RoleDate>().team);
        //    print("this.transform.localScale.x  " + this.transform.localScale.x);
        //    print("this.gameObject  " + this.gameObject);
        //}
        

        skill.GetComponent<JN_base>().GetPositionAndTeam(this.transform.position, this.transform.GetComponent<RoleDate>().team,this.transform.localScale.x,this.gameObject, isSkill);
        
        
        //TXPlay2(skill);
        //取到技能vo
        //如果是技能VO  按技能方法 直接在技能特效上写攻击力
        //如果是普通攻击 按DateZS里面来算
        //AtkAttributesVO _atkVVo = skill.GetComponent<AtkAttributesVO>();
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
