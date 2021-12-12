using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ZidanQiHeng : AI_SkillBase
{

    protected override void ACSkillShowOut()
    {
        print(" dongzuo name shijian **********************ac    " + TXName);


        //横向子弹  先浮起 然后 直线 朝着 玩家方向

        float nums = GlobalTools.GetRandomNum(4)+4;


        for (int i=0;i<nums;i++)
        {
            GameObject skillObj = Resources.Load(TXName) as GameObject;
            GameObject skill = ObjectPools.GetInstance().SwpanObject2(skillObj);
            skill.GetComponent<JN_base>().atkObj = this.gameObject;

            //出现位置
            float __x = this.transform.position.x - 2 + GlobalTools.GetRandomDistanceNums(4);
            float __y = this.transform.position.y - 1 + GlobalTools.GetRandomDistanceNums(1);

            skill.transform.position = new Vector2(__x,__y);
            skill.transform.parent = this.gameObject.transform.parent;
        }





        //GetComponent<ShowOutSkill>().ShowOutSkillByName(TXName, true);

        //GameObject skillObj = Resources.Load(TXName) as GameObject;


    
        //GameObject skill = ObjectPools.GetInstance().SwpanObject2(skillObj);
        //if (skill.GetComponent<LiziJNControl>())
        //{
        //    skill.GetComponent<LiziJNControl>().GetLiziObj().GetComponent<JN_base>().atkObj = this.gameObject;
        //}

        //if (FasheiPointNum == 1)
        //{
        //    skill.transform.position = zidanDian1.position;
        //}
        //else if (FasheiPointNum == 2)
        //{
        //    skill.transform.position = zidanDian2.position;
        //}
        //if (IsNeedSetScaleX)
        //{
        //    skill.transform.localScale = this.gameObject.transform.localScale;
        //}

        


        //_gameBody.GetDB().animation.lastAnimationState.timeScale = 0.1f;
        //这里要实现 JNDate 赋值
    }

}
