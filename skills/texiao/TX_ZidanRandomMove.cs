using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_ZidanRandomMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetMove();
    }

    protected GameObject _player;

    private void OnEnable()
    {
        if (!_player) _player = GlobalTools.FindObjByName("player");
        IsSetPos = false;
        jishi = 0;
        //GetComponent<TX_zidan>().IsAtkAuto = false;
        GetRandomPos();
    }

    private void OnDisable()
    {
        IsStart = false;
    }


    bool IsSetPos = false;
    Vector2 RanTargetPos = new Vector2(1000,1000);
    Vector2 thisPos = new Vector2(1000,1000);
    float RanMoveSpeed = 0.1f;
    Vector2 v2 = Vector2.zero;
    void GetRandomPos()
    {
        if (!IsSetPos)
        {
            IsSetPos = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            float __x = GlobalTools.GetRandomNum() > 50 ? this.gameObject.transform.position.x - 2 - GlobalTools.GetRandomDistanceNums(2) : this.gameObject.transform.position.x + 2 + GlobalTools.GetRandomDistanceNums(2);
            float __y = GlobalTools.GetRandomNum() > 50 ? this.gameObject.transform.position.y - 2 - GlobalTools.GetRandomDistanceNums(2) : this.gameObject.transform.position.y + 2 + GlobalTools.GetRandomDistanceNums(2);
            RanTargetPos = new Vector2(__x,__y);
            thisPos = this.transform.position;
            RanMoveSpeed = 0.4f + GlobalTools.GetRandomDistanceNums(1f);

            v2 = (RanTargetPos - thisPos) * RanMoveSpeed;
            //GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByPostion(RanTargetPos, this.transform.position, RanMoveSpeed); 
            //print("   ");

            IsStart = true;

        }
    }

    float jishiTime = 0.5f;
    float jishi = 0;

    bool IsStart = false;

    
    void GetMove()
    {
        if (!IsStart) return;
        GetComponent<Rigidbody2D>().velocity = v2;
        jishi += Time.deltaTime;
        print("jishi:  "+jishi+"  siduV2   "+v2+ "   IsStart  "+ IsStart);
        if (jishi>=jishiTime)
        {
            IsStart = false;
            print("******************************************* 计时结束 ");
            jishi = 0;

            //Time.timeScale = 0;
            //GetComponent<TX_zidan>().IsAtkAuto = true;
            if (_player&&this.gameObject.activeSelf) GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByPostion(_player.transform.position, this.transform.position, 20);
        }
    }

}
