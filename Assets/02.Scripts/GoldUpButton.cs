using UnityEngine;

public class GoldUpButton : MonoBehaviour
{
    public void OnClick()
    {
        CloudManager.Instance.GoldUp(1);
    }
}