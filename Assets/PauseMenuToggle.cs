using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private bool isPaused = false;


    private void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Hide the cursor
        Cursor.visible = false;
    }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component missing!");
        }

        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (isPaused)
        {
            // Lock the cursor to the center of the screen
            Cursor.lockState = CursorLockMode.Locked;

            // Hide the cursor
            Cursor.visible = false;
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        isPaused = !isPaused;

        canvasGroup.interactable = isPaused;
        canvasGroup.blocksRaycasts = isPaused;
        canvasGroup.alpha = isPaused ? 1f : 0f;
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
