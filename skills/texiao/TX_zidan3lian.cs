using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_zidan3lian : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        team = GetComponent<TX_zidan>().team;
    }

    // Update is called once per frame
    void Update()
    {
        OutZidan();
    }


    public void ResetAll()
    {
        nums = 3;
        jishi = 0;
    }

    float team = 2;
    int nums = 3;
    float jishi = 0;
    float jishiTimes = 0.2f;

    [Header("特效名字")]
    public string TXName = "TX_dianqiu";


    void OutZidan()
    {
        if (nums <= 0) {
            RemoveSelf();
            return;
        } 
        jishi += Time.deltaTime;
        if(jishi>= jishiTimes)
        {
            //出子弹
            jishi = 0;
            //GlobalTools.GetGameObjectByName()
            GameObject o = ObjectPools.GetInstance().SwpanObject2(Resources.Load(TXName) as GameObject);
            o.GetComponent<JN_Date>().team = team;
            o.transform.parent = this.gameObject.transform.parent;
            o.transform.position = this.transform.position;
            nums--;
        }
    }

    public void RemoveSelf()
    {
        gameObject.SetActive(false);
    }
}
