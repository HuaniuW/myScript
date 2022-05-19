using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot_alert : MonoBehaviour
{

    [Header("对话框弹出位置点")]
    public GameObject TalkPosObj;


    [Header("****内容id")]
    public string PlotID = "1";


    string PlotStr = "";
    public string PLOTSTR
    {
        //GET访问器，可以理解成另类的方法，返回已经被赋了值的私有变量a
        get { return PlotStr; }
        //SET访问器，将我们打入的值赋给私有变量money
        set
        {
            if (PlotID == "1")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        PlotStr = "世界不在了 我还在。";
                        break;
                    case Globals.JAPAN:
                        PlotStr = "世界は去り、私はまだここにいます。";
                        break;
                    case Globals.ENGLISH:
                        PlotStr = "The world is gone and I am still here.";
                        break;
                    case Globals.Portugal:
                        PlotStr = "El mundo se ha ido y yo sigo aquí.";
                        break;
                    case Globals.KOREAN:
                        PlotStr = "세상은 갔고 나는 여전히 여기에 있다. ";
                        break;
                    case Globals.CHINESEF:
                        PlotStr = "世界不在了 我還在。";
                        break;
                    case Globals.German:
                        PlotStr = "Die Welt ist vergangen und ich bin immer noch hier.";
                        break;
                    case Globals.French:
                        PlotStr = "Le monde a disparu et je suis toujours là.";
                        break;
                    case Globals.Italy:
                        PlotStr = "Il mondo è finito e io sono ancora qui.";
                        break;
                }
            }else if (PlotID == "1a")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        PlotStr = "神树这里可以记录状态，恢复血量蓝量，更换徽章后记得先在神树记录。";
                        break;
                    case Globals.JAPAN:
                        PlotStr = "ゴッドツリーはここでステータスを記録し、血液量と青の量を復元し、バッジを変更した後、それをゴッドツリーに記録することを忘れないでください。";
                        break;
                    case Globals.ENGLISH:
                        PlotStr = "The God Tree can record the status here, restore the blood volume and blue volume, and remember to record it in the God Tree after changing the badge.";
                        break;
                    case Globals.Portugal:
                        PlotStr = "El Árbol de Dios puede registrar el estado aquí, restaurar el volumen de sangre y el volumen azul, y recordar registrarlo en el Árbol de Dios después de cambiar la insignia.";
                        break;
                    case Globals.KOREAN:
                        PlotStr = "갓트리는 이곳에 상태를 기록할 수 있고, 혈액량과 블루볼륨을 복원할 수 있으며, 배지 변경 후 갓트리에 기록하는 것을 잊지 마십시오.";
                        break;
                    case Globals.CHINESEF:
                        PlotStr = "神樹這裡可以記錄狀態，恢復血量藍量，更換徽章後記得先在神樹記錄。";
                        break;
                    case Globals.German:
                        PlotStr = "Der Gottbaum kann hier den Status aufzeichnen, das Blutvolumen und das blaue Volumen wiederherstellen und daran denken, es nach dem Wechsel des Abzeichens im Gottbaum aufzuzeichnen.";
                        break;
                    case Globals.French:
                        PlotStr = "L'arbre divin peut enregistrer l'état ici, restaurer le volume sanguin et le volume bleu, et n'oubliez pas de l'enregistrer dans l'arbre divin après avoir changé le badge.";
                        break;
                    case Globals.Italy:
                        PlotStr = "Il God Tree può registrare lo stato qui, ripristinare il volume del sangue e il volume blu e ricordarsi di registrarlo nel God Tree dopo aver cambiato il badge.";
                        break;
                }
            }
            else if (PlotID == "2")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        PlotStr = "我扰乱人间，防止人间沉沦。";
                        break;
                    case Globals.JAPAN:
                        PlotStr = "私は悪魔の神によって創造された悪魔です。世界を邪魔し、世界が沈むのを防ぎましょう。";
                        break;
                    case Globals.ENGLISH:
                        PlotStr = "I am the devil created by the devil god, let me disturb the world and prevent the world from sinking.  ";
                        break;
                    case Globals.Portugal:
                        PlotStr = "Soy el diablo creado por el dios diablo, déjame perturbar el mundo y evitar que el mundo se hunda.  ";
                        break;
                    case Globals.KOREAN:
                        PlotStr = "나는 마신이 만든 마귀다. 세상을 어지럽히고 세상이 가라앉지 않게 하라. 내가 인간에게 주는 것은 유혹, 인간이 원하는 것, 나와 무슨 상관인가? 왜 모두가 다시 시작할 수 있지만 나만 할 수 없습니다.";
                        break;
                    case Globals.CHINESEF:
                        PlotStr = "我擾亂人間，防止人間沉淪。";
                        break;
                    case Globals.German:
                        PlotStr = "Ich störe die Welt und verhindere, dass sie untergeht.";
                        break;
                    case Globals.French:
                        PlotStr = "Je trouble le monde et l'empêche de sombrer.";
                        break;
                    case Globals.Italy:
                        PlotStr = "Disturbo il mondo e gli impedisco di affondare.";
                        break;
                }
            }
            else if (PlotID == "3")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        PlotStr = "战争 战争 还是战争";
                        break;
                    case Globals.JAPAN:
                        PlotStr = "戦争戦争または戦争";
                        break;
                    case Globals.ENGLISH:
                        PlotStr = "war war or war";
                        break;
                    case Globals.Portugal:
                        PlotStr = "guerra guerra o guerra";
                        break;
                    case Globals.KOREAN:
                        PlotStr = "전쟁 전쟁 또는 전쟁";
                        break;
                    case Globals.CHINESEF:
                        PlotStr = "戰爭 戰爭 還是戰爭";
                        break;
                    case Globals.German:
                        PlotStr = "Krieg Krieg oder Krieg";
                        break;
                    case Globals.French:
                        PlotStr = "guerre guerre ou guerre";
                        break;
                    case Globals.Italy:
                        PlotStr = "guerra guerra o guerra";
                        break;
                }
            }
            else if (PlotID == "6")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        PlotStr = "有死无生";
                        break;
                    case Globals.JAPAN:
                        PlotStr = "生死";
                        break;
                    case Globals.ENGLISH:
                        PlotStr = "life and death";
                        break;
                    case Globals.Portugal:
                        PlotStr = "vida y muerte";
                        break;
                    case Globals.KOREAN:
                        PlotStr = "삶과 죽음";
                        break;
                    case Globals.CHINESEF:
                        PlotStr = "有死無生";
                        break;
                    case Globals.German:
                        PlotStr = "Leben und Tod";
                        break;
                    case Globals.French:
                        PlotStr = "vie et mort";
                        break;
                    case Globals.Italy:
                        PlotStr = "vita e morte";
                        break;
                }
            }
            else if (PlotID == "6a")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        PlotStr = "长存是诅咒";
                        break;
                    case Globals.JAPAN:
                        PlotStr = "永続性は呪いです";
                        break;
                    case Globals.ENGLISH:
                        PlotStr = "Persistence is a curse";
                        break;
                    case Globals.Portugal:
                        PlotStr = "La persistencia es una maldición";
                        break;
                    case Globals.KOREAN:
                        PlotStr = "끈기는 저주다";
                        break;
                    case Globals.CHINESEF:
                        PlotStr = "長存是詛咒";
                        break;
                    case Globals.German:
                        PlotStr = "Beharrlichkeit ist ein Fluch";
                        break;
                    case Globals.French:
                        PlotStr = "La persévérance est une malédiction";
                        break;
                    case Globals.Italy:
                        PlotStr = "La persistenza è una maledizione";
                        break;
                }
            }
            else if (PlotID == "6b")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        PlotStr = "大多数人不知道为什么上战场";
                        break;
                    case Globals.JAPAN:
                        PlotStr = "ほとんどの人はなぜ戦争に行くのか分かりません";
                        break;
                    case Globals.ENGLISH:
                        PlotStr = "Most people don't know why they go to war";
                        break;
                    case Globals.Portugal:
                        PlotStr = "La mayoría de la gente no sabe por qué van a la guerra.";
                        break;
                    case Globals.KOREAN:
                        PlotStr = "대부분의 사람들은 전쟁을 하는 이유를 모른다";
                        break;
                    case Globals.CHINESEF:
                        PlotStr = "大多數人不知道為什麼上戰場";
                        break;
                    case Globals.German:
                        PlotStr = "Die meisten Menschen wissen nicht, warum sie in den Krieg ziehen";
                        break;
                    case Globals.French:
                        PlotStr = "La plupart des gens ne savent pas pourquoi ils partent en guerre";
                        break;
                    case Globals.Italy:
                        PlotStr = "La maggior parte delle persone non sa perché va in guerra";
                        break;
                }
            }
            else if (PlotID == "6c")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        PlotStr = "人们都向往天堂";
                        break;
                    case Globals.JAPAN:
                        PlotStr = "天国を切望する人々";
                        break;
                    case Globals.ENGLISH:
                        PlotStr = "people yearn for heaven";
                        break;
                    case Globals.Portugal:
                        PlotStr = "la gente anhela el cielo";
                        break;
                    case Globals.KOREAN:
                        PlotStr = "사람들은 천국을 갈망한다";
                        break;
                    case Globals.CHINESEF:
                        PlotStr = "人們都嚮往天堂";
                        break;
                    case Globals.German:
                        PlotStr = "Menschen sehnen sich nach dem Himmel";
                        break;
                    case Globals.French:
                        PlotStr = "les gens aspirent au paradis";
                        break;
                    case Globals.Italy:
                        PlotStr = "le persone bramano il paradiso";
                        break;
                }
            }
            else if (PlotID == "6d")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        PlotStr = "我种满了玫瑰花";
                        break;
                    case Globals.JAPAN:
                        PlotStr = "私はバラでいっぱいです";
                        break;
                    case Globals.ENGLISH:
                        PlotStr = "I am full of roses";
                        break;
                    case Globals.Portugal:
                        PlotStr = "Estoy lleno de rosas Los humanos dicen que las rosas simbolizan el amor. . .";
                        break;
                    case Globals.KOREAN:
                        PlotStr = "나는 장미로 가득 차 있습니다";
                        break;
                    case Globals.CHINESEF:
                        PlotStr = "我種滿了玫瑰花";
                        break;
                    case Globals.German:
                        PlotStr = "Ich bin voller Rosen";
                        break;
                    case Globals.French:
                        PlotStr = "je suis pleine de roses";
                        break;
                    case Globals.Italy:
                        PlotStr = "Sono pieno di rose";
                        break;
                }
            }
            else if (PlotID == "7")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        PlotStr = "有你的地方就是天堂";
                        break;
                    case Globals.JAPAN:
                        PlotStr = "あなたがいるところは天国です";
                        break;
                    case Globals.ENGLISH:
                        PlotStr = "Where there is you is heaven";
                        break;
                    case Globals.Portugal:
                        PlotStr = "Donde estas tu esta el cielo";
                        break;
                    case Globals.KOREAN:
                        PlotStr = "당신이 있는 곳에 천국이 있습니다.";
                        break;
                    case Globals.CHINESEF:
                        PlotStr = "有你的地方就是天堂";
                        break;
                    case Globals.German:
                        PlotStr = "Wo du bist, ist der Himmel";
                        break;
                    case Globals.French:
                        PlotStr = "Là où tu es est le paradis";
                        break;
                    case Globals.Italy:
                        PlotStr = "Dove ci sei tu è il paradiso";
                        break;
                }
            }
            else if (PlotID == "8")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        PlotStr = "发动战争的人从来不上战场";
                        break;
                    case Globals.JAPAN:
                        PlotStr = "戦争を始めた人は決して戦争に行きません";
                        break;
                    case Globals.ENGLISH:
                        PlotStr = "Those who start wars never go to war";
                        break;
                    case Globals.Portugal:
                        PlotStr = "Los que empiezan las guerras nunca van a la guerra";
                        break;
                    case Globals.KOREAN:
                        PlotStr = "전쟁을 시작하는 자는 결코 전쟁에 가지 않는다";
                        break;
                    case Globals.CHINESEF:
                        PlotStr = "發動戰爭的人從來不上戰場";
                        break;
                    case Globals.German:
                        PlotStr = "Diejenigen, die Kriege beginnen, ziehen nie in den Krieg";
                        break;
                    case Globals.French:
                        PlotStr = "Ceux qui déclenchent des guerres ne vont jamais à la guerre";
                        break;
                    case Globals.Italy:
                        PlotStr = "Chi inizia una guerra non va mai in guerra";
                        break;
                }
            }
            else if (PlotID == "9")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        PlotStr = "人类的战争机器及其高效";
                        break;
                    case Globals.JAPAN:
                        PlotStr = "人間の戦争機械とその効率";
                        break;
                    case Globals.ENGLISH:
                        PlotStr = "The Human War Machine and Its Efficiency";
                        break;
                    case Globals.Portugal:
                        PlotStr = "La máquina de guerra humana y su eficiencia";
                        break;
                    case Globals.KOREAN:
                        PlotStr = "인간 전쟁 기계와 그 효율성";
                        break;
                    case Globals.CHINESEF:
                        PlotStr = "人類的戰爭機器及其高效";
                        break;
                    case Globals.German:
                        PlotStr = "Die menschliche Kriegsmaschine und ihre Effizienz";
                        break;
                    case Globals.French:
                        PlotStr = "La machine de guerre humaine et son efficacité";
                        break;
                    case Globals.Italy:
                        PlotStr = "La macchina da guerra umana e la sua efficienza";
                        break;
                }
            }
            else if (PlotID == "10")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        PlotStr = "七月 每次清理完这里的残存的怨灵，世界就会重启";
                        break;
                    case Globals.JAPAN:
                        PlotStr = "7月、残りの不満が片付けられるたびに、世界は再開します";
                        break;
                    case Globals.ENGLISH:
                        PlotStr = "July, every time the remaining grievances are cleaned up, the world will restart";
                        break;
                    case Globals.Portugal:
                        PlotStr = "En julio, cada vez que se solucionen los agravios restantes, el mundo se reiniciará";
                        break;
                    case Globals.KOREAN:
                        PlotStr = "7월, 남은 불만이 정리될 때마다 세상은 다시 시작된다";
                        break;
                    case Globals.CHINESEF:
                        PlotStr = "七月 每次清理完這裡的殘存的怨靈，世界就會重啟";
                        break;
                    case Globals.German:
                        PlotStr = "Jedes Mal, wenn im Juli die verbleibenden Missstände beseitigt sind, wird die Welt neu gestartet";
                        break;
                    case Globals.French:
                        PlotStr = "En juillet, chaque fois que les griefs restants seront nettoyés, le monde redémarrera";
                        break;
                    case Globals.Italy:
                        PlotStr = "A luglio, ogni volta che le lamentele rimanenti verranno ripulite, il mondo ripartirà";
                        break;
                }
            }


        }
    }



    //碑文  我们战斗过，结果我断了一只角
    //一起离开过。可是离开这里，你最后还是湮灭了。。
    //记得你说天堂毁灭了，因为天堂没有梦想。。。所以神创造了人间世界，还创造了我
    //我种满了玫瑰花，人类说玫瑰象征爱情。。。




    //第一关 神树的 说明 可以记录 加血加蓝  更换徽章后记得先在神树记录



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jishi();
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
        PLOTSTR = "";
        _cBar.GetComponent<UI_talkBar>().ShowTalkText(PLOTSTR, _talkPos, 10);

    }




    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.tag == "Player")
        {
            GetTalkBar();
            
            CJJishi = 0;
            IsStartJishi = true;
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
            if (CJNAME !="" &&IsGetCJ())
            {
                GetCJ();
            }
        }
    }


    bool IsStartJishi = false;
    void Jishi()
    {
        if(CJNAME!=""&& IsStartJishi)
        {
            CJJishi += Time.deltaTime;
        }
    }
    float CJJishi = 0;
    bool IsGetCJ()
    {
        if (CJJishi >= 5) return true;
        return false;
    }




    //--------------------成就---------------------
    [Header("获得成就")]
    public string CJNAME = "";

    void GetCJ()
    {
        if (FileControl.GetInstance().GetValueByKey("CJ9") == "1") return;
        if (CJNAME == "") return;
        print("  获取成就  " + CJNAME);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHENGJIU, CJNAME), this);
        FileControl.GetInstance().AddNewKeyAndValue("CJ9", "1");
    }



}
