using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_HuanshuDBBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetStart()
    {
        if (JinZhongBeijings1)
        {
            GetObjListNameList(JinZhongBeijings1, _JinZhongBeijings1);

            //这里排列 不需要 铺开 就随机排列

        }
    }


    [Header("左上点")]
    public Transform tl;

    [Header("右下点")]
    public Transform rd;


    //获取 景 名字数组
    public List<string> GetObjListNameList(Transform ParentObj, List<string> NameList)
    {
        NameList.Clear();
        if (ParentObj == null || ParentObj.childCount == 0) return NameList;
        foreach (Transform child in ParentObj)
        {
            string objName = child.gameObject.name;
            print("景是什么 ？ " + objName);
            NameList.Add(objName);
        }
        return NameList;

    }


    [Header("近 中远 背景1")]
    public Transform JinZhongBeijings1;
    protected List<string> _JinZhongBeijings1 = new List<string>();


    [Header("远 背景1")]
    public Transform YuanBeijings1;
    protected List<string> _YuanBeijings1 = new List<string>();


    [Header("大远 背景1")]
    public Transform DaYuanBeijings1;
    protected List<string> _DaYuanBeijings1 = new List<string>();



    [Header("近 背景1")]
    public Transform JinBeijings;
    protected List<string> _JinBeijings = new List<string>();

    [Header("近 背景2")]
    public Transform JinBeijings3;
    protected List<string> _JinBeijings3 = new List<string>();

    [Header("近前景1")]
    public Transform JinQianjings1;
    protected List<string> _JinQianjings1 = new List<string>();


    [Header("大远 前景1")]
    public Transform DaYuanQianjings1;
    protected List<string> _DaYuanQianjings1 = new List<string>();


}
