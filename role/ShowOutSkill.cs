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
        //取到技能vo
        //如果是技能VO  按技能方法 直接在技能特效上写攻击力
        //如果是普通攻击 按DateZS里面来算
        //AtkAttributesVO _atkVVo = skill.GetComponent<AtkAttributesVO>();
    }
}
