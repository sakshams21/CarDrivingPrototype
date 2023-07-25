using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CarController : MonoBehaviour
{
    // Settings
    [SerializeField] private float MotorForce;
    [SerializeField] private float BreakForce;
    [SerializeField] private float MaxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider FrontLeftWheelCollider;
    [SerializeField] private WheelCollider FrontRightWheelCollider;
    [SerializeField] private WheelCollider RearLeftWheelCollider;
    [SerializeField] private WheelCollider RearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform FrontLeftWheelTransform;
    [SerializeField] private Transform FrontRightWheelTransform;
    [SerializeField] private Transform RearLeftWheelTransform;
    [SerializeField] private Transform RearRightWheelTransform;
    
    private float _horizontalInput, _verticalInput;
    private float _currentSteerAngle, _currentBreakForce;
    private bool _isBreaking;
    private static CustomInput _customInput;
    private void Awake()
    {
        _customInput = new CustomInput();
    }

    private void Start()
    {
        _customInput.Player.Enable();
    }

  

    private void FixedUpdate() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput() {
        // Steering Input
        _horizontalInput = _customInput.Player.Handling.ReadValue<Vector2>().x;

        // Acceleration Input
        _verticalInput = _customInput.Player.Handling.ReadValue<Vector2>().y;

        // Breaking Input
        _isBreaking = _customInput.Player.Breaking.IsPressed();
    }

    private void HandleMotor() {
        FrontLeftWheelCollider.motorTorque = _verticalInput * MotorForce;
        FrontRightWheelCollider.motorTorque = _verticalInput * MotorForce;
        _currentBreakForce = _isBreaking ? BreakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking() {
        FrontRightWheelCollider.brakeTorque = _currentBreakForce;
        FrontLeftWheelCollider.brakeTorque = _currentBreakForce;
        RearLeftWheelCollider.brakeTorque = _currentBreakForce;
        RearRightWheelCollider.brakeTorque = _currentBreakForce;
    }

    private void HandleSteering() {
        _currentSteerAngle = MaxSteerAngle * _horizontalInput;
        FrontLeftWheelCollider.steerAngle = _currentSteerAngle;
        FrontRightWheelCollider.steerAngle = _currentSteerAngle;
    }

    private void UpdateWheels() {
        UpdateSingleWheel(FrontLeftWheelCollider, FrontLeftWheelTransform);
        UpdateSingleWheel(FrontRightWheelCollider, FrontRightWheelTransform);
        UpdateSingleWheel(RearRightWheelCollider, RearRightWheelTransform);
        UpdateSingleWheel(RearLeftWheelCollider, RearLeftWheelTransform);
    }

    private static void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        wheelCollider.GetWorldPose(out var pos, out var rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

}
