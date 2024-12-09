using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float keyboardInputSensitivity = 1f;
    [SerializeField] float mouseInputSensitivity = 1f;
    [SerializeField] bool continous = true;

    [SerializeField] Transform BottomLeftBorder;
    [SerializeField] Transform TopRightBorder;

    Vector3 input;
    Vector3 pointofOrigin;
    
    // Update is called once per frame
   
    private void Update()
    {
        NullInput();
        
        MoveCameraInput();

        MoveCamera();
    }

    private void NullInput()
    {
        input.x = 0;
        input.y = 0;
        input.z = 0;
    }

    private void MoveCamera()
    {
        Vector3 position = transform.position;
        position += (input * Time.deltaTime);
        position.x = Mathf.Clamp(position.x, BottomLeftBorder.position.x, TopRightBorder.position.x);
        position.z = Mathf.Clamp(position.z, BottomLeftBorder.position.z, TopRightBorder.position.z);

        transform.position = position;
    }

    private void MoveCameraInput()
    {
        AxisInput();
        MouseInput();
    }

    private void MouseInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            pointofOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseInput = Input.mousePosition;
            input.x += (mouseInput.x - pointofOrigin.x) * mouseInputSensitivity;
            input.z += (mouseInput.y - pointofOrigin.y) * mouseInputSensitivity;
            if(continous == false)
            {
                pointofOrigin = mouseInput;
            }
        }
    }

    private void AxisInput()
    {
        input.x += Input.GetAxisRaw("Horizontal") * keyboardInputSensitivity;
        input.z += Input.GetAxisRaw("Vertical") * keyboardInputSensitivity;

    }
}
