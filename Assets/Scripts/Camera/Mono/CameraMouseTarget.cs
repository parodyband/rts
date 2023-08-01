using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseTarget : MonoBehaviour
{
    public GameObject reticle;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                Instantiate(reticle, hit.point, Quaternion.identity);
            }
        }
    }
}