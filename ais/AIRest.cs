using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRest : MonoBehaviour {
    [Header("是否转向")]
    public bool isZhuanXiang = true;
	// Use this for initialization
	void Start () {
		
	}


    float _times;
    bool isStart = false;
    
    public void GetRestByTimes(float times)
    {
        //print("休息 ---->rest");
        _times = times;
        isStart = true;
        
    }

    public void ReSet()
    {
        isStart = false;
    }

    public bool IsOver()
    {
        return !isStart;
    }

    // Update is called once per frame
    void Update () {
        if (isStart)
        {
            //print("休息    "+ GetComponent<AirGameBody>().GetPlayerRigidbody2D().velocity);
            if (GetComponent<RoleDate>().isBeHiting) return;
            if (!GetComponent<GameBody>()) return;
            GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
            _times -= Time.deltaTime;
            if (_times<=0)
            {
                isStart = false;
            }
        }
	}
}
