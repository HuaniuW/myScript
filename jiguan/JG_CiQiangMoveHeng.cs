using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_CiQiangMoveHeng : MonoBehaviour
{
    public GameObject CiQiang;
    public float MoveSpeedX = 0.4f;
    [Header("左边 点")]
    public Transform LPos;
    [Header("右边 点")]
    public Transform RPos;

    public AudioSource AudioZD;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveStart();
    }

    bool IsMoveStart = false;
    void MoveStart()
    {
        if (IsMoveStart)
        {
            if(CiQiang.transform.position.x< RPos.position.x)
            {
                CiQiang.transform.position = new Vector2(CiQiang.transform.position.x+ MoveSpeedX, CiQiang.transform.position.y);
            }
            else
            {
                IsMoveStart = false;
                if (AudioZD) AudioZD.Stop();
                //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "end"), this);
            }
        }
    }


    public void StartMove()
    {
        IsMoveStart = true;
        //print("zhengdonga ！！！！！！！！！！！！！！！！！！");
        //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-10"), this);
        if (AudioZD) AudioZD.Play();
    }
}
