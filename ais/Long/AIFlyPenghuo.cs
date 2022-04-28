using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlyPenghuo : MonoBehaviour,ISkill
{
    GameObject _player;

    //持续时间 和 移动速度-(速度自能是0.1和0,time必须大于0.8)  动和不动 0.8@0
    public void SetTimeAndMoveSpeed(string _timeAndMoveSpeed)
    {
        if(_timeAndMoveSpeed == "") _timeAndMoveSpeed = "2.5@0.1";

        float _times = float.Parse(_timeAndMoveSpeed.Split('@')[0]);
        if (_times < 0.8f) _times = 0.8f;
        float _moveSpeed = float.Parse(_timeAndMoveSpeed.Split('@')[1]);
        if (IsXianzhiSpeed && _moveSpeed > 0.1f) _moveSpeed = 0.1f;


        penhuoCXTimes = _times;
        moveSpeedX = _moveSpeed;

    }

    [Header("是否限制 喷火 时候 飞行 速度")]
    public bool IsXianzhiSpeed = true;


    public void GetStart(GameObject gameObj)
    {
        ReSetAll();
        GetComponent<AIAirRunNear>().TurnToPlayer();
        _isGetOver = false;
        IsGetStartAC = true;
        _player = gameObj;
        GetStartAC();
        
    }

    public bool IsGetOver()
    {
        return _isGetOver;
    }

    public void ReSetAll()
    {
        IsGetStartAC = false;
        IsStartPenhuo = false;
        GetInStand = false;
        IsInStanding = false;
        penhuoJishi = 0;
        IsShock = false;

        _isGetOver = false;
        IsInPenhuoing = false;
        TX_QS.Stop();
        TX_Huoyan.Stop();
        TX_Huoyan.gameObject.SetActive(false);
    }



    //起始动作
    [Header("龙喷火起始 动作")]
    public string StartACName = "run_penhuoQS";
    protected bool IsGetStartAC = false; 
    protected void GetStartAC()
    {
        _gameBody.GetAcMsg(StartACName, 2);
        TX_QS.Play();
        Audio_Penhuo.Play();
    }



    //喷火动作 移动
    [Header("龙喷火 移动动作")]
    public string PenHuoACName = "run_penhuo";
    protected bool IsStartPenhuo = false;




    GameBody _gameBody;
    RoleDate _roleDate;
    // Start is called before the first frame update
    void Start()
    {
        _gameBody = GetComponent<GameBody>();
        _roleDate = GetComponent<RoleDate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_roleDate.isDie || _roleDate.isBeHiting|| (IsXianzhiSpeed&&_player && _player.GetComponent<RoleDate>().isDie))
        {
            ReSetAll();
            _isGetOver = true;
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "end"), this);
            return;
        }

        if (GetInStand)
        {
            if (IsOverInStand())
            {
                //print(" inStand!!!!!   ");
                ReSetAll();
                _isGetOver = true;
            }
            return;
        }
        //print(_gameBody.GetDB().animation.lastAnimationName + "   zhuangtai     " + _gameBody.GetDB().animation.lastAnimationState._fadeProgress);

        if (IsGetStartAC && !_gameBody.isAcing)
        {
            IsGetStartAC = false;
            //_gameBody.isAcing = false;
            print("  ---->???  qishou wancheng ");
            _gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;

            

            IsStartPenhuo = true;
            //播放 起始 的特效  这里是循环的  
            
            //_gameBody.GetAcMsg(PenHuoACName);

            return;
        }

        if (IsStartPenhuo)
        {
            //GetInStand = true;

            if (!IsInPenhuoing)
            {
                IsInPenhuoing = true;
                penhuoJishi = 0;
                _gameBody.GetDB().animation.FadeIn(PenHuoACName);
                if(!IsXianzhiSpeed) GetComponent<GameBody>().isAcing = true;
                TX_Huoyan.gameObject.SetActive(true);
                TX_Huoyan.Play();

                moveSpeedX = this.transform.localScale.x > 0 ? -Mathf.Abs(moveSpeedX) : Mathf.Abs(moveSpeedX);
                print(" ******************************* moveSpeedX  " + moveSpeedX + "     localScale    " + this.transform.localScale.x);
            }

            if (!IsShock)
            {
                IsShock = true;
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-" + (penhuoCXTimes-0.4f)), this);
            }
            

            penhuoJishi += Time.deltaTime;


            this.transform.position = new Vector2(this.transform.position.x+ moveSpeedX, this.transform.position.y);
            //print("  moveSpeedX  "+ moveSpeedX+ "     localScale    "+ this.transform.localScale.x);

            //print(" penhuoJishi  "+ penhuoJishi);
            if (penhuoJishi>= penhuoCXTimes)
            {
                penhuoJishi = 0;
                print(" penghuo OVER!!!!!!! ");
                TX_QS.Stop();
                TX_Huoyan.Stop();
                TX_Huoyan.gameObject.SetActive(false);
                IsStartPenhuo = false;
                GetInStand = true;
            }
        }


    }

    [Header("喷火的 起手特效")]
    public ParticleSystem TX_QS;
    [Header("喷火的 起手吼声")]
    public AudioSource Audio_Penhuo;


    [Header("火焰特效")]
    public ParticleSystem TX_Huoyan;

    bool IsShock = false;


    //计时 和 移动模式  
    bool IsInPenhuoing = false;
    float penhuoJishi = 0;
    float penhuoCXTimes = 2.5f; 

    float moveSpeedX = 0.1f;



    bool _isGetOver = false;
    bool GetInStand = false;
    bool IsInStanding = false;
    bool IsOverInStand()
    {
        if (!IsInStanding)
        {
            IsInStanding = true;
            //_gameBody.GetDB().animation.FadeIn("stand_1",0.2f,1);
            _gameBody.GetAcMsg("stand_1", 2);
        }
        if (_gameBody.GetDB().animation.lastAnimationName == "stand_1") return true;
        return false;
    }
}
