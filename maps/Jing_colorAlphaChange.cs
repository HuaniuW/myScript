using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jing_colorAlphaChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        YsColor = obj.GetComponent<SpriteRenderer>().color;
        theAlpha = YsColor.a;
    }

    // Update is called once per frame
    void Update()
    {
        AlphaChange();
    }

    public GameObject obj;

    float MinAlpha = 0.6f;
    float MaxAlpha = 1;

    float ChangeNums = 0.06f;

    float theAlpha = 1;
    float targetAlphaNums = 1;
    Color YsColor;
    void AlphaChange()
    {
        if (theAlpha > 0.98f)
        {
            theAlpha = 0.97f;
            targetAlphaNums = MinAlpha;
        }
        else if (theAlpha < 0.61f)
        {
            theAlpha = 0.62f;
            targetAlphaNums = MaxAlpha;
        }
        theAlpha += (targetAlphaNums - theAlpha) * ChangeNums;
        //print(" theAlpha>>>   "+ theAlpha);
        obj.GetComponent<SpriteRenderer>().color = new Color(YsColor.r, YsColor.g, YsColor.b, theAlpha);
    }
}
