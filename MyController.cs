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
        if (Globals.isInPlot) return;
        if (GlobalSetDate.instance.IsChangeScreening) return;

        if (Input.anyKey)
        {//得到按下什么键
            //print("anyKey  " + Input.inputString);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //print("jump");
            _body.GetJump();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            //|| Input.GetKeyUp("joystick2Button2")
            //values.GetValue(x).ToString()
            _body.GetAtk();
        }

       
        if (Input.GetKeyDown(KeyCode.J)) {
            _body.GetDodge1();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            _body.GetSkill1();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            _body.GetSkill2();
        }



        horizontalDirection = Input.GetAxis(HORIZONTAL);
        if (horizontalDirection > 0) {
            _body.RunRight(horizontalDirection);
        } else if(horizontalDirection<0) {
            _body.RunLeft(horizontalDirection);
        }
        else
        {
            _body.ReSetLR();
        }
        
       
    }
}
