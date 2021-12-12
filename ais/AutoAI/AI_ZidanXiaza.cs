using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ZidanXiaza : AI_SkillBase
{
    protected override void ACSkillShowOut()
    {
        print(" dongzuo name shijian **********************ac    " + TXName);


        GetZidanNumsArr();

        //JiangeKaikouXiaza();

        //横向子弹  先浮起 然后 直线 朝着 玩家方向
        //JianGeXianhouXiaza();

        AigeYiciXiaza();
    }


    [Header("TLPos 子弹出现 起始点")]
    public Transform TLPos;


    //几种模式 1.间隔几个 有个开口    2.单双   3.顺序

    int ZidanNums = 16;
    List<int> intArr = new List<int> { };


    void GetZidanNumsArr()
    {
        if(intArr.Count == 0)
        {
            for (int theN = 1; theN <= ZidanNums; theN++)
            {
                intArr.Add(theN);
            }
        }
    }


    //间隔开口 下载
    void JiangeKaikouXiaza()
    {
        //出现

        
        //随机 取出2个数字

       



        List<int> quchuArr = new List<int> { };
        for (int s = 0; s < 2; s++)
        {
            int n = intArr[GlobalTools.GetRandomNum(intArr.Count)];

            quchuArr.Add(n);

            intArr.Remove(n);
        }



        for (int i = 0; i < intArr.Count; i++)
        {
            GameObject skillObj = Resources.Load(TXName) as GameObject;
            GameObject skill = ObjectPools.GetInstance().SwpanObject2(skillObj);
            skill.GetComponent<JN_base>().atkObj = this.gameObject;

            //出现位置
            float __x = TLPos.position.x+1.5f* intArr[i];
            float __y = TLPos.position.y ;

            skill.transform.position = new Vector2(__x, __y);
            skill.transform.parent = this.gameObject.transform.parent;
            skill.GetComponent<TX_XiazaiZidan>().SetZidanXiazaTime(1);
        }
    }





    //间隔 先后 下砸
    void JianGeXianhouXiaza()
    {
        


        for (int i = 0; i < intArr.Count; i++)
        {
            GameObject skillObj = Resources.Load(TXName) as GameObject;
            GameObject skill = ObjectPools.GetInstance().SwpanObject2(skillObj);
            skill.GetComponent<JN_base>().atkObj = this.gameObject;

            //出现位置
            float __x = TLPos.position.x + 1.5f * intArr[i];
            float __y = TLPos.position.y;
            if (i % 2 == 1)
            {
                __y += 1;
                skill.GetComponent<TX_XiazaiZidan>().SetZidanXiazaTime(1);
            }
            else
            {
                skill.GetComponent<TX_XiazaiZidan>().SetZidanXiazaTime(1.5f);
            }

            skill.transform.position = new Vector2(__x, __y);
            skill.transform.parent = this.gameObject.transform.parent;
           
        }


    }




    //挨个依次下砸
    void AigeYiciXiaza()
    {
        

        for (int i = 0; i < intArr.Count; i++)
        {
            GameObject skillObj = Resources.Load(TXName) as GameObject;
            GameObject skill = ObjectPools.GetInstance().SwpanObject2(skillObj);
            skill.GetComponent<JN_base>().atkObj = this.gameObject;

            //出现位置
            float __x = TLPos.position.x + 1.5f * intArr[i];
            float __y = TLPos.position.y;
            //if (i % 2 == 1)
            //{
            //    __y += 1;
            //    skill.GetComponent<TX_XiazaiZidan>().SetZidanXiazaTime(1);
            //}
            //else
            //{
            //    skill.GetComponent<TX_XiazaiZidan>().SetZidanXiazaTime(1.5f);
            //}

            skill.GetComponent<TX_XiazaiZidan>().SetZidanXiazaTime(1+0.2f*i);

            skill.transform.position = new Vector2(__x, __y);
            skill.transform.parent = this.gameObject.transform.parent;

        }

    }



}
