using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPikachu : MonoBehaviour
{
    private float speed;
    private FixedJoystick fixedJoystick;
    private Rigidbody rigidBody;
    private Animator animator;
    private Button Dance;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize your speed or any other setup if needed
        speed = 0.5f;
        // Get the Animator component
        animator = GetComponent<Animator>();
        // Find the Dance button by its name
        Dance = GameObject.Find("Dance").GetComponent<Button>();

        // Add a listener to the Dance button
        Dance.onClick.AddListener(StartDanceAnimation);
    }

    // Update is called once per frame
    private void OnEnable()
    {
        fixedJoystick = FindObjectOfType<FixedJoystick>(); // FixedJoystick, not fixedJoystick
        rigidBody = gameObject.GetComponent<Rigidbody>(); // Rigidbody, not rigidBody
    }

    private void FixedUpdate()
    {
        float xVal = fixedJoystick.Horizontal;
        float yVal = fixedJoystick.Vertical;

        Vector3 movement = new Vector3(xVal, 0, yVal);
        rigidBody.velocity = movement * speed;

        if (xVal != 0 || yVal != 0) // Use || instead of &&
        {
            //animator.ResetTrigger("IsDance");
            // Set the IsWalking parameter to trigger the walking animation
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdle", false); // Ensure that IsIdle is set to false when walking
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(xVal, yVal) * Mathf.Rad2Deg, transform.eulerAngles.z);
        }
        else
        {
            // If not walking, set the IsWalking trigger to false
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsWalking", false);
        }
    }
    private void StartDanceAnimation()
    {
        // Check if the dance animation is not already playing
        //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
        {
            // Trigger the dance animation
            animator.SetTrigger("IsDance");
        }
    }
}
