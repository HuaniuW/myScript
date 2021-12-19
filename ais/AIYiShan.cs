using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class AIYiShan : MonoBehaviour ,ISkill {
    [Header("攻击动作")]
    public string acName = "";
    [Header("起手招式停顿时间")]
    public float StartZSStopTimes = 0.5f;
    [Header("起手招式停顿速度")]
    public float pauseNums = 0.04f;
    [Header("特效名称")]
    public string txName = "";
    [Header("攻击距离")]
    public float atkDistance = 10f;

    [Header("增加硬直")]
    public float addYZNum = 0;

    EnemyGameBody _gameBody;
    RoleDate _roleDate;

    // Use this for initialization
    [Header("烟尘 ")]
    public ParticleSystem YanChen;



    

    bool isStart = false;
    bool isFirstAddListener = true;
    public bool IsAcOver()
    {
        return !isStart;
    }

    [Header("一闪起手 音效")]
    public AudioSource Audio_YishanQishou;



    protected UnityArmatureComponent DBBody;
    public void GetStart(GameObject gameObj)
    {

        //判断 前面距离
        Vector2 start;
        //前面的距离测试 
        Vector2 end;
        //print("ZSyishanZSys -1");
        jishiNums = 0;
        _XZtimes = 0;
        if (this.transform.localScale.x < 0)
        {
            start = this.transform.position;
            end = new Vector2(start.x + atkDistance+2, start.y);
            Debug.DrawLine(start, end, Color.yellow);
            if (Physics2D.Linecast(start, end, _gameBody.groundLayer))
            {
                //Time.timeScale = 0;
                //print("撞墙 右！！！");
                GetOver();
                return;
            }

        }
        else
        {
            start = this.transform.position;
            end = new Vector2(start.x - atkDistance-2, start.y);
            Debug.DrawLine(start, end, Color.yellow);
            if (Physics2D.Linecast(start, end, _gameBody.groundLayer))
            {
                //Time.timeScale = 0;
                //print("撞墙 左！！！");
                GetOver();
                return;
            }
        }
        //print("ZSyishanZSys -2");
        if (isStart) return;


        //print("ZSyishanZSys -3");



        if (!isStart) isStart = true;
        //检测是否有悬崖 会冲出去    判断是否动作名是空    判断是否有需要的动作
        if (IsGetOutLand() || acName == "" || !_gameBody.IsHanAC(acName)) {
            //print("ZSyishanZSys -4");
            GetOver();
            return;
        }

        //print("ZSyishanZSys -5");
        if (addYZNum!=0) _roleDate.addYZ(addYZNum);

        if (!DBBody) DBBody = _gameBody.GetDB();

        if (isFirstAddListener) {
            isFirstAddListener = false;
            DBBody.AddDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX2);
        }

        //print("ZSyishanZSys -6");

        //摆出动作
        if (DBBody.animation.HasAnimation(acName)) {
            //_gameBody.GetACByName(acName, true);
            _gameBody._isHasAtkTX = true;
            //_gameBody.GetAcMsg(acName);
            _gameBody.GetDB().animation.GotoAndPlayByFrame(acName,0,1);
            _gameBody.isAcing = true;

            if(Audio_YishanQishou) Audio_YishanQishou.Play();
        }
        //print("ZSyishanZSys -7");

        //停顿时间
        _gameBody.GetPause(StartZSStopTimes, pauseNums);
    }

    void GetOver()
    {
        print("ys over!!");
        isStart = false;
        _isShowTX = false;
        _isOutDistance = false;
        _isSpeedStart = false;
        _roleDate.hfYZ(addYZNum);
        GetComponent<TheTimer>().ReSet();
        _gameBody._isHasAtkTX = false;
        _gameBody.IsJiasu = false;
        TempDis = 0;
        if(YanChen) YanChen.Stop();
        _XZtimes = 0;
        jishiNums = 0;
        _gameBody.isAcing = false;
        _gameBody.SetV0();
        //print("一闪结束！！！！！！！！！");
        //if(DBBody) DBBody.RemoveDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
        //DBBody = null;

    }

    float TempDis = 0;

    //是否会超出地面 掉到悬崖下面
    bool IsGetOutLand()
    {
        return false;
    }

   

    public void ReSet()
    {
        isStart = false;
        _isShowTX = false;
        _isOutDistance = false;
        _isSpeedStart = false;
    }

	void Start () {
        if (YanChen) YanChen.Stop();

        _gameBody = GetComponent<EnemyGameBody>();
        _roleDate = GetComponent<RoleDate>();

    }

    protected void ShowACTX2(string type, EventObject eventObject)
    {
        //print("type:  "+type);
        if (!isStart) return;
        if (type == EventObject.FRAME_EVENT)
        {
            if (eventObject.name == "jn_begin")
            {
                //释放准备动作的特效
                
            }


            if (eventObject.name == "ac")
            {
                print("一闪 内 侦听");
                _posX = this.transform.position.x;
                //推力
                //float tuili = this.transform.localScale.x < 0 ? XForce : -XForce;
                //GetComponent<Rigidbody2D>().AddForce(new Vector2(tuili, 0));
                speedX = this.transform.localScale.x < 0 ? Mathf.Abs(speedX) : -Mathf.Abs(speedX);
                //GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, 0);
                _isSpeedStart = true;
                _gameBody.IsJiasu = true;
            }
        }

    }

    [Header("一闪的移动速度")]
    public float speedX = 120;
    
    [Header("一闪的结束距离")]
    public float SJDistance = 12;

    [Header("一闪结束后 动作停顿时间")]
    public float _overACStopTime = 0.5f;



    bool _isSpeedStart = false;
    bool _isOutDistance = false;
    float _posX;
    bool _isShowTX = false;


    float _XZtimes = 0;

    public UnityEngine.Transform qianmianjiance;
    [Header("是否 撞墙探测")]
    public bool IsHitQiangQainmian;
    [Header("地面图层 包括机关")]
    public LayerMask groundLayer;
    public bool IsHitWall2
    {
        get
        {
            if (qianmianjiance == null) return false;
            Vector2 start = qianmianjiance.position;
            float __x = this.transform.localScale.x > 0 ? start.x - 2 : start.x + 2;
            Vector2 end = new Vector2(__x, start.y);
            Debug.DrawLine(start, end, Color.yellow);
            IsHitQiangQainmian = Physics2D.Linecast(start, end, groundLayer);
            return IsHitQiangQainmian;
        }
    }


    float jishiNums = 0;
    float Jishi = 5;
    bool AIJishiReset()
    {
        if (!isStart) return false;
        jishiNums += Time.deltaTime;
        if (jishiNums>= Jishi)
        {
            jishiNums = 0;
            GetOver();
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update () {


        //print("ZSyishanZSys 1");
        if (!isStart) return;
        if (_roleDate.isDie||_roleDate.isBeHiting)
        {
            GetOver();
            return;
        }
        //print("ZSyishanZSys 2");
        if (AIJishiReset()) return;
        //print("ZSyishanZSys 3");
        if (_isOutDistance)
        {
            //print("----------------> ?????????????/????  "+ GetComponent<Rigidbody2D>().velocity.x);
            _XZtimes += Time.deltaTime;
            if (_XZtimes >= _overACStopTime)
            {
                _isOutDistance = false;
                GetOver();
            }
        }
        //print("ZSyishanZSys 4");

        
        //print("ZSyishanZSys 5");
        if (!GetComponent<AIBase>().isActioning)
        {
            GetOver();
            return;
        }
        //print("ZSyishanZSys 6");

        //print("  ?????? -------s x:    "+GetComponent<Rigidbody2D>().velocity.x);

        if (_gameBody.isInAiring)
        {
            GetOver();
            return;
        }
        //print("ZSyishanZSys 7");
        //被击中 或者 悬空 就结束
        if (_roleDate.isBeHiting|| _roleDate.isDie)
        {
            GetOver();
            //print("------> 进来没？？   "+ GetComponent<Rigidbody2D>().velocity);
            if(_roleDate.isDie) GetComponent<Rigidbody2D>().velocity = new Vector2(speedX*0.1f, 0);
            return;
        }
        //print("ZSyishanZSys 8");
        if (_gameBody.IsHitWall|| IsHitWall2)
        {
            GetOver();
            return;
        }
        //print("ZSyishanZSys 9");
        if (_isSpeedStart)
        {
            //print("ZSyishan   " + _gameBody.GetDB().animation.lastAnimationName+ " ------------  acName  "+ acName+"     是否在isAcing  "+_gameBody.isAcing);
            if (_gameBody.GetDB().animation.lastAnimationName == acName)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, 0);
                if (YanChen) YanChen.Play();
            }
            else
            {
                GetOver();
            }
            
        }
        //print("ZSys 10");

        if (isStart)
        {
            if (IsAtkACOver())
            {
                if (!_isShowTX)
                {
                    _isShowTX = true;
                    GetComponent<ShowOutSkill>().ShowOutSkillByName(txName, true);
                    //GetComponent<TheTimer>().TimesAdd(1,CallBack);
                    print("rolePos  "+ this.transform.position.x+"   距离    "+SJDistance);
                    //GetOver();
                }
                

                float dis = Mathf.Abs(this.transform.position.x - _posX);
                //if (Mathf.Abs(dis - TempDis) < 0.02f)
                //{
                //    //撞墙判断
                //    GetOver();
                //    return;
                //}

                TempDis = dis;
                //print("   -------->  x位上的 速度     speedX   " + speedX +"  sudu  "+ GetComponent<Rigidbody2D>().velocity.x);
                //print("---------------------------移动距离 " + dis+"    总距离    "+ SJDistance);
                if (dis > SJDistance || _gameBody.IsHitWall)
                {
                    //Time.timeScale = 0;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    _isSpeedStart = false;
                    //GetOver();
                   
                    
                    if (!_isOutDistance)
                    {
                        _isOutDistance = true;
                        //GetComponent<TheTimer>().TimesAdd(_overACStopTime, CallBack);
                    }
                    
                }
                //GetOver();
                //if (GetComponent<Rigidbody2D>().velocity.x<=1) GetOver();
            }

        }

        //print("  22222222222-------s x:    " + GetComponent<Rigidbody2D>().velocity.x);
    }


    void CallBack(float n)
    {
        GetOver();
    }

    bool IsAtkACOver()
    {
        if(DBBody.animation.lastAnimationName == acName && DBBody.animation.isCompleted) {
            //这里需要 停在最后一帧
            //print("ZSyishan   是否 停顿！！！！！！  动作完成------    "+acName+ "    isAcing   " + _gameBody.isAcing);
            DBBody.animation.Stop();
            return true;
        }
        return false;
    }

    public void ReSetAll()
    {
        ReSet();
    }

    public bool IsGetOver()
    {
        return IsAtkACOver();
    }
}
