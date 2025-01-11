using UnityEngine;

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

    [SerializeField] public CustomerState currentCustomerState;
    [SerializeField] private CustomerState previousCustomerState;
    [SerializeField] uint tipMoney;
    [SerializeField] uint stateTimer;
    [SerializeField] uint interruptTimer;

    [SerializeField] CircleCollider2D innerCircleCollider;
    // [SerializeField] CircleCollider2D outerCircleCollider;
    [SerializeField] private GameObject table;

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
                break;
            case CustomerState.QUEUEING:
                break;
            case CustomerState.ESCORTED:
                break;
            case CustomerState.LOOK_AT_MENU:
                break;
            case CustomerState.WAITING_TO_PLACE_ORDER:
                break;
            case CustomerState.PLACING_ORDER:
                break;
            case CustomerState.WAITING_FOR_FOOD:
                break;
            case CustomerState.EATING:
                break;
            case CustomerState.CHECK_PLEASE:
                break;
            case CustomerState.LEAVING:
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
}
