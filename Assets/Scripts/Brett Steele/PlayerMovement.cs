using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    byte inputBitArray;
    Vector2 playerVelocity;
    [SerializeField] GameObject player;
    [SerializeField] int moveSpeed;

    [SerializeField] GameObject customer;
    private bool isCarryingFood; 

    void Start()
    {
        inputBitArray = 0;
        playerVelocity = new Vector2(0,0);
        isCarryingFood = false;
    }

    // Update is called once per frame
    void Update()
    {
        inputBitArray = getPlayerInput();
        movePlayer(inputBitArray);
    }

    byte getPlayerInput() {
        inputBitArray = 0;
        
        if (Input.GetKey("w")) {
            inputBitArray |= 1;
        }
        if (Input.GetKey("a")) {
            inputBitArray |= 2;
        }
        if (Input.GetKey("s")) {
            inputBitArray |= 4;
        }
        if (Input.GetKey("d")) {
            inputBitArray |= 8;
        }

        return inputBitArray;
    }

    void movePlayer(byte inputVals) {

        playerVelocity.x = 0;
        playerVelocity.y = 0;

        // W Pressed
        if ((inputVals & 1) == 1) {
            playerVelocity.y = 1;
        }
        // A Pressed
        if ((inputVals & 2) == 2) {
            playerVelocity.x = -1;
        }
        // S Pressed
        if ((inputVals & 4) == 4) {
            playerVelocity.y = -1;
        }
        // D Pressed
        if ((inputVals & 8) == 8) {
            playerVelocity.x = 1;
        }

        playerVelocity.Normalize();
        player.GetComponent<Rigidbody2D>().linearVelocity = playerVelocity * moveSpeed;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject collidedObj = collision.transform.parent.gameObject;
            if (collidedObj.name.Equals("Krustomer"))
            {
                if(customer.GetComponent<CustomerBehavior>().currentCustomerState == CustomerBehavior.CustomerState.QUEUEING)
                {
                    customer.GetComponent<CustomerBehavior>().updateCustomerState();
                }                
            }
            else if (collidedObj.name.Equals("Table"))
            {
                if (customer.GetComponent<CustomerBehavior>().currentCustomerState == CustomerBehavior.CustomerState.ESCORTED
                    || customer.GetComponent<CustomerBehavior>().currentCustomerState == CustomerBehavior.CustomerState.WAITING_TO_PLACE_ORDER
                    || (customer.GetComponent<CustomerBehavior>().currentCustomerState == CustomerBehavior.CustomerState.WAITING_FOR_FOOD && isCarryingFood)
                    || customer.GetComponent<CustomerBehavior>().currentCustomerState == CustomerBehavior.CustomerState.CHECK_PLEASE)
                {
                    isCarryingFood = false;
                    customer.GetComponent<CustomerBehavior>().updateCustomerState();
                }
            }
            else if (collidedObj.name.Equals("OrderTable"))
            {
                if(customer.GetComponent<CustomerBehavior>().currentCustomerState == CustomerBehavior.CustomerState.PLACING_ORDER)
                {
                    isCarryingFood = true;
                    customer.GetComponent<CustomerBehavior>().updateCustomerState();
                }
            }
        }
    }
}
