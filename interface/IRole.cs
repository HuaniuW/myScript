using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRole{

    string GetAcMsg(string acName, int type = 1, float FadeInTimes = 0.2f);
    void SpeedXStop();
	void SetACingfalse();
    void TestsI();
    void GetSkill1();
    void GetSkill2();
}
