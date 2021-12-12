using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_HuanshuBase : MonoBehaviour
{
    //起始点位置
    [Header("玩家 起始点位置 出现的位置")]
    public Transform Player_Out_Pos;

    //记录玩家 进来前的位置
    Vector2 PlayerPosBefore;

    //摄像机 出现位置 点
    public Transform Camera_Out_Pos;


    //记录摄像机 进来前的 位置
    Vector2 CameraPosBefore;

    //摄像机块
    public Transform CameraKuai;


    //之前的 摄像机块 名字  一般 是 kuang
    string CameraKuang = "kuang";


    //生成地形 类型判断  ** 只有左右连接 并且是 平地     跳跃云中   水面   位置点在中间出现   非打怪 从一边出现
    void GetDixing()
    {
        //基础 的 两组怪  精英 一般   都用固定 大小的 地形  有躲避机关的 倒计时  有打怪
        //景不一样 怪不一样      
        //1 基础打2-3组怪
        //2 跳跃地形 2-3组怪
        //3 水上 2-3组怪
        //******直接用6****4 机关 横向 喷火 左到右  有怪丢子弹  机关 机关  怪 这样的 生成
        //******直接用6****5 跳跃地形 有子弹机关 随机有怪 左到右 1组怪
        //6 跳跃 地板 混搭  上面 4-5 不用了

        //统一 红色 灯光

        //预留 根据难度 来调整怪物数量   

        //AI重启时间 停止
        //怪物不停出 有倒计时
        //破幻徽章

        //地火  电滚  子弹  有机关 有怪  只是 时间限制 6秒-10秒

        
        
        if (GlobalTools.GetRandomNum() > 0)
        {
            print("hs-- 战斗场景！");
            int suijiNum = GlobalTools.GetRandomNum();
            if (suijiNum < 30)
            {
                //地面
                
            }else if (suijiNum < 60)
            {
                //水面
            }else if (suijiNum < 90)
            {
                //跳跃
            }
            else
            {
                //精英
            }
        }
        else
        {
            //跳跃机关

            //躲避 电滚 机关 和子弹机关
        }


        //******倒计时 结束？？？  不停出怪？

        //暂停游戏

    }



    //地面地板 
    List<string> DimianDibanNameArr = new List<string>() { };

    
    //地面关
    void DimianGuan()
    {
        //颜色 红色
        //雾
        //树林
        //石头
    }

    //心跳声  结束 之前的 战斗音乐
    // 幻术  的红色 眼
    //恢复之前的 战斗音乐



    // Start is called before the first frame update
    void Start()
    {
        GetDixing();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //移除自己

}
