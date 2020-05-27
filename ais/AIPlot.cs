﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public ParticleSystem yanmu1;
    public ParticleSystem yanmu2;
    public ParticleSystem yanmu3;
    public ParticleSystem yanmu4;
    public ParticleSystem yanmu5;

    // Update is called once per frame
    void Update()
    {
        if(IsPlotStart) GetRun();
    }

    bool IsPlotStart = false;
    public void GetPlotStart()
    {
        if(!IsPlotStart) IsPlotStart = true;
    }


    float maxints = 8;
    float n = 0;
    bool IsZhuanbian = false;
    int bian = 1;
    string tempstr = "";

    Vector2 v = new Vector2(2,4);


    void YanmuStop2()
    {
        yanmu1.Stop();
        yanmu2.Stop();
        yanmu3.Stop();
        yanmu4.Stop();
        yanmu5.Stop();
    }


    bool IsLongHou = false;
    float _z = 0;


    //飞的动作
    public void GetRun()
    {
        if (!IsZhuanbian)
        {
            IsZhuanbian = true;
            //GetComponent<AirGameBody>().RunACChange("run_1");
            GetComponent<AirGameBody>().TSACControl = true;
            GetComponent<AirGameBody>().TurnRight();

            GetComponent<AirGameBody>().GetDB().animation.FadeIn("run_1");
            


            //GetComponent<AirGameBody>().GetBoneColorChange(new Color(0.2F,0.2F,0.2F));
            yanmu1.Play();
            yanmu2.Play();
            yanmu3.Play();
            yanmu4.Play();
            yanmu5.Play();
        }
        //GetComponent<AirGameBody>().GetPlayerRigidbody2D().velocity = v;
        this.gameObject.transform.position = new Vector3(this.transform.position.x+0.07f,this.transform.position.y+0.16f, this.transform.position.z+_z);


        //if (!IsLongHou && this.transform.position.x>GlobalTools.FindObjByName("player").transform.position.x)
        //{
        //    IsLongHou = true;
        //    GetComponent<RoleAudio>().PlayAudio("longhou");
        //}



        n += Time.deltaTime;

        if (!IsLongHou && n>=2.6f)
        {
            IsLongHou = true;
            GetComponent<RoleAudio>().PlayAudio("longhou");
            _z = -0.01f;
        }


        if (n >= 14)
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }

        if (n >= maxints)
        {
            //    n = 0;
            //    //YanmuStop2();
            //    //GetComponent<AirGameBody>().isAcing = true;
            //    if (GetComponent<GameBody>().GetDB().animation.lastAnimationName == "fly_1")
            //    {
            //        //GetComponent<GameBody>().GetDB().animation.FadeIn("run_3");

            //        //GetComponent<AirGameBody>().RunACChange("run_1");
            //        //GetComponent<AirGameBody>().GetAcMsg("run_3");
            //        GetComponent<AirGameBody>().GetDB().animation.FadeIn("run_1", 0.5f);
            //        v = new Vector2(4, 3f);
            //    }
            //    else
            //    {
            //        //GetComponent<GameBody>().GetDB().animation.FadeIn("fly_1");
            //        //GetComponent<AirGameBody>().RunACChange("fly_1");
            //        //GetComponent<AirGameBody>().GetAcMsg("fly_1");
            //        GetComponent<AirGameBody>().GetDB().animation.FadeIn("fly_1", 0.5f);
            //        v = new Vector2(4, 1);
            //        YanmuStop2();
            //    }

            YanmuStop2();
        }


        //v = new Vector2(4, 4);
        //GetComponent<AirGameBody>().RunRight(4f);
        //GetComponent<AirGameBody>().RunY(4f);


        //print(" v22222    "+GetComponent<AirGameBody>().GetPlayerRigidbody2D().velocity+"  -----0000    "+v);
        //GetComponent<AirGameBody>().GetPlayerRigidbody2D().velocity = v;
        //print("  v "+v);
        return;

        if (GetComponent<AirGameBody>().isAcing)
        {
            if (GetComponent<GameBody>().GetDB().animation.lastAnimationName == "fly_1")
            {
                tempstr = "fly_1";
            }
            else
            {
                tempstr = "run_3";
            }
        }
        else
        {
            if (n >= maxints)
            {
                n = 0;
                if (GetComponent<GameBody>().GetDB().animation.lastAnimationName == "fly_1")
                {
                    GetComponent<AirGameBody>().GetAcMsg("run_3");
                    bian = 1;
                    //GetComponent<AirGameBody>().RunACChange("run_3");
                }
                else
                {
                    GetComponent<AirGameBody>().GetAcMsg("fly_1");
                    //GetComponent<AirGameBody>().RunACChange("fly_1");
                }
            }
        }

        GetComponent<AirGameBody>().RunRight(1f);
        //GetComponent<AirGameBody>().RunACChange("fly_1");
    }
    //飞到哪
    //烟幕

}