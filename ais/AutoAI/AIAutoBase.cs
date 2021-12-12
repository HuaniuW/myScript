using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAutoBase : MonoBehaviour
{
    //难度系数  难度系数 如何 计算？
    [Header("怪物难度系数")]
    public int Hardlevel= 1;

    //怪物类型 空中 地面 毒系 电系 混合等
    [Header("怪物类型 地面 空中")]
    public string MonsterType = "kongzhong";

    [Header("怪物属性 电 火 毒 一般")]
    public string MonsterNature = "yiban";


    //精英 boss 小怪
    [Header("怪物级别 小怪 精英 boss")]
    public string MonsterLevel = "xiaoguai";



    //技能招式列表
    //攻击型招式
    //防守型招式
    //移动方式  隐身 远离
    //召唤 在 召唤类里面 设置 是否能召唤 召唤动作  召唤位置  召唤的怪物名字

    //变招 如果 玩家靠近  切换反击模式


    //高级别怪物 模型 比如龙 配置就不一样
    //怪物组合 组合的 特殊技能--
    //这里 要获取 和排除 是否 含有 招式的 代码？  能不能动态获取 不能的话就给到AI里面去
    //哪些 能力 可以动态获取？   子弹类型   攻击距离 预警距离  子弹速度

    //越厉害 子弹级别控制？
    List<string> ZS_Zidans = new List<string>() { "AIZiDans_1", "AIZiDans_2", "AIZiDans_3", "AIZiDans_4", "AIZiDans_5" };

    //间隔 休息的时间控制  


    //空怪 一般是 子弹 和 冲击  压 能
    //移动 有隐身 飞行
    //防御 技能   被攻击 隐身 后退 爆炸（这里面又有好多类型 范围爆炸  落下爆成火焰区域燃烧  爆炸出子弹 等 ）


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //*** 第一个目标   子弹123456 选几个  移动方式选1个   被动技能选一个



}
