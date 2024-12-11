using UnityEngine;

public class TransitionManager : MonoBehaviour
{
  public static bool isNewGame = false;
  private string boolName;
  public GameObject mainMenuPanel;

  public void SetBoolAnimator(string boolName) => this.boolName = boolName;

  public void OpenPanel(Animator animator) => animator.SetBool(boolName, true);

  public void ClosePanel(Animator animator) => animator.SetBool(boolName, false);

  public void ShowMainMenu() => mainMenuPanel.SetActive(true);

  public void HideMainMenu() => mainMenuPanel.SetActive(false);

  public static void EndingPromptOpen(Animator animator) => animator.SetBool("isEnding", true);

  public static void EndingPromptClose(Animator animator) => animator.SetBool("isEnding", false);

  public void SetIsNewGame(bool isNewGame) => TransitionManager.isNewGame = isNewGame;
}
