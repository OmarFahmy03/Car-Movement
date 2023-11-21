using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    private const string H = "Horizontal";
    private const string V = "Vertical";

    private float horizontalInput;
    private float vertiaclInput;
    private float currentbreakForce;
    private float CurrentSteerangel;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float maxspeed;
    [SerializeField] private float breakforce;
    [SerializeField] private float maxSteerAngel;

    [SerializeField] private WheelCollider FLwheelCol;
    [SerializeField] private WheelCollider FRwheelcCol;
    [SerializeField] private WheelCollider BLwheelCol;
    [SerializeField] private WheelCollider BRwheelCol;
    
    [SerializeField] private Transform FLWheelTrans;
    [SerializeField] private Transform FRWheelTrans;
    [SerializeField] private Transform BLWheelTrans;
    [SerializeField] private Transform BRWheelTrans;

    [SerializeField] private TextMeshProUGUI  speedText;

    void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        DisplaySpeed();
    }
    private void DisplaySpeed()
    {
        float speed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f; // Multiply by 3.6 to convert m/s to km/h
        speedText.text = "Speed: " + Mathf.Round(speed) + " km/h";
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        vertiaclInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetButton("Fire1");
    }
    private void HandleMotor()
    {
        FRwheelcCol.motorTorque = vertiaclInput * motorForce;
        FLwheelCol.motorTorque = vertiaclInput * motorForce;
        currentbreakForce = isBreaking ? breakforce : 0f;
        ApplyBreaking();
    }
    private void ApplyBreaking()
    {
        FRwheelcCol.brakeTorque = currentbreakForce;
        FLwheelCol.brakeTorque = currentbreakForce;
        BLwheelCol.brakeTorque = currentbreakForce;
        BRwheelCol.brakeTorque = currentbreakForce;
    }
    private void HandleSteering()
    {
        CurrentSteerangel = maxSteerAngel * horizontalInput;
        FLwheelCol.steerAngle = CurrentSteerangel;
        FRwheelcCol.steerAngle = CurrentSteerangel;
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(FLwheelCol , FLWheelTrans);
        UpdateSingleWheel(FRwheelcCol , FRWheelTrans);
        UpdateSingleWheel(BLwheelCol , BLWheelTrans);
        UpdateSingleWheel(BRwheelCol , BRWheelTrans);
    }
    private void UpdateSingleWheel(WheelCollider wheelcol , Transform wheeltrans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelcol.GetWorldPose(out pos, out rot);
        wheeltrans.rotation = rot;
        wheeltrans.position = pos;
    }
}
