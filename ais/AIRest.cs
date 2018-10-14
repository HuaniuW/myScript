using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


    float _times;
    bool isStart = false;
    
    public void GetRestByTimes(float times)
    {
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
            _times -= Time.deltaTime;
            if (_times<=0)
            {
                isStart = false;
            }
        }
	}
}
