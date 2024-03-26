using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Running
    }

    public static Action OnRunnerDied;

    [Header("Settings")]
    [SerializeField]
    private float searchRadius;

    [SerializeField]
    private float moveSpeed;

    private EnemyState enemyState;
    private Transform targetRunner;

    private void Awake()
    {
        enemyState = EnemyState.Idle;   
    }

    private void Update()
    {
        HandleState();      
    }

    private void HandleState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                SearchForTarget();
                break;
            case EnemyState.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void SearchForTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);

        foreach (Collider collider in colliders)
        {
            if(collider.TryGetComponent(out Runner runner))
            {
                if(runner.IsTargeted())
                {
                    continue;
                }
                runner.SetTarget();
                targetRunner = runner.transform;

                ChangeStateToRunning();

                return;
            }
        }

    }

    private void ChangeStateToRunning()
    {
        enemyState = EnemyState.Running;
        GetComponent<Animator>().Play("Run");
    }

    private void RunTowardsTarget()
    {
        if(targetRunner != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, Time.deltaTime * moveSpeed);

            if(Vector3.Distance(transform.position, targetRunner.position) < .1f)
            {
                OnRunnerDied?.Invoke();

                Destroy(targetRunner.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
