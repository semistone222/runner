using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class CloudManager : MonoBehaviour
{
    private bool _startedToLoadAvatar = false;
    
    private uint level, exp;
    private Text nameText, levelText, expText, infoText;
    
    private static CloudManager m_instance;

    public static CloudManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<CloudManager>();

                if (m_instance == null)
                {
                    m_instance = new GameObject("CloudManager").AddComponent<CloudManager>();
                }
            }

            return m_instance;
        }
    }

    public bool IsAuthorized
    {
        get
        {
            return UM_GameServiceManager.Instance.ConnectionSate == UM_ConnectionState.CONNECTED;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        UserAuthoricate();
        
        UM_GameServiceManager.OnPlayerConnected += OnPlayerConnected;
        UM_GameServiceManager.OnPlayerDisconnected += OnPlayerDisconnected;

        nameText = GameObject.Find("Google ID Text").GetComponent<Text>();
        levelText = GameObject.Find("Google Level Text").GetComponent<Text>();
        expText = GameObject.Find("Google Exp Text").GetComponent<Text>();
        infoText = GameObject.Find("DebugInfo Text").GetComponent<Text>();
    }

    public void UserAuthoricate()
    {
        UM_GameServiceManager.Instance.Connect();

        if (IsAuthorized)
        {
            OnPlayerConnected();
        }
    }

    private void OnConnectionFailed(GooglePlayConnectionResult result)
    {
        if (result.IsSuccess)
        {
            Debug.Log("Connection Success!");
        }
        else
        {
            Debug.LogError("Connection Failed, re-initialization begining...");
        }
    }

    private void OnPlayerConnected()
    {
        //Debug.Log("OnPlayerConnected() - Player Connected");

        //if (UM_GameServiceManager.Instance.Player != null)
        //{
        //    Debug.Log("PlayerId : " + UM_GameServiceManager.Instance.Player.PlayerId);
        //    Debug.Log("Name : " + UM_GameServiceManager.Instance.Player.Name);

        //    if (!_startedToLoadAvatar)
        //    {
        //        _startedToLoadAvatar = true;
        //        UM_GameServiceManager.Instance.Player.LoadSmallPhoto();
        //    }
        //}

        infoText.text = "Connected!";
        nameText.text = UM_GameServiceManager.Instance.Player.Name;

        LoadFromGooglePlay();
    }

    private void OnPlayerDisconnected()
    {
        infoText.text = "Disconnected!";
    }

    public void ShowAchivementWindow()
    {
        if (!IsAuthorized)
        {
            UserAuthoricate();
            return;
        }

        UM_GameServiceManager.Instance.ShowAchievementsUI();
    }

    public void ShowLeaderBoardWindow()
    {
        if (!IsAuthorized)
        {
            UserAuthoricate();
            return;
        }

        UM_GameServiceManager.Instance.ShowLeaderBoardsUI();
    }
    
    public void LoadFromGooglePlay()
    {
        StartCoroutine("LoadParallelLoop");
    }

    private IEnumerator LoadParallelLoop()
    {
        if (!IsAuthorized)
        {
            infoText.text = "Load Fail!";
            UserAuthoricate();
        }
        else
        {
            levelText.text = UM_Storage.GetString("Level");
            expText.text = UM_Storage.GetString("Exp");

            level = Convert.ToUInt32(levelText.text);
            exp = Convert.ToUInt32(expText.text);

            infoText.text = "Load Complete!";
        }

        yield return null;
    }

    public void SaveToGooglePlay()
    {
        StartCoroutine("SaveParallelLoop");
    }

    private IEnumerator SaveParallelLoop()
    {
        if (!IsAuthorized)
        {
            infoText.text = "Save Fail!";
            UserAuthoricate();
        }
        else
        {
            UM_Storage.Save("Level", level.ToString());
            UM_Storage.Save("Exp", exp.ToString());

            infoText.text = "Save Complete!";
        }

        yield return null;
    }

    public void LevelUp(uint value)
    {
        string tmpStr = "level - B : " + level.ToString();

        level += value;
        levelText.text = level.ToString();

        tmpStr += ", A : " + level.ToString();

        infoText.text = tmpStr;
    }

    public void ExpUp(uint value)
    {
        string tmpStr = "exp - B : " + exp.ToString();

        exp += value;
        expText.text = exp.ToString();

        tmpStr += ", A : " + exp.ToString();

        infoText.text = tmpStr;
    }
}