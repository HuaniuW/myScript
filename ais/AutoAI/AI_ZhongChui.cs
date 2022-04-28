using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ZhongChui : AI_SkillBase
{
    [Header("大锤 攻击地面1")]
    public GameObject DachuiZhengdi1;
    [Header("大锤 攻击地面2 全屏 在角色外")]
    public GameObject DachuiZhengdi2;



    public string TX_Zhongchui1Name = "";
    public Transform Zhongchui1_pos;


    public string TX_Zhongchui2Name = "";
    public Transform Zhongchui2_pos;


    public Transform PosL;
    public Transform PosR;



    [Header("特效 落石 名字 ")]
    public string LuoshiName = "TX_Luoshi";
    void Luoshi()
    {
        //显示 落石 就可以了  落石 自己 生成烟幕 和落石


        GameObject skillObj = Resources.Load(LuoshiName) as GameObject;


        for (int i=0;i<3;i++)
        {
            GameObject skill = ObjectPools.GetInstance().SwpanObject2(skillObj);
            skill.GetComponent<TX_Luoshi>().GetStart(this.gameObject);
            float __x = PosL.position.x + GlobalTools.GetRandomDistanceNums(PosR.position.x - PosL.position.x);
            float __y = PosL.position.y+7;
            skill.transform.position = new Vector2(__x,__y);
        }


        


    }



    protected override void ACSkillShowOut()
    {
        print(" dongzuo name shijian **********************ac    " + TXName+"  重锤  ");
        //GetComponent<ShowOutSkill>().ShowOutSkillByName(TXName, true);
        Luoshi();
        //if (!DachuiZhengdi1.activeSelf) DachuiZhengdi1.SetActive(true);
        //if (!DachuiZhengdi2.activeSelf) DachuiZhengdi2.SetActive(true);
        GameObject skillObj = Resources.Load(TX_Zhongchui1Name) as GameObject;
        GameObject skill = ObjectPools.GetInstance().SwpanObject2(skillObj);
        skill.GetComponent<JN_base>().atkObj = this.gameObject;
        skill.transform.position = Zhongchui1_pos.position;
        skill.transform.parent = this.gameObject.transform.parent;
        //skill.transform.position = Zhongchui1_pos.position;


        GameObject skillObj2 = Resources.Load(TX_Zhongchui2Name) as GameObject;
        GameObject skill2 = ObjectPools.GetInstance().SwpanObject2(skillObj2);
        skill2.GetComponent<JN_base>().atkObj = this.gameObject;
        skill2.transform.position = Zhongchui2_pos.position;
        skill2.transform.parent = this.gameObject.transform.parent;

        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.6"), this);

        //return;
        //GameObject skillObj = Resources.Load(TXName) as GameObject;


        //if (skillObj == null)
        //{
        //    print("  skillObj = null  ");
        //    //Time.timeScale = 0;
        //    return;
        //}
        ////GameObject skill = ObjectPools.GetInstance().SwpanObject2(skillObj);
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

        //skill.transform.parent = this.gameObject.transform.parent;


        //_gameBody.GetDB().animation.lastAnimationState.timeScale = 0.1f;
        //这里要实现 JNDate 赋值
    }
}
