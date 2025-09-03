using Enemy;
using System.Collections.Generic;
using UnityEngine;

public class AutoTargeting : MonoBehaviour
{
    [Header("Targeting Settings")]
    [SerializeField] private float detectionRadius = 100f; // �� ���� ����
    [SerializeField] private float snapDistanceThreshold = 100f; // ������ �Ÿ� �Ӱ谪 (�ȼ�)
    [SerializeField] private LayerMask enemyLayerMask = -1; // �� ���̾� ����ũ
    
    private Camera playerCamera;
    private EnemyBehavior currentTarget;
    private bool isTargeting = false;
    private bool lockMode = false;

    CustomCursor customCursor;

    private void Start()
    {
        // �÷��̾� ī�޶� ã��
        playerCamera = Camera.main;
        if (playerCamera == null)
        {
            playerCamera = FindFirstObjectByType<Camera>();
        }

        customCursor = CustomCursor.Instance;
    }

    private void Update()
    {
        // When holding right mouse button, enable lock mode. Otherwise, disable it.
        lockMode = Input.GetMouseButton(1);

        if (lockMode)
        {
            // In lock mode, always find and target enemies
            FindAndTargetEnemies();
        }
        else
        {
            ClearTarget();
        }
    }

    private void FindAndTargetEnemies()
    {
        // 1�ܰ�: ��ó ���� ã��
        List<EnemyBehavior> nearbyEnemies = FindNearbyEnemies();
        
        if (nearbyEnemies.Count == 0)
        {
            // ���� ������ Ÿ�� ����
            ClearTarget();
            return;
        }
        
        // 2�ܰ�: ���콺 ��ġ ��������
        Vector3 mouseScreenPosition = Input.mousePosition;
        
        // 3�ܰ�: ���� ����� �� ã�� (�Ÿ� ����)
        EnemyBehavior closestEnemy = FindClosestEnemyByDistance(nearbyEnemies, mouseScreenPosition);
        
        if (closestEnemy != null)
        {
            // 4�ܰ�: �Ÿ� ���� ���
            float distance = CalculateScreenDistance(mouseScreenPosition, closestEnemy);
            
            // 5�ܰ�: �Ӱ谪���� ������ Ÿ����
            if (distance <= snapDistanceThreshold)
            {
                SetTarget(closestEnemy);
            }
            else
            {
                ClearTarget();
            }
        }
        else
        {
            ClearTarget();
        }
    }

    private void SnapCursorToTarget(EnemyBehavior enemy)
    {
        if (customCursor != null && enemy != null)
        {
            Vector3 enemyScreenPos = playerCamera.WorldToScreenPoint(enemy.transform.position);
            customCursor.MoveTo(enemyScreenPos);
        }
    }

    private void ReturnCursorToMouse()
    {
        if (customCursor != null)
        {
            customCursor.ReturnToMousePos();
        }
    }

    public List<EnemyBehavior> FindNearbyEnemies()
    {
        List<EnemyBehavior> enemies = new List<EnemyBehavior>();
        
        // Physics.OverlapSphere�� ����Ͽ� ��ó �ݶ��̴� ã��
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayerMask);
        
        foreach (Collider col in colliders)
        {
            EnemyBehavior enemy = col.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }
        
        return enemies;
    }
    
    private EnemyBehavior FindClosestEnemyByDistance(List<EnemyBehavior> enemies, Vector3 mouseScreenPos)
    {
        EnemyBehavior closestEnemy = null;
        float smallestDistance = float.MaxValue;
        
        foreach (EnemyBehavior enemy in enemies)
        {
            float distance = CalculateScreenDistance(mouseScreenPos, enemy);
            
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                closestEnemy = enemy;
            }
        }
        
        return closestEnemy;
    }
    
    private float CalculateScreenDistance(Vector3 mouseScreenPos, EnemyBehavior enemy)
    {
        // ���� ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
        Vector3 enemyScreenPos = playerCamera.WorldToScreenPoint(enemy.transform.position);
        
        // ���콺 ��ġ�� ���� ��ũ�� ��ġ ������ �Ÿ� ��� (�ȼ� ����)
        float distance = Vector2.Distance(mouseScreenPos, enemyScreenPos);
        
        return distance;
    }
    
    private void SetTarget(EnemyBehavior enemy)
    {
        if (currentTarget != enemy)
        {
            // ���� Ÿ�� ����
            if (currentTarget != null)
            {
                currentTarget.SetTargeted(false);
            }
            
            // ���ο� Ÿ�� ����
            currentTarget = enemy;
            currentTarget.SetTargeted(true);
            isTargeting = true;
        }
        SnapCursorToTarget(enemy);
    }

    private void ClearTarget()
    {
        if (currentTarget != null)
        {
            currentTarget.SetTargeted(false);
            currentTarget = null;
        }
        isTargeting = false;
        ReturnCursorToMouse();
    }

    // ���� Ÿ�� �������� (�̻��� �߻� �� ���)
    public EnemyBehavior GetCurrentTarget()
    {
        return currentTarget;
    }
    
    public bool IsTargeting()
    {
        return isTargeting;
    }
}