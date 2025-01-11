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

    [SerializeField] CustomerState currentCustomerState;
    [SerializeField] private CustomerState previousCustomerState;
    [SerializeField] uint tipMoney;
    [SerializeField] uint stateTimer;
    [SerializeField] uint interruptTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCustomerState = INIT;
        previousCustomerState = INIT;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentCustomerState) {
            case INIT:
                break;
            case QUEUEING:
                break;
            case ESCORTED:
                break;
            case LOOK_AT_MENU:
                break;
            case WAITING_TO_PLACE_ORDER:
                break;
            case PLACING_ORDER:
                break;
            case WAITING_FOR_FOOD:
                break;
            case EATING:
                break;
            case CHECK_PLEASE:
                break;
            case LEAVING:
                break;
            case INTERRUPTION_QUESTION:
                break;
            case INTERRUPTION_ACTION:
                break;
            default:
                break;
        }
    }

    void updateCustomerState() {

        if (currentCustomerState == INTERRUPTION_ACTION || currentCustomerState == INTERRUPTION_QUESTION) {
            CustomerState temp = previousCustomerState;
            previousCustomerState = currentCustomerState;
            currentCustomerState = temp;
            return;
        }

        previousCustomerState = currentCustomerState;

        switch (currentCustomerState) {
            case INIT:
                currentCustomerState = QUEUEING;
                break;
            case QUEUEING:
                currentCustomerState = ESCORTED;
                break;
            case ESCORTED:
                currentCustomerState = LOOK_AT_MENU;
                break;
            case LOOK_AT_MENU:
                currentCustomerState = WAITING_TO_PLACE_ORDER;
                break;
            case WAITING_TO_PLACE_ORDER:
                currentCustomerState = PLACING_ORDER;
                break;
            case PLACING_ORDER:
                currentCustomerState = WAITING_FOR_FOOD;
                break;
            case WAITING_FOR_FOOD:
                currentCustomerState = EATING;
                break;
            case EATING:
                currentCustomerState = CHECK_PLEASE;
                break;
            case CHECK_PLEASE:
                currentCustomerState = LEAVING;
                break;
            case LEAVING:
                break;
            default:
                currentCustomerState = LEAVING;
                break;
        }
    }
}
