using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_Konglei : DD_Base
{
    //空中地雷

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void GetStart()
    {
        if (!IsStart)
        {
            IsStart = true;
            //TX_Tuijin1.Play();
            JinGaoStart();
            //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.LAIXI_JINGGAO, this.GetJinggao);
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.LAIXI_JINGGAO, null), this);
        }
        else
        {
            ZibaoJishi();
            LaixiJingao();
        }
    }



    protected override void LaixiJingao()
    {
        if (TX_LaixiJinggao)
        {
            TX_LaixiJinggao.transform.position = new Vector3(player.transform.position.x + 28, this.transform.position.y,this.transform.position.z);
            //if (TX_LaixiJinggao.transform.position.x < JinggaoXObj.position.x)
            //{
            //    TX_LaixiJinggao.transform.position = new Vector2(JinggaoXObj.position.x, this.transform.position.y);
            //}
            //TX_LaixiJinggao.transform.localScale = new Vector3(4, 4, 4);

            //TX_LaixiJinggao.GetComponent<ParticleSystem>().startSize = 2;

            if (this.transform.position.x <= TX_LaixiJinggao.transform.position.x)
            {
                ObjectPools.GetInstance().DestoryObject2(TX_LaixiJinggao);
            }
        }
    }
}
