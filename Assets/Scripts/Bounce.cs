using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float bounceForce = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidBody = collision.gameObject.GetComponent<Rigidbody>();

        if (rigidBody != null)
        {
            rigidBody.linearVelocity = Vector3.zero;

            Vector3 bounceDirection = transform.up;

            rigidBody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
