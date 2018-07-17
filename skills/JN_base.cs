using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_base : MonoBehaviour
{

    GameObject hitKuai;

        // Use this for initialization
    void Start()
    {
        //print("-------------------->1");
        //绕过 位置无效的bug
        hitKuai = ObjectPools.GetInstance().SwpanObject2(Resources.Load("fk") as GameObject);
        hitKuai.transform.position = new Vector3(0, 200, 0);
        StartCoroutine(ObjectPools.GetInstance().IEDestory2(hitKuai));
    }



    private void OnEnable()
    {
		//print("-------------------->1????");

        
		//hitKuai.transform.position = this.gameObject.transform.position;

    }

	public void GetPositionAndTeam(Vector3 _position, float team,float _sacaleX)
    {
        this.transform.position = _position;

        //hitKuai.transform.position = _position;
        hitKuai = Resources.Load("fk") as GameObject;
        
        hitKuai = ObjectPools.GetInstance().SwpanObject2(hitKuai);
        //hitKuai.transform.localPosition = Vector3.zero;
        //print(hitKuai.transform.position + "    t  " + _position);
        //hitKuai.transform.parent = this.transform.parent;
        hitKuai.transform.position = _position;
        hitKuai.GetComponent<AtkAttributesVO>().GetValue(DataZS.atk_1_v);
		hitKuai.GetComponent<AtkAttributesVO>().team = team;

        this.transform.localScale = new Vector3(-_sacaleX, transform.localScale.y, transform.localScale.z);

		//print(hitKuai.transform.position+"    t  "+gameObject.transform.position);

		StartCoroutine(ObjectPools.GetInstance().IEDestory2(hitKuai));
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(gameObject, 1f));
    }



}
