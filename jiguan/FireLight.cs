using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour {

    public Light pointLight;
    float lightNums = 0.05f;

	// Use this for initialization
	void Start () {
        //if(pointLight!=null) print("pointLight  >> " + pointLight.intensity);
	}

    float maxNums = 1.2f;
    float minNums = 0.6f;
	
	// Update is called once per frame
	void Update () {
        if (pointLight != null)
        {
            pointLight.intensity += lightNums;
            if (pointLight.intensity >= maxNums)
            {
                if (lightNums > 0) lightNums *= -1;
            }
            else if(pointLight.intensity <= minNums)
            {
                if (lightNums < 0) lightNums *= -1;
                if (GlobalTools.GetRandomNum() > 20) minNums = 0.6f + GlobalTools.GetRandomDistanceNums(0.8f);
            }
        }
	}
}
