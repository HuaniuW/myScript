﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDistoryObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //print("start");
        //GetDontDistory();
        //GetDontDistory();
        /*if (GlobalTools.FindObjByName(this.name) == null)
        {
            GetDontDistory();
        }
        else
        {
            //DistorySelf();
            Destroy(this);
        }*/
        //HideSelf();
        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_SCREEN, ChangeScreen);
    }

    private void Awake()
    {
        //print("awake");
      /*  if (GlobalTools.FindObjByName(this.name) == null)
        {
            GetDontDistory();
        }
        else
        {
            //DistorySelf();
            Destroy(this);
        }*/
    }

    private void OnDestroy()
    {
        //print("OnDestroy!!!!!!");
        //ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANGE_SCREEN, ChangeScreen);
    }

    //void ChangeScreen(UEvent uEvent)
    //{

    //}

    // Update is called once per frame
    void Update () {
	}

    public static bool isClone;
    void GetDontDistory()
    {
        DontDestroyOnLoad(this);
    }
    

    public void RemoveSelf()
    {
        isClone = false;
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    public void DistorySelf()
    {

    }
}
