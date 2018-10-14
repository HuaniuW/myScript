using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AITheWay_dcr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	

    public string[] zsArr1 = { "atk_1", "atk_2", "atk_3" };
    public string[] zsArr2 = { "atk_1", "atk_2", "atk_3" };
    public string[] zsArr3 = { "atk_1", "atk_2", "atk_3" };

    Array[] atkArrs = new Array[] { };


    public string[] GetZSArrays(int lie = 0)
    {
        Array[] atkArrs = { zsArr1, zsArr2, zsArr3 };
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
        GetZSArrLength();
    }
}
