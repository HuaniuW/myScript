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

    internal void ShowOutSkillByName(string hzSkillName)
    {
      
        GameObject skill = ObjectPools.GetInstance().SwpanObject2(Resources.Load(hzSkillName) as GameObject);
        skill.GetComponent<JN_base>().GetPositionAndTeam(this.transform.position, this.transform.GetComponent<RoleDate>().team,this.transform.localScale.x,this.gameObject);
        //判断是否是普通攻击
        string[] strArr = hzSkillName.Split('_');
        string topStr = strArr[0];
        //print(jnbs);
       
        if (topStr == "pg")
        {
            skill.transform.parent = this.transform;
        }
        
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
