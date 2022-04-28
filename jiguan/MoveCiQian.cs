using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCiQian : MonoBehaviour
{
    [Header("探测地板顶点 碰到后机关停止")]
    public Transform Tcdian;

    //震动 和声音
    [Header("震动 音效")]
    public AudioSource AudioZD;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    bool IsHasZD = false;
    void ZhenDong()
    {
        //GlobalTools.FindObjByName("MainCamera").GetComponent<CameraController>().GetShockZing2();
        if (!IsHasZD)
        {
            IsHasZD = true;
            if (AudioZD) AudioZD.Play();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-10"), this);
        }
    }


    [Header("y移动速度")]
    public float SpeedY = 0.2f;


    //float jishi = 0;


    bool IsMoveStart = false;
    void MoveStart() {
        if (!IsMoveStart) return;
        //jishi += Time.deltaTime;
        //if (jishi>=0.1f)
        //{
        //    jishi = 0;
            
        //}

        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - SpeedY);


    }

    public void StopMove()
    {
        IsMoveStart = false;
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "end"), this);
        if (AudioZD) AudioZD.Stop();
    }


    public void StartMove()
    {
        IsMoveStart = true;
        ZhenDong();
    }


    [Header("地面图层")]
    public LayerMask groundLayer;

    public virtual bool IsHitGround
    {
        get
        {
            if (Tcdian == null) return false;
            Vector2 start = Tcdian.position;
            Vector2 end = new Vector2(start.x, start.y + 0.2f);
            Debug.DrawLine(start, end, Color.red);
            return Physics2D.Linecast(start, end, groundLayer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHitGround)
        {
            StopMove();
            return;
        }
        MoveStart();
    }
}
