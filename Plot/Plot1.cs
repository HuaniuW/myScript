using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plot1 : MonoBehaviour
{

    public static string QISHI = "qishi";
    // Start is called before the first frame update
    void Start()
    {

        //print(GlobalDateControl.GetCurrentGKDate());
        //GlobalDateControl.SetDateInCGKDateAndGetInZGKDate(QISHI);

        if (GlobalDateControl.IsHasDateByName(QISHI))
        {
            IsHasPlayed = true;
            print(" 起始剧情已经播放完毕！！！  ");
        }
        else
        {
            print(" @@@@  没有记录 ");
        }

        if (IsHasPlayed) {
            //飞船是到岸的 默认-89.62   到岸是 -23.73
            FeiChuanDaoAn();
            _text.GetComponent<CanvasGroup>().alpha = 0;
            //龙不动
            //门是关的
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "Men_1-1"), this);
            return;
        }
        

        zz.GetComponent<CanvasGroup>().alpha = 1;
        txtCSPos = _text.transform.position;
        _text.GetComponent<CanvasGroup>().alpha = 0;
    }

    void FeiChuanDaoAn()
    {
        feichuan.GetComponent<Feichuan>().IsStopPlot = true;
        feichuan.transform.position = new Vector3(-23.73f, feichuan.transform.position.y, feichuan.transform.position.z);
        feichuan.GetComponent<Feichuan>().Denglizi.Stop();
        feichuan.GetComponent<Feichuan>().LiziPlay();
        //feichuan.GetComponent<Feichuan>().LiziPlay();
        //feichuan.GetComponent<Feichuan>().LiziPlay();
        //feichuan.GetComponent<Feichuan>().LiziPlay();


    }

    public bool IsHasPlayed = false;

    Vector3 txtCSPos = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (IsHasPlayed||feichuan.GetComponent<Feichuan>().IsDaoan()) return;
        GetMainUI();
        HideZZ();

        ShowTxts();

        CameraControl();

        LongStart();
    }

    void LongStart()
    {
        if(player.transform.position.x>=B_long.transform.position.x)B_long.GetComponent<AIPlot>().GetPlotStart();
    }

    public AudioSource fengsheng;

    public GameObject player;

    public Image zz;


    public Text _text;

    public GameObject B_long;

    public GameObject feichuan;


    bool IsHideZZ = false;
    float zzNums = 0;

    void HideZZ()
    {
        zzNums += Time.deltaTime;
        if (!IsHideZZ && zzNums >= 1)
        {
            
            if (zz.GetComponent<CanvasGroup>().alpha <= 0.08f) {
                zz.GetComponent<CanvasGroup>().alpha = 0;
                
                IsHideZZ = true;
            }
            zz.GetComponent<CanvasGroup>().alpha += (0 - zz.GetComponent<CanvasGroup>().alpha) * 0.04f;
        }
    }




    bool IsGetMainUI = false;
    GameObject mainUI;
    void GetMainUI()
    {
        if (!IsGetMainUI)
        {
            IsGetMainUI = true;
            mainUI = GlobalTools.FindObjByName("PlayerUI");
            if (mainUI) mainUI.GetComponent<PlayerUI>().HideSelf();
            //GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().Player

            
            if (!player) player = GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetPlayerObj();
            player.GetComponent<GameBody>().TurnRight();

            //player.GetComponent<GameBody>().TSACControl = true;
            player.GetComponent<GameBody>().GetDB().animation.GotoAndPlayByFrame("sit_1");
            Globals.isInPlot = true;
        }
    }

    public void FengShengStop()
    {
        fengsheng.Stop();
    }



    List<string> txtList = new List<string> {_str2};
    int listI = 0;

    const string _str1 = "“传说月之石可以穿越时空！于是有悔恨的人们对它趋之若鹜”";
    const string _str2 = "“It is said that Moonlight Gem can travel through time and space, so people with regrets want it very much.”";

    bool isTxtStart = false;
    float _y = 0;
    float txtNums = 0;
    bool IsChuXian = true;
    void ShowTxts()
    {
        if (zzNums < 2f) return;

        if (!isTxtStart)
        {
            if(listI>= txtList.Count)
            {
                return;
            }

            isTxtStart = true;
            IsChuXian = true;
            ShowTxt(txtList[listI]);
            listI++;
            //_text.transform.position = new Vector3(_text.transform.position.x, txtCSPos.y - 30, _text.transform.position.z);
            _y = _text.transform.position.y;
        }


        if (IsChuXian)
        {
            if (_y < txtCSPos.y - 0.2f)
            {
                _y += (txtCSPos.y - _text.transform.position.y) * 0.02f;
            }
            else
            {
                _y = txtCSPos.y;
                txtNums += Time.deltaTime;
                if (txtNums >= 10)
                {
                    IsChuXian = false;
                    txtNums = 0;
                }

            }

            if (_text.GetComponent<CanvasGroup>().alpha < 0.94)
            {
                _text.GetComponent<CanvasGroup>().alpha += (1 - _text.GetComponent<CanvasGroup>().alpha) * 0.02f;
            }
            else
            {
                _text.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
        else
        {
            if (_y < txtCSPos.y + 20 - 0.4f)
            {
                _y += (txtCSPos.y + 20 - _text.transform.position.y) * 0.04f;
            }
            else
            {
                _y = txtCSPos.y + 20;

                txtNums += Time.deltaTime;
                if (txtNums >= 0.5f)
                {
                    isTxtStart = false;
                    txtNums = 0;
                }
            }

            if (_text.GetComponent<CanvasGroup>().alpha > 0.04)
            {
                _text.GetComponent<CanvasGroup>().alpha += (0 - _text.GetComponent<CanvasGroup>().alpha) * 0.04f;
            }
            else
            {
                _text.GetComponent<CanvasGroup>().alpha = 0;
            }

        }
        _text.transform.position = new Vector3(_text.transform.position.x, _y, _text.transform.position.z);

    }

    public void ShowTxt(string txtStr)
    {
        _text.text = txtStr;
        _text.GetComponent<CanvasGroup>().alpha = 0;
        _text.transform.position = new Vector3(txtCSPos.x, txtCSPos.y - 30, txtCSPos.z);


        //print(_text.transform.position);
    }



    public GameObject mainCamera;
    void CameraControl()
    {
        if (!mainCamera) {
            mainCamera = GlobalTools.FindObjByName("MainCamera");            
        }
        mainCamera.GetComponent<CameraController>().SetNewAddXPos(3);
        //Vector3 newPos = new Vector3(player.transform.position.x+8,);

    }




}
