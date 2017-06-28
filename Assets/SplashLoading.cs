using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashLoading : MonoBehaviour {

	public Slider slider;
	public Text LoadingText; 

	bool IsDone = false;
	float fTime = 0f;
	AsyncOperation async_operation;

	void Start()
	{
		StartCoroutine(StartLoad("SelectMode"));
	}

	void Update()
	{
		fTime += Time.deltaTime;
		slider.value = fTime;
		LoadingText.text = 	"Loading ( "+((int)(slider.value * 100)).ToString() +"/ 100 )";

		if (fTime >= 3)
		{
			async_operation.allowSceneActivation = true;
		}
	}

	public IEnumerator StartLoad(string strSceneName)
	{
		async_operation = Application.LoadLevelAsync(strSceneName);
		async_operation.allowSceneActivation = false;

		if (IsDone == false)
		{
			IsDone = true;

			while (async_operation.progress < 0.9f)
			{
				slider.value = async_operation.progress;

				yield return true;
			}
		}
	}
}
