using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAI : MonoBehaviour {
    public GameObject dog;
    public Animator animationController;
    
    private float speed = .25f;
    private bool isWalkingTowardFarAway = false;
    private bool isWalkingTowardClose = false;
    private Vector3 farPosition = new Vector3(0f,0f,1.8f);
    private Vector3 closePosition = Vector3.zero;
    private Vector3 closeRotation = new Vector3(0f, 180f, 0f);
    private float positionThreshold = 0.025f;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if(isWalkingTowardFarAway)
        {
            float step = speed * Time.deltaTime;
            dog.transform.position = Vector3.MoveTowards(dog.transform.position, farPosition, step);

            if ((dog.transform.position - farPosition).magnitude < positionThreshold)
            {
                animationController.SetFloat("Move", 0.0f);
                isWalkingTowardFarAway = false;
            }
        }
        if (isWalkingTowardClose)
        {
            float step = speed * Time.deltaTime;
            dog.transform.position = Vector3.MoveTowards(dog.transform.position, closePosition, step);
            if((dog.transform.position - closePosition).magnitude < positionThreshold)
            {
                animationController.SetFloat("Move", 0.0f);
                isWalkingTowardClose = false;
            }
        }
    }

    public void PlayIdleFarAway()
    {
        isWalkingTowardFarAway = true;
    }

    public void PlayComeCloser()
    {
        animationController.Play("FrenchieWalk");
        animationController.SetFloat("Move", 2.0f);
        isWalkingTowardClose = true;
    }
}
