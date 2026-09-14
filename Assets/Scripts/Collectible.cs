using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            playerInventory.OnCollect();
            gameObject.SetActive(false);
        }
    }
}
