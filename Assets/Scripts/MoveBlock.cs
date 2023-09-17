using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{

    public float speed = 0;
    private Vector3 dir = Vector3.left;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
    transform.Translate(dir*speed*Time.deltaTime);

     if(transform.position.x <= 6){
          dir = Vector3.right;
     }else if(transform.position.x >= 9){
          dir = Vector3.left;
     }
    }
}
