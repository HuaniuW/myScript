using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_YanChenC : MonoBehaviour
{

    [Header("控制的烟尘1")]
    public ParticleSystem yanchen1;

    public ParticleSystem ZhuTX;

    [Header("碰撞块")]
    public GameObject HitFK;

    [Header("是否延迟消失")]
    public bool IsYanchiDis = false;

    [Header("在空中 不显示烟尘")]
    public bool IsInAir = false;

    [Header("是否碰到地面 没有碰到地面 不显示烟尘")]
    public bool IsHitGround = true;


    TheTimer _theTime;

    [Header("延迟消失时间")]
    public float yanchiDisTime = 3;


    [Header("碰撞检测点")]
    public Transform groundCheck;

    [Header("地面图层")]
    public LayerMask groundLayer;



    public virtual bool IsGround
    {
        get
        {
            //print("groundCheck 是否有这个 变量   "+ groundCheck);
            if (!groundCheck) return false;
            Vector2 start = groundCheck.position;
            Vector2 end = new Vector2(start.x, start.y - 1);
            Debug.DrawLine(start, end, Color.blue);
            return Physics2D.Linecast(start, end, groundLayer);
        }
    }



    JN_Date _jnDate;
    // Start is called before the first frame update
    void Start()
    {
        //print("  TX_YanChenC!!!!!!!!!!!!!!!!!!!!!!!!! ");
    }

   

    private void OnEnable()
    {
        HitFK.SetActive(true);
        ZhuTX.gameObject.SetActive(true);
        if(!_jnDate) _jnDate = GetComponent<JN_Date>();
        if (!_theTime) _theTime = GetComponent<TheTimer>();
        _theTime.TimesAdd(_jnDate.TXDisTime, DisTX);
        //print("-----------------------> zhutexiao  延迟消失的时间  ");
    }



    void DisTX(float n)
    {
        //print("主特效 消失！");

        //主特效 消失
        ZhuTXAndHitKuaiDis();
        //停止 特效速度运行
        //yanchen1.Stop();

        if(_jnDate._type == "2")
        {
            //停止移动
            GetComponent<JN_base>().StopSD();
        }

        //指定时间销毁
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(gameObject, yanchiDisTime));
    }

    void ZhuTXAndHitKuaiDis()
    {
        HitFK.SetActive(false);
        ZhuTX.gameObject.SetActive(false);
    }

    private void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //判断 是否在空中？  这个不需要判断 只要判断 是否碰撞就行
        if (yanchen1 && IsGround)
        {
            yanchen1.Play();
        }
        else
        {
            yanchen1.Stop();
        }
    }
}
