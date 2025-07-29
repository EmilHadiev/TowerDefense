using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderRotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        //transform.RotateAround(Vector3.left, _rotateSpeed * Time.deltaTime);
        transform.Rotate(Vector3.left, _rotateSpeed * Time.deltaTime);
    }
}