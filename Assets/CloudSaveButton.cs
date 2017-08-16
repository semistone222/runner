using UnityEngine;

public class CloudSaveButton : MonoBehaviour
{
    public void OnClick()
    {
        CloudManager.Instance.SaveToGooglePlay();
    }
}