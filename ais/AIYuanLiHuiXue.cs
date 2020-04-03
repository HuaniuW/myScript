using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIYuanLiHuiXue : MonoBehaviour
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

    TheTimer _theTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (!_theTimer) _theTimer = GetComponent<TheTimer>();
        if (HuixueTX) HuixueTX.Stop();
    }


    private void OnEnable()
    {
        isResetAll = false;
    }

    public void GetStart()
    {
        _theTimer.ContinuouslyTimesAdd(Hxtimes,1,callBack);
        _theTimer.GetContinuouslyTimesStart();
        isStarting = true;
        //特效播放
        HuixueTX.Play();
        //持续动作
        GetHXAC();
        GetComponent<RoleDate>().addYZ(10000);
    }


    bool isResetAll = false;
    void ReSetAll()
    {

        
        GetComponent<RoleDate>().hfYZ(10000);
        GetComponent<GameBody>().isAcing = false;
        HuixueTX.Stop();
        _theTimer.StopAllCoroutines();
        //结束
        isStarting = false;
    }

    void callBack(float nums)
    {
        if(nums == 0)
        {
            ReSetAll();
            return;
        }

        GetComponent<RoleDate>().live += MeiMiaoHX;
        
    }

    bool isStarting = false;
    public bool IsGetOver()
    {
        return !isStarting;
    }


    // Update is called once per frame
    void Update()
    {
        if (GetComponent<RoleDate>().isDie)
        {
            if (!isResetAll)
            {
                isResetAll = true;
                ReSetAll();
            }
            
            return;
        }

        GetHXAC();
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
