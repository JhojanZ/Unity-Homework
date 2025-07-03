using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MoveSet : MonoBehaviour
{
    /* TAG:
     * Map
    */

    /* Layer:
     * Map
     */

    private GrapplingHook grapplingHook;

    public KeysConfig keysConfig = new KeysConfig();

    [SerializeField]
    private Rigidbody2D player;
    private int jumpCount;
    public int maxJumps = 2;
    public float moveSpeed = 10f;
    private Vector3 MovePlayerX;
    private Vector3 direction;
    public Vector3 position;
    private int layerNormal;
    private Animator animator;


    public static float jumpForceY = 3f;
    private static Vector2 jumpForce = new Vector2(0, jumpForceY);

    [SerializeField]
    private static string levelName;

    // Variables de intangibilidad
    private bool esIntangible = false;
    [SerializeField] private int intangibleCount = 2;
    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        jumpCount = maxJumps;
        
        if (keysConfig == null)
        {
            Debug.LogError("keysConfig no est� inicializado.");
            return;
        }
        layerNormal = gameObject.layer;

        grapplingHook = gameObject.AddComponent<GrapplingHook>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontr� SpriteRenderer en el GameObject.");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró el Animator en el GameObject.");
        }

    }

    void UpdateValues()
    {
        MovePlayerX.x = (keysConfig.GetHorizontalAxis());
        position.Set(transform.position.x, transform.position.y, transform.position.z);

        direction = new Vector2(0, 0);
        if (Input.GetKey(keysConfig.GetCodeKey("Left"))) { direction.x--; }
        if (Input.GetKey(keysConfig.GetCodeKey("Right"))) { direction.x++; }
        if (Input.GetKey(keysConfig.GetCodeKey("Up"))) { direction.y++; }
        if (Input.GetKey(keysConfig.GetCodeKey("Down"))) { direction.y--; }
        if (Input.GetKey(KeyCode.Q)) { Debug.Log("to see: " + direction); }
        direction = direction.normalized;

        if (direction.x > 0.1f)
            spriteRenderer.flipX = true;
        else if (direction.x < -0.1f)
            spriteRenderer.flipX = false;
    }


    void Update()
    {
        UpdateValues();
        PlayerMove();
        PlayerSkill();

        // Actualizar animaciones
        float velocidadX = Mathf.Abs(player.velocity.x);
        animator.SetFloat("Velocidad", velocidadX);

        bool enElAire = !IsGrounded();
        animator.SetBool("EnElAire", enElAire);

    }
    
    private void FixedUpdate()
    {
        MoveX();
    }

    IEnumerator ActivarIntangibilidad()
    {
        if (intangibleCount > 0 && !esIntangible)
        {
            intangibleCount--;
            esIntangible = true;
            gameObject.layer = LayerMask.NameToLayer("Intangible");

            // Hacer semi-transparente
            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;


            yield return new WaitForSeconds(2f); // duracion de la intangibilidad


            gameObject.layer = layerNormal;
            esIntangible = false;

            // Restaurar opacidad
            color.a = 1f;
            spriteRenderer.color = color;
        }
    }

    void PlayerSkill()
    {
        if (Input.GetKeyDown(keysConfig.GetCodeKey("Jump")) && jumpCount < maxJumps - 1)
        {
            grapplingHook?.Main(position, direction);
        }
        if (Input.GetKeyUp(keysConfig.GetCodeKey("Jump")))
        {
            grapplingHook?.Destroy();
        }

        if (Input.GetKeyDown(keysConfig.GetCodeKey("Special")))
        {
            Debug.Log("Player Intangible");
            StartCoroutine(ActivarIntangibilidad());
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

    public Vector3 startPosition = new Vector3(-5.3f, -3.09f, 0f);
    void Respawn()
    {
        if (Stats.Instance != null)
        {
            Stats.Instance.AddHealth(-1);
            if (Stats.health <= 0)
            {
                Stats.GameOver();
            }
        }
        intangibleCount = 2;
        transform.position = startPosition;
        player.velocity = Vector2.zero;
        grapplingHook?.Destroy();
    }

    bool IsGrounded()
    {
        return Mathf.Abs(player.velocity.y) < 0.01f;
    }

}
