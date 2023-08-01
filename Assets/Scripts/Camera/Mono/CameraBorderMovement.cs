using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBorderMovement : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;

    private Vector3 m_Vel;
    private Vector3 m_TargetPosition;

    private void Start()
    {
        m_TargetPosition = transform.position;
    }

    private void Update()
    {
        // moving camera with mouse position
        // if (Input.mousePosition.y >= Screen.height - panBorderThickness) // top of screen
        // {
        //     m_TargetPosition += transform.forward * (panSpeed * Time.deltaTime);
        // }
        // if (Input.mousePosition.y <= panBorderThickness) // bottom of screen
        // {
        //     m_TargetPosition -= transform.forward * (panSpeed * Time.deltaTime);
        // }
        // if (Input.mousePosition.x >= Screen.width - panBorderThickness) // right of screen
        // {
        //     m_TargetPosition += transform.right * (panSpeed * Time.deltaTime);
        // }
        // if (Input.mousePosition.x <= panBorderThickness)        // left of screen
        // {
        //     m_TargetPosition -= transform.right * (panSpeed * Time.deltaTime);
        // }

        m_TargetPosition += transform.right * (Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime);
        m_TargetPosition += transform.forward * (Input.GetAxis("Vertical") * panSpeed * Time.deltaTime);

        var currentPosition = transform.position;
        
        m_TargetPosition.x = Mathf.Clamp(m_TargetPosition.x, -panLimit.x, panLimit.x);
        m_TargetPosition.z = Mathf.Clamp(m_TargetPosition.z, -panLimit.y, panLimit.y);
        
        transform.position = Vector3.SmoothDamp(currentPosition,m_TargetPosition, ref m_Vel, Time.deltaTime * 35f);
    }

}
