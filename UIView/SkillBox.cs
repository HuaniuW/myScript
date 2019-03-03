using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SkillBox : CanTouchBox
{
    public int boxNum;
    public RectTransform rongqi;
    public RectTransform zz;
    public Text _text;
    GameObject _obj;
    // Use this for initialization
    void Start () {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.ZD_SKILL,this.GetSkill);
        if(zz!=null)ZZText(0);
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.ZD_SKILL, this.GetSkill);
    }


    //Vector3 imgReduceScale = new Vector3(0.95f, 0.95f, 1);   //设置图片缩放
    //Vector3 imgNormalScale = new Vector3(1, 1, 1);   //正常大小
    //当鼠标按下时调用 接口对应  IPointerDownHandler
    override public void OnPointerDown(PointerEventData eventData)
    {
        if (skill != null && !skill.GetComponent<HZDate>().IsCDOver()) return;
        IRole _role = this.transform.parent.GetComponent<XueTiao>().gameObj.GetComponent<GameBody>();
        //_role.ShowSkill(skill.GetComponent<HZDate>().TXName);
        if (boxNum == 1)
        {
            _role.GetSkill1();
        }
        else {
            _role.GetSkill2();
        }
        this.GetComponent<RectTransform>().localScale = imgReduceScale;
    }

    //当鼠标抬起时调用  对应接口  IPointerUpHandler
    override public void OnPointerUp(PointerEventData eventData)
    {
        //print("鼠标抬起！！！！！");
        //if (!skill.GetComponent<HZDate>().IsCDOver()) return;
        this.GetComponent<RectTransform>().localScale = imgNormalScale;
    }



    // Update is called once per frame
    void Update () {
        if(skill!=null&& skill.GetComponent<HZDate>().GetCdNums() >= 0)
        {
            ZZText(skill.GetComponent<HZDate>().GetCdNums());
        }
	}

    public void ZZText(int cdNums = 0)
    {
        int _alpha = cdNums>0 ? 1 : 0;
        bool isShow = cdNums > 0 ? true : false;
        zz.GetComponent<CanvasGroup>().alpha = _alpha;
        zz.GetComponent<CanvasGroup>().interactable = isShow; 
        zz.GetComponent<CanvasGroup>().blocksRaycasts = isShow;
        _text.gameObject.SetActive(isShow);
        if(cdNums>=0) _text.text = cdNums + "";
    }

    public RectTransform skill;

    void GetSkill(UEvent e) {
        List<RectTransform> hzs = (List<RectTransform>)e.eventParams;
        if (_obj != null) GetSkillOut(_obj);
        skill = null;
        for (var i=0;i<hzs.Count;i++)
        {
            if(i == this.boxNum - 1)
            {
                if (hzs[i] == null) continue;
                skill = hzs[i];
                string jnName = "jn_" + hzs[i].GetComponent<HZDate>().objName;
                //if (Globals.isDebug) print("name  "+jnName+"    "+ hzs[i].GetComponent<HZDate>()._cd);
                _obj = GlobalTools.GetGameObjectByName(jnName);
                GetSkillIn(_obj);
            }
        }
    }


    public HZDate GetSkillHZDate()
    {
        if (skill != null) return skill.GetComponent<HZDate>();
        return null;
    }

    void GetSkillIn(GameObject obj)
    {
        //obj.transform.parent = this.transform.parent;
        obj.transform.parent = this.rongqi.transform;
        //obj.transform.position = Vector2.zero;
        obj.transform.position = this.transform.position;
    }

    void GetSkillOut(GameObject obj)
    {
        DestroyImmediate(obj, true);
    }
}
