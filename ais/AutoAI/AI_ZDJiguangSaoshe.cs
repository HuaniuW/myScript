using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ZDJiguangSaoshe : AI_SkillBase
{
    protected override void TheStart()
    {
        //ZDAIName  ----> ZD_ZDJiguangSaoshe_1_90
        //ZD_ZDJiguangSaoshe_1_90   -----> 1是从左到右 90是扫射度数     ZD_ZDJiguangSaoshe_2^100   --->2是从右往左扫
        //ZDJiguangSaoshe
        //base.TheStart();
        this.ZSName = "ZDJiguangSaoshe";
        if (_player == null) _player = GlobalTools.FindObjByName("player");
        this.IsSpeAISkill = true;
        print("***************************************激光管开始*********************************************");
    }

    //从左往右 还是 从右往左
    bool IsStartFromRight = true;

    [Header("扫射角度")]
    public float SaoguoJiaodu = 90;
    [Header("扫射 速度")]
    public float JiaoduSpeed = 1;

    float _QishiJiaodu = 90;



    protected override void GetTheStart()
    {
        //是不是从右边开始
        IsStartFromRight = int.Parse(ZDAIName.Split('_')[2]) == 2 ? true : false;
        //扫射 弧度
        SaoguoJiaodu = float.Parse(ZDAIName.Split('_')[3]);
        GetJiguang();
        GetQishiJiaodu();
        print("  激光开始！！！！！ ");

    }


    void TheOver()
    {
        //关闭激光

        Jiguang.GetComponent<Skill_JiguangSaoshe>().JiguangStop();
        Jiguang.gameObject.SetActive(false);
        TheResetAll();
        ReSetAll();
        TheSkillOver();
    }

    void TheResetAll()
    {
        QishihongYinxianNums = 0;
        IsYinxianOver = false;
        IsStartSaoshe = false;
        //IsStartYinxian = false;
    }

    [Header("起手引线 延迟时间")]
    public float QishiHongYinxianTimes = 0.5f;
    float QishihongYinxianNums = 0;
    bool IsYinxianOver = false;
    //结束时候的扫射角度
    float OverSaosheJD = 0;


    bool IsStartSaoshe = false;


    void StartJN()
    {
        if (Jiguang == null) return;

        if (!IsYinxianOver)
        {
            //显示 激光引导线
            //显示 发射 特效
          
            Jiguang.GetComponent<Skill_JiguangSaoshe>().JiguangYinxianStart();
            QishihongYinxianNums += Time.deltaTime;
            //print("  QishihongYinxianNums  "+ QishihongYinxianNums);
            if (QishihongYinxianNums >= QishiHongYinxianTimes)
            {
                IsYinxianOver = true;
                IsStartSaoshe = true;
                //关闭激光引导线 打开激光
                Jiguang.GetComponent<Skill_JiguangSaoshe>().JiguangYinxianStop();
                Jiguang.GetComponent<Skill_JiguangSaoshe>().JiguangStart();

            }
        }



        if (IsStartSaoshe)
        {
            if (IsStartFromRight)
            {
                print("从右边开始！！！！！！");
                _QishiJiaodu -= JiaoduSpeed;
                if (_QishiJiaodu <= OverSaosheJD)
                {
                    TheOver();
                }
            }
            else
            {
                _QishiJiaodu += JiaoduSpeed;
                if (_QishiJiaodu >= OverSaosheJD)
                {
                    TheOver();
                }
            }
            Jiguang.transform.localEulerAngles = new Vector3(0, 0, _QishiJiaodu);

        }




    }


    GameObject Jiguang;
    void GetJiguang()
    {
        Jiguang = GlobalTools.GetGameObjectByName("Skill_JiguangSaoshe");
        Jiguang.gameObject.SetActive(true);
        Jiguang.transform.position = zidanDian1.position;
        Jiguang.transform.localEulerAngles = new Vector3(0, 0, 0);
    }



    void JiGuangRemove()
    {
        if (Jiguang)
        {
            Jiguang.gameObject.SetActive(false);
            DestroyImmediate(Jiguang);
        }
    }


    void GetQishiJiaodu()
    {
        print("初始角度  "+ Jiguang.transform.localEulerAngles);
        if (IsStartFromRight)
        {
            
            _QishiJiaodu = 90 + (180-SaoguoJiaodu) * 0.5f+ SaoguoJiaodu;
            OverSaosheJD = _QishiJiaodu - SaoguoJiaodu;
            print("  从右开始    "+ OverSaosheJD);
        }
        else
        {
            _QishiJiaodu = 90 + (180-SaoguoJiaodu) * 0.5f;
            OverSaosheJD = _QishiJiaodu + SaoguoJiaodu;
            print("  从左开始    " + OverSaosheJD);
        }

        Jiguang.transform.localEulerAngles = new Vector3(0, 0, _QishiJiaodu);
        print("  角度*****    " + Jiguang.transform.localEulerAngles);
        //transform.localEulerAngles = new Vector3(0, 0, _rotationZ);
    }


    protected override void OtherOver()
    {
        //ReSetAll();
        //TheOver();
        //TheSkillOver();
        //print("***************************************************DIEoVER");
        //JiGuangRemove();
        //_isGetOver = true;
    }

    protected override void DieOrBehit()
    {
        JiGuangRemove();
    }


    protected override void ChixuSkillStarting()
    {
        if (_isGetOver) return;
        if (_roleDate.isDie || _roleDate.isBeHiting)
        {
            ReSetAll();
            //TheSkillOver();
            //print("***************************************************DIEoVER");
            JiGuangRemove();
            _isGetOver = true;
            return;
        }


        StartJN();
    }



}
