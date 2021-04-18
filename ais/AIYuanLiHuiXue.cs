using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIYuanLiHuiXue : MonoBehaviour,ISkill
{
    //当玩家 远离的 时候 进入 回血状态

    //持续性 的 动作  
    [Header("回血动作")]
    public string HxACName = "stand_1";

    [Header("回血 特效播放")]
    public ParticleSystem HuixueTX;

    [Header("回血 持续时间")]
    public float Hxtimes = 0;

    [Header("每秒回血量")]
    public float MeiMiaoHX = 300;

    [Header("远离回血的音效")]
    public AudioSource SkillAudio;

    //TheTimer _theTimer;

    // Start is called before the first frame update
    void Start()
    {
        //if (!_theTimer) _theTimer = GetComponent<TheTimer>();
        if (HuixueTX) HuixueTX.Stop();
        _roleDate = GetComponent<RoleDate>();
    }
    RoleDate _roleDate;

    private void OnEnable()
    {
        isResetAll = false;
    }

    public void GetStart(GameObject obj)
    {
        //_theTimer.ContinuouslyTimesAdd(Hxtimes,1,callBack);
        //_theTimer.GetContinuouslyTimesStart();
        isStarting = true;
        //特效播放
        HuixueTX.Play();
        //持续动作
        GetHXAC();
        _roleDate.addYZ(10000);
        if (SkillAudio) SkillAudio.Play();
        IsStartHuixue = true;
    }


    float JishiNums = 0;
    bool IsStartHuixue = false;
    float ChiXuZongShijian = 0;

    void HuiXue()
    {
        if (!IsStartHuixue) return;
        JishiNums += Time.deltaTime;
        ChiXuZongShijian+= Time.deltaTime;
        if (JishiNums >= 1)
        {
            JishiNums = 0;
            _roleDate.live += MeiMiaoHX;
        }
        if(ChiXuZongShijian>= Hxtimes|| _roleDate.live>= _roleDate.maxLive)
        {
            ChiXuZongShijian = 0;
            IsStartHuixue = false;
            ReSetAll();
        }
    }

    bool isResetAll = false;
    public void ReSetAll()
    {


        _roleDate.hfYZ(10000);
        GetComponent<GameBody>().isAcing = false;
        HuixueTX.Stop();
        //_theTimer.StopAllCoroutines();
        //结束
        isStarting = false;
        JishiNums = 0;
        ChiXuZongShijian = 0;
        IsStartHuixue = false;
    }

    void callBack(float nums)
    {
        if(nums == 0)
        {
            ReSetAll();
            return;
        }

        _roleDate.live += MeiMiaoHX;
        
    }

    bool isStarting = false;
    public bool IsGetOver()
    {
        return !isStarting;
    }


    // Update is called once per frame
    void Update()
    {
        if (_roleDate.isDie)
        {
            if (!isResetAll)
            {
                isResetAll = true;
                ReSetAll();
            }
            
            return;
        }

        GetHXAC();
        HuiXue();
    }

    void GetHXAC()
    {
        if (!isStarting)
        {
            GetComponent<GameBody>().isAcing = false;
            return;
        }
        if (GetComponent<GameBody>().GetDB().animation.lastAnimationName != HxACName)
        {
            GetComponent<GameBody>().GetAcMsg(HxACName);
        }
    }
}
