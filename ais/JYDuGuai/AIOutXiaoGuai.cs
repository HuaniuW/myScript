using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOutXiaoGuai : MonoBehaviour
{

    //吐小绿怪  单一方向 的小绿怪 撞墙 回身
    //声音
    [Header("生成怪时 发出的声音")]
    public AudioSource Audio_GuaiOut;
    //特效 名字
    [Header("生成怪的 特效")]
    public ParticleSystem TX_OutGuai;
    //出怪名字
    [Header("生成怪的名字")]
    public string OutGuaiName = "";
    //出怪位置
    [Header("出怪的位置")]
    public Transform GuaiOutPos;


    RoleDate _roleDate;


    // Start is called before the first frame update
    void Start()
    {
        _roleDate = GetComponent<RoleDate>();
    }

    [Header("出怪的间隔时间")]
    public float GuaiOutTime = 4;
    float Jishi_GuaiOut = 0;
    void JishiGuaiOut()
    {
        if (_roleDate.isDie || _roleDate.isBeHiting) return;

        Jishi_GuaiOut += Time.deltaTime;
        if (Jishi_GuaiOut >= GuaiOutTime)
        {
            Jishi_GuaiOut = 0;
            GuaiOut();
        }
    }


    public void GuaiOut()
    {
        GameObject guai = Resources.Load(OutGuaiName) as GameObject;
        if (guai != null)
        {
            guai = ObjectPools.GetInstance().SwpanObject2(guai);
            guai.transform.position = GuaiOutPos.position;
            guai.name = OutGuaiName;

            if (Audio_GuaiOut) Audio_GuaiOut.Play();
            if(TX_OutGuai) TX_OutGuai.Play();

        }
        

    }


    // Update is called once per frame
    void Update()
    {
        
        JishiGuaiOut();
    }
}
