using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField]
    private float _groundCheckDistance;

    [SerializeField]
    private LayerMask _groundLayerMask;

    private float? _groundedTime;

    private CapsuleCollider _capsuleCollider;

    public bool IsGrounded { get; private set; }

    public float? DistanceToGround { get; private set; }

    public Vector3? GroundPosition { get; private set; }

    public Vector3? GroundNormal { get; private set; }

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        Vector3 spherCastOrigin = transform.position + new Vector3(0f, _capsuleCollider.radius, 0f);
        float sphereCastRadius = _capsuleCollider.radius - 0.1f;

        bool groundIsBelow = Physics.SphereCast(spherCastOrigin, sphereCastRadius, Vector3.down, out RaycastHit hitInfo, 1000, _groundLayerMask, QueryTriggerInteraction.Ignore);

        if (groundIsBelow)
        {
            DistanceToGround = transform.position.y - hitInfo.point.y;
            GroundPosition = hitInfo.point;
            GroundNormal = hitInfo.normal;
        }
        else
        {
            DistanceToGround = null;
            GroundPosition = null;
            GroundNormal = null;
        }

        IsGrounded = groundIsBelow && DistanceToGround <= _groundCheckDistance;

        if (IsGrounded)
        {
            _groundedTime = Time.time;
        }
    }

    public bool GroundedRecently(float groundedGracePeriod)
    {
        float? timeSinceLastGrounded = Time.time - _groundedTime;

        return timeSinceLastGrounded <= groundedGracePeriod;
    }
}
