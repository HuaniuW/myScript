using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBBase : MonoBehaviour
{
    [Header("左上点")]
    public Transform tl;

    [Header("右下点")]
    public Transform rd;

    public GameObject light2d;

    public GameObject diban1;
    public GameObject diban2;
    public GameObject diban3;
    public GameObject diban4;



    [Header("上面的 连接点")]
    public Transform lianjiedianU;

    [Header("下面的 连接点")]
    public Transform lianjiedianD;

    [Header("右边的 连接点")]
    public Transform lianjiedianR;


    [Header("左边的 连接点")]
    public Transform lianjiedianL;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public float GetWidth()
    {
        return Mathf.Abs(tl.position.x - rd.position.x);
    }

    public float GetHight()
    {
        return Mathf.Abs(tl.position.y - rd.position.y);
    }



    public void CreateJing(string type)
    {
        //生成景
    }

  







    public virtual Vector2 GetRightPos()
    {
        return lianjiedianR.position;
    }

    public virtual Vector2 GetLeftPos()
    {
        return lianjiedianL.position;
    }


    public Vector2 GetUpPos()
    {
        return lianjiedianU.position;
    }


    public Vector2 GetDownPos()
    {
        return lianjiedianD.position;
    }



   


    

    //设置深度
    public virtual void SetSD(int sd)
    {
        if (diban1) diban1.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd;
        if (diban2) diban2.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd+1;
        if (diban3) diban3.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd + 2;
        if (diban4) diban4.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd + 3;
    }


    public virtual int GetSD()
    {
        return diban1.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder;
    }

    public virtual void SetLightColor()
    {
        //if(light2d) light2d.GetComponent<Light>()
    }


}
