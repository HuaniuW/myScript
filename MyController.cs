using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MyController : MonoBehaviour
{
    GameBody _body;
    JijiaGamebody _jijiaBody;

    [Header("水平方向")]
    public float horizontalDirection;

    [Header("垂直方向")]
    public float verticalDirection;

    //水平方向的按键响应 Input.GetAxis
    const string HORIZONTAL = "Horizontal";

    const string VERTICAL = "Vertical";


    //const string ATK = "Joystick1Button0";

    //const string JUMP = "Joystick1Button1";

    //const string SHANJIN = "Joystick1Button2";

    //const string SKILL = "Joystick1Button3";




    // Use this for initialization
    void Start()
    {
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
        if (!Globals.IsCanControl) return false;
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

    bool IsInShoubing = false;

    private string currentButton;
    void ShouBing()
    {
        //var values = Enum.GetValues(typeof(KeyCode));//存储所有的按键
        //for (int x = 0; x < values.Length; x++)
        //{
        //    if (Input.GetKeyDown((KeyCode)values.GetValue(x)))
        //    {
        //        currentButton = values.GetValue(x).ToString();//遍历并获取当前按下的按键
        //        print("手柄按键  " + currentButton);
        //    }
        //}
        //detectPressedKeyOrButton();

        //float hl = Input.GetAxis("Horizontal_L");
        //float vl = Input.GetAxis("Vertical_L");

        //print("  hl "+hl);
        //print("  vl " + vl);



        horizontalDirection = Input.GetAxis(HORIZONTAL);

        verticalDirection = Input.GetAxis(VERTICAL);

        if (horizontalDirection > 0.8 || horizontalDirection < -0.8) IsInShoubing = true;

        if (verticalDirection > 0.6)
        {
            IsInShoubing = true;
            Globals.isKeyUp = true;
            Globals.isKeyDown = false;
        }
        else if (verticalDirection < -0.6)
        {
            IsInShoubing = true;
            Globals.isKeyUp = false;
            Globals.isKeyDown = true;
        }
        else
        {
            if (IsInShoubing)
            {
                Globals.isKeyUp = false;
                Globals.isKeyDown = false;
            }

        }



        //if (Input.GetKeyDown(KeyCode.Joystick1Button3) && Globals.isKeyUp)
        //{
        //    //if (IsCanControl()) _body.GetSkill1();
        //    //print("技能释放---------   right ");

        //    ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "center"), this);
        //    return;
        //}
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) && Globals.isKeyUp)
        {
            //if (IsCanControl()) _body.GetSkill1();
            //print("技能释放---------  up");
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "up"), this);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button3) && Globals.isKeyDown)
        {
            //if (IsCanControl()) _body.GetSkill1();
            //print("技能释放---------  down");
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "down"), this);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            //if (IsCanControl()) _body.GetSkill1();
            //print("技能释放---------   center");
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "center"), this);
            return;
        }



        if (IsInShoubing)
        {
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
        }

        //print("verticalDirection    " + verticalDirection);


        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            //if (IsCanControl()) _body.GetSkill1();
            //print("技能释放---------   center");
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "center"), this);

            if (IsCanControl()) _body.Gedang();
            return;
        }


        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if (Globals.IsHitPlotKuai) return;
            //print(">>>>>>>>>>>???????  JJJJJJJ  Globals.isKeyUp    "+ Globals.isKeyUp);
            if (IsCanControl()) _body.GetAtk();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            if (IsCanControl()) _body.GetJump();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (IsCanControl()) _body.GetDodge1();
        }

    }



    float _LT = 0;
    float _RT = 0;

    bool IsShanjin = false;

    void ShouBing2()
    {

        _LT = Input.GetAxis("LT");
        //_RT = Input.GetAxis("RT");



        //print("  _LT  "+ _LT);
        //print("  _RT*****  " + _RT);

        /*   if (_LT > 0.6)
           {
               IsInShoubing = true;
               print("  LT>0.6 ");
           }*/
        if (_RT < -0.6)
        {
            IsInShoubing = true;
            //print("  LT<0.6 ");
        }

        if (_LT >= -0.6f && _LT <= 0.6f)
        {
            if (IsInShoubing)
            {
                IsShanjin = false;
                //print("  LT------------------------------up ");
            }
        }




        horizontalDirection = Input.GetAxis(HORIZONTAL);

        verticalDirection = Input.GetAxis(VERTICAL);

        if (horizontalDirection > 0.8 || horizontalDirection < -0.8) IsInShoubing = true;

        if (verticalDirection > 0.6)
        {
            IsInShoubing = true;
            Globals.isKeyUp = true;
            Globals.isKeyDown = false;
        }
        else if (verticalDirection < -0.6)
        {
            IsInShoubing = true;
            Globals.isKeyUp = false;
            Globals.isKeyDown = true;
        }
        else
        {
            if (IsInShoubing)
            {
                Globals.isKeyUp = false;
                Globals.isKeyDown = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.Joystick1Button3) && Globals.isKeyUp)
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "up"), this);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button3) && Globals.isKeyDown)
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "down"), this);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "center"), this);
            return;
        }



        if (IsInShoubing)
        {
            if (horizontalDirection >=0.1f)
            {
                //-1
                //if (IsCanControl()) _body.RunRight(horizontalDirection);
                if (IsCanControl()) _body.RunRight(1);
            }
            else if (horizontalDirection <= -0.1f)
            {
                //if (IsCanControl()) _body.RunLeft(horizontalDirection);
                if (IsCanControl()) _body.RunLeft(-1);
            }
            else
            {
                if (IsCanControl() && !Globals.isXNBtn) _body.ReSetLR();
            }
        }

        //print("verticalDirection    " + verticalDirection);


        if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            //if (IsCanControl()) _body.GetSkill1();
            //print("技能释放---------   center");
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RELEASE_SKILL, "center"), this);

            if (IsCanControl()) _body.Gedang();
            return;
        }


        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            if (Globals.IsHitPlotKuai) return;
            //print(">>>>>>>>>>>???????  JJJJJJJ  Globals.isKeyUp    "+ Globals.isKeyUp);
            if (IsCanControl()) _body.GetAtk();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if (IsCanControl()) _body.GetJump();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button4) || (_LT < -0.6 && !IsShanjin))
        {
            IsShanjin = true;
            if (IsCanControl()) _body.GetDodge1();
        }

    }



    void NewKey()
    {

        //ShouBing();

        if (Input.GetKeyDown(KeyCode.U) && Input.GetKey(KeyCode.D))
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
            IsInShoubing = false;
            Globals.isKeyUp = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            IsInShoubing = false;
            Globals.isKeyDown = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Globals.isKeyUp = false;
            IsInShoubing = false;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            Globals.isKeyDown = false;
            IsInShoubing = false;
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            //测试用
            _body.GetSit2();
        }

        if (Input.GetKey(KeyCode.A))
        {
            IsInShoubing = false;
            if (IsCanControl()) _body.RunLeft(-1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            IsInShoubing = false;
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

    void Update()
    {

        //ShouBing();
        if (_body.IsInJijia)
        {
            //print("  在机甲里面？？？？？？？？？？？？？？ ");
            //驾驶机甲
            JijiaControl();
            //JijiaControlShoubing();
            JijiaControlShoubing2();
            DebugControl();
        }
        else
        {

            if (!IsCanContolJijis) return;
            NewKey();
            //ShouBing();
            ShouBing2();
            //呼出菜单 和 背包
            //GameControl();
            GameControl2();

            DebugControl();
        }






        //OldKey();
    }


    void GameControl()
    {
        //if (!IsCanControl()) return;

        //if (!Globals.IsCanControl) return;
        //if (Globals.isInPlot) return;
        //if (!_isCanControl) return false;
        //if (GlobalSetDate.instance.IsChangeScreening) return false;
        if (GetComponent<RoleDate>().isDie) return;


        //背包
        if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.Joystick1Button8))
        {
            //设置
            print(" 打开设置 ");
            if (GlobalTools.FindObjByName(GlobalTag.PLAYERUI))
            {
                GlobalTools.FindObjByName(GlobalTag.PLAYERUI).GetComponent<PlayerUI>().GetSetUI();
            }
        }
    }


    void GameControl2()
    {
        //if (!IsCanControl()) return;

        if (!Globals.IsCanControl) return;
        if (Globals.isInPlot) return;
        //if (!_isCanControl) return false;
        //if (GlobalSetDate.instance.IsChangeScreening) return false;
        if (GetComponent<RoleDate>().isDie) return;


        //背包
        if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            //设置
            print(" 打开设置 ");
            if (GlobalTools.FindObjByName(GlobalTag.PLAYERUI))
            {
                GlobalTools.FindObjByName(GlobalTag.PLAYERUI).GetComponent<PlayerUI>().GetSetUI();
            }
        }
    }



    bool IsNengliangPao = false;
    bool IsJiali = false;
    void JijiaControl()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (IsCanControl())
            {
                IsNengliangPao = true;
                _jijiaBody.ShowNengliangPao();
            }

        }

        if (Input.GetKeyUp(KeyCode.U))
        {
            if (IsCanControl())
            {
                IsNengliangPao = false;
                _jijiaBody.ShowNengliangPao(false);
            }
        }




        if (Input.GetKey(KeyCode.L) && !IsNengliangPao)
        {
            IsInShoubing = false;
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
                IsInShoubing = false;
                if (IsCanControl()) _jijiaBody.FlyUp();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                IsInShoubing = false;
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
            if (!IsInShoubing)
            {
                IsJiali = false;
                if (IsCanControl()) _jijiaBody.GaosuFly(false);
            }

        }

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


        if (Input.GetKey(KeyCode.A))
        {
            if (!IsJiali)
            {
                IsInShoubing = false;
                if (IsCanControl() || !Input.GetKeyDown(KeyCode.J)) _jijiaBody.FlyHou();
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            IsInShoubing = false;
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

    //手柄控制机甲
    void JijiaControlShoubing()
    {

        horizontalDirection = Input.GetAxis(HORIZONTAL);

        verticalDirection = Input.GetAxis(VERTICAL);

        if (horizontalDirection > 0.8 || horizontalDirection < -0.8) IsInShoubing = true;

        if (verticalDirection > 0.6)
        {
            IsInShoubing = true;
            Globals.isKeyUp = true;
            Globals.isKeyDown = false;
        }
        else if (verticalDirection < -0.6)
        {
            IsInShoubing = true;
            Globals.isKeyUp = false;
            Globals.isKeyDown = true;
        }
        else
        {
            if (IsInShoubing)
            {
                Globals.isKeyUp = false;
                Globals.isKeyDown = false;
            }

        }




        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            if (IsCanControl())
            {
                IsNengliangPao = true;
                _jijiaBody.ShowNengliangPao();
            }

        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            if (IsCanControl())
            {
                IsNengliangPao = false;
                _jijiaBody.ShowNengliangPao(false);
            }
        }


        //L是加速

        if (Input.GetKey(KeyCode.Joystick1Button6) && !IsNengliangPao)
        {
            IsInShoubing = true;
            if (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.Joystick1Button7))
            {
                if (IsCanControl()) _jijiaBody.Jipao();
            }
            else
            {
                if (IsCanControl()) _jijiaBody.Jipao(false);
            }

            if (Globals.isKeyUp)
            {
                if (IsCanControl()) _jijiaBody.FlyUp();
            }
            else if (Globals.isKeyDown)
            {
                if (IsCanControl()) _jijiaBody.FlyDown();
            }
            else
            {
                if (IsCanControl()) _jijiaBody.FlyUp(false);
                if (IsCanControl()) _jijiaBody.FlyDown(false);
            }


            if (Input.GetKey(KeyCode.Joystick1Button3))
            {
                if (IsCanControl()) _jijiaBody.Ganraodan();
            }
            else
            {
                if (IsCanControl()) _jijiaBody.Ganraodan(false);
            }



            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                if (IsCanControl()) _jijiaBody.GZDaodan();
            }

            if (Input.GetKeyUp(KeyCode.Joystick1Button1))
            {
                if (IsCanControl()) _jijiaBody.GZDaodan(false);
            }

            IsJiali = true;
            if (IsCanControl()) _jijiaBody.GaosuFly();
            return;
        }
        else
        {
            if (IsInShoubing)
            {
                IsJiali = false;
                if (IsCanControl()) _jijiaBody.GaosuFly(false);
            }
        }

        if (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.Joystick1Button7))
        {
            if (IsCanControl()) _jijiaBody.Jipao();
        }
        else
        {
            if (IsInShoubing)
            {
                if (IsCanControl()) _jijiaBody.Jipao(false);
            }

        }


        if (Globals.isKeyUp)
        {
            if (IsCanControl()) _jijiaBody.FlyUp();
        }
        else if (Globals.isKeyDown)
        {
            if (IsCanControl()) _jijiaBody.FlyDown();
        }
        else
        {
            if (IsInShoubing)
            {
                if (IsCanControl()) _jijiaBody.FlyUp(false);
                if (IsCanControl()) _jijiaBody.FlyDown(false);
            }

        }


        if (horizontalDirection < -0.6)
        {
            if (!IsJiali)
            {
                if (IsCanControl() || !Input.GetKeyDown(KeyCode.J)) _jijiaBody.FlyHou();
            }

        }
        else if (horizontalDirection > 0.6)
        {
            if (IsCanControl()) _jijiaBody.FlyQian();
        }
        else
        {
            if (IsInShoubing)
            {
                if (IsCanControl()) _jijiaBody.FlyHou(false);
                if (IsCanControl()) _jijiaBody.FlyQian(false);
            }

        }




        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            if (IsCanControl()) _jijiaBody.Ganraodan();
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button3))
        {
            if (IsCanControl()) _jijiaBody.Ganraodan(false);
        }


        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            if (IsCanControl()) _jijiaBody.GZDaodan();
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button1))
        {
            if (IsCanControl()) _jijiaBody.GZDaodan(false);
        }

    }



    void JijiaControlShoubing2()
    {


        _LT = Input.GetAxis("LT");
        //_RT = Input.GetAxis("RT");



        print("  _LT  " + _LT);
        //print("  _RT*****  " + _RT);

        /*   if (_LT > 0.6)
           {
               IsInShoubing = true;
               print("  LT>0.6 ");
           }*/
        if (_RT < -0.6)
        {
            IsInShoubing = true;
            print("  LT<0.6 ");
        }

        if (_LT >= -0.6f && _LT <= 0.6f)
        {
            if (IsInShoubing)
            {
                IsShanjin = false;
                //print("  LT------------------------------up ");
            }

        }


        horizontalDirection = Input.GetAxis(HORIZONTAL);

        verticalDirection = Input.GetAxis(VERTICAL);

        if (horizontalDirection > 0.8 || horizontalDirection < -0.8) IsInShoubing = true;

        if (verticalDirection > 0.6)
        {
            IsInShoubing = true;
            Globals.isKeyUp = true;
            Globals.isKeyDown = false;
        }
        else if (verticalDirection < -0.6)
        {
            IsInShoubing = true;
            Globals.isKeyUp = false;
            Globals.isKeyDown = true;
        }
        else
        {
            if (IsInShoubing)
            {
                Globals.isKeyUp = false;
                Globals.isKeyDown = false;
            }

        }




        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            if (IsCanControl())
            {
                IsNengliangPao = true;
                _jijiaBody.ShowNengliangPao();
            }

        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            if (IsCanControl())
            {
                IsNengliangPao = false;
                _jijiaBody.ShowNengliangPao(false);
            }
        }


        //L是加速

        if (_LT < -0.6f && !IsNengliangPao)
        {
            IsInShoubing = true;
            if (Input.GetKey(KeyCode.Joystick1Button2))
            {
                if (IsCanControl()) _jijiaBody.Jipao();
            }
            else
            {
                if (IsCanControl()) _jijiaBody.Jipao(false);
            }

            if (Globals.isKeyUp)
            {
                if (IsCanControl()) _jijiaBody.FlyUp();
            }
            else if (Globals.isKeyDown)
            {
                if (IsCanControl()) _jijiaBody.FlyDown();
            }
            else
            {
                if (IsCanControl()) _jijiaBody.FlyUp(false);
                if (IsCanControl()) _jijiaBody.FlyDown(false);
            }


            if (Input.GetKey(KeyCode.Joystick1Button0))
            {
                if (IsCanControl()) _jijiaBody.Ganraodan();
            }
            else
            {
                if (IsCanControl()) _jijiaBody.Ganraodan(false);
            }



            if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                if (IsCanControl()) _jijiaBody.GZDaodan();
            }

            if (Input.GetKeyUp(KeyCode.Joystick1Button3))
            {
                if (IsCanControl()) _jijiaBody.GZDaodan(false);
            }

            IsJiali = true;
            if (IsCanControl()) _jijiaBody.GaosuFly();
            return;
        }
        else
        {
            if (IsInShoubing)
            {
                IsJiali = false;
                if (IsCanControl()) _jijiaBody.GaosuFly(false);
            }
        }

        if (Input.GetKey(KeyCode.Joystick1Button2))
        {
            if (IsCanControl()) _jijiaBody.Jipao();
        }
        else
        {
            if (IsInShoubing)
            {
                if (IsCanControl()) _jijiaBody.Jipao(false);
            }

        }


        if (Globals.isKeyUp)
        {
            if (IsCanControl()) _jijiaBody.FlyUp();
        }
        else if (Globals.isKeyDown)
        {
            if (IsCanControl()) _jijiaBody.FlyDown();
        }
        else
        {
            if (IsInShoubing)
            {
                if (IsCanControl()) _jijiaBody.FlyUp(false);
                if (IsCanControl()) _jijiaBody.FlyDown(false);
            }

        }


        if (horizontalDirection < -0.6)
        {
            if (!IsJiali)
            {
                if (IsCanControl() || !Input.GetKeyDown(KeyCode.J)) _jijiaBody.FlyHou();
            }

        }
        else if (horizontalDirection > 0.6)
        {
            if (IsCanControl()) _jijiaBody.FlyQian();
        }
        else
        {
            if (IsInShoubing)
            {
                if (IsCanControl()) _jijiaBody.FlyHou(false);
                if (IsCanControl()) _jijiaBody.FlyQian(false);
            }

        }




        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if (IsCanControl()) _jijiaBody.Ganraodan();
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button0))
        {
            if (IsCanControl()) _jijiaBody.Ganraodan(false);
        }


        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            if (IsCanControl()) _jijiaBody.GZDaodan();
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button3))
        {
            if (IsCanControl()) _jijiaBody.GZDaodan(false);
        }

    }




    void DebugControl()
    {
        if (Globals.isDebug2)
        {
            if (Input.GetKeyUp(KeyCode.Y))
            {
                //隐藏 和 显示UI
                GameObject ui = GlobalTools.FindObjByName(GlobalTag.PLAYERUI);
                if (ui && ui.activeSelf)
                {
                    ui.SetActive(false);
                }
                //else
                //{
                //    ui.SetActive(true);
                //}
            }

            if (Input.GetKeyUp(KeyCode.T))
            {
                //隐藏 和 显示UI
                GameObject player = GlobalTools.FindObjByName(GlobalTag.PlayerObj);
                if (player && player.activeSelf)
                {
                    player.SetActive(false);
                }
                //else
                //{
                //    ui.SetActive(true);
                //}
            }
        }




        if (!Globals.isDebug) return;

        if (Input.GetKey(KeyCode.Alpha1))
        {
            GlobalSetDate.instance.GetInMenWeizhi("men_n_1");
            GlobalSetDate.instance.screenName = "map_o-1";
            GlobalSetDate.instance.doorName = "men_n_1";
            ChangeScreen();
           
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            GlobalSetDate.instance.GetInMenWeizhi("men_n_1");
            GlobalSetDate.instance.screenName = "ng2_2-1";
            GlobalSetDate.instance.doorName = "men_n_1";
            ChangeScreen();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            GlobalSetDate.instance.GetInMenWeizhi("men_n_1");
            GlobalSetDate.instance.screenName = "ng3_0c";
            GlobalSetDate.instance.doorName = "men_n_1";
            ChangeScreen();
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            GlobalSetDate.instance.GetInMenWeizhi("men_n_1");
            GlobalSetDate.instance.screenName = "ng4_1";
            GlobalSetDate.instance.doorName = "men_n_1";
            ChangeScreen();
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            GlobalSetDate.instance.GetInMenWeizhi("men_n_1");
            GlobalSetDate.instance.screenName = "ng5_1";
            GlobalSetDate.instance.doorName = "men_n_1";
            ChangeScreen();
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            GlobalSetDate.instance.GetInMenWeizhi("men_n_1");
            GlobalSetDate.instance.screenName = "ng6_1";
            GlobalSetDate.instance.doorName = "men_n_1";
            ChangeScreen();
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            GlobalSetDate.instance.GetInMenWeizhi("men_n_1");
            GlobalSetDate.instance.screenName = "ng7_3";
            GlobalSetDate.instance.doorName = "men_n_1";
            ChangeScreen();
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            GlobalSetDate.instance.GetInMenWeizhi("men_n_1");
            GlobalSetDate.instance.screenName = "ng8_1a";
            GlobalSetDate.instance.doorName = "men_n_1";
            ChangeScreen();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            GameObject o = GlobalTools.GetGameObjectByName("WP_huizhang10");
            GameObject _p = GlobalTools.FindObjByName(GlobalTag.PlayerObj);
            Vector2 v2 = new Vector2(_p.transform.position.x+4,_p.transform.position.y+2);
            o.transform.position = v2;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            GlobalSetDate.instance.GetInMenWeizhi("men_n_1");
            String _gkName = FileControl.GetInstance().GetValueByKey("GoScreen");
            if (_gkName == ""|| _gkName == "0") return;
            GlobalSetDate.instance.screenName = _gkName;
            GlobalSetDate.instance.doorName = "men_n_1";
            ChangeScreen();
        }





    }


    GameObject playerUI;
    void ChangeScreen()
    {
        print(" 场景 变化   screenChange ");
        playerUI = GlobalTools.FindObjByName("PlayerUI");
        if (playerUI) playerUI.GetComponent<PlayerUI>().skill_bar.GetComponent<UI_ShowPanel>().SaveAllHZDate();

        //用来判断 地图地形 是否生成 过 如果生成过 就不用再生成了 直接根据数据生成就可以（控制 地图块内的 自动生成 粒子 草之类的）
        GlobalSetDate.instance.IsCMapHasCreated = false;

        //获取角色当前数据 当前血量 当前蓝量  发给GlobalSetDate  什么格式 以后再说  cLive=1000,cLan=1000
        GlobalSetDate.instance.ScreenChangeDateRecord();
        GlobalSetDate.instance.HowToInGame = GlobalSetDate.CHANGE_SCREEN;
        if (GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1") != null)
        {
            //GlobalSetDate.instance.bagDate = GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1").GetComponent<Mianban1>().HZSaveDate();
            //GlobalSetDate.instance.CurrentMapMsgDate.bagDate = GlobalSetDate.instance.CurrentMapMsgDate.bagDate = GlobalSetDate.instance.bagDate;
            GlobalSetDate.instance.CurrentMapMsgDate.bagDate = GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1").GetComponent<Mianban1>().HZSaveDate();
            print("切换到新 地图时  总地图数据  " + GlobalSetDate.instance.CurrentMapMsgDate.mapDate);
            print("GlobalSetDate.instance.CurrentMapMsgDate.bagDate： " + GlobalSetDate.instance.CurrentMapMsgDate.bagDate);
            GlobalDateControl.SaveMapDate();
        }


        //通知储存关卡变化的数据
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_SCREEN, null), this);
        //ObjectPools.GetInstance().delAll();//解决切换场景时候特效没回收被销毁导致再取取不出来的问题  但是这样销毁会导致卡顿很长的切换速度 已用重新创建解决
        SetPlayerPositionAndScreen();
        //SceneManager.LoadScene("loads");
        //print("***  GlobalSetDate.instance.screenName   "+ GlobalSetDate.instance.screenName);
        //StartCoroutine(IEStartLoading(GlobalSetDate.instance.screenName));
        //StartCoroutine(IEStartLoading(GoScreenName));
        //if (IsYuJiazai)
        //{
        //    op.allowSceneActivation = true;
        //}
        //else
        //{

        //}

        StartCoroutine(IEStartLoading(GlobalSetDate.instance.screenName));

        //启动遮罩
        playerUI.GetComponent<PlayerUI>().GetScreenZZChange(1);

        //GlobalSetDate.instance.Show_UIZZ();
    }

    void SetPlayerPositionAndScreen()
    {
        GlobalSetDate.instance.IsChangeScreening = true;
        //GlobalSetDate.instance.doorName = doorName;
    }

    AsyncOperation op;
    private IEnumerator IEStartLoading(string scene)
    {
        int displayProgress = 0;
        int toProgress = 0;
        //if (Globals.isDebug) print("*** screen>  " + scene);
        //op = null;

        //Scene scene = SceneManager.GetSceneByName(sceneName);
        //SceneManager.UnloadSceneAsync(0);
        op = SceneManager.LoadSceneAsync(scene);
        //是否允许自动跳场景 （如果设为false 只会加载到90 不会继续加载）
        op.allowSceneActivation = false;
        //print("hi!!!");
        while (op.progress < 0.9f)
        {
            //print("in");
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                //SetLoadingPercentage(displayProgress);
                //loadingBar.rectTransform.se
                playerUI.GetComponent<PlayerUI>().ShowLoadProgressNums(displayProgress);
                //GlobalSetDate.instance.ShowLoadProgressNums(displayProgress);
                //print("screenChange progress: "+ displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            //SetLoadingPercentage(displayProgress);
            playerUI.GetComponent<PlayerUI>().ShowLoadProgressNums(displayProgress);
            //print("screenChange progress: " + displayProgress);
            yield return new WaitForEndOfFrame();
        }
        //设置为true 后 加载到100后直接自动跳转
        op.allowSceneActivation = true;

    }





}
