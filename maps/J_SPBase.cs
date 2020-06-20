using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class J_SPBase : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform l;
    public Transform r;
    public Light2D light2d;
    public ParticleSystem denglizi1;
    public ParticleSystem denglizi2;

    void Start()
    {
        
    }


    public float GetWidth()
    {
        if (l == null) return 0;
        return Mathf.Abs(r.position.x - l.position.x);
    }



    // Update is called once per frame
    void Update()
    {
        
    }


    public virtual void SetSD(int sd)
    {
        int n = this.transform.childCount;
        for(int i = 0; i < n - 1; i++)
        {
            if (this.transform.GetChild(i).GetComponent<SpriteRenderer>())
            {
                this.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = (sd + i) % 4;
            }
        }
    }


    public virtual int GetSD()
    {
        return this.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;
    }




    public virtual void SetLightColor()
    {
        if (light2d)
        {
            Color _theColor = GlobalTools.RandomColor();
            light2d.GetComponent<Light2D>().color = _theColor;
            light2d.GetComponent<Light2D>().intensity = 0.6f + GlobalTools.GetRandomDistanceNums(0.6F);


            //if (denglizi1) denglizi1.startColor = _theColor;
            SetDengLiziColor(_theColor);
        }
    }


    public void SetDengLiziColor(Color _theColor)
    {
        if (denglizi1)
        {
            ParticleSystem.MainModule mainModule = denglizi1.main;
            mainModule.startColor = _theColor;
            ParticleSystem.MainModule mainModule2 = denglizi2.main;
            mainModule2.startColor = _theColor;
        }
    }

    public void SetLightColorByValue(Color _color)
    {
        if (light2d) light2d.GetComponent<Light2D>().color = _color;
    }


    public Color GetLightColor()
    {
        if (!light2d) return Color.white;
        return light2d.GetComponent<Light2D>().color;
    }
}
