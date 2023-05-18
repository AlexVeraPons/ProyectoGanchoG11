using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TrailRenderer))]
public class Missile : Hazard
{
    [Space(10)]
    [Header("Missile Variables")]
    [SerializeField]
    private float _rotationalSpeed;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject _target;
    private Vector3 _targetPos => _target.transform.position;

    private Rigidbody2D _rigidbody2D;
    private TrailRenderer _trailRenderer;
    private Vector2 _intialPosition;
    

    private protected override void Awake()
    {
        base.Awake();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _target = GameObject.FindObjectOfType<PlayerEntity>().gameObject;
    }
    private void Start()
    {
        _intialPosition = transform.position;
    }

    private protected override void Appear()
    {
        base.Appear();

        _trailRenderer = GetComponent<TrailRenderer>();
        _trailRenderer.enabled = true;
    }

    private protected override void Disappear()
    {
        this.gameObject.SetActive(false);
    }

    private protected override void HazardUpdate()
    {
        Vector2 direction = (Vector2)_targetPos - _rigidbody2D.position;
        direction.Normalize();

        Vector3 rotationAmount = Vector3.Cross(direction, transform.up);

        _rigidbody2D.angularVelocity = -_rotationalSpeed * rotationAmount.z;

        _rigidbody2D.velocity = transform.up * _speed;
    }

    private protected override void DamageableAction(Collider2D collision)
    {
        base.DamageableAction(collision);
        AudioManager._instance.PlaySingleSound(SingleSound.MissileCrash);
    }

    public override void ResetHazard()
    {
        transform.position = _intialPosition;
        base.ResetHazard();
    }
}
