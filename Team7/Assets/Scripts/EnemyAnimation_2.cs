using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation_2 : MonoBehaviour
{
    public Enemydasher _enemyReference;
    public Animator _enemyAnimator;
    public SpriteRenderer _enemySprite;
    public Vector3 _dir;
    public float _direction;
    public int _parameter;
    private bool _attacking;
    private int _walkAttack;


    void Start()
    {
        _walkAttack = 0;
        _enemyReference.Attack += Attacking;
        _parameter = 0;
        _enemyAnimator.SetInteger("Parameter", _parameter);
    }

    void Update()
    {
        Debug.Log(_parameter);
        _dir = _enemyReference.dir;

        if (Mathf.Abs(_dir.x) > Mathf.Abs(_dir.y))
        {
            if (_dir.x == 0)
            {
                
                _parameter = 0;
                _enemyAnimator.SetInteger("Parameter", _parameter);
            }
            else if (_dir.x < 0)
            {
                _direction = 0.3f;
                _enemyAnimator.SetFloat("Direction", _direction);
                _enemySprite.flipX = false;
                
                    _parameter = _walkAttack;
                _enemyAnimator.SetInteger("Parameter", _parameter);
            }
            else if (_dir.x > 0)
            {
                _direction = 0.3f;
                _enemyAnimator.SetFloat("Direction", _direction);
                _enemySprite.flipX = true;
                
                    _parameter = _walkAttack;
                _enemyAnimator.SetInteger("Parameter", _parameter);
            }

        }
        else if (_dir.y > 0)
        {
            _direction = 1f;
            _enemyAnimator.SetFloat("Direction", _direction);
            
                _parameter = _walkAttack;
            _enemyAnimator.SetInteger("Parameter", _parameter);
        }
        else if (_dir.y < 0)
        {
            _direction = 0f;
            _enemyAnimator.SetFloat("Direction", _direction);
            
                _parameter = _walkAttack;
            _enemyAnimator.SetInteger("Parameter", _parameter);
        }
        
    }

    private void Attacking()
    {
        Debug.Log("Attack");
        _walkAttack = 2;
        _enemyAnimator.SetInteger("Parameter", _walkAttack);
        _attacking = true;
        StartCoroutine(Wait());

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);

        _walkAttack = 1;
        _enemyAnimator.SetInteger("Parameter", _walkAttack);
        _attacking = false;

    }
}
