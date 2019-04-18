using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Btn_FX : CanTouchBox {

    public GameObject player;
    public GameBody playerObj;
    public Image btnImg;
    Vector2 chushi;
    // Use this for initialization
    void Start () {
        if (player == null)
        {
            player = GlobalTools.FindObjByName("player");
            if(player) playerObj = player.GetComponent<GameBody>();
        }
        chushi = btnImg.transform.position;
    }
    
    bool IsDown = false;
    float dx = 0;
    float moveNums = 0.3f;
	// Update is called once per frame
	void Update () {
        if (IsDown)
        {
            Globals.isXNBtn = true;
            if (dx < 156)
            {
                playerObj.RunLeft(-moveNums);
                btnImg.transform.position = new Vector2(chushi.x-10,chushi.y);
            }
            else if (dx > 190)
            {
                playerObj.RunRight(moveNums);
                btnImg.transform.position = new Vector2(chushi.x + 10, chushi.y);
            }
            else
            {
                btnImg.transform.position = chushi;
               
            }
        }
        else
        {
            Globals.isXNBtn = false;
            //playerObj.ReSetLR();
        }
	}

    override public void OnPointerDown(PointerEventData eventData)
    {
        //this.GetComponent<RectTransform>().localScale = imgReduceScale;
        //Vector2 mouseDown = eventData.position;
        //print("点击坐标  "+mouseDown);
        IsDown = true;
        Vector2 mouseEnter = eventData.position;   //当鼠标拖动时的屏幕坐标
        //print("坐标  " + mouseEnter);

        dx = mouseEnter.x;
    }

    override public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouseEnter = eventData.position;   //当鼠标拖动时的屏幕坐标
        //print("坐标  " + mouseEnter);

        dx = mouseEnter.x;
    }

    override public void OnPointerEnter(PointerEventData eventData)
    {
        //imgRect.localScale = imgReduceScale;   //缩小图片
        //IsDown = true;
    }

    override public void OnPointerExit(PointerEventData eventData)
    {
        IsDown = false;
        btnImg.transform.position = chushi;
        //imgRect.localScale = imgNormalScale;   //回复图片
    }

    override public void OnPointerUp(PointerEventData eventData)
    {
        IsDown = false;
        btnImg.transform.position = chushi;
    }
}
