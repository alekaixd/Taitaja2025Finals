using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Data;

public class PlayerController : MonoBehaviour
{
	public float accelerationRate = 5f; // Rate at which the player accelerates
	public float BaseSpeed;
	public float jumpForce; // Force applied when jumping
	public float levitationForce;
	public  int JumpCooldown, JumpCooldownLeft;
	public int LevitationCd, LevitationDurLeft;
	public int levitationDuration;
	public float Hor;
    public Vector2 Velocity;
	Animator animator;
	public int DeathCount;
	private Rigidbody2D rb;
	public bool isGrounded;
	public GameObject MainCamera,LastCheckpoint;


	void Start()
	{
		MainCamera = GameObject.Find("Main Camera");
		
		DontDestroyOnLoad(gameObject);
		rb = GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator>();
	}

	void FixedUpdate()
	{
        if (Input.GetButtonDown("hard reset"))
        {
			SceneManager.LoadScene("Main Menu");
			Destroy(GameObject.Find("Player"));
            Destroy(GameObject.Find("MUSIC MAN 2"));
            Destroy(GameObject.Find("Canvas"));
        }
        if (Input.GetButtonDown("reset"))
		{
			Death();
		}
		if ( JumpCooldownLeft != 0 )
		{ JumpCooldownLeft -= 1; }
		if ( LevitationDurLeft != 0 )
		{
			LevitationDurLeft -= 1;
			rb.velocity = new Vector2(rb.velocity.x,4);
		}
		else
		{ rb.gravityScale = 1; }

		// Jumping
		if (Input.GetButton("Jump") && isGrounded && JumpCooldownLeft == 0)
		{
			rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			isGrounded = false;
			JumpCooldownLeft = JumpCooldown;
		}
		if (Input.GetButton("Jump"))
		{
			rb.AddForce(Vector2.up);
		}

		Hor = Input.GetAxisRaw("Horizontal");
		if (Hor != 0)
		{
			rb.AddForce(new Vector2(Hor * accelerationRate * BaseSpeed, 0), ForceMode2D.Force);
		}
		else
		{
			rb.velocity -= new Vector2(rb.velocity.x / 5, 0);
			if (Mathf.Abs(rb.velocity.x) < 0.01)
			{
				rb.velocity = new Vector2 (0, rb.velocity.y);
			}
		}
		
		MainCamera.transform.localPosition = Vector3.MoveTowards(MainCamera.transform.localPosition, new Vector3 (rb.velocity.x/8, 0, -1), 0.25f);
		animator.SetFloat("Hor", rb.velocity.x);
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isGrounded = true;
		}
		else if (collision.gameObject.CompareTag("Spike"))
		{
			Death();

			Debug.Log("Death!");
		}
	}
	void Death()
	{
		rb.velocity = Vector2.zero;
		JumpCooldownLeft = 0;
		LevitationDurLeft = 0;
		DeathCount += 1;
		GameObject.Find("Deaths").GetComponent<TextMeshProUGUI>().text = "Deaths: " + DeathCount;
		gameObject.transform.position = LastCheckpoint.transform.position;
	}
	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isGrounded = true;
			LevitationDurLeft = 0;
		}
        if (collision.gameObject.CompareTag("LevitationObject"))
        {
            rb.gravityScale = -0.05f;
            LevitationDurLeft = LevitationCd;
        }
    }
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isGrounded = false;
		}
	}
	// Coroutine for levitation effect
	
	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.gameObject.CompareTag("Bouncer"))
		{
			rb.velocity = new Vector2(rb.velocity.x * -2, 2);
			Debug.Log("Bounce!");
		}
		if (collision.gameObject.CompareTag("Checkpoint"))
		{
			LastCheckpoint = collision.gameObject;
		}
	}
}

