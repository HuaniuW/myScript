using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class AIYiShan : MonoBehaviour {
    [Header("攻击动作")]
    public string acName = "";
    [Header("X方向推力")]
    public float XForce = 2000;
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

        //检测是否有悬崖 会冲出去    判断是否动作名是空    判断是否有需要的动作
        if (IsGetOutLand() || acName == "" || !GetComponent<EnemyGameBody>().IsHanAC(acName)) {
            GetOver();
            return;
        }

        if(addYZNum!=0) GetComponent<RoleDate>().addYZ(addYZNum);

        if (!DBBody) DBBody = GetComponent<GameBody>().GetDB();

        if (isFirstAddListener) {
            isFirstAddListener = false;
            DBBody.AddDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX2);
        } 



        //摆出动作
        if (DBBody.animation.HasAnimation(acName)) GetComponent<EnemyGameBody>().GetACByName(acName,true);
        //停顿时间
        GetComponent<GameBody>().GetPause(StartZSStopTimes, pauseNums);
    }

    void GetOver()
    {
        isStart = false;
        GetComponent<RoleDate>().hfYZ(addYZNum);
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
                print("一闪 内 侦听");
                //推力
                float tuili = this.transform.localScale.x < 0 ? XForce : -XForce;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(tuili, 0));
                
            }
        }

    }

    // Update is called once per frame
    void Update () {
        if (!isStart) return;

        if (GetComponent<RoleDate>().isBeHiting)
        {
            GetOver();
        }

        if (isStart)
        {
            if (IsAtkACOver())
            {
                GetComponent<ShowOutSkill>().ShowOutSkillByName(txName, true);
                GetOver();
            }
        }
	}

    bool IsAtkACOver()
    {
        if(DBBody.animation.lastAnimationName == acName && DBBody.animation.isCompleted) {
            return true;
        }
        return false;
    }
}
