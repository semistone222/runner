using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip BGM1;    //Lobby BGM
    public AudioClip BGM2;    //Play BGM

    public AudioSource CharChoiSE;
    public AudioSource ClickSound2;
    public AudioSource ClickSound3;
    public AudioSource UseGemSE;
    public AudioSource BuyCoinSE;
    public AudioSource StageSelectSE;
    public AudioSource ItemSelectSE;
    public AudioSource ItemDelectSE;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayCharChoiSE()
    {
        CharChoiSE.Play();
    }

    public void PlayClickSound2()
    {
        ClickSound2.Play();
    }

    public void PlayClickSound3()
    {
        ClickSound3.Play();
    }

    public void PlayUseGemSE()
    {
        UseGemSE.Play();
    }

    public void PlayBuyCoinSE()
    {
        BuyCoinSE.Play();
    }

    public void PlayStageSelectSE()
    {
        StageSelectSE.Play();
    }

    public void PlayItemSelectSE()
    {
        ItemSelectSE.Play();
    }

    public void PlayItemDelectSE()
    {
        ItemDelectSE.Play();
    }

    public void SetBGMNull()
    {
        //AudioSource bgmSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();

        //bgmSource.clip = null;
    }

    public void SetBGM1()
    {
        //AudioSource bgmSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();

        //bgmSource.clip = BGM1;

        //bgmSource.Play();
    }

    public void SetBGM2()
    {
        //AudioSource bgmSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();

        //bgmSource.clip = BGM2;

        //bgmSource.Play();
    }
}