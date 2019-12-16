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
        ContinuouslyTimes();
    }

    public bool isStart = false;

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

    public void End()
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




    //连续计时


    public delegate void callback(float nums);
    callback _call;
    float _timeNums = 0;
    float _maxTimesNum = 0;
    float _intervals = 1;

    public void ContinuouslyTimesAdd(float timeNums,float intervals, callback call)
    {
        _maxTimesNum = timeNums / intervals;
        _timeNums = _maxTimesNum;
        _intervals = intervals;
        _call = call;
    }

    bool _isContinuouslyTimesStart = false;
    void ContinuouslyTimesStart()
    {
        if (_timeNums == 0) {
            _isContinuouslyTimesStart = false;
            _timeNums = (int)_maxTimesNum;
            return;
        }

        if (!IsPauseTimeOver())
        {
            return;
        }
        else {
            GetStopByTime(_intervals);
            _timeNums--;
            //print("cd _timeNums   " + _timeNums);
            _call(_timeNums);
        } 

    }

    public void GetContinuouslyTimesStart()
    {
        _isContinuouslyTimesStart = true;
    }

    void ContinuouslyTimes()
    {
        if (_isContinuouslyTimesStart) ContinuouslyTimesStart();
    }

}
