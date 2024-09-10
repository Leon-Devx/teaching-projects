using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private int _targetFPS = 60;

    private void Awake()
    {
        Application.targetFrameRate = _targetFPS;
    }
}
