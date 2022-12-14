using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CrossPlatformInputController : MonoBehaviour
{
    public enum InputMethod
    { 
        KEYBOARD,
        TOUCH
    }

    public static CrossPlatformInputController instance; // Singleton
    
    /// <summary>
    /// Cross platform input method
    /// </summary>
    public InputMethod inputMethod;

    // Input values
    public float horizontalInput;
    public float brakeInput;
    public float reverseInput;
    public float throttleInput;
    public bool boostInput;

    private bool isThrottling;
    private bool isBraking;
    private bool isBoosting;

    public TextMeshProUGUI switchInputsText;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }



    // Update is called once per frame
    void Update()
    {
        if (CarController.instance.userInput)
        {
            switch (inputMethod)
            {
                case InputMethod.KEYBOARD:
                    HandlePCInputs();
                    
                    break;
                case InputMethod.TOUCH:
                    HandleMobileInputs();
                    
                    break;
                default:
                    HandlePCInputs();
                    break;
            }
        }

        if (inputMethod == InputMethod.KEYBOARD)
        {
            switchInputsText.text = "Switch Input Method to MOBILE";
        }
        else
        {
            switchInputsText.text = "Switch Input Method to PC";
        }
        
    }

    /// <summary>
    /// Checks for player input on pc and sets values
    /// </summary>
    private void HandlePCInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        throttleInput = Input.GetKey(KeyCode.W) ? 1 : 0;
        throttleInput = Input.GetKey(KeyCode.S) ? -1 : throttleInput;
        brakeInput = Input.GetKey(KeyCode.Space) ? 1 : 0;
        boostInput = Input.GetKey(KeyCode.LeftShift);
    }

    /// <summary>
    /// Checks for player input on mobile devices and sets values
    /// </summary>
    private void HandleMobileInputs()
    {
        horizontalInput = SteeringWheel.instance.Value;
        throttleInput = isThrottling ? 1 : 0;
        brakeInput = isBraking ? 1 : 0;
        boostInput = isBoosting;
        // Boost input is changed directly via button
        
        // throttleInput = Input.GetKey(KeyCode.S) ? -1 : throttleInput;
    }

    public void SwitchInputMethod()
    {
        if (inputMethod == InputMethod.TOUCH)
            inputMethod = InputMethod.KEYBOARD;
        else
            inputMethod = InputMethod.TOUCH;
    }

    public void ThrottleButtonDown()
    {
        isThrottling = true;
    }

    public void ThrottleButtonUp()
    { 
        isThrottling = false;
    }

    public void BrakeButtonDown()
    {
        isBraking = true;
    }

    public void BrakeButtonUp()
    {
        isBraking = false;
    }

    public void BoostButtonDown()
    {
        isBoosting = true;
        isThrottling = true;
    }

    public void BoostButtonUp()
    {
        isBoosting = false;
        isThrottling = false;
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
