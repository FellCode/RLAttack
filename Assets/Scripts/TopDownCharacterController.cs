using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;

        private Animator animator;

        public bool isMoving=false;

        

        private void Start()
        {
            animator = GetComponent<Animator>();
            transform.localPosition = SceneData.playerPosition;
        }


        private void Update()
        {   
            isMoving = false;
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
                isMoving=true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
                isMoving=true;
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                animator.SetInteger("Direction", 1);
                isMoving=true;

            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetInteger("Direction", 0);
                isMoving=true;
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
            
        }

    public bool charIsMoving(){
        return isMoving;
    }
}
