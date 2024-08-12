using System;
using UnityEngine;

public class InputManager : MonoBehaviour {

    //Singleton pattern
    public static InputManager Instance;

    private TouchInputActions inputActions;

    //events to send the player about touch contact and touch ended
    public event EventHandler OnTouchStarted;
    public event EventHandler OnTouchEnded;
    public event EventHandler OnEscaePressed;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one instance of InputManager");

        inputActions = new TouchInputActions();
    }

    private void Start() {
        inputActions.Touch.Enable();

        inputActions.Touch.TouchContact.started += TouchContact_started;
        inputActions.Touch.TouchContact.canceled += TouchContact_canceled;
        inputActions.Touch.BackButton.performed += BackButton_performed;
    }

    private void BackButton_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnEscaePressed?.Invoke(this, EventArgs.Empty);
    }

    private void TouchContact_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnTouchEnded?.Invoke(this, EventArgs.Empty);
    }

    private void TouchContact_started(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnTouchStarted?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy() {
        //Dispose the input actions so a new one is not created

        inputActions.Touch.TouchContact.started -= TouchContact_started;
        inputActions.Touch.TouchContact.canceled -= TouchContact_canceled;

        inputActions.Touch.BackButton.performed += BackButton_performed;

        inputActions.Dispose();
    }
    
    public Vector2 GetTouchPosition() {
        return inputActions.Touch.TouchPosition.ReadValue<Vector2>();
    }

}
