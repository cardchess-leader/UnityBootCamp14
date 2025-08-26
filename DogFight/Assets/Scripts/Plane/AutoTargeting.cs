using UnityEngine;
using System.Collections.Generic;

public class AutoTargeting : MonoBehaviour
{
    [Header("Targeting Settings")]
    [SerializeField] private float detectionRadius = 100f; // 적 감지 범위
    [SerializeField] private float snapAngleThreshold = 30f; // 스냅할 각도 임계값 (도)
    [SerializeField] private LayerMask enemyLayerMask = -1; // 적 레이어 마스크
    
    [Header("Cursor Settings")]
    [SerializeField] private Texture2D crosshairTexture; // 십자 커서 텍스처
    [SerializeField] private Vector2 cursorHotspot = new Vector2(16, 16); // 커서 핫스팟 (중앙점)
    
    private Camera playerCamera;
    private Enemy currentTarget;
    private bool isTargeting = false;
    private bool isCrosshairActive = false;
    
    private void Start()
    {
        // 플레이어 카메라 찾기
        playerCamera = Camera.main;
        if (playerCamera == null)
        {
            playerCamera = FindObjectOfType<Camera>();
        }
        
        // 기본 커서 설정 (보이지만 기본 모양)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        // 십자 커서 텍스처가 없으면 기본 생성
        if (crosshairTexture == null)
        {
            CreateDefaultCrosshair();
        }
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
            // 적이 없으면 타겟 해제 및 일반 커서로 변경
            ClearTarget();
            SetNormalCursor();
            return;
        }
        
        // 2단계: 마우스 위치 가져오기
        Vector3 mouseScreenPosition = Input.mousePosition;
        
        // 3단계: 가장 가까운 적 찾기 (각도 기준)
        Enemy closestEnemy = FindClosestEnemyByAngle(nearbyEnemies, mouseScreenPosition);
        
        if (closestEnemy != null)
        {
            // 4단계: 각도 차이 계산
            float angleDifference = CalculateAngleDifference(mouseScreenPosition, closestEnemy);
            
            // 5단계: 임계값보다 작으면 타겟팅
            if (angleDifference <= snapAngleThreshold)
            {
                SetTarget(closestEnemy);
                SetCrosshairCursor();
            }
            else
            {
                ClearTarget();
                SetNormalCursor();
            }
        }
        else
        {
            ClearTarget();
            SetNormalCursor();
        }
    }
    
    private void CreateDefaultCrosshair()
    {
        // 32x32 픽셀의 십자 커서 생성
        int size = 32;
        crosshairTexture = new Texture2D(size, size, TextureFormat.RGBA32, false);
        Color[] pixels = new Color[size * size];
        
        // 모든 픽셀을 투명하게 초기화
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.clear;
        }
        
        // 십자 모양 그리기
        int center = size / 2;
        int lineWidth = 2;
        
        // 수직선
        for (int y = 0; y < size; y++)
        {
            for (int x = center - lineWidth/2; x <= center + lineWidth/2; x++)
            {
                if (x >= 0 && x < size)
                {
                    pixels[y * size + x] = Color.white;
                }
            }
        }
        
        // 수평선
        for (int x = 0; x < size; x++)
        {
            for (int y = center - lineWidth/2; y <= center + lineWidth/2; y++)
            {
                if (y >= 0 && y < size)
                {
                    pixels[y * size + x] = Color.white;
                }
            }
        }
        
        // 중앙에 작은 점 추가 (정확한 조준점)
        pixels[center * size + center] = Color.red;
        
        crosshairTexture.SetPixels(pixels);
        crosshairTexture.Apply();
        
        // 핫스팟을 텍스처 중앙으로 설정
        cursorHotspot = new Vector2(center, center);
    }
    
    private void SetCrosshairCursor()
    {
        if (!isCrosshairActive)
        {
            Cursor.SetCursor(crosshairTexture, cursorHotspot, CursorMode.Auto);
            isCrosshairActive = true;
        }
    }
    
    private void SetNormalCursor()
    {
        if (isCrosshairActive)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            isCrosshairActive = false;
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
        // 적의 월드 좌표를 스크린 좌표로 변환
        Vector3 enemyScreenPos = playerCamera.WorldToScreenPoint(enemy.transform.position);
        
        // 스크린 중앙점
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        
        // 스크린 중앙에서 마우스까지의 벡터
        Vector2 mouseDirection = (mouseScreenPos - screenCenter).normalized;
        
        // 스크린 중앙에서 적까지의 벡터
        Vector2 enemyDirection = (enemyScreenPos - screenCenter).normalized;
        
        // 두 벡터 사이의 각도 계산
        float angle = Vector2.Angle(mouseDirection, enemyDirection);
        
        return angle;
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
    
    // 기즈모로 감지 범위 표시 (에디터에서만)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    
    private void OnDestroy()
    {
        // 스크립트가 파괴될 때 커서를 기본값으로 복원
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}