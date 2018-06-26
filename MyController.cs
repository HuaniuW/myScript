using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyController : MonoBehaviour {
    GameBody _body;

    [Header("水平方向")]
    public float horizontalDirection;

    //水平方向的按键响应 Input.GetAxis
    const string HORIZONTAL = "Horizontal";
    // Use this for initialization
    void Start () {
        _body = GetComponent<GameBody>();
        //print("body: "+_body.);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //print("jump");
            _body.getJump();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            _body.getAtk();
        }

        horizontalDirection = Input.GetAxis(HORIZONTAL);
        if (horizontalDirection > 0) {
            _body.runRight(horizontalDirection);
        } else if(horizontalDirection<0) {
            _body.runLeft(horizontalDirection);
        }
        else
        {
            _body.reSetLR();
        }
        
       
    }
}
