using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{

    [SerializeField] private Rigidbody MainBody;
    [SerializeField] private float ForceModifier;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float HandlingModifier;
    private CustomInput _customInputRef;

    private Vector2 _handlingValue;
    private Vector3 _mainBodyVelocity;
    private void Awake()
    {
        _customInputRef = new CustomInput();
    }

    private void Start()
    {
        _customInputRef.Player.Enable();
    }

    private void Update()
    {
        _handlingValue = _customInputRef.Player.Handling.ReadValue<Vector2>();

        Debug.Log(MainBody.velocity);
        Move(_handlingValue.x,_handlingValue.y);
        
    }

    private void Move(float horizontal,float vertical)
    {
        if(vertical==0) return;

        _mainBodyVelocity = MainBody.velocity;

        if (vertical > 0)
        {
            if(_mainBodyVelocity.z<MaxSpeed)
                MainBody.AddForce(Vector3.forward*ForceModifier,ForceMode.Acceleration);
            else
            {
                _mainBodyVelocity.z = MaxSpeed;
            }
        }

        else
        {
            if(_mainBodyVelocity.z>=-MaxSpeed*0.3f)
                MainBody.AddForce(Vector3.back * (ForceModifier * 0.3f),ForceMode.Acceleration);
            else 
                _mainBodyVelocity.z = -1*(MaxSpeed * 0.3f);
        }
        if (horizontal != 0)
        {
            _mainBodyVelocity = MainBody.velocity;
            _mainBodyVelocity.x = horizontal * HandlingModifier;
            MainBody.velocity = _mainBodyVelocity;
        }
        else
        {
            MainBody.velocity = _mainBodyVelocity;
        }
    }


}
