using UnityEngine;

public class LevelUpButton : MonoBehaviour
{
    public void OnClick()
    {
        CloudManager.Instance.LevelUp(1);
    }
}