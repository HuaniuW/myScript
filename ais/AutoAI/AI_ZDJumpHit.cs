using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ZDJumpHit : AI_SkillBase
{
    string TheJNName = "JumpHit";
    protected override void TheStart()
    {
        IsSpeAISkill = true;
        IsResetInStand = false;
    }

    float JumpVX = 500;

    float _playerX = 0;

    [Header("落地 烟尘 特效")]
    public ParticleSystem Lizi_LuodiYancheng;


    [Header("起跳声音")]
    public AudioSource JumpAudio;

    [Header("落地 声音")]
    public AudioSource JumpDownAudio;

    public override void GetStart(GameObject gameObj)
    {
        ReSetAll();
        if (StartAudio) StartAudio.Play();
        print("zd getStart *****************************************************************自动技能开始   "+TheJNName);
        _isGetStart = true;

        if (_player == null)
        {
            _player = GlobalTools.FindObjByName("player");
        }

        _playerX = _player.transform.position.x;

        float vx = this.transform.position.x > _player.transform.position.x ? -JumpVX : JumpVX;

        //_gameBody.GetPlayerRigidbody2D().velocity = new Vector2(0, 190);
        _gameBody.GetJump();
        _gameBody.Jump();
        //_gameBody.GetDB().animation.Stop();

        //print(" --------------> 落地 ？？？？？？？？ " + _gameBody.GetPlayerRigidbody2D().velocity.y + "    ACName    " + _gameBody.GetDB().animation.lastAnimationName);
        if (JumpAudio) JumpAudio.Play();

        _gameBody.GetZongTuili(new Vector2(vx, 0));
        //_gameBody.GetZongTuili(new Vector2(vx, 900));
    }


    protected override void ChixuSkillStarting()
    {
        IsGetOver();
        if (_gameBody.IsShentiHitWall && !_roleDate.isDie)
        {
            float vx = this.transform.localScale.x > 0 ? 400 : -400;
            print("  身体 前面撞墙了！！！！！ " + vx);
            _gameBody.GetZongTuili(new Vector2(vx, 0));
            return;
        }
    }

    protected override void OtherOver()
    {
        _gameBody.isJumping = false;
    }


    public override bool IsGetOver()
    {
        //print("  落地 ？？？？？？？？ " + _gameBody.GetPlayerRigidbody2D().velocity.y+"    ACName    "+ _gameBody.GetDB().animation.lastAnimationName);
        if (_gameBody.IsGround)
        {
            if (Lizi_LuodiYancheng && Lizi_LuodiYancheng.isStopped) {
                //print("  落地2222222222222 ？？？？？？？？ "+_gameBody.GetPlayerRigidbody2D().velocity.y);
                Lizi_LuodiYancheng.Play();
                if (JumpDownAudio&&_gameBody.GetPlayerRigidbody2D().velocity.y<=0) JumpDownAudio.Play();
            }
        }

        //print("  --------    "+ _gameBody.IsGround+"  ?? ");

        if (GetComponent<RoleDate>().isBeHiting || GetComponent<RoleDate>().isDie || (_gameBody.IsGround && _gameBody.IsGetStand()))
        {
            print("  licheng over!!! ");
            ReSetAll();
            TheSkillOver();
            return true;
        }
        return false;
    }


}
