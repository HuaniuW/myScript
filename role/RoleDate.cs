﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleDate : MonoBehaviour {
    float _maxLive;
    [Header("最大生命值")]
    public float maxLive = 10000;
    
    [Header("生命值")]
    public float live = 10000;

    [Header("防御力")]
    public float def = 50;

    [Header("攻击力")]
    public float atk = 100;
    // Use this for initialization

    [Header("硬直")]
    public float yingzhi = 100;

    [Header("蓝")]
    public float Lan = 1000;
    public virtual float lan
    {
        get
        {
            return Lan;
        }
        set
        {
            Lan = value;
            if (Lan < 0) Lan = 0;
        }
    }


    [Header("最大蓝")]
    public float maxLan = 1000;

    [Header("被击中时发出的声音")]
    public string beHitVudio = "hit1";

    public bool isCanBeHit = true;

    public bool isBeHit = false;
    public bool isBeHiting = false;

    public bool isDie = false;

    [Header("测试")]
    public string[] testArr;

    public float team = 1;

    [Header("怪的类型")]
    public string enemyType = "enemy";

    [Header("被击中X方向的推力倍数")]
    public float beHitXFScale = 1;


    public string BeHitVudio
    {
        get
        {
            return beHitVudio;
        }

        set
        {
            beHitVudio = value;
        }
    }

    void Start () {
		
	}
	

    public void GetInit()
    {
        isCanBeHit = true;
        isBeHit = false;
        isBeHiting = false;
    }

    //初始硬直
    float csYZ;
    public void addYZ(float yzNums)
    {
        yingzhi += yzNums;
    }

    public void hfYZ(float yzNums)
    {
        yingzhi -= yzNums;
    }




	// Update is called once per frame
	void Update () {
		
	}

    [Header("被动防御技能")]
    public List<string> passive_def_skill = new List<string> { };
}
