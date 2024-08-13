using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BouncePadController : MonoBehaviour
{
    // Jumping SFX
    [SerializeField] private AudioSource jumpSFX;
    [SerializeField] private AudioSource bounceSFX;

    // Charging Bar UI
    public GameObject chargingBarPrefab;
    private GameObject chargingBar;

    // Control the charging of the bounce pad
    private float baseJumpForce = 8;
    private float chargeRate = 19f;
    private float maxCharge = 6;
    [SerializeField] private float charge;

    // Check if this bounce pad has already been used
    private bool hasBounce;

    // Check if the player is inside collider
    private bool isInside;

    private GameObject player;

    private Animator animator;
    private GameManager gameManager;

    // Instruction Text for first use
    public TextMeshProUGUI instructionText;

    // Start is called before the first frame update
    void Start()
    {
        hasBounce = false;
        isInside = false;
        charge = 0;
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
       
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if(gameManager.hasShown)
        {
            instructionText.gameObject.SetActive(false);
        }
        else
        {
            instructionText.gameObject.SetActive(true);
            gameManager.hasShown = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Press and charge the bounce pad when hold down space
        if ((Input.GetButton("Jump") || Input.GetMouseButton(0)) && !hasBounce && isInside)
        {
            animator.SetBool("isPressed", true);
            if (!chargingBar.activeSelf)
            {
                chargingBar.SetActive(true);
            }

            if (charge < maxCharge) // charging
            {
                charge += (chargeRate * Time.deltaTime);
            }
            else // If the charge is full, hold it till key up
            {
                charge = maxCharge;
                chargingBar.GetComponent<Animator>().SetBool("isFull", true);
            }
        }

        if ((Input.GetButtonUp("Jump") || Input.GetMouseButtonUp(0)) && isInside && !hasBounce)
        {
            float jumpForce = baseJumpForce * charge;
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            player.GetComponent<PlayerController>().jumpCount = player.GetComponent<PlayerController>().maxJumpCount;
            chargingBar.SetActive(false);
            hasBounce = true;
            animator.SetBool("isPressed", false);   
            jumpSFX.Play();
            bounceSFX.Play();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInside = true;
            // Create the charging bar for this instance of bounce pad
            chargingBar = Instantiate(chargingBarPrefab, new Vector3(-5, -4, 3), chargingBarPrefab.transform.rotation);
            chargingBar.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInside = false;
            Destroy(chargingBar);
        }
    }
}
