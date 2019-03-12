using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public LayerMask movementMask;
    public Interactable focus; 

    Camera cam;
    PlayerMotor motor;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            removeFocus();
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: "+hit.collider.name);
                motor.MoveToPoint(hit.point);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.collider.name);
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null)
                {
                    setFocus(interactable);
                }
            }
        }
        if (Input.GetKey("up"))
        {
            transform.Translate(0, 0, 10*Time.deltaTime);
            Debug.Log("going forward");
        }
        else if (Input.GetKey("down"))
        {
            transform.Translate(0, 0, -10*Time.deltaTime);
            Debug.Log("going back");
        }
        if (Input.GetKey("left"))
        {
            transform.Translate(-10*Time.deltaTime, 0, 0);
            Debug.Log("going left");
        }
        else if (Input.GetKey("right"))
        {
            transform.Translate(10*Time.deltaTime, 0, 0);
            Debug.Log("going right");
        }

    }

    void setFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if(focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
            motor.FollowTarget(focus);
        }
        newFocus.OnFocused(transform);
    }

    void removeFocus()
    {
        if(focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
}
