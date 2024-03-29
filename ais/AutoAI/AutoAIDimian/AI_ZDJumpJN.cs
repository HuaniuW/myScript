﻿using System;
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
                if (!IsAtking && IsACOver(SkillSFACName))
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

    [Header("地面释放技能动作")]
    public string SkillSFACName = "atk_1";
    [Header("空中***释放技能动作")]
    public string SkillSFACNameInAir = "atk_1";

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
            if (StartParticle&& ACName == SkillSFACName)
            {
                print("  ---*****----------- 播放起手特效！！！！ ");
                StartParticle.gameObject.SetActive(true);
                StartParticle.Play();
            }

        }
        return false;
    }




    protected override void ACSkillShowOut()
    {
       
        //base.ACSkillShowOut();
        if (SkillType == "1"|| SkillType == "x4rzb")
        {
            //4忍者镖
            Kongzhong4Renzhebiao();
        }else
        {
            KongzhongDiu3Huo();
        }
        
        _gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
        _gameBody.GetPlayerRigidbody2D().AddForce(new Vector2(0, 100));
        print("show skill out!!!!!!!!!!!!!!!!!");
       
    }

    private void Kongzhong4Renzhebiao()
    {
        //throw new NotImplementedException();
        ObjName = "AQ_renzhebiao";

        GetObjOut(2, -18,true);
        GetObjOut(12, -18,true);
        GetObjOut(22, -18,true);

    }

    string ObjName = "jn_dihuo_3";
    void KongzhongDiu3Huo()
    {
        ObjName = "jn_dihuo_3";
        GetObjOut(8, 8);
        GetObjOut(12, 8);
        GetObjOut(18, 8);
    }


    void GetObjOut(float __x,float __y,bool isRZB = false)
    {
        GameObject o = GlobalTools.GetGameObjectInObjPoolByName(ObjName);
        __x = this.transform.localScale.x > 0 ? -__x : __x;
        o.transform.position = zidanDian1.position;
        o.transform.parent = this.transform.parent;
        o.name = ObjName;
        if (isRZB)
        {
            o.GetComponent<TX_RenzheBiao>().SetV2Speed(new Vector2(__x, __y));
            o.GetComponent<TX_RenzheBiao>().GetSpeedV2();
        }
        else
        {
            o.GetComponent<Rigidbody2D>().velocity = new Vector2(__x, __y);
        }
        
    }



}
