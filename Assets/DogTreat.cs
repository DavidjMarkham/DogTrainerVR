using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTreat : MonoBehaviour {
    public MeshRenderer treatModel;

    private Rigidbody rigidBody;
    private Vector3 throwStartPosition = new Vector3(0f, 0.5f, -0.8f);
    private Vector3 throwVelocity = new Vector3(0f, 2f, 1.0f);
    private Vector3 throwAngularVelocity = new Vector3(0f, 4f, 0f);
    private Vector3 defaultPosition = new Vector3(0f, 0.3f, -0.4f);
    private Vector3 highPosition = new Vector3(0f, 0.6f, -0.4f);
    private Vector3 lowPosition = new Vector3(0f, 0.1f, -0.4f);
    private Quaternion defaultRotation = new Quaternion(-90f,0f,0f,0f);    
    private bool isMovingToHighPosition = false;
    private bool isMovingToLowPosition = false;
    private float speed = .25f;
    private float positionThreshold = 0.05f;
    private System.Action callback;
    private bool isPlayerControlled = false;
    private bool isWaitingForHighPosition = false;
    private bool isWaitingForLowPosition = false;
    private float timeLeftInPosition = 0f;


    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isMovingToHighPosition)
        {
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, highPosition, step);
            if ((this.transform.position - highPosition).magnitude < positionThreshold)
            {
                this.isMovingToHighPosition = false;
            }
        }

        if (isMovingToLowPosition)
        {
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, lowPosition, step);
            if ((this.transform.position - lowPosition).magnitude < positionThreshold)
            {
                this.isMovingToLowPosition = false;
            }
        }

        if (isPlayerControlled)
        {
            // Tie height of treat to rotation of controller
            this.transform.position = new Vector3(0f, -1f * (OVRInput.GetLocalControllerRotation(OVRInput.GetActiveController()).x - 0.5f), -.4f);

            if (this.transform.position.y > 0.6f)
            {
                this.transform.position = new Vector3(0f, 0.6f, -.4f);
            }

            if (this.transform.position.y < 0.1f)
            {
                this.transform.position = new Vector3(0f, 0.1f, -.4f);
            }

            if(isWaitingForHighPosition && this.transform.position.y == 0.6f)
            {
                timeLeftInPosition -= Time.deltaTime;

                if (timeLeftInPosition <= 0f)
                {
                    isWaitingForHighPosition = false;
                    isPlayerControlled = false;
                    callback();
                }
            }

            if (isWaitingForLowPosition && this.transform.position.y == 0.1f)
            {
                timeLeftInPosition -= Time.deltaTime;

                if (timeLeftInPosition <= 0f)
                {
                    isWaitingForLowPosition = false;
                    isPlayerControlled = false;
                    callback();
                }
            }
        }
    }

    private void OnGUI()
    {

        
        
    }

    public void showTreat()
    {
        treatModel.enabled = true;
        this.rigidBody.useGravity = true;
    }

    public void hideTreat()
    {
        treatModel.enabled = false;        
    }

    public void throwTreat()
    {
        isPlayerControlled = false;
        isMovingToHighPosition = false;
        showTreat();

        this.transform.position = throwStartPosition;
        this.rigidBody.velocity = throwVelocity;
        this.rigidBody.angularVelocity = throwAngularVelocity;
    }

    public void moveTreatAboveHead()
    {
        treatModel.enabled = true;
        this.rigidBody.useGravity = false;
        this.transform.position = this.defaultPosition;
        this.transform.rotation = this.defaultRotation;
        this.isMovingToHighPosition = true;
    }

    // Give player control of the dog treat and let them position it
    public void WaitForHighPosition(System.Action inputCallback)
    {
        treatModel.enabled = true;
        this.rigidBody.useGravity = false;
        this.transform.position = this.defaultPosition;
        this.transform.rotation = this.defaultRotation;
        callback = inputCallback;
        isWaitingForHighPosition = true;
        isWaitingForLowPosition = false;
        isPlayerControlled = true;
        timeLeftInPosition = 1.5f;
    }

    // Give player control of the dog treat and let them position it
    public void WaitForLowPosition(System.Action inputCallback)
    {
        treatModel.enabled = true;
        this.rigidBody.useGravity = false;
        this.transform.position = this.defaultPosition;
        this.transform.rotation = this.defaultRotation;
        callback = inputCallback;
        isWaitingForLowPosition = true;
        isWaitingForHighPosition = false;
        isPlayerControlled = true;
        timeLeftInPosition = 1.5f;
    }
}
