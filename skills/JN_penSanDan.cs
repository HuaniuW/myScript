using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JN_penSanDan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ZiDanType();
    }

    //喷射子弹
    const string PENSHE_ZIDAN = "TX_zidansan";
    //上升高速子弹
    const string SSGaoSu_ZIDAN = "TX_zidanUpGaoSu";
    //上升后的 慢速跟踪子弹
    const string SSGenZong_ZIDAN = "TX_zidanUpGZ";

    //上升后一般慢速子弹
    const string SSYiBanSuDu_ZIDAN = "TX_zidanUpYiBanSuDu";


    string zidanName = PENSHE_ZIDAN;

    [Header("单边喷发的数量")]
    public int FSNums = 5;

    [Header("喷发的 宽度 ")]
    public float PSWidth = 6;

    [Header("喷发的 高度 ")]
    public float PSHeight = 6;

    [Header("喷发的 宽度 单个子弹的随机宽度")]
    public float RandomWidth = 0.5f;

    [Header("喷发的 宽度 单个子弹的随机宽度")]
    public float RandomHeight = 5f;



    [Header("是否喷向上 的散弹")]
    public bool IsPenSanDanUp = true;
    [Header("是否喷向前 的散弹")]
    public bool IsPenSanDanFront = false;

    [Header("跟踪弹 向上的")]
    public bool IsPenGenzongDanUp = false;

    [Header("先向上的 高速子弹")]
    public bool IsPenGaoSuZidanUp = false;

    [Header("先向上的 一般速度子弹")]
    public bool IsYiBanSuDuZidanUp = false;


    [Header("子弹的 初始速度")]
    public float SpeedZD = 20;


    [Header("子弹的 队伍")]
    public int Team = 2;



    bool IsFaSheSelf = false;

    void ZiDanType()
    {
        if (IsPenSanDanUp)
        {
            IsFaSheSelf = false;
            zidanName = PENSHE_ZIDAN;
        }
        else if(IsPenGaoSuZidanUp)
        {
            //高速
            IsFaSheSelf = true;
            zidanName = SSGaoSu_ZIDAN;
        }else if (IsPenGenzongDanUp)
        {
            //跟踪
            IsFaSheSelf = true;
            zidanName = SSGenZong_ZIDAN;
        }else if (IsYiBanSuDuZidanUp)
        {
            IsFaSheSelf = false;
            zidanName = SSYiBanSuDu_ZIDAN;
        }
    }



    bool isGetStart = false;
    private void OnEnable()
    {
        isGetStart = false;
    }




    void PenSanDanUp()
    {
        //左边
        //int nums = int.Parse(FSNums/2);
        PiLiangFaSheYC();
        PiLiangFaSheYC(false);

        //IsFaSheing = true;
        //IsFaSheingL = true;
        //print(" >>>>>>>>>>>>>>>发射数量    "+FSNums);

    }

    //同时发射  批量发射
    void PiLiangFaShe(bool isLeft = true)
    {
        for (int i = 0; i < FSNums; i++)
        {
            //创建 子弹
            FaShei(isLeft,i);
        }

    }

    //延迟发射
    void PiLiangFaSheYC(bool isLeft = true)
    {
        float yctime = 0;

        for (int i = 0; i < FSNums; i++)
        {
            //print("总发射子弹数量   "+ FSNums+"    i    "+i);
            yctime = GlobalTools.GetRandomDistanceNums(0.6f);
            //如果长度 只有1  直接发射 无延迟
            if (FSNums == 1) yctime = 0;
            //print("yctimes      "+yctime);
            StartCoroutine(IEFaShe(isLeft, i, yctime));
            //创建 子弹
            //FaShei(isLeft, i);
        }

    }

    void ReSetAll()
    {
        IsFaSheing = false;
        IsStartFaShe = false;
        _YCJiShiTimes = 0;
        _YCTime = 0;
        _i = 0;

        IsFaSheingL = false;
        IsStartFaSheL = false;
        _YCJiShiTimesL = 0;
        _YCTimeL = 0;
        _iL = 0;
    }


    bool IsFaSheing = false;
    bool IsStartFaShe = false;
    float _YCJiShiTimes = 0;
    float _YCTime = 0;
    int _i = 0;

    void ZiDanFaShe()
    {
        if (!IsFaSheing) return;
        if (!IsStartFaShe)
        {
            IsStartFaShe = true;
            _YCTime = GlobalTools.GetRandomDistanceNums(0.3f);
            _YCJiShiTimes = 0;
        }

        _YCJiShiTimes += Time.deltaTime;
        if (_YCJiShiTimes >= _YCTime)
        {
            _YCJiShiTimes = 0;
            _i++;
            if (_i<= FSNums)
            {
                FaShei(false, _i);
            }
            else
            {
                _i = 0;
                IsFaSheing = false;
            }
        }
    }



    bool IsFaSheingL = false;
    bool IsStartFaSheL = false;
    float _YCJiShiTimesL = 0;
    float _YCTimeL = 0;
    int _iL = 0;

    //朝左 发射
    void ZiDanFaSheL()
    {
        if (!IsFaSheingL) return;
        if (!IsStartFaSheL)
        {
            IsStartFaSheL = true;
            _YCTimeL = GlobalTools.GetRandomDistanceNums(0.3f);
            _YCJiShiTimesL = 0;
        }

        _YCJiShiTimesL += Time.deltaTime;
        if (_YCJiShiTimesL >= _YCTimeL)
        {
            _YCJiShiTimesL = 0;
            _iL++;
            if (_iL <= FSNums)
            {
                FaShei(true, _iL);
            }
            else
            {
                _iL = 0;
                IsFaSheingL = false;
            }
        }
    }





    public IEnumerator IEFaShe(bool isLeft = true, int i = 0, float time = 1)
    {
        //Debug.Log("i:   "+i+"   time   "+time);
        yield return new WaitForSeconds(time);
        //print("发射！！！！！    "+time);
        FaShei(isLeft, i);
        //yield return new WaitForSeconds(time);
        //GetInstance().DestoryObject(gameObject);
    }


    

    void FaShei(bool isLeft = true,int i = 0)
    {
        GameObject zidan = ObjectPools.GetInstance().SwpanObject2(Resources.Load(zidanName) as GameObject);
        //GlobalTools.GetGameObjectByName(PENSHE_ZIDAN);

        //初始化子弹位置

        //print(this.transform.position  +"    ---  "+zidan.transform.position);

        //zidan.transform.position = this.transform.position;

        //子弹的 队伍
        zidan.GetComponent<JN_Date>().team = this.Team;

        //计算 角度
        float _x = 0;
        float __x = 0;
        if (isLeft)
        {
            //zidan.transform.position = this.transform.position;
            __x = this.transform.position.x - PSWidth / FSNums * i;
            zidan.transform.position = new Vector3(__x, this.transform.position.y, this.transform.position.z);

            _x = __x - GlobalTools.GetRandomDistanceNums(RandomWidth);
        }
        else
        {
            __x = this.transform.position.x + PSWidth / FSNums * i;
            zidan.transform.position = new Vector3(__x, this.transform.position.y, this.transform.position.z);
            _x = __x + GlobalTools.GetRandomDistanceNums(RandomWidth);
        }

        float _y = this.transform.position.y + PSHeight + GlobalTools.GetRandomDistanceNums(RandomHeight);

        float xiuzheng = 1;

        if(PenSheType == 1)
        {
            xiuzheng = 1;
        }
        else if (PenSheType == 2)
        {
            if (GlobalTools.FindObjByName("player").transform.position.x > this.transform.position.x)
            {
                //在右
                xiuzheng = 1 + GlobalTools.GetRandomDistanceNums(1f);
            }
            else
            {
                xiuzheng = 0.6f + GlobalTools.GetRandomDistanceNums(0.4f);
            }
        }
        else
        {
            if (GlobalTools.FindObjByName("player").transform.position.x > this.transform.position.x)
            {
                //在右
                xiuzheng = 1.2f + GlobalTools.GetRandomDistanceNums(0.8f);
            }
            else
            {
                xiuzheng = 0.2f + GlobalTools.GetRandomDistanceNums(0.4f);
            }
        }

       



        Vector2 pos = new Vector2(_x* xiuzheng, _y);
        //发射
        if (!IsFaSheSelf) {
            zidan.GetComponent<TX_zidan>().GetDiretionByV2(pos, SpeedZD);
        }
        else
        {
            zidan.GetComponent<TX_zidan>().GetZiDanStart();
        }
        
    }


    [Header("喷射的类型 1正常 2稍微偏向玩家方向 3多偏向玩家")]
    public int PenSheType = 1;

   

    public void GetStart()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isGetStart)
        {
            isGetStart = true;
            //print("发射子弹！！！！！！！！！！！！！");
            PenSanDanUp();
        }
        //ZiDanFaShe();
        //ZiDanFaSheL();
    }
}
