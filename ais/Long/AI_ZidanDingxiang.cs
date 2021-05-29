using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class AI_ZidanDingxiang : AI_ZiDans
{
    //子弹 定向 -向固定方向 发射
    protected override void StartMove()
    {
        AddEvents();
    }

    void AddEvents()
    {
        _gameBody.GetDB().AddDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
        _gameBody.GetDB().AddDBEventListener(DragonBones.EventObject.SOUND_EVENT, this.ShowACTX);
    }

    private void OnDisable()
    {
        RemoveEvent();
    }

    void RemoveEvent()
    {
        _gameBody.GetDB().RemoveDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
        _gameBody.GetDB().RemoveDBEventListener(DragonBones.EventObject.SOUND_EVENT, this.ShowACTX);
    }

    public override void GetStart(GameObject gameObj)
    {
        //print(" 子弹s "+ZiDanTypeNum);
        _player = gameObj;
        _isGetOver = false;
        _isQiShiAC = true;
        _gameBody.IsSFSkill = true;
        IsInStanding = false;
        GetInStand = false;
        GetStartAC();
    }



    string StartACName = "longdan_1";

    void GetStartAC()
    {
        if (this.transform.position.x > _player.transform.position.x)
        {
            _gameBody.TurnLeft();
        }
        else
        {
            _gameBody.TurnRight();
        }


        _gameBody.GetAcMsg(StartACName,2);
        if(GlobalTools.GetRandomNum()>=30)QSAudio.Play();
        _gameBody.StopVSpeed();
    }



    bool GetInStand = false;
    bool IsInStanding = false;
    bool IsOverInStand()
    {
        if (!IsInStanding)
        {
            IsInStanding = true;
            //_gameBody.GetDB().animation.FadeIn("stand_1",0.2f,1);
            _gameBody.GetAcMsg("stand_1", 2);
        }
        if (_gameBody.GetDB().animation.lastAnimationName == "stand_1") return true;
        return false;
    }

    protected override void ResetAllMore()
    {
        //IsInStanding = false;
        _gameBody.IsSFSkill = false;
        GetInStand = false;
    }


    protected override void Update()
    {
        if (_roleDate.isDie || _roleDate.isBeHiting)
        {
            ReSetAll();
            _gameBody.IsSFSkill = false;
            _isGetOver = true;
            return;
        }


        if (GetInStand)
        {
            if (IsOverInStand())
            {
                //print(" inStand!!!!!   ");
                ReSetAll();
                _gameBody.IsSFSkill = false;
                _isGetOver = true;
            }
            return;
        }


        if (!_gameBody.isAcing)
        {
            GetInStand = true;
            //print("  ac Over gotoIn stand!!!!   ");
            return;
        }

    }


    //protected override void QiShiAC()
    //{


    //    if (QSAudio) QSAudio.Play();

    //    _qishiACJS += Time.deltaTime;

    //    if (TX_Qishi && TX_Qishi.isStopped)
    //    {
    //        TX_Qishi.Play();
    //    }


    //    if (_qishiACJS >= QiShiACYCTimes)
    //    {
    //        ReSetAll();
    //        _qishiACJS = 0;
    //        Fire();
    //    }
    //}



    protected override Vector2 ZidanFX()
    {
        Vector2 v2 = _player.transform.position - ZiDanPos.position;
        Vector3 targetPot = Vector2.zero;
        if (this.transform.localScale.x == 1)
        {
            //朝左 放子弹
            targetPot = new Vector2(ZiDanPos.position.x - (3+GlobalTools.GetRandomDistanceNums(1)), ZiDanPos.position.y - 3);
        }
        else
        {
            targetPot = new Vector2(ZiDanPos.position.x + (3 + GlobalTools.GetRandomDistanceNums(1)), ZiDanPos.position.y - 3);
        }

        v2 = targetPot - ZiDanPos.position;
        return v2;
    }


    protected virtual void ShowACTX(string type, EventObject eventObject)
    {

        //print("type:  "+type);
        //print("eventObject  ????  " + eventObject);
        //print("__________________________________________骨骼动画事件_________________________________________________________________________________name    "+ eventObject.name+"    name  "+ _gameBody.GetDB().animation.lastAnimationName);

        if (_gameBody.GetDB().animation.lastAnimationName == "shuaiwei_1")
        {
            return;
        }

        if (type == EventObject.SOUND_EVENT)
        {
            //if(eventObject.name=="run1"|| eventObject.name == "run2") _yanmu.Play();
            GetComponent<RoleAudio>().PlayAudio(eventObject.name);
        }


        if (type == EventObject.FRAME_EVENT)
        {

            if (eventObject.name == "jn_begin")
            {
              
            }


            if (eventObject.name == "ac")
            {
                if (TX_Qishi)
                {
                    TX_Qishi.Play();
                }

                Fire();
            }
        }

    }



}
