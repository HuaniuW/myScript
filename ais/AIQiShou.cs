using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIQiShou : MonoBehaviour {


    public bool isQishouAtk = false;
    public string[] qishouAtkArr = {"atk_1"};
    public bool isFirstAtked = false;
	// Use this for initialization
	void Start () {
		//print("起手----------------------------------->     "+qishouAtkArr.ToString()+"       "+qishouAtkArr.Length+"   ??    "+qishouAtkArr[0]);
        if (qishouAtkArr.Length == 0) isQishouAtk = false;
		//print("isQishouAtk    "+ isQishouAtk);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
