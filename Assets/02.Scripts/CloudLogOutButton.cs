using UnityEngine;

public class CloudLogOutButton : MonoBehaviour
{
    public void OnClick()
    {
        CloudManager.Instance.SignOut();
    }
}