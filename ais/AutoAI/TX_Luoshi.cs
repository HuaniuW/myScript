using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_Luoshi : MonoBehaviour
{
    [Header("下落石块")]
    public GameObject XiaLuoShitou;
    [Header("落石 粒子")]
    public ParticleSystem LuoshiLizi;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void GetStart(GameObject AtkObj)
    {
        if (LuoshiLizi && !LuoshiLizi.isPlaying) LuoshiLizi.Play();
         IsDisJishi = true;
        DisJishi = 0;
        XiaLuoShitou.transform.position = new Vector2(0,GlobalTools.GetRandomDistanceNums(2));
        XiaLuoShitou.GetComponent<Rigidbody2D>().gravityScale = 1 + GlobalTools.GetRandomDistanceNums(2);
        XiaLuoShitou.GetComponent<JN_base>().atkObj = AtkObj;
        //HitObj.transform.parent = this.transform.parent;


    }


    //public GameObject GetHitObj()
    //{
    //    GetStart();
    //    return HitObj;
    //}

    bool IsDisJishi = false;
    [Header("移除时间")]
    public float DisTimes = 3;
    float DisJishi = 0;
    private void Update()
    {
        if (IsDisJishi)
        {
            DisJishi += Time.deltaTime;
            if(DisJishi>= DisTimes)
            {

            }
        }
    }

}
