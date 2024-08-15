using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] PlayerGrapple playerGrapple; //Link to player's grapple ability. Used to check that player is not grappling when dash is attempted.

    [SerializeField] private float dashCooldown; //Minimum value between dashes
    private float dashCooldownTimer; //Constantly updated time to check if dash is available

    [SerializeField] private float doubleTapInterval;
    private float doubleTapTimer;

    [SerializeField] private float dashForce;

    [SerializeField] private float dashTime;

    [SerializeField] Rigidbody playerRB;
    [SerializeField] Transform playerRotation;

    [SerializeField] Slider dashSlider;

    private bool dashing = false;
    private Vector3 dashDirection; 

    enum ButtonPressed
    {
        Forward, Back, Left, Right, Null //Including "Null" value if nothing has been pressed in a particular frame
    }

    [Space(10)]
    [SerializeField] ButtonPressed lastPressed;

    // Start is called before the first frame update
    void Start()
    {
        dashCooldownTimer = dashCooldown; //Make dash instantly available
    }

    // Update is called once per frame
    void Update()
    {
        dashCooldownTimer += Time.deltaTime;
        doubleTapTimer += Time.deltaTime;
        UpdateDashSlider();

        UserInput();
    }

    private void FixedUpdate()
    {
        if(dashing)
        {
            playerRB.position += dashForce * Time.fixedDeltaTime * dashDirection; //Adjusting position directly has the issue of phasing through objects. Patched with Collision Event
        }
    }

    private IEnumerator DashLifetime(ButtonPressed direction)
    {
        dashCooldownTimer = 0; //Reset timer
        Debug.Log("Dashing");

        playerRB.velocity = Vector3.zero; //Stop player movement before assigning new force

        Vector3 forceDirection = Vector3.zero;

        switch (direction)
        {
            case ButtonPressed.Forward:
                forceDirection = Vector3.forward;
                break;
            case ButtonPressed.Back:
                forceDirection = Vector3.back;
                break;
            case ButtonPressed.Left:
                forceDirection = Vector3.left;
                break;
            case ButtonPressed.Right:
                forceDirection = Vector3.right;
                break;
        }

        dashing = true;
        dashDirection = playerRotation.localRotation * forceDirection;
        playerRB.useGravity = false; //Disable gravity to keep constant direction
        
        yield return new WaitForSeconds(dashTime);
        dashing = false;
        playerRB.useGravity = true;
    }

    void UserInput()
    {

        ButtonPressed nowPressed = ButtonPressed.Null;
        if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)
        {
            // forward
            nowPressed = ButtonPressed.Forward;
        }
        if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)
        {
            // back
            nowPressed = ButtonPressed.Back;
        }
        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            // right
            nowPressed = ButtonPressed.Right;
        }
        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            // left
            nowPressed = ButtonPressed.Left;
        }

        if(nowPressed == lastPressed)
        {
            if (doubleTapTimer <= doubleTapInterval) //If within dashing time window
            {
                if (dashCooldownTimer >= dashCooldown) //If dash has cooled down
                {
                    StartCoroutine(DashLifetime(nowPressed));
                }
                else
                    Debug.Log("Cooling down - " + (dashCooldown - dashCooldownTimer) + "s left");
            }
        }

        if(nowPressed != ButtonPressed.Null)
        {
            //If a key has been pressed
            lastPressed = nowPressed; //Save direction pressed
            doubleTapTimer = 0; //Reset timer
        }
    }

    void UpdateDashSlider()
    {
        if (dashCooldownTimer >= dashCooldown)
        {
            dashSlider.gameObject.SetActive(false);
        }
        else
        {
            dashSlider.value = (1-(dashCooldownTimer /dashCooldown));
            dashSlider.gameObject.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision) //Stay instead of enter in case player is already touching collider
    {
        if (dashing)
        {
            //Possibly check layer here?
            dashing = false;
        }
    }

}
