using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public float speed = 1;
    public int score = 0;
    private Vector3 direction;
    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        Restart();
    }

    public void Restart()
    {
        transform.position = new Vector3(0, 0.75f, 0);
        direction = new Vector3();
        score = 0;
        UIGame.instance.scoreGame.text = "Score: " + score;
    }

    public void FixedUpdate()
    {
        rb.velocity = (direction * speed) + (Physics.gravity);
        if (transform.position.y < -1f) UIGame.instance.ShowGameOver(score);
    }

    public void Tap()
    {
        if (direction == Vector3.forward)
            direction = Vector3.right;
        else
            direction = Vector3.forward;
    }

    public void TakeCrystal(int scorePlus)
    {
        score +=scorePlus;
        UIGame.instance.scoreGame.text = "Score: " + score;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
