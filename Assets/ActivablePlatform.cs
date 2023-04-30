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
    }

    void Move(Vector2 direction)
    {
        _rigidbody2D.velocity = direction * _linearSpeed * Time.deltaTime;
    }

    bool ReachedNode(Vector2 nodePosition)
    {
        Vector2 position = nodePosition + _direction * this.transform.localScale.x / 2
            + _direction * _checkRadius;

        var results = Physics2D.OverlapCircleAll(position, _checkRadius,_layerMask);
        if(results.Length > 0)
        {
            foreach(var result in results)
            {
                if(result.gameObject == this.gameObject)
                {
                    return true;
                }
            }
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

        print("He hecho el appear");
        transform.position = _nodes[0];

        _direction = _nodes[1] - _nodes[0];
        _direction = _direction.normalized;
    }

    private protected override void Disappear()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private protected override void HazardUpdate()
    {
         if(_state == ActivablePlatformState.MovingForward)
        {
            if(ReachedNode(_nodes[1]))
            {
                SwitchState(ActivablePlatformState.ReachedEnd);
            }
        }
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
                SwitchState(ActivablePlatformState.MovingForward);
            }
            else if(_state == ActivablePlatformState.ReachedBegining)
            {
                SwitchState(ActivablePlatformState.MovingBackward);
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