using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public GameObject left_arm_col;
    public GameObject right_arm_col;
    public GameObject left_leg_col;
    public GameObject right_leg_col;
    public GameObject head_col;
    public GameObject body_col;

    private PlayerController _controller;
    private MovementController _moveController;
    [HideInInspector] public Animator _animator;

    private float _speed;
    [HideInInspector] public bool attacking = false;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _moveController = GetComponent<MovementController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _moveController.TryMove(_speed);

        if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Elf Jump")
        {
            left_leg_col.SetActive(false);
            right_leg_col.SetActive(false);
        }
        else
        {
            left_leg_col.SetActive(true);
            right_leg_col.SetActive(true);
        }

        if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Elf Crouch")
        {
            head_col.SetActive(false);
            body_col.SetActive(false);
            left_arm_col.SetActive(false);
            right_arm_col.SetActive(false);
        }
        else
        {
            head_col.SetActive(true);
            body_col.SetActive(true);
            left_arm_col.SetActive(true);
            right_arm_col.SetActive(true);
        }


        // Elf only
        if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Elf Light Down")
        {
            right_arm_col.tag = "Attack";
        }
        else
        {
            right_arm_col.tag = "Static";
        }

        if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Elf Light Up")
        {
            left_arm_col.tag = "Attack";
        }
        else
        {
            left_arm_col.tag = "Static";
        }

        if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Elf Heavy Down")
        {
            left_leg_col.tag = "Attack";
            right_leg_col.tag = "Attack";
        }
        else
        {
            left_leg_col.tag = "Static";
            right_leg_col.tag = "Static";
        }

        if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Elf Heavy Up")
        {
            right_leg_col.tag = "Attack";
        }
        else
        {
            right_leg_col.tag = "Static";
        }

        // check if attaking
        if (left_arm_col.CompareTag("Attack") || right_arm_col.CompareTag("Attack") || left_leg_col.CompareTag("Attack") || right_leg_col.CompareTag("Attack")
            || !left_leg_col.activeSelf || !head_col.activeSelf)
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }
    }
    // Start is called before the first frame update
    private void OnMove(InputValue input)
    {
        _speed = input.Get<float>();
    }

    private void OnQuickSoftAttack()
    {
        if (!attacking) _controller.TryLowQuickAttack();
    }
    private void OnQuickHeavyAttack()
    {
        if (!attacking) _controller.TryHighQuickAttack();
    }
    private void OnSlowSoftAttack()
    {
        if (!attacking) _controller.TryLowSlowAttack();
    }
    private void OnSlowHeavyAttack()
    {
        if (!attacking) _controller.TryHighSlowAttack();
    }
    private void OnJump()
    {
        if (!attacking) _controller.TryHighBlock();
    }
    private void OnCrouch()
    {
        if (!attacking) _controller.TryLowBlock();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
