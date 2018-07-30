using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeHit : MonoBehaviour {
    RoleDate roleDate;
    GameBody gameBody;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetBeHit(JN_Date jn_date,float sx)
    {
        //print("被击中 !! "+this.transform.name+"   攻击力 "+ jn_date.atkPower+"  我的防御力 "+this.GetComponent<RoleDate>().def);


        gameBody = GetComponent<GameBody>();
        roleDate = GetComponent<RoleDate>();

        roleDate.live -= (jn_date.atkPower - this.GetComponent<RoleDate>().def);
        if (roleDate.live < 0) roleDate.live = 0;
        //print("live "+ roleDate.live);
        if (!roleDate.isCanBeHit) return;
       
        if (gameBody != null)
        {
            //判断是否破防
            gameBody.HasBeHit();
        }




        //判断是否在躲避阶段  无法被攻击
        //判断击中特效播放位置
        //击退 判断方向
        float _psScaleX = sx;
        //判断是否在空中
        //挨打动作  判断是否破硬直
        //判断是否生命被打空
        Bloods(_psScaleX);
    }

    //击中特效
    void Bloods(float psScaleX)
    {
        //print("fx:   "+psScaleX);
        GameObject blood = Resources.Load("BloodSplatCritical2D1") as GameObject;
        blood = ObjectPools.GetInstance().SwpanObject2(blood);
        blood.transform.position = this.transform.position;
        blood.transform.localScale = new Vector3(1, 1, psScaleX);

        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(blood, 0.5f));

    }
}
