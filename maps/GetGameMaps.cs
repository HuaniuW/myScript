using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetGameMaps : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!IsShowMaps)
            {
                print("打开 地图");
                IsShowMaps = true;
                ShowMaps();
            }
            else
            {
                print("关闭 地图");
                IsShowMaps = false;
                HideMaps();
            }
        }

    }


    bool IsShowMaps = false;

    bool IsCreateMapsUI = false;
    void ShowMaps()
    {
        if (!IsCreateMapsUI)
        {
            IsCreateMapsUI = true;
            ShengChengImgMap();
        }
        //显示我在哪
        WhereIsMe();
        _canva.GetComponent<CanvasGroup>().alpha = 1;
    }


    void WhereIsMe()
    {
        for(int i=0;i< imgMapObjArr.Count; i++)
        {
            if(imgMapObjArr[i].name == GlobalSetDate.instance.CReMapName)
            {
                imgMapObjArr[i].GetComponent<Image>().color = Color.blue;
                return;
            }
        }
    }


    GameObject _canva;
    void ShengChengImgMap()
    {

        _canva = GlobalTools.FindObjByName("MapUI");
        if (Globals.mapZBArr == null || Globals.mapZBArr.Count == 0) return;
        for (var i = 0; i < Globals.mapZBArr.Count; i++)
        {
            //print(i);
            //print(i+ " ----->   "+ mapZBArr[i]);
            string _name = Globals.mapZBArr[i].Split('!')[0];
            float _x = 200 + float.Parse(Globals.mapZBArr[i].Split('!')[1].Split('#')[0]) * 102;
            float _y = 200 + float.Parse(Globals.mapZBArr[i].Split('!')[1].Split('#')[1]) * 42;
            GameObject map = GetGKImgAndPosition(_name, _x, _y);

            string[] fzList = Globals.mapZBArr[i].Split('!')[2].Split('^');
            for (var j = 0; j < fzList.Length; j++)
            {
                if (IsHasImgObjInList(fzList[j].Split(':')[1])) continue;


                LianXianMaps(map, fzList[j].Split(':')[0]);
            }

        }
    }


    void LianXianMaps(GameObject currentMap, string fx)
    {
        GameObject xian = null;
        if (fx == "u")
        {
            xian = GlobalTools.GetGameObjectByName("map_xian2");
            xian.transform.position = new Vector2(currentMap.transform.position.x, currentMap.transform.position.y + currentMap.GetComponent<RectTransform>().rect.height * 0.5f + xian.GetComponent<RectTransform>().rect.height * 0.4f);

        }
        else if (fx == "d")
        {
            xian = GlobalTools.GetGameObjectByName("map_xian2");
            xian.transform.position = new Vector2(currentMap.transform.position.x, currentMap.transform.position.y - currentMap.GetComponent<RectTransform>().rect.height * 0.5f - xian.GetComponent<RectTransform>().rect.height * 0.4f);
        }
        else if (fx == "r")
        {
            xian = GlobalTools.GetGameObjectByName("map_xian1");
            xian.transform.position = new Vector2(currentMap.transform.position.x + currentMap.GetComponent<RectTransform>().rect.width * 0.5f + xian.GetComponent<RectTransform>().rect.width * 0.4f, currentMap.transform.position.y);
        }
        else if (fx == "l")
        {
            xian = GlobalTools.GetGameObjectByName("map_xian1");
            xian.transform.position = new Vector2(currentMap.transform.position.x - currentMap.GetComponent<RectTransform>().rect.width * 0.5f - xian.GetComponent<RectTransform>().rect.width * 0.4f, currentMap.transform.position.y);
        }

        print(xian);
        print(currentMap);

        xian.name = currentMap.name + "#" + fx;
        xian.transform.parent = _canva.transform;
        //xian.GetComponent<GKMapXian>().LianJieMap(currentMap.name, gkImg.name);
    }


    bool IsHasImgObjInList(string _mapName)
    {
        foreach (GameObject o in imgMapObjArr)
        {
            if (o.name == _mapName) return true;
        }
        return false;
    }


    List<GameObject> imgMapObjArr = new List<GameObject> { };
    GameObject GetGKImgAndPosition(string _name, float px = 0, float py = 0)
    {
        GameObject gkImg = GlobalTools.GetGameObjectByName("gkImg");
        gkImg.transform.parent = _canva.transform;
        gkImg.transform.position = new Vector2(px, py);

        gkImg.name = _name;

        string _zb = GetCurrentMapZBByName(_name);
        if (GlobalMapDate.IsSpeMapByName(_name))
        {
            gkImg.GetComponent<gkImgTextTest>().GetText("<color=#ff1111>speMap</color>   " + _zb);
        }
        else
        {
            gkImg.GetComponent<gkImgTextTest>().GetText("<color=#000000>speMap</color>   " + _zb);
        }




        imgMapObjArr.Add(gkImg);

        return gkImg;
    }

    //通过当前地图的名字获取当前地图的坐标
    string GetCurrentMapZBByName(string cZXMapName)
    {
        string zb = "";
        //print(" ????????????????cZXMapName " + cZXMapName);
        for (int i = 0; i < Globals.mapZBArr.Count; i++)
        {
            //print(i + "  ?? " + Globals.mapZBArr[i]);
            if (Globals.mapZBArr[i].Split('!')[0] == cZXMapName)
            {
                zb = Globals.mapZBArr[i].Split('!')[1];
                return zb;
            }
        }
        return zb;
    }






    void HideMaps()
    {
        //if(_canva.activeSelf) _canva.SetActive(false);
        _canva.GetComponent<CanvasGroup>().alpha = 0;
    }
}
