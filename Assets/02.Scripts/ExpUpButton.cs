using UnityEngine;

public class ExpUpButton : MonoBehaviour
{
    public void OnClick()
    {
        CloudManager.Instance.ExpUp(1);
    }
}