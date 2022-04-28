using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuaiDubai : MonoBehaviour
{
    [Header("***怪物名字 用来做key")]
    public string GuaiName = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    string _dubai = "";


    public string GetDubai()
    {
        DUBAI = "";
        return DUBAI;
    }


    void OnGUI()
    {
        if (Globals.isDebug)
        {
            //GetDubai();
            //GUI.TextArea(new Rect(0, 60, 450, 40), "GuaiName  ID : " + GuaiName+"    独白内容  "+ DUBAI);//使用GUI在屏幕上面实时打印当前按下的按键
            //Zhenshu();
        }

    }


    public string DUBAI
    {
        //GET访问器，可以理解成另类的方法，返回已经被赋了值的私有变量a
        get { return _dubai; }
        //SET访问器，将我们打入的值赋给私有变量money
        set
        {
            if (GuaiName == "B_dlws")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        _dubai = "雨。。。。。";
                        break;
                    case Globals.JAPAN:
                        _dubai = "雨。 。 。 。 。";
                        break;
                    case Globals.ENGLISH:
                        _dubai = "rain. . . . .";
                        break;
                    case Globals.Portugal:
                        _dubai = "lluvia. . . . .";
                        break;
                    case Globals.KOREAN:
                        _dubai = "비. . . . .";
                        break;
                    case Globals.CHINESEF:
                        _dubai = "雨。。。。。";
                        break;
                    case Globals.German:
                        _dubai = "Regen....";
                        break;
                    case Globals.French:
                        _dubai = "pluie. . . . .";
                        break;
                    case Globals.Italy:
                        _dubai = "piovere. . . . .";
                        break;
                }
            }
            else if (GuaiName == "B_dlws1")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        _dubai = "寂寥。。。。。";
                        break;
                    case Globals.JAPAN:
                        _dubai = "寂しい。 。 。 。 。";
                        break;
                    case Globals.ENGLISH:
                        _dubai = "lonely. . . . .";
                        break;
                    case Globals.Portugal:
                        _dubai = "solitario. . . . .";
                        break;
                    case Globals.KOREAN:
                        _dubai = "외로운. . . . .";
                        break;
                    case Globals.CHINESEF:
                        _dubai = "寂寥。 。 。 。 。";
                        break;
                    case Globals.German:
                        _dubai = "einsam. . . . .";
                        break;
                    case Globals.French:
                        _dubai = "solitaire. . . . .";
                        break;
                    case Globals.Italy:
                        _dubai = "solitario. . . . .";
                        break;
                }
            }
            else if (GuaiName == "B_yg")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        _dubai = "愚蠢的人类！！";
                        break;
                    case Globals.JAPAN:
                        _dubai = "愚かな人間！ ！";
                        break;
                    case Globals.ENGLISH:
                        _dubai = "Stupid humans! !";
                        break;
                    case Globals.Portugal:
                        _dubai = "¡Humanos estúpidos! !";
                        break;
                    case Globals.KOREAN:
                        _dubai = "멍청한 인간들! !";
                        break;
                    case Globals.CHINESEF:
                        _dubai = "愚蠢的人類！ ！";
                        break;
                    case Globals.German:
                        _dubai = "Dumme Menschen! !";
                        break;
                    case Globals.French:
                        _dubai = "Humains stupides ! !";
                        break;
                    case Globals.Italy:
                        _dubai = "Stupidi umani! !";
                        break;
                }
            }
            else if (GuaiName == "B_She")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        _dubai = "害怕吗，颤抖吗！！";
                        break;
                    case Globals.JAPAN:
                        _dubai = "あなたは恐れていますか、あなたは震えていますか？ ！";
                        break;
                    case Globals.ENGLISH:
                        _dubai = "Are you afraid, are you trembling! !";
                        break;
                    case Globals.Portugal:
                        _dubai = "¿Tienes miedo, estás temblando? !";
                        break;
                    case Globals.KOREAN:
                        _dubai = "두려워 떨고 있니! !";
                        break;
                    case Globals.CHINESEF:
                        _dubai = "害怕嗎，顫抖嗎！ ！";
                        break;
                    case Globals.German:
                        _dubai = "Hast du Angst, zitterst du! !";
                        break;
                    case Globals.French:
                        _dubai = "Avez-vous peur, tremblez-vous ! !";
                        break;
                    case Globals.Italy:
                        _dubai = "Hai paura, stai tremando! !";
                        break;
                }
            }


        }
    }

}
