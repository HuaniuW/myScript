using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlyToPot2GD : AIFlyToPot1
{
    //飞到固定 位置点
    protected override void ChoseToNearPot()
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
        print("  选择 点位置  " + ChosePos);
    }



}
