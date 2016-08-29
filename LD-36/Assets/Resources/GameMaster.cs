using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
  public static GameMaster Instance = null;
  public float Timer;

  private float timeLeft;
  private bool timeOn;
  private Text textGO;

  // Use this for initialization
  void Awake () {
    if (Instance == null) {
      Instance = this;
      DontDestroyOnLoad(this);
    } else if (Instance != this) {
      Destroy(gameObject);
    }
    SceneManager.sceneLoaded += SceneManager_sceneLoaded;
  }

  private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
    if (arg0.buildIndex > 0) {
      timeLeft = Timer;
      timeOn = true;
      textGO = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
    }
  }

  public void FixedUpdate() {
    if (timeOn) {
      timeLeft -= Time.deltaTime;
      if (timeLeft <= 0.0001f) {
        timeOn = false;
        EndGame();
      }
      textGO.text = string.Format("{0:00}:{1:00}", Mathf.Floor(timeLeft / 60), Mathf.Floor(timeLeft % 60));
    }
  }

  public void Update() {
    if (Input.GetButtonDown("Cancel")) {
      if (SceneManager.GetActiveScene().buildIndex == 0) {
        Application.Quit();
      } else {
        GoToMenu();
      }
    }
  }

  public void EndGame() {
    textGO.enabled = false;
    GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformerMotor2D>().enabled = false;
    GameObject.FindGameObjectWithTag("FadeScreen").GetComponent<Animator>().SetTrigger("Fade Out");
  }

  public void GoToMenu() {
    SceneManager.LoadScene(0);
  }

  public void GoToLevel() {
    SceneManager.LoadScene(1);
  }
}
