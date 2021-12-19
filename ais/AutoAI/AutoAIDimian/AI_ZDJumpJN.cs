using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ZDJumpJN : AI_SkillBase
{
    // Start is called before the first frame update


    //ZD_ZDJumpJN
   
    protected override void TheStart()
    {
        this.ZSName = "ZDJumpJN";
        base.TheStart();
        this.IsSpeAISkill = true;
    }

    float _gravityScale = 4;

    protected override void GetTheStart()
    {
        base.GetTheStart();
        if (GetComponent<AIBase>())
        {
            GetComponent<AIBase>().AIBaseJump = false;
        }
        _gravityScale = _gameBody.GetPlayerRigidbody2D().gravityScale;
        _isGetStart = true;
    }

    public override void ReSetAll()
    {
        base.ReSetAll();
        if (GetComponent<AIBase>())
        {
            GetComponent<AIBase>().AIBaseJump = true;
        }
        IsJumping = false;
        _gameBody.isAcing = false;
        IsAtking = false;
    }


    float JumpPower = 1400;
    float JumpPowerX = 0;
    float DistanceInAirToPlayer = 6;
    [Header("跳跃动作 ")]
    public string JumpACName = "jumpUp_1";
    bool IsJumping = false;

    bool IsAtking = false;

    protected override void ChixuSkillStarting()
    {



        //print("--1");
        if (!_isGetStart) {

            if (_gameBody.isInAiring)
            {
                _gameBody.GetDB().animation.GotoAndPlayByFrame("jumpDown_1");
            }
            return;
        }
       
        //print("--2");
        if (!GetComponent<AIBase>().isActioning)
        {
            TheSkillOver();
            return;
        }
        //print("--3");

        if (_roleDate.isBeHiting || _roleDate.isDie)
        {
            

            TheSkillOver();
            //print("------> 进来没？？   "+ GetComponent<Rigidbody2D>().velocity);
            if (_roleDate.isDie) GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            return;
        }

        //print("--4");

        if (!IsJumping)
        {
            if (IsACOver(JumpACName))
            {
                IsJumping = true;
                _gameBody.SetV0();
                JumpPowerX = (Mathf.Abs(this.transform.position.x - _player.transform.position.x)- DistanceInAirToPlayer )* 100;
                if(this.transform.localScale.x > 0)
                {
                    JumpPowerX *= -1;
                }
                _gameBody.GetPlayerRigidbody2D().AddForce(new Vector2(JumpPowerX, JumpPower));
            }
            return;
        }
        //print("--5");
        if (_gameBody.isInAiring)
        {
            if (_gameBody.GetPlayerRigidbody2D().velocity.y < 0)
            {
                if (!IsAtking && IsACOver("atk_1"))
                {
                    IsAtking = true;
                }
            }
            return;
        }
        else
        {

            //print("isACing  " + _gameBody.isAcing + "   ----------   " + _gameBody.IsGround);
            if (IsAtking && (_gameBody.GetDB().animation.lastAnimationName == "downOnGround_1" || _gameBody.GetDB().animation.lastAnimationName == "stand_1"))
            {
                //print("着陆！！！");
                TheSkillOver();
            }else if (_gameBody.IsGround)
            {
                TheSkillOver();
            }

        }
        //print("--6");

    }



    bool IsACOver(string ACName)
    {
        if (_gameBody.GetDB().animation.lastAnimationName == ACName && _gameBody.GetDB().animation.isCompleted)
        {
            _gameBody.isAcing = false;
            print("  动作 完成！！！！！ "+ACName);
            return true;
        }


        if (!_gameBody.isAcing  &&_gameBody.GetDB().animation.lastAnimationName != ACName)
        {
            _gameBody.isAcing = true;
            _gameBody.GetDB().animation.GotoAndPlayByFrame(ACName, 0, 1);
            
        }
        return false;
    }




    protected override void ACSkillShowOut()
    {
        base.ACSkillShowOut();
        ShowOutSkill();
        _gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
        _gameBody.GetPlayerRigidbody2D().AddForce(new Vector2(0, 100));
        print("show skill out!!!!!!!!!!!!!!!!!");
       
    }


    string ObjName = "jn_dihuo_3";
    void ShowOutSkill()
    {
        GetObjOut(8, 8);
        GetObjOut(12, 8);
        GetObjOut(18, 8);
    }


    void GetObjOut(float __x,float __y)
    {
        GameObject o = GlobalTools.GetGameObjectInObjPoolByName(ObjName);
        __x = this.transform.localScale.x > 0 ? -__x : __x;
        __y = 8;
        o.transform.position = zidanDian1.position;
        o.transform.parent = this.transform.parent;
        o.GetComponent<Rigidbody2D>().velocity = new Vector2(__x, __y);
    }



}
