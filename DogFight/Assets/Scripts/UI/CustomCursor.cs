using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    // Make this singleton so that it can be accessed from other scripts
    public static CustomCursor Instance { get; private set; }
    private void Awake()
    {
        // Ensure that there is only one instance of CustomCursor
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private GameObject crosshairUI; // ���⿡ CrosshairUI �Ҵ�
    [SerializeField] private Canvas canvas;
    [SerializeField] private float cursorMoveSpeed = 500f; // Ŀ�� �̵� �ӵ� ���� ����

    private RectTransform crosshairRect;
    //private bool isActive = true;
    
    private void Start()
    {
        if (crosshairUI != null)
        {
            crosshairRect = crosshairUI.GetComponent<RectTransform>();
        }
        
        // �ϵ���� Ŀ�� �׻� ���̰� (�⺻ Ŀ�� ���)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //public void ShowCrosshair()
    //{
    //    isActive = true;
    //}
    
    //public void HideCrosshair()
    //{
    //    isActive = false;
    //}

    public void MoveTo(Vector3 targetScreenPos)
    {
        if (crosshairRect != null)
        {
            Vector3 currentPos = crosshairRect.position;
            Vector3 newPos = Vector3.MoveTowards(currentPos, targetScreenPos, cursorMoveSpeed * Time.deltaTime);
            crosshairRect.position = newPos;
        }
    }

    public void ReturnToMousePos()
    {
        if (crosshairRect != null)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 currentPos = crosshairRect.position;
            Vector3 newPos = Vector3.MoveTowards(currentPos, mousePos, cursorMoveSpeed * Time.deltaTime);
            crosshairRect.position = newPos;
        }
    }
}