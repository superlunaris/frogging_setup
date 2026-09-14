using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int HatsCollected { get; private set; }

    public void OnCollect()
    {
        HatsCollected++;
    }
}
