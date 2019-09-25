using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TXFZTest : MonoBehaviour {
    GameObject MainCamera;
    Vector3 maPos;
    Vector3 cameraPos;

	// Use this for initialization
	void Start () {
        MainCamera = GameObject.Find("/MainCamera");

    }
	
	// Update is called once per frame
	void Update () {
        maPos = this.transform.position;
        cameraPos = MainCamera.transform.position;



        

    }
}
