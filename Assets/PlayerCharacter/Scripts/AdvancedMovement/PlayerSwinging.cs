using UnityEngine;

public class PlayerSwinging : MonoBehaviour
{
    [Header("Input")]
    public KeyCode swingKey = KeyCode.Mouse0;

    [Header("References")]
    public LineRenderer lineRenderer;
    public Transform tongueTip, playerCamera, playerModel;
    public LayerMask isSwingable;
    public PlayerMovement playerMovement;

    [Header("Swinging")]
    [SerializeField] private float maxSwingDistance = 25f;
    private Vector3 swingPoint;
    private SpringJoint joint;

    [Header("Aerial")]
    public Transform orientation;
    public Rigidbody rigidBody;
    public float horizontalForce;
    public float forwardForce;
    public float extendTongueSpeed;

    void Update()
    {
        DrawRope();

        if (Input.GetKeyDown(swingKey))
        {
            StartSwing();
        }
        if (Input.GetKeyUp(swingKey))
        {
            StopSwing();
        }

        if (joint != null)
        {
            AerialMobility();
        }
    }

    private void StartSwing()
    {
        playerMovement.swinging = true;

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, maxSwingDistance))
        {
            swingPoint = hit.point;
            joint = playerModel.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(playerModel.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lineRenderer.positionCount = 2;
            currentTonguePosition = tongueTip.position;
        }
    }

    private void StopSwing()
    {
        playerMovement.swinging = false;

        lineRenderer.positionCount = 0;
        Destroy(joint);
    }

    private void AerialMobility()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddForce(orientation.right * horizontalForce * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddForce(-orientation.right * horizontalForce * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce(orientation.forward * forwardForce * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.AddForce(-orientation.forward * forwardForce * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = swingPoint - transform.position;
            rigidBody.AddForce(directionToPoint.normalized * forwardForce * Time.deltaTime);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, swingPoint) + extendTongueSpeed;

            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }
    }

    private Vector3 currentTonguePosition;

    void DrawRope()
    {
        if (!joint) return;

        currentTonguePosition = Vector3.Lerp(currentTonguePosition, swingPoint, Time.deltaTime * 8f);

        lineRenderer.SetPosition(0, tongueTip.position);
        lineRenderer.SetPosition(1, swingPoint);
    }

}
