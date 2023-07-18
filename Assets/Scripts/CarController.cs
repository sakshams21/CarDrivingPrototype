using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{

    [SerializeField] private Rigidbody MainBody;
    [SerializeField] private float ForceModifier;
    private CustomInput _customInputRef;

    private Vector2 _value;
    private void Awake()
    {
        _customInputRef = new CustomInput();
    }

    private void Start()
    {
        _customInputRef.Player.Enable();
        //_customInputRef.Player.ForwardBackward.performed += Movement;

    }

    private void Update()
    {
        _value = _customInputRef.Player.ForwardBackward.ReadValue<Vector2>();
       
        if(_value.y==0) return;
        if(_value.y>0)
            MainBody.AddForce(Vector3.forward*ForceModifier);
        else
            MainBody.AddForce(Vector3.back*ForceModifier);
    }


}
