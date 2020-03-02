using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
    private KeyCode waitingForKeyCode = KeyCode.None;
    private System.Action callback;
    private bool waitingForTouchpad = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {        
        if (waitingForTouchpad)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
            {
                callback();
                waitingForKeyCode = KeyCode.None;
                waitingForTouchpad = false;
            }
            /*
            // is player using a controller?
            if (OVRInput.GetActiveController() == OVRInput.Controller.LTrackedRemote ||
                OVRInput.GetActiveController() == OVRInput.Controller.RTrackedRemote)
            {
                // yes, are they touching the touchpad?
                if (OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
                {
                    // yes, let's require an actual click rather than just a touch.
                    if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
                    {
                        // button is depressed, handle the touch.
                        Vector2 touchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
                        callback();
                        waitingForKeyCode = KeyCode.None;
                        waitingForTouchpad = false;
                    }
                }
            }
            else if (OVRInput.Get(OVRInput.Touch.PrimaryTouchpad)) // finger on HMD pad?
            {
                // not using controller, same behavior as before.
                Vector2 touchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
                callback();
                waitingForKeyCode = KeyCode.None;
                waitingForTouchpad = false;
            }*/
        }


    }

    void OnGUI()
    {
        if (waitingForKeyCode != KeyCode.None && callback!=null) {
            Event e = Event.current;
            // Check for keyboard input
            if (e.isKey && e.keyCode == waitingForKeyCode)
            {                
                waitingForKeyCode = KeyCode.None;
                waitingForTouchpad = false;
                System.Action tempCallback  = callback;
                callback = null;
                tempCallback();                
            }
        }


    }

    // Call a callback when a certain input from the user has been received
    public void WaitForKeyboardInput(KeyCode inputKeyCode, System.Action inputCallback)
    {
        waitingForKeyCode = inputKeyCode;
        callback = inputCallback;
    }

    // Call a callback when a certain input from the user has been received
    public void WaitForTouchpadClick(System.Action inputCallback)
    {        
        callback = inputCallback;
        waitingForTouchpad = true;
    }
}
