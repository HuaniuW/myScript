﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_yueguang : MonoBehaviour
{
    GameObject hitKuai;
    // Use this for initialization
    void Start () {
		
	}

    private void OnEnable()
    {
        GetComponentInChildren<AtkAttributesVO>().getValue(DataZS.atk_1_v);
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void getPositionAndTeam(Vector3 _position, float team)
    {
        this.transform.position = _position;
        GetComponentInChildren<AtkAttributesVO>().team = team;
        //hitKuai.GetComponent<AtkAttributesVO>().team = team;
        //hitKuai.transform.position = _position;
    }
}
