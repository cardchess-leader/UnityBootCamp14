using UnityEngine;
using System.Collections.Generic;

public class AutoTargeting : MonoBehaviour
{
    [Header("Targeting Settings")]
    [SerializeField] private float detectionRadius = 100f; // �� ���� ����
    [SerializeField] private float snapAngleThreshold = 30f; // ������ ���� �Ӱ谪 (��)
    [SerializeField] private LayerMask enemyLayerMask = -1; // �� ���̾� ����ũ
    
    [Header("Cursor Settings")]
    [SerializeField] private Texture2D crosshairTexture; // ���� Ŀ�� �ؽ�ó
    [SerializeField] private Vector2 cursorHotspot = new Vector2(16, 16); // Ŀ�� �ֽ��� (�߾���)
    
    private Camera playerCamera;
    private Enemy currentTarget;
    private bool isTargeting = false;
    private bool isCrosshairActive = false;
    
    private void Start()
    {
        // �÷��̾� ī�޶� ã��
        playerCamera = Camera.main;
        if (playerCamera == null)
        {
            playerCamera = FindObjectOfType<Camera>();
        }
        
        // �⺻ Ŀ�� ���� (�������� �⺻ ���)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        // ���� Ŀ�� �ؽ�ó�� ������ �⺻ ����
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
        // 1�ܰ�: ��ó ���� ã��
        List<Enemy> nearbyEnemies = FindNearbyEnemies();
        
        if (nearbyEnemies.Count == 0)
        {
            // ���� ������ Ÿ�� ���� �� �Ϲ� Ŀ���� ����
            ClearTarget();
            SetNormalCursor();
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
        // 32x32 �ȼ��� ���� Ŀ�� ����
        int size = 32;
        crosshairTexture = new Texture2D(size, size, TextureFormat.RGBA32, false);
        Color[] pixels = new Color[size * size];
        
        // ��� �ȼ��� �����ϰ� �ʱ�ȭ
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.clear;
        }
        
        // ���� ��� �׸���
        int center = size / 2;
        int lineWidth = 2;
        
        // ������
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
        
        // ����
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
        
        // �߾ӿ� ���� �� �߰� (��Ȯ�� ������)
        pixels[center * size + center] = Color.red;
        
        crosshairTexture.SetPixels(pixels);
        crosshairTexture.Apply();
        
        // �ֽ����� �ؽ�ó �߾����� ����
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
    
    // ������ ���� ���� ǥ�� (�����Ϳ�����)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    
    private void OnDestroy()
    {
        // ��ũ��Ʈ�� �ı��� �� Ŀ���� �⺻������ ����
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}