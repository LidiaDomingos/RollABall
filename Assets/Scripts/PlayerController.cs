using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
     public float speed = 0;
     public TextMeshProUGUI countText;
     public TextMeshProUGUI countLife;
     public TextMeshProUGUI countTime;
     public GameObject winTextObject;
     public GameObject loseTextObject;
     public GameObject playAgainButtonObject;
     public GameObject player;
     public AudioSource getCube;
     public AudioSource loseLife;
     public AudioSource gameOver;
     public AudioSource win;

     private int count;
     private float time;
     private float immortalDuration = 1.0f; 
     private float blinkInterval = 0.2f; 
     private float blinkTimer = 0.0f; 
     private bool immortal;

     private int life;
     private Rigidbody rb;
     private float movementX;
     private float movementY;
     private bool endGame;
     private bool timeRunning;


     // Start is called before the first frame update
     void Start()
     {
          rb = GetComponent<Rigidbody>();
          count = 0;
          life = 5;
          time = 180;
          endGame = false;
          timeRunning = true;
          immortal = false;

          SetCountText();
          SetCountLife();
          SetCountTime();

          winTextObject.SetActive(false);
          loseTextObject.SetActive(false);
          playAgainButtonObject.SetActive(false);
     }

     void Update (){
          if (timeRunning){
               if (endGame){
                    playAgainButtonObject.SetActive(true);
                    timeRunning = false;
               }
               else if (time > 0){
                    time = time - Time.deltaTime;
               }
               else {
                    time = 0;
                    speed = 0;
                    timeRunning = false;
                    loseTextObject.SetActive(true);
                    playAgainButtonObject.SetActive(true);
                    endGame = true;
               }
               SetCountTime();
          }

          if (player.transform.position.y < -2.5){
               life = life - 1;
               loseLife.Play();
               SetCountLife();

               player.transform.position = new Vector3(-8, 1 , -8);
          }

     }

     public void RestartGame ()
     {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     }

     void OnMove(InputValue movementValue)
     {
          Vector2 movementVector = movementValue.Get<Vector2>();

          movementX = movementVector.x;
          movementY = movementVector.y;
     }

     void SetCountTime(){
          countTime.text = time.ToString("0") + " : 15 ";
     }

     void SetCountText()
     {
          countText.text = " x " + count.ToString();
          if (count >= 15){
               win.Play();
               winTextObject.SetActive(true);
               playAgainButtonObject.SetActive(true);
               speed = 0;
               endGame = true;
          }
     }

     void SetCountLife()
     {
          countLife.text = " x " + life.ToString();
          if (life == 0){
               gameOver.Play();
               loseTextObject.SetActive(true);
               playAgainButtonObject.SetActive(true);
               speed = 0;
               endGame = true;
          }
     }

     void FixedUpdate()
     {
          Vector3 movement = new Vector3(movementX, 0.0f, movementY);
          rb.AddForce(movement * speed);
     }

     private void OnTriggerEnter(Collider other)
     {
          if(other.gameObject.CompareTag("PickUp"))
          {
               other.gameObject.SetActive(false);
               getCube.Play();
               count = count + 1;

               SetCountText();


          }
     }
     
     private IEnumerator DisableImmortality()
     {
          while (blinkTimer < immortalDuration)
          {
               yield return new WaitForSeconds(blinkInterval);
               blinkTimer += blinkInterval;
          }

          immortal=false;
     }

     private void OnCollisionEnter(Collision other)
     {
          if(other.gameObject.CompareTag("Block"))
          {
               if (!endGame && !immortal ){
                    loseLife.Play();
                    blinkTimer = 0.0f;
                    immortal = true;
                    StartCoroutine(DisableImmortality());
                    life = life - 1;
                    SetCountLife();
               }
               
          }
     }

}
