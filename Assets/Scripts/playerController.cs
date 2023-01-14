
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class playerController : MonoBehaviour
{
    public float runSpeed = 12.0f; //running speed
    public float jumpHeight = 12.0f; //jumping height
  //  public float fall = -20; //fall death
    public AudioClip jumpSound; //jumping sound
    //public AudioClip fallSound; //falling sound

    private RaycastHit hit; //hit
    private float jumpCounter = 0.0f; //jumping counter
    private CharacterController controller; //character controller
    private Vector3 vel; //direction and speed
    private float lookX; //it is + or -
    private bool canControl = true; //if it can be control
    private bool canCeiling = true; //if it can hit the ceiling
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        lookX = transform.localScale.x;
    }

    void Update()
    {   //gravity application
        if (!controller.isGrounded) //if player isnt grounded
        {
            jumpCounter += Time.deltaTime;
            vel.y -= Time.deltaTime * 30; // give it the gravity 40
        }else{
            jumpCounter = 0.0f;
            vel.y = -1;  //let player stay on the ground
        }
        
        //character flipping (if x >= 0, character will face to the right)
        if (controller.velocity.x > 0){ 
            transform.localScale = new Vector3(lookX, transform.localScale.y, transform.localScale.z);
        }
        if (controller.velocity.x < 0){ 
            transform.localScale = new Vector3(-lookX, transform.localScale.y, transform.localScale.z);
        }

        //movement setting
        if (canControl)
        {
            if (Input.GetKey("left") || Input.GetKey("right")){
                if (Input.GetKey(KeyCode.LeftArrow)){
                    vel.x = -runSpeed;
                }

                if (Input.GetKey(KeyCode.RightArrow)){
                    vel.x = runSpeed;
                }
            }else{
                vel.x = 0;
            }

            if (Input.GetKey(KeyCode.UpArrow)){
                if (jumpCounter < 0.1f){
                    vel.y = jumpHeight;
                    jumpCounter = 0.1f;
                    source.PlayOneShot(jumpSound);
                }
             }
        }

        if ((controller.collisionFlags & CollisionFlags.Above) != 0 && canCeiling)
        {
            canCeiling = false;
            vel.y = 0;
            StartCoroutine(resetCeiling());
        }

        controller.Move(vel * Time.deltaTime);

        // if (transform.position.y < fall){
        //     string lvlName = SceneManager.GetActiveScene().name; 
        //     SceneManager.LoadScene(lvlName);
        // }

        // if(transform.position.y < -3 && transform.position.y > fall){
        //     source.PlayOneShot(fallSound);
        // }

    }

    public IEnumerator resetCeiling(){
        yield return new WaitForSeconds(0.25f);
        canCeiling = true;
    }

    //if player died, player cannot controll
    void died() {
        canControl = false;
        vel.x = 0;
    }
}
