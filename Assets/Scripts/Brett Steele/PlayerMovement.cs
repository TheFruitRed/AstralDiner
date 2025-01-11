using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    byte inputBitArray;
    Vector2 playerVelocity;
    [SerializeField] public Vector3 forwardVector;
    [SerializeField] GameObject player;
    [SerializeField] int moveSpeed;

    [SerializeField] GameObject customer;
    [SerializeField] private bool isCarryingFood; 

    
    [SerializeField] private bool isCollidingCustomer; 
    [SerializeField] private bool isCollidingTable; 
    [SerializeField] private bool isCollidingOrderTable; 

    void Start()
    {
        inputBitArray = 0;
        playerVelocity = new Vector2(0,0);
        forwardVector = Vector3.zero;
        isCarryingFood = false;
    }

    // Update is called once per frame
    void Update()
    {
        inputBitArray = getPlayerInput();
        movePlayer(inputBitArray);
        updateForwardVector(inputBitArray);

        if (Input.GetKeyDown("space"))
        {
            
            if (isCollidingTable)
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

            if (isCollidingOrderTable)
            {
                if(customer.GetComponent<CustomerBehavior>().currentCustomerState == CustomerBehavior.CustomerState.WAITING_FOR_FOOD)
                {
                    isCarryingFood = true;
                }
            }
            
            if (isCollidingCustomer)
            {
                if(customer.GetComponent<CustomerBehavior>().currentCustomerState == CustomerBehavior.CustomerState.QUEUEING)
                {
                    customer.GetComponent<CustomerBehavior>().updateCustomerState();
                }                
            }
        }
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

    void updateForwardVector(byte inputVals) {

        if ((inputBitArray & 15) == 0) {
            return;
        }

        // W Pressed
        if ((inputVals & 1) == 1) {
            forwardVector = Vector3.up;
        }
        // A Pressed
        if ((inputVals & 2) == 2) {
            forwardVector = Vector3.left;
        }
        // S Pressed
        if ((inputVals & 4) == 4) {
            forwardVector = Vector3.down;
        }
        // D Pressed
        if ((inputVals & 8) == 8) {
            forwardVector = Vector3.right;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Krustomer"))
        {
            isCollidingCustomer = true;                
        }
        
        if (collision.name.Equals("Table"))
        {
            isCollidingTable = true;                
        }
        
        if (collision.name.Equals("OrderTable"))
        {
            isCollidingOrderTable = true;                
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("Krustomer"))
        {
            isCollidingCustomer = false;                
        }
        
        if (collision.name.Equals("Table"))
        {
            isCollidingTable = false;                
        }
        
        if (collision.name.Equals("OrderTable"))
        {
            isCollidingOrderTable = false;                
        }
    }
}
