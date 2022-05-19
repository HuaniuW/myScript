using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillSM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (IsStart) {
            GetStart();
            Globals.isInPlot = true;
        }
    }

    //计时 自己先删除
    //按键删除


    float Jishi = 0;
    float removeSelfTime = 5;


    void RemoveSelfByTimes()
    {
        if (Globals.isInPlot)
        {
            //Jishi += Time.deltaTime;
            //if (Jishi>= removeSelfTime)
            //{
            //    Globals.isInPlot = false;
            //    this.gameObject.SetActive(false);
            //}


            if(Input.GetKeyUp(KeyCode.K)|| Input.GetKeyUp(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                print("   ---------------------技能说明 ");
                Globals.isInPlot = false;
                this.gameObject.SetActive(false);
            }

        }
    }

    private void Update()
    {
        RemoveSelfByTimes();
    }




    public bool IsStart = false;

    public Text U;

    public void GetStart()
    {
        print("  键盘说明   " + Globals.language);
        TXTU = "";
        U.text = TXTU;
      
    }



    string TxtU = "将徽章放入徽章格。如果是<color=#ff4500>主动技能</color>徽章,放入前三个徽章栏,用<color=#ffff0f>方向键+技能按键</color>来释放相应位置徽章技能<color=#ffff0f>（从左到右对应-格子1:方向键上+技能释放按钮，格子2:直接按技能释放，格子3:下+技能释放-来释放主动技能）</color>.";
    public string TXTU
    {
        get { return TxtU; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    TxtU = "将徽章放入徽章格。如果是<color=#ff4500>主动技能</color>徽章,放入前三个徽章栏,用<color=#ffff0f>方向键+技能按键</color>来释放相应位置徽章技能<color=#ffff0f>（从左到右对应-格子1:方向键上+技能释放按钮，格子2:直接按技能释放，格子3:下+技能释放-来释放主动技能）</color>.";
                    break;
                case Globals.JAPAN:
                    TxtU = "バッジをバッジスロットに入れます。 <color=#ff4500>アクティブスキル</color>バッジの場合は、最初の3つのバッジスロットに配置し、<color=#ffff0f>方向キー+スキルボタン</color>を使用してバッジスキルを解放します対応する位置で<color=#ffff0f>（左から右に対応-グリッド1：矢印キー上+スキルリリースボタン、グリッド2：スキルを直接押してリリース、グリッド3：下+スキルリリース-アクティブをリリーススキル）</color>。";
                    break;
                case Globals.ENGLISH:
                    TxtU = "Put the badge in the badge slot. If it is a <color=#ff4500>active skill</color> badge, put it in the first three badge slots, and use the <color=#ffff0f>direction key+skill button</color> to release the badge skill at the corresponding position<color=#ffff0f>(Corresponding from left to right - grid 1: arrow key up + skill release button, grid 2: directly press the skill to release, grid 3: down + skill release- to release the active skill)</color>.";
                    break;
                case Globals.Portugal:
                    TxtU = "Coloque la insignia en la ranura de la insignia. Si se trata de una insignia de <color=#ff4500>habilidad activa</color>, colóquela en las tres primeras ranuras de la insignia y use la <color=#ffff0f>tecla de dirección+botón de habilidad</color> para liberar la habilidad de la insignia. en la posición correspondiente<color=#ffff0f>(Correspondiente de izquierda a derecha - cuadrícula 1: tecla de flecha hacia arriba + botón de liberación de habilidad, cuadrícula 2: presione directamente la habilidad para liberar, cuadrícula 3: abajo + liberación de habilidad- para liberar el activo habilidad)</color>";
                    break;
                case Globals.KOREAN:
                    TxtU = "배지 슬롯에 배지를 넣습니다. <color=#ff4500>액티브 스킬</color> 배지인 경우 처음 세 개의 배지 슬롯에 넣고 <color=#ffff0f>방향키+스킬 버튼</color>을 사용하여 배지 스킬을 해제합니다. 해당 위치에서<color=#ffff0f>(왼쪽에서 오른쪽으로 해당 - 그리드 1: 화살표 키 위 + 스킬 해제 버튼, 그리드 2: 스킬을 직접 누르면 해제, 그리드 3: 아래로 + 스킬 해제- 활성 해제 스킬)</color>.";
                    break;
                case Globals.CHINESEF:
                    TxtU = "將徽章放入徽章格。如果是<color=#ff4500>主動技能</color>徽章,放入前三個徽章欄,用<color=#ffff0f>方向鍵+技能按鍵</color>來釋放相應位置徽章技能<color=#ffff0f>（從左到右對應-格子1:方向鍵上+技能釋放按鈕，格子2:直接按技能釋放，格子3:下+技能釋放-來釋放主動技能）</color>.";
                    break;
                case Globals.German:
                    TxtU = "Stecken Sie den Ausweis in den Ausweisschlitz. Wenn es sich um ein <color=#ff4500>aktives Fertigkeitsabzeichen</color> handelt, legen Sie es in die ersten drei Abzeichenplätze und verwenden Sie die <color=#ffff0f>Richtungstaste+Fähigkeitstaste</color>, um die Abzeichenfertigkeit freizugeben an der entsprechenden Position<color=#ffff0f>(Entsprechend von links nach rechts - Raster 1: Pfeiltaste nach oben + Skill-Release-Taste, Raster 2: Direktes Drücken des Skills zum Freigeben, Raster 3: Runter + Skill-Release- zum Freigeben des aktiven Fertigkeit)</color>.";
                    break;
                case Globals.French:
                    TxtU = "Placez le badge dans la fente du badge. S'il s'agit d'un badge de <color=#ff4500>compétence active</color>, placez-le dans les trois premiers emplacements de badge et utilisez la <color=#ffff0f>touche de direction + bouton de compétence</color> pour libérer le badge de compétence à la position correspondante<color=#ffff0f>(Correspondant de gauche à droite - grille 1 : flèche vers le haut + bouton de libération de la compétence, grille 2 : appuyez directement sur la compétence pour la libérer, grille 3 : vers le bas + libération de la compétence - pour libérer l'actif compétence)</color>.";
                    break;
                case Globals.Italy:
                    TxtU = "Metti il ​​badge nell'apposito slot. In corrispondenza della posizione del badge con abilità attive, il tasto di direzione + pulsante abilità, rilascia l'abilità del badge della posizione corrispondente";
                    break;
            }
        }
    }
}
