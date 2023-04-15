using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _Distancejoint;
    public Rigidbody2D rb;
    public float force;
    private Vector3 MouseDir;
    public Transform LinePosition;
    public bool canGrapple;
    public Transform lookToHook;


    // Start is called before the first frame update
    void Start()
    {
        canGrapple = true;
        _Distancejoint.autoConfigureDistance = true; // segurament ho voldre en false
        _Distancejoint.enabled = false;
        _lineRenderer.enabled = false;

    }


    // Update is called once per frame
    void Update()
    {
        MouseDir = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (canGrapple == true)
        {

            if (Input.GetKeyDown(KeyCode.Space)) //set location where to grapple
            {

                Vector2 mousepos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition); //agafa pos

                _lineRenderer.SetPosition(0, mousepos); //pos final de la linea, mouse
                _lineRenderer.SetPosition(1, transform.position); //pos inicial de la linea, pj

                _Distancejoint.connectedAnchor = mousepos;
                _Distancejoint.enabled = true;

                LinePosition.position = mousepos; 


            }
            if (Input.GetKey(KeyCode.Space)) 
            {

                _lineRenderer.SetPosition(1, transform.position); //setea la pos 1 de la linea al PJ


                _lineRenderer.enabled = true; //pinta linea

            }
            // else if (Input.GetKeyUp(KeyCode.Space)) //despinta la linea
            // {
            //     _Distancejoint.enabled = false;

            //     _lineRenderer.enabled = false;
            // }
            if (_Distancejoint.enabled)
            {
                _lineRenderer.SetPosition(1, transform.position); //s'asegura que la pos inicial sempre es el player
            }
            if (Input.GetKey(KeyCode.Space)) //Input.GetKey(KeyCode.E) && -- && hasCollided
            {

                Vector3 Direction = LinePosition.position - transform.position;

                rb.velocity = new Vector2(Direction.x * force, Direction.y * force).normalized * force * Time.deltaTime; 
                _Distancejoint.enabled = false;
            }

            if (Input.GetKeyUp(KeyCode.Space)) //Input.GetKeyUp(KeyCode.E) && 
            {
                _Distancejoint.enabled = false;

                _lineRenderer.enabled = false;

            }
        }

    }
}
