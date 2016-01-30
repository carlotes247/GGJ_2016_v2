﻿/* 
* Copyright 2007 Carlos González Díaz
*  
* Licensed under the EUPL, Version 1.1 or – as soon they
will be approved by the European Commission - subsequent
versions of the EUPL (the "Licence");
* You may not use this work except in compliance with the
Licence.
* You may obtain a copy of the Licence at:
*  
*
https://joinup.ec.europa.eu/software/page/eupl
*  
* Unless required by applicable law or agreed to in
writing, software distributed under the Licence is
distributed on an "AS IS" basis,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
express or implied.
* See the Licence for the specific language governing
permissions and limitations under the Licence.
*/ 
using UnityEngine;
using System.Collections;

/// <summary>
/// The Input Controller, controls the input devices and logic of the game
/// </summary>
public class InputController : MonoBehaviour {

    /// The pointer position in screen coordinates
    // Field 
    [SerializeField]
    private Vector3 screenPointerPos;
    // Property
    public Vector3 ScreenPointerPos { get { return this.screenPointerPos; } set { this.screenPointerPos = value; } }

    /// <summary>
    /// (Field) The axis that the actual controller is returning
    /// </summary>
    [SerializeField]
    private Vector2 m_ControllerAxis;
    /// <summary>
    /// (Property) The axis that the actual controller is returning
    /// </summary>
    public Vector2 ControllerAxis { get { return this.m_ControllerAxis; } }

    /// The Wiimote Input received by the computer
    // Field
    [SerializeField]
    private WiiMoteInput wiimoteInput;
    // Property
    public WiiMoteInput WiimoteInput { get { return this.wiimoteInput; } set { this.wiimoteInput = value; } }

    /// The input mode that is currently active
    // The enum to decide which control do we have
    public enum TypeOfInput
    {
        Mouse,
        WiiMote
    }
    // Field
    [SerializeField]
    private TypeOfInput inputType;
    // Property
    public TypeOfInput InputType
    {
        get { return this.inputType; }
        set { this.inputType = value; }
    }

    /// <summary>
    /// (Field) The timer of the object
    /// </summary>
    private TimerController m_Timer;

    /// <summary>
    /// (Field) The delay between the shots
    /// </summary>
    [SerializeField]
    private float m_DelayBetweenShots;
    /// <summary>
    /// (Property) The delay between the shots
    /// </summary>
    public float DelayBetweenShots { get { return this.m_DelayBetweenShots; } set { this.m_DelayBetweenShots = value; } }

    /// <summary>
    /// (Field) The rotation of the camera in eulerAngles
    /// </summary>
    [SerializeField]
    private Vector2 m_RotationCamera;
    /// <summary>
    /// (Property) The rotation of the camera in eulerAngles
    /// </summary>
    public Vector2 RotationCamera { get { return this.m_RotationCamera; } set { this.m_RotationCamera = value; } }

    /// <summary>
    /// (Field) The Axis of the camera scalated to (-1, 1)
    /// </summary>
    private Vector2 m_CameraAxis;
    /// <summary>
    /// (Property) The Axis of the camera scalated to (-1, 1)
    /// </summary>
    private Vector2 CameraAxis { get { return m_CameraAxis; } set { this.m_CameraAxis = value; } }

    /// <summary>
    /// (Field) The speed of the rotation of the camera
    /// </summary>
    [SerializeField]
    private float m_CameraRotationSpeed;

    // Use this for initialization
    void Start () {
        //WiimoteInput.WiimoteInputLogic();

        // We set always mouse on android to avoid invoking the UniWii dll
#if UNITY_ANDROID
        this.InputType = TypeOfInput.Mouse;
#endif
        // We add a timer for the input
        m_Timer = this.gameObject.AddComponent<TimerController>();
        m_Timer.ObjectLabel = "InputController Timer";

        // We check the type of input currently connected
        CheckInputType();

	}
	
	// Update is called once per frame
	void Update () {
        // We check the type of input for any changes
        CheckInputType();
        // We update the values of the screenPointerPos
        UpdateScreenPointerPos();
        // We update the values of the controllerAxis
        UpdateControllerAxis();
        // We update the values of the camera Axis
        UpdateCameraAxis();
        // We draw the pointer in the specified pos
        DrawScreenPointer(ScreenPointerPos);
        // We check if the user press any buttons
        CheckUserInput();
	}

    /// Check the type of the Input
    void CheckInputType()
    {
        //if (WiimoteInput.WiiMoteCount > 0)
        //{
        //    InputType = TypeOfInput.WiiMote;
        //}
        //else
        //{
        //    InputType = TypeOfInput.Mouse;
        //}

    }

    /// Update ScreenPointer position
    void UpdateScreenPointerPos ()
    {
        switch (InputType)
        {
            case TypeOfInput.Mouse:
                ScreenPointerPos = Input.mousePosition;
                break;
            case TypeOfInput.WiiMote:
                // We run the logic of the wiimote, to populate all the variables
                //WiimoteInput.WiimoteInputLogic();
                // We get the position of the wiimote
                ScreenPointerPos = WiimoteInput.CursorPosition;
                break;
            default:
                Debug.LogError("Type of Input not recognized!");
                break;
        }
    }

    /// <summary>
    /// Updates the values of the controller Axis
    /// </summary>
    private void UpdateControllerAxis ()
    {
        switch (InputType)
        {
            case TypeOfInput.Mouse:
                // We get the input from the keyboard
                UpdateAxisWASD();
                break;
            case TypeOfInput.WiiMote:
                // We get the input from the WiimoteNunchuck
                m_ControllerAxis = WiimoteInput.NunchuckJoystickValues;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Updates the ControllerAxis from the WASD keys in the keyboard
    /// </summary>
    private void UpdateAxisWASD ()
    {
        // We get the input from the keyboard
        // X axis
        if (Input.GetKey(KeyCode.D))
        {
            m_ControllerAxis.x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_ControllerAxis.x = -1;
        }
        else
        {
            m_ControllerAxis.x = 0;
        }
        // Y axis
        if (Input.GetKey(KeyCode.W))
        {
            m_ControllerAxis.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            m_ControllerAxis.y = -1;
        }
        else
        {
            m_ControllerAxis.y = 0;
        }
    }

    /// <summary>
    /// Updates the Camera Axis to a value between (-1, 1)
    /// </summary>
    private void UpdateCameraAxis ()
    {
        // X is the Scalated from the normalized screenPointerPos and the screen width
        m_CameraAxis.x = ReusableMethods.Normalization.ScaleNormalize((ReusableMethods.Normalization.Normalize(ScreenPointerPos.x, 0, Screen.width)), -1, 1);
        // Y is the Scalated from the normalized screenPointerPos and the screen height
        m_CameraAxis.y = ReusableMethods.Normalization.ScaleNormalize((ReusableMethods.Normalization.Normalize(ScreenPointerPos.y, 0, Screen.height)), -1, 1);
    
    }

    /// The function in charge of Drawing on Screen a pointer (can be used by the any source of input, including mouse, Wiimote, PSMove, etc)
    void DrawScreenPointer(Vector3 values)
    {
        Toolbox.Instance.GameManager.HudController.InGameCursor.transform.position = new Vector3(values.x, values.y, 0);        
    }

    void DrawScreenPointer(int value_x, int value_y)
    {
        Toolbox.Instance.GameManager.HudController.InGameCursor.transform.position = new Vector3(value_x, value_y, 0);
    }

    /// <summary>
    /// The function to check the user Input
    /// </summary>
    private void CheckUserInput()
    {
        /* CODE FOR SHOOTING NOT IN THIS GAME */
        //// We check for main click input
        //if (Input.GetMouseButtonDown(0) || WiimoteInput.ButtonB && (m_Timer.GenericCountDown(m_DelayBetweenShots)) )
        //{
        //    if (!Toolbox.Instance.GameManager.MenuController.MenuOpen)
        //    {
        //        // Toolbox.Instance.GameManager.WeaponController.Shoot(Camera.main.ScreenPointToRay(ScreenPointerPos).direction);
        //        Toolbox.Instance.GameManager.Player.WeaponController.Shoot(Camera.main.ScreenPointToRay(ScreenPointerPos).direction);
        //        // If the wiimote is selected as the input...
        //        if (InputType == TypeOfInput.WiiMote)
        //        {
        //            // We rumble the wiimote of player 1
        //            WiimoteInput.SetWiimoteRumble(0, WiimoteInput.TimeToRumble);  
        //        }
        //    }
        //}

        // Player Movement Input
        // If the player can move from the inputController...
        if (Toolbox.Instance.GameManager.Player.MovementController.TypeOfMovement == MovementController.TypeOfMovementEnum.InputController)
        {
            // ... We move the player according to the axis
            Toolbox.Instance.GameManager.Player.MovementController.MoveInputController(m_ControllerAxis,
                Toolbox.Instance.GameManager.Player.MovementController.MaxVelocity);

            // ... We rotate the camera according to the pointer pos
            // The deadzone is (0.2f, 0.1f)
            if (m_CameraAxis.x > -0.2 && m_CameraAxis.x < 0.2)
            {
                m_CameraAxis.x = 0f;
            }
            if (m_CameraAxis.y > -0.1 && m_CameraAxis.y < 0.1)
            {
                m_CameraAxis.y = 0f;
            }

            //m_YawCamera += 2f * ReusableMethods.Normalization.ScaleNormalize((ReusableMethods.Normalization.Normalize(ScreenPointerPos.x, 0, Screen.width)), -1, 1);
            //m_YawCamera += 2f * m_CameraAxis.x;
            m_RotationCamera.y += m_CameraRotationSpeed * m_CameraAxis.x;
            //m_PitchCamera -= 2f * ReusableMethods.Normalization.ScaleNormalize((ReusableMethods.Normalization.Normalize(ScreenPointerPos.y, 0, Screen.height)), -1, 1);
            //m_PitchCamera -= 2f * m_CameraAxis.y;
            m_RotationCamera.x -= m_CameraRotationSpeed * m_CameraAxis.y;

            // We limit the rotation in the Y axis to 90 degrees (that is the X one in euler angles)
            if (Mathf.Abs(m_RotationCamera.x) > 90)
            {
                m_RotationCamera.x = 90 * Mathf.Sign(m_RotationCamera.x);
            }

            //Toolbox.Instance.GameManager.Player.ObjectTransform.eulerAngles = m_RotationCamera;
            
            // We rotate the player on the X axis only
            Toolbox.Instance.GameManager.Player.ObjectTransform.eulerAngles = new Vector3 (
                Toolbox.Instance.GameManager.Player.ObjectTransform.eulerAngles.x,
                m_RotationCamera.y,
                Toolbox.Instance.GameManager.Player.ObjectTransform.eulerAngles.z);
            // We then rotate the camera on the Y axis
            Camera.main.transform.eulerAngles = new Vector3(
                m_RotationCamera.x, 
                Camera.main.transform.eulerAngles.y, 
                Camera.main.transform.eulerAngles.z);
            //Camera.main.transform.eulerAngles = new Vector3 ();
            //Camera.main.transform.eulerAngles = m_RotationCamera;
            //Toolbox.Instance.GameManager.Player.ObjectTransform.LookAt()
        }
    }

    /// <summary>
    /// Resets the rotation of the player and the camera to zero
    /// </summary>
    public void RestartRotationCameraPlayer ()
    {
        // We set the rotation to zero
        m_RotationCamera = Vector2.zero;

        // We rotate the player on the X axis only
        Toolbox.Instance.GameManager.Player.ObjectTransform.eulerAngles = new Vector3(
            Toolbox.Instance.GameManager.Player.ObjectTransform.eulerAngles.x,
            m_RotationCamera.y,
            Toolbox.Instance.GameManager.Player.ObjectTransform.eulerAngles.z);
        // We then rotate the camera on the Y axis
        Camera.main.transform.eulerAngles = new Vector3(
            m_RotationCamera.x,
            Camera.main.transform.eulerAngles.y,
            Camera.main.transform.eulerAngles.z);
    }

    
}
