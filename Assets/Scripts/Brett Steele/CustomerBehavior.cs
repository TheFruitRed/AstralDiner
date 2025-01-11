using UnityEngine;
using TMPro;

public class CustomerBehavior : MonoBehaviour
{
    public enum CustomerState {
        INIT,
        QUEUEING,
        ESCORTED,
        LOOK_AT_MENU,
        WAITING_TO_PLACE_ORDER,
        PLACING_ORDER,
        WAITING_FOR_FOOD,
        EATING,
        CHECK_PLEASE,
        LEAVING,
        INTERRUPTION_QUESTION,
        INTERRUPTION_ACTION
    }

    [SerializeField] GameObject player;
    [SerializeField] public CustomerState currentCustomerState;
    [SerializeField] private CustomerState previousCustomerState;
    [SerializeField] uint tipMoney;
    [SerializeField] float stateTimer;
    [SerializeField] uint interruptTimer;
    [SerializeField] int followDistance;
    [SerializeField] GameObject TEMP_Table;
    [SerializeField] TMP_Text TEMP_Score;
    [SerializeField] TMP_Text TEMP_Timer;
    [SerializeField] uint TEMP_Score_Money;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCustomerState = CustomerState.INIT;
        previousCustomerState = CustomerState.INIT;
        TEMP_Score_Money = 0;
        TEMP_Timer.text = "Time: N/A";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            resetCharacter();
        }

        TEMP_Score.text = "$" + Mathf.Ceil(TEMP_Score_Money);

        switch (currentCustomerState) {
            case CustomerState.INIT:
                initState();
                break;
            case CustomerState.QUEUEING:
                queueingState();
                break;
            case CustomerState.ESCORTED:
                escortedState();
                break;
            case CustomerState.LOOK_AT_MENU:
                lookAtMenuState();
                break;
            case CustomerState.WAITING_TO_PLACE_ORDER:
                waitingToPlaceOrderState();
                break;
            case CustomerState.PLACING_ORDER:
                placingOrderState();
                break;
            case CustomerState.WAITING_FOR_FOOD:
                waitingForFoodState();
                break;
            case CustomerState.EATING:
                eatingState();
                break;
            case CustomerState.CHECK_PLEASE:
                checkPleaseState();
                break;
            case CustomerState.LEAVING:
                leavingState();
                break;
            case CustomerState.INTERRUPTION_QUESTION:
                break;
            case CustomerState.INTERRUPTION_ACTION:
                break;
            default:
                break;
        }
    }

    public void updateCustomerState() {
        Debug.Log("Moving to next state");
        if (currentCustomerState == CustomerState.INTERRUPTION_ACTION || currentCustomerState == CustomerState.INTERRUPTION_QUESTION) {
            CustomerState temp = previousCustomerState;
            previousCustomerState = currentCustomerState;
            currentCustomerState = temp;
            return;
        }

        previousCustomerState = currentCustomerState;

        switch (currentCustomerState) {
            case CustomerState.INIT:
                currentCustomerState = CustomerState.QUEUEING;
                break;
            case CustomerState.QUEUEING:
                currentCustomerState = CustomerState.ESCORTED;
                break;
            case CustomerState.ESCORTED:
                lookAtMenuInit();
                currentCustomerState = CustomerState.LOOK_AT_MENU;
                break;
            case CustomerState.LOOK_AT_MENU:
                currentCustomerState = CustomerState.WAITING_TO_PLACE_ORDER;
                break;
            case CustomerState.WAITING_TO_PLACE_ORDER:
                currentCustomerState = CustomerState.PLACING_ORDER;
                break;
            case CustomerState.PLACING_ORDER:
                currentCustomerState = CustomerState.WAITING_FOR_FOOD;
                break;
            case CustomerState.WAITING_FOR_FOOD:
                eatingInit();
                currentCustomerState = CustomerState.EATING;
                break;
            case CustomerState.EATING:
                currentCustomerState = CustomerState.CHECK_PLEASE;
                break;
            case CustomerState.CHECK_PLEASE:
                currentCustomerState = CustomerState.LEAVING;
                break;
            case CustomerState.LEAVING:
                currentCustomerState = CustomerState.INIT;
                break;
            default:
                currentCustomerState = CustomerState.LEAVING;
                break;
        }
    }

    void updateCustomerStateTimer() {
        stateTimer -= Time.deltaTime;
        TEMP_Timer.text = "Time: " + Mathf.Ceil(stateTimer);
    }

    void initState() {
        updateCustomerState();
    }

    void queueingState() {
        return;
    }

    void escortedState() {
        transform.position = player.transform.position - player.GetComponent<PlayerMovement>().forwardVector * followDistance;
    }

    void lookAtMenuState() {
        updateCustomerStateTimer();
        if (stateTimer <= 0) {
            stateTimer = 0;
            updateCustomerState();
        }
    }
    void lookAtMenuInit() {
        transform.position = TEMP_Table.transform.position;
        stateTimer = 5;
    }

    void waitingToPlaceOrderState() {

    }

    void placingOrderState() {
        updateCustomerState();
    }

    void waitingForFoodState() {

    }

    void eatingState() {
        updateCustomerStateTimer();
        if (stateTimer <= 0) {
            stateTimer = 0;
            updateCustomerState();
        }
    }
    void eatingInit() {
        stateTimer = 10;
    }

    void checkPleaseState() {

    }

    void leavingState() {
        TEMP_Score_Money += tipMoney;
        resetCharacter();
    }

    void resetCharacter() {
        transform.position = new Vector3(-6.42f, 0.0f, 0.0f);
        stateTimer = 0;
        TEMP_Timer.text = "Time: " + Mathf.Ceil(stateTimer);
        previousCustomerState = CustomerState.INIT;
        currentCustomerState = CustomerState.INIT;
    }
}
