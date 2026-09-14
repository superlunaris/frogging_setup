using UnityEngine;

public class PlayerGrappling : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement playerMovement;
    public Transform playerCamera;
    public Transform tongueTip;
    public LayerMask isGrappleable;
    public LineRenderer lineRenderer;

    [Header("Grappling")]
    [SerializeField] private float maxGrappleDistance;
    [SerializeField] private float grappleDelay;
    [SerializeField] private float overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    [SerializeField] private float grapplingCooldown;
    private float grapplingCooldownTime;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }

        if (grapplingCooldownTime > 0)
        {
            grapplingCooldownTime -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            lineRenderer.SetPosition(0, tongueTip.position);
        }
    }

    private void StartGrapple()
    {
        if (grapplingCooldownTime > 0) return;

        grappling = true;
        playerMovement.freeze = true;

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, maxGrappleDistance, isGrappleable))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelay);
        }
        else
        {
            StopGrapple();

            //grapplePoint = playerCamera.position + playerCamera.forward * maxGrappleDistance;

            //Invoke(nameof(StopGrapple), grappleDelay);
        }

        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {
        playerMovement.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0)
        {
            highestPointOnArc = overshootYAxis;
        }

        playerMovement.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        grappling = false;
        playerMovement.freeze = false;
        grapplingCooldownTime = grapplingCooldown;

        lineRenderer.enabled = false;
    }

}
