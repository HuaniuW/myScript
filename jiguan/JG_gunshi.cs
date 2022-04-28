using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_gunshi : MonoBehaviour {
    public GameObject yshitou;
    public ParticleSystem _yanmu;
    public AudioSource zdSound;
    public Transform jianceObj;
    //第一次落地 撞击音效
    public AudioSource hitSound;

    public Transform endYObj;

    [Header("地面图层")]
    public LayerMask groundLayer;

    float ydistance = 0;
    // Use this for initialization
    void Start () {
        if (jianceObj)
        {
            ydistance = yshitou.transform.position.y - jianceObj.position.y;
        }
        yshitou.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public bool grounded;
    public virtual bool IsGround
    {
        get
        {
            Vector2 start = jianceObj.position;
            Vector2 end = new Vector2(start.x, start.y - 1f);
            Debug.DrawLine(start, end, Color.red);
            grounded = Physics2D.Linecast(start, end, groundLayer);
            return grounded;
        }
    }

    bool isStart = false;
    public void GetStart()
    {
        isStart = true;
        yshitou.GetComponent<Rigidbody2D>().gravityScale = 10;
    }


    bool isFirstHitDiban = false;
    bool isStartShock = false;

    // Update is called once per frame
    void Update () {
        if (!isStart) return;



        if (yshitou) {
            jianceObj.position = new Vector2(yshitou.transform.position.x, yshitou.transform.position.y - ydistance);
            /* yshitou.GetComponent<Rigidbody2D>().AddForce(new Vector2(3000f, 0));
             print(yshitou.GetComponent<Rigidbody2D>().velocity.x);
             if (yshitou.GetComponent<Rigidbody2D>().velocity.x < 12)
             {

             }*/

            if (yshitou.GetComponent<Rigidbody2D>().velocity.x < 16)
            {
                yshitou.GetComponent<Rigidbody2D>().velocity = new Vector2(16, yshitou.GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                yshitou.GetComponent<Rigidbody2D>().AddForce(new Vector2(2000, 0));
            }
            print(yshitou.GetComponent<Rigidbody2D>().velocity.x);

            if (yshitou.GetComponent<Rigidbody2D>().velocity.x > 20)
            {
                yshitou.GetComponent<Rigidbody2D>().velocity = new Vector2(20, yshitou.GetComponent<Rigidbody2D>().velocity.y);
            }


            
            if (IsGround)
            {
                if (!isStartShock)
                {
                    isStartShock = true;
                    ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-10000"), this);
                    //玩家 进入 高速跑
                    ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_RUN_AC_2, ""), this);
                }
                YanmuPlay();
                ZdSoundPlay();
            }
            else
            {
                if (isStartShock)
                {
                    isStartShock = false;
                    ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "end"), this);
                    YanmuStop();
                    ZdSoundStop();
                }
            }
        }

        //print(this.yshitou.transform.position.y);
        if (this.yshitou.transform.position.y<=endYObj.position.y)
        {
            Destroy(this.gameObject);
        }
        
    }

    void YanmuPlay()
    {
        if (_yanmu) {
            _yanmu.Play();
            _yanmu.gameObject.transform.position = jianceObj.transform.position;
        }
        
    }


    void YanmuStop()
    {
        if (_yanmu) {
            _yanmu.Stop();
            _yanmu.gameObject.transform.position = jianceObj.transform.position;
        }
    }


    void ZdSoundPlay()
    {
        if (zdSound&&!zdSound.isPlaying) zdSound.Play();
    }

    void ZdSoundStop()
    {
        if (zdSound && zdSound.isPlaying) zdSound.Stop();
    }

}
