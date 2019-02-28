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
	
	// Update is called once per frame
	void Update () {
		
	}

    void GetSkill(UEvent e) {
        List<RectTransform> hzs = (List<RectTransform>)e.eventParams;
        if (_obj != null) GetSkillOut(_obj);
        for (var i=0;i<hzs.Count;i++)
        {
            if(i == this.boxNum - 1)
            {
                string jnName = "jn_" + hzs[i].GetComponent<HZDate>().objName;
                print("name  "+jnName);
                _obj = GlobalTools.GetGameObjectByName(jnName);
                GetSkillIn(_obj);
            }
        }
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
