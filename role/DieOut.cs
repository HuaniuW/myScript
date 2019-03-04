using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOut : MonoBehaviour {

    public string type = "1";

    public string diaoluowu = "";
	// Use this for initialization
	void Start () {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.DIE_OUT, this.DieOutDo);
	}

    void DieOutDo(UEvent e)
    {
        if (!IsDie && this.GetComponent<RoleDate>().isDie) {
            IsDie = true;
            if (diaoluowu == "")return;
            int jv = Random.Range(0, 1000);
            int fx = this.transform.position.x > this.GetComponent<AIBase>().gameObj.transform.position.x ? 1 : -1;
            string[] diaoluowuArr = diaoluowu.Split('|');
            for (var i = 0;i< diaoluowuArr.Length; i++) {
                string objName = diaoluowuArr[i].Split('-')[0];
                //掉落几率
                int dljv = int.Parse(diaoluowuArr[i].Split('-')[1]);
                if (jv < dljv)
                {
                    GameObject o = GlobalTools.GetGameObjectByName(objName);
                    o.transform.position = this.transform.position;
                    o.GetComponent<Wupinlan>().GetXFX(Random.Range(100,300) * fx);
                }
            }



        }
        //掉落几率 掉落的等级 ==  掉落多个物体
        //掉落 血  蓝  物品
    }

    bool IsDie = false;

	// Update is called once per frame
	void Update () {
        //if (!IsDie && this.GetComponent<RoleDate>().isDie)
        //{
        //    IsDie = true;
        //    DieOutDo();
        //}
    }
}
