using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Powerup.PowerupType? powerupType = collision.gameObject.GetComponent<Powerup>()?.powerupType;

        if (powerupType == null)
        {
            return;
        }

        switch (powerupType)
        {
            case Powerup.PowerupType.SPEED:
                Debug.Log("Speed powerup collected");

                break;
            case Powerup.PowerupType.JUMP:
                Debug.Log("Jump powerup collected");
                break;
            case Powerup.PowerupType.INVINCIBILITY:
                Debug.Log("Invincibility powerup collected");
                break;
        }
    }
}
