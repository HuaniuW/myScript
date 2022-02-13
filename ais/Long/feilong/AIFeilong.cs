using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFeilong : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetRun();
    }

    float maxints = 8;
    float n = 0;
    bool IsZhuanbian = false;
    int bian = 1;
    string tempstr = "";

    bool IsLongHou = false;
    float _z = 0;


    float RunSpeedX = 0.39f;
    float RunSpeedY = -0.03f;

    float jiali = 24;

    public void GetRun()
    {
        if (!IsZhuanbian)
        {
            IsZhuanbian = true;
            //GetComponent<AirGameBody>().RunACChange("run_1");
            GetComponent<AirGameBody>().TSACControl = true;
            GetComponent<AirGameBody>().TurnRight();

            GetComponent<AirGameBody>().GetDB().animation.FadeIn("fly_1");
        }
        //GetComponent<AirGameBody>().GetPlayerRigidbody2D().velocity = v;
        //this.gameObject.transform.position = new Vector3(this.transform.position.x + RunSpeedX, 0, 0);

        GetComponent<AirGameBody>().GetZongTuili(new Vector2(100 * jiali, 0));

        GetComponent<AirGameBody>().ControlSpeed(10);

        //if (!IsLongHou && this.transform.position.x>GlobalTools.FindObjByName("player").transform.position.x)
        //{
        //    IsLongHou = true;
        //    GetComponent<RoleAudio>().PlayAudio("longhou");
        //}



        n += Time.deltaTime;

        //if (n % 5 == 0)
        //{
        //    if (GlobalTools.GetRandomNum()>50)
        //    {
        //        GetComponent<AirGameBody>().GetDB().animation.GotoAndPlayByFrame("fly_1");
        //    }
        //    else
        //    {
        //        GetComponent<AirGameBody>().GetDB().animation.GotoAndPlayByFrame("run_3");
        //    }

        //}


        QiehuanFeixing();

        if (!IsLongHou && n >= 3.6f)
        {
            IsLongHou = true;
            GetComponent<RoleAudio>().PlayAudio("longhou");
            _z = -0.01f;
        }


        if (n >= 54)
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }

       
        return;

       
    }


    float nn = 0;


    bool IsHuaxing = false;
    bool IsShanchibang = false;
    void QiehuanFeixing()
    {
        if (GlobalTools.FindObjByName("player_jijia").transform.position.x > this.transform.position.x)
        {
            if (!IsShanchibang)
            {
                IsShanchibang = true;
                IsHuaxing = false;
                jiali = 29;
                GetComponent<AirGameBody>().GetDB().animation.GotoAndPlayByFrame("run_3");
                nn = 0;
            }
        }
        else
        {
            nn += Time.deltaTime;
            if (nn >= 2)
            {
                jiali = 24;
                nn = 0;
            }
           
            if (!IsHuaxing)
            {
                IsHuaxing = true;
                IsShanchibang = false;
                GetComponent<AirGameBody>().GetDB().animation.GotoAndPlayByFrame("fly_1");
            }

        }
    }


}
