using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour
{
	public bool IsGonnaDie;
	public float speed;
	private Rigidbody2D rb2d;

	//private bool isFacingRight = true;  Tämä on pala vanhaa koodia, jota nykyinen (ja parempi) Dash ei tarvitse enää, tosin tätä saatetaan tarvita vielä animaatio työssä joten ÄLÄ POISTA!
	private float horizontal;
	private float vertical;

	private bool canDash = true;
	private bool isDashing;
	private float DashingPower = 24f;
	private float dashingTime = 0.2f;
	private float dashingCooldown = 1f;


	[SerializeField] private TrailRenderer tr; //dashauksen taakse tulevan viivan porttaamiseksi
	[SerializeField] private GrabObjects grabObjects;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (isDashing) //Estää sen ettei pelaaja liiku samaan aikaan kun dashaa
		{
			return;
		}

		horizontal = Input.GetAxis("Horizontal");
		gameObject.GetComponent<Animator>().SetFloat("MoveX", horizontal);
		vertical = Input.GetAxis("Vertical");
		gameObject.GetComponent<Animator>().SetFloat("MoveY", vertical);

		rb2d.velocity = new Vector2(horizontal * speed, vertical * speed);

		if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
		{
			StartCoroutine(Dash());
		}

		// Apply movement to player's rigidbody
		rb2d.MovePosition(rb2d.position + rb2d.velocity * Time.deltaTime);

		//Flip();

	}
	//Flip oli koodia joka "flippasi" pelattavan hahmon ympäri jos sattui toiseen suuntaan katsomaan. Myöhemmin kun koko Dash koodi uusittiin niin tämä muuttui vähän hyödyttömäksi

	//Mutta on tärkeä huomata että tätä saatetaan vielä tarvita mm. animaatiotyössä, joten ÄLÄ POISTA!!!!!!!!!!!!!!!!

	/*private void Flip() 
	{
		if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
		{
			Vector3 localScale = transform.localScale;
			isFacingRight = !isFacingRight;
			localScale.x *= -1f;
			transform.localScale = localScale;
		}

	}*/

	private IEnumerator Dash()
	{
		var sky = GameObject.Find("Skycollider");				//Hae taivas tilemap
		sky.GetComponent<Collider2D>().isTrigger = true;//Muuta taivas collideristä triggeriksi
		canDash = false; //Varmistaa ettei Dashayksen aikana voi dashata
		isDashing = true; //Aika selkeä

		//rb2d.velocity = new Vector2(transform.localScale.x * DashingPower, 0f);
		rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * DashingPower;

		tr.emitting = true; //Luo taakse taaksesi semmoseine hienon vanan
		yield return new WaitForSeconds(dashingTime);
		tr.emitting = false; //poistaa vanan
		isDashing = false; //Aika selkeä
		sky.GetComponent<Collider2D>().isTrigger = false;//Muuta taivas triggeristä collideriksi
		if (IsGonnaDie)
        {
			var current = SceneManager.GetActiveScene();
			SceneManager.SetActiveScene(current);
        }
		yield return new WaitForSeconds(dashingCooldown); //Cooldown dashiin
		
		canDash = true;
		


	}
}

