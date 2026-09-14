using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float bounceForce = 100f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidBody = collision.gameObject.GetComponent<Rigidbody>();

        if (rigidBody != null)
        {
            rigidBody.linearVelocity = Vector3.zero;

            Vector3 bounceDirection = Vector3.up;

            rigidBody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
