using UnityEngine;

public class CloudLogInButton : MonoBehaviour
{
    public void OnClick()
    {
        CloudManager.Instance.SignIn();
    }
}