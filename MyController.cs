using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyController : MonoBehaviour {
    GameBody _body;
    JijiaGamebody _jijiaBody;

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
        _jijiaBody = GetComponent<JijiaGamebody>();
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

    public void detectPressedKeyOrButton()
    {
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                Debug.Log("KeyCode down: " + kcode);
        }

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
                //print("手柄按键  " + currentButton);
            }
        }
        //detectPressedKeyOrButton();

        //float hl = Input.GetAxis("Horizontal_L");
        //float vl = Input.GetAxis("Vertical_L");

        //print("  hl "+hl);
        //print("  vl " + vl);



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

        //print("verticalDirection    " + verticalDirection);

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



        //if (Input.GetKeyDown("Joystick1Button0"))
        //{
        //    if (Globals.IsHitPlotKuai) return;
        //    //print(">>>>>>>>>>>???????  JJJJJJJ  Globals.isKeyUp    "+ Globals.isKeyUp);
        //    if (IsCanControl()) _body.GetAtk();
        //}
        //if (Input.GetKeyDown("Joystick1Button1"))
        //{
        //    if (IsCanControl()) _body.GetJump();
        //}

        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    //if (IsCanControl()) _body.GetSkill1();
        //}

        //if (Input.GetKeyDown("Joystick1Button3"))
        //{
        //    if (IsCanControl()) _body.GetSkill2();
        //}

        //if (Input.GetKeyDown("Joystick1Button2"))
        //{
        //    if (IsCanControl()) _body.GetDodge1();
        //}




        //Debug.Log("手柄按钮控制！！");

        if (currentButton == "Joystick1Button0" || currentButton == "JoystickButton0")
        {
            //Debug.Log("Joystick1Button0 !!!!! ");
            if (Globals.IsHitPlotKuai) return;
            if (IsCanControl()) _body.GetAtk();
            currentButton = "";
        }

        if (currentButton == "Joystick1Button1")
        {
            if (IsCanControl()) _body.GetJump();
            currentButton = "";
        }

        if (currentButton == "Joystick1Button2")
        {
            if (IsCanControl()) _body.GetDodge1();
            currentButton = "";
        }

        if (currentButton == "Joystick1Button3")
        {
            currentButton = "";
        }




    }



    void NewKey()
    {

        //ShouBing();

        if (Input.GetKeyDown(KeyCode.U)&& Input.GetKey(KeyCode.D))
        {
            //if (IsCanControl()) _body.GetSkill1();
            //print("技能释放---------   right ");

            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "center"), this);
            return;
        }
        if (Input.GetKeyDown(KeyCode.U) && Input.GetKey(KeyCode.W))
        {
            //if (IsCanControl()) _body.GetSkill1();
            //print("技能释放---------  up");
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "up"), this);
            return;
        }

        if (Input.GetKeyDown(KeyCode.U) && Input.GetKey(KeyCode.S))
        {
            //if (IsCanControl()) _body.GetSkill1();
            //print("技能释放---------  down");
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "down"), this);
            return;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            //if (IsCanControl()) _body.GetSkill1();
            //print("技能释放---------   center");
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "center"), this);
            return;
        }


      



        if (Input.GetKeyDown(KeyCode.I) && Input.GetKey(KeyCode.W))
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.DIU_FEIDAO, "up"), this);
            return;
        }

        if (Input.GetKeyDown(KeyCode.I) && Input.GetKey(KeyCode.S))
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.DIU_FEIDAO, "down"), this);
            return;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            //print("i");
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.DIU_FEIDAO, ""), this);
            return;
        }





        if (Input.GetKeyDown(KeyCode.W))
        {
            //print("   W----upupupupupup!!! ");
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

        if (Input.GetKeyUp(KeyCode.T))
        {
            _body.GetSit2();
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

        if (Input.GetKeyDown(KeyCode.H))
        {
            //if (IsCanControl()) _body.GetSkill1();
            //print("技能释放---------   center");
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "center"), this);

            if (IsCanControl()) _body.Gedang();
            return;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (Globals.IsHitPlotKuai) return;
            //print(">>>>>>>>>>>???????  JJJJJJJ  Globals.isKeyUp    "+ Globals.isKeyUp);
            if (IsCanControl()) _body.GetAtk();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (IsCanControl()) _body.GetJump();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            //if (IsCanControl()) _body.GetSkill1();
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

    public bool IsCanContolJijis = true;

    void Update () {

        //ShouBing();
        if (_body.IsInJijia)
        {
            //print("  在机甲里面？？？？？？？？？？？？？？ ");
            //驾驶机甲
            JijiaControl();
        }
        else
        {
            if (!IsCanContolJijis) return;
            NewKey();
        }
       



        //OldKey();
    }



    bool IsNengliangPao = false;
    bool IsJiali = false;
    void JijiaControl()
    {


        if (Input.GetKeyDown(KeyCode.U))
        {
            if (IsCanControl()) {
                IsNengliangPao = true;
                _jijiaBody.ShowNengliangPao();
            }
            
        }

        if (Input.GetKeyUp(KeyCode.U))
        {
            if (IsCanControl()) {
                IsNengliangPao = false;
                _jijiaBody.ShowNengliangPao(false);
            } 
        }




        if (Input.GetKey(KeyCode.L)&&!IsNengliangPao)
        {
            if (Input.GetKey(KeyCode.J))
            {
                if (IsCanControl()) _jijiaBody.Jipao();
            }
            else
            {
                if (IsCanControl()) _jijiaBody.Jipao(false);
            }

            if (Input.GetKey(KeyCode.W))
            {
                if (IsCanControl()) _jijiaBody.FlyUp();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (IsCanControl()) _jijiaBody.FlyDown();
            }
            else
            {
                if (IsCanControl()) _jijiaBody.FlyUp(false);
                if (IsCanControl()) _jijiaBody.FlyDown(false);
            }


            if (Input.GetKey(KeyCode.I))
            {
                if (IsCanControl()) _jijiaBody.Ganraodan();
            }
            else
            {
                if (IsCanControl()) _jijiaBody.Ganraodan(false);
            }

          

            if (Input.GetKeyDown(KeyCode.K))
            {
                if (IsCanControl()) _jijiaBody.GZDaodan();
            }

            if (Input.GetKeyUp(KeyCode.K))
            {
                if (IsCanControl()) _jijiaBody.GZDaodan(false);
            }




            IsJiali = true;
            if (IsCanControl()) _jijiaBody.GaosuFly();
            return;
        }
        else
        {
            IsJiali = false;
            if (IsCanControl()) _jijiaBody.GaosuFly(false);
        }

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    if (IsCanControl()) _jijiaBody.Jipao();
        //}

        //if (Input.GetKeyUp(KeyCode.J))
        //{
        //    if (IsCanControl()) _jijiaBody.Jipao(false);
        //}

        if (Input.GetKey(KeyCode.J))
        {
            if (IsCanControl()) _jijiaBody.Jipao();
        }
        else
        {
            if (IsCanControl()) _jijiaBody.Jipao(false);
        }


        //if (Input.GetKeyUp(KeyCode.W))
        //{
        //    Debug.Log("您抬起了W键");
        //    if (IsCanControl()) _jijiaBody.FlyUp(false);
        //}

        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    Debug.Log("您抬起了S键");
        //    if (IsCanControl()) _jijiaBody.FlyDown(false);
        //}

        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    Debug.Log("您抬起了A键");
        //    if (IsCanControl()) _jijiaBody.FlyHou(false);
        //}

        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    Debug.Log("您抬起了D键");
        //    if (IsCanControl()) _jijiaBody.FlyQian(false);
        //}


        if (Input.GetKey(KeyCode.W))
        {
            if (IsCanControl()) _jijiaBody.FlyUp();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (IsCanControl()) _jijiaBody.FlyDown();
        }
        else
        {
            if (IsCanControl()) _jijiaBody.FlyUp(false);
            if (IsCanControl()) _jijiaBody.FlyDown(false);
        }


        if (Input.GetKey(KeyCode.A))
        {
            if (!IsJiali)
            {
                if (IsCanControl() || !Input.GetKeyDown(KeyCode.J)) _jijiaBody.FlyHou();
            }
           
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (IsCanControl()) _jijiaBody.FlyQian();
        }
        else
        {
            if (IsCanControl()) _jijiaBody.FlyHou(false);
            if (IsCanControl()) _jijiaBody.FlyQian(false);
        }


     
             
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (IsCanControl()) _jijiaBody.Ganraodan();
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            if (IsCanControl()) _jijiaBody.Ganraodan(false);
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            if (IsCanControl()) _jijiaBody.GZDaodan();
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            if (IsCanControl()) _jijiaBody.GZDaodan(false);
        }

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
