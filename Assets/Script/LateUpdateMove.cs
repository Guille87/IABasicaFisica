using UnityEngine;

public class LateUpdateMove : MonoBehaviour
{
    public float speed = 0.5f;
    void LateUpdate()
    {
        this.transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
