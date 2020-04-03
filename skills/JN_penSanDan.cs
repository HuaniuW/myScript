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


    const string PENSHE_ZIDAN = "TX_zidansan";
    //上升高速子弹
    const string SSGS_ZIDAN = "TX_zidanUpGD";
    //上升后的 慢速跟踪子弹
    const string SSGZ_ZIDAN = "TX_zidanUpGZ";


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
    public bool IsPenGenzongdanUp = false;

    [Header("先向上的 高速子弹")]
    public bool IsPenGaoSudanUp = false;




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
        else if(IsPenGaoSudanUp)
        {
            IsFaSheSelf = true;
            zidanName = SSGS_ZIDAN;
        }else if (IsPenGenzongdanUp)
        {
            IsFaSheSelf = true;
            zidanName = SSGZ_ZIDAN;
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
    }

    //同时发射
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
            yctime = GlobalTools.GetRandomDistanceNums(1);
            //如果长度 只有1  直接发射 无延迟
            if (FSNums == 1) yctime = 0;
            //print("yctimes      "+yctime);
            StartCoroutine(IEFaShe(isLeft, i, yctime));
            //创建 子弹
            //FaShei(isLeft,i);
        }

    }


    public IEnumerator IEFaShe(bool isLeft = true, int i = 0, float time = 1)
    {
        //Debug.Log("time   "+time);
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
        Vector2 pos = new Vector2(_x, _y);
        //发射
        if (!IsFaSheSelf) {
            zidan.GetComponent<TX_zidan>().GetDiretionByV2(pos, SpeedZD);
        }
        else
        {
            zidan.GetComponent<TX_zidan>().GetZiDanStart();
        }
        
    }


    
    

   

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
        
    }
}
