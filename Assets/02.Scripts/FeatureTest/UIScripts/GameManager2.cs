using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager2 : MonoBehaviour
{
    public GameObject Room_0; //Main Room
    public GameObject Room_1; //Sub Room
    public GameObject[] Cats;
    public Sprite[] CatSprites; //고양이 스프라이트들(임시)
    public Canvas uiConfig;
    public Canvas uiShop;
    public Canvas uiInventory;
    public Canvas uiStatus;
    public Canvas uiNote;
    public GameObject buyBox;

    private int current_Room = 0;
    private bool isUiOn = false;
    private AudioSource clickSound;

    void Awake()
    {
        clickSound = GetComponent<AudioSource>();

        uiConfig.enabled = false;
        uiShop.enabled = false;
        uiInventory.enabled = false;
        uiNote.enabled = false;

        current_Room = 0;
        isUiOn = false;

    }

    void Update()
    {
        if ( (uiConfig.enabled | uiShop.enabled | uiNote.enabled) == false)
        {
            isUiOn = false;
        }
        else
        {
            isUiOn = true;
        }

        if (isUiOn)
        {
            Cats[0].GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            Cats[0].GetComponent<BoxCollider2D>().enabled    = true;
        }
    }

    public void btnRoomChange()
    {
        clickSound.Play();
        RoomChange();
    }
    public void btnConfig()
    {
        clickSound.Play();
        uiConfig.enabled = !uiConfig.enabled;
        uiNote.enabled = false;
        uiShop.enabled = false;
        uiStatus.enabled = false;
        uiInventory.enabled = false;

    }
    public void btnNote()
    {
        clickSound.Play();
        uiNote.enabled = !uiNote.enabled;
        uiConfig.enabled = false;
        uiShop.enabled = false;
        uiStatus.enabled = false;
        uiInventory.enabled = false;

    }
    public void btnShop()
    {
        clickSound.Play();
        uiShop.enabled = !uiShop.enabled;
        if(!uiShop.enabled)
        {
            buyBox.SetActive(false);
        }
        uiNote.enabled = false;
        uiConfig.enabled = false;
        uiStatus.enabled = false;
        uiInventory.enabled = false;
        uiShop.GetComponent<AudioSource>().Play();
    }
    public void btnInventory()
    {
        clickSound.Play();
        uiInventory.enabled = !uiInventory.enabled;
        uiStatus.enabled = uiInventory.enabled;
        uiNote.enabled = false;
        uiShop.enabled = false;
        uiConfig.enabled = false;
    }
    public void clickGameBtn()
    {
        clickSound.Play();
        SceneManager.LoadScene("SelectGame");
    }

    void RoomChange()
    {
        switch (current_Room)
        {
            case 0: // main room
                {
                    Room_0.transform.localPosition
                        = new Vector2(-720, 0);
                    Room_1.transform.localPosition
                        = new Vector2(0, 0);
                    current_Room = 1;
                    break;
                }

            case 1: // sub room
                {
                    Room_0.transform.localPosition
                        = new Vector2(0, 0);
                    Room_1.transform.localPosition
                        = new Vector2(-720, 0);
                    current_Room = 0;
                    break;
                }
            default:
                {
                    break;
                }

        }

    }


    /*
    public void OnClick()
    {
        isGameMenu = !isGameMenu;
        StartCoroutine(AnimateGameMenu(isGameMenu));
    }
    */
    /*
    //Slide Menu Coroutine 
    IEnumerator AnimateGameMenu(bool iGM)
    {
        const int transValue = 64;
        const int rangeValue = 5;

        Vector2 btnGT_Pos; //캣타워 버튼의 위치를 담을 변수
        Vector2 btnGF_Pos; //냥파이브 ...
        Vector2 btnGS_Pos; //까꿍냥...

        //수정 예정사항 : bool에 따라 +-만 바뀌면 되는데
        //너무 비효율적인 코드를 사용중 
        if (iGM)
        {
            foreach (int i in Enumerable.Range(0, rangeValue))
            {
                btnGT_Pos = Button_GameTower.transform.localPosition;
                btnGF_Pos = Button_GameFive.transform.localPosition;
                btnGS_Pos = Button_GameStealth.transform.localPosition;

                Button_GameTower.transform.localPosition
                    = new Vector2(btnGT_Pos.x, btnGT_Pos.y + transValue);
                Button_GameFive.transform.localPosition
                    = new Vector2(btnGF_Pos.x, btnGF_Pos.y + transValue);
                Button_GameStealth.transform.localPosition
                    = new Vector2(btnGS_Pos.x, btnGS_Pos.y + transValue);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            foreach (int i in Enumerable.Range(0, rangeValue))
            {
                btnGT_Pos = Button_GameTower.transform.localPosition;
                btnGF_Pos = Button_GameFive.transform.localPosition;
                btnGS_Pos = Button_GameStealth.transform.localPosition;

                Button_GameTower.transform.localPosition
                    = new Vector2(btnGT_Pos.x, btnGT_Pos.y - transValue);
                Button_GameFive.transform.localPosition
                    = new Vector2(btnGF_Pos.x, btnGF_Pos.y - transValue);
                Button_GameStealth.transform.localPosition
                    = new Vector2(btnGS_Pos.x, btnGS_Pos.y - transValue);
                yield return new WaitForSeconds(0.01f);
            }
        }

    }
    */

}
