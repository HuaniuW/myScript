using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRunCut : MonoBehaviour,ISkill{
    string oldRunName = "";
    float oldRunSpeedX = 0;
    public float CutDistance = 4;
    [Header("跑步动作")]
    public string RunName = "run_4";
    [Header("变化的追击移动速度")]
    public float moveSpeedX = 12;
    [Header("增加硬直")]
    public float addYZNum = -300;
    [Header("攻击动作 *在配置招式中找这个动作*")]
    public string AtkName = "atk_1";

    [Header("攻击动作信息 配置的招式 在ZSDate 里面找")]
    public string AtkMsgName = "atk_407";

    AIBase _aiBase;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isAcing)
        {
            if (GetComponent<AIBase>().IsTuihuiFangshouquing) {
                ReSetAll();
                return;
            }

            //重启
            if(GetComponent<AIBase>().IsChongqi) AcOver();
            //


            if (!isRunNear)
            {
                RunNear();
                return;
            }

            GetAtk();
        }	
	}

    bool isAcOver = false;
    GameBody gameBody;
    bool isAcing = false;
    GameObject EnemyObj;
    public void GetStart(GameObject obj)
    {
        EnemyObj = obj;
        
        //提高硬值
        if (!isAcing)
        {
            isAcing = true;
            GetComponent<RoleDate>().addYZ(addYZNum);
            gameBody = GetComponent<GameBody>();
            _aiBase = GetComponent<AIBase>();
            
            if (RunName == ""||!gameBody.GetDB().animation.HasAnimation(RunName)||!gameBody.GetDB().animation.HasAnimation(AtkName))
            {
                AcOver();
                return;
            }
            oldRunName = gameBody.GetRunName();
            oldRunSpeedX = gameBody.maxSpeedX;
            print("---------------->AtkName    " + AtkName + "   RunName  " + RunName+ "     oldRunName  "+ oldRunName);

            gameBody.RunACChange(RunName, moveSpeedX);
        }
    }

    bool isRunNear = false;
    void RunNear()
    {
        if(!isRunNear&& _aiBase.NearRoleInDistance(CutDistance,2))
        {
            isRunNear = true;
        }
    }

    bool isAtking = false;
    [Header("是否有第二个招式 切换不是连招")]
    public bool IsAC2 = false;

    public float Live_bili = 0.3f;
    public string Ac2Name = "";
    void GetAtk()
    {
        if (GetComponent<RoleDate>().isBeHiting)
        {
            //print("------------------------------------------------->被击中打断！");
            AcOver();
            return;
        }

        if (gameBody.GetDB().animation.lastAnimationName == AtkName && gameBody.GetDB().animation.isCompleted)
        {
            AcOver();
            return;
        }

        if (!isAtking) {
            isAtking = true;
            if(AtkMsgName!= Ac2Name&&IsAC2 && Ac2Name != "")
            {
                if(GetComponent<RoleDate>().live< GetComponent<RoleDate>().maxLive* Live_bili)
                {
                    AtkMsgName = Ac2Name;
                }
            }
            gameBody.GetAtk(AtkMsgName);
        }

        
    }



    void AcOver()
    {
        if (isAcing)
        {
            isAcing = false;
            isAtking = false;
            isRunNear = false;
            GetComponent<RoleDate>().hfYZ(addYZNum);
            gameBody.RunACChange(oldRunName, oldRunSpeedX);
        }
    }

    public bool IsAcOver()
    {
        return !isAcing;
    }

    public void ReSetAll()
    {
        AcOver();
    }

    public bool IsGetOver()
    {
        return IsAcOver();
    }
}
