using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBox : MonoBehaviour {
    public int boxNum;
    GameObject _obj;
    // Use this for initialization
    void Start () {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.ZD_SKILL,this.GetSkill);
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.ZD_SKILL, this.GetSkill);
    }

    // Update is called once per frame
    void Update () {
		
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
        obj.transform.parent = this.transform.parent;
        obj.transform.position = this.transform.position;
    }

    void GetSkillOut(GameObject obj)
    {
        DestroyImmediate(obj, true);
    }
}
