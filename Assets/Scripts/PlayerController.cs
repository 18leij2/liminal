using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public float groundCheckDist = 1.2f;
    public LayerMask groundLayer;
    public float jumpForce = 5f;
    public GameObject flashLight;
    public GameObject lights;

    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float movementSpeed = 10f;
    private Rigidbody rb;
    private bool canPress = false;
    private bool canExit = false;

    public static event Action OpenDoors;
    
    public GameObject staminaBarObject;
    public Image staminaBar;
    public float stamina, maxStamina = 100f;

    public GameObject hotbarObject;
    public GameObject inventoryObject;
    public GameObject hotbarSlotsObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inventoryObject.SetActive(false);
        hotbarObject.SetActive(true);
        hotbarSlotsObject.SetActive(true);
        movementSpeed = 10f;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        transform.Translate(horizontal, 0, vertical);

        // Debugging: Draw the ray in Scene view
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDist, isGrounded ? Color.green : Color.red);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Ground check using Raycast
            isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDist, groundLayer);
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }        
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (vertical > 0) {
                staminaBarObject.SetActive(true);
                if (stamina > 0.1f) {
                    stamina -= 0.1f;
                    staminaBar.fillAmount = stamina / maxStamina;
                    movementSpeed = 20f;
                } else {
                    movementSpeed = 10f;
                }
            }
        }
        else
        {
            
            movementSpeed = 10f;
            if (stamina + 0.1f <= maxStamina) {
                stamina += 0.1f;
                staminaBar.fillAmount = stamina / maxStamina;
            } else {
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
            }    

            if (canExit && false)
            {
                transform.position = new Vector3(0, 0, 0);
                lights.SetActive(true);
                canPress = false;
                canExit = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (inventoryObject.activeSelf) {
                inventoryObject.SetActive(false);
                hotbarObject.SetActive(true);
                // hotbarSlotsObject.transform.localPosition = new Vector3(0f, -120f, 0f);
            } else {
                inventoryObject.SetActive(true);
                hotbarObject.SetActive(false);
                // hotbarSlotsObject.transform.localPosition = new Vector3(0f, -90f, 0f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pressure")
        {
            canPress = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Pressure")
        {
            canPress = false;
        }
    }
}

