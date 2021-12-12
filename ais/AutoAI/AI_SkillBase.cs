using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class AI_SkillBase : MonoBehaviour,ISkill,ISkillBuchong
{
    protected float StartTimes = 0;
    protected float OverTimes = 0;

    [Header("技能开始 的延迟时间")]
    public float StartDelayTimes = 0;
    [Header("技能结束后 的延迟时间")]
    public float OverDelayTimes = 0;

    [Header("起手粒子特效显示")]
    public ParticleSystem StartParticle;

    bool IsStartParticle = false;

    [Header("招式的名字 用来判断 接收事件")]
    public string ZSName = "";

    [Header("释放特效的名字")]
    public string TXName = "";

    [Header("是否要 设定 ScaleX  例如子弹什么的 不需要改变scaleX")]
    public bool IsNeedSetScaleX = true;


    [Header("技能释放点 1")]
    public UnityEngine.Transform zidanDian1;
    [Header("技能释放点 2")]
    public UnityEngine.Transform zidanDian2;

    [Header("技能发射点 num")]
    public int FasheiPointNum = 1;

    [Header("骨骼动画 慢动作 速率   不要用  不明bug ")]
    public float ACScaleNums = 0.2f;
    [Header("骨骼动画 慢动作 速率 开始时间  不要用  不明bug ")]
    public float ACScaleStartTimes = 0;

    [Header("技能 开始时候 声音 ")]
    public AudioSource StartAudio;

    [Header("技能 释放时候的声音 声音 ")]
    public AudioSource SkillOutAudio;


    [Header("攻击距离 如果默认为0 就不用移动 直接 发起攻击*** 如果是 空中的怪 则另算 距离 ")]
    public float AtkDistances = 0;
    bool IsInAtkDistances = false;

    protected GameBody _gameBody;
    protected RoleDate _roleDate;
    protected bool _isGetOver = true;

    // Start is called before the first frame update
    void Start()
    {
        _gameBody = GetComponent<GameBody>();
        _roleDate = GetComponent<RoleDate>();
        _gameBody.GetDB().AddDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);

        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.ZD_SKILL_SHOW, ZDSkillShow);
        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.ZD_SKILL_OVER, ZDSkillOver);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.ZD_SKILL_OVER_ALL, ZDSkillOverAll);
        //print(" zd **********************  正厅了 ");
        TheStart();
    }

    protected virtual void TheStart()
    {

    }

    protected virtual void OnDisable()
    {
        if (_gameBody)
        {
            _gameBody.GetDB().RemoveDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
            ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.ZD_SKILL_SHOW, ZDSkillShow);
            //ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.ZD_SKILL_OVER, ZDSkillOver);
            ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.ZD_SKILL_OVER_ALL, ZDSkillOverAll);
        }
        
    }


    protected string ZDAIName = "";
    protected string GetZSName = "";
    protected virtual void ZDSkillShow(UEvent e)
    {
        GetZSName = "";
        string[] strArr = e.eventParams.ToString().Split('@');
        ZDAIName = strArr[1];
        string id = strArr[0];
        GetZSName = strArr[1].Split('_')[1];

        //print("zd 自动技能 释放 接收 参数 " + e.eventParams.ToString() + "   本技能名字  " + ZSName+ " GetZSName   "+ GetZSName);

        if (id == this.gameObject.GetInstanceID().ToString()&& GetZSName == ZSName)
        {
            //print("zd 发动技能  " + ZSName);
            GetStart(null);
        }
    }

    protected void ZDSkillOver(UEvent e)
    {
        ReSetAll();
        _isGetOver = true;
    }

    protected void ZDSkillOverAll(UEvent e)
    {
        ReSetAll();
        _isGetOver = true;
    }



    bool IsSkillShowOut = false;
    [Header("技能动作名字")]
    public string ACName = "skill_1";

    protected bool _isGetStart = false;
    public virtual void GetStart(GameObject gameObj)
    {
        ReSetAll();
        if (StartAudio) StartAudio.Play();
        //print("zd getStart *****************************************************************自动技能开始");
        _isGetStart = true;
        GetTheStart();
    }


    protected virtual void GetTheStart()
    {

    }
    


    bool IsZhuangxiang = false;

    protected GameObject _player;

    // Update is called once per frame
    void Update()
    {
        if (_isGetOver) return;
        if (_roleDate.isDie || _roleDate.isBeHiting)
        {
            ReSetAll();
            TheSkillOver();
            
            _isGetOver = true;
            //_gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
            print(this.name +"    ----over !!! ");
            
            return;
        }

        if(_player == null)
        {
            _player = GlobalTools.FindObjByName("player");
        }

        if (_player == null) return;

        if (AtkDistances != 0&& !IsInAtkDistances)
        {
            if (GetComponent<AIAirBase>())
            {
                //print("金利来的 是 空中的吗？？？？？");
                //if (GetComponent<AIAirBase>().NearRoleInDistance(AtkDistances))
                //{
                //    if (!IsInAtkDistances)
                //    {
                //        IsInAtkDistances = true;
                //        GetComponent<AIBase>().ZhuanXiang();
                //    }
                //}

                if (GetComponent<AIAirRunNear>().ZhijieMoveToPoint(_player.transform.position, AtkDistances,0.5f))
                {
                    if (!IsInAtkDistances)
                    {
                        IsInAtkDistances = true;
                        GetComponent<AIBase>().ZhuanXiang();
                    }
                }


            }
            else
            {
                if (GetComponent<AIBase>() && GetComponent<AIBase>().NearRoleInDistance(AtkDistances))
                {
                    if (!IsInAtkDistances)
                    {
                        IsInAtkDistances = true;
                        GetComponent<AIBase>().ZhuanXiang();
                    }
                }
            }


           
        }
        else
        {
            if (!IsZhuangxiang)
            {
                IsZhuangxiang = true;
                if (GetComponent<AIBase>().thePlayer.transform.position.x > this.gameObject.transform.position.x)
                {
                    GetComponent<GameBody>().TurnRight();
                }
                else
                {
                    GetComponent<GameBody>().TurnLeft();
                }
               
            }
            SkillStarting();
        }


       
    }



    protected virtual void ChixuSkillStarting()
    {

    }


    protected bool IsStopSelf = false;
    //如果是 特殊的 技能 只能在 ChixuSkillStarting 单独处理完成 就为true 自己在子类处理
    protected bool IsSpeAISkill = false;

    //有AC有动作事件的 技能释放
    protected void SkillStarting()
    {
        ChixuSkillStarting();

        if (IsSpeAISkill) return;

        if (!IsStopSelf)
        {
            IsStopSelf = true;
            _gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
        }

        //print("  进来没  发动技能！！！！ ");
        if (_isGetStart)
        {
            if (StartParticle != null &&!IsStartParticle)
            {
                IsStartParticle = true;
                //print("zd qs 播放 起手特效");
                StartParticle.gameObject.SetActive(true);
                StartParticle.Play();
            }

            

            if (IsStartDelayOver())
            {
                if (!IsSkillShowOut)
                {
                    IsSkillShowOut = true;

                    //print("zd acName:    "+ACName);
                    if (ACName != "")
                    {
                        //print("zd*****************************************播放 技能动作！！！！" + ACName);
                        _gameBody.isAcing = true;
                        //_gameBody.GetDB().animation.GotoAndPlayByFrame(ACName, 0, 1);
                        _gameBody.GetDB().animation.FadeIn(ACName,0.2f,1);
                        //_gameBody.GetAcMsg(ACName);
                        //print(" zd    当前播放的技能动作  "+ _gameBody.GetDB().animation.lastAnimationName);
                    }
                    else
                    {
                        
                        ACSkillShowOut();
                    }

                }

                if (ACName != "")
                {
                    //print("zd   "+_gameBody.GetDB().animationName + "   ZD  ****** over   " + ACName + "    ??  " + _gameBody.GetDB().animation.lastAnimationState.totalTime + "    " + _gameBody.GetDB().animation.lastAnimationState._actionTimeline.currentTime+ " ACScaleStartTimes   "+ ACScaleStartTimes);
                    if (ACScaleStartTimes != 0 && _gameBody.GetDB().animation.lastAnimationState._actionTimeline.currentTime >= ACScaleStartTimes)
                    {
                        _gameBody.GetDB().animation.lastAnimationState.timeScale = ACScaleNums;
                    }




                    if (_gameBody.GetDB().animation.lastAnimationName == ACName && _gameBody.GetDB().animation.lastAnimationState.isCompleted)
                    {
                        _gameBody.GetDB().animation.lastAnimationState.timeScale = 1;
                        if (!IsOverDelayOver())
                        {
                            //print( OverDelayTimes+ " ----OVERTIMES----  "+OverTimes);
                            //print("  _gameBody.GetDB().animation.lastAnimationName "+ _gameBody.GetDB().animation.lastAnimationName);
                            //_gameBody.GetDB().animation.Stop();
                        }
                        else
                        {
                            _gameBody.isAcing = false;
                            TheSkillOver();
                        }


                    }
                    else
                    {
                        //print(ACName + "  zd 技能动作未播完  " + _gameBody.GetDB().animation.lastAnimationName + "    _gameBody.GetDB().animation.lastAnimationState._actionTimeline.currentTime   " + _gameBody.GetDB().animation.lastAnimationState._actionTimeline.currentTime+ " _gameBody.isAcing  "+ _gameBody.isAcing);
                        if (ACName!=""&& _gameBody.GetDB().animation.lastAnimationName!= ACName)
                        {
                            //_gameBody.GetDB().animation.GotoAndPlayByFrame(ACName);
                            print("zd >>>>>>>>>>>>>>>>>>>> 强行修正！！！！！   不知道哪里回复到站立的  ");
                            ReSetAll();
                            TheSkillOver();
                        }
                        
                    }
                }
                else
                {
                    if (IsOverDelayOver())
                    {

                        TheSkillOver();
                    }
                }
            }
            else
            {
                if (ACName != "")
                {
                    if (!IsQishouYanchi)
                    {
                        IsQishouYanchi = true;
                        //_gameBody.isAcing = true;
                        //_gameBody.GetDB().animation.FadeIn(ACName, 1, 1);
                        //如果用了 这里 在动作进入后  进入攻击 又会再进一次  导致动画穿帮
                    }
                   
                }
            }
        }
    }

    protected bool IsQishouYanchi = false;

    protected void TheSkillOver()
    {
        OtherOver();
        if (StartAudio) StartAudio.Stop();
        if (StartParticle != null) {
            StartParticle.Stop();
            StartParticle.gameObject.SetActive(false);
        }
        
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ZD_SKILL_OVER, this.gameObject.GetInstanceID()), this);
        _isGetOver = true;
    }


    protected bool IsACSkillShowOut = false;
    protected virtual void ACSkillShowOut()
    {
        IsACSkillShowOut = true;
        print(" dongzuo name shijian **********************ac    "+TXName);
        //GetComponent<ShowOutSkill>().ShowOutSkillByName(TXName, true);

        GameObject skillObj = Resources.Load(TXName) as GameObject;


        if (skillObj == null)
        {
            print("  skillObj = null  ");
            //Time.timeScale = 0;
            return;
        }

        if (SkillOutAudio) SkillOutAudio.Play();

        GameObject skill = ObjectPools.GetInstance().SwpanObject2(skillObj);
        skill.gameObject.SetActive(true);
        skill.name = TXName;
        if (skill.GetComponent<LiziJNControl>())
        {
            skill.GetComponent<LiziJNControl>().GetLiziObj().GetComponent<JN_base>().atkObj = this.gameObject;
            
        }

        if (FasheiPointNum == 1)
        {
            skill.transform.position = zidanDian1.position;
        }
        else if (FasheiPointNum == 2)
        {
            skill.transform.position = zidanDian2.position;
        }
        if (IsNeedSetScaleX)
        {
            skill.transform.localScale = this.gameObject.transform.localScale;
        }
        
        skill.transform.parent = this.gameObject.transform.parent;
        

        //_gameBody.GetDB().animation.lastAnimationState.timeScale = 0.1f;
        //这里要实现 JNDate 赋值
    }


    protected virtual void ShowACTX(string type, EventObject eventObject)
    {
        //print(type+"  ???????   "+ eventObject.name);
        if (type == EventObject.FRAME_EVENT)
        {

            if (eventObject.name == "ac")
            {
                print("GetZSName>   "+ GetZSName+ "  ZSName   "+ ZSName);
                if(GetZSName == ZSName) ACSkillShowOut();

            }

            if (eventObject.name == "zd" || eventObject.name == "zd2")
            {
                //闪一下点特效
                GameObject shanGuang = ObjectPools.GetInstance().SwpanObject2(Resources.Load("TX_zidan1shan") as GameObject);  //GlobalTools.GetGameObjectByName("TX_zidan1shan");
                shanGuang.transform.position = zidanDian1.position;

                //判断子弹类型 和 动作类型
                //zidan_1   zidan_2
                //zidan_1_1       第三个1 是动作  atk_zd_+ '1'   如果没有就是 1
                //1.普通子弹  2.高速子弹 3.跟踪子弹 4.3连弹  5.直线弹


                //GameObject zidan = ObjectPools.GetInstance().SwpanObject2(Resources.Load(ZDName) as GameObject);
                //zidan.transform.position = zidanDian.position;
                //zidan.transform.localScale = this.transform.localScale;
                //放出子弹 子弹方向   点位置 - 目标位置+ -Y
                //print("发射子弹！！！！！！！！！！！！！！！！！！");
            }
        }
    }

    public virtual void ReSetAll()
    {
        _isGetOver = false;
        _isGetStart = false;
        IsSkillShowOut = false;
        IsQishouYanchi = false;
        IsInAtkDistances = false;
        StartTimes = 0;
        OverTimes = 0;
        IsStartParticle = false;
        IsZhuangxiang = false;
        IsStopSelf = false;
        IsACSkillShowOut = false;
    }



    public virtual bool IsGetOver()
    {
        return _isGetOver;
    }

    public virtual bool IsOverDelayOver()
    {
        if (OverDelayTimes == 0) return true;
        OverTimes += Time.deltaTime;
        if (OverTimes >= OverDelayTimes) return true;
        return false;
    }

    public virtual bool IsStartDelayOver()
    {
        if (StartDelayTimes == 0) return true;
        //print(" 起手计时  "+ StartTimes+"     -------delay   "+ StartDelayTimes);
        StartTimes += Time.deltaTime;
        if (StartTimes >= StartDelayTimes) return true;
        return false;
    }




    protected virtual void OtherOver()
    {

    }



}
