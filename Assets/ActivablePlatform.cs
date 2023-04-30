using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivablePlatform : Hazard, IInteractable
{
    [SerializeField]
    private LayerMask _layerMask;
    private const float _checkRadius = 0.3f;
    private Rigidbody2D _rigidbody2D;
    
    [Header("ActivablePlatformValues")]
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
    }

    private void FixedUpdate()
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

    private protected override void HazardUpdate()
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

    private protected override void Appear()
    {
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

    private protected override void Disappear()
    {
        GetComponent<SpriteRenderer>().enabled = false;
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
                GetComponent<SpriteRenderer>().color = Color.green;
                OnMoveForward?.Invoke();
            break;

            case ActivablePlatformState.MovingBackward:
                GetComponent<SpriteRenderer>().color = Color.green;
                OnMoveBackward?.Invoke();
            break;

            case ActivablePlatformState.ReachedEnd:
                GetComponent<SpriteRenderer>().color = Color.red;
                _rigidbody2D.velocity = Vector2.zero;
                this.transform.position = _nodes[1];
            break;

            case ActivablePlatformState.ReachedBegining:
                GetComponent<SpriteRenderer>().color = Color.red;
                _rigidbody2D.velocity = Vector2.zero;
                this.transform.position = _nodes[0];
            break;

            default: break;
        }
    }

        public void DoInteraction()
    {
        //It does nothing on interaction
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
}

public enum ActivablePlatformState
{
    Inactive,
    MovingForward,
    ReachedEnd,
    MovingBackward,
    ReachedBegining
}