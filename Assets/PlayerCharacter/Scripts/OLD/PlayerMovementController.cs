using System.Collections;
using UnityEngine;

namespace CreatingCharacters.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] protected float movementSpeed = 5f;
        [SerializeField] protected float jumpForce = 5f;
        [SerializeField] protected float mass = 1f;
        [SerializeField] protected float damping = 5f;
        //[SerializeField] protected float jumpDelay = 0.2f;
        //[SerializeField] protected bool canJump = true;

        protected CharacterController characterController;
        protected float velocityY;
        protected Vector3 currentImpact;

        private readonly float gravity = Physics.gravity.y;

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        protected virtual void Update()
        {
            Move();
            Jump();
        }

        protected virtual void Move()
        {
            Vector3 movementInput = Vector3.zero;

            movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

            movementInput = transform.TransformDirection(movementInput);

            if (characterController.isGrounded && velocityY < 0f)
            {
                velocityY = 0f;
            }

            velocityY += gravity * Time.deltaTime;

            Vector3 velocity = movementInput * movementSpeed + Vector3.up * velocityY;

            if (currentImpact.magnitude > 0.2f)
            {
                velocity += currentImpact;
            }

            characterController.Move(velocity * Time.deltaTime);

            currentImpact = Vector3.Lerp(currentImpact, Vector3.zero, damping * Time.deltaTime);
        }

        public void ResetImpact()
        {
            currentImpact = Vector3.zero;
            velocityY = 0f;
        }

        public void ResetImpactY()
        {
            currentImpact.y = 0f;
            velocityY = 0f;
        }

        protected virtual void Jump()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (characterController.isGrounded)
                {
                    AddForce(Vector3.up, jumpForce);
                }
            }
        }

        /* 
        public IEnumerator ResetJump()
        {
            canJump = false;
            yield return new WaitForSeconds(jumpDelay);
            canJump = true;
        }
        */

        public void AddForce(Vector3 direction, float magnitude)
        {
            currentImpact += direction.normalized * magnitude / mass;
        }
    }

}
