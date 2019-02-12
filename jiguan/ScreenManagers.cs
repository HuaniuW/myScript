using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManagers : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(BatchLoadingScenes(screenNameList));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<string> screenNameList;
    List<AsyncOperation> BatchAsynOperation;


    IEnumerator BatchLoadingScenes(List<string> namesOfScene)
    {
        BatchAsynOperation = new List<AsyncOperation>();

        for (int i = 0; i < namesOfScene.Count; i++)
        {
            AsyncOperation SceneLoading = SceneManager.LoadSceneAsync(namesOfScene[i]);
            //SceneManager.GetSceneByBuildIndex(0)
            //SceneManager.UnloadSceneAsync();
            //SceneManager.SetActiveScene
            //SceneManager.UnloadScene();
            //LoadSceneMode.Additive这个参数是保留场景 不销毁之前的场景  可以用于拼接地图
            //AsyncOperation SceneLoading = SceneManager.LoadSceneAsync(namesOfScene[i], LoadSceneMode.Additive);
            SceneLoading.allowSceneActivation = false;
            BatchAsynOperation.Add(SceneLoading);
            //print("screenname " + namesOfScene[i] + "  进度  " + BatchAsynOperation[i].progress + "   --  " + BatchAsynOperation[i].ToString());
            while (BatchAsynOperation[i].progress < 0.9f)
            {
                yield return null;
            }

        }
        
    }

    IEnumerator TTTT()
    {
        for (int i = 0; i < BatchAsynOperation.Count; i++)
        {
            BatchAsynOperation[i].allowSceneActivation = true;

            while (!BatchAsynOperation[i].isDone)
            {
                yield return null;
            }

            //yield return StartCoroutine(OnBatchSceneLoaded[i]);
        }
    }


    public void ToChangeScreenByName(string _name)
    {

        StartCoroutine(TTTT());

        print(SceneManager.sceneCount+" ?  "+ SceneManager.sceneCountInBuildSettings);
        AsyncOperation TempScene = null;
        string name;
        for (int i = 0; i < screenNameList.Count; i++)
        {
            if (screenNameList[i] != _name) {
                name = screenNameList[i];
                print("hi  "+name);
                SceneManager.UnloadSceneAsync(name);
            }
            else
            {
                name = screenNameList[i];
                print("hi2  " + name);
                TempScene = BatchAsynOperation[i];
            }
        }
        //if(TempScene!=null) TempScene.allowSceneActivation = true;
    }
}
