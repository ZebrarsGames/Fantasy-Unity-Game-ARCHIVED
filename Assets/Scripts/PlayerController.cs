using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 2f;
    private Rigidbody _rb;
    public float rotationSpeed = 100.0f; 

    public GameObject inv;
    public GameObject mainCanvas;
    public GameObject pauseCanvas;
    public GameObject cam;
    private float currentXRotation = 0f;
    private bool isCrouch = false;
    private bool isPause = false;
    private float defaultCamY;
    float baseSpeed = 2f;
    public float jumpForce = 200f;
    public float camOffset = 1.5f;
    public Inventory inventory;
    public TargetsLogic targetsLogic;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        defaultCamY = cam.transform.position.y;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Tab)) 
        {
            inv.SetActive(!inv.activeSelf);
            mainCanvas.SetActive(!mainCanvas.activeSelf);
            targetsLogic.ShowTargetPanel();
        }
        
        if(Input.GetKeyUp(KeyCode.LeftControl)) 
        {           
            if(isCrouch) {
                cam.transform.position = new Vector3(cam.transform.position.x, defaultCamY, cam.transform.position.z);
                speed = baseSpeed;
            } else
            { 
                cam.transform.position = new Vector3(cam.transform.position.x, defaultCamY-camOffset, cam.transform.position.z);
                speed = baseSpeed * 0.5f;
            }
            isCrouch = !isCrouch;
            Debug.Log("Is Crouch == " + isCrouch);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out RaycastHit hit, 2f))
            {
                NPCControl npc = hit.collider.GetComponentInParent<NPCControl>();
                DoorInfoStorage door = hit.collider.GetComponentInParent<DoorInfoStorage>();

                if (npc != null)
                {
                    npc.Interact();
                }
                if (door != null)
                {
                    door.Interact();
                }
            }

        }



        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            float step = rotationSpeed * Time.deltaTime;  
            currentXRotation += step;
            currentXRotation = Mathf.Clamp(currentXRotation, -60f, 60f);
            cam.transform.localRotation = Quaternion.Euler(currentXRotation, 0, 0);
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            float step = rotationSpeed * Time.deltaTime;  
            currentXRotation -= step;
            currentXRotation = Mathf.Clamp(currentXRotation, -60f, 60f);
            cam.transform.localRotation = Quaternion.Euler(currentXRotation, 0, 0);
        }

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
        
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        if(isGrounded && Input.GetKeyUp(KeyCode.Space))
            _rb.AddForce(Vector3.up * jumpForce);
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W)) 
        { 
            _rb.linearVelocity = transform.TransformDirection(new Vector3(0, _rb.linearVelocity.y, speed)); 
        }

        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift)) 
        { 
            _rb.linearVelocity = transform.TransformDirection(new Vector3(0, _rb.linearVelocity.y, speed*2.5f)); 
        } 

        if(Input.GetKey(KeyCode.S)) 
        { 
            _rb.linearVelocity = transform.TransformDirection(new Vector3(0, _rb.linearVelocity.y, -speed)); 
        }

        if(Input.GetKey(KeyCode.A)) 
        { 
            _rb.linearVelocity = transform.TransformDirection(new Vector3(-speed, _rb.linearVelocity.y, 0)); 
        } 

        if(Input.GetKey(KeyCode.D)) 
        {
            _rb.linearVelocity = transform.TransformDirection(new Vector3(speed, _rb.linearVelocity.y, 0)); 
        }  
        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)) 
        {
            _rb.linearVelocity = transform.TransformDirection(new Vector3(speed, _rb.linearVelocity.y, speed)); 
        }    
        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S)) 
        {
            _rb.linearVelocity = transform.TransformDirection(new Vector3(speed, _rb.linearVelocity.y, -speed)); 
        } 
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) 
        {
            _rb.linearVelocity = transform.TransformDirection(new Vector3(-speed, _rb.linearVelocity.y, speed)); 
        } 
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)) 
        {
            _rb.linearVelocity = transform.TransformDirection(new Vector3(-speed, _rb.linearVelocity.y, -speed)); 
        }
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) 
        {
            _rb.linearVelocity = transform.TransformDirection(new Vector3(0, 0, 0)); 
        }
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) 
        {
            _rb.linearVelocity = transform.TransformDirection(new Vector3(0, 0, 0)); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "ItemID1":
                inventory.NewItem(1);
                break;
            default:
                break;
        }
        Destroy(other.gameObject);
    }

    public void Pause()
    {
        if(isPause)
            {
                pauseCanvas.SetActive(false);
                isPause = false;
                mainCanvas.SetActive(true);
                Time.timeScale = 1f;
            } else
            {
                pauseCanvas.SetActive(true);
                isPause = true;
                mainCanvas.SetActive(false);
                Time.timeScale = 0f; 
            }
    }

}