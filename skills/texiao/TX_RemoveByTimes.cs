using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_RemoveByTimes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RemoveSelfByTimes();
    }

    [Header("移除自身 计时 时间")]
    public int RemoeTimesNum = 20;

    float jishiTimes = 0;

    void RemoveSelfByTimes()
    {
        jishiTimes += Time.deltaTime;
        if (jishiTimes >= 1)
        {
            jishiTimes = 0;
            RemoeTimesNum--;
            if (RemoeTimesNum ==0)
            {
                print("  ---->  销毁自身 ");
                DestroyImmediate(this.gameObject);
            }
        }
    }

}
