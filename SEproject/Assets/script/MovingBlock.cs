using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2.0f;

    private float t = 0;
    private bool movingToB = true;

    void Update()
    {
        // Move towards point B
        if (movingToB)
        {
            t += Time.deltaTime * speed;
            if (t >= 1)
            {
                t = 1;
                movingToB = false;
            }
        }
        // Move back towards point A
        else
        {
            t -= Time.deltaTime * speed;
            if (t <= 0)
            {
                t = 0;
                movingToB = true;
            }
        }

        // Smooth movement between A and B
        transform.position = Vector3.Lerp(pointA, pointB, t);
    }
}
