using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using DragonBones;
using UnityEngine.SceneManagement;

public class PlotJiushu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TXTLIST = new List<string> { };
        txtCSPos = TxtMsg.transform.position;
        PlotStart();
        IsShowTxts = true;
        //IsHidezz = true;

       

    }

    GameObject player;

    GameObject PlayerUI;

    void PlotStart()
    {

        string str = FileControl.GetInstance().GetValueByKey("yuyan");
        string isDebug = FileControl.GetInstance().GetValueByKey("debug");
        Globals.isDebug = isDebug == "1999201710" ? true : false;
        print("  是否是 debug " + isDebug + "   ??? " + Globals.isDebug);


        if (str != "")
        {
            Globals.language = str;
        }



        if (!PlayerUI)
        {
            PlayerUI = GlobalTools.FindObjByName(GlobalTag.PLAYERUI);
        }

        if (!player)
        {
            player = GlobalTools.FindObjByName(GlobalTag.PlayerObj);
        }

        PlayerUI.GetComponent<PlayerUI>().HideUIs();

        player.GetComponent<PlayerGameBody>().TurnRight();
        //4.5
        player.GetComponent<Rigidbody2D>().gravityScale = 0;

        Globals.IsCanControl = false;

    }



    [Header("黑色 图片遮罩")]
    public Image ImgZZ;

    [Header("最后 出现的 文本")]
    public Text TxtMsg;

    Vector2 txtCSPos = Vector2.zero;

    List<string> txtList = new List<string> { "'每次人类的无知和自大最后引发战争自我毁灭。。。。。。'","'重启人间就像一场游戏一样，每次都重来。'","'想找不一样的答案，可惜每次结局都一样。'", "'现在还有一件事要做。。。'", "'只是。。。你的记忆消失的话。。。整个世界时间连接就没了。。。'"};
    public List<string> TXTLIST
    {
        //GET访问器，可以理解成另类的方法，返回已经被赋了值的私有变量a
        get { return txtList; }
        //SET访问器，将我们打入的值赋给私有变量money
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    txtList = txtList1;
                    break;
                case Globals.JAPAN:
                    txtList = txtList1r;
                    break;
                case Globals.ENGLISH:
                    txtList = txtList1y;
                    break;
                case Globals.Portugal:
                    txtList = txtList1x;
                    break;
                case Globals.KOREAN:
                    txtList = txtList1h;
                    break;
                case Globals.CHINESEF:
                    txtList = txtList1zf;
                    break;
                case Globals.German:
                    txtList = txtList1d;
                    break;
                case Globals.French:
                    txtList = txtList1f;
                    break;
                case Globals.Italy:
                    txtList = txtList1i;
                    break;

            }

        }
    }



    List<string> txtList1 = new List<string> { "'每次人类的无知和自大最后引发战争自我毁灭。。。。。。'", "'重启人间就像一场游戏一样，每次都重来。'", "'想找不一样的答案，可惜每次结局都一样。'", "'现在还有一件事要做。。。'", "'只是。。。你的记忆消失的话。。。整个世界时间连接就没了。。。'" };
    List<string> txtList1r = new List<string> { "'人間の無知と傲慢が最終的に戦争と自己破壊につながるたびに。 。 。 。 。 。'",
        "'世界を再開することはゲームのようなものであり、毎回最初からやり直します。'",
        "'別の答えを探していますが、残念ながらエンディングは毎回同じです。'",
        "'もう1つやるべきことがあります。 。 。'",
        "'それだけ。 。 。 あなたの記憶が消えた場合。 。 。 全世界の時間のつながりはなくなっています。 。 。'" };

    List<string> txtList1y = new List<string> { "'Every time human ignorance and arrogance eventually lead to war and self-destruction. . . . . .'",
        "'Restarting the world is like a game, and it starts over every time.'",
        "'Looking for a different answer, but unfortunately the ending is the same every time.'",
        "'Now there is one more thing to do. . .'",
        "'only. . . If your memory disappears. . . The whole world time connection is gone. . .'" };

    List<string> txtList1x = new List<string> { "'Cada vez que la ignorancia humana y la arrogancia eventualmente conducen a la guerra y la autodestrucción. . . . . .'",
        "'Reiniciar el mundo es como un juego, y comienza de nuevo cada vez.'",
        "'Buscando una respuesta diferente, pero desafortunadamente el final es el mismo cada vez.'",
        "'Ahora hay una cosa más que hacer. . .'",
        "'solamente. . . Si tu memoria desaparece. . . Toda la conexión horaria mundial se ha ido. . .'" };

    List<string> txtList1h = new List<string> { "'매번 인간의 무지와 오만은 결국 전쟁과 자멸로 이어집니다. . . . . .'",
        "'세상을 다시 시작하는 것은 게임과 같으며 매번 다시 시작됩니다.'",
        "'다른 답을 찾고 있지만 안타깝게도 매번 결말은 똑같다.'",
        "'이제 할 일이 하나 더 있습니다. . .'",
        "'오직. . . 기억이 사라진다면. . . 전 세계 시간 연결이 끊어졌습니다. . .'" };


    List<string> txtList1zf = new List<string> { "'每次人類的無知和自大最後引發戰爭自我毀滅。。。。。。'", "'重啟人間就像一場遊戲一樣，每次都重來。'", "'想找不一樣的答案，可惜每次結局都一樣。'", "'現在還有一件事要做。。。'", "'只是。。。你的記憶消失的話。。。整個世界時間連接就沒了。。。'" };
    List<string> txtList1d = new List<string> { "'Jedes Mal, wenn die Ignoranz und Arroganz der Menschen schließlich zu Krieg und Selbstzerstörung führt...'", "'Die Welt neu zu starten ist wie ein Spiel, und es beginnt jedes Mal von vorne.'", "'Ich will etwas anderes finden Die Antwort ist leider, dass das Ende jedes Mal dasselbe ist.'", "'Jetzt gibt es noch etwas zu tun...'", "'Es ist nur...wenn deine Erinnerung verschwindet... die ganze Weltzeitverbindung geht verloren. . . '" };
    List<string> txtList1f = new List<string> { "'Chaque fois que l'ignorance et l'arrogance des êtres humains mènent finalement à la guerre et à l'autodestruction...'", "'Redémarrer le monde est comme un jeu, et ça recommence à chaque fois.'", "'Je veux trouver quelque chose de différent La réponse est, malheureusement, la fin est la même à chaque fois.'", "'Il y a encore une chose à faire maintenant...'", "'C'est juste que... si ta mémoire disparaît... toute la connexion à l'heure mondiale sera perdue. . . '" };
    List<string> txtList1i = new List<string> { "'Ogni volta che l'ignoranza e l'arroganza degli esseri umani portano alla fine alla guerra e all'autodistruzione...'", "'Riavviare il mondo è come un gioco, e ricomincia da capo ogni volta.'", "'Voglio farlo trova qualcosa di diverso La risposta è, sfortunatamente, il finale è sempre lo stesso.'", "'C'è un'altra cosa da fare ora...'", "'È solo... se la tua memoria scompare... l intera connessione dell ora mondiale andrà perduta. . . '" };

    int listI = 0;


    bool isTxtStart = false;
    float _y = 0;
    float txtNums = 0;
    bool IsChuXian = true;

    bool IsShowTxts = false;
    void ShowTxts()
    {
        if (!IsShowTxts) return;
        if (!isTxtStart)
        {
            TXTLIST = new List<string>{ };
            print("listI   " + listI+ "  -txtList.Count-  "+ txtList.Count);

            if (listI >= txtList.Count)
            {
                IsShowTxts = false;
                IsHidezz = true;
                return;
            }

            isTxtStart = true;
            IsChuXian = true;
            ShowTxt(txtList[listI]);
            listI++;
            //_text.transform.position = new Vector3(_text.transform.position.x, txtCSPos.y - 30, _text.transform.position.z);
            _y = TxtMsg.transform.position.y;
        }


        if (IsChuXian)
        {
            if (_y < txtCSPos.y - 0.2f)
            {
                _y += (txtCSPos.y - TxtMsg.transform.position.y) * 0.05f;
            }
            else
            {
                _y = txtCSPos.y;
                txtNums += Time.deltaTime;
                if (txtNums >= 5)
                {
                    IsChuXian = false;
                    txtNums = 0;
                }

            }

            if (TxtMsg.GetComponent<CanvasGroup>().alpha < 0.94)
            {
                TxtMsg.GetComponent<CanvasGroup>().alpha += (1 - TxtMsg.GetComponent<CanvasGroup>().alpha) * 0.02f;
            }
            else
            {
                TxtMsg.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
        else
        {
            if (_y < txtCSPos.y + 20 - 0.4f)
            {
                _y += (txtCSPos.y + 20 - TxtMsg.transform.position.y) * 0.08f;
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

            if (TxtMsg.GetComponent<CanvasGroup>().alpha > 0.04)
            {
                TxtMsg.GetComponent<CanvasGroup>().alpha += (0 - TxtMsg.GetComponent<CanvasGroup>().alpha) * 0.04f;
            }
            else
            {
                TxtMsg.GetComponent<CanvasGroup>().alpha = 0;
            }

        }
        TxtMsg.transform.position = new Vector3(TxtMsg.transform.position.x, _y, TxtMsg.transform.position.z);

    }

    public void ShowTxt(string txtStr)
    {
        TxtMsg.text = txtStr;
        TxtMsg.GetComponent<CanvasGroup>().alpha = 0;
        TxtMsg.transform.position = new Vector2(txtCSPos.x, txtCSPos.y - 20);


        //print(_text.transform.position);
    }

    bool IsJueseDown = false;
    bool IsHidezz = false;
    void HideZZ()
    {
        if (!IsHidezz) return;
        print("场景切换 遮罩调用  ZZAlphaNums： ");
        if (this.ImgZZ != null)
        {
            this.ImgZZ.GetComponent<CanvasGroup>().alpha += (0 - this.ImgZZ.GetComponent<CanvasGroup>().alpha) * 0.1f;
            if (this.ImgZZ.GetComponent<CanvasGroup>().alpha <= 0.2)
            {
                //print("??????????????????????????????????????   >>>>>>>>>>>>>>  "+ ZZAlphaNums);
                this.ImgZZ.GetComponent<CanvasGroup>().alpha = 0;
                IsHidezz = false;
                IsJueseDown = true;
                //防止遮挡鼠标事件
                //ImgZZ.gameObject.SetActive(false);
                player.transform.position = new Vector2(-2.7f, 18.4f);
                player.GetComponent<Rigidbody2D>().gravityScale = 4.5f;
            }
        }
    }




    //先黑屏出文本  之前 要有什么文本？  光特效
    //隐藏loading 计时
    //每次人类的无知和自大最后引发战争自我毁灭。重启人间就像一场游戏一样，每次都重来，想找不一样的答案，可惜每次结局都一样。真的值吗？其实我也不知道了。。。
    //对了，还有事要做。。。
    //只是。。。你的记忆消失的话。。。整个时间连接就没了。。。
    //对不起。。。让你一直这么难过。。。

    //如果我消失了，忘了我。----七月

    //停一段时间 等文本播完 再掉下来


    // Update is called once per frame
    void Update()
    {
        if (player&&!IsJueseDown)
        {
            //print("  player  ???????????????????    " + player.GetComponent<Rigidbody2D>().gravityScale);
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.transform.position = new Vector2(-2.7f, 18.4f);
        }


        ShowTxts();
        HideZZ();
        if(!IsShowTxts) Shengming();
        GaoguangHouXiaoshi();
        WuzhongLive();
        ShowZZ();
        Thanks();
    }

    
    [Header("生命叶 粒子效果")]
    public ParticleSystem Lizi_Shengming;
    [Header("生命叶 声音效果")]
    public AudioSource AudioShengming;

    bool IsShengming = false;
    bool _isACShengming = false;

    bool IsShengmingJishi = false;
    float JishiNums = 0;
    float TimesNums = 2;

    bool IsStartJiushu = false;


    bool IsCameraPos = false;
    void Shengming()
    {

        if (IsCameraPos)
        {
            IsCameraPos = false;
            GameObject mainCamera;
            mainCamera = GlobalTools.FindObjByName(GlobalTag.MAINCAMERA);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x - 5, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
       

        //if (!IsStartJiushu)
        //{
        //    JishiNums += Time.deltaTime;
        //    if (JishiNums >= 1)
        //    {
        //        JishiNums = 0;
        //        IsStartJiushu = true;
        //    }
        //}

        //print("  IsStartJiushu  "+ IsStartJiushu);
        if (!IsStartJiushu && player.GetComponent<PlayerGameBody>().IsGround && player.GetComponent<PlayerGameBody>().GetDB().animation.lastAnimationName == player.GetComponent<PlayerGameBody>().STAND)
        {
            IsStartJiushu = true;
            IsShengming = true;
            
          
        }

        if (!IsStartJiushu)
        {
            return;
        }

        if (IsShengming)
        {
            if (!_isACShengming)
            {
                _isACShengming = true;
                player.GetComponent<PlayerGameBody>().isAcing = true;
                player.GetComponent<PlayerGameBody>().GetDB().animation.FadeIn("jiaxue_1",0.2f,1);
            }
            if (player.GetComponent<PlayerGameBody>().GetDB().animation.lastAnimationName == "jiaxue_1"&& player.GetComponent<PlayerGameBody>().GetDB().animation.isCompleted)
            {
                print("   -------------------------- jiaxue!!!!   ");
                IsShengmingJishi = true;
                if(Lizi_Shengming.isStopped) Lizi_Shengming.Play();
                if(!AudioShengming.isPlaying) AudioShengming.Play();
            }

            if (IsShengmingJishi)
            {
                JishiNums += Time.deltaTime;
                if(JishiNums>= TimesNums)
                {
                    JishiNums = 0;
                    IsShengmingJishi = false;
                    player.GetComponent<PlayerGameBody>().isAcing = false;
                    player.GetComponent<PlayerGameBody>().GetStand();
                    Lizi_Shengming.Stop();
                    AudioShengming.Stop();
                    //进入 高光消失计时
                    IsGaoguangXiaoshiLizi = true;
                }
            }
        }
    }

    [Header("高光后 消失粒子")]
    public ParticleSystem Lizi_Xiaoshi;

    public Light2D LightGaoguang;
    [Header("飞走声音")]
    public AudioSource Audio_xiu;

    bool IsGaoguangXiaoshiLizi = false;
    //高光后消失
    void GaoguangHouXiaoshi()
    {
        if (IsGaoguangXiaoshiLizi)
        {
            if(LightGaoguang) LightGaoguang.GetComponent<Light2D>().intensity += (9 - LightGaoguang.GetComponent<Light2D>().intensity) * 0.01f;
            //增强 灯光
            JishiNums += Time.deltaTime;
            if (JishiNums >= 3)
            {
                JishiNums = 0;
                IsGaoguangXiaoshiLizi = false;
                Lizi_Xiaoshi.Play();
                Audio_xiu.Play();
                player.gameObject.SetActive(false);
                IsWuzhongLive = true;
            }
        }
    }




    //吾重站起来

    protected UnityArmatureComponent DBBody;
    public UnityArmatureComponent GetDB()
    {
        if (!DBBody) DBBody = Wuzhong.GetComponentInChildren<UnityArmatureComponent>();
        return DBBody;
    }


    public GameObject Wuzhong;
    bool isWZStand = false;
    bool IsWuzhongLive = false;
    void WuzhongLive()
    {
        if (IsWuzhongLive)
        {
            if(!isWZStand) JishiNums+= Time.deltaTime;
            if (!isWZStand&& JishiNums>=4)
            {
                isWZStand = true;
                JishiNums = 0;
                GetDB().animation.FadeIn("zhanqilai_1",0.4f,1);
                IsShowZZ = true;
            }

            if (GetDB().animation.lastAnimationName == "zhanqilai_1" && GetDB().animation.isCompleted)
            {
                GetDB().animation.GotoAndPlayByFrame("stand_4");
            }


           
        }
    }



    //最后 播字幕 结束




    //显示遮罩
    bool IsShowZZ = false;
    void ShowZZ()
    {
        if (!IsShowZZ) return;
        JishiNums += Time.deltaTime;
        if (JishiNums <= 5)
        {
            return;
        }
        IsWuzhongLive = false;
        //print("场景切换 遮罩调用  ZZAlphaNums： "+ this.ImgZZ.GetComponent<CanvasGroup>().alpha);
        if (this.ImgZZ != null)
        {
            this.ImgZZ.GetComponent<CanvasGroup>().alpha += (1 - this.ImgZZ.GetComponent<CanvasGroup>().alpha) * 0.02f;
            if (this.ImgZZ.GetComponent<CanvasGroup>().alpha >= 0.96f)
            {
                this.ImgZZ.GetComponent<CanvasGroup>().alpha = 1;
                IsShowZZ = false;
                //防止遮挡鼠标事件
                //ImgZZ.gameObject.SetActive(false);

                //player.GetComponent<Rigidbody2D>().gravityScale = 4.5f;
                //print("  ****场景切换 遮罩调用*** ");
                //thanks  退出游戏
                ThanksTxt.gameObject.SetActive(true);
                IsThanks = true;
            }
        }
    }


    [Header("结语感谢")]
    public Text ThanksTxt;

    float thankJishi = 0;

    bool IsThanks = false;
    void Thanks()
    {
        if (!IsThanks) return;
        if (ThanksTxt)
        {
            ThanksTxt.GetComponent<CanvasGroup>().alpha += (1 - ThanksTxt.GetComponent<CanvasGroup>().alpha) * 0.02f;
           
        }
        //print(" ThanksTxt.GetComponent<CanvasGroup>().alpha " + ThanksTxt.GetComponent<CanvasGroup>().alpha);
        if (ThanksTxt.GetComponent<CanvasGroup>().alpha>=0.96f)
        {
            ThanksTxt.GetComponent<CanvasGroup>().alpha = 1;
            thankJishi += Time.deltaTime;
            if (thankJishi >= 10)
            {
                //回到开始界面
                SceneManager.LoadScene("startScreen");
                //DestroyImmediate(this.gameObject, true);
            }
        }


    }


}
