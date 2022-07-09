using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    public FixedJoystick joysctickHorizontal;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private bool pressedHandbrake = false;
    private bool pressedGas = false;
    private bool pressedGasBack = false;

    private void FixedUpdate()
    {
        GetInput();
        HandleSteering();
        UpdateWheels();
        if (pressedHandbrake) 
        {   
            currentbreakForce = 3000f;
            ApplyBreaking();
        }

        if (pressedGas) 
        {   
            frontLeftWheelCollider.motorTorque = motorForce;
            frontRightWheelCollider.motorTorque =  motorForce;
            currentbreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();   
        }

        if (pressedGasBack) 
        {   
            frontLeftWheelCollider.motorTorque = -motorForce;
            frontRightWheelCollider.motorTorque =  -motorForce;
            currentbreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();   
        }
    }


    private void GetInput()
    {
        horizontalInput = joysctickHorizontal.Horizontal;
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LastPos"))
        {   
            Debug.Log("Finish");
            SceneManager.LoadScene("Menu");
        }
    }

     public void onDownHandbrake()
    {
        pressedHandbrake = true;
    }

    public void onUpHandbrake()
    {
        pressedHandbrake = false;
    }

    public void onDownGas()
    {
        pressedGas = true;
    }

    public void onUpGas()
    {
        pressedGas = false;
    }

    public void onDownGasBack()
    {
        pressedGasBack = true;
    }

    public void onUpGasBack()
    {
        pressedGasBack = false;
    }


    public void Restart()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
