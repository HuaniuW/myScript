using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_txtBar : MonoBehaviour
{

    public Image bgImg;
    public Text StrText;
    public Image ChoseImg;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }


    private void OnEnable()
    {
        //Init();
    }

    [Header("是否被选中")]
    public bool IsHasChose = false;

    //是否初始化
    //bool IsHasCSH = false;
    Color _yColor;
    Color _txtColor;
    Vector3 _yPos;
    public void Init()
    {
        //if (IsHasCSH) return;
        //IsHasCSH = true;
        _yColor = bgImg.color;
        _txtColor = StrText.color;
        _yPos = this.transform.position;
        ChoseImg.gameObject.SetActive(false);
        if (IsHasChose) GetBeChose();
        //print(" _txtColor   " + _txtColor+ "   _yColor   "+ _yColor);

    }

    // Update is called once per frame
    void Update()
    {
        GetChose();
    }


    string eventStr = "";
    int _nextId = 0;
    public void GetTxtMsg(string msg,string msg2) {
        StrText.text = msg;
        string[] sArr = msg2.Split('^');
        _nextId = int.Parse(sArr[0]);
        if(sArr.Length>1) eventStr = sArr[1].Split('$')[1];
    }


    public int GetNextId()
    {
        return _nextId;
    }

    public string GetEventStr()
    {
        return eventStr;
    }



    public void NotBeChose()
    {
        bgImg.color = _yColor;
        //print("_yColor>    "+ _yColor);
        StrText.color = _txtColor;
        this.transform.position = _yPos;
        IsHasChose = false;
        ChoseImg.gameObject.SetActive(false); 
    }



    public void Bianhei()
    {
        bgImg.color = new Color(0.2f, 0.2f, 0.2f);
        StrText.color = new Color(1, 1, 1);
    }

    public void Bianbai()
    {
        bgImg.color = new Color(255f, 255f, 255f);
        StrText.color = new Color(0.2f, 0.2f, 0.2f);
    }





    public void GetBeChose()
    {
        IsHasChose = true;
        bgImg.color = new Color(0.2f, 0.2f, 0.2f);
        StrText.color = new Color(1,1,1);
        IsGetChose = true;
        ChoseImg.gameObject.SetActive(true);
    }

    bool IsGetChose = false;
    float theZ = 0;
    float ChoseZNums = -0.2f;
    void GetChose()
    {
        if (!IsGetChose) return;
        if (this.transform.position.z <= -0.94f)
        {
            theZ = ChoseZNums;
        }
        else
        {
            theZ += (ChoseZNums - theZ) * 0.03f;
        }
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theZ);
    }
}
