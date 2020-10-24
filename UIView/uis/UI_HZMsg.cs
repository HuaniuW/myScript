using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UI_HZMsg : MonoBehaviour
{
    [Header("徽章图片")]
    public Image HZ_Img;
    [Header("徽章介绍信息txt")]
    public Text HZ_msg;
    [Header("徽章面板")]
    public GameObject HZMsgBar;


    // Start is called before the first frame update
    void Start()
    {
        //IsShowHZMsgBar = true;
    }

    private void OnEnable()
    {
        HZMsgBar.GetComponent<CanvasGroup>().alpha = 0;
    }


    public void StartShowBar(string ImgName,string Msg)
    {
        ReSetAll();
        IsShowHZMsgBar = true;
        //Sprite sp = Resources.Load("i_huizhang12", typeof(Sprite)) as Sprite;  这个用不了 无法被加载 只能用 GameObject来加载

        GameObject sp = Resources.Load(ImgName) as GameObject;
        HZ_Img.overrideSprite = sp.GetComponent<SpriteRenderer>().sprite;
        HZ_msg.text = Msg;
    }



    //自动消失时间
    float Zidongxiaoshishijian = 3;
    float jishiNums = 0;
    bool IsXiaoshiJishi = false;

    void XiaoshiJishi()
    {
        jishiNums += Time.deltaTime;
        if(jishiNums>= Zidongxiaoshishijian)
        {
            IsXiaoshiJishi = false;
            jishiNums = 0;
            IsXiaoshi = true;
        }
    }


    bool IsShowHZMsgBar = false;
    void ShowHZMsgBar()
    {
        if (HZMsgBar.GetComponent<CanvasGroup>().alpha < 0.94f)
        {
            HZMsgBar.GetComponent<CanvasGroup>().alpha += (1 - HZMsgBar.GetComponent<CanvasGroup>().alpha) * 0.1f;
        }
        else
        {
            HZMsgBar.GetComponent<CanvasGroup>().alpha = 1;
            IsShowHZMsgBar = false;
            IsXiaoshiJishi = true;
        }
    }




    bool IsXiaoshi = false;
    void Xiaoshi()
    {
        if(HZMsgBar.GetComponent<CanvasGroup>().alpha > 0.6f)
        {
            HZMsgBar.GetComponent<CanvasGroup>().alpha += (0 - HZMsgBar.GetComponent<CanvasGroup>().alpha) * 0.1f;
        }
        else
        {
            HZMsgBar.GetComponent<CanvasGroup>().alpha = 0;
            IsXiaoshi = false;
        }
    }





    void ReSetAll()
    {
        IsShowHZMsgBar = false;
        IsXiaoshiJishi = false;
        IsXiaoshi = false;
        jishiNums = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsShowHZMsgBar) ShowHZMsgBar();
        if (IsXiaoshi) Xiaoshi();
        if (IsXiaoshiJishi) XiaoshiJishi();

    }
}
