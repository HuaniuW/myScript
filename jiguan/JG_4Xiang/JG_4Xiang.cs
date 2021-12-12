using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_4Xiang : MonoBehaviour
{


    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        if (Boss)
        {
            print("  有boss ");
        }


        xianshi1.GetComponent<ParticleSystem>().Stop();
        xianshi2.GetComponent<ParticleSystem>().Stop();
        xianshi3.GetComponent<ParticleSystem>().Stop();
        xianshi4.GetComponent<ParticleSystem>().Stop();


        //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.BOSS_IS_DIE, this.name), this);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.BOSS_IS_DIE,this.GetOver);
    }

    private void GetOver(UEvent evt)
    {
        //throw new NotImplementedException();
        IsOver = true;
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.BOSS_IS_DIE, this.GetOver);
    }

    bool IsStart = false;
    // Update is called once per frame
    void Update()
    {
        if (!IsStart) return;
        GetRotation();

        JGStop();
    }




    public void GetStart()
    {
        IsStart = true;

    }

    float _rotationZ = 0;
    float _rotationSpeed = 0.4f;
    float jishi = 0;
    void GetRotation()
    {
        _rotationZ += _rotationSpeed;
        transform.localEulerAngles = new Vector3(0, 0, _rotationZ);

        //jishi += Time.deltaTime;
        //if (jishi >= 0.2f)
        //{
        //    jishi = 0;
           
           
        //}
    }

    bool IsOver = false;
    float jiaodu = 0;
    public void JGStop()
    {
        //if(this.transform.localRotation.x == 90)
        //{

        //}

        jiaodu = this.transform.localEulerAngles.z;


        if (!IsOver) {
            return;
        }
        else
        {
            if (jiaodu>=0 &&jiaodu <= 2)
            {
                jiaodu = 0;
            }else if(jiaodu >= 89 && jiaodu <= 91)
            {
                jiaodu = 90;
            }
            else if (jiaodu >= 179 && jiaodu <= 181)
            {
                jiaodu = 180;
            }
            else if (jiaodu >= 269 && jiaodu <= 271)
            {
                jiaodu = 270;
            }
        }
        


        
        if (jiaodu ==0)
        {
            //2 3 4
            diban2.SetActive(false);
            diban3.SetActive(false);
            diban4.SetActive(false);
            xianshi2.GetComponent<ParticleSystem>().Play();
            xianshi3.GetComponent<ParticleSystem>().Play();
            xianshi4.GetComponent<ParticleSystem>().Play();
            IsStart = false;
        }
        else if(jiaodu == 90){
            //4 3 1
            diban1.SetActive(false);
            diban3.SetActive(false);
            diban4.SetActive(false);
            xianshi1.GetComponent<ParticleSystem>().Play();
            xianshi3.GetComponent<ParticleSystem>().Play();
            xianshi4.GetComponent<ParticleSystem>().Play();
            IsStart = false;
        }
        else if (jiaodu == 180) {
            //3 1 2
            diban2.SetActive(false);
            diban3.SetActive(false);
            diban1.SetActive(false);
            xianshi2.GetComponent<ParticleSystem>().Play();
            xianshi3.GetComponent<ParticleSystem>().Play();
            xianshi1.GetComponent<ParticleSystem>().Play();
            IsStart = false;
        }
        else if (jiaodu == 270)
        {
            //1 2 4
            diban2.SetActive(false);
            diban1.SetActive(false);
            diban4.SetActive(false);
            xianshi2.GetComponent<ParticleSystem>().Play();
            xianshi1.GetComponent<ParticleSystem>().Play();
            xianshi4.GetComponent<ParticleSystem>().Play();
            IsStart = false;
        }

        //print("  --------> "+this.transform.localRotation+"  ----   "+this.transform.localEulerAngles);
    }




    public GameObject diban1;
    public GameObject diban2;
    public GameObject diban3;
    public GameObject diban4;



    public ParticleSystem xianshi1;
    public ParticleSystem xianshi2;
    public ParticleSystem xianshi3;
    public ParticleSystem xianshi4;


    private void HideDibans()
    {
      
    }


    public void ShowDB()
    {
        if (diban2) diban2.SetActive(true);
        if (diban3) diban3.SetActive(true);
        if (diban4) diban4.SetActive(true);
        xianshi2.GetComponent<ParticleSystem>().Play();
        xianshi3.GetComponent<ParticleSystem>().Play();
        xianshi4.GetComponent<ParticleSystem>().Play();


    }
}
