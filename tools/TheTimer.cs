using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTimer : MonoBehaviour {
    //控制慢动作用的计时器  必须挂在 模块上 不能单独 new 否则不能回调
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ContinuouslyTimes();
        DanciTimes();
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
        //_isContinuouslyTimesStart = false;
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
        print("!!!!!!!!!!   "+ _timeNums);
        _call = call;
    }

    public void TempSetTimeNums(float nums)
    {
        _timeNums = nums;
        _isContinuouslyTimesStart = true;
    }

    public void TimesAdd(float times, callback call)
    {
        GetStopByTime(times);
        isDanciTimes2 = true;
        _call = call;
    }

    bool isDanciTimes2 = false;
    void DanciTimes() {
        if (isDanciTimes2) {
            DanciStart();
        } 
    }

    void DanciStart() {
        if (IsPauseTimeOver())
        {
            _call(0);
            isDanciTimes2 = false;
        }
    }

    bool _isContinuouslyTimesStart = false;
    void ContinuouslyTimesStart()
    {
        //print("------------------------------------------------------------------_timeNums      "+ _timeNums);
        if (IsPauseTimeOver())
        {
            GetStopByTime(_intervals);
            _timeNums--;
            _call(_timeNums);
        }


        if (_timeNums == 0) {
            GetFull();
            return;
        }

      

    }


    public void GetFull()
    {
        _isContinuouslyTimesStart = false;
        _timeNums = (int)_maxTimesNum;
    }

    public void GetContinuouslyTimesStart()
    {
        if(!_isContinuouslyTimesStart) _isContinuouslyTimesStart = true;
        //print("开始计时  _timeNums  " + _timeNums);
    }

    void ContinuouslyTimes()
    {
        if (_isContinuouslyTimesStart) ContinuouslyTimesStart();
    }

}
