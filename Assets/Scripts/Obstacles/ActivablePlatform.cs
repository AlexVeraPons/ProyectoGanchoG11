using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivablePlatform : MonoBehaviour, IInteractable
{
    [SerializeField]
    private LayerMask _layerMask;
    private const float _checkRadius = 0.3f;
    private Rigidbody2D _rigidbody2D;
    
    [Header("ActivablePlatformValues")]
    [SerializeField]
    private Color _activatedColor;
    [SerializeField]
    private Color _pendingColor;
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _linearSpeed = 0;
    
    [SerializeField]
    private Transform _nodesParent;

    private Vector2[] _nodes;
    Vector2 _direction;
    [SerializeField] ActivablePlatformState _state;

    private Action OnMoveForward;
    private Action OnMoveBackward;

    private void Awake()
    {
        _nodes = new Vector2[_nodesParent.childCount];
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        _spriteRenderer = this.GetComponent<SpriteRenderer>();

        // populate the nodes array

        _nodes[0] = this.transform.position;

        for (int i = 0; i < _nodesParent.childCount; i++)
        {
            _nodes[i] = _nodesParent.GetChild(i).gameObject.transform.position;
        }

        transform.position = _nodes[0];

        _direction = _nodes[1] - _nodes[0];
        _direction = _direction.normalized;
    }

    private void Start()
    {
        _spriteRenderer.color = _pendingColor;
    }

    void Update()
    {
        if(_state == ActivablePlatformState.MovingForward)
        {
            if(ReachedNode(_nodes[1], _direction))
            {
                SwitchState(ActivablePlatformState.ReachedEnd);
            }
        }
        else if(_state == ActivablePlatformState.MovingBackward)
        {
            if(ReachedNode(_nodes[0], -_direction))
            {
                SwitchState(ActivablePlatformState.ReachedBegining);
            }
        }
    }

    void FixedUpdate()
    {
        if(_state == ActivablePlatformState.MovingForward)
        {
            Move(direction: _direction);
        }
        else if(_state == ActivablePlatformState.MovingBackward)
        {
            Move(direction: -_direction);
        }
    }

    void Move(Vector2 direction)
    {
        _rigidbody2D.velocity = direction * _linearSpeed * Time.deltaTime;
    }

    bool ReachedNode(Vector2 nodePosition, Vector2 direction)
    {
        float _distance = Vector2.Distance(_nodes[0], _nodes[1]);

        switch(_state)
        {
            case ActivablePlatformState.MovingForward:
                if(Vector2.Distance(this.transform.position, _nodes[0]) > _distance)
                {
                    return true;
                }
            break;

            case ActivablePlatformState.MovingBackward:
                if(Vector2.Distance(this.transform.position, _nodes[1]) > _distance)
                {
                    return true;
                }
            break;

            default: break;
        }

        return false;
    }

    void SwitchState(ActivablePlatformState newState)
    {
        _state = newState;
        OnEnterState(newState);
    }

    void OnEnterState(ActivablePlatformState newState)
    {
        switch(newState)
        {
            case ActivablePlatformState.MovingForward:
                _spriteRenderer.color = _activatedColor;
                OnMoveForward?.Invoke();
            break;

            case ActivablePlatformState.MovingBackward:
                _spriteRenderer.color = _activatedColor;
                OnMoveBackward?.Invoke();
            break;

            case ActivablePlatformState.ReachedEnd:
                _spriteRenderer.color = _pendingColor;
                _rigidbody2D.velocity = Vector2.zero;
                this.transform.position = _nodes[1];
            break;

            case ActivablePlatformState.ReachedBegining:
                _spriteRenderer.color = _pendingColor;
                _rigidbody2D.velocity = Vector2.zero;
                this.transform.position = _nodes[0];
            break;

            default: break;
        }
    }

    public void DoInteraction()
    {
            //THIS HAS TO BE EMPTY
    }

    public void UndoInteraction()
    {
        if(_state == ActivablePlatformState.Inactive)
        {
            SwitchState(ActivablePlatformState.MovingForward);
        }
        else
        {
            if(_state == ActivablePlatformState.ReachedEnd)
            {
                SwitchState(ActivablePlatformState.MovingBackward);
            }
            else if(_state == ActivablePlatformState.ReachedBegining)
            {
                SwitchState(ActivablePlatformState.MovingForward);
            }
        }
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}

public enum ActivablePlatformState
{
    Inactive,
    MovingForward,
    ReachedEnd,
    MovingBackward,
    ReachedBegining
}