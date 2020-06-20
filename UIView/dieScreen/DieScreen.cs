using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieScreen : MonoBehaviour {

    public Text textShan;

    // Use this for initialization
    void Start () {
        this.GetComponent<CanvasGroup>().alpha = 0;
	}
    
    bool isStartShowOut = false;
    public void StartAC()
    {
        this.GetComponent<CanvasGroup>().alpha = 0;
        isStartShowOut = true;
    }

    public void StopAC()
    {
        this.GetComponent<CanvasGroup>().alpha = 0;
        isStartShowOut = false;
        isCanTouch = false;
        textShan.GetComponent<ShanYin>().StopAC();
    }

    bool isCanTouch = false;
    float alphaNums = 0.05f;
    void ShowOut()
    {
        this.GetComponent<CanvasGroup>().alpha += (1 - this.GetComponent<CanvasGroup>().alpha) * alphaNums;
        if (this.GetComponent<CanvasGroup>().alpha >= 0.9)
        {
            this.GetComponent<CanvasGroup>().alpha = 1;
            textShan.GetComponent<ShanYin>().StartAC();
            isStartShowOut = false;
            isCanTouch = true;
        }
    }

	// Update is called once per frame
	void Update () {
        if (isStartShowOut) {
            ShowOut();
        }
        
        if (Input.anyKey)
        {
            //得到按下什么键
            //print("anyKey  " + Input.inputString);
            if (isCanTouch)
            {
                isCanTouch = false;
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_RESTART, null), this);
            }
        }
    }
}
