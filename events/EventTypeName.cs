using System.Collections;

public class EventTypeName
{
    public const string CUBE_CLICK = "cube_click";
    public const string OPEN_DOOR = "open_door";

    public const string JINGSHI = "jingshi";


    public const string All_DIE_OPEN_DOOR = "all_die_open_door";

    //切换场景
    public const string CHANGE_SCREEN = "change_screen";
    //获取敌人
    public const string GET_ENEMY = "GET_ENEMY";

    public const string RECORDOBJ_CHANGE = "recordObj_change";
    //徽章切换
    public const string CHANGE_HZ = "change_hz";

    public const string CHANEG_LIVE = "change_live";

    //角色是否能操控
    public const string ROLECANCONTROL = "roleCanControl";

    //怪物死亡掉落 不能用 全部没死的怪也会侦听到
    //public const string GUAI_DIE_OUT = "guai_die_out";

    public const string GET_OBJ_NAME = "get_obj_name";

    public const string ZD_SKILL = "zd_skill";

    public const string GET_DIAOLUOWU = "get_diaoluowu";

    /// <summary>
    /// 血瓶
    /// </summary>
    public const string GET_XP = "get_xp";
    /// <summary>
    /// 加血
    /// </summary>
    public const string JIAXUE = "jiaxue";

    /// <summary>
    /// 是否有诅咒装备
    /// </summary>
    public const string GET_ZUZHOU = "get_zuzhou";

    public const string DIE_OUT = "die_out";

    public const string GAME_OVER = "game_over";
    public const string GAME_RESTART = "game_restart";
    public const string GAME_SAVEING = "game_saveing";

    public const string ADD_MAX_LIVE = "add_max_live";

    //boss出现 传入boss链接血条
    public const string BOSS_IS_OUT = "boss_is_out";
    public const string BOSS_IS_DIE = "boss_is_die";
    //摄像机边界块还原
    public const string CAMERA_KUAI_REDUCTION = "boss_is_reduction";
    //摄像机震动
    public const string CAMERA_SHOCK = "boss_shock";


    public const string GAME_LANGUAGE = "game_language";

    public const string BAG_OPEN = "bag_open";

    //点到徽章时 派发 徽章信息 来刷新 徽章面板信息
    public const string HZ_TOUCH = "hz_touch";

    public const string SHOW_UIBY_ALPHA = "show_uiby_alpha";



    //跟换主动技能徽章 的UI切换显示  参数是装备栏徽章的数组
    public const string SKILL_UI_CHANGE = "skill_ui_change";

    //释放技能
    public const string RELEASE_SKILL = "release_skill";

    public const string CHANGE_HUN = "change_hun";

    //魂值不够 提示
    public const string NO_HUN_PROMPT = "no_hun_prompt";

    public const string DIU_FEIDAO = "diu_feidao";

    public const string SHOU_FEIDAO = "shou_feidao";

    //回收飞刀 销毁
    public const string DISTORY_FEIDAO = "distory_feidao";


    //其他的 什么处罚机关
    public const string JG_OTHER_EVENT = "jg_other_event";


    //音量变化
    public const string AUDIO_VALUE = "audio_value";


    public const string GUAI_DIE = "guai_die";



    //剧情对话中 下一个 选中 id 事件
    public const string NEXT_ID = "next_id";

    public const string CHOSE_EVENT = "chose_event";
    public const string STATR_FIGHT = "startfight";
    public const string PLOT_KILL_PLAYER = "plot_kill_player";


    //切换跑步动作
    public const string CHANGE_RUN_AC = "change_run_ac";
    public const string CHANGE_RUN_AC_2 = "change_run_ac_2";

    //全部die 开门 检测  
    public const string ALLDIE_OPEN_DOOR = "alldie_open_door";



    //角色状态
    public const string PLAYER_ZT = "player_zt";



   

}