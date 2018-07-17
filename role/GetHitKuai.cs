using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitKuai : MonoBehaviour {

	// Use this for initialization
	void Start () {
        hitBar = ObjectPools.GetInstance().SwpanObject2(Resources.Load("fk") as GameObject);
        hitBar.transform.position = new Vector3(0, 200, 0);
        StartCoroutine(ObjectPools.GetInstance().IEDestory2(hitBar));
    }

    //块
    GameObject hitBar;
    //发射物
    GameObject fashewu;
    public void GetKuai(string jn_name = "fk",string moshi = "ljxs")
    {
        //hitBar = Resources.Load(jn_name) as GameObject;
       
        //fashewu = GameObject.Find("player");
        
        hitBar = ObjectPools.GetInstance().SwpanObject2(Resources.Load(jn_name) as GameObject);
        print("hitBar.transform.position    "+ hitBar.transform.position+"  ???   "+this.transform.position);
        //hitBar.transform.position.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z);
       
        print("hitBar.transform.position    " + hitBar.transform.position + "  2222   " + this.transform.position);

        AtkAttributesVO atkVVo = hitBar.GetComponent<AtkAttributesVO>();
        atkVVo.team = this.transform.GetComponent<RoleDate>().team;
        atkVVo.GetValue(DataZS.atk_1_v);
        //print("id2    "+hitBar.GetInstanceID());
        //print("id    "+hitBar.GetInstanceID());
        if (atkVVo !=null)
        {
            //print("size1 " + hitBar.transform.GetComponent<BoxCollider>().bounds.size.x);获取不到
            hitBar.transform.localScale = new Vector3(atkVVo._scaleW, atkVVo._scaleH, hitBar.transform.localScale.z);
            Vector3 xdp = this.transform.position;
            //print("进来没  "+xdp);
            xdp.x = this.transform.localScale.x == 1? xdp.x + atkVVo._xdx: xdp.x - atkVVo._xdx;
            xdp.y += atkVVo._xdy;
            hitBar.transform.position = xdp;
        }
        hitBar.transform.position = this.transform.position;

        //print("hi");
        //ObjectPools.GetInstance().IEDestory(hitBar, 1f);
        if (moshi == "ljxs")
        {
            StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(hitBar,2f));
        }
        
        //IEtest();
        //ObjectPools.GetInstance().DestoryObject(hitBar);
    }

    



    // Update is called once per frame
    void Update () {
		
	}
}
