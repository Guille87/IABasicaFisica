using UnityEngine;

public class FireShell : MonoBehaviour {
    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;
    public Transform turretBase;

    private float speed = 15.0f;
    private float rotSpeed = 5.0f;
    private float moveSpeed = 1.0f;

    static float delayReset = 0.2f;
    float delay = delayReset;

    void CreateBullet() {
        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        shell.GetComponent<Rigidbody>().linearVelocity = speed * turretBase.forward;
    }

    float? RotateTurret() {
        float? angle = CalculateAngle(true);

        if (angle != null) {
            turretBase.localEulerAngles = new Vector3(360.0f - (float)angle, 0.0f, 0.0f);
        }
        return angle;
    }

    float? CalculateAngle(bool low) {
        Vector3 targetDir = enemy.transform.position - transform.position;
        float y = targetDir.y;
        targetDir.y = 0.0f;
        float x = targetDir.magnitude - 1.0f;
        float gravity = 9.8f;
        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0.0f) {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low) return Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg;
            else return Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg;
        } else
            return null;
    }

    void Update() {
        delay -= Time.deltaTime;
        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
        float? angle = RotateTurret();

        if (angle != null && delay <= 0.0f) {

            CreateBullet();
            delay = delayReset;
        } else {

            transform.Translate(0.0f, 0.0f, Time.deltaTime * moveSpeed);
        }
    }
}
