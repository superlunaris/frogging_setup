using UnityEngine;

public class TestTeleport : MonoBehaviour
{
    public KeyCode teleport0 = KeyCode.Alpha0;
    public KeyCode teleport1 = KeyCode.Alpha1;
    public KeyCode teleport2 = KeyCode.Alpha2;
    public KeyCode teleport3 = KeyCode.Alpha3;
    public KeyCode teleport4 = KeyCode.Alpha4;
    public KeyCode teleport5 = KeyCode.Alpha5;
    public KeyCode teleport6 = KeyCode.Alpha6;
    public KeyCode teleport7 = KeyCode.Alpha7;

    public Transform teleportPosition0;
    public Transform teleportPosition1;
    public Transform teleportPosition2;
    public Transform teleportPosition3;
    public Transform teleportPosition4;
    public Transform teleportPosition5;
    public Transform teleportPosition6;
    public Transform teleportPosition7;

    private void Update()
    {
        if (Input.GetKeyDown(teleport0))
        {
            gameObject.transform.position = teleportPosition0.position;
        }
        else if (Input.GetKeyDown(teleport1))
        {
            gameObject.transform.position = teleportPosition1.position;
        }
        else if (Input.GetKeyDown(teleport2))
        {
            gameObject.transform.position = teleportPosition2.position;
        }
        else if (Input.GetKeyDown(teleport3))
        {
            gameObject.transform.position = teleportPosition3.position;
        }
        else if (Input.GetKeyDown(teleport4))
        {
            gameObject.transform.position = teleportPosition4.position;
        }
        else if (Input.GetKeyDown(teleport5))
        {
            gameObject.transform.position = teleportPosition5.position;
        }
        else if (Input.GetKeyDown(teleport6))
        {
            gameObject.transform.position = teleportPosition6.position;
        }
        else if (Input.GetKeyDown(teleport7))
        {
            gameObject.transform.position = teleportPosition7.position;
        }
    }
}
