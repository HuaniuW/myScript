using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_Dianding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        roleDate = GetComponent<RoleDate>();
    }

    // Update is called once per frame
    void Update()
    {
        GetDie();
        Xuanfu();
        if (!IsStopXuanfu) return;
        Daojishi();
        
    }


    public void GetStart()
    {
        ResetAll();
        CXTX_Dianding.Play();
    }

    [Header("电钉持续特效")]
    public ParticleSystem CXTX_Dianding;


    int XuanfuNum = 1;
    float XuanfuTimes = 0;
    bool IsStopXuanfu = false;

    //出现 后 先悬浮 1秒
    void Xuanfu()
    {

        if (IsStopXuanfu) return;

        XuanfuTimes += Time.deltaTime;
        if(XuanfuTimes>= XuanfuNum)
        {
            XuanfuTimes = 0;
            IsStopXuanfu = true;
        }

        if (IsStopXuanfu)
        {
            GetComponent<Rigidbody2D>().gravityScale = 4;
            return;
        }
    }



    void ResetAll()
    {
        IsDie = false;
        nums = 5;
        jishi = 0;
        XuanfuTimes = 0;
        IsStopXuanfu = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        CXTX_Dianding.Stop();
    }



    //记录 自己是否 存在  不存在 可以再生成 否则 不生成 新的

    [Header("电钉 爆炸 点1")]
    public Transform BZPos1;
    [Header("电钉 爆炸 点2")]
    public Transform BZPos2;


    RoleDate roleDate;
    bool IsDie = false;
    void GetDie()
    {
        if (roleDate.live <= 0)
        {
            if (!IsDie)
            {
                IsDie = true;
               
                //移除自己
                RemoveSelf();
            }
        }
    }

    [Header("爆炸特效 的名字")]
    public string TX_BZName = "TX_zidan1_bz";

    void DieBZ()
    {
        CXTX_Dianding.Stop();
        //GlobalTools.GetGameObjectByName
        GameObject bz1 =  ObjectPools.GetInstance().SwpanObject2(Resources.Load(TX_BZName) as GameObject);
        bz1.transform.position = BZPos1.position;

        GameObject bz2 = ObjectPools.GetInstance().SwpanObject2(Resources.Load(TX_BZName) as GameObject);
        bz2.transform.position = BZPos2.position;

    }

    public void RemoveSelf()
    {
        //显示爆炸
        DieBZ();
        ObjectPools.GetInstance().DestoryObject2(this.gameObject);
    }



    //电击
    [Header("电钉 放电 倒计时 声音1")]
    public AudioSource AudioDing1;

    [Header("电钉 放电 倒计时 声音2")]
    public AudioSource AudioDing2;

    [Header("放电特效")]
    public ParticleSystem TXFangdian;



    int nums = 5;
    float jishi = 0;
    void Daojishi()
    {
        if (IsDie) return;

        if (!IsGround) return;

        jishi += Time.deltaTime;
        if (jishi >= 1)
        {
            jishi = 0;
           
            nums--;

            if (nums == 0)
            {
                //放电
                FangDian();
                nums = 5;
                return;
            }
            else if(nums == 1)
            {
                if(AudioDing2) AudioDing2.Play();
                if(TXFangdian) TXFangdian.Play();
            }
            else
            {
                if(AudioDing1) AudioDing1.Play();
            }

        }
    }


    [Header("底部的 探测点")]
    public Transform DiPos;

    [Header("地面图层 包括机关")]
    public LayerMask groundLayer;


    //在玩家底部是一条短射线 碰到地板说明落到地面 
    public virtual bool IsGround
    {
        get
        {
            //print("groundCheck 是否有这个 变量   "+ groundCheck);
            if (!DiPos) return false;
            Vector2 start = DiPos.position;
            Vector2 end = new Vector2(start.x, start.y - 1);
            Debug.DrawLine(start, end, Color.blue);
            return Physics2D.Linecast(start, end, groundLayer);
        }
    }



    public string DianQiangName = "TX_DianqiangHeng";

    void FangDian()
    {
        GameObject dianqiangHeng = ObjectPools.GetInstance().SwpanObject2(Resources.Load(DianQiangName) as GameObject);
        dianqiangHeng.transform.position = new Vector2(DiPos.position.x, DiPos.position.y+0.6f);
        dianqiangHeng.GetComponent<JN_base>().atkObj = this.GetComponent<JN_base>().atkObj;
        dianqiangHeng.GetComponent<JN_Date>().team = this.GetComponent<JN_Date>().team;
        dianqiangHeng.GetComponent<TX_Dianqiang>().GetStart();
    }


}
