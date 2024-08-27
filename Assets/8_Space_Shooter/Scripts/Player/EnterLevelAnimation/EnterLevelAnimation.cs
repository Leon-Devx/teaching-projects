using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLevelAnimation : MonoBehaviour
{
    private SpaceshipMovement2D _spaceshipMovement2D;
    private SpaceshipShootAction _spaceshipShootAction;
    private Animator _animator;

    private void Awake()
    {
        _spaceshipMovement2D = GetComponent<SpaceshipMovement2D>();
        _spaceshipShootAction = GetComponent<SpaceshipShootAction>();
        _animator = GetComponent<Animator>();

        _spaceshipMovement2D.enabled = false;
        _spaceshipShootAction.enabled = false;
    }

    // Method is invoked from animation
    private void EnableSpaceship()
    {
        _spaceshipMovement2D.enabled = true;
        _spaceshipShootAction.enabled = true;
        _animator.enabled = false;
        enabled = false;
    }
}
