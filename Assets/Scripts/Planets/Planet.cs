using System;
using UnityEngine;

public class Planet : MonoBehaviour {
    public enum State {
        Normal,
        Jump,
        Hook,
        Reverse,
    }

    [SerializeField] public State planetState;
    [SerializeField] private float surfaceSpeed = 3f;

    private SurfaceEffector2D surface;
    private float startSpeedValue;

    private void Awake() {
        surface = GetComponent<SurfaceEffector2D>();
    }

    private void Start() {
        startSpeedValue = surfaceSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            if (other.transform.position.x < transform.position.x) {
                surface.speed = startSpeedValue;
            }
            else {
                surface.speed = -startSpeedValue;
            }
        }
    }
}