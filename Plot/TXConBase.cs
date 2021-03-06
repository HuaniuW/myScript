﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TXConBase : MonoBehaviour {
    [Header("持续多久消失的时间")]
    public float disTime = 0.7f;

    public bool isRadomZ = false;

    Vector3 _localEulerAngles;


    // Use this for initialization
    void Start () {
        //print("start!!");
        _localEulerAngles = this.transform.localEulerAngles;
    }

    void OnEnable()
    {
        //this.transform.localEulerAngles = _localEulerAngles;
        //print("this.transform.localEulerAngles     "+ this.transform.localEulerAngles);
        //print("this.transform.name      " + this.transform.name+ "   _localEulerAngles   "+ _localEulerAngles+ " this.transform.localRotation " + this.transform.localRotation);
        if (isRadomZ)
        {
            RadomZ();
        }
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject, disTime));
    }

    void RadomZ()
    {
        //float rx = Random.Range(1, 30);
        //transform.localEulerAngles = new Vector3(rx, 0, 0);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
