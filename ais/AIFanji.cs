﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFanji : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!_gameBody) _gameBody = GetComponent<GameBody>();
    }

    public string SHANBI = "houshan_1";


    [Header("反击前 后闪 速度 **不要超过100")]
    public float HouShanValue = 40;

    public string FANJI = "atk_1";

    bool isFanjiing = false;
    bool isFanji = false;

    bool isGongji = false;
    bool isGongjiing = false;

    [Header("是否增加硬直")]
    public bool isAddYZ = false;



    bool isQianh = false;
    bool isQhuaing = false;
    [Header("是否增加前滑动作")]
    public bool isQianHua = false;

    [Header("是否是技能攻击反击(有名字就是 “”就不是)")]
    public string skillAtkName = "";

    [Header("增加硬直数值")]
    public float addYZNum = 500f;

    [Header("反击几率")]
    public int fanjijilv = 0;

    [Header("反击几率是否能根据被攻击次数增加")]
    public bool isJVCanZJ = true;

    
    [Header("反击是否动作延迟（给与一定反应时间）")]
    public float atkDelayTimes = 0;

    public bool IsFanjiing()
    {
        return isFanjiing;
    }


    int BeHitNum = 0;
    int jsNums = 0;
    int AddMaxNums = 30;

    //是否被打断累加（被连续攻击 反击值会累加）
    bool IsBeHitCut()
    {
        if (jsNums>AddMaxNums) {
            BeHitNum = 0;
            jsNums = 0;
            return true;
        }
        return false;
    }

    void JiShuQi()
    {
        //print("反击计数器");
        jsNums++;
        if (jsNums > AddMaxNums)
        {
            //print("-------------------------------------------中断！！"+ (fanjijilv + BeHitNum));
            BeHitNum = 0;
            jsNums = 0;
        }
    }

    GameObject thePlayer;


    int beHitNum = 0;
    public void GetFanji()
    {
        //print("  ************************************************反击*******************************************************************  ");
        //print("   进入 AI反击 ！！  "+_gameBody.isAcing+"  v2  "+_gameBody.GetPlayerRigidbody2D().velocity+ "   isFanjiing   "+ isFanjiing);


        //print(" 进入反击 几率 动作是什么 ？ "+_gameBody.GetDB().animation.lastAnimationName);


        if (isFanjiing) return;
        //print(1 + "    beHitNum  "+ beHitNum+ "   gameBody.beHitNum???  " + _gameBody.beHitNum);
        if (beHitNum == _gameBody.beHitNum) return;
        print(2);
        beHitNum = _gameBody.beHitNum;
        print(3);
        int n = (int)UnityEngine.Random.Range(0, 100);
        if (isJVCanZJ&&!IsBeHitCut())
        {
            BeHitNum += 5;
            jsNums = 0;
            //print("???  判断是否被连续攻击 BeHitNum  " + BeHitNum);
        }
        if (n <= (100 - fanjijilv- BeHitNum)) return;

        print("    、、、、、、、、、、、、、、、、///////////////// 反击成功！！！！  ");
        //print("----------------------------------反击！！！   n:  "+n+"  ??:  "+(100-(fanjijilv + BeHitNum))+ "   BeHitNum   "+ BeHitNum+ "  fanjijilv   "+ fanjijilv);
        if (!isFanji)
        {
            isFanji = true;
            isFanjiing = true;
            print("  进入 反击    "+ _gameBody.isAcing);
            if (GetComponent<AIAirBase>())
            {
                GetComponent<AIAirBase>().ReSetAll2();
                GetComponent<AIAirBase>().QuXiaoAC();
                //print("  空怪 反击  这个时候的 速度是多少 " + _gameBody.GetPlayerRigidbody2D().velocity);
                //_gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
            }
            else
            {
                //GetComponent<AIBase>()
            }
            _gameBody.FanJiBeHitReSet();
            //清0速度
            _gameBody.SpeedXStop();
            //print("fj >>>>>>   "+ HouShanValue);


            if (!thePlayer) thePlayer = GlobalTools.FindObjByName(GlobalTag.PlayerObj);

            if (thePlayer)
            {
                //print("-----------------------------------------------------------------------后闪 ");
                //print("----thisname   " + this.name);
                //print("   ----  " + this.transform.localScale.x);

                if (thePlayer.transform.position.x > this.transform.position.x)
                {

                    GetComponent<GameBody>().TurnRight();
                    //print(" ----  右转身！！！！ ");
                }
                else
                {
                    GetComponent<GameBody>().TurnLeft();
                    //print(" ----  左转身！！！！ ");
                }

                //print("   ----！！！！  " + this.transform.localScale.x);

            }

            _gameBody.GetBackUp(HouShanValue);
        }
        
    }


    bool isHasHFYZ = true;
    private void init()
    {
        if (!isHasHFYZ&& isAddYZ)
        {
            isHasHFYZ = true;
            GetComponent<RoleDate>().hfYZ(addYZNum);
        }
        isFanji = false;
        isFanjiing = false;
        isGongji = false;
        isGongjiing = false;

        isQianh = false;
        isQhuaing = false;
        
    }

    GameBody _gameBody;
    // Update is called once per frame
    void Update () {
        //print("----------------------------------------------------------------->     "+GlobalTools.GetRandomNum(2));


        JiShuQi();

        if (GetComponent<RoleDate>().isDie) {
            init();
            return;
        }

        if (GetComponent<RoleDate>().isBeHiting)
        {
            init();
            return;
        }

        if (!isFanjiing) return;

        if (isGongjiing && _gameBody.IsAtkOver())
        {
            init();
            return;
        }


        if (isQhuaing)
        {
            if (_gameBody.GetQianhuaOver())
            {
                isQhuaing = false;
            }
            return;
        }



        if (_gameBody && _gameBody.GetBackUpOver())
        {
            if (isQianHua&&!isQianh)
            {
                isQianh = true;
                isQhuaing = true;
                //qianhua
                _gameBody.Qianhua();
                return;
            }

            
            if (!isGongji)
            {
                isGongji = true;
                //判断是否包含该攻击动作 没有的话 返回
                //if (!_gameBody.IsHanAC(FANJI))
                //{
                //    isGongji = false;
                //    isFanjiing = false;
                //    return;
                //}

                //判断是否是技能攻击
                if (skillAtkName!="")
                {
                    string[] strArr = skillAtkName.Split('|');
                    int length = strArr.Length;
                    string _skilName = skillAtkName; 
                    if (length > 1)
                    {
                        int i = GlobalTools.GetRandomNum(length);
                        print("  fanjinums  i =     "+i);
                        _skilName = strArr[i];
                    }
                    print(" ******************************************反击的 技能名字   "+ _skilName);


                    if (GetComponent<AIAirBase>())
                    {
                        GetComponent<AIAirBase>().GetAtkFSByName(_skilName);
                    }
                    else
                    {
                        GetComponent<AIBase>().GetAtkFSByName(_skilName);
                    }
                    
                    init();
                    return;
                }
                else{
                    _gameBody.GetAtk(FANJI);
                }
                
                if (atkDelayTimes!=0)
                {
                    _gameBody.GetPause(atkDelayTimes);
                }
                isGongjiing = true;
                //print("-------------------------------------------------------反击");
                //是否提高硬直
                if (isAddYZ)
                {
                    GetComponent<RoleDate>().addYZ(addYZNum);
                    isHasHFYZ = false;
                }
               
            }
        }
        
       
	}
}
