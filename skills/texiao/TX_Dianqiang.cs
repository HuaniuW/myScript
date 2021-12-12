using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_Dianqiang : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Lizi_DianQiang) _liziMain = Lizi_DianQiang.main;
    }

    [Header("电墙粒子")]
    public ParticleSystem Lizi_DianQiang;

    ParticleSystem.MainModule _liziMain;

    // Update is called once per frame
    void Update()
    {
        //print("thisname    "+this.name+ "   IsCanMove "+ IsCanMove);
        if (!IsStarting)
        {
            GetStart();
        }
        if (IsCanMove)
        {
            Move();
        }
        else
        {
            DieSelfByTimes();
        }
        
    }


    private void Awake()
    {
        _thisX = this.transform.position.x;
        _thisY = this.transform.position.y;
    }

    private void OnEnable()
    {
        
        //ResetAll();
        //print("*********************************************************************************dianqiang");
        //print("*********************************************************************************dianqiang");
        //print("*********************************************************************************dianqiang");
        //print("*********************************************************************************dianqiang");
        //print("*********************************************************************************dianqiang");
        //print("_thisX   " + _thisX);
    }

    public void GetStart()
    {
        _thisY = this.transform.position.y;
        _thisX = this.transform.position.x;
        //print("_thisY   "+ _thisY);
        ResetAll();
        IsStarting = true;
        _liziMain = Lizi_DianQiang.main;
        _liziMain.loop = true;

        //print("?????  Lizi_DianQiang   " + Lizi_DianQiang);
        //if (Lizi_DianQiang)
        //{
        //    Lizi_DianQiang.gameObject.SetActive(true);
        //    Lizi_DianQiang.Play();
        //}

    }

    bool IsStarting = false;

    float _thisX = 0;
    float _thisY = 0;

    //移动距离
    [Header("可以移动的距离")]
    public float MoveDistance = 16;

    //移动到 目标点后 持续的时间
    [Header("移动到目标点后 移除自己的计时时间")]
    public float GetToPosDisTimes = 4;
    float DisTimes = 0;

    [Header("移动速度")]
    public float MoveSpeed = 0.2f;

    [Header("固定的Y点坐标")]
    public float PosY = 8;


    public bool IsCanMove = true;
    public float DisSelfTimes = 2;
    void DieSelfByTimes()
    {

        if (IsMoveEnd) {
            RemoveSelfInEnd();
            return;
        }
       

        DisTimes += Time.deltaTime;
        if (DisTimes >= DisSelfTimes)
        {
            DisTimes = 0;
            IsMoveEnd = true;
            //RemoveSelf();
        }

       



        return;
    }


    void RemoveSelfInEnd()
    {
        if (_liziMain.loop)
        {
            print("关闭粒子循环！！！！");
            //停止碰撞快 碰撞
            _liziMain.loop = false;
        }
        if (Lizi_DianQiang && Lizi_DianQiang.isStopped)
        {
            print("  移除。。。。。。。。。。。。。。。。。。。。 ");
            RemoveSelf();
        }
    }



    protected bool IsMoveEnd = false;

    public bool IsMoveY = false;


    private void Move()
    {
        //print("  /////////////////////////////////////////// ");
        if (!IsStarting) return;

        if (IsMoveEnd)
        {
            RemoveSelfInEnd();
            return;
        }


        if (IsMoveY)
        {
            if (Mathf.Abs(this.transform.position.y - _thisY) >= MoveDistance)
            {
                DisTimes += Time.deltaTime;
                if (DisTimes >= GetToPosDisTimes)
                {
                    DisTimes = 0;
                    IsMoveEnd = true;
                    //RemoveSelf();
                }
                return;
            }

            this.transform.position = new Vector2(this.transform.position.x , this.transform.position.y- MoveSpeed);
        }
        else
        {
            if (Mathf.Abs(this.transform.position.x - _thisX) >= MoveDistance)
            {
                DisTimes += Time.deltaTime;
                if (DisTimes >= GetToPosDisTimes)
                {
                    DisTimes = 0;
                    IsMoveEnd = true;
                    //RemoveSelf();
                }
                return;
            }


            this.transform.position = new Vector2(this.transform.position.x + MoveSpeed, this.transform.position.y);
        }
        
        //print("ysX "+_thisX+"  ----   "+   this.transform.position.x+ "    --  MoveSpeed  " + MoveSpeed);
    }

    //设置 速度方向
    public void SetSpeedFX(float _fx)
    {
        MoveSpeed *= _fx;
    }

    void ResetAll()
    {
        DisTimes = 0;
        IsStarting = false;
        MoveSpeed = 0.2f;

        //_liziMain = Lizi_DianQiang.main;
        //_liziMain.loop = true;
        IsMoveEnd = false;
        //if (Lizi_DianQiang) Lizi_DianQiang.GetComponent<ParticleSystem>().loop = true;
    }

    void RemoveSelf()
    {

        IsStarting = false;
        //print("////*********************************************************************************dianqiang");
        //print("////*********************************************************************************dianqiang");
        //print("////*********************************************************************************dianqiang 移除");
        //gameObject.SetActive(false);
        ObjectPools.GetInstance().DestoryObject2(this.gameObject);
    }
}
