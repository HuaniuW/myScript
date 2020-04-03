using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class AIYiShan : MonoBehaviour {
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
   
    // Use this for initialization

    bool isStart = false;
    bool isFirstAddListener = true;
    public bool IsAcOver()
    {
        return !isStart;
    }


    protected UnityArmatureComponent DBBody;
    public void GetStart()
    {
        
        if (isStart) return;
        if (!isStart) isStart = true;
        _gameBody = GetComponent<EnemyGameBody>();
        //检测是否有悬崖 会冲出去    判断是否动作名是空    判断是否有需要的动作
        if (IsGetOutLand() || acName == "" || !_gameBody.IsHanAC(acName)) {
            GetOver();
            return;
        }

        if(addYZNum!=0) GetComponent<RoleDate>().addYZ(addYZNum);

        if (!DBBody) DBBody = _gameBody.GetDB();

        if (isFirstAddListener) {
            isFirstAddListener = false;
            DBBody.AddDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX2);
        } 



        //摆出动作
        if (DBBody.animation.HasAnimation(acName)) _gameBody.GetACByName(acName,true);
        //停顿时间
        _gameBody.GetPause(StartZSStopTimes, pauseNums);
    }

    void GetOver()
    {
        isStart = false;
        _isShowTX = false;
        _isOutDistance = false;
        _isSpeedStart = false;
        GetComponent<RoleDate>().hfYZ(addYZNum);
        GetComponent<TheTimer>().ReSet();
        //if(DBBody) DBBody.RemoveDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
        //DBBody = null;

    }

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
                //print("一闪 内 侦听");
                _posX = this.transform.position.x;
                //推力
                //float tuili = this.transform.localScale.x < 0 ? XForce : -XForce;
                //GetComponent<Rigidbody2D>().AddForce(new Vector2(tuili, 0));
                speedX = this.transform.localScale.x < 0 ? Mathf.Abs(speedX) : -Mathf.Abs(speedX);
                //GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, 0);
                _isSpeedStart = true;
            }
        }

    }

    [Header("一闪的移动速度")]
    public float speedX = 120;
    
    [Header("一闪的结束距离")]
    float _cjDistance = 12;

    [Header("一闪结束后 动作停顿时间")]
    float _overACStopTime = 0.5f;



    bool _isSpeedStart = false;
    bool _isOutDistance = false;
    float _posX;
    bool _isShowTX = false;
    // Update is called once per frame
    void Update () {
        if (!isStart) return;

        if (_gameBody.isInAiring)
        {
            GetOver();
            return;
        }

        if (GetComponent<RoleDate>().isBeHiting|| GetComponent<RoleDate>().isDie)
        {
            GetOver();
            //print("------> 进来没？？   "+ GetComponent<Rigidbody2D>().velocity);
            if(GetComponent<RoleDate>().isDie) GetComponent<Rigidbody2D>().velocity = new Vector2(speedX*0.1f, 0);
            return;
        }

        if (_isSpeedStart)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, 0);
        }

        if (isStart)
        {
            if (IsAtkACOver())
            {
                if (!_isShowTX)
                {
                    _isShowTX = true;
                    GetComponent<ShowOutSkill>().ShowOutSkillByName(txName, true);
                    //GetComponent<TheTimer>().TimesAdd(1,CallBack);
                    
                    //GetOver();
                }


                float dis = Mathf.Abs(this.transform.position.x - _posX);
                //print("---------------------------移动距离 "+dis);
                if (dis > _cjDistance|| _gameBody.IsHitWall)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    _isSpeedStart = false;
                    //GetOver();
                   
                    
                    if (!_isOutDistance)
                    {
                        _isOutDistance = true;
                        GetComponent<TheTimer>().TimesAdd(_overACStopTime, CallBack);
                    }
                    
                }
                //GetOver();
                //if (GetComponent<Rigidbody2D>().velocity.x<=1) GetOver();
            }

        }
	}


    void CallBack(float n)
    {
        GetOver();
    }

    bool IsAtkACOver()
    {
        if(DBBody.animation.lastAnimationName == acName && DBBody.animation.isCompleted) {
            DBBody.animation.Stop();
            return true;
        }
        return false;
    }
}
