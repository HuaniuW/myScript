using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Huanshu : AI_SkillBase
{
    //生成地板  打怪 还是机关 机关1 机关2 机关3等
    void GetChangjing()
    {

    }


    // 生成怪 打机组   小怪 还是精英

    //记录 角色位置
    //GameObject _player;
    Vector2 PlayerPos;
    void RecordPlayerPos()
    {
        PlayerPos = _player.transform.position;
    }
    //记录原始 摄像机块 摄像机位置
    GameObject _mainCamera;
    Vector2 MainCameraPos;
    void RecordMainCameraPos()
    {
        MainCameraPos = _mainCamera.transform.position;
    }



    public ParticleSystem TX_HuanshuStart;     
    //开始 红色 幻术特效
    void ShowHuanshuTX()
    {
        //显示幻术特效 这个要在UI层面做？  显示 幻术 粒子    然后 全屏红 进入 新场景12
        //显示 幻术粒子  一定时间后   UI遮盖  进入场景
    }



    //进入 幻术场景
    //切换 摄像机位置摄像机块


    //结束 收到什么消息结束 返回原场景

    //切换 摄像机 位置  摄像机块



    protected override void ACSkillShowOut()
    {
        IsACSkillShowOut = true;

        if(!_player)_player = GlobalTools.FindObjByName("player");
        if (!_mainCamera) _mainCamera = GlobalTools.FindObjByName("MainCamera");

        //释放 特效
        //变红 灯光 颜色  4个点位 放大？
        //A*要移上去

    }


}
