﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitKuai : MonoBehaviour {

    public int teamNum;
	// Use this for initialization
	void Start () {
        //print(this.transform);
	}

    AtkAttributesVO _atkVVo;
    //GameObject的位置  攻击属性  相对位置x y  尺寸   是否立即消失
    public void getAtkVVo(AtkAttributesVO atkVVo)
    {
        _atkVVo = atkVVo;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D Coll)
    {
       
        if(Coll.GetComponent<RoleDate>()&& Coll.GetComponent<RoleDate>().team != _atkVVo.team)
        {
            if (Coll.GetComponent<BeHit>()) Coll.GetComponent<BeHit>().beHit(_atkVVo);
        }
        
        
    }
    void OnTriggerExit2D(Collider2D Coll)
    {
        //print("Trigger - B");
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        //print("Trigger - C");
    }
}
