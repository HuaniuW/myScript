using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIQiShou : MonoBehaviour {


    public bool isQishouAtk = false;
    public string[] qishouAtkArr = {"atk_1" };
    public bool isFirstAtked = false;
	// Use this for initialization
	void Start () {
        if (qishouAtkArr.Length == 0) isQishouAtk = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
