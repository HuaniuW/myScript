using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_chufaCi : MonoBehaviour {
    //刺
    public GameObject jg_ci;
    //粒子效果
    public ParticleSystem _yanmu;
    //刺出声音
    public AudioSource ciSound_1;
    public AudioSource ciSound_2;

    //刺的移动终点位置点
    public Transform endPos;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      

        if (theTime.isStart && theTime.IsPauseTimeOver())
        {
            isStart = true;
            YanmuStop();
            SoundPlay();
        }

        if (isStart)
        {
            CiMoveOut();
        }
    }

    TheTimer theTime = new TheTimer();
    public float ciTmes = 0.5f;
    float ciSpeed = 0.5f;
    bool isStart = false;
    bool isOver = false;

    void CiMoveOut()
    {
        if (jg_ci.transform.position.y < endPos.position.y)
        {
            print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            float _y = jg_ci.transform.position.y + ciSpeed;
            jg_ci.transform.position = new Vector3(jg_ci.transform.position.x, _y + jg_ci.transform.position.z);
        }
        else
        {
            isStart = false;
            
        }
    }


    void BeStart()
    {
        //烟幕 出现0.5F后 出刺

        if (isOver) return;
        isOver = true;
        Yanmu();
        theTime.GetStopByTime(ciTmes);
    }


    void Yanmu()
    {
        if (_yanmu) _yanmu.Play();
    }

    void YanmuStop()
    {
        if (_yanmu) _yanmu.Stop();
    }

    void SoundPlay()
    {
        //随机选声音
        int n = (int)UnityEngine.Random.Range(0, 100);
        if (n < 50)
        {
            ciSound_1.Play();
        }
        else
        {
            ciSound_2.Play();
        }
    }


    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.tag == "Player")
        {
            print(" 刺 chufa !!!!!!!");
            BeStart();
        }
    }
}
