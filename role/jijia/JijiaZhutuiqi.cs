using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JijiaZhutuiqi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //BaozhaSuipian();
    }


    [Header("助推器 断开 特效")]
    public ParticleSystem TX_Duankai;
    [Header("助推器 断开 ***音效")]
    public AudioSource Audio_Duankai;



    bool IsStartJiaoSD = false;
    float JiaoSD = 30;
    public void SetJiaoSDSpeed()
    {
        IsStartJiaoSD = true;
        JiaoSD = 10 + GlobalTools.GetRandomDistanceNums(20);
        if (TX_Duankai) TX_Duankai.Play();
        if(Audio_Duankai) Audio_Duankai.Play();
        this.transform.parent = this.transform.parent.transform.parent;
        this.GetComponent<Rigidbody2D>().gravityScale = 1;
    }


    public void BaozhaSuipian(float vx = 0)
    {
        //print(" vx "+vx);
        SetJiaoSDSpeed();
        IsStartJiaoSD = true;
        JiaoSD = 50 + GlobalTools.GetRandomDistanceNums(100);
        float __x = 4 + GlobalTools.GetRandomDistanceNums(4);
        if (GlobalTools.GetRandomNum() > 50) {
            __x = -__x;
        }
        else
        {
            __x = GlobalTools.GetRandomDistanceNums(4)+ vx*0.5f;
        }
        float __y = 4 + GlobalTools.GetRandomDistanceNums(4);
        //print("vx---> "+(__x+vx));
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(__x, __y);
    }


    // Update is called once per frame
    void Update()
    {
        if (IsStartJiaoSD) {
            transform.Rotate(new Vector3(0, 0, JiaoSD) * Time.deltaTime);
            if (this.transform.position.y < -30)
            {
                //print("助推器 被删除");
                DestroyImmediate(this.gameObject);
            }
        }
        
    }
}
