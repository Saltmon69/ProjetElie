using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider2D bladeCollider;
    private bool slicing;
    private TrailRenderer trail;
    public float sliceForce = 5f;

    public float minSliceVelocity = 0.01f;
    
    public Vector2 direction { get; private set; }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
    }

    private void Awake()
    {
        bladeCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
        trail = GetComponent<TrailRenderer>();
    }

    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        
        transform.position = newPosition;
        
        slicing = true;
        bladeCollider.enabled = true;
        trail.enabled = true;
        trail.Clear();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        trail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }
}
