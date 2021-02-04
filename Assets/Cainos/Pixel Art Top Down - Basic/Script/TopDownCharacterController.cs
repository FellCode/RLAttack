using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;

        private Animator animator;

        public bool playerHasMoved = false;

        public EncounterGenerator generator;

        private int nextUpdate=1;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
                if(Time.time>=nextUpdate){
                    nextUpdate=Mathf.FloorToInt(Time.time)+1;
                    generator.checkEncouter();
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
                if(Time.time>=nextUpdate){
                    nextUpdate=Mathf.FloorToInt(Time.time)+1;
                    generator.checkEncouter();
                }
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                animator.SetInteger("Direction", 1);
                if(Time.time>=nextUpdate){
                    nextUpdate=Mathf.FloorToInt(Time.time)+1;
                    generator.checkEncouter();
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetInteger("Direction", 0);
                if(Time.time>=nextUpdate){
                    nextUpdate=Mathf.FloorToInt(Time.time)+1;
                    generator.checkEncouter();
                }
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }
    }
}
