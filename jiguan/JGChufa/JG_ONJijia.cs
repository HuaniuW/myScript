using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_ONJijia : JG_ChufaBase
{
    [Header("机甲")]
    public GameObject Jijia;

    Transform OnPos;

    GameObject _player;

    protected override void GetStart()
    {
        base.GetStart();
        if (!_player) _player = GlobalTools.FindObjByName(GlobalTag.PlayerObj);
        OnPos = Jijia.GetComponent<JijiaGamebody>().OnPos;
    }

    protected override void Chufa()
    {
        print("  乘坐 机甲 触发！！！！！！！！！！！ ");
        Globals.isInPlot = true;
        //GlobalTools.FindObjByName(GlobalTag.PLAYERUI).GetComponent<PlayerUI>().HideSelf();
        _player.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
        IsGotoJijia = true;
        _player.GetComponent<PlayerGameBody>().DodgeOver();
    }


    
    private void Update()
    {
        GotoJijia();

       

    }

   

    bool IsGotoJijia = false;
    void GotoJijia()
    {
        if (!IsGotoJijia) return;
        if (_player.GetComponent<GameBody>().IsGround)
        {
           
            if (Mathf.Abs(_player.transform.position.x - OnPos.position.x) <= 0.2f)
            {
                _player.transform.position = new Vector2(OnPos.position.x, _player.transform.position.y);
                IsGotoJijia = false;
                _player.GetComponent<GameBody>().GetStand();
                _player.transform.localScale = Jijia.transform.localScale;
                Shangji();
                return;
            }

            if (_player.GetComponent<GameBody>().GetDB().animation.lastAnimationName != _player.GetComponent<GameBody>().RUN)
            {
                _player.GetComponent<GameBody>().GetDB().animation.GotoAndPlayByFrame(_player.GetComponent<GameBody>().RUN);
            }



            if (_player.transform.position.x < OnPos.position.x)
            {
                _player.GetComponent<GameBody>().RunRight(2, true);
            }
            else
            {
                _player.GetComponent<GameBody>().RunLeft(-2, true);
                print("超过了 转向啊 ！！！！  ");
            }
            _player.GetComponent<GameBody>().ControlSpeed(4);
        }
    }

    private void Shangji()
    {
        _player.gameObject.SetActive(false);
        Jijia.GetComponent<JijiaGamebody>().GetUI();
        RemoveSelf();
    }
}
