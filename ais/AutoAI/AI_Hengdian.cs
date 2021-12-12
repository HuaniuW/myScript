using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Hengdian : AI_SkillBase
{
    [Header("X位置点 如果没有 就用自身的X")]
    public Transform XPos;

    Vector2 GetOutPos()
    {
        float __x = 0;
        float __y = 0;
        if (XPos)
        {
            __x = XPos.position.x;
        }
        else
        {
            __x = zidanDian1.position.x;
        }

        __y = zidanDian1.position.y;
        return new Vector2(__x,__y);
    }


    protected override void ACSkillShowOut()
    {
        if (StartAudio) StartAudio.Stop();
        print(" dongzuo name shijian **********************ac    " + TXName);
        //GetComponent<ShowOutSkill>().ShowOutSkillByName(TXName, true);

        GameObject skillObj = Resources.Load(TXName) as GameObject;


        if (skillObj == null)
        {
            print("  skillObj = null  ");
            //Time.timeScale = 0;
            return;
        }
        GameObject skill = ObjectPools.GetInstance().SwpanObject2(skillObj);
        if (skill.GetComponent<LiziJNControl>())
        {
            skill.GetComponent<LiziJNControl>().GetLiziObj().GetComponent<JN_base>().atkObj = this.gameObject;
        }
        else
        {
            if (skill.GetComponent<JN_base>())
            {
                skill.GetComponent<JN_base>().atkObj = this.gameObject;
            }
        } 

        if (FasheiPointNum == 1)
        {
            skill.transform.position = GetOutPos();
        }
        else if (FasheiPointNum == 2)
        {
            skill.transform.position = zidanDian2.position;
        }
        if (IsNeedSetScaleX)
        {
            skill.transform.localScale = this.gameObject.transform.localScale;
        }

        skill.transform.parent = this.gameObject.transform.parent;


        //_gameBody.GetDB().animation.lastAnimationState.timeScale = 0.1f;
        //这里要实现 JNDate 赋值
    }


}
