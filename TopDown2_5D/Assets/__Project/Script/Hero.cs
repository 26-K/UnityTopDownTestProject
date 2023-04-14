using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PPD
{

    public class Hero : MonoBehaviour, IMoveable
    {
        private Vector3 moveDirection = Vector3.zero;
        [SerializeField] Rigidbody rgd;
        [SerializeField] float spd = 1.0f;
        // Update is called once per frame
        void Start()
        {
            InputControlBehaviour.MoveEvent += ((IMoveable)this).Move;
        }

        private void OnDestroy()
        {
            if (this != null)
            {
                InputControlBehaviour.MoveEvent -= ((IMoveable)this).Move;
            }
        }

        void Update()
        {
        }

        void FixedUpdate()
        {
            if (this.moveDirection != Vector3.zero)
            {
                this.Move();
            }
            this.moveDirection = Vector3.zero;
        }

        void Move()
        {
            this.rgd.velocity = Vector3.zero;
            this.rgd.MovePosition(this.transform.position + moveDirection.normalized * this.spd * Time.deltaTime);
        }

        void IMoveable.Move(Vector2 vector)
        {
            this.moveDirection = new Vector3(vector.x, 0, vector.y).normalized;
        }
    }
}