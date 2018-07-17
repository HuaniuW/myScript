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
        skill.GetComponent<JN_base>().GetPositionAndTeam(this.transform.position, this.transform.GetComponent<RoleDate>().team);
        //取到技能vo
        //AtkAttributesVO _atkVVo = skill.GetComponent<AtkAttributesVO>();
    }
}
