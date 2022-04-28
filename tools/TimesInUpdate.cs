using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimesInUpdate
{
    public static TimesInUpdate GetInstance()
    {
        return new TimesInUpdate();
    }

    float jishi = 0;

    public bool GetTimesStart(float TimesNum)
    {
        jishi += Time.deltaTime;
        if(jishi>= TimesNum)
        {
            jishi = 0;
            return true;
        }
        return false;
    }
}
