using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using System.Collections.Generic;

//给空间添加监听事件要实现的一些接口
public class MyDrag : CanTouchBox
{

    public RectTransform canvas;          //得到canvas的ugui坐标
    private RectTransform imgRect;        //得到图片的ugui坐标
    Vector2 offset = new Vector3();    //用来得到鼠标和图片的差值
    //Vector3 imgReduceScale = new Vector3(0.8f, 0.8f, 1);   //设置图片缩放
    //Vector3 imgNormalScale = new Vector3(1, 1, 1);   //正常大小

    // Use this for initialization
    void Start()
    {
        imgRect = GetComponent<RectTransform>();
        if (canvas == null)
        {
            //用于拖拽
            canvas = this.transform.parent.parent as RectTransform; 
        }
    }

    //当鼠标按下时调用 接口对应  IPointerDownHandler
    override public void OnPointerDown(PointerEventData eventData)
    {
        print("鼠标按下！！！！！");
        this.transform.SetAsLastSibling();
        Vector2 mouseDown = eventData.position;    //记录鼠标按下时的屏幕坐标
        Vector2 mouseUguiPos = new Vector2();   //定义一个接收返回的ugui坐标
        //RectTransformUtility.ScreenPointToLocalPointInRectangle()：把屏幕坐标转化成ugui坐标
        //canvas：坐标要转换到哪一个物体上，这里img父类是Canvas，我们就用Canvas
        //eventData.enterEventCamera：这个事件是由哪个摄像机执行的
        //out mouseUguiPos：返回转换后的ugui坐标
        //isRect：方法返回一个bool值，判断鼠标按下的点是否在要转换的物体上
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown, eventData.enterEventCamera, out mouseUguiPos);
        if (isRect)   //如果在
        {
            //计算图片中心和鼠标点的差值
            offset = imgRect.anchoredPosition - mouseUguiPos;
            //显示点击徽章的数据
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.HZ_TOUCH, this.GetComponent<HZDate>()), this);
        }
    }

    //当鼠标拖动时调用   对应接口 IDragHandler
    override public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouseDrag = eventData.position;   //当鼠标拖动时的屏幕坐标
        Vector2 uguiPos = new Vector2();   //用来接收转换后的拖动坐标
        //和上面类似
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDrag, eventData.enterEventCamera, out uguiPos);

        if (isRect)
        {
            //设置图片的ugui坐标与鼠标的ugui坐标保持不变
            imgRect.anchoredPosition = offset + uguiPos;
        }
    }

    //当鼠标抬起时调用  对应接口  IPointerUpHandler
    override public void OnPointerUp(PointerEventData eventData)
    {
        print("鼠标抬起！！！！！");
        CheckNear();
        offset = Vector2.zero;
    }

    //当鼠标结束拖动时调用   对应接口  IEndDragHandler
    override public void OnEndDrag(PointerEventData eventData)
    {
        //print("???????结束拖动");
        offset = Vector2.zero;
    }

    //当鼠标进入图片时调用   对应接口   IPointerEnterHandler
    override public void OnPointerEnter(PointerEventData eventData)
    {
        //imgRect.localScale = imgReduceScale;   //缩小图片
    }

    //当鼠标退出图片时调用   对应接口   IPointerExitHandler
    override public void OnPointerExit(PointerEventData eventData)
    {
        //imgRect.localScale = imgNormalScale;   //回复图片
    }

    //初始父对象
    void GetCSParent()
    {

    }

    //回到之前的父对象
    void BackYSParent()
    {

    }

    //新的父对象
    void InNewParent()
    {

    }

    public RectTransform OldRQ;

    void CheckNear()
    {
        List<RectTransform> _geziArr = this.transform.parent.GetComponent<Mianban1>().geziArr;
        for (var i=0;i<_geziArr.Count;i++)
        {
            float distance = Vector2.Distance(_geziArr[i].position, this.transform.position);
            if (distance<45f)
            {
                //格子没开启
                if (!_geziArr[i].GetComponent<Gezi>().IsOpen) {
                    this.transform.position = OldRQ.transform.position;
                    return;
                }

                /*if ((this.GetComponent<HZDate>().type == "bd" && _geziArr[i].tag == "JN_zhuangbeilan")|| 
                    this.GetComponent<HZDate>().type == "zd" && _geziArr[i].tag == "zhuangbeilan") {
                    this.transform.position = OldRQ.transform.position;
                    return;
                }*/
                

                if (_geziArr[i].name == OldRQ.name)
                {
                    this.transform.position = OldRQ.transform.position;
                    return;
                }
                //取到格子里面之前的物品
                RectTransform tempObj = _geziArr[i].GetComponent<Gezi>().IsHasObj();
                //print("tempObj   "+ tempObj);
                if (tempObj!=null)
                {
                    //print("之前的格子   "+OldRQ);
                    if (OldRQ != null) {
                        OldRQ.GetComponent<Gezi>().GetInObj(tempObj,false,true);
                    }
                    _geziArr[i].GetComponent<Gezi>().GetInObj(imgRect,true);
                }
                else
                {
                    _geziArr[i].GetComponent<Gezi>().GetInObj(imgRect);
                }
                return;
            }
        }
        this.transform.position = OldRQ.transform.position;
    }


}