using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    byte inputBitArray;
    Vector2 playerVelocity;
    [SerializeField] public Vector3 forwardVector;
    [SerializeField] GameObject player;
    [SerializeField] int moveSpeed;



    void Start()
    {
        inputBitArray = 0;
        playerVelocity = new Vector2(0,0);
        forwardVector = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        inputBitArray = getPlayerInput();
        movePlayer(inputBitArray);
        updateForwardVector(inputBitArray);
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
}
