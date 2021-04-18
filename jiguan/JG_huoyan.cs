using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.Rendering.LWRP;
//using UnityEngine.Experimental.Rendering.Universal.Light2D;

public class JG_huoyan : MonoBehaviour
{
    //火焰机关 间隔喷发的火焰机关
    public GameObject HitKuai;
    public ParticleSystem huoyan;
    [Header("间隔时间")]
    public float jiangeshijian;
    [Header("喷发持续时间")]
    public float penfashijian;
    [Header("喷发的声音")]
    public AudioSource sounds;


    public UnityEngine.Experimental.Rendering.Universal.Light2D TheLight;
    //public GameObject TheLight;

    TheTimer _TheTimer;

    //开始喷火
    public bool IsPHStart = false;

    //停止喷火 
    public bool IsStop = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!_TheTimer) _TheTimer = GetComponent<TheTimer>();
        if (huoyan) huoyan.Stop();
        HitKuai.SetActive(false);
        TheLight.gameObject.SetActive(false);

    }

    //火焰持续时间回调
    void CallBackHuoYanCX(float n) {
        IsStop = true;
    }


    void CallBackStop(float n)
    {
        IsPHStart = true;
    }

    
    void GetStarting()
    {
        IsPHStart = false;
        HitKuai.SetActive(true);
        TheLight.gameObject.SetActive(true);
        if (huoyan.isStopped) {
            huoyan.Play();
            sounds.Play();
        }
        
        IsStop = false;
        //计时持续时间
        _TheTimer.TimesAdd(penfashijian, CallBackHuoYanCX);
    }

    void GetStoping()
    {
        IsStop = false;
        IsPHStart = false;
        HitKuai.SetActive(false);
        TheLight.gameObject.SetActive(false);
        if (huoyan.isPlaying) {
            huoyan.Stop();
            sounds.Stop();
        }
        
        _TheTimer.TimesAdd(jiangeshijian, CallBackStop);
    }


    // Update is called once per frame
    void Update()
    {
        if (IsPHStart)
        {
            GetStarting();
        }

        if (IsStop)
        {
            GetStoping();
        }
    }
}
