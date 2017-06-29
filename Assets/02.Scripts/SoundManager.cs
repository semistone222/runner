using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {


	public AudioSource CharChoiSE;
	public AudioSource ClickSound2;
	public AudioSource ClickSound3;
	public AudioSource UseGemSE;
	public AudioSource BuyCoinSE;
	public AudioSource StageSelectSE;
	public AudioSource ItemSelectSE;
	public AudioSource ItemDelectSE;

	void Start(){
		DontDestroyOnLoad (this.gameObject);
	}

	public void PlayCharChoiSE(){
		CharChoiSE.Play ();
	}

	public void PlayClickSound2(){
		ClickSound2.Play ();
	}

	public void PlayClickSound3(){
		ClickSound3.Play ();
	}

	public void PlayUseGemSE(){
		UseGemSE.Play ();
	}

	public void PlayBuyCoinSE(){
		BuyCoinSE.Play ();
	}

	public void PlayStageSelectSE(){
		StageSelectSE.Play ();
	}

	public void PlayItemSelectSE(){
		ItemSelectSE.Play ();
	}

	public void PlayItemDelectSE(){
		ItemDelectSE.Play ();
	}
}
