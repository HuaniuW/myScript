using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_FD : MonoBehaviour {
    Vector2 startPos;
    Vector2 thisPos;
    float fdSpeed = 3;
    bool _isStartFly = false;
    public GameObject feidao;
    Vector3 _rotation;
    Vector2 scaleFD;
    public AudioSource diufeidao;
    //原地放飞刀的声音
    public AudioSource diufeidao2;
    //飞刀击中地板的声音
    public AudioSource diufeidao3;
    // Use this for initialization

    [Header("侦测地板的射线起点")]
    public UnityEngine.Transform groundCheck;

    [Header("感应与面前墙的距离")]
    [Range(0, 1)]
    public float distance = 0.13f;

    [Header("地面图层")]
    public LayerMask groundLayer;

    bool isHitWall = false;

    public virtual bool IsHitWall
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            isHitWall = Physics2D.Linecast(start, end, groundLayer);
            return isHitWall;
        }
    }


    void Start () {
        scaleFD = feidao.transform.localScale;
        _rotation = feidao.transform.eulerAngles;
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.DISTORY_FEIDAO, ShouFeidao);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_OVER, GameOver);
    }

    void GameOver(UEvent e)
    {
        OnDistory();
    }

    void OnEnable()
    {
        ShowFeiDao();
    }

    void OnDistory()
    {
        print("我特么被销毁了！！！！！！！！！！！！！！！！！！！");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DISTORY_FEIDAO, ShouFeidao);
    }

    void ShouFeidao(UEvent e)
    {
        //Globals.feidao = null;
        //GetComponent<JN_base>().DisObj();
    }

    public void ShowFeiDao()
    {
        //获取上下左右方向

        _isStartFly = true;
        //print("--------------------------------------------------------------------------------------->this.transform.position   " + this.transform.position+ "    startPos   "+ startPos);
    }

    // Update is called once per frame
    void Update () {
        GetMove();
    }

    /**
    bool isHitTheWall = false;
    void FixedUpdate()
    {
        isHitTheWall = IsHitWall;
    }*/

    bool _isUp = false;
    bool _isDown = false;

    void GetMove()
    {
        if (_isStartFly)
        {
            _isStartFly = false;
            _isUp = false;
            _isDown = false;
            //thisPos = this.transform.position;
            startPos = this.transform.position;
            feidao.transform.eulerAngles = _rotation;
            feidao.transform.localScale = scaleFD;

            if (GetComponent<JN_base>().atkObj != null) {
                //print("------------------------------------------------------------>  " + GetComponent<JN_base>().atkObj.transform.localScale.x);
                if (Globals.feidaoFX == "")
                {
                    if (GetComponent<JN_base>().atkObj.transform.localScale.x > 0)
                    {
                        //print("???  左 ");
                        //this.transform.localScale = new Vector2(1, 1);
                        fdSpeed = 1;
                    }
                    else
                    {
                        //print("???  右 ");
                        fdSpeed = -1;
                        feidao.transform.localScale = new Vector2(-scaleFD.x, -scaleFD.y);
                        //this.transform.localScale = new Vector2(-1, 1);
                    }
                }
                else if (Globals.feidaoFX == "up") {
                    fdSpeed = 1;
                    _isUp = true;
                    feidao.transform.eulerAngles = new Vector3(0,0,-111);
                    //print("Globals.feidaoFX     "+ Globals.feidaoFX);
                } else if (Globals.feidaoFX == "down") {
                    fdSpeed = -1;
                    _isDown = true;
                    feidao.transform.eulerAngles = new Vector3(0, 0, -291);
                    //print("Globals.feidaoFX     " + Globals.feidaoFX);
                }
                
            }

            if(Globals.feidaoFX == "down")
            {
                if (GetComponent<JN_base>().atkObj.GetComponent<GameBody>().isInAiring) {
                    if (diufeidao) diufeidao.Play();
                }
                else
                {
                    if (diufeidao2) diufeidao2.Play();
                }
            }
            else
            {
                if(diufeidao) diufeidao.Play();
            }
        }

        if (IsHitWall || Mathf.Abs(this.transform.position.x - startPos.x) >= 10|| Mathf.Abs(this.transform.position.y - startPos.y) >= 10) {
            //记录位置
            if (Globals.feidao == null) {
                Globals.feidao = this.gameObject;
            }
            
        }
        else
        {
            //print(startPos+"    ??    "+this.transform.position);
           


            if (_isUp|| _isDown)
            {
                if(_isDown && !GetComponent<JN_base>().atkObj.GetComponent<GameBody>().isInAiring)
                {
                    //这里是 在地面上 自由落体的飞刀
                    return;
                }
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+fdSpeed, this.transform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x - fdSpeed, this.transform.position.y, this.transform.position.z);
            }
            
        }

            
    }
}
