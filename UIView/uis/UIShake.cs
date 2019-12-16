using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShake : MonoBehaviour {
    public float shakeUpNum = 1.2f;
    public float initScaleNum = 1;
	// Use this for initialization
	void Start () {
        if(initScaleNum == 1)initScaleNum = this.transform.localScale.x;
        //print("this.transform.localScale    " + this.transform.localScale);
        shakeUpNum = initScaleNum + 0.2f;
        shackNums = initScaleNum;
    }

  
	
	// Update is called once per frame
	void Update () {
        Shakeing();
    }

    GameObject obj;


    bool isShakeing = false;
    bool isUp = false;
    float shackNums = 1;
    public void GetShake()
    {
        isShakeing = true;
        isUp = true;
    }

    void Shakeing()
    {
        if (!isShakeing) return;
        if (isUp)
        {
            shackNums += (shakeUpNum - shackNums) * 0.2f;
            if (shackNums >= shakeUpNum-0.04f)
            {
                shackNums = shakeUpNum;
                isUp = false;
            }
        }
        else
        {
            shackNums += (initScaleNum - shackNums) * 0.2f;
            if (shackNums <= initScaleNum+ 0.04f)
            {
                shackNums = initScaleNum;
                isShakeing = false;
            }
        }

        obj.transform.localScale = new Vector3(shackNums, shackNums, 1);
    }

    public void GetShakeObj(GameObject shakeObj)
    {
        obj = shakeObj;
        InitShake();
    }

    void InitShake()
    {
        obj.transform.localScale = new Vector3(initScaleNum, initScaleNum, initScaleNum);
        isShakeing = false;
        isUp = false;
    }

}
