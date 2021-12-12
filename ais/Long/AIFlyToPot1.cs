using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlyToPot1 : MonoBehaviour,ISkill
{
    AIAirRunNear _aiAirRunNear;


    //随机 就近 去附近2个点的 附近
    public Transform Pot1;
    public Transform Pot2;

    public Transform Pot3;
    public Transform Pot4;

    public Transform Pot5;
    public Transform Pot6;

    public Transform Pot7;
    public Transform Pot8;

    [Header("默认 点组 类型")]
    public int PotType = 1;

    protected Vector2 Pot_1;
    protected Vector2 Pot_2;



    public void SetPotType(int _type = 1)
    {
        PotType = _type;
        if(PotType == 1)
        {
            Pot_1 = Pot1.position;
            Pot_2 = Pot2.position;
            //print("1111111111111111111hi!!!!!   "+Pot_1);
        }else if (PotType == 2)
        {
            Pot_1 = Pot3.position;
            Pot_2 = Pot4.position;
            //print("  ------------------------------------------22222222222222222222222222222222222222222222  ");
        }
        else if (PotType == 3)
        {
            Pot_1 = Pot5.position;
            Pot_2 = Pot6.position;
        }
        else if (PotType == 4)
        {
            Pot_1 = Pot7.position;
            Pot_2 = Pot8.position;
        }
    }



    //误差距离 
    float ChosePointMoreDistance = 2;

    //选定的点
    protected Vector2 ChosePos;


    float FlySpeed = 4;

    float FlyMaxSpeed = 14;


    //选择就近点
    protected virtual void ChoseToNearPot()
    {
        float distance1 = GlobalTools.GetDistanceByTowPointBySqrMagnitude(Pot_1, this.transform.position);
        float distance2 = GlobalTools.GetDistanceByTowPointBySqrMagnitude(Pot_2, this.transform.position);

        if (distance1 > distance2)
        {
            ChosePos = Pot_2;
        }
        else
        {
            ChosePos = Pot_1;
        }

        float __x = ChosePos.x = GlobalTools.GetRandomNum() > 50 ? ChosePos.x - GlobalTools.GetRandomDistanceNums(ChosePointMoreDistance) : ChosePos.x + GlobalTools.GetRandomDistanceNums(ChosePointMoreDistance);
        float __y = ChosePos.y = GlobalTools.GetRandomNum() > 50 ? ChosePos.y - GlobalTools.GetRandomDistanceNums(ChosePointMoreDistance) : ChosePos.y + GlobalTools.GetRandomDistanceNums(ChosePointMoreDistance);

        ChosePos = new Vector2(__x, __y);
        print("  选择 点位置  "+ ChosePos);
    }

    bool IsStartFlying = false;
    void FlyToChosePoint()
    {
        if (GetComponent<RoleDate>().isBeHiting|| GetComponent<RoleDate>().isDie)
        {
            ReSetAll();
        }



        if (!IsStartFlying) return;
        if (_aiAirRunNear.ZhijieMoveToPoint(ChosePos, 1, FlySpeed, false,true,FlyMaxSpeed))
        {
            ReSetAll();
            print(" 到达目标地点！！！！ ");
        }
    }

    void TurnToPlayer()
    {
        print("  PotType "+ PotType+"   dianname  "+ Pot_1);
        if(this.transform.position.x<Pot_1.x|| this.transform.position.x > Pot_2.x)
        {
            GetComponent<AIAirRunNear>().TurnToPlayer();
        }
    }

    public void GetStart(GameObject gameObj)
    {
        print("  pottype ---   "+PotType);
        ChoseToNearPot();
        IsStartFlying = true;
        TurnToPlayer();
    }

    public bool IsGetOver()
    {
        return !IsStartFlying;
    }

    public void ReSetAll()
    {
        IsStartFlying = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        _aiAirRunNear = GetComponent<AIAirRunNear>();
    }

    // Update is called once per frame
    void Update()
    {
        FlyToChosePoint();
    }
}
