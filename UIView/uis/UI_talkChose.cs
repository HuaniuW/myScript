using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_talkChose : MonoBehaviour
{
    public GameObject bar1;
    public GameObject bar2;
    public GameObject bar3;


    float bar1Y = 0;
    float bar2Y = 0;
    float bar3Y = 0;


    float cbar1y = 0;
    float cbar2y = 0;
    float cbar3y = 0;

    //float cbar1z = 0;
    //float cbar2z = 0;
    //float cbar3z = 0;


    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }

    private void OnEnable()
    {
        Init();
    }


    int MaxLength = 3;
    public void GetStrMsg(string msg)
    {
        print(" msg "+msg);
        string[] strArr = msg.Split('@');
        MaxLength = strArr.Length;
        
        bar1.GetComponent<UI_txtBar>().GetTxtMsg(strArr[0].Split('#')[0], strArr[0].Split('#')[1]);
        bar2.GetComponent<UI_txtBar>().GetTxtMsg(strArr[1].Split('#')[0], strArr[1].Split('#')[1]);
        if (MaxLength == 3)
        {
            bar3.GetComponent<UI_txtBar>().GetTxtMsg(strArr[2].Split('#')[0], strArr[2].Split('#')[1]);
        }
        else
        {
            bar3.GetComponent<CanvasGroup>().alpha = 0;
        }
        
        //bar1.GetComponent<UI_txtBar>().GetBeChose();
    }



    private void Init()
    {
        bar1.GetComponent<CanvasGroup>().alpha = 0;
        bar2.GetComponent<CanvasGroup>().alpha = 0;
        bar3.GetComponent<CanvasGroup>().alpha = 0;

        bar1Y = bar1.transform.position.y;
        bar2Y = bar2.transform.position.y;
        bar3Y = bar3.transform.position.y;
        bar1.transform.position = new Vector3(bar1.transform.position.x, bar1.transform.position.y - 0.1f, bar1.transform.position.z);
        bar2.transform.position = new Vector3(bar2.transform.position.x, bar2.transform.position.y - 0.1f, bar2.transform.position.z);
        bar3.transform.position = new Vector3(bar3.transform.position.x, bar3.transform.position.y - 0.1f, bar3.transform.position.z);
        cbar1y = bar1.transform.position.y;
        cbar2y = bar2.transform.position.y;
        cbar3y = bar3.transform.position.y;

        //bar1.GetComponent<UI_txtBar>().Init();
        //bar1.GetComponent<UI_txtBar>().GetBeChose();
    }

    bool IsStartACOver = false;
    float JLTimes = 0;
    float hdSpeed = 0.1f;
    void StartAC()
    {
        if (IsStartACOver) return;
        JLTimes += Time.deltaTime;
        //if(JLTimes>=0.02f) IsStartACOver = true;

        if (bar1.GetComponent<CanvasGroup>().alpha >= 0.96f)
        {
            bar1.GetComponent<CanvasGroup>().alpha =1;
        }
        else
        {
            bar1.GetComponent<CanvasGroup>().alpha += (1 - bar1.GetComponent<CanvasGroup>().alpha) * hdSpeed;
        }


        //if (bar1.transform.position.y>= bar1Y-0.06f)
        //{
        //    bar1.transform.position = new Vector3(bar1.transform.position.x, bar1Y,0);
        //}
        //else
        //{
        //    cbar1y += (bar1Y - cbar1y) * 0.1f;
        //    bar1.transform.position = new Vector3(bar1.transform.position.x, cbar1y,0);
        //}
        


        if (JLTimes >= 0.1f){
            if (bar2.GetComponent<CanvasGroup>().alpha >= 0.96f)
            {
                bar2.GetComponent<CanvasGroup>().alpha = 1;
                if(MaxLength == 2) IsStartACOver = true;
            }
            else
            {
                bar2.GetComponent<CanvasGroup>().alpha += (1 - bar2.GetComponent<CanvasGroup>().alpha) * hdSpeed;
            }

            //if (bar2.transform.position.y >= bar2Y - 0.06f)
            //{
            //    bar2.transform.position = new Vector3(bar2.transform.position.x, bar2Y, 0);
            //}
            //else
            //{
            //    cbar2y += (bar2Y - cbar2y) * 0.1f;
            //    bar2.transform.position = new Vector3(bar2.transform.position.x, cbar2y, 0);
            //}
        }


        if (MaxLength == 3 && JLTimes >= 0.2f)
        {
            if (bar3.GetComponent<CanvasGroup>().alpha >= 0.96f)
            {
                bar3.GetComponent<CanvasGroup>().alpha = 1;

                IsStartACOver = true;
                //print("*******************************");
            }
            else
            {
                bar3.GetComponent<CanvasGroup>().alpha += (1 - bar3.GetComponent<CanvasGroup>().alpha) * hdSpeed;
            }

            //if (bar3.transform.position.y >= bar3Y - 0.06f)
            //{
            //    bar3.transform.position = new Vector3(bar3.transform.position.x, bar3Y, 0);
            //}
            //else
            //{
            //    cbar3y += (bar3Y - cbar3y) * 0.1f;
            //    bar3.transform.position = new Vector3(bar3.transform.position.x, cbar3y, 0);
            //}
        }


    }

    void NotChoseAll()
    {
        bar1.GetComponent<UI_txtBar>().NotBeChose();
        bar2.GetComponent<UI_txtBar>().NotBeChose();
        if(MaxLength == 3)bar3.GetComponent<UI_txtBar>().NotBeChose();
    }


    const string VERTICAL = "Vertical";
    float verticalDirection;

    bool IsXZ = false;

    int hasChoseNums = 1;
    void CanChose()
    {

        verticalDirection = Input.GetAxis(VERTICAL);
        if (verticalDirection > -0.6f && verticalDirection < 0.6f) IsXZ = false;


        if (Input.GetKeyDown(KeyCode.W)||(!IsXZ&& verticalDirection>0.6f))
        {
            IsXZ = true;
            if (hasChoseNums > 1)
            {
                hasChoseNums--;
                GetChoseBar();
            }
        }else if (Input.GetKeyDown(KeyCode.S) || (!IsXZ && verticalDirection < -0.6f))
        {
            IsXZ = true;
            if (hasChoseNums < MaxLength)
            {
                hasChoseNums++;
                //print("hasChoseNums     "+ hasChoseNums);
                GetChoseBar();
            }
        }


        if (!IsStartACOver) return;

        if (Input.GetKeyDown(KeyCode.J)|| Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            //print("  J 确定    Id  ");
            GetChose();
            RemoveSelf();
        }
       
    }

    public void RemoveSelf()
    {
        ObjectPools.GetInstance().DestoryObject2(this.gameObject);
    }



    void GetChose()
    {
        if (hasChoseNums == 1)
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.NEXT_ID, bar1.GetComponent<UI_txtBar>().GetNextId()), this);
            if (bar1.GetComponent<UI_txtBar>().GetEventStr()!="")
            {
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHOSE_EVENT, bar1.GetComponent<UI_txtBar>().GetEventStr()), this);
            }
        }
        else if (hasChoseNums == 2)
        {
            //bar2.GetComponent<UI_txtBar>().GetBeChose();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.NEXT_ID, bar2.GetComponent<UI_txtBar>().GetNextId()), this);
            if (bar2.GetComponent<UI_txtBar>().GetEventStr() != "")
            {
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHOSE_EVENT, bar2.GetComponent<UI_txtBar>().GetEventStr()), this);
            }
        }
        else if (hasChoseNums == 3)
        {
            //bar3.GetComponent<UI_txtBar>().GetBeChose();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.NEXT_ID, bar3.GetComponent<UI_txtBar>().GetNextId()), this);
            if (bar3.GetComponent<UI_txtBar>().GetEventStr() != "")
            {
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHOSE_EVENT, bar3.GetComponent<UI_txtBar>().GetEventStr()), this);
            }
        }
    }

    void GetChoseBar()
    {
        NotChoseAll();

        if (hasChoseNums == 1)
        {
            bar1.GetComponent<UI_txtBar>().GetBeChose();
        }else if (hasChoseNums == 2)
        {
            bar2.GetComponent<UI_txtBar>().GetBeChose();
        }
        else if(hasChoseNums == 3)
        {
            bar3.GetComponent<UI_txtBar>().GetBeChose();
        }
    }


    // Update is called once per frame
    void Update()
    {
        StartAC();
        CanChose();
    }
}
