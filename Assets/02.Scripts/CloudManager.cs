using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class CloudManager : MonoBehaviour
{
    [Serializable]
    struct DataContainer
    {
        public int gold, level, exp;
    }

    DataContainer dataContainer;

    private Text nameText, goldText, levelText, expText, infoText;

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

    // 안드로이드 빌더 초기화
    private Action<bool> signInCallback;    // 로그인 성공 여부 확인을 위한 Callback 함수

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .Build();

        PlayGamesPlatform.InitializeInstance(config);

        // 구글 플레이 로그를 확인할려면 활성화
        PlayGamesPlatform.DebugLogEnabled = true;

        // 구글 플레이 활성화
        PlayGamesPlatform.Activate();

        // Callback 함수 정의
        signInCallback = (bool success) =>
        {
            if (success)
            {
                nameText.text = "Name : " + PlayGamesPlatform.Instance.GetUserDisplayName();
                infoText.text = "Info : " + PlayGamesPlatform.Instance.GetUserDisplayName() + " SignIn Success!";
            }
            else
                infoText.text = "Info : SignIn Fail!";
        };

        dataContainer = new DataContainer();

        // 정보 출력을 위한 GetComponent
        nameText = GameObject.Find("Google ID Text").GetComponent<Text>();
        goldText = GameObject.Find("Google Gold Text").GetComponent<Text>();
        levelText = GameObject.Find("Google Level Text").GetComponent<Text>();
        expText = GameObject.Find("Google Exp Text").GetComponent<Text>();
        infoText = GameObject.Find("DebugInfo Text").GetComponent<Text>();
    }

    // 로그인
    public void SignIn()
    {
        // 로그아웃 상태면 호출
        if (!IsAuthorized)
            PlayGamesPlatform.Instance.Authenticate(signInCallback);
    }

    // 로그아웃
    public void SignOut()
    {
        // 로그인 상태면 호출
        if (IsAuthorized)
        {
            infoText.text = "Info : " + PlayGamesPlatform.Instance.GetUserDisplayName() + " Sign Out!";
            PlayGamesPlatform.Instance.SignOut();
        }
    }

    public bool IsAuthorized
    {
        get
        {
            return PlayGamesPlatform.Instance.IsAuthenticated();
        }
    }

    public void ShowAchivementWindow()
    {
        if (!IsAuthorized)
        {
            SignIn();
            return;
        }

        Social.ShowAchievementsUI();
    }

    public void ShowLeaderBoardWindow()
    {
        if (!IsAuthorized)
        {
            SignIn();
            return;
        }

        Social.ShowLeaderboardUI();
    }

    public void GoldUp(int value)
    {
        dataContainer.gold += value;
        UpdateGoldInfo();
    }

    void UpdateGoldInfo()
    {
        goldText.text = "Gold : " + dataContainer.gold.ToString();
        infoText.text = "Info : gold - " + dataContainer.gold.ToString();
    }

    public void LevelUp(int value)
    {
        dataContainer.level += value;
        UpdateLevelInfo();
    }

    void UpdateLevelInfo()
    {
        levelText.text = "Level : " + dataContainer.level.ToString();
        infoText.text = "Info : level - " + dataContainer.level.ToString();
    }

    public void ExpUp(int value)
    {
        dataContainer.exp += value;
        UpdateExpInfo();
    }

    void UpdateExpInfo()
    {
        expText.text = "Exp : " + dataContainer.exp.ToString();
        infoText.text = "Info : exp - " + dataContainer.exp.ToString();
    }

    // 게임 저장은 다음과 같이 합니다.
    public void SaveToCloud()
    {
        if (!IsAuthorized) // 로그인 되지 않았으면
        {
            // 로그인 루틴을 진행하던지 합니다.
            SignIn();
            return;
        }

        // 파일 이름에 적당히 사용하실 파일 이름을 지정해줍니다.
        infoText.text = "Info : Data Saving...";
        StartCoroutine(OpenSavedGame("RunnerCloudData", true));
    }

    IEnumerator OpenSavedGame(string filename, bool bSave)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if (bSave)
            savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToSave); // 저장 루틴 진행
        else
            savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToRead); // 로딩 루틴 진행

        yield return null;
    }

    // savedGameClient.OpenWithAutomaticConflictResolution 호출 시 아래 함수를 콜백으로 지정했습니다. 준비된 경우 자동으로 호출 될겁니다.
    void OnSavedGameOpenedToSave(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.

            // 파일이 준비되었습니다. 실제 게임 저장을 수행합니다.

            // 저장할 데이터 바이트 배열에 저장하실 데이터의 바이트 배열을 지정합니다.
            SaveGame(game, DataSerialize(dataContainer), DateTime.Now.TimeOfDay);
        }
        else
        {
            // 파일 열기에 실패 했습니다. 오류 메시지를 출력하든지 합니다.
            infoText.text = "Info : OnSavedGameOpenedToSave - Save Game Fail!";
        }
    }

    void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

        builder = builder
        .WithUpdatedPlayedTime(totalPlaytime)
        .WithUpdatedDescription("Saved game at " + DateTime.Now);

        /*
        if (savedImage != null)
        {
            // This assumes that savedImage is an instance of Texture2D
            // and that you have already called a function equivalent to
            // getScreenshot() to set savedImage

            // NOTE: see sample definition of getScreenshot() method below

            byte[] pngData = savedImage.EncodeToPNG();
            builder = builder.WithUpdatedPngCoverImage(pngData);
        }
		*/

        SavedGameMetadataUpdate updatedMetadata = builder.Build();

        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // 데이터 저장이 완료되었습니다.
            infoText.text = "Info : Save Data Finish!";
        }
        else
        {
            // 데이터 저장에 실패했습니다.
            infoText.text = "Info : OnSavedGameWritten - Save Cloud Data Fail!";
        }
    }

    // ----------------------------------------------------------------------------------------------------------------

    // 클라우드로부터 파일 읽기
    public void LoadFromCloud()
    {
        if (!IsAuthorized)
        {
            // 로그인되지 않았으니 로그인 루틴을 진행하던지 합니다.
            SignIn();
            return;
        }

        // 내가 사용할 파일이름을 지정해줍니다. 그냥 컴퓨터상의 파일과 똑같다 생각하시면됩니다.
        infoText.text = "Info : Data Loading...";
        StartCoroutine(OpenSavedGame("RunnerCloudData", false));
    }

    void OnSavedGameOpenedToRead(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            LoadGameData(game);
        }
        else
        {
            // 파일 열기에 실패한 경우, 오류 메시지를 출력하던지 합니다.
            infoText.text = "Info : Load Data Fail!";
        }
    }

    // 데이터 읽기를 시도합니다.
    void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle processing the byte array data

            // 데이터 읽기에 성공했습니다.

            // data 배열을 복구해서 적절하게 사용하시면 됩니다.
            dataContainer = (DataContainer)DataDeserialize(data);

            goldText.text = "Gold : " + dataContainer.gold.ToString();
            levelText.text = "Level : " + dataContainer.level.ToString();
            expText.text = "Exp : " + dataContainer.exp.ToString();

            infoText.text = "Info : Load Data Finish!";
        }
        else
        {
            // 읽기에 실패 했습니다. 오류 메시지를 출력하던지 합니다.
            infoText.text = "Info : OnSavedGameDataRead - Save Data Fail!";
        }
    }

    // 데이터를 직렬화하는 함수
    public byte[] DataSerialize(object data)
    {
        BinaryFormatter binFormmater = new BinaryFormatter();
        MemoryStream mStream = new MemoryStream();

        binFormmater.Serialize(mStream, data);

        return mStream.ToArray();
    }

    // 데이터를 역직렬화하는 함수
    public object DataDeserialize(byte[] buffer)
    {
        BinaryFormatter binFormatter = new BinaryFormatter();
        MemoryStream mStream = new MemoryStream();

        mStream.Write(buffer, 0, buffer.Length);
        mStream.Position = 0;

        return binFormatter.Deserialize(mStream);
    }
}