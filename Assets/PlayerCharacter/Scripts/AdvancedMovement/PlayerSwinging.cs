using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Input")]
    public KeyCode swingKey = KeyCode.Mouse0;

    [Header("References")]
    public LineRenderer lineRenderer;
    public Transform tongueTip, playerCamera, playerModel;
    public LayerMask isSwingable;

    [Header("Swinging")]
    [SerializeField] private float maxSwingDistance = 25f;
    private Vector3 swingPoint;
    private SpringJoint joint;

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
    }

    private void StartSwing()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, maxSwingDistance, isSwingable))
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
        lineRenderer.positionCount = 0;
        Destroy(joint);
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
