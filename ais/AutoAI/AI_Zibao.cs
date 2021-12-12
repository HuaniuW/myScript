using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Zibao : AI_SkillBase
{
    //自爆类

    bool IsZibao = false;
    string TX_ZiBao = "TX_DianranHuoXiaoGuo2";
    protected override void OtherOver()
    {
        //好像可以 进来多次 生成 很多炸弹
        if (IsZibao) return;
        IsZibao = true;
        GameObject TX_Baozha =  GlobalTools.GetGameObjectInObjPoolByName(TX_ZiBao);
        TX_Baozha.transform.position = this.gameObject.transform.position;
        TX_Baozha.transform.parent = this.gameObject.transform.parent;

        this._roleDate.live = 0;


    }
}
