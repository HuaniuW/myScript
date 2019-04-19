using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShanYin : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void StartAC()
    {
        startShan = true;
        this.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void StopAC()
    {
        startShan = false;
        this.GetComponent<CanvasGroup>().alpha = 0;
    }

    bool startShan = false;
    float alphaNums = 0.02f;
    public void StartShanYin()
    {
        if (this.GetComponent<CanvasGroup>().alpha == 1)
        {
            alphaNums = -0.02f;
        }
        else if (this.GetComponent<CanvasGroup>().alpha <= 0.4f)
        {
            alphaNums = 0.02f;
        }

        this.GetComponent<CanvasGroup>().alpha += alphaNums;
    }
	
	// Update is called once per frame
	void Update () {
        if (startShan) StartShanYin();

    }
}
