using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shanbi : UI_Skill
{
    // Start is called before the first frame update
    GameObject _player;

    void Start()
    {
        if (IsLeft)
        {
            maxCDDistance = MaskImg.GetComponent<RectTransform>().rect.width;
        }
        else
        {
            maxCDDistance = MaskImg.GetComponent<RectTransform>().rect.height;
        }
        //_player = GlobalTools.FindObjByName("player");
        //skillCanUseTimes = SkillCanUseNums;
        //初始化 技能可用次数的 显示文本
        //SetText(skillCanUseTimes.ToString());

        //设置 默认 遮罩地板颜色
        Color _col = new Color(66 / 255f, 66 / 255f, 66 / 255f, 1);
        SetImgColor(TheMask, _col);
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override int GetUseTimes()
    {
        return skillCanUseTimes;
    }

    int shanbiNums = 0;

    public override bool isCanBeUseSkill()
    {
        //print("skillCanUseTimes   "+ skillCanUseTimes+"    ");
        shanbiNums = skillCanUseTimes;
        if (skillCanUseTimes == 0)
        {
            
            if (NoNumsBeep != null) NoNumsBeep.Play();
            GetComponent<UIShake>().GetShake();
            if (!_player) {
                _player = GlobalTools.FindObjByName("player");
            }

            if (_player.GetComponent<RoleDate>().live > 100)
            {
                GetComponent<TheTimer>().CDQing0();
                CDStart();                
                _player.GetComponent<RoleDate>().live -= 50;
                shanbiNums = skillCanUseTimes - 1;
                return true;
            }

            return false;
        }

        skillCanUseTimes--;
        SetText(skillCanUseTimes.ToString());
        if (skillCanUseTimes == 0)
        {
            //print("开始技能计时    ---------------------");
            CDStart();
        }
        return true;
    }


    public void Init()
    {
        if (_hzDate != null) return;
        //_hzDate = hzDate;
        //在这根据 信息 初始化
        SkillCanUseNums = 3;
        skillCanUseTimes = 3;
        CDTimeNums = 2;
        //print("CDTimeNums   "+ CDTimeNums+"   ----    "+ Intervals);
        GetComponent<TheTimer>().ContinuouslyTimesAdd(CDTimeNums, Intervals, CDCallBack);
        GetComponent<UIShake>().GetShakeObj(this.gameObject);
        //print("初始化 徽章的 技能信息！！！" + skillCanUseTimes);
        SetText(skillCanUseTimes.ToString());


        //查询 是够有 全局技能使用记录 有的话 从记录开始初始化   数据记录系统 优化

    }
}
