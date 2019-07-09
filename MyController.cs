using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.ROLECANCONTROL, this.IsRoleCanControl);
    }

    private void OnDestroy()
    {
        //ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.ROLECANCONTROL, this.IsRoleCanControl);
    }

    //bool _isCanControl = true;
    //void IsRoleCanControl(UEvent e) {
    //    _isCanControl = (bool)e.eventParams;
    //}

    // Update is called once per frame

    bool IsCanControl()
    {
        if (Globals.isInPlot) return false;
        //if (!_isCanControl) return false;
        if (GlobalSetDate.instance.IsChangeScreening) return false;
        if (GetComponent<RoleDate>().isDie) return false;
        return true;
    }

    private string currentButton;
    void ShouBing()
    {
        var values = Enum.GetValues(typeof(KeyCode));//存储所有的按键
        for (int x = 0; x < values.Length; x++)
        {
            if (Input.GetKeyDown((KeyCode)values.GetValue(x)))
            {
                currentButton = values.GetValue(x).ToString();//遍历并获取当前按下的按键
            }
        }
        if (currentButton == "Joystick2Button0")
        {
            if (IsCanControl()) _body.GetAtk();
            currentButton = "";
        }

        if (currentButton == "Joystick2Button1")
        {
            if (IsCanControl()) _body.GetJump();
            currentButton = "";
        }

        if (currentButton == "Joystick2Button2")
        {
            if (IsCanControl()) _body.GetDodge1();
            currentButton = "";
        }

        if (currentButton == "Joystick2Button3")
        {
            currentButton = "";
        }

    }


    void Update () {
       

        if (Input.anyKey)
        {//得到按下什么键
            //print("anyKey  " + Input.inputString);
        }

        
        if (Input.inputString == "Joystick2Button0") {
            //print("atk!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }

        ShouBing();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //print("jump");
            if (IsCanControl()) _body.GetJump();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            //|| Input.GetKeyUp("joystick2Button2")
            //values.GetValue(x).ToString()
            if (IsCanControl()) _body.GetAtk();
        }

       
        if (Input.GetKeyDown(KeyCode.J)) {
            if (IsCanControl()) _body.GetDodge1();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (IsCanControl()) _body.GetSkill1();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (IsCanControl()) _body.GetSkill2();
        }



        horizontalDirection = Input.GetAxis(HORIZONTAL);
        if (horizontalDirection > 0) {
            if (IsCanControl()) _body.RunRight(horizontalDirection);
        } else if(horizontalDirection<0) {
            if (IsCanControl()) _body.RunLeft(horizontalDirection);
        }
        else
        {
            if (IsCanControl()&&!Globals.isXNBtn) _body.ReSetLR();
        }
        
       
    }
}
