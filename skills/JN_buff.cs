using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_buff : MonoBehaviour
{
    //buff技能 放在玩家身上显示的 特效 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Header("buff 出现时的声音")]
    public AudioSource buffAudio;

    [Header("buff 持续显示时间")]
    public float Duration = 2;
    float DurationTimes = 0;
    bool IsDuration = false;
    void InDuration()
    {
        if (!IsDuration) return;
        DurationTimes += Time.deltaTime;
        //print("  -----buff持续时间  "+ DurationTimes+"    weizhi "+this.transform.position);
        if (DurationTimes>= Duration)
        {
            ResetAll();
            RemoveSelf();
        }
    }



    protected void ResetAll()
    {
        DurationTimes = 0;
        IsDuration = false;
    }



    protected virtual void GetStart()
    {
        IsDuration = true;
        if (buffAudio) buffAudio.Play();
    }





    private void OnEnable()
    {
        print("buff 技能启动");
        GetStart();
    }



    protected virtual void RemoveSelf()
    {
        ObjectPools.GetInstance().DestoryObject2(this.gameObject);
    }

    private void OnDisable()
    {
        print("buff技能移除 自身");
    }

    // Update is called once per frame
    void Update()
    {
        InDuration();
    }
}
