using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot_alert : MonoBehaviour
{

    [Header("对话框弹出位置点")]
    public GameObject TalkPosObj;

    [Header("****剧情 提示 语 内容")]
    public string PlotStr = "";


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    Vector2 _talkPos = Vector2.zero;
    GameObject _cBar;
    void GetTalkBar()
    {
        if (_cBar)
        {
            _cBar.GetComponent<UI_talkBar>().RemoveSelf();
        }

        _cBar = ObjectPools.GetInstance().SwpanObject2(Resources.Load("TalkBar2") as GameObject);

        _talkPos = this.TalkPosObj.transform.position;

        _cBar.GetComponent<UI_talkBar>().ShowTalkText(PlotStr, _talkPos, 10);

    }




    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.tag == "Player")
        {
            GetTalkBar();
            //ShowPlotStr();
        }
    }


    private void OnTriggerStay2D(Collider2D Coll)
    {
        if (Coll.tag == "Player")
        {
            //print("?");
            //出文本了

        }
    }

    private void OnTriggerExit2D(Collider2D Coll)
    {
        print("离开");
        if (Coll.tag == "Player")
        {
            if (_cBar) _cBar.GetComponent<UI_talkBar>().RemoveSelf();
        }
    }

}
