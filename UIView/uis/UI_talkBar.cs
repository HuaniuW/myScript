using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_talkBar : MonoBehaviour
{
    public Text WaiText;
    public Image barImg;
    public Text talkText;

    string _talkObjName = "";

    [Header("可以点的 提示 符号")]
    public Image CanClickImg;


    // Start is called before the first frame update
    void Start()
    {
        
        //CanClickImg.GetComponent<CanvasGroup>().alpha = 0;
    }


    private void OnEnable()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        CanClickShow();
        ShowOut();
    }


    private void Init()
    {
        _bgImgColor = barImg.color;
        //print("  barImg.color>   " + barImg.color);
        this.GetComponent<CanvasGroup>().alpha = 0;
        CanClickImg.GetComponent<CanvasGroup>().alpha = 0;
        IsShowCanClickImg = false;
        IsStartShowOut = true;
    }

    bool IsStartShowOut = false;
    void ShowOut()
    {
        if (!IsStartShowOut) return;
        if (this.GetComponent<CanvasGroup>().alpha > 0.94f)
        {
            IsStartShowOut = true;
            //IsShowCanClickImg = false;
            //IsShowCanClickImg = true;
            this.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            this.GetComponent<CanvasGroup>().alpha += (1 - this.GetComponent<CanvasGroup>().alpha) * 0.2f;
        }
        
    }



    Color _bgImgColor;
    public void BgImgColorChange()
    {
        Color _newColor = new Color(0.2f,0.2f,0.2f);
        barImg.color = _newColor;
    }

    //bool IsCanClick = false;
    public void CanClick()
    {
        IsShowCanClickImg = true;
    }



    public void StopCanClick()
    {
        IsShowCanClickImg = false;
        CanClickImg.GetComponent<CanvasGroup>().alpha = 0;
    }


    bool IsShowCanClickImg = false;
    float _changeNums = 0.08f;
    bool _isUp = false;
    void CanClickShow (){
        if (!IsShowCanClickImg)
        {
            CanClickImg.GetComponent<CanvasGroup>().alpha = 0;
            return;
        }


        if (CanClickImg.GetComponent<CanvasGroup>().alpha == 1)
        {
            _isUp = false;
        }

        if (CanClickImg.GetComponent<CanvasGroup>().alpha == 0)
        {
            _isUp = true;
        }

        if (_isUp)
        {
            CanClickImg.GetComponent<CanvasGroup>().alpha += (1 - CanClickImg.GetComponent<CanvasGroup>().alpha) * _changeNums;
            if (CanClickImg.GetComponent<CanvasGroup>().alpha >= 0.94f)
            {
                CanClickImg.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
        else
        {
            CanClickImg.GetComponent<CanvasGroup>().alpha += (0 - CanClickImg.GetComponent<CanvasGroup>().alpha) * _changeNums;
            if (CanClickImg.GetComponent<CanvasGroup>().alpha <= 0.06f)
            {
                CanClickImg.GetComponent<CanvasGroup>().alpha = 0;
            }
        }

    }

    public string GetTalkObjName()
    {
        return this._talkObjName;
    }


    string StrEvents = "";
    public void ShowTalkText(string txts,Vector2 pos,float disTime = 1,string talkObjName = "")
    {
        this._talkObjName = talkObjName;
        this.transform.position = pos;
        string msgs = "";
        if (txts.Split('^').Length > 1)
        {
            msgs = txts.Split('^')[0];
            StrEvents = txts.Split('^')[1].Split('$')[1];
        }
        else
        {
            msgs = txts;
        }
        WaiText.text = talkText.text = msgs;

        //print("   球！     " + WaiText.transform.GetComponent<RectTransform>().rect);
        //print(this.transform.position);

        if (disTime!=0) GetComponent<TheTimer>().TimesAdd(disTime, TimeCallBack);
    }

    public void TimeCallBack(float nums)
    {
        RemoveSelf();
    }

    public void RemoveSelf()
    {
        ObjectPools.GetInstance().DestoryObject2(this.gameObject);

       
        
    }

    public void SetOutEvent()
    {
        if (StrEvents != "")
        {
            print("  派发的事件 是啥  StrEvents     " + StrEvents);
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHOSE_EVENT, StrEvents), this);
        }
    }
}
