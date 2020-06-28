using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_xialuo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Header("震动 声音")]
    public AudioSource AudioZD;

    //下落地板
    // Update is called once per frame
    void Update()
    {
        if (IsXiaLuoBegin)
        {
            jishi += Time.deltaTime;
            if (jishi >= YCMiao)
            {
                IsXiaLuoBegin = false;
                Xialuo();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D Coll)
    {
        if (Coll.collider.tag == "Player") {
            print(" hit!!!!!!!!!! ");
            XiaCheng();
        }
    }



    //冒烟
    [Header("烟幕")]
    public ParticleSystem YanMu;

    bool IsXiaLuoBegin = false;

    //下沉一下
    void XiaCheng()
    {
        //this.transform.position = new Vector2(this.transform.position.x,this.transform.position.y-0.2f);
        if (YanMu) YanMu.Play();
        IsXiaLuoBegin = true;
        if (AudioZD) AudioZD.Play();
    }

    //延迟 几秒后
    [Header("延迟秒数")]
    public float YCMiao = 0.4f;
    float jishi = 0;
    //改变 type 让自由落体
    void Xialuo()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
   
}
