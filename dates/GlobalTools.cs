using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTools : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static GameObject FindObjByName(string _name)
    {
        GameObject obj = GameObject.Find("/" + _name) as GameObject;
        if (obj == null) obj = GameObject.Find("/" + _name + "(Clone)") as GameObject;
        return obj;
    }

    public static GameObject GetGameObjectByName(string ObjName)
    {
        GameObject obj = Resources.Load(ObjName) as GameObject;
        obj = Instantiate(obj);
        return obj;
    }
}
