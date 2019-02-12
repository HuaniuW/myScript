using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Yujiazai : MonoBehaviour {
    //预加载场景
    // Use this for initialization
    
    [Header("位置")]
    public string PlayerPosition = "";
    [Header("去的场景名字")]
    public string GoScreenName = "";
	void Start () {
        //print("GoScreenName>" + GoScreenName);
        //StartCoroutine(loadScence(GoScreenName));
    }

    AsyncOperation async;
    IEnumerator loadScence(string sceneName)
    {
        print(sceneName);
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        yield return null;
    }

    void SetPlayerPositionAndScreen()
    {
        GlobalSetDate.instance.playerPosition = PlayerPosition;
        GlobalSetDate.instance.screenName = GoScreenName;
        GlobalSetDate.instance.IsChangeScreening = true;
    }

    public void needToChange()
    {
        //SceneManager.UnloadSceneAsync(0);
        SetPlayerPositionAndScreen();
        async.allowSceneActivation = true;
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(GoScreenName));
        //SceneManager.sceneUnloaded();
        //print(async.allowSceneActivation+"   ???  " +async.isDone);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.tag == "Player")
        {
            //print(Coll.tag + "  --  " + Coll.transform.tag+"   hitname "+this.GoScreenName);
            //needToChange();
            GlobalTools.FindObjByName("screenManager").GetComponent<ScreenManagers>().ToChangeScreenByName(GoScreenName);
        }
    }
}
