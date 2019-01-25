using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTimer : MonoBehaviour {
    //控制慢动作用的计时器
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isStart) return;
	}

    bool isStart = false;

    public bool IsPauseTimeOver() {
        if (!isStart) return true;
        CurTime += Time.deltaTime;
        if (CurTime > EndTime)
        {
            End();
            return true;
        }
        return false;       
    }

    void End()
    {
        ReSet();
    }

    float StartTime;
    float CurTime;
    float EndTime;
  
    public bool GetStopByTime(float Duration)
    {
        isStart = true;
        StartTime = Time.time;
        CurTime = StartTime;
        EndTime = StartTime + Duration;
        return false;
    }

    public void ReSet()
    {
        isStart = false;

    }
}
