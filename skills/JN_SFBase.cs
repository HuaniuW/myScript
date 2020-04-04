using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class JN_SFBase : MonoBehaviour
{
    //技能释放基础类
    [Header("释放特效的 动作 名字")]
    public string ACName = "";
    [Header("释放特效的 名字")]
    public string TXName = "";
    [Header("是否要取 gamebody")]
    public bool IsGetBody = true;

    [Header("修正 X")]
    public float XZX = 0;

    [Header("修正 Y")]
    public float XZY = 0;

    [Header("攻击距离")]
    public float AtkDistance = 0;

    [Header("接近 速度")]
    public float NearSpeed = 0.9f;

    [Header("起手 延迟  技能动作的 延迟动作")]
    public float qishoudongzuoyanchi = 0;


    protected GameBody _gameBody;

    protected bool IsStarting = false;

    bool IsInAtkDistance = false;


    GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        if (IsGetBody) {
            //print("??  start! ");
            _gameBody = GetComponent<GameBody>();
            _gameBody.GetDB().AddDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print("?");
        GetUpDate();
    }

    protected virtual void GetUpDate()
    {
        //print("0");
        if (IsStarting)
        {
            //print("2");
            if (!IsInAtkDistance)
            {
                //print("3");
                if (NearRoleInDistance(AtkDistance))
                {
                    //print("4");
                    IsInAtkDistance = true;

                    GetAC();
                }
            }else
            {
                if (!_gameBody.isAcing)
                {
                    //角色动作走完
                    IsStarting = false;
                }
            }
           


        }

        //if(_gameBody) print("_gameBody.GetDB().animation.timeScale3 :       " + _gameBody.GetDB().animation.timeScale);
    }



    protected virtual void GetAC()
    {
        _gameBody.IsSFSkill = true;
        //角色 释放动作

        
        //print(" acName    "+ACName);
        _gameBody.GetAcMsg(ACName);

        //这里是否有 什么释放动作  慢动作 减速 +硬直 等   地上的前置特效

        //起手慢动作

        if(qishoudongzuoyanchi!=0) _gameBody.GetPause(qishoudongzuoyanchi, 0);
        //print("_gameBody.GetDB().animation.timeScale:       "+ _gameBody.GetDB().animation.timeScale);
        //_gameBody.GetDB().animation.timeScale = 0;
        //print("_gameBody.GetDB().animation.timeScale2 :       " + _gameBody.GetDB().animation.timeScale);
    }



    public virtual void GetStart(GameObject obj)
    {
        _player = obj;
        //print("?????   "+obj);
        //进入 攻击距离
        IsStarting = true;
    }



    public virtual bool IsGetOver() {
        if (!IsStarting)
        {
            ReSetAll();
        }
        return !IsStarting;
    }



    protected virtual void ReSetAll()
    {
        _gameBody.IsSFSkill = false;
        IsInAtkDistance = false;
    }




    protected virtual void ShowACTX(string type, EventObject eventObject)
    {
        //print("type:  "+type);
        //print("eventObject  ????  " + eventObject);
        //print("___________________________________________________________________________________________________________________________name    "+ eventObject.name);
       


        if (type == EventObject.FRAME_EVENT)
        {
            if (!IsStarting) return;
            if (eventObject.name == "ac")
            {

                GetComponent<ShowOutSkill>().ShowOutSkillByName(TXName, true);

              
            }
        }

    }



    private void OnDisable()
    {
        print("释放技能类 销毁");
        _gameBody.GetDB().RemoveDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
    }



    public virtual bool NearRoleInDistance(float distance)
    {
        //print("  --------->distance    " + distance);
        if (_player.transform.position.x - transform.position.x > distance)
        {
            //目标在右
            _gameBody.RunRight(NearSpeed);
            return false;
        }
        else if (_player.transform.position.x - transform.position.x < -distance)
        {
            //目标在左
            _gameBody.RunLeft(-NearSpeed);
            return false;
        }
        else
        {
            return true;
        }
    }

}
