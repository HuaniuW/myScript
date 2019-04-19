using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AITheWay_dcr : MonoBehaviour {
    float theLive;
    [Header("下限1")]
    public float bz1 = 0.5f;
    [Header("下限2")]
    public float bz2 = 0f;
    // Use this for initialization
    void Start () {
        theLive = this.GetComponent<RoleDate>().live;
    }
	
    

    public string[] zsArr1 = { "atk_1", "atk_2", "atk_3" };
    public string[] zsArr2 = { "atk_1", "atk_2", "atk_3" };
    public string[] zsArr3 = { "atk_1", "atk_2", "atk_3" };
    public string[] zsArr4 = { "atk_1", "atk_1", "atk_1" };
    public string[] zsArr5 = { "atk_1", "atk_1", "atk_1" };
    public string[] zsArr6 = { "atk_1", "atk_1", "atk_1" };

    Array[] atkArrs = new Array[] { };


    public string[] GetZSArrays(int lie = 0)
    {
        if (this.GetComponent<RoleDate>().live <= theLive * bz2) {

        }else if(this.GetComponent<RoleDate>().live<= theLive * bz1)
        {
            atkArrs = new Array[] { zsArr4, zsArr5, zsArr6 };
        }
        else
        {
            atkArrs = new Array[] { zsArr1, zsArr2, zsArr3 };
        }
        
        //print("a: "+ atkArrs.Length);
        string[] zss = (string[])atkArrs[lie];
        //print(zss[0]);
        return zss;
    }

    public int GetZSArrLength()
    {
        Array[] atkArrs = { zsArr1, zsArr2, zsArr3 };
        return atkArrs.Length;
    }

    // Update is called once per frame
    void Update () {
        //GetZSArrLength();
    }
}
