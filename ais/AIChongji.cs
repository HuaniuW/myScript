using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChongji : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        runNear = GetComponent<AIAirRunNear>();
        runAway = GetComponent<AIAirRunAway>();
        CJYanmu.Stop();
    }

    AIAirRunNear runNear;
    AIAirRunAway runAway;
    // Update is called once per frame
    void Update()
    {
        if (isStarting) Starting();
    }

    Transform _targetObj;
    bool isGetOver = false;

    public bool IsChongjiOver()
    {
        return isGetOver;
    }

    Vector2 v2Speed;
    float chongjiSpeed = 16;

    public void GetStart(Transform targetObj)
    {
        if (_targetObj == null) _targetObj = targetObj;
        isGetOver = false;
        Vector2 targetPoint = targetObj.position;
        v2Speed = GlobalTools.GetVector2ByPostion(targetPoint, this.transform.position, chongjiSpeed);
        GetComponent<RoleDate>().addYZ(1000);
        isStarting = true;
    }

    void ReSetAll()
    {
        isStarting = false;
        isGetOver = false;
        isTanSheing = false;
        isGetOver = true;
        deltaNums = 0;
        CJYanmu.Stop();
        GetComponent<AirGameBody>().GetDB().animation.timeScale = 1f;
    }


    void Starting()
    {
        
        if (GetComponent<RoleDate>().isDie|| _targetObj==null||_targetObj.GetComponent<RoleDate>().isDie)
        {
            ReSetAll();
            return;
        }
        if (!isTanSheing && GetNearTarget())isTanSheing = true;
        if (isTanSheing) Tanshe();
    }

    float fantanNums = 0.1f;
    bool isStarting = false;
    public bool isTanSheing = false;
    float deltaNums = 0;
    void Tanshe()
    {
        /* if (GetComponent<RoleDate>().isBeHiting)
         {
             ReSetAll();
             return;
         }*/

        


        //做起始动作
        //起始动作完成 弹射 做第二个动作
        //完成后 还原动作
        if (GetComponent<AirGameBody>().GetDB().animation.HasAnimation("chongji_begin")&& GetComponent<AirGameBody>().GetDB().animation.HasAnimation("chongji_start"))
        {
            print("00000000000000000000000000  "+ GetComponent<AirGameBody>().GetDB().animation.timeScale);
            if (GetComponent<AirGameBody>().GetDB().animation.lastAnimationName == "chongji_begin" && GetComponent<AirGameBody>().GetDB().animation.isCompleted)
            {
                print(">>>>>>>>>>>>>>>>??????????????????????????????????????????????????????????????");
                v2Speed = GlobalTools.GetVector2ByPostion(_targetObj.position, this.transform.position, chongjiSpeed);
                //GetComponent<AirGameBody>().GetDB().animation.GotoAndPlayByFrame("chongji_start", 0, 1);
                GetComponent<AirGameBody>().GetAcMsg("chongji_start");
                GetComponent<AirGameBody>().GetDB().animation.timeScale = 1f;
            }
            print("弹射----------------------------------------------------------------------》》》弹射"+ GetComponent<AirGameBody>().GetDB().animation.lastAnimationName);
            if (GetComponent<AirGameBody>().GetDB().animation.lastAnimationName != "chongji_begin" && GetComponent<AirGameBody>().GetDB().animation.lastAnimationName != "chongji_start")
            {
                print("-------------------------------------------我靠 进来没 ");
                //GetComponent<AirGameBody>().GetDB().animation.Play("chongji_begin");  //GotoAndPlayByFrame("chongji_begin", 0, 1);
                //转向 朝向玩家
                GetComponent<AirGameBody>().GetAcMsg("chongji_begin");
                GetComponent<AirGameBody>().GetDB().animation.timeScale = 0.000000001f;
                return;
            }

           
        }
       

        //print("    deltaNums " + deltaNums+"  suduV2  "+v2Speed);
        deltaNums += Time.deltaTime;
        if (deltaNums >= _tsTimes)
        {
            deltaNums = 0;
            CJYanmu.Stop();
            GetComponent<RoleDate>().hfYZ(1000);
            //CJYanmu.loop = false;
            isStarting = false;
            isGetOver = true;
            isTanSheing = false;
            //print("*************************************************************冲击 结束！！！！！");
        }

        if (runAway.IsHitDown)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + fantanNums);
            v2Speed = new Vector2(v2Speed.x, -v2Speed.y);
        }
        else if (runAway.IsHitTop)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - fantanNums);
            v2Speed = new Vector2(v2Speed.x, -v2Speed.y);
        }
        else if (runAway.IsHitQianmain)
        {
            if (this.transform.localScale.x > 0)
            {
                this.transform.position = new Vector2(this.transform.position.x + fantanNums, this.transform.position.y);
            }
            else
            {
                this.transform.position = new Vector2(this.transform.position.x - fantanNums, this.transform.position.y);
            }

            v2Speed = new Vector2(-v2Speed.x, v2Speed.y);
        }

        //this.GetComponent<AirGameBody>().GetPlayerRigidbody2D().AddForce(v2Speed);
        this.GetComponent<AirGameBody>().GetPlayerRigidbody2D().velocity = v2Speed;


        CJYanmu.Play();
    }

    float _tsTimes = 0.5f;

    [Header("冲击烟幕")]
    public ParticleSystem CJYanmu;

    //攻击距离
    float _atkDistance = 10;
    //定位目标
    bool GetNearTarget()
    {
        return runNear.Zhuiji(_atkDistance);
    }
    //算xy速度
    //角色攻击动作
    //弹射
    //碰到墙壁 速度翻转
    //距离喷射拖尾特效
}
