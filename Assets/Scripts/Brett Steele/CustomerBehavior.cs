using UnityEngine;

public class CustomerBehavior : MonoBehaviour
{
    enum CustomerState {
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
    [SerializeField] CustomerState currentCustomerState;
    [SerializeField] private CustomerState previousCustomerState;
    [SerializeField] uint tipMoney;
    [SerializeField] float stateTimer;
    [SerializeField] uint interruptTimer;
    [SerializeField] int followDistance;
    [SerializeField] GameObject TEMP_Table;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCustomerState = CustomerState.INIT;
        previousCustomerState = CustomerState.INIT;
    }

    // Update is called once per frame
    void Update()
    {
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
                break;
            default:
                currentCustomerState = CustomerState.LEAVING;
                break;
        }
    }

    void updateCustomerStateTimer() {
        stateTimer -= Time.deltaTime;
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
        
    }
}
