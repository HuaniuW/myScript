using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyController : MonoBehaviour {
    GameBody _body;

    [Header("水平方向")]
    public float horizontalDirection;

    [Header("垂直方向")]
    public float verticalDirection;

    //水平方向的按键响应 Input.GetAxis
    const string HORIZONTAL = "Horizontal";

    const string VERTICAL = "Vertical";
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
                print("手柄按键  "+ currentButton);
            }
        }


        horizontalDirection = Input.GetAxis(HORIZONTAL);

        verticalDirection = Input.GetAxis(VERTICAL);
        if (horizontalDirection > 0)
        {
            if (IsCanControl()) _body.RunRight(horizontalDirection);
        }
        else if (horizontalDirection < 0)
        {
            if (IsCanControl()) _body.RunLeft(horizontalDirection);
        }
        else
        {
            if (IsCanControl() && !Globals.isXNBtn) _body.ReSetLR();
        }

        print("verticalDirection    " + verticalDirection);

        if (verticalDirection > 0.6)
        {
            Globals.isKeyUp = true;
            Globals.isKeyDown = false;
        }
        else if (verticalDirection < -0.6)
        {
            Globals.isKeyUp = false;
            Globals.isKeyDown = true;
        }
        else
        {
            Globals.isKeyUp = false;
            Globals.isKeyDown = false;
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



    void NewKey()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Globals.isKeyUp = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Globals.isKeyDown = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Globals.isKeyUp = false;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            Globals.isKeyDown = false;
        }



        if (Input.GetKey(KeyCode.A))
        {
            if (IsCanControl()) _body.RunLeft(-1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (IsCanControl()) _body.RunRight(1f);
        }
        else
        {
            if (IsCanControl() && !Globals.isXNBtn) _body.ReSetLR();
        }

        /*if (Input.GetKeyUp(KeyCode.A))
        {
            if (IsCanControl() && !Globals.isXNBtn) _body.ReSetLR();
        }

        if (Input.GetKeyUp(KeyCode.D))
        {

            if (IsCanControl() && !Globals.isXNBtn) _body.ReSetLR();
        }*/

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (IsCanControl()) _body.GetAtk();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (IsCanControl()) _body.GetJump();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (IsCanControl()) _body.GetSkill1();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (IsCanControl()) _body.GetSkill2();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (IsCanControl()) _body.GetDodge1();
        }

    }


    void Update () {


        NewKey();

        //ShouBing();

        //OldKey();
    }


    void OldKey()
    {
        if (Input.anyKey)
        {//得到按下什么键
            //print("anyKey  " + Input.inputString);
        }


        if (Input.inputString == "Joystick2Button0")
        {
            //print("atk!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("new atk!");
            if (IsCanControl()) _body.GetAtk();
        }



        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //print("jump");
            if (IsCanControl()) _body.GetJump();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //|| Input.GetKeyUp("joystick2Button2")
            //values.GetValue(x).ToString()
            if (IsCanControl()) _body.GetAtk();
        }


        if (Input.GetKeyDown(KeyCode.J))
        {
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

        verticalDirection = Input.GetAxis(VERTICAL);
        if (horizontalDirection > 0)
        {
            if (IsCanControl()) _body.RunRight(horizontalDirection);
        }
        else if (horizontalDirection < 0)
        {
            if (IsCanControl()) _body.RunLeft(horizontalDirection);
        }
        else
        {
            if (IsCanControl() && !Globals.isXNBtn) _body.ReSetLR();
        }

        if (verticalDirection > 0.1)
        {
            Globals.isKeyUp = true;
            Globals.isKeyDown = false;
        }
        else if (horizontalDirection<-0.1)
        {
            Globals.isKeyUp = false;
            Globals.isKeyDown = true;
        }
        else
        {
            Globals.isKeyUp = false;
            Globals.isKeyDown = true;
        }
    }
}
