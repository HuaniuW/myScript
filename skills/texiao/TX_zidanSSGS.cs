using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_zidanSSGS : TX_zidan
{
    //上升的 高速子弹
    // Start is called before the first frame update
    void Start()
    {
        print("子文档 start!!");
    }



    void ReSetAll()
    {
        isUpStart = false;
        this.GetComponent<Rigidbody2D>().gravityScale = 4;
        isReset = false;
        isFaShe = false;
        testN = 0;
    }


    private void OnEnable()
    {
        ReSetAll();
    }

    public float SpeedUp = 15;
    public float SpeedGS = 40;


    bool isUpStart = false;
    public void GetUpStart()
    {
        if (!isUpStart)
        {
            //print(">>>>>>>>>>>>>>>>>>>>>>>>   高速子弹！！！ ");
            isUpStart = true;
            float upPos = 2 + GlobalTools.GetRandomDistanceNums(2);
            Vector2 v2 = new Vector2(this.transform.position.x, this.transform.position.y + upPos);
            GetDiretionByV2(v2,SpeedUp);
        }
    }

    public override void GetZiDanStart()
    {
        GetUpStart();
    }

    [Header("是否是跟踪子弹")]
    public bool IsGZZiDan = false;

    int testN = 0;
    [Header("数值越大 跟踪能力越强  可以模拟跟踪子弹 ")]
    public int MaxTestN = 0;
    void JiluSpeed()
    {
        
        if (testN > MaxTestN) return;


        if (!isFaShe)
        {
            if (this.GetComponent<Rigidbody2D>().velocity.y < 0)
            {

                //重力清0 速度清0  并且保持播放？
                if (!isReset)
                {
                    isReset = true;
                    ReSetSpeedAndGravity();
                    
                }

               
                
                
                
                //print("??? ------------   testN    "+testN);
                //设置飞行速度
                SetZDSpeed(SpeedGS);
                //print("player  ???  "+_player);
                if (!_player)
                {                    
                    _player = GlobalTools.FindObjByName("player");
                    //return;
                }


                isFaShe = true;
                fire();
            }
        }
    }


    void GenZong()
    {
        if (!_player) return;
        //print("??? ------------   testN    " + testN);
        if (testN < MaxTestN) {
            testN++;
            GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByPostion(_player.transform.position, this.transform.position, speeds);
        }
        
    }

    bool isReset = false;
    void ReSetSpeedAndGravity()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUpStart) {
            JiluSpeed();
        }
        else
        {
            if (IsGZZiDan) GenZong();
        }
        
        
    }
}
