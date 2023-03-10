using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    Vector2 MoveLimits=new Vector2(-5,5);

    [SerializeField]
    float SafetyDistance=0.5f;
    // Start is called before the first frame update

    PlayerInput pi;
    

    #region AnimationParamNames
    const string SPEED = "Speed";
    const string ATTACK_HIGH_QUICK = "AttackHighQuick";
    const string DIE = "Die";
    const string WIN = "Win";

    #endregion

    private Animator _animator;
    private Transform _otherPlayer;

    public static int _playercount;
    int _id;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _id = _playercount++;
        pi = GetComponent<PlayerInput>();
    }

    public void SetOtherPlayer(Transform other)
    {
        _otherPlayer = other;
    }

    
    public void TryMove(float speed)
    {
        if (CanMove(speed))
        {       
            _animator.SetFloat(SPEED, _id == 1 ? -speed : speed);
            if (gameObject.name == "elf_model Variant(Clone)")
            {
                transform.Translate(0, 0, speed * 0.005f);
            }
            else
            {
                transform.Translate(0, 0, -speed * 0.005f);
            }
        }
            
        else
            _animator.SetFloat(SPEED, 0);
    }

    public void LateUpdate()
    {
        var pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }

    bool CanMove(float speed)
    {
        if (pi.attacking) return false;

        if (speed < 0)
            return CanMoveLeft();
        if (speed > 0)
            return CanMoveRight();

        return true;
    }

    bool CanMoveLeft()
    {
        if (transform.position.x <= MoveLimits.x)
            return false;

        if (gameObject.CompareTag("Zombie"))
        {
            if (this.transform.position.x - 1.0f <= GameObject.FindGameObjectWithTag("Elf").transform.position.x)
            {
                return false;
            }
        }

        return true;
    }
    bool CanMoveRight()
    {
        if (transform.position.x >= MoveLimits.y)
            return false;

        if (gameObject.CompareTag("Elf"))
        {
            if (this.transform.position.x + 1.0f >= GameObject.FindGameObjectWithTag("Zombie").transform.position.x)
            {
                return false;
            }
        }

        return true;
    }

  
}
