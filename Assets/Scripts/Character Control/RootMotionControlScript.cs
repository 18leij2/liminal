using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

//require some things the bot control needs
[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterInputController))]
public class RootMotionControlScript : MonoBehaviour
{
    public CanvasGroup hintCanvas;
    private bool canHint;

    public TextMeshProUGUI loreText;
    public Canvas loreCanvas;
    private bool canLore;
    private bool inLore;

    public float animationSpeed = 1f;
    public float rootMovementSpeed = 1f;
    public float rootTurnSpeed = 1f;

    public GameObject buttonObject;

    private Animator anim;
    private Rigidbody rbody;
    private CharacterInputController cinput;

    private Transform leftFoot;
    private Transform rightFoot;


    public GameObject buttonPressStandingSpot;
    public float buttonCloseEnoughForMatchDistance = 2f;
    public float buttonCloseEnoughForPressDistance = 0.22f;
    public float buttonCloseEnoughForPressAngleDegrees = 5f;
    public float initalMatchTargetsAnimTime = 0.25f;
    public float exitMatchTargetsAnimTime = 0.75f;

    public Transform cameraTransform;
    public float rotationSpeed = 0.8f;

    public CanvasGroup hintGroup;

    public string[] notes;

    // classic input system only polls in Update()
    // so must treat input events like discrete button presses as
    // "triggered" until consumed by FixedUpdate()...
    bool _inputActionFired = false;

    // ...however constant input measures like axes can just have most recent value
    // cached.
    float _inputForward = 0f;
    float _inputTurn = 0f;
    float _inputRight = 0f;

    private bool canPress = false;
    private bool canExit = false;

    public static event Action OpenDoors;

    public float speedMultiplier = 0.5f;
    public GameObject staminaBarObject;
    public Image staminaBar;
    public float stamina, maxStamina = 100f;

    public GameObject hotbarObject;
    public GameObject hotbarSlotsObject;
    public CanvasGroup inventoryGroup;

    public GameObject flashLight;
    public GameObject lights;


    //Useful if you implement jump in the future...
    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;

    private int groundContactCount = 0;

    public bool IsGrounded
    {
        get
        {
            return groundContactCount > 0;
        }
    }

    void Awake()
    {

        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found");

        rbody = GetComponent<Rigidbody>();

        if (rbody == null)
            Debug.Log("Rigid body could not be found");

        cinput = GetComponent<CharacterInputController>();
        if (cinput == null)
            Debug.Log("CharacterInput could not be found");
    }


    // Use this for initialization
    void Start()
    {
        //example of how to get access to certain limbs
        leftFoot = this.transform.Find("mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot");
        rightFoot = this.transform.Find("mixamorig:Hips/mixamorig:RightUpLeg/mixamorig:RightLeg/mixamorig:RightFoot");

        if (leftFoot == null || rightFoot == null)
            Debug.Log("One of the feet could not be found");

        // inventoryObject.SetActive(false);
        // hotbarObject.SetActive(true);
        // hotbarSlotsObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "noteclue")
        {
            canHint = true;
            hintGroup.alpha = 1.0f;
        }

        if (other.gameObject.tag == "lorenote")
        {
            hintGroup.alpha = 1f;
            canLore = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "noteclue")
        {
            canHint = false;
            hintGroup.alpha = 0f;
            hintCanvas.alpha = 0f;
        }

        if (other.gameObject.tag == "lorenote")
        {
            inLore = false;
            canLore = false;
            hintGroup.alpha = 0f;
            loreCanvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (cinput.enabled)
        {
            _inputForward = cinput.Forward;
            _inputTurn = cinput.Turn;
            _inputRight = cinput.Right;

            // Note that we don't overwrite a true value already stored
            // Is only cleared to false in FixedUpdate()
            // This makes certain that the action is handled!
            _inputActionFired = _inputActionFired || cinput.Action;

        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (true) //(vertical > 0)
            {
                staminaBarObject.SetActive(true);
                if (stamina > 0.1f)
                {
                    stamina -= 0.1f;
                    staminaBar.fillAmount = stamina / maxStamina;
                    speedMultiplier = 1f;
                }
                else
                {
                    speedMultiplier = 0.5f;
                }
            }
        }
        else
        {
            speedMultiplier = 0.5f;
            if (stamina + 0.1f <= maxStamina)
            {
                stamina += 0.1f;
                staminaBar.fillAmount = stamina / maxStamina;
            }
            else
            {
                staminaBarObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashLight.activeSelf == false)
            {
                flashLight.SetActive(true);
            }
            else if (flashLight.activeSelf == true)
            {
                flashLight.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canPress)
            {
                lights.SetActive(false);
                canExit = true;
                OpenDoors?.Invoke();

                hintGroup.alpha = 0f;
                hintGroup.interactable = false;
                hintGroup.blocksRaycasts = false;
            }

            if (canExit && false)
            {
                transform.position = new Vector3(0, 0, 0);
                lights.SetActive(true);
                canPress = false;
                canExit = false;
            }

            if (canHint)
            {
                hintCanvas.alpha = 1f;
                hintGroup.alpha = 0f;
            }

            if (canLore && !inLore)
            {
                inLore = true;
                loreCanvas.gameObject.SetActive(true);
                int index = UnityEngine.Random.Range(0, notes.Length);
                loreText.text = notes[index];
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventoryGroup.alpha == 0f) {
                inventoryGroup.alpha = 1f;
                inventoryGroup.interactable = true;
                inventoryGroup.blocksRaycasts = true;
                // hotbarObject.SetActive(false);
                // hotbarSlotsObject.transform.localPosition = new Vector3(0f, -90f, 0f);
                
            } else {
                inventoryGroup.alpha = 0f;
                inventoryGroup.interactable = false;
                inventoryGroup.blocksRaycasts = false;
                hotbarObject.SetActive(true);
                // hotbarSlotsObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
        }
    }


    void FixedUpdate()
    {

        bool doButtonPress = false;
        bool doMatchToButtonPress = false;

        //onCollisionXXX() doesn't always work for checking if the character is grounded from a playability perspective
        //Uneven terrain can cause the player to become technically airborne, but so close the player thinks they're touching ground.
        //Therefore, an additional raycast approach is used to check for close ground.
        //This is good for allowing player to jump and not be frustrated that the jump button doesn't
        //work
        bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

        float buttonDistance = float.MaxValue;
        float buttonAngleDegrees = float.MaxValue;

        if (buttonPressStandingSpot != null)
        {
            buttonDistance = Vector3.Distance(transform.position, buttonPressStandingSpot.transform.position);
            buttonAngleDegrees = Quaternion.Angle(transform.rotation, buttonPressStandingSpot.transform.rotation);
        }

        if (_inputActionFired)
        {
            _inputActionFired = false; // clear the input event that came from Update()

            Debug.Log("Action pressed");

            if (buttonDistance <= buttonCloseEnoughForMatchDistance)
            {
                if (buttonDistance <= buttonCloseEnoughForPressDistance &&
                    buttonAngleDegrees <= buttonCloseEnoughForPressAngleDegrees)
                {
                    Debug.Log("Button press initiated");

                    doButtonPress = true;

                }
                else
                {
                    // TODO UNCOMMENT THESE LINES FOR TARGET MATCHING
                    Debug.Log("match to button initiated");
                    doMatchToButtonPress = true;
                }

            }
        }


        // get info about current animation
        var animState = anim.GetCurrentAnimatorStateInfo(0);
        // If the transition to button press has been initiated then we want
        // to correct the character position to the correct place
        if (animState.IsName("MatchToButtonPress")
        && !anim.IsInTransition(0) && !anim.isMatchingTarget)
        {
            if (buttonPressStandingSpot != null)
            {
                Debug.Log("Target matching correction started");
                initalMatchTargetsAnimTime = animState.normalizedTime;
                var t = buttonPressStandingSpot.transform;
                anim.MatchTarget(t.position, t.rotation, AvatarTarget.Root,
                new MatchTargetWeightMask(new Vector3(1f, 0f, 1f),
                1f),
                initalMatchTargetsAnimTime,
                exitMatchTargetsAnimTime);
            }
        }



        // anim.SetFloat("velX", _inputTurn);
        if (Mathf.Abs(_inputRight) > 0f || Mathf.Abs(_inputForward) > 0f)
        {
            anim.SetBool("Movement", true);
        }
        else
        {
            anim.SetBool("Movement", false);
        }

        if (_inputForward >= 0f)
        {
            anim.SetBool("Forward", true);

            anim.SetFloat("velX", (_inputRight * speedMultiplier));
            anim.SetFloat("velY", (_inputForward * speedMultiplier));
        }
        else
        {
            anim.SetBool("Forward", false);

            anim.SetFloat("velX", _inputRight);
            anim.SetFloat("velY", _inputForward);
        }

        
        anim.SetBool("isFalling", !isGrounded);
        
        // anim.SetBool("doButtonPress", doButtonPress);
        // anim.SetBool("matchToButtonPress", doMatchToButtonPress);

        anim.speed = animationSpeed;

        // rotate towards camera direction
        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
        this.gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }


    //This is a physics callback
    void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.gameObject.tag == "ground")
        {

            ++groundContactCount;

            // Generate an event that might play a sound, generate a particle effect, etc.
            EventManager.TriggerEvent<PlayerLandsEvent, Vector3, float>(collision.contacts[0].point, collision.impulse.magnitude);

        }

        if (collision.gameObject.tag == "Pressure")
        {
            canPress = true;
            hintGroup.alpha = 1f;
            hintGroup.interactable = true;
            hintGroup.blocksRaycasts = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.transform.gameObject.tag == "ground")
        {
            --groundContactCount;
        }

        if (collision.gameObject.tag == "Pressure")
        {
            canPress = false;
            hintGroup.alpha = 0f;
            hintGroup.interactable = false;
            hintGroup.blocksRaycasts = false;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (anim)
        {
            AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo(0);
            if (astate.IsName("ButtonPress"))
            {
                float buttonWeight = anim.GetFloat("buttonClose");
                // Set the look target position, if one has been assigned
                if (buttonObject != null)
                {
                    anim.SetLookAtWeight(buttonWeight);
                    anim.SetLookAtPosition(buttonObject.transform.position);
                    anim.SetIKPositionWeight(AvatarIKGoal.RightHand, buttonWeight);
                    anim.SetIKPosition(AvatarIKGoal.RightHand,
                    buttonObject.transform.position);
                }
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetLookAtWeight(0);
            }
        }
    }

    void OnAnimatorMove()
    {

        Vector3 newRootPosition;
        Quaternion newRootRotation;

        bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

        if (isGrounded)
        {
            //use root motion as is if on the ground		
            newRootPosition = anim.rootPosition;
        }
        else
        {
            //Simple trick to keep model from climbing other rigidbodies that aren't the ground
            newRootPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z);
        }

        //use rotational root motion as is
        newRootRotation = anim.rootRotation;

        //TODO Here, you could scale the difference in position and rotation to make the character go faster or slower
        newRootPosition = Vector3.LerpUnclamped(this.transform.position, newRootPosition, rootMovementSpeed);
        newRootRotation = Quaternion.LerpUnclamped(this.transform.rotation, newRootRotation, rootTurnSpeed);

        // old way
        //this.transform.position = newRootPosition;
        //this.transform.rotation = newRootRotation;

        rbody.MovePosition(newRootPosition);
        rbody.MoveRotation(newRootRotation);
    }




}
