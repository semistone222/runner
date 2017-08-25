using UnityEngine;

public class LeaderBoardButton : MonoBehaviour
{
    public void OnClick()
    {
        CloudManager.Instance.ShowLeaderBoardWindow();
    }
}