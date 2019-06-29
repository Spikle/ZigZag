using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smooth = 5f;

    private Vector3 offset;

    public void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.fixedDeltaTime * smooth);
    }
}
