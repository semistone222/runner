using UnityEngine;

public class AchivementButton : MonoBehaviour
{
    public void OnClick()
    {
        CloudManager.Instance.ShowAchivementWindow();
    }
}