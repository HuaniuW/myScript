using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIBag : MonoBehaviour {
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Transform image;

    public RectTransform mianban1;
    public RectTransform mianban2;
    public RectTransform mianban3;
    // Use this for initialization
    void Start () {
        btn1.onClick.AddListener(Click1);
        btn2.onClick.AddListener(Click2);
        btn3.onClick.AddListener(Click3);
        // btn1.interactable = false;
        mianbanHide(mianban2);
        mianbanHide(mianban3);

        mianbanHide(this.GetComponent<RectTransform>());

    }
	
    void Click1()
    {
        mianbanShow(mianban1);
        mianbanHide(mianban2);
        mianbanHide(mianban3);
    }
    void Click2()
    {
        mianbanHide(mianban1);
        mianbanShow(mianban2);
        mianbanHide(mianban3);
    }
    void Click3()
    {
        mianbanHide(mianban1);
        mianbanHide(mianban2);
        mianbanShow(mianban3);
    }

    void mianbanShow(RectTransform mianban)
    {
        mianban.GetComponent<CanvasGroup>().alpha = 1;
        mianban.GetComponent<CanvasGroup>().interactable = true; //相互作用
        mianban.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    void mianbanHide(RectTransform mianban)
    {
        mianban.GetComponent<CanvasGroup>().alpha = 0;
        mianban.GetComponent<CanvasGroup>().interactable = false; //相互作用
        mianban.GetComponent<CanvasGroup>().blocksRaycasts = false;//块光纤投射
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 mouseDown = eventData.position;    //记录鼠标按下时的屏幕坐标
       // Vector2 mouseUguiPos = new Vector2();   //定义一个接收返回的ugui坐标
        print(mouseDown);
        //RectTransformUtility.ScreenPointToLocalPointInRectangle()：把屏幕坐标转化成ugui坐标
        //canvas：坐标要转换到哪一个物体上，这里img父类是Canvas，我们就用Canvas
        //eventData.enterEventCamera：这个事件是由哪个摄像机执行的
        //out mouseUguiPos：返回转换后的ugui坐标
        //isRect：方法返回一个bool值，判断鼠标按下的点是否在要转换的物体上
        //bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown, eventData.enterEventCamera, out mouseUguiPos);
       // if (isRect)   //如果在
        //{
            //计算图片中心和鼠标点的差值
            //offset = imgRect.anchoredPosition - mouseUguiPos;
        //}
    }

    bool IsOpen = false;
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(Globals.OPEN_ZTCD))
        {
            if (!IsOpen)
            {
               //print("KeyCode.P>  " + KeyCode.P);
                IsOpen = true;
                //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ROLECANCONTROL, false), this);
                GlobalSetDate.instance.IsChangeScreening = true;
                mianbanShow(this.GetComponent<RectTransform>());
            }
            else
            {
                IsOpen = false;
                GlobalSetDate.instance.IsChangeScreening = false;
                //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ROLECANCONTROL, true), this);
                mianbanHide(this.GetComponent<RectTransform>());
            }
        }
    }
}
