using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCircleBlock : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        // Spin the object around the target at 20 degrees/second.
        transform.RotateAround(target.transform.position, Vector3.up, 180 * Time.deltaTime);
    }
}
