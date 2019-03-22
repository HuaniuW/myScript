using System.Collections;

public class EventTypeName
{
    public const string CUBE_CLICK = "cube_click";
    public const string OPEN_DOOR = "open_door";
    //切换场景
    public const string CHANGE_SCREEN = "change_screen";
    //获取敌人
    public const string GET_ENEMY = "GET_ENEMY";

    public const string CLOSE_DOOR = "close_door";
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
}