using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ZDShiyingATK : AI_SkillBase
{
    //自动 适应攻击
    //ZD_ZDShiyingATK_1       ZD_lz_ZDShiyingATK
    //默认的 1  可以前攻击 上攻击
    // Start is called before the first frame update
    protected override void TheStart()
    {
        //base.TheStart();
        this.ZSName = "ZDShiyingATK";
        this.IsSpeAISkill = true;
    }

    [Header("攻击距离之外的 距离")]
    public float OutAtkDistanceX = 14;

    [Header("攻击前面的 动作名字")]
    public string AtkQianmianACName = "";
    [Header("攻击 上面的 动作名字")]
    public string AtkUpACName = "";

    protected override void GetTheStart()
    {
        base.GetTheStart();
        if (_player == null)
        {
            _player = GlobalTools.FindObjByName("player");
        }
        //判断 是否 在攻击距离之外
        if (Mathf.Abs(this.transform.position.x - _player.transform.position.x) > 14)
        {
            print("在 攻击范围外 不攻击！");
            TheSkillOver();
            return;
        }


    }

    [Header("临时提高硬直")]
    public float TempAddYingzhi = 800;

    bool IsAtk = false;

    protected override void ChixuSkillStarting()
    {
        


        if (!IsAtk)
        {
            IsAtk = true;

            GetComponent<TempAddValues>().TempAddYZ(TempAddYingzhi,1);

            if (_player.transform.position.y > this.transform.position.y + 1 && Mathf.Abs(_player.transform.position.x - this.transform.position.x) <= 3)
            {
                _gameBody.GetAtk(AtkUpACName);
            }
            else
            {
                _gameBody.GetAtk(AtkQianmianACName);
            }
        }

        if (_gameBody.IsAtkOver())
        {
            TheSkillOver();
        }

    }

    public override void ReSetAll()
    {
        base.ReSetAll();
        IsAtk = false;
    }
}
