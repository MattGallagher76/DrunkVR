using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Import the Input System

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Initial,
        EatingDecision,
        PregameDecision,
        PartyDrinkOffer,
        StrangerOffer,
        PeerPressure,
        DriveOrRideDecision,
        CrashOrHangover,
        End
    }

    public static GameStateManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    public Text uiText;
    public GameObject food;
    public GameObject table;
    public GameObject blackScreen;
    public GameObject liquor;
    public GameObject beer;
    public GameObject water;
    public GameObject eatingiMessage;
    public GameObject beeriMessage;
    public GameObject liqouriMessage;
    public Vector3 targetPosition;
    public float moveDuration = 1.0f;

    private string[] texts = {
        "You haven't eaten anything all day.",    // 0
        "You did not eat.",                      // 1
        "You ate.",                              // 2
        "Pregame? water is far right, beer middle, liquor left.", // 3
        "You chose water.",                      // 4
        "You chose liquor.",                     // 5
        "You chose beer.",                       // 6
        "You are at the party.",                 // 7
        "Your friend offers you another drink.", // 8
        "A stranger offers you a drink. This could be dangerous.", // 9
        "Your friends are pressuring you to drink.", // 10
        "Do you want to drive home or take a ride?", // 11
        "You crashed or woke up with a hangover.",  // 12
        "The game is over." ,                    // 13
        "Hey! Have a drink on me!",              //14
    };
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
        HandleMouseInput();
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
                currentIndex = 0;
                StartCoroutine(ShowText());
                break;

            case GameState.EatingDecision:
                EnableChoices();
                break;

            case GameState.PregameDecision:
                EnableChoices();
                currentIndex = 3;
                StartCoroutine(ShowText());
                break;

            case GameState.PartyDrinkOffer:
                currentIndex = 14;
                StartCoroutine(ShowText());

                EnableChoices();

                
                
                break;


            case GameState.StrangerOffer:
                currentIndex = 9;
                StartCoroutine(ShowText());
                break;

            case GameState.PeerPressure:
                currentIndex = 10;
                StartCoroutine(ShowText());
                break;

            case GameState.DriveOrRideDecision:
                currentIndex = 11;
                StartCoroutine(ShowText());
                break;

            case GameState.CrashOrHangover:
                currentIndex = 12;
                StartCoroutine(ShowText());
                break;

            case GameState.End:
                currentIndex = 13;
                StartCoroutine(ShowText());
                break;
        }
    }

    IEnumerator ShowText()
    {
        string textToShow = texts[currentIndex];
        for (int i = 0; i <= textToShow.Length; i++)
        {
            uiText.text = textToShow.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }

        if (currentIndex == 0)
        {
            ChangeState(GameState.EatingDecision);
        }
        yield return new WaitForSeconds(3f);
    }

    void EnableChoices()
    {
        if (CurrentState == GameState.EatingDecision)
        {
            food.SetActive(true);
        }
        else if (CurrentState == GameState.PregameDecision)
        {
            water.SetActive(true);
            liquor.SetActive(true);
            beer.SetActive(true);
        }
        else if (CurrentState == GameState.PartyDrinkOffer)
        {
            water.SetActive(true);
            liquor.SetActive(true);
            beer.SetActive(true);
        }
    }

    void DisableChoices()
    {
        food.SetActive(false);
        water.SetActive(false);
        liquor.SetActive(false);
        beer.SetActive(false);
    }

    public void OnEat()
    {
        Debug.Log("Player chose to eat.");
        DisableChoices();
        currentIndex = 2;
        StartCoroutine(ShowText());
        StartCoroutine(WaitAndTransition(3f, GameState.PregameDecision));
    }

    public void OnChooseOther()
    {
        Debug.Log("Player chose something else.");
        if (eatingiMessage != null)
        {
            eatingiMessage.SetActive(true);
            StartCoroutine(MoveMessageUpwards(eatingiMessage));
        }
        currentIndex = 1;
        DisableChoices();
        StartCoroutine(ShowText());
        StartCoroutine(WaitAndTransition(3f, GameState.PregameDecision));
    }

    public void OnWater()
    {
        Debug.Log("Player chose water.");
        currentIndex = 4;
        DisableChoices();
        StartCoroutine(ShowText());

        if(CurrentState == GameState.PregameDecision)
        {
            StartCoroutine(WaitAndTransition(3f, GameState.PartyDrinkOffer));

        }else if(CurrentState == GameState.PartyDrinkOffer)
        {
            StartCoroutine(WaitAndTransition(3f, GameState.StrangerOffer));


        }
    }

    public void OnLiquor()
    {
        Debug.Log("Player chose liquor.");
        if (liqouriMessage != null)
        {
            liqouriMessage.SetActive(true);
            StartCoroutine(MoveMessageUpwards(liqouriMessage));
        }
        currentIndex = 5;
        DisableChoices();
        StartCoroutine(ShowText());
        if (CurrentState == GameState.PregameDecision)
        {
            StartCoroutine(WaitAndTransition(3f, GameState.PartyDrinkOffer));

        }
        else if (CurrentState == GameState.PartyDrinkOffer)
        {
            StartCoroutine(WaitAndTransition(3f, GameState.StrangerOffer));


        }
    }

    public void OnBeer()
    {
        Debug.Log("Player chose beer.");
        if (beeriMessage != null)
        {
            beeriMessage.SetActive(true);
            StartCoroutine(MoveMessageUpwards(beeriMessage));
        }
        currentIndex = 6;
        DisableChoices();
        StartCoroutine(ShowText());
        if (CurrentState == GameState.PregameDecision)
        {
            StartCoroutine(WaitAndTransition(3f, GameState.PartyDrinkOffer));

        }
        else if (CurrentState == GameState.PartyDrinkOffer)
        {
            StartCoroutine(WaitAndTransition(3f, GameState.StrangerOffer));


        }
    }

    IEnumerator WaitAndTransition(float waitTime, GameState nextState)
    {
        yield return new WaitForSeconds(waitTime);
        SceneTransition(nextState);
    }

    IEnumerator MoveMessageUpwards(GameObject message)
    {
        RectTransform rectTransform = message.GetComponent<RectTransform>();
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 targetYPosition = new Vector2(startPosition.x, targetPosition.y);

        float elapsedTime = 0;

        while (elapsedTime < moveDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetYPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetYPosition;
    }

    private void HandleMouseInput()
    {
        if (CurrentState == GameState.EatingDecision || CurrentState == GameState.PregameDecision || CurrentState == GameState.PartyDrinkOffer)
        {
            var mouse = Mouse.current;
            if (mouse.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (CurrentState == GameState.EatingDecision)
                    {
                        if (hit.collider.gameObject == food) OnEat();
                        else if (hit.collider.gameObject == table) OnChooseOther();
                    }
                    else if (CurrentState == GameState.PregameDecision || CurrentState == GameState.PartyDrinkOffer)
                    {
                        if (hit.collider.gameObject == water) OnWater();
                        else if (hit.collider.gameObject == liquor) OnLiquor();
                        else if (hit.collider.gameObject == beer) OnBeer();
                    }
                }
            }
        }
    }

    private void HandleObjectsDuringTransition()
    {
        eatingiMessage.SetActive(false);
        beeriMessage.SetActive(false);
        liqouriMessage.SetActive(false);
        uiText.text = "";
    }

    IEnumerator FadeBlackScreenInAndOut(GameState nextState)
    {
        blackScreen.SetActive(true);
        Image blackImage = blackScreen.GetComponent<Image>();
        Color color = blackImage.color;

        color.a = 0;
        blackImage.color = color;
        float fadeDuration = 2f;

        while (color.a < 1)
        {
            color.a += Time.deltaTime / fadeDuration;
            blackImage.color = color;
            yield return null;
        }

        HandleObjectsDuringTransition();

        yield return new WaitForSeconds(2f);

        while (color.a > 0)
        {
            color.a -= Time.deltaTime / fadeDuration;
            blackImage.color = color;
            yield return null;
        }

        blackScreen.SetActive(false);
        ChangeState(nextState);
    }

    private void SceneTransition(GameState nextState)
    {
        StartCoroutine(FadeBlackScreenInAndOut(nextState));
    }
}
