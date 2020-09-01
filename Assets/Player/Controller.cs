using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private float damping = 4;
        [SerializeField] private new Camera camera = default;

        private Rigidbody2D _rigidbody;
        private Vector2 _direction;
        private Vector2 _mouse;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce((_direction * speed - _rigidbody.velocity) / Time.fixedDeltaTime / damping);
        }

        private void Update()
        {
            var mousePosition = camera.ScreenToWorldPoint(_mouse);
            var playerPosition = transform.position;

            transform.rotation = Quaternion.LookRotation(
                Vector3.forward,
                mousePosition - playerPosition
            );
        }

        public void OnMove(InputValue value)
        {
            _direction = value.Get<Vector2>();
        }

        public void OnLook(InputValue value)
        {
            _mouse = value.Get<Vector2>();
        }
    }
}