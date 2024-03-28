using UnityEngine;

public class DamageTextController : MonoBehaviour
{
    [SerializeField]
    private float timeUntilSelfDestroy = 2f;

    [SerializeField]
    private float moveSpeed = 5f;

    private void Awake() {
        Destroy(this.gameObject, timeUntilSelfDestroy);
    }

    private void Update() {
        this.transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }
}
