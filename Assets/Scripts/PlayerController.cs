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
     public GameObject winTextObject;
     public GameObject loseTextObject;
     public GameObject playAgainButtonObject;


     private int count;
     private int life;
     private Rigidbody rb;
     private float movementX;
     private float movementY;
     private bool endGame;

     // Start is called before the first frame update
     void Start()
     {
          rb = GetComponent<Rigidbody>();
          count = 0;
          life = 5;
          endGame = false;

          SetCountText();
          SetCountLife();
          winTextObject.SetActive(false);
          loseTextObject.SetActive(false);
          playAgainButtonObject.SetActive(false);
     }

     public void RestartGame ()
     {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     }

     public void GoToMenu ()
     {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
     }

     void OnMove(InputValue movementValue)
     {
          Vector2 movementVector = movementValue.Get<Vector2>();

          movementX = movementVector.x;
          movementY = movementVector.y;
     }


     void SetCountText()
     {
          countText.text = "Count: " + count.ToString();
          if (count >= 10){
               winTextObject.SetActive(true);
               playAgainButtonObject.SetActive(true);
               speed = 0;
               endGame = true;
          }
     }

     void SetCountLife()
     {
          countLife.text = "Life: " + life.ToString();
          if (life == 0){
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
               count = count + 1;

               SetCountText();


          }
     }

     private void OnCollisionEnter(Collision other)
     {
          if(other.gameObject.CompareTag("Block"))
          {
               if (endGame == false){
                    life = life - 1;
                    SetCountLife();
               }
               

          }
     }

}
