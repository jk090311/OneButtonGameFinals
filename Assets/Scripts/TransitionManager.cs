using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
  public static bool isNewGame = false;
  public void SettingsPageOpen (Animator animator) => animator.SetBool("isSettingsPageOpen", true);
  public void SettingsPageClose (Animator animator) => animator.SetBool("isSettingsPageOpen", false);
  public void GameSelOpen (Animator animator) => animator.SetBool("isGameSelOpen", true);
  public void GameSelClose (Animator animator) => animator.SetBool("isGameSelOpen", false);
  public static void EndingPromptOpen (Animator animator) => animator.SetBool("isEnding", true);
  public static void EndingPromptClose (Animator animator) => animator.SetBool("isEnding", false);
  public void SetIsNewGame(bool isNewGame) => TransitionManager.isNewGame = isNewGame;
}
