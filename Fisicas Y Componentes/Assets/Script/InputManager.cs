using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    #region Atributes
    //Public Atributes
    public bool debugMode = false;
    public Transform PlayerTransform;
    public Transform CameraTransform;
    public float Speed = 8f;
    public float Sensitivity = 80f;
    public float ZoomSpeed = 20f;
 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Eesto no se inicia en la emulacion si no clicas en la pantalla del emulador
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region Debug
        if (debugMode == true)
        {
        
        //Mouse and Keyboard Debug
        Debug.DrawRay(PlayerTransform.position, Vector3.right * Input.GetAxis("Horizontal"), Color.red);
        Debug.DrawRay(PlayerTransform.position, Vector3.forward * Input.GetAxis("Vertical"), Color.blue);
        Debug.DrawRay(PlayerTransform.position, Vector3.right * Input.GetAxis("Mouse X"), Color.red);
        Debug.DrawRay(PlayerTransform.position, Vector3.forward * Input.GetAxis("Mouse Y"), Color.blue);

        //Button
        print("Horizontal: " + Input.GetAxis("Horizontal"));
        if (Input.GetButtonUp("Fire1"))
        {
            print("Fire");
        }

        //Mouse Wheel and Mouse Position
        print("MouseWheel: " + Input.GetAxis("Mouse ScrollWheel"));

        print("Mouse X: " + Input.GetAxis("Mouse X"));
        print("Mouse Y: " + Input.GetAxis("Mouse Y"));

        //Player Movement Mouse
        PlayerTransform.Translate(Vector3.right * Input.GetAxis("Mouse X") * Time.deltaTime * Speed);
        PlayerTransform.Translate(Vector3.forward * Input.GetAxis("Mouse Y") * Time.deltaTime * Speed);

        //Player Movement Keyboard
        PlayerTransform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * Speed);
        PlayerTransform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * Speed);
        }
        #endregion

        #region Muvement

        //Camera Rotation
        CameraTransform.RotateAround(PlayerTransform.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * Sensitivity);
        CameraTransform.RotateAround(PlayerTransform.position, CameraTransform.right, Input.GetAxis("Mouse Y") * Time.deltaTime * Sensitivity * -1);

        //Camera axes
        Vector3 camForward = CameraTransform.forward;
        Vector3 camRight = CameraTransform.right;
        camForward.Normalize();
        camRight.Normalize();

        //Camera Zoom
        CameraTransform.LookAt(PlayerTransform.position);
        CameraTransform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ZoomSpeed * -1);

        //Player Movement
        Vector3 moveDir = (camForward * Input.GetAxis("Vertical") + camRight * Input.GetAxis("Horizontal")).normalized;
        PlayerTransform.position += moveDir * Speed * Time.deltaTime;
        Vector3 verticalMove = Vector3.zero;
        if (Input.GetKey(KeyCode.Space)) verticalMove.y += Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) verticalMove.y -= Speed * Time.deltaTime;
        PlayerTransform.position += verticalMove;

        #endregion

        #region Reset
        //Reset Position
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerTransform.position = new Vector3(0, 5, 0);
            CameraTransform.position = new Vector3(0, 2, -5);
            CameraTransform.LookAt(PlayerTransform.position);
        }

        #endregion

    }
}
