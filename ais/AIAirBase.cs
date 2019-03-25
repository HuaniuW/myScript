using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAirBase : AIBase {

	// Use this for initialization
	void Start () {
        GetStart();
	}
	
	// Update is called once per frame
	void Update () {
        GetUpdate2();
	}

    protected override bool NearRoleInDistance(float distance)
    {
        if (DontNear) return true;
        if (gameObj.transform.position.x - transform.position.x > distance)
        {
            //目标在右
            gameBody.RunRight(0.9f);
            return false;
        }
        else if (gameObj.transform.position.x - transform.position.x < -distance)
        {
            //目标在左
            gameBody.RunLeft(-0.9f);
            return false;
        }
        else
        {
            return true;
        }
    }

    protected virtual void GetUpdate2()
    {
        if (!gameObj)
        {
            gameObj = GlobalTools.FindObjByName("player");
            return;
        }

        if (gameObj.GetComponent<RoleDate>().isDie)
        {
            gameBody.ResetAll();
            //gameBody.Stand();
            return;
        }

        //被攻击没有重置 isAction所以不能继续攻击了
        if (GetComponent<RoleDate>().isBeHiting)
        {
            AIBeHit();
            return;
        }

        if (gameBody.tag != "AirEnemy" && !gameBody.IsGround)
        {
            return;
        }



        if (isPatrol && !IsFindEnemy())
        {
            AIReSet();
            Patrol();
        }


        if (!isActioning && IsHitWallOrNoWay)
        {
            isFindEnemy = false;
            AIReSet();
            gameBody.GetStand();
            return;
        }


        //超出追击范围
        IsEnemyOutAtkDistance();

        if (!IsFindEnemy()) return;
        GetAtkFS();
    }

    protected virtual bool TongY(float distance)
    {
        if (gameObj.transform.position.y - transform.position.y > distance)
        {
            //目标在上面
            this.GetComponent<AirGameBody>().RunY(0.9f);
            return false;
        }
        else if (gameObj.transform.position.y - transform.position.y < -distance)
        {
            //目标在下面
            this.GetComponent<AirGameBody>().RunY(-0.9f);
            return false;
        }
        else
        {
            return true;
        }
    }
}
