using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFanji : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!_gameBody) _gameBody = GetComponent<GameBody>();
    }

    public string SHANBI = "houshan_1";

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


    int beHitNum = 0;
    public void GetFanji()
    {
        //print("   进入 AI反击 ！！  "+_gameBody.isAcing+"  v2  "+_gameBody.GetPlayerRigidbody2D().velocity);
        if (isFanjiing) return;
        //print(1);
        if (beHitNum == _gameBody.beHitNum) return;
        //print(2);
        beHitNum = _gameBody.beHitNum;
        //print(3);
        int n = (int)UnityEngine.Random.Range(0, 100);
        if (isJVCanZJ&&!IsBeHitCut())
        {
            BeHitNum += 5;
            jsNums = 0;
            //print("???  判断是否被连续攻击 BeHitNum  " + BeHitNum);
        }
        if (n <= (100 - fanjijilv- BeHitNum)) return;

        print("   进入 AI反击 ！！  反击成功！！！！  ");
        print("----------------------------------反击！！！   n:  "+n+"  ??:  "+(100-(fanjijilv + BeHitNum))+ "   BeHitNum   "+ BeHitNum+ "  fanjijilv   "+ fanjijilv);
        if (!isFanji)
        {
            isFanji = true;
            isFanjiing = true;
            print("  进入 反击    "+ _gameBody.isAcing);
            _gameBody.FanJiBeHitReSet();
            //清0速度
            _gameBody.SpeedXStop();
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
                    GetComponent<AIBase>().GetAtkFSByName(skillAtkName);
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
