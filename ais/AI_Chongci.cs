using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Chongci : MonoBehaviour, ISkill
{

    public GameObject HitKuai;
    public float Distances = 30;
    public float Speed = 26;
    public string RunName = "run_3";
    [Header("是否有起始动作")]
    public bool IsHasQSAC = true;
    [Header("起始动作名字")]
    public string QishiACName = "";

    [Header("冲击的粒子效果")]
    public ParticleSystem Lizi;

    float _positionX = 0;

    // Start is called before the first frame update
    void Start()
    {
        HitKuai.SetActive(false);
        Lizi.gameObject.SetActive(false);
        _gameBody = GetComponent<GameBody>();
        
    }

    bool _isStarting = false;
    bool _isAcOver = false;
    //原来的默认跑步动作
    string _ysRunName;
    float _ysRunSpeed;

    GameObject _player;
    GameBody _gameBody;

    string runDirection = "left";
    //奔跑动作 和 速度 还原
    void RunAndSpeedHY()
    {
        _gameBody.RunACChange(_ysRunName, _ysRunSpeed);
    }


    public void GetStart(GameObject player)
    {
        //判断是否有动作
        if (!_gameBody.GetDB().animation.HasAnimation(RunName))
        {
            _isAcOver = true;
            return;
        }
        else
        {

            _ysRunSpeed = _gameBody.maxSpeedX;
            _ysRunName = _gameBody.GetRunName();
            _gameBody.RunACChange(RunName, Speed);
        }
        
        
        //寻找玩家
        if (!_player)_player = player;
        if (_player.GetComponent<RoleDate>().isDie)
        {
            _isAcOver = true;
            return;
        }

        //判断距离  大于冲刺距离 结束
        if (Mathf.Abs(_player.transform.position.x - this.transform.position.x)> Distances)
        {
            ReSetAll();
            return;
        }


        _positionX = this.transform.position.x;


        //判断 玩家 在左还是右
        if (_player.transform.position.x > this.transform.position.x)
        {
            runDirection = "right";
        }else
        {
            runDirection = "left";
        }

        _isAcOver = false;
        _isStarting = true;
        if (!IsHasQSAC) {
            //如果没有 前置动作 直接显示碰撞快 和特效
            HitKuai.SetActive(true);
            ShowTX();
        }
        
    }

    void ShowTX()
    {
        Lizi.gameObject.SetActive(true);
        //print("----------  "+this.transform.localScale+"   rotation    "+Lizi.transform.localRotation+"  ???    "+Lizi.transform.rotation);
        //Lizi.transform.localRotation = Quaternion.Euler(0,0,0);  //new Quaternion(-1f, Lizi.transform.localRotation.y, Lizi.transform.localRotation.z, Lizi.transform.localRotation.w);
        //if (this.transform.localScale.x == -1)
        //{
        //    Lizi.transform.localEulerAngles = new Vector3(0, Lizi.transform.localEulerAngles.y, Lizi.transform.localEulerAngles.z); //new Quaternion(180, Lizi.transform.localRotation.y, Lizi.transform.localRotation.z, Lizi.transform.localRotation.w);
        //}
        //else
        //{
        //    Lizi.transform.localEulerAngles = new Vector3(180, Lizi.transform.localEulerAngles.y, Lizi.transform.localEulerAngles.z); //new Quaternion(0, Lizi.transform.localRotation.y, Lizi.transform.localRotation.z, Lizi.transform.localRotation.w);
        //}
    }

    bool _isQiShiAcOver = false;
    void startRunToTarget()
    {
        //被攻击 中断
        if(GetComponent<RoleDate>().isDie|| GetComponent<RoleDate>().isBeHiting)
        {
            ReSetAll();
            return;
        }

        //判断 前面如果撞墙 就结束
        if (_gameBody.IsHitWall)
        {
            print("-------------------------------------------  冲刺撞墙 ");
            ReSetAll();
            return;
        }


        if (IsHasQSAC && !_isQiShiAcOver)
        {
            //print("进来没？？？？？   " + QishiACName+"  shifouhanyouAC  "+);
            //起始动作做完
            if (_gameBody.GetDB().animation.lastAnimationName != QishiACName)
            {
                //_player.GetComponent<GameBody>().GetDB().animation.GotoAndPlayByFrame(QishiACName);
                //print("?????????????进来没？？");
                _gameBody.GetAcMsg(QishiACName);
            }
            else
            {
                //print("------->> 1");
                if (_gameBody.GetDB().animation.isCompleted)
                {
                    _isQiShiAcOver = true;
                }

            }
            return;
        }

        

        //下压的力 放置 角色飞起来了
        //_gameBody.GetPlayerRigidbody2D().AddForce(new Vector2(0, -20));
        _gameBody.GetZongTuili(new Vector2(0, -20));

        if (runDirection == "right")
        {
            _gameBody.RunRight(3f);
        }
        else
        {
            _gameBody.RunLeft(-3f);
        }
        if(Mathf.Abs(this.transform.position.x - _positionX) >= Distances)
        {
            ReSetAll();
        }


        if (!IsShowTXAndHitKuai&& _isStarting)
        {
            //print("  速度是多少： "+_gameBody.GetPlayerRigidbody2D().velocity.x);
            if (Mathf.Abs(_gameBody.GetPlayerRigidbody2D().velocity.x)<3) return;
            IsShowTXAndHitKuai = true;
            //print("_gameBody.GetDB().animation.lastAnimationName   " + _gameBody.GetDB().animation.lastAnimationName);
            HitKuai.SetActive(true);
            //HitKuai.transform.position = this.transform.position;
            ShowTX();
        }
        if (HitKuai)
        {
            //hitkuai跟随 特效的位置  后面也可以加一个参照点 跟随参照点
            HitKuai.transform.position = Lizi.transform.position;
        }
    }

    bool IsShowTXAndHitKuai = false;

    public void ReSetAll()
    {
        
        _isStarting = false;
        _isAcOver = true;
        IsShowTXAndHitKuai = false;
        HitKuai.SetActive(false);
        Lizi.gameObject.SetActive(false);
        _isQiShiAcOver = false;
        RunAndSpeedHY();
        
    }


    public bool IsAcOver()
    {
        return _isAcOver;
    }



    // Update is called once per frame
    void Update()
    {
        if (GetComponent<AIBase>().IsTuihuiFangshouquing)
        {
            ReSetAll();
            return;
        }
        if (_isStarting) startRunToTarget();
    }

    public bool IsGetOver()
    {
        return IsAcOver();
    }
}
