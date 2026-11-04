using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator), typeof(GroundCheck))]
public class PlayerController : MonoBehaviour
{
    #region Tunable Parameters
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    private int _lives = 3; //internal lives value
    [SerializeField] private int maxLives = 9;
    public int lives
    {
        get { return _lives; }
        set
        {
            if (value >  maxLives)
            {
                _lives = value;
            }
            else if (value < 0)
            {
                _lives = 0;
                //go to game over
            }
            else if (_lives > value)
            {
                _lives = value;
                //respawn goes here
            }
            else
            {
                _lives = value;
            }
            Debug.Log($"lives were changed to {_lives}");
        }
    }

    //C++ style getter/setter
    //public void SetLives(int lifeValue)
  //  {
    //    if (lifeValue > maxLives)
       // {
      //      lives = maxLives;
      //  }
      //  else if (lifeValue < 0)
       // {
       //     lives = 0;
            //gameover triggers here
      //  }
      //  else if (lives > lifeValue)
      //  {
      //      lives = lifeValue;
      //  }
 //   }

   // public int GetLives()
   // {  
   //     return lives; 
  //  }
    #endregion

    #region Component References
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private Animator anim;
    private GroundCheck gndChk;
    #endregion

    #region Properties
    private int currentJumps = 0;
    private float currentPowerupDuration = 0;
    private float initialJumpForce;
    public int initialpowerupDuration;

    private Coroutine jumpForceCoroutine = null;
    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        gndChk = GetComponent<GroundCheck>();
        gndChk.Init(bc, rb);

        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        initialJumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        gndChk.CheckGround();
        
        //collect inputs for current frame
        //bool upPressed = Input.GetAxisRaw("Vertical") > 0f;
        float moveX = Input.GetAxisRaw("Horizontal");
        bool jumpPresssed = Input.GetButtonDown("Jump");
        bool fireInput = Input.GetButtonDown("Fire1");


        //apply mechanics based on inputs
        rb.linearVelocityX = moveX * moveSpeed;

        if (jumpPresssed && currentJumps < maxJumps)
        {
            currentJumps++;
            gndChk.SetGroundAfterJump();
            rb.linearVelocityY = 0f;
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        }



        if (moveX != 0f) SpriteFlip(moveX);
        if (gndChk.isGrounded) currentJumps = 0;

        // Update animator parameters
        anim.SetBool("isGrounded", gndChk.isGrounded);
        anim.SetFloat("moveX", Mathf.Abs(moveX));
        if (fireInput)
        {
            anim.SetTrigger("attack");
        }
    }

    void SpriteFlip(float moveX) => sr.flipX = (moveX < 0f);
    //{
    //    if (sr.flipX && moveX > 0f || !sr.flipX && moveX< 0f)
    //    {
    //        sr.flipX = !sr.flipX;
    //    }
    //}

    public void JumpForceChange ()
    {
        if (jumpForceCoroutine  != null)
        {
            StopCoroutine(jumpForceCoroutine);
            jumpForceCoroutine = null;
            jumpForce = initialJumpForce;
        }
        jumpForceCoroutine = StartCoroutine(JumpForceChangeCoroutine());

    }
    IEnumerator JumpForceChangeCoroutine()
    {
        currentPowerupDuration = initialpowerupDuration + currentPowerupDuration;
        jumpForce = 15;

        yield return new WaitForSeconds(currentPowerupDuration);
    }
}
