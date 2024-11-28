using UnityEngine;

public class SecondsUpdate : MonoBehaviour
{
    float timeStartOffset = 0;
    bool gotStartTime = false;
    public float speed = 0.5f;

    void Update()
    {
        if (!gotStartTime)
        {
            timeStartOffset = Time.realtimeSinceStartup;
            gotStartTime = true;
        }
        transform.position = new Vector3(transform.position.x,
                                              transform.position.y,
                                              (Time.realtimeSinceStartup - timeStartOffset) * speed);
    }
}
