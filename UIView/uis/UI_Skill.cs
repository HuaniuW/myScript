using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Skill : MonoBehaviour {
    [Header("技能id")]
    public string skillID = "";
    public int skillNums;
    public Image TheMask;
    public Image MaskImg;
    [Header("技能CD满后 使用的次数 显示文本  text")]
    public Text SkillCanUseNumsText;
    [Header("技能CD满后 可以使用的次数")]
    public int SkillCanUseNums = 0;
    public AudioSource Beep;

    public AudioSource NoNumsBeep;
    [Header("CD时间")]
    public float CDTimeNums = 0;
    [Header("CD成长间隔时间")]
    public float Intervals = 0.1f;

    public bool IsLeft = false;

    //CD时间
    //使用次数

    protected int skillCanUseTimes = 0;

    public virtual int GetUseTimes()
    {
        return skillCanUseTimes;
    }
    //是否 可以使用技能
    public virtual bool isCanBeUseSkill()
    {
        //print("skillCanUseTimes   "+ skillCanUseTimes+"    ");
        if (skillCanUseTimes == 0) {
            if(_hzDate.type == "zd")
            {
                if (NoNumsBeep != null) NoNumsBeep.Play();
                GetComponent<UIShake>().GetShake();
            }
            return false;
        }
        
        skillCanUseTimes--;
        SetText(skillCanUseTimes.ToString());
        if (skillCanUseTimes == 0) {
            //print("开始技能计时    ---------------------");
            CDStart();
        }
        return true;
    }

    public bool IsCDSkillCanBeUse()
    {
        //print("   剩余使用次数  skillCanUseTimes  "+ skillCanUseTimes);
        if (skillCanUseTimes == 0) return false;
        return true;
    }




    void OnDistory()
    {

    }


    public void CDFull()
    {
        skillCanUseTimes = _hzDate.usenums;
        SetText(skillCanUseTimes.ToString());

        if (IsLeft)
        {
            Wh(MaskImg, maxCDDistance, 30);
        }
        else
        {
            Wh(MaskImg, 30, maxCDDistance);
        }

        GetComponent<TheTimer>().GetFull();
    }

    //在全局找 技能信息的数据
    public void GetGlobalSkillDate()
    {
        string globalSkillUseDate =  GlobalSetDate.instance.CurrentMapMsgDate.skill_use_date;
        //string globalSkillUseDate = GlobalSetDate.instance.TempSkillUseRecord;
        //print("读取 背包数据:   " + GlobalSetDate.instance.CurrentUserDate.bagDate);
        //print(" 读取 CurrentMapMsgDate 技能的 使用数据  " + GlobalSetDate.instance.CurrentMapMsgDate.skill_use_date);
        //print("GetGlobalSkillDate   ? "+ globalSkillUseDate+"  ------  "+ _hzDate.HZName);
        string msg = GetStringByName(_hzDate.HZName, globalSkillUseDate);
        print("msg:  "+msg+"  =null?  "+(msg == null));
        if (msg != null)
        {
            string[] strArr = msg.Split('_');
            int nums = int.Parse(strArr[1]);
            float cds = float.Parse(strArr[2]);
            float miaoshuNums = float.Parse(strArr[3]);
            SetSkillDate(nums,cds,miaoshuNums);
        }
    }

    string GetStringByName(string _name,string dateStr) {
        string[] strArr = dateStr.Split('|');
        foreach(string s in strArr)
        {
            if (s.Split('_')[0] == _name) return s;
        }
        return null;
    }

    string TiHuanStringInStr(string _name, string tihuanDate,string dateStr)
    {
        string[] strArr = dateStr.Split('|');
        string str = "";
        for (int i=0;i<strArr.Length;i++)
        {
            if (strArr[i].Split('_')[0] == _name) {
                str += i== strArr.Length-1? tihuanDate:tihuanDate + "|";
                continue;
            } 
            str += i == strArr.Length - 1 ? strArr[i]:strArr[i]+"|";
        }
        //print("str 替换 后 是什么鬼   "+str);
        return str;
    }

    //对比 设置 技能数据
    public void SetSkillDate(int nums,float cds,float miaoshuNums)
    {
        //print("对比数据   "+_hzDate.HZName+"    "+nums);


        skillCanUseTimes = nums;
        if (nums == 0)
        {
            /**if (Time.realtimeSinceStartup - (gameTime + miaoshuNums) > _hzDate.cd)
            {
                CDFull();
                return;
            }*/
            //开始 CD计时

            //skillCanUseTimes = nums;
            //print("----------------------------------------------------------------------------------");
            //print("skillCanUseTimes   "+ skillCanUseTimes);
            //print(" cdDistance      "+cds);
            //print("  miaoshuNums   " + miaoshuNums);

            //print("----------------------------------------------------------------------------------@");

            
            
            cdDistance = cds;
            GetComponent<TheTimer>().TempSetTimeNums(miaoshuNums);
        }
        SetText(skillCanUseTimes.ToString());
    }

    public string GetSkillDate()
    {
        string str = "";
        //徽章名称_剩下可以使用次数_cd长度（图片涨到哪了 宽高）_剩余cd读秒数    
        str = _hzDate.HZName + "_" + skillCanUseTimes + "_" + cdDistance + "_" + miaoshuNum;
        return str;
    }

    //将数据 存入全局数据
    public void GetDateInGlobalSkillDate()
    {
        string hzUseDate = GetSkillDate();
        //print(" hzUseDate  "+ hzUseDate);
        string globalSkillUseDate = GlobalSetDate.instance.CurrentMapMsgDate.skill_use_date;
        print(globalSkillUseDate);
        //string globalSkillUseDate = GlobalSetDate.instance.TempSkillUseRecord;
        string msg = GetStringByName(_hzDate.HZName, globalSkillUseDate);
        //print(" msg=?  "+msg);
        if (msg == null) {
            GlobalSetDate.instance.CurrentMapMsgDate.skill_use_date += "|" + hzUseDate;
        }
        else
        {
            //替换文字信息？？？
            GlobalSetDate.instance.CurrentMapMsgDate.skill_use_date = TiHuanStringInStr(_hzDate.HZName, GetSkillDate(), GlobalSetDate.instance.CurrentMapMsgDate.skill_use_date);
        }

        //print(" 存入徽章的使用数据 后的 总date数据   "+ globalSkillUseDate);

    }

    protected HZDate _hzDate;
    public virtual void SetHZDate(HZDate hzDate)
    {
        if (_hzDate != null) return;
        _hzDate = hzDate;
        //在这根据 信息 初始化
        SkillCanUseNums = _hzDate.usenums;
        skillCanUseTimes = SkillCanUseNums;
        CDTimeNums = _hzDate.cd;
        //print("CDTimeNums   "+ CDTimeNums+"   ----    "+ Intervals);
        GetComponent<TheTimer>().ContinuouslyTimesAdd(CDTimeNums, Intervals, CDCallBack);
        GetComponent<UIShake>().GetShakeObj(this.gameObject);
        //print("初始化 徽章的 技能信息！！！" + skillCanUseTimes);
        SetText(skillCanUseTimes.ToString());


        //查询 是够有 全局技能使用记录 有的话 从记录开始初始化   数据记录系统 优化
        
    }

    public HZDate GetHZDate()
    {
        return _hzDate;
    }

    string _skillPos;
    //在哪个方向上 u  d  c r
    public void SetSkillPos(string str)
    {
        _skillPos = str;
    }

    public string GetSkillPos()
    {
        return _skillPos;
    }


    // Use this for initialization
    void Start () {
        //是横向 还是纵向
        if (IsLeft)
        {
            maxCDDistance = MaskImg.GetComponent<RectTransform>().rect.width;
        }
        else
        {
            maxCDDistance = MaskImg.GetComponent<RectTransform>().rect.height;
        }

        //skillCanUseTimes = SkillCanUseNums;
        //初始化 技能可用次数的 显示文本
        //SetText(skillCanUseTimes.ToString());

        //设置 默认 遮罩地板颜色
        Color _col = new Color(66 / 255f, 66 / 255f, 66 / 255f, 1);
        SetImgColor(TheMask,_col);

        //设置 CD时间
       
        //CDStart();
    }


    //是否可以用技能 如果不能 播放不能 使用技能的声音


    float miaoshuNum = 0;
    protected void CDCallBack(float nums) {
        cdDistance += maxCDDistance / CDTimeNums* Intervals;
        if (IsLeft)
        {
            Wh(MaskImg, cdDistance, 30);
        }
        else
        {
            Wh(MaskImg, 30, cdDistance);
        }
        miaoshuNum = nums;
        //print("miaoshuNum  返回的数据数值是多少     "+ miaoshuNum);
        if (nums <= 0) {
            //CD满 播放提示音 和抖一下
            Beep.Play();
            SetText(skillCanUseTimes.ToString());
            //SetText("2");
            GetComponent<UIShake>().GetShake();
            skillCanUseTimes = SkillCanUseNums;
            SetText(skillCanUseTimes.ToString());
            //print("CD完成！！！");
            if (GetComponent<UI_FeiDao>() != null) GetComponent<UI_FeiDao>().HasShouDao(null);
        }
    }

   


    protected float maxCDDistance = 0;
    float cdDistance = 0;
    //CD归0 并且开始CD
    protected void CDStart(float cds = 1,float cdDistanceNums = 0)
    {
        if (IsLeft)
        {
            Wh(MaskImg, cds, 30);
        }
        else
        {
            Wh(MaskImg, 30, cds);
        }
        cdDistance = cdDistanceNums;
        //print("再次开始计时CD   ");
        GetComponent<TheTimer>().GetContinuouslyTimesStart();

    }

	
	// Update is called once per frame
	void Update () {

    }

    void Wh(Image obj, float w, float h)
    {
        var rt = obj.GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
    }

    protected void SetImgColor(Image obj, Color color)
    {
        //obj.GetComponent<Image>().material.color = color;  改变 image material 的属性颜色  这个灰改变所有UI的 material的颜色
        //这个才是改变 image 的color
        obj.GetComponent<Image>().color = color;
    }

    //改变 text文本
    protected void SetText(string str)
    {
        if (SkillCanUseNumsText == null) return;
        SkillCanUseNumsText.text = str;
        //print("SkillCanUseNumsText.text --->   "+ SkillCanUseNumsText.text);
        if (SkillCanUseNumsText.text == "0")
        {
            SkillCanUseNumsText.text = "";
        }
    }
}
