using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_shoubinSM : MonoBehaviour
{
    // Start is called before the first frame update
    //手柄说明
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetStart()
    {
        TXTX = "";
        X.text = TXTX;

        TXTY = "";
        Y.text = TXTY;

        TXTA = "";
        A.text = TXTA;

        TXTB = "";
        B.text = TXTB;

        TXTRB = "";
        RB.text = TXTRB;

        LB.text = TXTRB;

        TXTLT = "";
        LT.text = TXTLT;
        //print("LT.text   "+ LT.text+ "  --------------  TXTLT  "+ TXTLT);

        TXTBB = "";
        BB.text = TXTBB;

        TXTV = "";
        SZ.text = TXTV;

    }


    public Text Y;
    public Text X;
    public Text A;
    public Text B;

    public Text RB;

    public Text LT;
    public Text LB;

    public Text SZ;
    public Text BB;

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


    string TxtBB = "玩家背包";
    public string TXTBB
    {
        get { return TxtBB; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtBB = "玩家背包";
                    break;
                case Globals.JAPAN:
                    TxtBB = "プレーヤーのバックパック";
                    break;
                case Globals.ENGLISH:
                    TxtBB = "player backpack";
                    break;
                case Globals.Portugal:
                    TxtBB = "mochila de jugador";
                    break;
                case Globals.KOREAN:
                    TxtBB = "플레이어 백팩";
                    break;
                case Globals.CHINESEF:
                    TxtBB = "玩家背包";
                    break;
                case Globals.German:
                    TxtBB = "Rucksack für Spieler";
                    break;
                case Globals.French:
                    TxtBB = "sac à dos joueur";
                    break;
                case Globals.Italy:
                    TxtBB = "zaino del giocatore";
                    break;
            }
        }
    }




    string TxtX = "攻击/机甲机炮";
    public string TXTX
    {
        get { return TxtX; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtX = "攻击/机甲机炮";
                    break;
                case Globals.JAPAN:
                    TxtX = "アタック/メカキャノン";
                    break;
                case Globals.ENGLISH:
                    TxtX = "Attack/Mecha Cannon";
                    break;
                case Globals.Portugal:
                    TxtX = "Ataque/Mecha Cañón";
                    break;
                case Globals.KOREAN:
                    TxtX = "공격/메카 캐논";
                    break;
                case Globals.CHINESEF:
                    TxtX = "攻擊/機甲機砲";
                    break;
                case Globals.German:
                    TxtX = "Angriff/Mecha-Kanone";
                    break;
                case Globals.French:
                    TxtX = "Attaque/Mecha Cannon";
                    break;
                case Globals.Italy:
                    TxtX = "ttacco/cannone meccanico";
                    break;
            }
        }
    }


    string TxtY = "技能释放/机甲导弹";
    public string TXTY
    {
        get { return TxtY; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtY = "攻击/机甲机炮";
                    break;
                case Globals.JAPAN:
                    TxtY = "スキルリリース/メカミサイル";
                    break;
                case Globals.ENGLISH:
                    TxtY = "Skill Release/Mecha Missile";
                    break;
                case Globals.Portugal:
                    TxtY = "Lanzamiento de habilidad/Mecha misil";
                    break;
                case Globals.KOREAN:
                    TxtY = "스킬 해제/메카 미사일";
                    break;
                case Globals.CHINESEF:
                    TxtY = "技能釋放/機甲導彈";
                    break;
                case Globals.German:
                    TxtY = "Skill-Release/Mecha-Rakete";
                    break;
                case Globals.French:
                    TxtY = "Libération de compétences / Missile Mecha";
                    break;
                case Globals.Italy:
                    TxtY = "Rilascio abilità/missili meccanici";
                    break;
            }
        }
    }

    string TxtA = "跳跃/机甲干扰弹";
    public string TXTA
    {
        get { return TxtA; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtA = "跳跃/机甲干扰弹";
                    break;
                case Globals.JAPAN:
                    TxtA = "ジャンプ/メカディスラプター";
                    break;
                case Globals.ENGLISH:
                    TxtA = "Jump/Mecha Disruptor";
                    break;
                case Globals.Portugal:
                    TxtA = "Disruptor de salto/mecha";
                    break;
                case Globals.KOREAN:
                    TxtA = "점프/메카 디스럽터";
                    break;
                case Globals.CHINESEF:
                    TxtA = "跳躍/機甲干擾彈";
                    break;
                case Globals.German:
                    TxtA = "Sprung-/Mecha-Disruptor";
                    break;
                case Globals.French:
                    TxtA = "Perturbateur de saut/méca";
                    break;
                case Globals.Italy:
                    TxtA = "Jump/Mecha Disruptor";
                    break;
            }
        }
    }

    string TxtB = "快速闪进";
    public string TXTB
    {
        get { return TxtB; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtB = "快速闪进";
                    break;
                case Globals.JAPAN:
                    TxtB = "フラッシュイン";
                    break;
                case Globals.ENGLISH:
                    TxtB = "flash in";
                    break;
                case Globals.Portugal:
                    TxtB = "flash en";
                    break;
                case Globals.KOREAN:
                    TxtB = "플래시 인";
                    break;
                case Globals.CHINESEF:
                    TxtB = "快速閃進";
                    break;
                case Globals.German:
                    TxtB = "einblitzen";
                    break;
                case Globals.French:
                    TxtB = "clignoter";
                    break;
                case Globals.Italy:
                    TxtB = "lampeggia";
                    break;
            }
        }
    }


    string TxtRB = "格挡/机甲机炮";
    public string TXTRB
    {
        get { return TxtRB; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtRB = "格挡/机甲机炮";
                    break;
                case Globals.JAPAN:
                    TxtRB = "ブロック/メカキャノン";
                    break;
                case Globals.ENGLISH:
                    TxtRB = "Block/Mecha Cannon";
                    break;
                case Globals.Portugal:
                    TxtRB = "Cañón de bloque/mecha";
                    break;
                case Globals.KOREAN:
                    TxtRB = "블록/메카 캐논";
                    break;
                case Globals.CHINESEF:
                    TxtRB = "格擋/機甲機砲";
                    break;
                case Globals.German:
                    TxtRB = "Block/Mecha-Kanone";
                    break;
                case Globals.French:
                    TxtRB = "Bloc/Mecha Canon";
                    break;
                case Globals.Italy:
                    TxtRB = "Blocco/cannone meccanico";
                    break;
            }
        }
    }


    string TxtLT = "机甲加力";
    public string TXTLT
    {
        get { return TxtLT; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtLT = "机甲加力";
                    break;
                case Globals.JAPAN:
                    TxtLT = "メカアフターバーナー";
                    break;
                case Globals.ENGLISH:
                    TxtLT = "Mecha afterburner";
                    break;
                case Globals.Portugal:
                    TxtLT = "poscombustión mecánica";
                    break;
                case Globals.KOREAN:
                    TxtLT = "메카 애프터버너";
                    break;
                case Globals.CHINESEF:
                    TxtLT = "機甲加力";
                    break;
                case Globals.German:
                    TxtLT = "Mecha Nachbrenner";
                    break;
                case Globals.French:
                    TxtLT = "Mecha postcombustion";
                    break;
                case Globals.Italy:
                    TxtLT = "Postcombustore meccanico";
                    break;
            }
        }
    }





}
