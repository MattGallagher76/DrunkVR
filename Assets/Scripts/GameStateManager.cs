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
        CrashEnd,
        StrangerEnd,
        SafeEnd,
        HangOverEnd,
        End,
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
    public GameObject keys;
    public GameObject phone;
    public BACScript bacScript;
    public Text endingText;
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
        "DRINK THIS OR ELSE.", // 9
        "Your friends are pressuring you to drink.", // 10
        "Do you want to drive home or take a ride?", // 11
        "You crashed or woke up with a hangover.",  // 12
        "The game is over." ,                    // 13
        "Hey! Have a drink on me!",              //14
        "GAME OVER DON'T ACCEPT DRINKS FROM STRANGERS",//15
    };

    private string[] endingTexts =
    {
        "Game Over. Do not accept drinks from strangers.",
        "You blacked out",
        "You got in a car crash",
        "You have bad hangover",
        "you don;t have hangiver good job"
    };

    private int currentIndex = 0;
    private int currEnding = 0;
    

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
        Debug.Log($"Changing state to: {newState}");
        CurrentState = newState;
        HandleState(newState);
    }

    private void HandleState(GameState state)
    {
        Debug.Log($"Handling state: {state}");
        switch (state)
        {
            case GameState.Initial:
                currentIndex = 0;
                StartCoroutine(ShowText(true));
                break;

            case GameState.EatingDecision:
                EnableChoices();
                break;

            case GameState.PregameDecision:
                EnableChoices();
                currentIndex = 3;
                StartCoroutine(ShowText(true));
                break;

            case GameState.PartyDrinkOffer:
                currentIndex = 14;
                StartCoroutine(ShowText(true));
                EnableChoices();
                break;

            case GameState.StrangerOffer:
                Debug.Log("Entering StrangerOffer state");
                currentIndex = 9;
                StartCoroutine(ShowText(true));
                EnableChoices();
                break;

            case GameState.PeerPressure:
                currentIndex = 10;
                StartCoroutine(ShowText(true));
                EnableChoices();
                break;

            case GameState.DriveOrRideDecision:
                currentIndex = 11;
                StartCoroutine(ShowText(true));
                EnableChoices();
                break;

            case GameState.StrangerEnd:
                Debug.Log("Entering StrangerEnd state");
                break;
            case GameState.CrashEnd:
                Debug.Log("Entering StrangerEnd state");
                break;
            case GameState.SafeEnd:
                Debug.Log("Entering StrangerEnd state");
                break;
            case GameState.HangOverEnd:
                Debug.Log("Entering StrangerEnd state");
                break;

            case GameState.End:
                currentIndex = 13;
                StartCoroutine(ShowText(true));
                break;
        }
    }

    IEnumerator ShowText(bool regUi)
    {
        string textToShow;
        if (regUi)
        {
            textToShow = texts[currentIndex];
            Debug.Log($"Showing text: {textToShow}");

            for (int i = 0; i <= textToShow.Length; i++)
            {
                uiText.text = textToShow.Substring(0, i);
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            textToShow = endingTexts[currEnding];
            Debug.Log($"Showing ending text: {textToShow}");
            for (int i = 0; i <= textToShow.Length; i++)
            {
                endingText.text = textToShow.Substring(0, i);
                yield return new WaitForSeconds(0.05f);
            }
        }


        if (currentIndex == 0)
        {
            Debug.Log("Transitioning to EatingDecision state");
            ChangeState(GameState.EatingDecision);
        }
        yield return new WaitForSeconds(3f);
    }

    void EnableChoices()
    {
        Debug.Log($"Enabling choices for state: {CurrentState}");
        if (CurrentState == GameState.EatingDecision)
        {
            food.SetActive(true);
        }
        else if (CurrentState == GameState.PregameDecision || CurrentState == GameState.PartyDrinkOffer || CurrentState == GameState.PeerPressure)
        {
            water.SetActive(true);
            liquor.SetActive(true);
            beer.SetActive(true);
        }
        else if (CurrentState == GameState.StrangerOffer)
        {
            beer.SetActive(true);
        }
        else if (CurrentState == GameState.DriveOrRideDecision)
        {
            keys.SetActive(true);
            phone.SetActive(true);

        }
    }

    void DisableChoices()
    {
        Debug.Log("Disabling all choices");
        food.SetActive(false);
        water.SetActive(false);
        liquor.SetActive(false);
        beer.SetActive(false);
        keys.SetActive(false);
        phone.SetActive(false);
    }

    public void OnEat()
    {
        Debug.Log("Player chose to eat.");
        bacScript.ate = true;

        DisableChoices();
        currentIndex = 2;
        StartCoroutine(ShowText(true));
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
        StartCoroutine(ShowText(true));
        StartCoroutine(WaitAndTransition(3f, GameState.PregameDecision));
    }

    public void OnWater()
    {
        Debug.Log("Player chose water.");
        currentIndex = 4;
        DisableChoices();
        StartCoroutine(ShowText(true));

        if (CurrentState == GameState.PregameDecision)
        {
            StartCoroutine(WaitAndTransition(3f, GameState.PartyDrinkOffer));
        }
        else if (CurrentState == GameState.PartyDrinkOffer)
        {
            StartCoroutine(WaitAndTransition(3f, GameState.StrangerOffer));
        }else if (CurrentState == GameState.PeerPressure)
        {
            StartCoroutine(WaitAndTransition(3f, GameState.DriveOrRideDecision));
        }
    }

    IEnumerator OnStrangerEnd()
    {
        Debug.Log("Entering StrangerEnd coroutine");
        DisableChoices();
        currEnding = 0;
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

        StartCoroutine(ShowText(false));
    }

    IEnumerator OnCrashEnd()
    {
        Debug.Log("Entering CrashEnd coroutine");
        DisableChoices();
        currEnding = 2;
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

        StartCoroutine(ShowText(false));
    }

    IEnumerator OnSafeEnd()
    {
        Debug.Log("Entering SafeEnd coroutine");
        DisableChoices();
        currEnding = 4;
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

        StartCoroutine(ShowText(false));
    }

    IEnumerator OnHangOverEnd()
    {
        Debug.Log("Entering HangOverEnd coroutine");
        DisableChoices();
        currEnding = 3;
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

        StartCoroutine(ShowText(false));
    }



    public void OnLiquor()
    {
        Debug.Log("Player chose liquor.");
        bacScript.updateBAC(0.04f);
        if (liqouriMessage != null)
        {
            liqouriMessage.SetActive(true);
            StartCoroutine(MoveMessageUpwards(liqouriMessage));
        }
        currentIndex = 5;
        DisableChoices();
        StartCoroutine(ShowText(true));
        if (CurrentState == GameState.PregameDecision)
        {
            StartCoroutine(WaitAndTransition(3f, GameState.PartyDrinkOffer));
        }
        else if (CurrentState == GameState.PartyDrinkOffer)
        {
            StartCoroutine(WaitAndTransition(3f, GameState.StrangerOffer));
        }
        else if (CurrentState == GameState.PeerPressure)
        {
            StartCoroutine(WaitAndTransition(3f, GameState.DriveOrRideDecision));
        }
    }

    public void OnBeer()
    {
        Debug.Log("Player chose beer.");
        bacScript.updateBAC(0.02f);
        if (CurrentState == GameState.PregameDecision)
        {
            if (beeriMessage != null)
            {
                beeriMessage.SetActive(true);
                StartCoroutine(MoveMessageUpwards(beeriMessage));
            }
            StartCoroutine(WaitAndTransition(3f, GameState.PartyDrinkOffer));
        }
        else if (CurrentState == GameState.PartyDrinkOffer)
        {
            if (beeriMessage != null)
            {
                beeriMessage.SetActive(true);
                StartCoroutine(MoveMessageUpwards(beeriMessage));
            }
            StartCoroutine(WaitAndTransition(3f, GameState.StrangerOffer));
        }
        else if (CurrentState == GameState.PeerPressure)
        {
            StartCoroutine(WaitAndTransition(3f, GameState.DriveOrRideDecision));
        }

    }



    IEnumerator WaitAndTransition(float waitTime, GameState nextState)
    {
        Debug.Log($"Waiting for {waitTime} seconds before transitioning to {nextState}");
        yield return new WaitForSeconds(waitTime);
        SceneTransition(nextState);
    }

    IEnumerator MoveMessageUpwards(GameObject message)
    {
        Debug.Log($"Moving message {message.name} upwards");
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
        var mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"Raycast hit: {hit.collider.gameObject.name}");
                if (CurrentState == GameState.EatingDecision)
                {
                    if (hit.collider.gameObject == food) OnEat();
                    else if (hit.collider.gameObject == table) OnChooseOther();
                }
                else if (CurrentState == GameState.PregameDecision || CurrentState == GameState.PartyDrinkOffer || CurrentState == GameState.PeerPressure)
                {
                    if (hit.collider.gameObject == water) OnWater();
                    else if (hit.collider.gameObject == liquor) OnLiquor();
                    else if (hit.collider.gameObject == beer) OnBeer();
                }
                else if (CurrentState == GameState.StrangerOffer)
                {
                    if (hit.collider.gameObject == beer)
                    {
                        StartCoroutine(OnStrangerEnd());
                    }else if (hit.collider.gameObject == table)
                    {
                        StartCoroutine(WaitAndTransition(3f, GameState.PeerPressure));
                    }
                }else if(CurrentState == GameState.DriveOrRideDecision)
                {
                    if(hit.collider.gameObject == keys)
                    {
                        
                        if (bacScript.getBAC() > 0.08)
                        {
                            StartCoroutine(OnCrashEnd());
                        }
                        else //drive home
                        {
                            StartCoroutine(OnSafeEnd());
                        }
                        

                    }
                    else if (hit.collider.gameObject == phone)
                    {
                        //GOOD SLEEP
                        
                        if (bacScript.getBAC() <= 0.12)
                        {
                            StartCoroutine(OnSafeEnd());

                        }
                        else
                        {
                            StartCoroutine(OnHangOverEnd());

                        }
                        

                        //HANGOVER

                    }
                }
            }
            else
            {
                Debug.Log("Raycast did not hit anything");
            }
        }
    }

    private void HandleObjectsDuringTransition()
    {
        Debug.Log("Handling objects during transition");
        eatingiMessage.SetActive(false);
        beeriMessage.SetActive(false);
        liqouriMessage.SetActive(false);
        beer.SetActive(false);
        liquor.SetActive(false);
        water.SetActive(false);
        keys.SetActive(false);
        phone.SetActive(false);
        uiText.text = "";
    }

    IEnumerator FadeBlackScreenInAndOut(GameState nextState)
    {
        Debug.Log($"Fading black screen in and out for transition to {nextState}");
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
        Debug.Log($"Initiating scene transition to {nextState}");
        StartCoroutine(FadeBlackScreenInAndOut(nextState));
    }
}
