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

    int skillCanUseTimes = 0;
    //是否 可以使用技能
    public bool isCanBeUseSkill()
    {
        if (skillCanUseTimes == 0) {
            if (NoNumsBeep != null) NoNumsBeep.Play();
            return false;
        }
        
        skillCanUseTimes--;
        SetText(SkillCanUseNums.ToString());
        return true;
    }


    void OnDistory()
    {

    }

    HZDate _hzDate;
    public void SetHZDate(HZDate hzDate)
    {
        _hzDate = hzDate;
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

        skillCanUseTimes = SkillCanUseNums;
        //初始化 技能可用次数的 显示文本
        SetText(SkillCanUseNums.ToString());

        //设置 默认 遮罩地板颜色
        Color _col = new Color(66 / 255f, 66 / 255f, 66 / 255f, 1);
        SetImgColor(TheMask,_col);

        //设置 CD时间
        GetComponent<TheTimer>().ContinuouslyTimesAdd(CDTimeNums, Intervals, CDCallBack);
        GetComponent<UIShake>().GetShakeObj(this.gameObject);
        CDStart();
    }


    //是否可以用技能 如果不能 播放不能 使用技能的声音



    void CDCallBack(float nums) {
        cdDistance += maxCDDistance / CDTimeNums* Intervals;
        if (IsLeft)
        {
            Wh(MaskImg, cdDistance, 30);
        }
        else
        {
            Wh(MaskImg, 30, cdDistance);
        }
        if (nums == 0) {
            //CD满 播放提示音 和抖一下
            Beep.Play();
            SetText(skillCanUseTimes.ToString());
            //SetText("2");
            GetComponent<UIShake>().GetShake();
        }
    }

   


    float maxCDDistance = 0;
    float cdDistance = 0;
    //CD归0 并且开始CD
    void CDStart()
    {
        if (IsLeft)
        {
            Wh(MaskImg, 1, 30);
        }
        else
        {
            Wh(MaskImg, 30, 1);
        }
        cdDistance = 0;
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

    void SetImgColor(Image obj, Color color)
    {
        //obj.GetComponent<Image>().material.color = color;  改变 image material 的属性颜色  这个灰改变所有UI的 material的颜色
        //这个才是改变 image 的color
        obj.GetComponent<Image>().color = color;
    }

    //改变 text文本
    void SetText(string str)
    {
        if (SkillCanUseNumsText == null) return;
        SkillCanUseNumsText.text = str;
        if (SkillCanUseNumsText.text == "1"|| SkillCanUseNumsText.text == "0")
        {
            SkillCanUseNumsText.text = "";
        }
    }
}
