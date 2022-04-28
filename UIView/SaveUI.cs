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
        SetBtn();
        ShowSaveDateMsg();
    }

    void SetBtn()
    {
        Button[] b = { btn1, btn2, btn3 };
        btns.AddRange(b);
        kuang.transform.position = btn1.transform.position;
        getRQ = btn1;
        btn1.onClick.AddListener(GetDate1);
        btn2.onClick.AddListener(GetDate2);
        btn3.onClick.AddListener(GetDate3);
        btnX.onClick.AddListener(RemoveSelf);
    }


    UserDate date1;
    UserDate date2;
    UserDate date3;
    void ShowSaveDateMsg()
    {
        date1 = GameSaveDate.GetInstance().GetSaveDateByName(btn1.name);
        date2 = GameSaveDate.GetInstance().GetSaveDateByName(btn2.name);
        date3 = GameSaveDate.GetInstance().GetSaveDateByName(btn3.name);
        ShowBtnMsg(btn1, date1);
        ShowBtnMsg(btn2, date2);
        ShowBtnMsg(btn3, date3);
    }

    void ShowBtnMsg(Button btn,UserDate date) {
        Text text = btn.transform.Find("Text").GetComponent<Text>();
        if (date != null)
        {
            text.text = date.userName;
        }
        else {
            text.text = "no date";
        }
    }

    void GetSetSaveGameByBtnName(string btnName)
    {
        if (isGetDate)
        {
            //取
            if (GameSaveDate.GetInstance().GetSaveDateByName(btnName) != null)
            {
                GlobalSetDate.instance.CurrentUserDate = GameSaveDate.GetInstance().GetSaveDateByName(btnName);
                UserDate t = GlobalSetDate.instance.CurrentUserDate;
                GlobalSetDate.instance.isInFromSave = true;
                //调用进入游戏
                GlobalSetDate.instance.GetGameDateStart();
            }
        }
        else {
            //存
            if (GlobalSetDate.instance.CurrentUserDate != null)
            {
                GameSaveDate.GetInstance().SaveDateByURLName(btnName, GlobalSetDate.instance.CurrentUserDate);
            }
            else {
                //给弹框
            }
            
        }
        RemoveSelf();
    }


    void GetDate1()
    {
        kuang.transform.position = btn1.transform.position;
        getRQ = btn1;
        GetSetSaveGameByBtnName(btn1.name);
    }
    

    void GetDate2()
    {
        kuang.transform.position = btn2.transform.position;
        getRQ = btn2;
        GetSetSaveGameByBtnName(btn2.name);
    }

    void GetDate3()
    {
        kuang.transform.position = btn3.transform.position;
        getRQ = btn3;
        GetSetSaveGameByBtnName(btn3.name);
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
            print("enter");
            GetChoseObj();
        }
    }

    void GetChoseObj()
    {
        print(getRQ.name);
        if (getRQ.name == "date1")
        {
            GetDate1();
        }
        else if (getRQ.name == "date2")
        {
            GetDate2();
        }
        else if (getRQ.name == "date3")
        {
            GetDate3();
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
