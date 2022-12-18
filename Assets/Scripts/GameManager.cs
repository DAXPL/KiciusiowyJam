using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public DoorManager doorManager;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerStart;
    [SerializeField] private float timePerRoom;
    private bool timeFly = true;
    private float timePassed;

    private PlayerMovement player;
    public int score = 0;
    [Header("Question")]
    [SerializeField] private List<Question> questionsBase = new List<Question>();
    private List<Question> questionsLeft = new List<Question>();
    private Question currentQuestion;
    [Space]
    [SerializeField] private TextMeshProUGUI display;
    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private TextMeshProUGUI timerDisplay;
    [Space]
    [SerializeField] private GameObject DeathScreen;
    [SerializeField] private TextMeshProUGUI bestScoreDisplay;
    void Start()
    {
        if(Instance== null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple GameManagers!");
            Destroy(this);
            return;
        }
        //Application.targetFrameRate = 120;
        if(playerStart != null)
        {
            player = Instantiate(playerPrefab, playerStart.transform.position, Quaternion.identity, null).GetComponent<PlayerMovement>();
        }
        DeathScreen.SetActive(false);
        ResetChamber();
    }
    private void Update()
    {
        if (timeFly)
        {
            timePassed+=Time.deltaTime;
            timerDisplay.text = (timePerRoom-timePassed).ToString("F0");
            if (timePassed > timePerRoom)
            {
                KillPlayer();
            }
        }
    }
    public void TeleportPlayerTo(Transform position)
    {
        if(player != null)
        {
            player.Teleport(position);
        }
    }

    public void Score()
    {
        score++;
        points.text = $"Score: {score}";
        //zagraj animacje otwierania drzwi
    }
    public void ResetChamber()
    {
        NextQuestion();
        TeleportPlayerTo(playerStart);
        doorManager.ResetDoors();
        timePassed = 0;
        timeFly = true;
    }
    public void NextQuestion()
    {
        if(questionsLeft.Count <= 0)
        {
            questionsLeft = questionsBase.GetRange(0,questionsBase.Count);
        }

        currentQuestion = questionsLeft[Random.Range(0, questionsLeft.Count)];
        questionsLeft.Remove(currentQuestion);
        display.text = GetCurrentDescription();
    }
    public bool GetCorrectAnswer()
    {
        return currentQuestion.correctAnswer;
    }
    public string GetCurrentDescription()
    {
        return currentQuestion.desc;
    }
    public void KillPlayer()
    {
        player.Die();
        int bestScore = PlayerPrefs.GetInt("BestScore",0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore",score);
            PlayerPrefs.Save();
            bestScoreDisplay.text = $"Nowy rekord! {score} punktów ";
        }
        else
        {
            bestScoreDisplay.text = $"Zdoby³eœ {score} punktów ";
        }
        DeathScreen.SetActive(true);
    }
    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void ZaWardo()
    {
        timeFly = false;
    }
}
