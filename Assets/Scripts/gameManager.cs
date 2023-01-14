using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public Button startButton;
    public Text level;
    public Button homeButton;

   private void Awake()  {
       Time.timeScale = 0f;
   }

   private void OnEnable() {
      startButton.onClick.AddListener(StartGame);
   }

   private void OnDisable() {
      startButton.onClick.RemoveListener(StartGame);
   }

   private void StartGame()  {
      Time.timeScale = 1f;

      // Hides the button
      startButton.gameObject.SetActive(false);
      level.gameObject.SetActive(false);
      homeButton.gameObject.SetActive(true);

   }
}
