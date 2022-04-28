using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HZDate : MonoBehaviour {

    // Use this for initialization
    public string id = "";
    [Header("名字")]
    public string HZName;
  

    [Header("徽章装配特效的名字(用于徽章显示特效自动调用)")]
    public string HZZBTXName = "";
    [Header("出现特效的名字")]
    public string TXName;
    [Header("调用的名字")]
    public string objName;
    [Header("增加攻击力")]
    public float atk;
    [Header("增加攻击力比例")]
    public float atkP;
    [Header("增加防御力")]
    public float def;
    [Header("增加防御力比例")]
    public float defP;
    [Header("增加生命")]
    public float live;
    [Header("增加生命的比例")]
    public float liveP;

    [Header("增加蓝量")]
    public float addLan;


    [Header("附带技能")]
    public string skill;

    [Header("附带技能动作")]
    public string skillACName;


    [Header("技能动作的 喊声")]
    public string AudioName;

    [Header("附带技能动作  空中")]
    public string skillACNameInAir;
    [Header("空中释放技能 悬停时间")]
    public float AirXTTimes = 0.5f;
    [Header("技能动作起始延迟")]
    public float ACyanchi = 0;

    [Header("技能动作 结束延迟")]
    public float ACOveryanchi = 0;

    [Header("抗火")]
    public float kanghuo;
    [Header("是被动技能还是主动技能")]
    public string type = "bd";

    [Header("有UI的技能 **名字(显示UI的名字)")]
    public string zd_skill_ui_Name = "";
    [Header("技能CD")]
    public int cd = 0;
    [Header("技能可以连续使用次数")]
    public int usenums = 1;

    [Header("消耗蓝")]
    public float xyLan = 0;

    [Header("消耗血")]
    public float xyXue = 0;

    [Header("徽章介绍")]
    public string HZ_information = "";

    [Header("主动技能图片介绍")]
    public string imgName = "";

    public string RQName = "";

    [Header("被动防御技能-触发几率")]
    public float Chance_of_Passive_Skills = 0;

    [Header("防御效果")]
    public string def_effect = "";

    [Header("临时提高的硬直")]
    public float TempAddYingZhi = 0;

    [Header("临时提高的硬直的持续时间")]
    public float TempAddYingZhiTimes = 1;


    [Header("伤害减免比例")]
    public float ShanghaiJianmianBili = 0;


    [Header(" >> 临时伤害减免比例（0-100）")]
    public float TempShanghaiJianmianBili = 0;
    [Header(" -- 临时伤害减免比例 持续时间")]
    public float tempJSTimes = 0;

    //增加硬直
    [Header("*增加硬直*")]
    public float yingzhi = 0;
    [Header("*增加硬直百分比增加*")]
    public float yingzhiP = 0;
    //临时增加硬直

    [Header("暴击率")]
    public float BaoJiLv = 0;

    [Header("暴击伤害倍数")]
    public float BaoJiShangHaiBeiLv = 0;



    [Header("抗毒几率")]
    public float KangDuJilv = 0;
    [Header("抗中毒伤害几率")]
    public float KangDuShanghaiJilv = 0;


    [Header("抗 *火点燃* 几率")]
    public float KangHuoJilv = 0;
    [Header("*火* 伤害抵抗率")]
    public float KangHuoShanghaiJilv = 0;


    [Header("抗 *电* 几率")]
    public float KangDianJilv = 0;
    [Header("*电* 麻痹抵抗率")]
    public float KangDianMabiJilv = 0;


    [Header("*技能 恢复血量**")]
    public float HuifuXue = 0;



    void Start () {
        
    }

    int _cd = 0;
    
    public bool IsCDOver()
    {
        if (_cd > 0)
        {
            //_cd--;
            //print("cd  "+_cd);
            return false;
        }
        return true;
    }



    string hz_name = "徽章名字: ";
    public string HZ_NAME
    {
        //GET访问器，可以理解成另类的方法，返回已经被赋了值的私有变量a
        get { return hz_name; }
        //SET访问器，将我们打入的值赋给私有变量money
        set {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_name = "徽章名字: ";
                    break;
                case Globals.JAPAN:
                    hz_name = "名前: ";
                    break;
                case Globals.ENGLISH:
                    hz_name = "name: ";
                    break;
                case Globals.Portugal:
                    hz_name = "nome: ";
                    break;
                case Globals.KOREAN:
                    hz_name = "이름: ";
                    break;

            }

        }
    }
    string hz_information = "";
    public string HZ_INFORMATION
    {
        get { return hz_information; }
        set
        {
            hz_information = "";
            if (Globals.language== Globals.CHINESE)
            {
                if (id == "10001") {

                }
            }


            if (id == "10001")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "";
                        break;
                    case Globals.JAPAN:
                        hz_information = "";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "";
                        break;
                    case Globals.Portugal:
                        hz_information = "";
                        break;
                    case Globals.KOREAN:
                        hz_information = "";
                        break;
                }

            }else if (id == "10002")
            {

            }
            else if (id == "10003")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "宝珠周围大量能量附体，佩戴后增加防御力,可以释放防御子弹的护盾";
                        break;
                    case Globals.JAPAN:
                        hz_information = "オーブの周りには多くのエネルギーが付着しているため、着用後の防御力が高まり、弾丸から防御するシールドを解放することができます";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "A lot of energy is attached around the orb, which increases defense after wearing it, and can release a shield that defends against bullets";
                        break;
                    case Globals.Portugal:
                        hz_information = "Se adjunta mucha energía alrededor del orbe, lo que aumenta la defensa después de usarlo y puede liberar un escudo que defiende contra las balas.";
                        break;
                    case Globals.KOREAN:
                        hz_information = "오브 주변에 많은 에너지가 부착되어 있어 착용 후 방어력이 증가하고, 총알을 방어하는 방패를 해제할 수 있습니다.";
                        break;
                }
            }
            else if (id == "10004")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "能够释放出一道剑气";
                        break;
                    case Globals.JAPAN:
                        hz_information = "刀を抜くことができます";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Can release a sword";
                        break;
                    case Globals.Portugal:
                        hz_information = "Puede soltar una espada";
                        break;
                    case Globals.KOREAN:
                        hz_information = "검을 놓을 수 있다";
                        break;
                }
            }
            else if (id == "10005")
            {

            }
            else if (id == "10006")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "具有疗伤功能的神奇叶子，使用可以恢复一定量生命";
                        break;
                    case Globals.JAPAN:
                        hz_information = "癒しの性質を持つ魔法の葉、一定量の生命を回復するために使用";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Magical leaves with healing properties, use to restore a certain amount of life";
                        break;
                    case Globals.Portugal:
                        hz_information = "Hojas mágicas con propiedades curativas, utilízalas para restaurar cierta cantidad de vida.";
                        break;
                    case Globals.KOREAN:
                        hz_information = "치유력이 있는 마법의 잎, 일정량의 생명력을 회복하는 데 사용";
                        break;
                }
            }
            else if (id == "10007")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "提高一定的攻击力";
                        break;
                    case Globals.JAPAN:
                        hz_information = "特定の攻撃力を上げる";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Increase certain attack power";
                        break;
                    case Globals.Portugal:
                        hz_information = "Aumenta cierto poder de ataque";
                        break;
                    case Globals.KOREAN:
                        hz_information = "특정 공격력 증가";
                        break;
                }
            }
            else if (id == "10008")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "剑气乱舞，强大的攻击力";
                        break;
                    case Globals.JAPAN:
                        hz_information = "剣舞、強力な攻撃";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Sword dance, powerful attack";
                        break;
                    case Globals.Portugal:
                        hz_information = "Danza de la espada, ataque poderoso";
                        break;
                    case Globals.KOREAN:
                        hz_information = "검춤, 강력한 공격";
                        break;
                }
            }
            else if (id == "10012")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "消散的花瓣抵挡一次伤害";
                        break;
                    case Globals.JAPAN:
                        hz_information = "散逸した花びらは1つのダメージに抵抗します";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Dissipated petals resist one damage";
                        break;
                    case Globals.Portugal:
                        hz_information = "Los pétalos disipados resisten un daño.";
                        break;
                    case Globals.KOREAN:
                        hz_information = "흩어진 꽃잎은 하나의 피해에 저항합니다.";
                        break;
                }
            }
            else if (id == "10013")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "减少麻痹的几率和电麻痹时间";
                        break;
                    case Globals.JAPAN:
                        hz_information = "麻痺の可能性と電気麻痺の持続時間の短縮";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Reduced paralysis chance and electric paralysis duration";
                        break;
                    case Globals.Portugal:
                        hz_information = "Posibilidad de parálisis reducida y duración de la parálisis eléctrica.";
                        break;
                    case Globals.KOREAN:
                        hz_information = "마비 확률 및 전기 마비 지속 시간 감소";
                        break;
                }
            }
            else if (id == "10015")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "增加最大生命值";
                        break;
                    case Globals.JAPAN:
                        hz_information = "最大体力を増やす";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Increase max health";
                        break;
                    case Globals.Portugal:
                        hz_information = "Aumentar la salud máxima";
                        break;
                    case Globals.KOREAN:
                        hz_information = "최대 체력 증가";
                        break;
                }
            }
            else if (id == "10016")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "丢出一枚毒气炸弹";
                        break;
                    case Globals.JAPAN:
                        hz_information = "ガス爆弾を落とす";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "rop a gas bomb";
                        break;
                    case Globals.Portugal:
                        hz_information = "lanzar una bomba de gas";
                        break;
                    case Globals.KOREAN:
                        hz_information = "가스폭탄을 떨어뜨리다";
                        break;
                }
            }
            else if (id == "10018")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "丢出3枚毒气炸弹";
                        break;
                    case Globals.JAPAN:
                        hz_information = "ガス爆弾を3発投げる";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Throw 3 gas bombs";
                        break;
                    case Globals.Portugal:
                        hz_information = "Lanza 3 bombas de gas";
                        break;
                    case Globals.KOREAN:
                        hz_information = "가스 폭탄 3개 던지기";
                        break;
                }
            }
            else if (id == "10019")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "放出一道移动电墙";
                        break;
                    case Globals.JAPAN:
                        hz_information = "モバイル電気壁を解放します";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Release a mobile electric wall";
                        break;
                    case Globals.Portugal:
                        hz_information = "Liberar una pared eléctrica móvil";
                        break;
                    case Globals.KOREAN:
                        hz_information = "이동식 전기벽 해제";
                        break;
                }
            }
            else if (id == "10020"|| id == "10021")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "有一定几率暴击";
                        break;
                    case Globals.JAPAN:
                        hz_information = "ヒットするチャンスがあります";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Has a chance to hit";
                        break;
                    case Globals.Portugal:
                        hz_information = "Tiene la oportunidad de golpear";
                        break;
                    case Globals.KOREAN:
                        hz_information = "칠 기회가 있다";
                        break;
                }
            }
            else if (id == "10022")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "发动一次强大攻击力的攻击";
                        break;
                    case Globals.JAPAN:
                        hz_information = "強力な攻撃を解き放つ";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Unleash a powerful attack";
                        break;
                    case Globals.Portugal:
                        hz_information = "Desata un poderoso ataque";
                        break;
                    case Globals.KOREAN:
                        hz_information = "강력한 공격을 펼친다";
                        break;
                }
            }
            else if (id == "10023")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "被攻击时候触发护盾，在3秒内减少伤害的80%,并且提高800的硬直";
                        break;
                    case Globals.JAPAN:
                        hz_information = "攻撃されると、シールドがトリガーされ、3秒以内にダメージが80％減少し、硬度が800増加します。";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "When attacked, the shield is triggered, reducing damage by 80% within 3 seconds, and increasing the hardness by 800";
                        break;
                    case Globals.Portugal:
                        hz_information = "Cuando es atacado, el escudo se activa, lo que reduce el daño en un 80 % en 3 segundos y aumenta la dureza en 800";
                        break;
                    case Globals.KOREAN:
                        hz_information = "공격 시 보호막이 발동되어 3초 내 데미지 80% 감소, 경도 800 증가";
                        break;
                }
            }
            else if (id == "10024")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "在濒临死亡时有30%几率恢复50%的血,并且持续4秒减免80%伤害";
                        break;
                    case Globals.JAPAN:
                        hz_information = "死ぬとき、4秒間で血液の50％を回復し、ダメージの80％を減らす30％のチャンスがあります";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "When dying, there is a 30% chance to restore 50% of the blood and reduce 80% of damage for 4 seconds";
                        break;
                    case Globals.Portugal:
                        hz_information = "Al morir, hay un 30 % de probabilidad de restaurar el 50 % de la sangre y reducir el 80 % del daño durante 4 segundos.";
                        break;
                    case Globals.KOREAN:
                        hz_information = "사망 시 30% 확률로 4초 동안 50%의 혈액을 회복하고 80%의 피해를 감소시킵니다.";
                        break;
                }
            }
            else if (id == "10025")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "在空中向地面丢4枚火焰弹";
                        break;
                    case Globals.JAPAN:
                        hz_information = "空中で4つの火爆弾を地面に投げます";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Throws 4 fire bombs to the ground in the air";
                        break;
                    case Globals.Portugal:
                        hz_information = "Lanza 4 bombas incendiarias al suelo en el aire.";
                        break;
                    case Globals.KOREAN:
                        hz_information = "공중의 지면에 화염 폭탄 4개를 던집니다.";
                        break;
                }
            }
            else if (id == "10014")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "减少中毒的几率和伤害";
                        break;
                    case Globals.JAPAN:
                        hz_information = "中毒の可能性とダメージの減少";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Reduced chance and damage of poisoning";
                        break;
                    case Globals.Portugal:
                        hz_information = "Reducción de la probabilidad y el daño de envenenamiento.";
                        break;
                    case Globals.KOREAN:
                        hz_information = "중독 확률 및 피해 감소";
                        break;
                }
            }
            else if (id == "10017")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "火的庇佑，攻击附带火焰效果";
                        break;
                    case Globals.JAPAN:
                        hz_information = "火の祝福、火の効果で攻撃";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Blessing of fire, attack with fire effect";
                        break;
                    case Globals.Portugal:
                        hz_information = "Bendición de fuego, ataque con efecto de fuego.";
                        break;
                    case Globals.KOREAN:
                        hz_information = "불의 축복, 화염 효과로 공격";
                        break;
                }
            }
            else if (id == "10026")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "减少被火焰点燃几率和火焰带来的伤害";
                        break;
                    case Globals.JAPAN:
                        hz_information = "火災や火災による損傷の可能性を減らします";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Reduces chance of being ignited by fire and damage from fire";
                        break;
                    case Globals.Portugal:
                        hz_information = "Reduce la posibilidad de ser encendido por fuego y daño por fuego.";
                        break;
                    case Globals.KOREAN:
                        hz_information = "화재 및 화재 피해 감소";
                        break;
                }
            }
            else if (id == "10027")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        hz_information = "能再空中额外跳跃两次，并且重置前冲";
                        break;
                    case Globals.JAPAN:
                        hz_information = "空中でさらに2回ジャンプでき、前進推力をリセットします";
                        break;
                    case Globals.ENGLISH:
                        hz_information = "Can jump an additional two times in the air and resets the forward thrust";
                        break;
                    case Globals.Portugal:
                        hz_information = "Puede saltar dos veces más en el aire y restablece el empuje hacia adelante";
                        break;
                    case Globals.KOREAN:
                        hz_information = "공중에서 추가로 2회 점프할 수 있으며 전방 추력을 재설정합니다.";
                        break;
                }
            }




        }
    }



    public string HZ_NAME2
    {
        get { return HZName; }
        set
        {
            if (Globals.language == Globals.CHINESE)
            {
                if (id == "10001")
                {

                }
            }


            if (id == "10001")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "";
                        break;
                    case Globals.JAPAN:
                        HZName = "";
                        break;
                    case Globals.ENGLISH:
                        HZName = "";
                        break;
                    case Globals.Portugal:
                        HZName = "";
                        break;
                    case Globals.KOREAN:
                        HZName = "";
                        break;
                }

            }
            else if (id == "10002")
            {

            }
            else if (id == "10003")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "护盾宝珠";
                        break;
                    case Globals.JAPAN:
                        HZName = "シールドオーブ";
                        break;
                    case Globals.ENGLISH:
                        HZName = "Shield Orb";
                        break;
                    case Globals.Portugal:
                        HZName = "Orbe de escudo";
                        break;
                    case Globals.KOREAN:
                        HZName = "쉴드 오브";
                        break;
                }
            }
            else if (id == "10004")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "月光";
                        break;
                    case Globals.JAPAN:
                        HZName = "月光";
                        break;
                    case Globals.ENGLISH:
                        HZName = "moonlight";
                        break;
                    case Globals.Portugal:
                        HZName = "luz de la luna";
                        break;
                    case Globals.KOREAN:
                        hz_information = "월광";
                        break;
                }
            }
            else if (id == "10005")
            {

            }
            else if (id == "10006")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "生命叶";
                        break;
                    case Globals.JAPAN:
                        HZName = "生命の葉";
                        break;
                    case Globals.ENGLISH:
                        HZName = "leaf of life";
                        break;
                    case Globals.Portugal:
                        HZName = "hoja de vida";
                        break;
                    case Globals.KOREAN:
                        HZName = "생명의 잎";
                        break;
                }
            }
            else if (id == "10007")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "重锋之刃";
                        break;
                    case Globals.JAPAN:
                        HZName = "ヘビーブレード";
                        break;
                    case Globals.ENGLISH:
                        HZName = "Heavy Blade";
                        break;
                    case Globals.Portugal:
                        HZName = "Hoja pesada";
                        break;
                    case Globals.KOREAN:
                        HZName = "헤비 블레이드";
                        break;
                }
            }
            else if (id == "10008")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "乱刃";
                        break;
                    case Globals.JAPAN:
                        HZName = "ランブル";
                        break;
                    case Globals.ENGLISH:
                        HZName = "Rumble";
                        break;
                    case Globals.Portugal:
                        HZName = "Retumbar";
                        break;
                    case Globals.KOREAN:
                        HZName = "하인 좌석";
                        break;
                }
            }
            else if (id == "10012")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "花之盾";
                        break;
                    case Globals.JAPAN:
                        HZName = "花の盾";
                        break;
                    case Globals.ENGLISH:
                        HZName = "shield of flowers";
                        break;
                    case Globals.Portugal:
                        HZName = "escudo de flores";
                        break;
                    case Globals.KOREAN:
                        HZName = "꽃의 방패";
                        break;
                }
            }
            else if (id == "10013")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "抗电徽章";
                        break;
                    case Globals.JAPAN:
                        HZName = "反電気バッジ";
                        break;
                    case Globals.ENGLISH:
                        HZName = "Anti-electric badge";
                        break;
                    case Globals.Portugal:
                        HZName = "Insignia anti-eléctrica";
                        break;
                    case Globals.KOREAN:
                        HZName = "정전기 방지 배지";
                        break;
                }
            }
            else if (id == "10015")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "生命徽章";
                        break;
                    case Globals.JAPAN:
                        HZName = "ライフバッジ";
                        break;
                    case Globals.ENGLISH:
                        HZName = "life badge";
                        break;
                    case Globals.Portugal:
                        HZName = "insignia de vida";
                        break;
                    case Globals.KOREAN:
                        HZName = "생활 배지";
                        break;
                }
            }
            else if (id == "10016")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "毒炸弹";
                        break;
                    case Globals.JAPAN:
                        HZName = "毒爆弾";
                        break;
                    case Globals.ENGLISH:
                        HZName = "poison bomb";
                        break;
                    case Globals.Portugal:
                        HZName = "bomba de veneno";
                        break;
                    case Globals.KOREAN:
                        HZName = "독 폭탄";
                        break;
                }
            }
            else if (id == "10018")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "毒云炸弹";
                        break;
                    case Globals.JAPAN:
                        HZName = "毒雲爆弾";
                        break;
                    case Globals.ENGLISH:
                        HZName = "Poison Cloud Bomb";
                        break;
                    case Globals.Portugal:
                        HZName = "Bomba de nube de veneno";
                        break;
                    case Globals.KOREAN:
                        HZName = "독구름 폭탄";
                        break;
                }
            }
            else if (id == "10019")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "电闪";
                        break;
                    case Globals.JAPAN:
                        HZName = "ライトニング";
                        break;
                    case Globals.ENGLISH:
                        HZName = "lightning";
                        break;
                    case Globals.Portugal:
                        HZName = "relámpago";
                        break;
                    case Globals.KOREAN:
                        HZName = "번개";
                        break;
                }
            }
            else if (id == "10020" || id == "10021")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "暴击";
                        break;
                    case Globals.JAPAN:
                        HZName = "クリティカル";
                        break;
                    case Globals.ENGLISH:
                        HZName = "crit";
                        break;
                    case Globals.Portugal:
                        HZName = "crítico";
                        break;
                    case Globals.KOREAN:
                        HZName = "치명타";
                        break;
                }
            }
            else if (id == "10022")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "重切";
                        break;
                    case Globals.JAPAN:
                        HZName = "重いスラッシュ";
                        break;
                    case Globals.ENGLISH:
                        HZName = "heavy slash";
                        break;
                    case Globals.Portugal:
                        HZName = "corte pesado";
                        break;
                    case Globals.KOREAN:
                        HZName = "굵은 슬래시";
                        break;
                }
            }
            else if (id == "10023")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "神之盾";
                        break;
                    case Globals.JAPAN:
                        HZName = "神のイージス";
                        break;
                    case Globals.ENGLISH:
                        HZName = "Aegis of God";
                        break;
                    case Globals.Portugal:
                        HZName = "Égida de Dios";
                        break;
                    case Globals.KOREAN:
                        HZName = "신의 아이기스";
                        break;
                }
            }
            else if (id == "10024")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "神之祐";
                        break;
                    case Globals.JAPAN:
                        HZName = "神の祝福";
                        break;
                    case Globals.ENGLISH:
                        HZName = "God's blessing";
                        break;
                    case Globals.Portugal:
                        HZName = "bendición de Dios";
                        break;
                    case Globals.KOREAN:
                        HZName = "신의 축복";
                        break;
                }
            }
            else if (id == "10025")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "地火";
                        break;
                    case Globals.JAPAN:
                        HZName = "地上火災";
                        break;
                    case Globals.ENGLISH:
                        HZName = "ground fire";
                        break;
                    case Globals.Portugal:
                        HZName = "fuego de tierra";
                        break;
                    case Globals.KOREAN:
                        HZName = "지상 불";
                        break;
                }
            }
            else if (id == "10014")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "毒抗徽章";
                        break;
                    case Globals.JAPAN:
                        HZName = "毒耐性バッジ";
                        break;
                    case Globals.ENGLISH:
                        HZName = "Poison Resist Badge";
                        break;
                    case Globals.Portugal:
                        HZName = "Insignia de resistencia al veneno";
                        break;
                    case Globals.KOREAN:
                        HZName = "독 저항 배지";
                        break;
                }
            }
            else if (id == "10017")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "流刃若火";
                        break;
                    case Globals.JAPAN:
                        HZName = "火のように流れる";
                        break;
                    case Globals.ENGLISH:
                        HZName = "flow like fire";
                        break;
                    case Globals.Portugal:
                        HZName = "fluye como el fuego";
                        break;
                    case Globals.KOREAN:
                        HZName = "불처럼 흐르다";
                        break;
                }
            }
            else if (id == "10026")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "火之盾";
                        break;
                    case Globals.JAPAN:
                        HZName = "ファイアシールド";
                        break;
                    case Globals.ENGLISH:
                        HZName = "Fire Shield";
                        break;
                    case Globals.Portugal:
                        HZName = "Escudo de fuego";
                        break;
                    case Globals.KOREAN:
                        HZName = "파이어 쉴드";
                        break;
                }
            }
            else if (id == "10027")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        HZName = "化羽";
                        break;
                    case Globals.JAPAN:
                        HZName = "フェザー";
                        break;
                    case Globals.ENGLISH:
                        HZName = "feather";
                        break;
                    case Globals.Portugal:
                        HZName = "pluma";
                        break;
                    case Globals.KOREAN:
                        HZName = "깃털";
                        break;
                }
            }




        }
    }



    string hz_atk = "攻击力: ";

    public string HZ_ATK
    {
        get { return hz_atk; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_atk = "攻击力: ";
                    break;
                case Globals.JAPAN:
                    hz_atk = "攻撃力: ";
                    break;
                case Globals.ENGLISH:
                    hz_atk = "atk: ";
                    break;
                case Globals.Portugal:
                    hz_atk = "atk: ";
                    break;
                case Globals.KOREAN:
                    hz_atk = "공격력: ";
                    break;
            }
        }
    }



    string hz_atkP = "攻击力倍数: ";
    public string HZ_ATKP
    {
        get { return hz_atkP; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_atkP = "攻击力倍数: ";
                    break;
                case Globals.JAPAN:
                    hz_atkP = "攻撃力乗数: ";
                    break;
                case Globals.ENGLISH:
                    hz_atkP = "atk: ";
                    break;
                case Globals.Portugal:
                    hz_atkP = "atk: ";
                    break;
                case Globals.KOREAN:
                    hz_atkP = "공격력: ";
                    break;
            }
        }
    }


    string hz_def = "防御力: ";
    public string HZ_DEF
    {
        get { return hz_def; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_def = "防御力: ";
                    break;
                case Globals.JAPAN:
                    hz_def = "防衛: ";
                    break;
                case Globals.ENGLISH:
                    hz_def = "def: ";
                    break;
                case Globals.Portugal:
                    hz_def = "def: ";
                    break;
                case Globals.KOREAN:
                    hz_def = "방어: ";
                    break;
            }
        }
    }

    string hz_defP = "防御力倍数：";
    public string HZ_DEFP
    {
        get { return hz_defP; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_defP = "防御力倍数: ";
                    break;
                case Globals.JAPAN:
                    hz_defP = "防衛乗数: ";
                    break;
                case Globals.ENGLISH:
                    hz_defP = "defP: ";
                    break;
                case Globals.Portugal:
                    hz_defP = "defP: ";
                    break;
                case Globals.KOREAN:
                    hz_defP = "방어 승수: ";
                    break;
            }
        }
    }



    string hz_live = "生命值: ";
    public string HZ_LIVE
    {
        get { return hz_live; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_live = "生命值: ";
                    break;
                case Globals.JAPAN:
                    hz_live = "生命価値: ";
                    break;
                case Globals.ENGLISH:
                    hz_live = "live: ";
                    break;
                case Globals.Portugal:
                    hz_live = "valor de vida: ";
                    break;
                case Globals.KOREAN:
                    hz_live = "삶의 가치: ";
                    break;
            }
        }
    }

    string hz_liveP = "生命值倍数: ";
    public string HZ_LIVEP
    {
        get { return hz_liveP; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_liveP = "生命值倍数: ";
                    break;
                case Globals.JAPAN:
                    hz_liveP = "生命乗数: ";
                    break;
                case Globals.ENGLISH:
                    hz_liveP = "liveP: ";
                    break;
                case Globals.Portugal:
                    hz_liveP = "multiplicador de vida: ";
                    break;
                case Globals.KOREAN:
                    hz_liveP = "생명 승수: ";
                    break;
            }
        }
    }

    string hz_baojilv = "暴击率: ";
    public string HZ_BAOJILV
    {
        get { return hz_baojilv; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_baojilv = "暴击率: ";
                    break;
                case Globals.JAPAN:
                    hz_baojilv = "クリティカルチャンス: ";
                    break;
                case Globals.ENGLISH:
                    hz_baojilv = "crit chance: ";
                    break;
                case Globals.Portugal:
                    hz_baojilv = "oportunidad crítica: ";
                    break;
                case Globals.KOREAN:
                    hz_baojilv = "중요한 기회: ";
                    break;
            }
        }
    }

    string hz_baojishanghaibeishu = "暴击伤害倍数: ";
    public string HZ_BAOJISHANGHAILV
    {
        get { return hz_baojishanghaibeishu; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_baojishanghaibeishu = "暴击伤害倍数: ";
                    break;
                case Globals.JAPAN:
                    hz_baojishanghaibeishu = "クリティカルダメージ乗数: ";
                    break;
                case Globals.ENGLISH:
                    hz_baojishanghaibeishu = "Crit Damage Multiplier: ";
                    break;
                case Globals.Portugal:
                    hz_baojishanghaibeishu = "Multiplicador de daño crítico: ";
                    break;
                case Globals.KOREAN:
                    hz_baojishanghaibeishu = "치명타 피해 배율: ";
                    break;
            }
        }
    }


    string hz_kangdujilv = "抗毒几率: ";
    public string HZ_KANGDULV
    {
        get { return hz_kangdujilv; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_kangdujilv = "抗毒几率: ";
                    break;
                case Globals.JAPAN:
                    hz_kangdujilv = "抗毒のチャンス: ";
                    break;
                case Globals.ENGLISH:
                    hz_kangdujilv = "Antivirus: ";
                    break;
                case Globals.Portugal:
                    hz_kangdujilv = "antivirus: ";
                    break;
                case Globals.KOREAN:
                    hz_kangdujilv = "독극물 기회: ";
                    break;
            }
        }
    }

    string hz_kangdushanghaijilv = "毒伤害抵抗率: ";
    public string HZ_KANGDUSHANGHAILV
    {
        get { return hz_kangdushanghaijilv; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_kangdushanghaijilv = "毒伤害抵抗率: ";
                    break;
                case Globals.JAPAN:
                    hz_kangdushanghaijilv = "毒ダメージ: ";
                    break;
                case Globals.ENGLISH:
                    hz_kangdushanghaijilv = "Poison Damage: ";
                    break;
                case Globals.Portugal:
                    hz_kangdushanghaijilv = "Daño por veneno: ";
                    break;
                case Globals.KOREAN:
                    hz_kangdushanghaijilv = "독 피해: ";
                    break;
            }
        }
    }


    string hz_kanghuojilv = "抗火几率: ";
    public string HZ_KANGHUOLV
    {
        get { return hz_kanghuojilv; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_kanghuojilv = "抗火几率: ";
                    break;
                case Globals.JAPAN:
                    hz_kanghuojilv = "難燃性: ";
                    break;
                case Globals.ENGLISH:
                    hz_kanghuojilv = "fire resistant: ";
                    break;
                case Globals.Portugal:
                    hz_kanghuojilv = "resistente al fuego: ";
                    break;
                case Globals.KOREAN:
                    hz_kanghuojilv = "내화성: ";
                    break;
            }
        }
    }

    string hz_kanghuoshanghaijilv = "火伤害抵抗率: ";
    public string HZ_KANGHUOSHANGHAILV
    {
        get { return hz_kanghuoshanghaijilv; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_kanghuoshanghaijilv = "火伤害抵抗率: ";
                    break;
                case Globals.JAPAN:
                    hz_kanghuoshanghaijilv = "火災によるダメージ: ";
                    break;
                case Globals.ENGLISH:
                    hz_kanghuoshanghaijilv = "Fire damage: ";
                    break;
                case Globals.Portugal:
                    hz_kanghuoshanghaijilv = "Daño por fuego: ";
                    break;
                case Globals.KOREAN:
                    hz_kanghuoshanghaijilv = "화재 피해: ";
                    break;
            }
        }
    }





    string hz_kangdianjilv = "抗电几率: ";
    public string HZ_KANGDIANJL
    {
        get { return hz_kangdianjilv; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_kangdianjilv = "抗电几率: ";
                    break;
                case Globals.JAPAN:
                    hz_kangdianjilv = "反電気: ";
                    break;
                case Globals.ENGLISH:
                    hz_kangdianjilv = "Anti-electric: ";
                    break;
                case Globals.Portugal:
                    hz_kangdianjilv = "Anti-eléctrico: ";
                    break;
                case Globals.KOREAN:
                    hz_kangdianjilv = "정전기 방지: ";
                    break;
            }
        }
    }

    string hz_kangdianmabijilv = "抗电麻痹几率: ";
    public string HZ_KANGDIANMABIJL
    {
        get { return hz_kangdianmabijilv; }
        set
        {
            switch (Globals.language)
            {
                case Globals.CHINESE:
                    hz_kangdianmabijilv = "抗电麻痹几率: ";
                    break;
                case Globals.JAPAN:
                    hz_kangdianmabijilv = "麻痺防止: ";
                    break;
                case Globals.ENGLISH:
                    hz_kangdianmabijilv = "Anti-paralysis: ";
                    break;
                case Globals.Portugal:
                    hz_kangdianmabijilv = "Anti-parálisis: ";
                    break;
                case Globals.KOREAN:
                    hz_kangdianmabijilv = "항마비: ";
                    break;
            }
        }
    }



    public string GetHZ_information_str()
    {
        //做一次 语言判断
        print( "   语言类型    "+Globals.languageType+"   名字是什么？？？  "+ HZ_NAME);

        HZ_NAME = "";
        HZ_NAME2 = "";
        string str = "";
        string _name =  "<color=#FFFF00>"+ HZ_NAME + this.HZ_NAME2 + "</color>\n";
        str += _name;



        HZ_ATK = "";
        print("  HZ_ATK   "+ HZ_ATK+"   ***   ???? "+ hz_atk);
        string _atkStr = "";
        if (atk != 0)
        {
            _atkStr = "<color=#E74C3C>"+ HZ_ATK + "+" + this.atk + "</color>\n";
        }
        str += _atkStr;
        HZ_ATKP = "";

        string _atkPStr = "";
        if (atkP != 0)
        {
            _atkPStr = Globals.language ="<color=#E74C3C>"+ HZ_ATKP + this.atkP + "</color>\n";
        }
        str += _atkPStr;


        HZ_DEF = "";
        string _defStr = "";
        if (def != 0)
        {
            _defStr = "<color=#5DADE2>"+ HZ_DEF + "+" + this.def + "</color>\n" ;
        }
        str += _defStr;


        HZ_DEFP = "";
        string _defPStr = "";
        if (defP != 0)
        {
            _defPStr = "<color=#5DADE2>"+ HZ_DEFP + this.defP + "</color>\n" ;
        }
        str += _defPStr;


        HZ_LIVE = "";
        string _liveStr = "";
        if (live != 0)
        {
            _liveStr = "<color=#76D7C4>"+ HZ_LIVE + "+"+this.live + "</color>\n" ;
        }
        str += _liveStr;

        HZ_LIVEP = "";
        string _livePStr = "";
        if (liveP != 0)
        {
            _livePStr = "<color=#76D7C4>"+ HZ_LIVEP + this.liveP + "</color>\n" ;
        }
        str += _livePStr;

        HZ_BAOJILV = "";
        string _baojilv = "";
        if (BaoJiLv != 0)
        {
            _baojilv =  "<color=#06D7C4>" + HZ_BAOJILV + this.BaoJiLv + "</color>\n";
        }
        str += _baojilv;

        HZ_BAOJISHANGHAILV = "";
        string _baojishanghaibeishu = "";
        if (BaoJiShangHaiBeiLv != 0)
        {
            _baojishanghaibeishu = "<color=#76D704>" + HZ_BAOJISHANGHAILV + this.BaoJiShangHaiBeiLv + "</color>\n";
        }
        str += _baojishanghaibeishu;



        HZ_KANGHUOLV = "";
        string _kanghuojilv = "";
        if (KangHuoJilv!=0)
        {
            _kanghuojilv = "<color=#BBFFFF>" + HZ_KANGHUOLV + this.KangHuoJilv + "</color>\n";
        }
        str += _kanghuojilv;

        HZ_KANGDUSHANGHAILV = "";
        string _kanghuoshanghaijilv = "";
        if (KangHuoShanghaiJilv != 0)
        {
            _kanghuoshanghaijilv = "<color=#BBFFFF>" + this.HZ_KANGDUSHANGHAILV + this.KangHuoShanghaiJilv + "</color>\n" ;
        }
        str += _kanghuoshanghaijilv;





        HZ_KANGDULV = "";
        string _kangdujilv = "";
        if (this.KangDuJilv != 0)
        {
            _kangdujilv =  "<color=#8EE5EE>" + this.HZ_KANGDULV + this.KangDuJilv + "</color>\n" ;
        }
        str += _kangdujilv;

        HZ_KANGDUSHANGHAILV = "";
        string _kangdushanghaijiv = "";
        if (this.KangDuShanghaiJilv != 0)
        {
            _kangdushanghaijiv = "<color=#8EE5EE>" + this.HZ_KANGDUSHANGHAILV + this.KangDuShanghaiJilv + "</color>\n";
        }
        str += _kangdushanghaijiv;





        HZ_KANGDIANJL = "";
        string _kangdianjilv = "";
        if (this.KangDianJilv != 0)
        {
            _kangdianjilv = "<color=#00E5EE>" + this.HZ_KANGDIANJL + this.KangDianJilv + "</color>\n";
        }
        str += _kangdianjilv;

        HZ_KANGDIANMABIJL = "";
        string _kangdianmabijilv = "";
        if (this.KangDianMabiJilv != 0)
        {
            _kangdianmabijilv ="<color=#00E5EE>" + this.HZ_KANGDIANMABIJL + this.KangDianMabiJilv + "</color>\n";
        }
        str += _kangdianmabijilv;



        HZ_INFORMATION = "";
        string _information = "";
        if (HZ_information != "")
        {
            _information =  "\n<color=#CC99ff>" + this.HZ_INFORMATION + "</color>\n";
        }
        str += _information;

        return str;
    }


    public int GetCdNums()
    {
        return _cd;
    }

    public void StartCD()
    {
        _cd = cd;
        Jishi();
    }

    void Jishi()
    {
        if (_cd > 0) StartCoroutine(IETimeBySeconds());
    }

    public void OnDestroy()
    {
        StopCoroutine(IETimeBySeconds());
    }



    public IEnumerator IETimeBySeconds()
    {
        yield return new WaitForSeconds(1);
        if (_cd > 0) {
            _cd--;
            Jishi();
        }
    }


    // Update is called once per frame
    void Update () {
        //if(_cd>0)IsCDOver();
    }
}
