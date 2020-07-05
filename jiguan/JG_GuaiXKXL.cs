using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_GuaiXKXL : MonoBehaviour
{
    //悬空怪 机关  碰到 触发下落
    // Start is called before the first frame update
    [Header("触发机关后 延迟多少时间 下落怪物")]
    public float HitStartTimes = 2f;


    bool IsHasCF = false;

    [Header("悬空怪数组")]
    public List<GameObject> GuaiList = new List<GameObject>() { };
    void Start()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!IsHasCF && collision.transform.tag == "Player")
        {
            //触发延迟 计时
            IsHitJGStart = true;
            IsHasCF = true;
        }
    }

    bool IsHitJGStart = false;


    float jishiqi = 0;
    bool IsYCTimeOver()
    {
        if (jishiqi >= HitStartTimes) return true;
        jishiqi += Time.deltaTime;
        return false;
    }


    // Update is called once per frame
    void Update()
    {
        if (IsHitJGStart)
        {
            if (IsYCTimeOver())
            {
                IsHitJGStart = false;

                for(var i=0;i< GuaiList.Count; i++)
                {
                    GuaiList[i].GetComponent<GameBody>().GetPlayerRigidbody2D().gravityScale = 8;
                }
            }
        }
    }
}
