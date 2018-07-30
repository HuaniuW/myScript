﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleDate : MonoBehaviour {
    float _maxLive;
    [Header("最大生命值")]
    public float maxLive = 1000;
    
    [Header("生命值")]
    public float live = 1000;

    [Header("防御力")]
    public float def = 50;

    [Header("攻击力")]
    public float atk = 100;
    // Use this for initialization

    [Header("硬直")]
    public float yingzhi = 100;

    public bool isCanBeHit = true;

    public bool isBeHit = false;
    public bool isBeHiting = false;

    public float team = 1;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}