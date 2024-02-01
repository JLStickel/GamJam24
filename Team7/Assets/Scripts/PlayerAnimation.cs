using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerController _playerReference;
    private Animator _playerAnimator;
    private SpriteRenderer _playerSprite;
    [SerializeField] private GameObject _dashParticle;
    private bool _dashActive = false;

    private float _direction;
    private int _parameter;

    void Start()
    {
        _playerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        _playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpriteRenderer>();

        _playerReference._startDash += Dash;
        _playerReference._endDash += Dash;

        _direction = 0;
        _parameter = 0;
        _playerAnimator.SetFloat("Direction", _direction);
        _playerAnimator.SetInteger("Parameter", _parameter);
    }

    void Update()
    {
        if(_playerReference.moveVertical == 0)
        {
            if (_playerReference.moveHorizontal == 0)
            {
                _parameter = 0;
                _playerAnimator.SetInteger("Parameter", _parameter);
            }
            else if (_playerReference.moveHorizontal < 0)
            {
                _direction = 0.3f;
                _playerAnimator.SetFloat("Direction", _direction);
                _playerSprite.flipX = false;
                _parameter = 1;
                _playerAnimator.SetInteger("Parameter", _parameter);
            }
            else if(_playerReference.moveHorizontal > 0)
            {
                _direction = 0.3f;
                _playerAnimator.SetFloat("Direction", _direction);
                _playerSprite.flipX = true;
                _parameter = 1;
                _playerAnimator.SetInteger("Parameter", _parameter);
            }

        }
        else if(_playerReference.moveVertical > 0)
        {
            _direction = 1f;
            _playerAnimator.SetFloat("Direction", _direction);
            _parameter = 1;
            _playerAnimator.SetInteger("Parameter", _parameter);
        }
        else if(_playerReference.moveVertical < 0)
        {
            _direction = 0f;
            _playerAnimator.SetFloat("Direction", _direction);
            _parameter = 1;
            _playerAnimator.SetInteger("Parameter", _parameter);
        }

    }

    private void Parry()
    {
        _parameter = 2;
        _playerAnimator.SetInteger("Parameter", _parameter);
    }

    private void Dash()
    {
        Instantiate<GameObject>(_dashParticle, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform );
    }


}
