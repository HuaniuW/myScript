using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class CanTouchBox : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler,
    IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    protected Vector3 imgReduceScale = new Vector3(0.95f, 0.95f, 1);   //设置图片缩放
    protected Vector3 imgNormalScale = new Vector3(1, 1, 1);   //正常大小
    void Start () {
		
	}

    //当鼠标按下时调用 接口对应  IPointerDownHandler
    virtual public void OnPointerDown(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localScale = imgReduceScale;
    }

    //当鼠标抬起时调用  对应接口  IPointerUpHandler
    virtual public void OnPointerUp(PointerEventData eventData)
    {
        //print("鼠标抬起！！！！！");
        this.GetComponent<RectTransform>().localScale = imgNormalScale;
    }

    //当鼠标拖动时调用   对应接口 IDragHandler
    virtual public void OnDrag(PointerEventData eventData)
    {
       
    }


    //当鼠标结束拖动时调用   对应接口  IEndDragHandler
    virtual public void OnEndDrag(PointerEventData eventData)
    {
    }

    //当鼠标进入图片时调用   对应接口   IPointerEnterHandler
    virtual public void OnPointerEnter(PointerEventData eventData)
    {
        //imgRect.localScale = imgReduceScale;   //缩小图片
    }

    //当鼠标退出图片时调用   对应接口   IPointerExitHandler
    virtual public void OnPointerExit(PointerEventData eventData)
    {
        //imgRect.localScale = imgNormalScale;   //回复图片
    }



    // Update is called once per frame
    void Update () {
		
	}
}
