using UnityEngine;

public class CloudLoadButton : MonoBehaviour
{
    public void OnClick()
    {
        CloudManager.Instance.LoadFromGooglePlay();
    }
}