using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_HXChongji2 : MonoBehaviour,ISkill
{
    GameBody _gameBody;
    RoleDate _roledate;

    GameObject _player;


    [Header("冲击的 时候 身上附带的 火焰")]
    public ParticleSystem Lizi_Huoyan;

    [Header("冲击的 开始的时候 龙吼声音")]
    public AudioSource Audio_CJStart;

    [Header("冲击的 攻击块")]
    public GameObject ChongJiHitKuai;

    [Header("自己的 攻击块")]
    public GameObject SelfHitKuai;
    [Header("冲击前的 准备动作")]
    public string CJ_Begin = "stand_4";
    [Header("冲击前的动作")]
    public string CJ_Start = "fly_1";
    [Header("冲击的横向 距离")]
    public float ChongJiDistance = 30;
    [Header("冲击的 最小 速度")]
    public float MinSpeed = 4;
    [Header("冲击的 最大-- 速度")]
    public float MaxSpeed = 20;


    [Header("冲击的 伤害值")]
    public float ChongjiShanghai = 1000;

    float YSShanghai = 0;

    //默认的 跑动动作
    public string MoRenRunACName = "";

    bool isStartChongji = false;

    Vector2 TargetPos;
    

    bool IsChongjiHX2Start = false;

    void GetStart()
    {
        if (MoRenRunACName == "") MoRenRunACName = _gameBody.GetRunName();
        YSShanghai = GetComponent<JN_Date>().atkPower;
        ReSetAll();
        GetComponent<AIAirRunNear>().TurnToPlayer();
        print(" MoRenRunACName "+ MoRenRunACName+ "    ?????????? _gameBody.GetRunName "+ _gameBody.GetRunName());

        IsChongjiHX2Start = true;
        GetTargetPos();
        _gameBody.GetAcMsg(CJ_Begin, 2);
        Audio_CJStart.Play();
        
    }

    void GetTargetPos()
    {
        if(this.transform.position.x> _player.transform.position.x)
        {
            TargetPos = new Vector2(this.transform.position.x - ChongJiDistance, this.transform.position.y-1);
        }
        else
        {
            TargetPos = new Vector2(this.transform.position.x + ChongJiDistance, this.transform.position.y-1);
        }
    }

    bool IsInStartAC = false;
    bool IsChongjiOver = false;

    void ChongjiBegin()
    {
        if (!IsChongjiHX2Start) return;

        if (_roledate.isBeHiting || _roledate.isDie||_gameBody.IsGround|| _gameBody.IsHitWall)
        {
            ReSetAll();
            IsChongjiOver = true;
            return;
        }

        if (IsChongjiOver) return;
        if (isStartChongji)
        {
            _gameBody.isAcing = true;
            if (!IsInStartAC)
            {
                IsInStartAC = true;
                _gameBody.isAcing = true;
                
                _gameBody.RunACChange(CJ_Start);
                _gameBody.GetDB().animation.FadeIn(CJ_Start, 0.2f);
                //_gameBody.GetDB().animation.GotoAndPlayByTime(CJ_Start, 0);
                //print("  进入 飞行 冲击动作  "+ CJ_Start+"   是否包含该动作 "+ _gameBody.GetDB().animation.HasAnimation(CJ_Start)+"    当前动作？？？  "+ _gameBody.GetDB().animation.lastAnimationName);

                Lizi_Huoyan.Play();
                ChongJiHitKuai.SetActive(true);
                SelfHitKuai.SetActive(false);

                //伤害变动
                GetComponent<JN_Date>().atkPower = ChongjiShanghai;
            }
            if (GetComponent<AIAirRunNear>().ZhijieMoveToPoint(TargetPos, 1, MinSpeed, false, true, MaxSpeed))
            {
                //还原到 站立动作
                _gameBody.RunACChange(MoRenRunACName);
                print(" 还原的 跑动动作是啥？？？？？？？？？MoRenRunACName    " + MoRenRunACName);
                _gameBody.isAcing = false;
                
                _gameBody.GetAcMsg(_gameBody.GetStandACName(), 2);
                //ReSetAll();
                //IsChongjiOver = true;
            }

            if (!_gameBody.isAcing)
            {
                ReSetAll();
                IsChongjiOver = true;
            }

            return;
        }

        if(_gameBody.GetDB().animation.lastAnimationName == CJ_Begin&&!_gameBody.isAcing)
        {
            isStartChongji = true;
        }
    }

    

    public void GetStart(GameObject gameObj)
    {
        _player = gameObj;
        GetStart();
    }

    public bool IsGetOver()
    {
        return IsChongjiOver;
    }

    public void ReSetAll()
    {
        ChongJiHitKuai.SetActive(false);
        SelfHitKuai.SetActive(true);
        Lizi_Huoyan.Stop();
        IsChongjiOver = false;
        isStartChongji = false;
        IsInStartAC = false;
        IsChongjiHX2Start = false;
        _gameBody.isAcing = false;
        GetComponent<JN_Date>().atkPower = YSShanghai;
        _gameBody.RunACChange(MoRenRunACName);
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameBody = GetComponent<GameBody>();
        _roledate = GetComponent<RoleDate>();
    }

    // Update is called once per frame
    void Update()
    {
        ChongjiBegin();
    }
}
