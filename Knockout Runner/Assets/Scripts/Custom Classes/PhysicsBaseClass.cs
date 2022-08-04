using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class PhysicsBaseClass : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] protected float gravityAmount;
        
        protected Vector3 Velocity;
        
        [Header("Ground Check")]
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask groundLayer;
        protected bool IsGrounded;


        protected virtual void Start()
        { }
        protected virtual void OnDisable()
        { }

        protected virtual void Update()
        { }

        protected  void ApplyGravity()
        {
            if (!controller) return;
            //ground check
            IsGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckDistance, groundLayer);
            if (IsGrounded && Velocity.y < 0)
                Velocity.y = -2f;

            Velocity.y += gravityAmount * Time.deltaTime;
            if(!controller.enabled) return;
            controller.Move(Velocity * Time.deltaTime);
        }
        
    }
