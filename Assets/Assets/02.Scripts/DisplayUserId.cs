using UnityEngine;
using UnityEngine.UI;

public class DisplayUserId : MonoBehaviour {
    public Text userId;
    private PhotonView pv = null;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        userId.text = pv.owner.NickName;
    }
}
