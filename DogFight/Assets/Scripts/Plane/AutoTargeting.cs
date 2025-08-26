using UnityEngine;
using System.Collections.Generic;

public class AutoTargeting : MonoBehaviour
{
    [Header("Targeting Settings")]
    [SerializeField] private float detectionRadius = 100f; // �� ���� ����
    [SerializeField] private float snapAngleThreshold = 30f; // ������ ���� �Ӱ谪 (��)
    [SerializeField] private LayerMask enemyLayerMask = -1; // �� ���̾� ����ũ
    
    private Camera playerCamera;
    private Enemy currentTarget;
    private bool isTargeting = false;

    CustomCursor customCursor;

    private void Start()
    {
        // �÷��̾� ī�޶� ã��
        playerCamera = Camera.main;
        if (playerCamera == null)
        {
            playerCamera = FindObjectOfType<Camera>();
        }

        customCursor = CustomCursor.Instance;
    }

    private void Update()
    {
        FindAndTargetEnemies();
    }
    
    private void FindAndTargetEnemies()
    {
        // 1�ܰ�: ��ó ���� ã��
        List<Enemy> nearbyEnemies = FindNearbyEnemies();
        
        if (nearbyEnemies.Count == 0)
        {
            // ���� ������ Ÿ�� ����
            ClearTarget();
            return;
        }
        
        // 2�ܰ�: ���콺 ��ġ ��������
        Vector3 mouseScreenPosition = Input.mousePosition;
        
        // 3�ܰ�: ���� ����� �� ã�� (���� ����)
        Enemy closestEnemy = FindClosestEnemyByAngle(nearbyEnemies, mouseScreenPosition);
        
        if (closestEnemy != null)
        {
            // 4�ܰ�: ���� ���� ���
            float angleDifference = CalculateAngleDifference(mouseScreenPosition, closestEnemy);
            
            // 5�ܰ�: �Ӱ谪���� ������ Ÿ����
            if (angleDifference <= snapAngleThreshold)
            {
                SetTarget(closestEnemy);
                SnapCursorToTarget(closestEnemy);
            }
            else
            {
                ClearTarget();
                ReturnCursorToMouse();
            }
        }
        else
        {
            ClearTarget();
            ReturnCursorToMouse();
        }
    }

    private void SnapCursorToTarget(Enemy enemy)
    {
        Debug.Log("SnapCursorToTarget");
        if (customCursor != null && enemy != null)
        {
            Vector3 enemyScreenPos = playerCamera.WorldToScreenPoint(enemy.transform.position);
            customCursor.MoveTo(enemyScreenPos);
        }
    }

    private void ReturnCursorToMouse()
    {
        Debug.Log("ReturnCursorToMouse");
        if (customCursor != null)
        {
            customCursor.ReturnToMousePos();
        }
    }

    private List<Enemy> FindNearbyEnemies()
    {
        List<Enemy> enemies = new List<Enemy>();
        
        // Physics.OverlapSphere�� ����Ͽ� ��ó �ݶ��̴� ã��
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayerMask);
        
        foreach (Collider col in colliders)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }
        
        return enemies;
    }
    
    private Enemy FindClosestEnemyByAngle(List<Enemy> enemies, Vector3 mouseScreenPos)
    {
        Enemy closestEnemy = null;
        float smallestAngle = float.MaxValue;
        
        foreach (Enemy enemy in enemies)
        {
            float angle = CalculateAngleDifference(mouseScreenPos, enemy);
            
            if (angle < smallestAngle)
            {
                smallestAngle = angle;
                closestEnemy = enemy;
            }
        }
        
        return closestEnemy;
    }
    
    private float CalculateAngleDifference(Vector3 mouseScreenPos, Enemy enemy)
    {
        // ���� ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
        Vector3 enemyScreenPos = playerCamera.WorldToScreenPoint(enemy.transform.position);
        
        // ��ũ�� �߾���
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        
        // ��ũ�� �߾ӿ��� ���콺������ ����
        Vector2 mouseDirection = (mouseScreenPos - screenCenter).normalized;
        
        // ��ũ�� �߾ӿ��� �������� ����
        Vector2 enemyDirection = (enemyScreenPos - screenCenter).normalized;
        
        // �� ���� ������ ���� ���
        float angle = Vector2.Angle(mouseDirection, enemyDirection);
        
        return angle;
    }
    
    private void SetTarget(Enemy enemy)
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
    }
    
    private void ClearTarget()
    {
        if (currentTarget != null)
        {
            currentTarget.SetTargeted(false);
            currentTarget = null;
        }
        isTargeting = false;
    }
    
    // ���� Ÿ�� �������� (�̻��� �߻� �� ���)
    public Enemy GetCurrentTarget()
    {
        return currentTarget;
    }
    
    public bool IsTargeting()
    {
        return isTargeting;
    }
    
    //// ������ ���� ���� ǥ�� (�����Ϳ�����)
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, detectionRadius);
    //}
}