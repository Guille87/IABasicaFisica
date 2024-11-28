using UnityEngine;

public class AIShell : MonoBehaviour {

    public GameObject explosion;
    Rigidbody rb;

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "tank") {
            Debug.Log("Hit tank");
            GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(gameObject);
        }
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        transform.forward = rb.linearVelocity;
    }
}
