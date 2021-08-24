using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JingBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Header("自定义 中心点")]
    public Transform CenterPos;

    public float GetCenterPosY()
    {
        return -CenterPos.position.y;
    }

    [Header("可变地板")]
    public Transform Diban1;
    public virtual int GetSD()
    {
        int sd = 0;
        if(Diban1&& Diban1.GetComponent<Ferr2DT_PathTerrain>())
        {
            sd = Diban1.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder;
        }
        return sd;
    }

    public virtual void SetSD(int sd)
    {
        if (Diban1) {
            Diban1.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd;
            print(sd+" *********************sd   "+ Diban1.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder);
        }
        
      
    }


    public Transform LDPos;
    public Transform RTPos;


    public float GetWidth()
    {
        if (LDPos && RTPos)
        {
            return Mathf.Abs(LDPos.position.x - RTPos.position.x);
        }
        return 0;
    }

    public float GetHeight()
    {
        if (LDPos && RTPos)
        {
            return Mathf.Abs(LDPos.position.y - RTPos.position.y);
        }
        return 0;
    }

    
}
