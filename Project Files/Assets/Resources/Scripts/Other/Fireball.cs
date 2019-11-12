using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 8f;
    public float lifeDuration = 2f;
    public int damage = 15;

    private float lifeTimer;


    private void OnEnable()
    {
        lifeTimer = lifeDuration;
    }
    // Update is called once per frame
    void Update()
    {

        // make the projectile move
        transform.position += transform.forward * Time.deltaTime;

        // check if the bullet should be destroyed
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }

    }
}
