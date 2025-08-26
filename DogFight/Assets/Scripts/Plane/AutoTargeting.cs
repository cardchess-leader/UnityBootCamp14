using UnityEngine;
using System.Collections.Generic;

public class AutoTargeting : MonoBehaviour
{
    [Header("Targeting Settings")]
    [SerializeField] private float detectionRadius = 100f; // 적 감지 범위
    [SerializeField] private float snapDistanceThreshold = 100f; // 스냅할 거리 임계값 (픽셀)
    [SerializeField] private LayerMask enemyLayerMask = -1; // 적 레이어 마스크
    
    private Camera playerCamera;
    private Enemy currentTarget;
    private bool isTargeting = false;

    CustomCursor customCursor;

    private void Start()
    {
        // 플레이어 카메라 찾기
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
        // 1단계: 근처 적들 찾기
        List<Enemy> nearbyEnemies = FindNearbyEnemies();
        
        if (nearbyEnemies.Count == 0)
        {
            // 적이 없으면 타겟 해제
            ClearTarget();
            return;
        }
        
        // 2단계: 마우스 위치 가져오기
        Vector3 mouseScreenPosition = Input.mousePosition;
        
        // 3단계: 가장 가까운 적 찾기 (거리 기준)
        Enemy closestEnemy = FindClosestEnemyByDistance(nearbyEnemies, mouseScreenPosition);
        
        if (closestEnemy != null)
        {
            // 4단계: 거리 차이 계산
            float distance = CalculateScreenDistance(mouseScreenPosition, closestEnemy);
            
            // 5단계: 임계값보다 작으면 타겟팅
            if (distance <= snapDistanceThreshold)
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
        
        // Physics.OverlapSphere를 사용하여 근처 콜라이더 찾기
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
    
    private Enemy FindClosestEnemyByDistance(List<Enemy> enemies, Vector3 mouseScreenPos)
    {
        Enemy closestEnemy = null;
        float smallestDistance = float.MaxValue;
        
        foreach (Enemy enemy in enemies)
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
    
    private float CalculateScreenDistance(Vector3 mouseScreenPos, Enemy enemy)
    {
        // 적의 월드 좌표를 스크린 좌표로 변환
        Vector3 enemyScreenPos = playerCamera.WorldToScreenPoint(enemy.transform.position);
        
        // 마우스 위치와 적의 스크린 위치 사이의 거리 계산 (픽셀 단위)
        float distance = Vector2.Distance(mouseScreenPos, enemyScreenPos);
        
        return distance;
    }
    
    private void SetTarget(Enemy enemy)
    {
        if (currentTarget != enemy)
        {
            // 이전 타겟 해제
            if (currentTarget != null)
            {
                currentTarget.SetTargeted(false);
            }
            
            // 새로운 타겟 설정
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
    
    // 현재 타겟 가져오기 (미사일 발사 시 사용)
    public Enemy GetCurrentTarget()
    {
        return currentTarget;
    }
    
    public bool IsTargeting()
    {
        return isTargeting;
    }
}