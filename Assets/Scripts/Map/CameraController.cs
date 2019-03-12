using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;

    public Vector3 offset;

    public float zoomSpeed = 0.005f;
    public float minZoom = 2f;
    public float maxZoom = 8f;
    public float currentZoom = 6f;

    public float yawSpeed = 100f;
    public float currentYaw = 0f;
    public float minYaw = -25;
    public float maxYaw = 25;

    public float pitch = 10f;

    private void Update()
    {
        if (Input.GetKey("z"))
        {
            currentZoom -= zoomSpeed;
        }else if (Input.GetKey("x"))
        {
            currentZoom += zoomSpeed;
        }
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        //currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        currentYaw = Mathf.Clamp(currentYaw, minYaw, maxYaw);
    }

    // Update is called once per frame
    void LateUpdate () {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
	}
}
