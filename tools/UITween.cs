using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITween : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
   /* static UITween instance;
    public static UITween GetInstance()
    {
        if (instance == null) instance = new UITween();
        return instance;
    }*/

    // Update is called once per frame
    void Update () {
        //print("???????????????????   "+ isImgChange);
        if (isImgChange)
        {
            ImgChangeing();
        }

        if (isImgAlphaChange)
        {
            ImgAlphaChangeing();
        }
	}


    //--------------------------------------------------------------imgAlpha----------------------------------------------------------------------------
    bool isImgAlphaChange = false;
    bool isImgAlphaChangeing = false;

    float _setAlpha;
    float _toAlpha;
    float _alphaMspeed;
    float _alphaEndValue;
    float _imgAlpha;
    public void ImgAlphaStartSet(float setAlpha,float toAlpha,float alphaMspeed=0.3f ,float alphaEndValue = 0.02f)
    {
        _setAlpha = setAlpha;
        _toAlpha = toAlpha;
        _alphaMspeed = alphaMspeed;
        _alphaEndValue = alphaEndValue;
        _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, _setAlpha);
        isImgAlphaChange = true;
        isImgAlphaChangeing = false;
    }

    void ImgAlphaChangeing()
    {
        if (!isImgAlphaChangeing) {
            isImgAlphaChangeing = true;
            _imgAlpha = _img.color.a;
        }
        _imgAlpha += (_toAlpha - _setAlpha) * _alphaMspeed;
        if(_toAlpha - _imgAlpha< _alphaEndValue)
        {
            _imgAlpha = _toAlpha;
            isImgAlphaChange = false;
            isImgAlphaChangeing = false;
        }
        _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, _imgAlpha);
    }


    //--------------------------------------------------------------IMG---------------------------------------------------------------------------------
    Image _img;
    public UITween GetUIImage(Image img)
    {
        _img = img;
        return this;
    }

    bool isImgChange = false;
    bool isImgChangeing = false;
    public void ImgChangeStartSet(float setW,float setH,float toW,float toH,float MSpeed = 0.3f,float endValue = 0.02f)
    {
        _endValue = endValue;
        _MSpeed = MSpeed;
        _setW = setW;
        _setH = setH;
        _toW = toW;
        _toH = toH;
        _img.GetComponent<RectTransform>().sizeDelta = new Vector2(setW, setH);
        //_img.color = new Color(255, 255, 255, 0.2f);
        isImgChange = true;
        isImgChangeing = false;
    }

    float _endValue = 0.02f;
    float _MSpeed = 0.3f;
    float _setW;
    float _setH;
    float _w;
    float _h;
    float _toW;
    float _toH;
    void ImgChangeing()
    {
        if (!isImgChangeing)
        {
            isImgChangeing = true;
            _w = _img.rectTransform.rect.width;
            _h = _img.rectTransform.rect.height;
        }

        _w += (_toW - _w) * _MSpeed;
        _h += (_toH - _h) * _MSpeed;

        if (_toH - _h < _endValue)
        {
            _w = _toW;
            _h = _toH;
            isImgChangeing = false;
            isImgChange = false;
        }
        _img.GetComponent<RectTransform>().sizeDelta = new Vector2(_w, _h);
    }


}
