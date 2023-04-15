using Game.Dialogue;
using Photon.Pun;
using System;
using UnityEngine;

namespace Game.Control
{
    public class PlayerController : MonoBehaviourPunCallbacks
    {
        [SerializeField] float movementSpeed;
        [SerializeField] float swimSpeed;
        [SerializeField] float jumpSpeed;

        float speed;

        Rigidbody2D rigidbody;
        SpriteRenderer spriteRenderer;
        Animator animator;
        CapsuleCollider2D bodyColldier;
        PlayerLegs legs;
        PlayerHead head;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            bodyColldier = GetComponent<CapsuleCollider2D>();
            legs = transform.Find("Legs").GetComponent<PlayerLegs>();
            head = transform.Find("Head").GetComponent<PlayerHead>();
            speed = movementSpeed;
        }

        private void Update()
        {
            if (!photonView.IsMine)
                return;

            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);


            if (Talker.active) 
            {
                
                return; 
            }


            if (legs.OnGround())
                speed = movementSpeed;

            if (ClimbBehaviour()) return;
            MovementBehaviour();
            SwimBehaviour();
            JumpBehaviour();
        }

        private bool ClimbBehaviour()
        {
            bool bodyTouchingLadder = bodyColldier.IsTouchingLayers(LayerMask.GetMask("Ladder"));
            if (bodyTouchingLadder  &&  !legs.OnGround())
            {
                if(head.IsFacingLadder())
                {
                    animator.SetBool("Moving", false);
                    animator.SetBool("Climbing", true);
                }
                else
                {

                    animator.SetBool("Climbing", false);
                }
               

                float x = Input.GetAxis("Horizontal");
                float y = Input.GetAxis("Vertical");

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Climb"))
                {
                    animator.speed = 0;
                    if (Mathf.Abs(y) > 0.1f)
                    {
                        animator.speed = 1;
                        x = 0;
                    }
                    else if (Mathf.Abs(x) > 0.1f)
                        y = 0;
                }
               
                rigidbody.velocity = new Vector2(x * speed/2, y * speed/2);

                if (legs.OnLadder())
                    rigidbody.gravityScale = 0;

                else
                    rigidbody.gravityScale = 1;

                return true;

         }
            animator.SetBool("Climbing", false);
            animator.speed = 1;
            return false;
     }

        private void JumpBehaviour()
        {
            if (Input.GetButtonDown("Jump") && (legs.OnGround() || head.IsWet()))
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);    
            }
            animator.SetBool("Jumping", !legs.OnGround());
        }

        private void SwimBehaviour()
        {
            if(head.IsSwimming())
            {
                rigidbody.gravityScale = 0;
                speed = swimSpeed;
                rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical")* speed);
            }
            else
                rigidbody.gravityScale = 1f;
        }

        private void MovementBehaviour()
        {
            float movement = Input.GetAxis("Horizontal");
            rigidbody.velocity = new Vector2(movement * speed, rigidbody.velocity.y);

            if (Mathf.Abs(movement) > 0.1f)
            {
                //Flip
                if (movement < 0)
                    spriteRenderer.flipX = true;
                else
                    spriteRenderer.flipX = false;

                //Run
                animator.SetBool("Moving", true);

            }
            else
                animator.SetBool("Moving", false);
        }


        
    }
}
