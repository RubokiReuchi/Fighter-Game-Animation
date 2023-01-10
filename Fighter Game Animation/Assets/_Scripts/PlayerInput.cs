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
    public GameObject body_col1;

    public bool state = false; // death

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
        if (!state) _speed = input.Get<float>();
    }

    private void OnQuickSoftAttack()
    {
        if (!attacking && !state)
        {
            _controller.TryLowQuickAttack();
            if (gameObject.name == "elf_model Variant(Clone)")
            {
                right_arm_col.tag = "Attack";
                StartCoroutine("WaitUntilAnimEnds", 0.667f);
            }
            else
            {
                right_arm_col.tag = "Attack";
                StartCoroutine("WaitUntilAnimEnds", 0.667f);
            }
        }
    }
    private void OnQuickHeavyAttack()
    {
        if (!attacking && !state)
        {
            _controller.TryHighQuickAttack();
            if (gameObject.name == "elf_model Variant(Clone)")
            {
                left_arm_col.tag = "Attack";
                StartCoroutine("WaitUntilAnimEnds", 0.667f);
            }
            else
            {
                right_arm_col.tag = "Attack";
                StartCoroutine("WaitUntilAnimEnds", 0.667f);
            }
        }
    }
    private void OnSlowSoftAttack()
    {
        if (!attacking && !state)
        {
            _controller.TryLowSlowAttack();
            if (gameObject.name == "elf_model Variant(Clone)")
            {
                left_leg_col.tag = "Attack";
                right_leg_col.tag = "Attack";
                StartCoroutine("WaitUntilAnimEnds", 1.0f);
            }
            else
            {
                left_arm_col.tag = "Attack";
                right_arm_col.tag = "Attack";
                StartCoroutine("WaitUntilAnimEnds", 1.0f);
            }
        }
    }
    private void OnSlowHeavyAttack()
    {
        if (!attacking && !state)
        {
            _controller.TryHighSlowAttack();
            if (gameObject.name == "elf_model Variant(Clone)")
            {
                right_leg_col.tag = "Attack";
                StartCoroutine("WaitUntilAnimEnds", 1.0f);
            }
            else
            {
                left_arm_col.tag = "Attack";
                right_arm_col.tag = "Attack";
                StartCoroutine("WaitUntilAnimEnds", 1.0f);
            }
        }
    }
    private void OnJump()
    {
        if (!attacking && !state)
        {
            _controller.TryHighBlock();
            if (gameObject.name == "elf_model Variant(Clone)")
            {
                left_leg_col.SetActive(false);
                right_leg_col.SetActive(false);
                StartCoroutine("WaitUntilJumpEnds", 1.5f);
            }
            else
            {
                left_leg_col.SetActive(false);
                right_leg_col.SetActive(false);
                StartCoroutine("WaitUntilJumpEnds", 2.0f);
            }
        }
    }
    private void OnCrouch()
    {
        if (!attacking && !state)
        {
            _controller.TryLowBlock();
            if (gameObject.name == "elf_model Variant(Clone)")
            {
                head_col.SetActive(false);
                body_col1.SetActive(false);
                left_arm_col.SetActive(false);
                right_arm_col.SetActive(false);
                StartCoroutine("WaitUntilCrouchEnds", 1.067f);
            }
            else
            {
                head_col.SetActive(false);
                body_col1.SetActive(false);
                left_arm_col.SetActive(false);
                right_arm_col.SetActive(false);
                StartCoroutine("WaitUntilCrouchEnds", 2.0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    IEnumerator WaitUntilAnimEnds(float duration)
    {
        yield return new WaitForSeconds(duration);
        left_arm_col.tag = "Static";
        right_arm_col.tag = "Static";
        left_leg_col.tag = "Static";
        right_leg_col.tag = "Static";
    }

    IEnumerator WaitUntilJumpEnds(float duration)
    {
        yield return new WaitForSeconds(duration);
        left_leg_col.SetActive(true);
        right_leg_col.SetActive(true);
    }

    IEnumerator WaitUntilCrouchEnds(float duration)
    {
        yield return new WaitForSeconds(duration);
        head_col.SetActive(true);
        body_col1.SetActive(true);
        left_arm_col.SetActive(true);
        right_arm_col.SetActive(true);
    }
}
