using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    public float damage = 1.0f;
    Vector2 direction;
    public Vector2 Direction
    {
        set
        {
            direction = value.normalized;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // this.Direction = new Vector2(10, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" || collision.tag == "Enemy")
        {
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
