using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    public Transform playerModel;
    public Transform playerCamera;

    private void Update()
    {
        playerModel.transform.forward = playerCamera.transform.forward;
    }
}
