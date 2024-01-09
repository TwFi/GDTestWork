using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    public float Speed { get => speed; }

    public event Action OnStartMoving;
    public event Action<Vector3> OnChangeDirection;
    public event Action OnStopMoving;

    readonly Dictionary<KeyCode, Vector3> keyDirections = new()
    {
        {KeyCode.W, Vector3.forward},
        {KeyCode.S, Vector3.back},
        {KeyCode.A, Vector3.left},
        {KeyCode.D, Vector3.right}
    };

    private bool allowMove = true;
    bool isMoving = false;
    Vector3 lastDirection = Vector3.zero;

    private void Start()
    {
        SceneManager.Instance.OnLose += PlayerLoses;
    }

    private void Update()
    {
        if (allowMove && TryGetDirection(out Vector3 direction))
        {
            if (!isMoving)
                StartMoving();

            Move(direction);
        }
        else if (isMoving)
            Stop();
    }

    private bool TryGetDirection(out Vector3 direction)
    {
        direction = Vector3.zero;
        foreach (var keyDir in keyDirections)
        {
            if (Input.GetKey(keyDir.Key))
                direction += keyDir.Value;
        }

        if (direction.Equals(Vector3.zero))
            return false;
        else
            return true;
    }

    private void StartMoving()
    {
        isMoving = true;

        OnStartMoving?.Invoke();
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction * Speed * Time.deltaTime;

        if (direction != lastDirection)
        {
            lastDirection = direction;

            OnChangeDirection?.Invoke(direction);
        }
    }

    private void Stop()
    {
        isMoving = false;

        OnStopMoving?.Invoke();
    }

    private void PlayerLoses()
    {
        allowMove = false;
    }

    private void OnDestroy()
    {
        SceneManager.Instance.OnLose -= PlayerLoses;
    }
}