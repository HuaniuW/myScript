using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Huixue : AI_SkillBase
{
    [Header("提升硬直")]
    public float AddYingzhi = 500;

    [Header("持续时间")]
    public float ChixuShijian = 5;

    [Header("每秒回血")]
    public float MeimiaoHuixue = 500;



    public ParticleSystem HuixueTX;



    protected override void ACSkillShowOut()
    {
        //什么情况下回血 这个在怪物 血少于 1/2 时候 就会配置进技能


        IsACSkillShowOut = true;
        //提升硬直
        //显示加血特效
        HuixueTX.Play();
        //持续时间
        //回血量
        OverDelayTimes = ChixuShijian;

        _roleDate.addYZ(AddYingzhi);

    }


    protected override void OtherOver()
    {
        _roleDate.hfYZ(AddYingzhi);
        HuixueTX.Stop();
    }


    float jishiTimes = 0;
    protected override void ChixuSkillStarting()
    {
        if (IsACSkillShowOut)
        {
            jishiTimes += Time.deltaTime;
            if (jishiTimes >= 1)
            {
                jishiTimes = 0;
                _roleDate.live += MeimiaoHuixue;
            }
            
        }
    }
}
