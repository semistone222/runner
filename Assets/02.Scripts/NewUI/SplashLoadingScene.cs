using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashLoadingScene : MonoBehaviour {

	public Slider slider;
	public Text LoadingText;
    public string targetSceneString;

	bool IsDone = false;
	float fTime = 0f;
	AsyncOperation async_operation;

	void Start()
	{
		StartCoroutine(StartLoad(targetSceneString));
	}

	void Update()
	{
		fTime += Time.deltaTime;
		slider.value = fTime * 0.4f;
		//Debug.Log (fTime);
		LoadingText.text = 	"Loading ( "+((int)(slider.value * 100)).ToString() +"/ 100 )";

		if (fTime >= 3)
		{
			async_operation.allowSceneActivation = true;
		}
	}

	public IEnumerator StartLoad(string strSceneName)
	{
        async_operation = SceneManager.LoadSceneAsync(strSceneName);
		async_operation.allowSceneActivation = false;

		if (IsDone == false)
		{
			IsDone = true;

			while (async_operation.progress < 0.9f)
			{
				Debug.Log (async_operation.progress);
				slider.value = async_operation.progress;

				yield return new WaitForSeconds(1.0f);
				//yield return true;
			}
		}
	}
}
