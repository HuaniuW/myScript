using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkingAddYZ : MonoBehaviour {
    [Header("是否开启攻击时硬直增加")]
    public bool isOpen = false;

    [Header("硬直增加多少")]
    public float AddYZNums = 500;

    bool isAdded = false;
    // Use this for initialization
    void Start () {
		
	}

    private void init()
    {
        if(isAdded) this.GetComponent<RoleDate>().hfYZ(AddYZNums);
        isAdded = false;
    }

    // Update is called once per frame
    void Update () {
        if (!isOpen) return;

        if (GetComponent<RoleDate>().isBeHiting|| GetComponent<RoleDate>().isDie)
        {
            init();
            return;
        }

        if (isAdded&&this.GetComponent<GameBody>().IsAtkOver())
        {
            this.GetComponent<RoleDate>().hfYZ(AddYZNums);
            isAdded = false;
        }

        if (!isAdded && this.GetComponent<GameBody>().isAtking)
        {
            isAdded = true;
            this.GetComponent<RoleDate>().addYZ(AddYZNums);
        }
    }
}
