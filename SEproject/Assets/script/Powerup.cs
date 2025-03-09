using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField] private float powerupDuration = 5f;
    [SerializeField] private float powerupStrength = 10f;

    public enum PowerupType
    {
        SPEED,
        JUMP,
        INVINCIBILITY
    }

    public PowerupType powerupType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            Debug.Log("Powerup collected");
            Destroy(gameObject);
        }
    }
}
