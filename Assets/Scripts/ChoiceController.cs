using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceController : MonoBehaviour
{
    public GameObject choicesContainer;
    public TextMeshProUGUI choice1Text; // Reference to Choice 1 Text
    public TextMeshProUGUI choice2Text; // Reference to Choice 2 Text
    public RectTransform controllerButton; // Reference to the moving button
    private Vector2 centerPosition; // Default position for the button
    public Vector2 leftPosition; // Position for highlighting Choice 1
    public Vector2 rightPosition; // Position for highlighting Choice 2
    private int selectedChoice = 1; // 1 = Choice 1, 2 = Choice 2

    private Vector2 startTouchPosition;
    private Vector2 targetPosition; // Target position for smoother animation
    private bool isSwiping = false;
    private bool isSliding = false; // To prevent confirmation during sliding
    private bool isVideoPlaying = true; // To track if a video is playing
    private float smoothFactor = 5f; // Adjust for smoothness (higher = faster)

    public SceneManagerController sceneManagerController; // Reference to SceneManagerController
    private SceneData currentScene; // Current scene being played

    void Start()
    {
        EnableChoicesContainer(isVideoPlaying);
        centerPosition = new Vector2(0, controllerButton.anchoredPosition.y);
        targetPosition = centerPosition; // Initialize target position
        controllerButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!isSliding && !isVideoPlaying) ConfirmChoice();
        });
    }

    public void Initialize(SceneData scene, SceneManagerController manager)
    {
        currentScene = scene;
        sceneManagerController = manager;

        // Update choice texts
        if (scene.choices.Length > 0)
        {
            choice1Text.text = scene.choices[0].choiceText;
            if (scene.choices.Length > 1)
                choice2Text.text = scene.choices[1].choiceText;
        }
        else
        {
            Debug.LogError("No choices available for this scene.");
        }
    }

    void Update()
    {
        if (!isVideoPlaying) // Prevent sliding while video is playing
        {
            HandleSwipe();
        }
        
        // Smoothly interpolate the button position to the target position
        controllerButton.anchoredPosition = Vector2.Lerp(
            controllerButton.anchoredPosition,
            targetPosition,
            Time.deltaTime * smoothFactor
        );
    }

    void HandleSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            isSwiping = true;
            isSliding = false; // Reset sliding flag
        }

        if (isSwiping && Input.GetMouseButton(0))
        {
            Vector2 currentTouchPosition = Input.mousePosition;
            ProcessSwipe(currentTouchPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ResetButtonPosition(); // Reset the button to the center when the button is released
            isSwiping = false;
        }
    }

    void ProcessSwipe(Vector2 currentTouchPosition)
    {
        Vector2 swipeDelta = currentTouchPosition - startTouchPosition;

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // Horizontal swipe
        {
            isSliding = true; // Mark that a slide action is happening

            if (swipeDelta.x > 50) // Swipe right threshold
            {
                HighlightChoice(2);
            }
            else if (swipeDelta.x < -50) // Swipe left threshold
            {
                HighlightChoice(1);
            }
        }
    }

    void HighlightChoice(int choice)
    {
        selectedChoice = choice;

        if (choice == 1)
        {
            targetPosition = new Vector2(leftPosition.x, controllerButton.anchoredPosition.y); // Move button to the left while preserving Y
            choice1Text.color = Color.yellow; // Highlight Choice 1
            choice2Text.color = Color.gray; // Unhighlight Choice 2
        }
        else if (choice == 2)
        {
            targetPosition = new Vector2(rightPosition.x, controllerButton.anchoredPosition.y); // Move button to the right while preserving Y
            choice1Text.color = Color.gray; // Unhighlight Choice 1
            choice2Text.color = Color.yellow; // Highlight Choice 2
        }
    }

    void ResetButtonPosition()
    {
        targetPosition = new Vector2(centerPosition.x, controllerButton.anchoredPosition.y); // Reset target to center

        // Maintain the highlight for the currently selected choice
        if (selectedChoice == 1)
        {
            choice1Text.color = Color.yellow; // Keep Choice 1 highlighted
            choice2Text.color = Color.gray; // Unhighlight Choice 2
        }
        else if (selectedChoice == 2)
        {
            choice1Text.color = Color.gray; // Unhighlight Choice 1
            choice2Text.color = Color.yellow; // Keep Choice 2 highlighted
        }
    }

    public void ConfirmChoice()
    {
        Debug.Log($"Choice {selectedChoice} Confirmed");

        if (currentScene != null && currentScene.choices.Length >= selectedChoice)
        {
            var chosen = currentScene.choices[selectedChoice - 1];
            Debug.Log("Chosen Choice: " + chosen);
            if (sceneManagerController != null)
            {
                Debug.Log("Playing the next Scene");
                isVideoPlaying = true; // Mark video as playing
                sceneManagerController.videoPlayerController.PlayChoice(chosen);
                EnableChoicesContainer(isVideoPlaying);
            }
            else
            {
                Debug.LogError("SceneManagerController not assigned.");
            }
        }
        else
        {
            Debug.LogError("Invalid choice or no choices available.");
        }
    }

    public void EnableChoicesContainer(bool isVidPlaying)
    {
        if (isVidPlaying)
        {
            choicesContainer.gameObject.SetActive(false);
            controllerButton.GetComponent<Button>().interactable = false;
            return;
        }

        choicesContainer.gameObject.SetActive(true);
        controllerButton.GetComponent<Button>().interactable = true;
    }

    public void OnVideoEnd()
    {
        Debug.Log("Set Video to Not Playing");
        isVideoPlaying = false; // Mark video as stopped
    }
}
