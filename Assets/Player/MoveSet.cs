using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    /* TAG:
     * Map
    */

    /* Layer:
     * Map
     */

    private GrapplingHook grapplingHook;
    


    public KeysConfig keysConfig;

    [SerializeField]
    private Rigidbody2D player;
    private int jumpCount;
    public int maxJumps = 2;
    public float moveSpeed = 10f;
    private Vector3 MovePlayerX;
    private Vector3 direction;
    public Vector3 position;

    [SerializeField]
    public static float jumpForceY = 3f;
    private Vector2 jumpForce;

    [SerializeField]
    private static string levelName;




    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        jumpCount = maxJumps;
        jumpForce = new Vector2(0, jumpForceY);
    

    keysConfig = new KeysConfig();
        if (keysConfig == null)
        {
            Debug.LogError("keysConfig no está inicializado.");
            return;
        }

        grapplingHook = gameObject.AddComponent<GrapplingHook>();
    }

    void UpdateValues()
    {
        position.Set(transform.position.x, transform.position.y, transform.position.z);

        direction = new Vector2(0, 0);
        if (Input.GetKey(keysConfig.GetCodeKey("Left"))) { direction.x--; }
        if (Input.GetKey(keysConfig.GetCodeKey("Right"))) { direction.x++; }
        if (Input.GetKey(keysConfig.GetCodeKey("Up"))) { direction.y++; }
        if (Input.GetKey(keysConfig.GetCodeKey("Down"))) { direction.y--; }
        if (Input.GetKey(KeyCode.Q)) { Debug.Log("to see: " + direction); }
        direction = direction.normalized;
    }


    void Update()
    {
        
        UpdateValues();
        PlayerMove();
        PlayerSkill();

        MovePlayerX.x = (keysConfig.GetHorizontalAxis());
    }

    
    private void FixedUpdate()
    {
        MoveX();
       
    }



    void PlayerSkill()
    {
        //Nota: No estoy cual metodo sea ma intuitivo para el gancho, el del doble salto mas direccion
        //o usar la tecla especial (en este caso seria la c) mmmmmm
        //if (Input.GetKeyDown(keysConfig.GetCodeKey("Special")) && jumpCount < maxJumps)
        if (Input.GetKeyDown(keysConfig.GetCodeKey("Jump")) && jumpCount < maxJumps - 1)
        {
            grapplingHook?.Main(position, direction);
        }
        if (Input.GetKeyUp(keysConfig.GetCodeKey("Jump")))
        {
            // What is this
            grapplingHook?.Destroy();
        }
    }
        
    private void MoveX()
    {
        player.AddForce(MovePlayerX * moveSpeed, ForceMode2D.Impulse);
    }

    void PlayerMove()
    {
        if (Input.GetKeyDown(keysConfig.GetCodeKey("Jump")))
        {
            if (player != null && jumpCount > 0)
            {
                player.AddForce(jumpForce, ForceMode2D.Impulse);
            }
            jumpCount--;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            jumpCount = maxJumps;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger: " + other.gameObject.name);
        if (other.CompareTag("Rot"))
        {
            Respawn();
        }
    }
    private Vector3 startPosition = new Vector3(-5.3f, -3.09f, 0f);
    void Respawn()
    {
        Debug.Log("Respawn, ASCENCION");

        if (Stats.Instance)
        {

        }
        transform.position = startPosition;
        player.velocity = Vector2.zero;
        grapplingHook?.Destroy();
    }
}
