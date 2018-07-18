using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetBeHit(AtkAttributesVO atkVVo)
    {
        print("被击中 !! "+this.transform.name+"   攻击力 "+atkVVo.atkPower+"  我的防御力 "+this.GetComponent<RoleDate>().def);
    }
}
