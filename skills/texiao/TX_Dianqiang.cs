using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_Dianqiang : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
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
        _thisX = this.transform.position.x;
        print("_thisX   "+ _thisX);
        ResetAll();
        IsStarting = true;
    }

    bool IsStarting = false;

    float _thisX = 0;

    //移动距离
    float MoveDistance = 16;

    //移动到 目标点后 持续的时间
    float GetToPosDisTimes = 4;
    float DisTimes = 0;

    float MoveSpeed = 0.2f;

    [Header("固定的Y点坐标")]
    public float PosY = 8;


    public bool IsCanMove = true;
    public float DisSelfTimes = 2;
    void DieSelfByTimes()
    {
        DisTimes += Time.deltaTime;
        if (DisTimes >= DisSelfTimes)
        {
            DisTimes = 0;
            RemoveSelf();
        }
        return;
    }





    private void Move()
    {
        if (!IsStarting) return;
        if (Mathf.Abs(this.transform.position.x - _thisX) >= MoveDistance)
        {
            DisTimes += Time.deltaTime;
            if (DisTimes >= GetToPosDisTimes)
            {
                DisTimes = 0;
                RemoveSelf();
            }
            return;
        }
        this.transform.position = new Vector2(this.transform.position.x+ MoveSpeed,this.transform.position.y);
        print("ysX "+_thisX+"  ----   "+   this.transform.position.x+ "    --  MoveSpeed  " + MoveSpeed);
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
    }

    void RemoveSelf()
    {


        //print("////*********************************************************************************dianqiang");
        //print("////*********************************************************************************dianqiang");
        //print("////*********************************************************************************dianqiang 移除");
        //gameObject.SetActive(false);
        ObjectPools.GetInstance().DestoryObject2(this.gameObject);
    }
}
