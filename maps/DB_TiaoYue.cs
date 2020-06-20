using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_TiaoYue : DBBase
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GlobalSetDate.instance.IsCMapHasCreated) {
            SetDBPos();
            SetLightColor();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDBPos()
    {
        GameObject maps = GlobalTools.FindObjByName("maps");
        string tiaoyuediban = "tiaoyueDBD_" + maps.GetComponent<GetReMap>().DibanType;
        List<string> tiaoyuedibanDArr = GetDateByName.GetInstance().GetListByName(tiaoyuediban, MapNames.GetInstance());
        GameObject dibanD = GlobalTools.GetGameObjectByName(tiaoyuedibanDArr[GlobalTools.GetRandomNum(tiaoyuedibanDArr.Count)]);
        float _x1 = tl.position.x + 5;
        float _x2 = rd.position.x - 5;

        float _x = _x1 + GlobalTools.GetRandomDistanceNums(Mathf.Abs(_x2-_x1));
        float _y = tl.position.y-4+ GlobalTools.GetRandomDistanceNums(8);

        dibanD.transform.position = new Vector3(_x, _y, 0);
        dibanD.transform.parent = maps.transform;
    }

}
