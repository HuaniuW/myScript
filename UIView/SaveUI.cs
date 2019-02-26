using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveUI : MonoBehaviour {
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btnX;
    public Image kuang;

    //默认是取档
    bool isGetDate = true;


    List<Button> btns = new List<Button>();
    // Use this for initialization
    void Start () {
        Button[] b = {btn1,btn2,btn3};
        btns.AddRange(b);
        kuang.transform.position = btn1.transform.position;
        getRQ = btn1;
        btn1.onClick.AddListener(GetDate1);
        btn2.onClick.AddListener(GetDate2);
        btn3.onClick.AddListener(GetDate3);
        btnX.onClick.AddListener(RemoveSelf);
    }

    void GetDate1()
    {
        kuang.transform.position = btn1.transform.position;
        getRQ = btn1;
    }

    void GetDate2()
    {
        kuang.transform.position = btn2.transform.position;
        getRQ = btn2;
    }

    void GetDate3()
    {
        kuang.transform.position = btn3.transform.position;
        getRQ = btn3;
    }

    void GetDateInGame(string dateNum)
    {

    }

    void SaveDate(string dateNum)
    {

    }

    void RemoveSelf()
    {
        //this.gameObject.SetActive(false);
        //this.gameObject.SetActive(true);
        //Destroy(this.gameObject);
        DestroyImmediate(this.gameObject, true);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FindNearestQR("up");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FindNearestQR("down");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RemoveSelf();
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //print("enter");
            GetChoseObj();
        }
    }

    void GetChoseObj()
    {
        print(getRQ.name);
        if (getRQ.name == "Button2")
        {
            //GetSaveDateUI();
        }
    }



    Button getRQ = null;
    void FindNearestQR(string fx)
    {
        if (btns.Count == 0) return;
        float jl = 0;
        bool hasValue = false;
        getRQ = null;
        List<Button> tempList = new List<Button>();

        if (fx == "up")
        {
            //获取在我上方的容器list
            foreach (var rq in btns)
            {
                if (rq.interactable == true && rq.transform.position.y > kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "down")
        {
            foreach (var rq in btns)
            {
                if (rq.interactable == true && rq.transform.position.y < kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "right")
        {
            foreach (var rq in btns)
            {
                if (rq.interactable == true && rq.transform.position.x > kuang.transform.position.x)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "left")
        {
            foreach (var rq in btns)
            {
                if (rq.interactable == true && rq.transform.position.x < kuang.transform.position.x)
                {
                    tempList.Add(rq);
                }
            }
        }

        if (tempList.Count > 0)
        {
            foreach (var rq2 in tempList)
            {
                float jl2 = Vector2.Distance(rq2.transform.position, kuang.transform.position);
                if (hasValue)
                {
                    if (jl2 < jl)
                    {
                        jl = jl2;
                        getRQ = rq2;
                    }

                }
                else
                {
                    jl = jl2;
                    getRQ = rq2;
                    hasValue = true;
                }
            }
        }

        if (getRQ != null) kuang.transform.position = getRQ.transform.position;

    }
}
