using System.Collections;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    // Make this singleton so that it can be accessed from other scripts
    public static CustomCursor Instance { get; private set; }

    Coroutine cursorAnimateCoroutine = null;

    private void Awake()
    {
        // Ensure that there is only one instance of CustomCursor
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // 게임오브젝트 전체가 아닌 컴포넌트만 파괴
            Destroy(this);
            return;
        }
    }

    [SerializeField] private GameObject crosshairUI;
    [SerializeField] private float cursorMoveSpeed = 500f;
    Vector3 originalCursorSize;
    [SerializeField]
    Color originalCursorColor = Color.white;
    [SerializeField]
    Color targetCursorColor = Color.red;
    [SerializeField]
    float targetCursorScale = 1.5f;
    [SerializeField]
    float animDuration = 0.5f;
    float elapsedTime = 0f;


    private RectTransform crosshairRect;

    private void Start()
    {
        if (crosshairUI != null)
        {
            crosshairRect = crosshairUI.GetComponent<RectTransform>();
            originalCursorSize = crosshairRect.localScale;
        }

        // 하드웨어 커서 설정 활성화
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // 싱글톤 정리
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void MoveTo(Vector3 targetScreenPos)
    {
        if (crosshairRect != null)
        {
            Vector3 currentPos = crosshairRect.position;
            Vector3 newPos = Vector3.MoveTowards(currentPos, targetScreenPos, cursorMoveSpeed * Time.deltaTime);
            crosshairRect.position = newPos;
            StartCursorAnimate(true);
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
            StartCursorAnimate(false);
        }
    }

    void StartCursorAnimate(bool isEnlarge)
    {
        if (cursorAnimateCoroutine != null)
        {
            StopCoroutine(cursorAnimateCoroutine);
        }
        cursorAnimateCoroutine = StartCoroutine(isEnlarge ? EnlargeCursor() : ShrinkCursor());
    }

    IEnumerator EnlargeCursor()
    {
        while (elapsedTime < animDuration)
        {
            elapsedTime += Time.deltaTime;
            crosshairRect.localScale = Vector3.Lerp(originalCursorSize, originalCursorSize * targetCursorScale, elapsedTime / animDuration);
            crosshairRect.GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(originalCursorColor, targetCursorColor, elapsedTime / animDuration);
            yield return null;
        }
        crosshairRect.localScale = originalCursorSize * targetCursorScale;
        elapsedTime = animDuration;
        cursorAnimateCoroutine = null;
    }

    IEnumerator ShrinkCursor()
    {
        while (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            crosshairRect.localScale = Vector3.Lerp(originalCursorSize, originalCursorSize * targetCursorScale, elapsedTime / animDuration);
            crosshairRect.GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(originalCursorColor, targetCursorColor, elapsedTime / animDuration);
            yield return null;
        }
        crosshairRect.localScale = originalCursorSize;
        elapsedTime = 0;
        cursorAnimateCoroutine = null;
    }
}