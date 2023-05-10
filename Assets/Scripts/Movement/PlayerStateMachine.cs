using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerStateMachine : StateMachine
{
    public bool FacingRight => _facingRight;
    private bool _facingRight = true;

    [SerializeField]
    private LifeComponent _lifeComponent;

    public LifeComponent LifeComponent => _lifeComponent;

    [SerializeField]
    private float _speed;

    public float Speed => _speed;

    [SerializeField]
    private float _acceleration;

    public float Acceleration => _acceleration;

    [SerializeField]
    private float _deceleration;

    public float Deceleration => _deceleration;

    [SerializeField]
    [Tooltip("The amount of air resistance to apply to the player when in the air.")]
    private float _airResistance = 1f;

    public float AirResistance => _airResistance;
    
    private Rigidbody2D _rigidbody2D;

    public Rigidbody2D RigidBody2D => _rigidbody2D;

    private CollisionDetector _collisionDetector;
    private float  _directionalInput;
    public float DirectionalInput() => _directionalInput;

    public CollisionDetector CollisionDetector => _collisionDetector;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collisionDetector = GetComponent<CollisionDetector>();
        _lifeComponent = GetComponent<LifeComponent>();
        Initialize(new Grounded(this));
    }

    public override void Update()
    {
        base.Update();
        //FlipLogic();
    }

    private void FlipLogic()
    {
        // If the input is moving the player right and the player is facing left...
        if (_directionalInput > 0 && !_facingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (_directionalInput < 0 && _facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void DirectionalInput(InputAction.CallbackContext context)
    {
        _directionalInput = context.ReadValue<Vector2>().x;
    }
    
}
