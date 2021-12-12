using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_4XiangChufaJG : JG_ChufaBase
{
    public GameObject JG_4Xiang;
    protected override void Chufa()
    {
        Show4XiangQiang();
    }

    public GameObject Boss;
    protected override void GetStart()
    {
        StartCoroutine(IRemove(this.gameObject));
    }


  
    protected IEnumerator IRemove(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        if (Boss == null)
        {
            //RemoveSelf();
            DestroyImmediate(this.gameObject, true);
        }
    }



    void Show4XiangQiang()
    {
        //角色速度降低为0
        GlobalTools.FindObjByName("player").GetComponent<PlayerGameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
        GlobalTools.FindObjByName("player").GetComponent<PlayerGameBody>().Bianbai();
        //显示 隐身特效 声音播放

        //延迟 显示 地板
        if (JG_4Xiang)
        {
            JG_4Xiang.GetComponent<JG_4Xiang>().ShowDB();
            StartCoroutine(IStartXuanzhuan());
        }
    }


    void JGStart()
    {
       
        if (JG_4Xiang)
        {
            JG_4Xiang.GetComponent<JG_4Xiang>().GetStart();
            RemoveSelf();
        }
    }


    void Stop4XiangJG()
    {
        JG_4Xiang.GetComponent<JG_4Xiang>().JGStop();
    }

    public IEnumerator IStartXuanzhuan()
    {
        yield return new WaitForSeconds(1);
        JGStart();
    }


}
