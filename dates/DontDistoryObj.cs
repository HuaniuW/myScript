using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDistoryObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //print("start");
        GetDontDistory();
        //HideSelf();
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_SCREEN, ChangeScreen);
    }

    private void Awake()
    {
        //print("awake");
    }

    void ChangeScreen(UEvent uEvent)
    {

    }

    // Update is called once per frame
    void Update () {
	}

    public static bool isClone;
    void GetDontDistory()
    {
        DontDestroyOnLoad(this);
    }

    public void HideSelf() {
        //this.gameObject.SetActive(false);
        //this.gameObject.renderer.enabled = false;
        //this.gameObject.GetComponent<Renderer>().enabled = false;
        //this.gameObject.transform.position = new Vector3(0, -1000, 0);
    }

    public void ShowSelf()
    {
        this.gameObject.SetActive(true);
        //this.gameObject.GetComponent<Renderer>().enabled = true;
    }

    public void RemoveSelf()
    {
        isClone = false;
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
