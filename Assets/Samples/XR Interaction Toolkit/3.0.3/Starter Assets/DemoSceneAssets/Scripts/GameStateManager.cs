using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Initial,
        EatingDecision,
        PregameDecision,
        Party,
        DrinkOffer,
        StrangerOffer,
        PeerPressure,
        DriveOrRideDecision,
        CrashOrHangover,
        End
    }

    public static GameStateManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    public Text uiText;
    public GameObject sphere;  // For eating decision
    public GameObject otherObject;  // For other decision
    public GameObject blackScreen;

    private string[] texts = { "You haven't eaten anything all day.", "Do you want to eat?" };
    private int currentIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.Initial);
    }

    private void Update()
    {
        HandleKeyPresses();
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        HandleState(newState);
    }

    private void HandleState(GameState state)
    {
        switch (state)
        {
            case GameState.Initial:
                uiText.text = "";
                StartCoroutine(ShowText());
                break;

            case GameState.EatingDecision:
                EnableChoices();  // Enable sphere and otherObject
                break;

            case GameState.PregameDecision:
                uiText.text = "You decided to pregame.";
                break;

            case GameState.Party:
                uiText.text = "You are at the party.";
                break;

            case GameState.DrinkOffer:
                uiText.text = "Your friend offers you another drink.";
                break;

            case GameState.StrangerOffer:
                uiText.text = "A stranger offers you a drink. This could be dangerous.";
                break;

            case GameState.PeerPressure:
                uiText.text = "Your friends are pressuring you to drink.";
                break;

            case GameState.DriveOrRideDecision:
                uiText.text = "Do you want to drive home or take a ride?";
                break;

            case GameState.CrashOrHangover:
                uiText.text = "You crashed or woke up with a hangover.";
                break;

            case GameState.End:
                uiText.text = "The game is over.";
                break;
        }
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= texts[currentIndex].Length; i++)
        {
            uiText.text = texts[currentIndex].Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }

        if (currentIndex == 0)
        {
            currentIndex++;
            ChangeState(GameState.EatingDecision);
        }
    }

    void EnableChoices()
    {
        sphere.SetActive(true);
        otherObject.SetActive(true);
    }

    void DisableChoices()
    {
        sphere.SetActive(false);
        otherObject.SetActive(false);
    }

    public void OnEat()
    {
        Debug.Log("Player chose to eat.");
        DisableChoices();
        ShowText();
        ChangeState(GameState.PregameDecision);
    }

    public void OnChooseOther()
    {
        Debug.Log("Player chose something else.");
        DisableChoices();
        ChangeState(GameState.End);
    }


    private void HandleKeyPresses()
    {
        if (Input.GetKeyDown(KeyCode.A) && CurrentState == GameState.EatingDecision)
        {
            Debug.Log("HELP MEEEEEEEEEEE");
            OnEat();
        }
        else if (Input.GetKeyDown(KeyCode.S) && CurrentState == GameState.EatingDecision)
        {
            OnChooseOther();
        }
    }
}
