using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{

    SoundManager _instSoundM;

    bool IsDone = false;
    float LoadingTime = 0.0f;

    AsyncOperation asyn_operation;

    // Use this for initialization
    void Start()
    {

        _instSoundM = new SoundManager();
        _instSoundM.SetBGMNull();
        Debug.Log("SceneName = " + SendSence.SceneName);
        StartCoroutine(StartLoad(SendSence.SceneName));
    }

    // Update is called once per frame
    void Update()
    {
        LoadingTime += Time.deltaTime;
        //	asyn_operation.allowSceneActivation = true;
        if (LoadingTime >= 2)
        {
            asyn_operation.allowSceneActivation = true;
        }
    }

    public IEnumerator StartLoad(string StartSceneName)
    {
        asyn_operation = Application.LoadLevelAsync(StartSceneName);
        asyn_operation.allowSceneActivation = false;

        if (IsDone == false)
        {
            IsDone = true;
            while (asyn_operation.progress < 0.9f)
            {

                yield return true;
            }
        }
    }
    
}
