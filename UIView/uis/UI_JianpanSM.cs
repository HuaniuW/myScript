using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_JianpanSM : MonoBehaviour
{
    // Start is called before the first frame update
    //UI 键盘操作说明
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   


    public Text U;
    public Text I;


    public Text H;
    public Text J;
    public Text K;
    public Text L;

    public Text V;
    public Text B;




    public void GetStart()
    {
        print("  键盘说明   "+Globals.language);
        TXTU = "";
        U.text = TXTU;
        TXTI = "";
        I.text = TXTI;

        TXTH = "";
        H.text = TXTH;
        TXTJ = "";
        J.text = TXTJ;
        TXTK = "";
        K.text = TXTK;
        TXTL = "";
        L.text = TXTL;

        TXTV = "";
        V.text = TXTV;
        TXTB = "";
        B.text = TXTB;
    }



    string TxtU = "技能释放（配合方向键）/机甲能量炮";
    public string TXTU
    {
        get { return TxtU; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtU = "技能释放（配合方向键）/机甲能量炮";
                    break;
                case Globals.JAPAN:
                    TxtU = "スキルリリース（矢印キー付き）/メカエナジーキャノン";
                    break;
                case Globals.ENGLISH:
                    TxtU = "Skill release (with arrow keys)/Mecha energy cannon";
                    break;
                case Globals.Portugal:
                    TxtU = "Liberación de habilidad (con teclas de flecha)/Cañón de energía Mecha";
                    break;
                case Globals.KOREAN:
                    TxtU = "스킬 해제(화살표 키 사용)/메카 에너지 캐논";
                    break;
                case Globals.CHINESEF:
                    TxtU = "技能釋放（配合方向鍵）/機甲能量炮";
                    break;
                case Globals.German:
                    TxtU = "Skill-Release (mit Pfeiltasten)/Mecha-Energiekanone";
                    break;
                case Globals.French:
                    TxtU = "Libération de compétence (avec les touches fléchées) / Canon à énergie Mecha";
                    break;
                case Globals.Italy:
                    TxtU = "Rilascio abilità (con i tasti freccia)/Mecha cannone energetico";
                    break;
            }
        }
    }

    string TxtI = "机甲干扰弹";
    public string TXTI
    {
        get { return TxtI; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtI = "机甲干扰弹";
                    break;
                case Globals.JAPAN:
                    TxtI = "メカジャマー";
                    break;
                case Globals.ENGLISH:
                    TxtI = "Mecha Jammer";
                    break;
                case Globals.Portugal:
                    TxtI = "Bloqueador mecánico";
                    break;
                case Globals.KOREAN:
                    TxtI = "메카 방해기";
                    break;
                case Globals.CHINESEF:
                    TxtI = "機甲干擾彈";
                    break;
                case Globals.German:
                    TxtI = "Mecha-Disruptor";
                    break;
                case Globals.French:
                    TxtI = "Brouilleur méca";
                    break;
                case Globals.Italy:
                    TxtI = "Mecha Disruptor";
                    break;
            }
        }
    }

    string TxtH = "格挡";
    public string TXTH
    {
        get { return TxtH; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtH = "格挡";
                    break;
                case Globals.JAPAN:
                    TxtH = "ブロック";
                    break;
                case Globals.ENGLISH:
                    TxtH = "block";
                    break;
                case Globals.Portugal:
                    TxtH = "cuadra";
                    break;
                case Globals.KOREAN:
                    TxtH = "차단하다";
                    break;
                case Globals.CHINESEF:
                    TxtH = "格擋";
                    break;
                case Globals.German:
                    TxtH = "Block";
                    break;
                case Globals.French:
                    TxtH = "Bloc";
                    break;
                case Globals.Italy:
                    TxtH = "Blocco";
                    break;
            }
        }
    }

    string TxtJ = "攻击/机甲机炮";
    public string TXTJ
    {
        get { return TxtJ; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtJ = "攻击/机甲机炮";
                    break;
                case Globals.JAPAN:
                    TxtJ = "アタック/メカキャノン";
                    break;
                case Globals.ENGLISH:
                    TxtJ = "Attack/Mecha Cannon";
                    break;
                case Globals.Portugal:
                    TxtJ = "Ataque/Mecha Cañón";
                    break;
                case Globals.KOREAN:
                    TxtJ = "공격/메카 캐논";
                    break;
                case Globals.CHINESEF:
                    TxtJ = "攻擊/機甲機砲";
                    break;
                case Globals.German:
                    TxtJ = "Angriff/Mecha-Kanone";
                    break;
                case Globals.French:
                    TxtJ = "Attaque/Mecha Cannon";
                    break;
                case Globals.Italy:
                    TxtJ = "ttacco/cannone meccanico";
                    break;
            }
        }
    }


    string TxtK = "跳跃/机甲导弹";
    public string TXTK
    {
        get { return TxtK; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtK = "跳跃/机甲导弹";
                    break;
                case Globals.JAPAN:
                    TxtK = "ジャンプ/ メカミサイル";
                    break;
                case Globals.ENGLISH:
                    TxtK = "Jump/Mecha Missile";
                    break;
                case Globals.Portugal:
                    TxtK = "Salto/Misil Mecha";
                    break;
                case Globals.KOREAN:
                    TxtK = "점프/메카 미사일";
                    break;
                case Globals.CHINESEF:
                    TxtK = "跳躍/機甲導彈";
                    break;
                case Globals.German:
                    TxtK = "Sprung-/Mecha-Rakete";
                    break;
                case Globals.French:
                    TxtK = "Saut/missile méca";
                    break;
                case Globals.Italy:
                    TxtK = "Missile a salto/meccanico";
                    break;
            }
        }
    }

    string TxtL = "快速闪进/机甲加力";
    public string TXTL
    {
        get { return TxtL; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtL = "快速闪进/机甲加力";
                    break;
                case Globals.JAPAN:
                    TxtL = "クイックフラッシュ/メカアフターバーナー";
                    break;
                case Globals.ENGLISH:
                    TxtL = "Quick Flash/Mecha Afterburner";
                    break;
                case Globals.Portugal:
                    TxtL = "Flash rápido/poscombustión mecánica";
                    break;
                case Globals.KOREAN:
                    TxtL = "퀵 플래시/메카 애프터버너";
                    break;
                case Globals.CHINESEF:
                    TxtL = "快速閃進/機甲加力";
                    break;
                case Globals.German:
                    TxtL = "Schneller Blitz/Mecha-Nachbrenner";
                    break;
                case Globals.French:
                    TxtL = "Flash rapide/Mecha Afterburner";
                    break;
                case Globals.Italy:
                    TxtL = "Quick Flash/Mecha Afterburner";
                    break;
            }
        }
    }


    string TxtV = "设置";
    public string TXTV
    {
        get { return TxtV; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtV = "设置";
                    break;
                case Globals.JAPAN:
                    TxtV = "設定";
                    break;
                case Globals.ENGLISH:
                    TxtV = "set";
                    break;
                case Globals.Portugal:
                    TxtV = "configurar";
                    break;
                case Globals.KOREAN:
                    TxtV = "설정";
                    break;
                case Globals.CHINESEF:
                    TxtV = "设置";
                    break;
                case Globals.German:
                    TxtV = "aufstellen";
                    break;
                case Globals.French:
                    TxtV = "d'installation";
                    break;
                case Globals.Italy:
                    TxtV = "impostare";
                    break;
            }
        }
    }


    string TxtB = "玩家背包";
    public string TXTB
    {
        get { return TxtB; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtB = "玩家背包";
                    break;
                case Globals.JAPAN:
                    TxtB = "プレーヤーのバックパック";
                    break;
                case Globals.ENGLISH:
                    TxtB = "player backpack";
                    break;
                case Globals.Portugal:
                    TxtB = "mochila de jugador";
                    break;
                case Globals.KOREAN:
                    TxtB = "플레이어 백팩";
                    break;
                case Globals.CHINESEF:
                    TxtB = "玩家背包";
                    break;
                case Globals.German:
                    TxtB = "Rucksack für Spieler";
                    break;
                case Globals.French:
                    TxtB = "sac à dos joueur";
                    break;
                case Globals.Italy:
                    TxtB = "zaino del giocatore";
                    break;
            }
        }
    }






}
