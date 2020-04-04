using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_LuanRen : MonoBehaviour
{
    [Header("增加的硬直")]
    public float AddYingzhiNums = 1000;


    public GameObject tx1;
    public GameObject tx2;
    public GameObject tx3;
    public GameObject tx4;
    public GameObject tx5;
    public GameObject tx6;




    //public ParticleSystem yanmu1;
    //public ParticleSystem yanmu2;
    //public ParticleSystem yanmu3;
    //public ParticleSystem yanmu4;
    //public ParticleSystem yanmu5;
    //public ParticleSystem yanmu6;

    public GameObject hitkuai1;
    public GameObject hitkuai2;
    public GameObject hitkuai3;
    public GameObject hitkuai4;
    public GameObject hitkuai5;
    public GameObject hitkuai6;


    

    // Start is called before the first frame update
    void Start()
    {
        print("start!!!!!!!!");
        //if (!atkObj) atkObj = GetComponent<JN_base>().atkObj;
        //AddYZ();
        //GetAtkObjInHitKuai();
    }

  

    //void HideHitKuai()
    //{
    //    if (hitkuai1) hitkuai1.SetActive(false);
    //    if (hitkuai2) hitkuai2.SetActive(false);
    //    if (hitkuai3) hitkuai3.SetActive(false);
    //    if (hitkuai4) hitkuai4.SetActive(false);
    //    if (hitkuai5) hitkuai5.SetActive(false);
    //    if (hitkuai6) hitkuai6.SetActive(false);
    //}


    //void ShowHitKuai()
    //{
    //    if (hitkuai1) hitkuai1.SetActive(true);
    //    if (hitkuai2) hitkuai2.SetActive(true);
    //    if (hitkuai3) hitkuai3.SetActive(true);
    //    if (hitkuai4) hitkuai4.SetActive(true);
    //    if (hitkuai5) hitkuai5.SetActive(true);
    //    if (hitkuai6) hitkuai6.SetActive(true);
    //}



    public void GetStart()
    {
        AddYZ();
    }

    //int inNums = 0;
    void GetAtkObjInHitKuai()
    {
        //inNums++;
        //print("   ????? atkobj   "+atkObj +"   nums "+inNums );
        //if(atkObj)print("     TEAM    " +atkObj.GetComponent<RoleDate>().team);


        //AddYZ();


        //tx1.GetComponent<JN_base>().atkObj = atkObj;
        //tx2.GetComponent<JN_base>().atkObj = atkObj;
        //tx3.GetComponent<JN_base>().atkObj = atkObj;
        //tx4.GetComponent<JN_base>().atkObj = atkObj;
        //tx5.GetComponent<JN_base>().atkObj = atkObj;
        //tx6.GetComponent<JN_base>().atkObj = atkObj;


        //tx1.GetComponent<JN_Date>().team = atkObj.GetComponent<RoleDate>().team;
        //tx2.GetComponent<JN_Date>().team = atkObj.GetComponent<RoleDate>().team;
        //tx3.GetComponent<JN_Date>().team = atkObj.GetComponent<RoleDate>().team;
        //tx4.GetComponent<JN_Date>().team = atkObj.GetComponent<RoleDate>().team;
        //tx5.GetComponent<JN_Date>().team = atkObj.GetComponent<RoleDate>().team;
        //tx6.GetComponent<JN_Date>().team = atkObj.GetComponent<RoleDate>().team;



        //hitkuai1.GetComponent<HitKuai>().GetTXObj(tx1.gameObject);
        //hitkuai2.GetComponent<HitKuai>().GetTXObj(tx2.gameObject);
        //hitkuai3.GetComponent<HitKuai>().GetTXObj(tx3.gameObject);
        //hitkuai4.GetComponent<HitKuai>().GetTXObj(tx4.gameObject);
        //hitkuai5.GetComponent<HitKuai>().GetTXObj(tx5.gameObject);
        //hitkuai6.GetComponent<HitKuai>().GetTXObj(tx6.gameObject);

        //hitkuai1.GetComponent<HitKuai>().GetTXObj(gameObject);
        //hitkuai2.GetComponent<HitKuai>().GetTXObj(gameObject);
        //hitkuai3.GetComponent<HitKuai>().GetTXObj(gameObject);
        //hitkuai4.GetComponent<HitKuai>().GetTXObj(gameObject);
        //hitkuai5.GetComponent<HitKuai>().GetTXObj(gameObject);
        //hitkuai6.GetComponent<HitKuai>().GetTXObj(gameObject);


        print("----------->1");





        GameObject atkObj = GetComponent<JN_base>().atkObj;
        //hitkuai1.GetComponent<HitKuai>().GetTXObj(null, false, 1, atkObj);
        //hitkuai2.GetComponent<HitKuai>().GetTXObj(null, false, 1, atkObj);
        //hitkuai3.GetComponent<HitKuai>().GetTXObj(null, false, 1, atkObj);
        //hitkuai4.GetComponent<HitKuai>().GetTXObj(null, false, 1, atkObj);
        //hitkuai5.GetComponent<HitKuai>().GetTXObj(null, false, 1, atkObj);
        //hitkuai6.GetComponent<HitKuai>().GetTXObj(null, false, 1, atkObj);
    }

    void ReSetAll()
    {
        IsAddYz = false;
        IsTX1 = false;
        IsTX2 = false;
        IsTX3 = false;
        IsTX4 = false;
        IsTX5 = false;
        IsTX6 = false;

        IsHitKuai1 = false;
        IsHitKuai2 = false;
        IsHitKuai3 = false;
        IsHitKuai4 = false;
        IsHitKuai5 = false;
        IsHitKuai6 = false;
    }


    bool IsTX1 = false;
    bool IsTX2 = false;
    bool IsTX3 = false;
    bool IsTX4 = false;
    bool IsTX5 = false;
    bool IsTX6 = false;

    bool IsHitKuai1 = false;
    bool IsHitKuai2 = false;
    bool IsHitKuai3 = false;
    bool IsHitKuai4 = false;
    bool IsHitKuai5 = false;
    bool IsHitKuai6 = false;

    void SetAtkObj()
    {
        //用在addyz后面 addYZ里面有 atkObj
        if (tx1)
        {
            //if (!IsTX1)
            //{
            //    IsTX1 = true;
            //    print("1   "+tx1);
            //    tx1.GetComponent<JN_base>().atkObj = atkObj;
            //    tx1.GetComponent<JN_Date>().team = atkObj.GetComponent<RoleDate>().team;
                
            //}



            if (hitkuai1.activeSelf)
            {
                if (!IsHitKuai1)
                {
                    IsHitKuai1 = true;
                    print("hitkuai--->  1");
                    hitkuai1.GetComponent<HitKuai>().GetTXObj(this.gameObject);
                }
                
            }

            //if (hitkuai1) {

                

            //    if (!IsHitKuai1)
            //    {
            //        IsHitKuai1 = true;
            //        hitkuai1.GetComponent<HitKuai>().GetTXObj(tx1.gameObject);
            //    }
                
            //}
            
        }

        if (tx2)
        {
            //if (!IsTX2)
            //{
            //    IsTX2 = true;
            //    print("2    "+tx2);
            //    tx2.GetComponent<JN_base>().atkObj = atkObj;
            //    tx2.GetComponent<JN_Date>().team = atkObj.GetComponent<RoleDate>().team;
                
            //}

            if (hitkuai2.activeSelf)
            {
                if (!IsHitKuai2)
                {
                    IsHitKuai2 = true;
                    print("hitkuai--->  2");
                    hitkuai2.GetComponent<HitKuai>().GetTXObj(this.gameObject);
                }

            }
            //if (hitkuai2)
            //{
            //    if (!IsHitKuai2)
            //    {
            //        IsHitKuai2 = true;
            //        hitkuai2.GetComponent<HitKuai>().GetTXObj(tx1.gameObject);
            //    }

            //}

        }

        if (tx3)
        {
            //if (!IsTX3)
            //{
            //    IsTX3 = true;
            //    print("3    "+tx3);
            //    tx3.GetComponent<JN_base>().atkObj = atkObj;
            //    tx3.GetComponent<JN_Date>().team = atkObj.GetComponent<RoleDate>().team;
                
            //}

            if (hitkuai3.activeSelf)
            {
                if (!IsHitKuai3)
                {
                    IsHitKuai3 = true;
                    print("hitkuai--->  3");
                    hitkuai3.GetComponent<HitKuai>().GetTXObj(this.gameObject);
                }

            }
        }


        if (tx4)
        {
            //if (!IsTX4)
            //{
            //    IsTX4 = true;
            //    print("4    "+tx4);
            //    tx4.GetComponent<JN_base>().atkObj = atkObj;
            //    tx4.GetComponent<JN_Date>().team = atkObj.GetComponent<RoleDate>().team;
             
            //}
            if (hitkuai4.activeSelf)
            {
                if (!IsHitKuai4)
                {
                    IsHitKuai4 = true;
                    print("hitkuai--->  4");
                    hitkuai4.GetComponent<HitKuai>().GetTXObj(this.gameObject);
                }

            }
        }


        if (tx5)
        {
            //if (!IsTX5)
            //{
            //    IsTX5 = true;
            //    print("5    "+tx5);
            //    tx5.GetComponent<JN_base>().atkObj = atkObj;
            //    tx5.GetComponent<JN_Date>().team = atkObj.GetComponent<RoleDate>().team;
                
            //}
            if (hitkuai5.activeSelf)
            {
                if (!IsHitKuai5)
                {
                    IsHitKuai5 = true;
                    print("hitkuai--->  5");
                    hitkuai5.GetComponent<HitKuai>().GetTXObj(this.gameObject);
                }

            }
        }

        if (tx6)
        {
            //if (!IsTX6)
            //{
            //    IsTX6 = true;
            //    print("6    "+tx6);
            //    tx6.GetComponent<JN_base>().atkObj = atkObj;
            //    tx6.GetComponent<JN_Date>().team = atkObj.GetComponent<RoleDate>().team;
                
            //}
            if (hitkuai6.activeSelf)
            {
                if (!IsHitKuai6)
                {
                    IsHitKuai6 = true;
                    print("hitkuai--->  6");
                    hitkuai6.GetComponent<HitKuai>().GetTXObj(this.gameObject);
                    
                }

            }
        }



    }

    




   

    private void OnEnable()
    {
        //HideHitKuai();

        print("atkObj    "+atkObj+"   222    "+GetComponent<JN_base>().atkObj);
        ReSetAll();
    }

    GameObject atkObj;


   

    bool IsAddYz = false;
    //提高硬直
    void AddYingzhi()
    {
        print("-------------------------------> 增加 硬直 ！！！");
        atkObj.GetComponent<RoleDate>().addYZ(AddYingzhiNums);
        GetComponent<TheTimer>().TimesAdd(1,YZHYCallBack);
    }

    void YZHYCallBack(float n)
    {
        atkObj.GetComponent<RoleDate>().hfYZ(AddYingzhiNums);
    }

    private void OnDisable()
    {
        //atkObj.GetComponent<RoleDate>().hfYZ(AddYingzhiNums);
        ReSetAll();
    }

    // Update is called once per frame
    void Update()
    {
        AddYZ();
    }



    void AddYZ()
    {
        if (!IsAddYz)
        {
            IsAddYz = true;
            print("-------------///////////////////////////-----是不是先进来的？？？？？");
            atkObj = GetComponent<JN_base>().atkObj;
            AddYingzhi();
            //ShowHitKuai();
            //GetAtkObjInHitKuai();
            print("----------->2");
        }

        SetAtkObj();
    }
}
