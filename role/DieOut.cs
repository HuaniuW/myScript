using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOut : MonoBehaviour {

    public string type = "1";

	// Use this for initialization
	void Start () {

	}

    void DieOutDo()
    {
        //掉落几率 掉落的等级 ==  掉落多个物体
        //掉落 血  蓝  物品
    }

    bool IsDie = false;

	// Update is called once per frame
	void Update () {
        if (!IsDie && this.GetComponent<RoleDate>().isDie)
        {
            IsDie = true;
            DieOutDo();
        }
    }
}
