using UnityEngine;
using System.Collections;

public class TransitionUI : MonoBehaviour
{
    [SerializeField] private float _transitionInDelay = 1.5f;
    
    private Animator _animator;
    private Spaceship _spaceship;
    private WaitForSeconds _waitDelay;

    private readonly string _transitionIn = "transition_in";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spaceship = FindObjectOfType<Spaceship>();
        _waitDelay = new WaitForSeconds(_transitionInDelay);
    }

    private void OnEnable()
    {
        _spaceship.OnDestroyed += TransitionIn;
    }

    private void TransitionIn()
    {
        StartCoroutine(ExecuteTransitionIn());
    }

    private IEnumerator ExecuteTransitionIn()
    {
        yield return _waitDelay;
        _animator.SetTrigger(_transitionIn);
        LevelManager.Instance.RestartLevelAfterGameOver();
    }
}
