using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Awake()
    {
        _deltaPosition = transform.position - player.transform.position;
    }

    private void Update()
    {
        transform.position = player.transform.position + _deltaPosition;
    }

    [SerializeField] private GameObject player;
    private Vector3 _deltaPosition;
}
