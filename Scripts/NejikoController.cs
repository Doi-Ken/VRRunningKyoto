using UnityEngine;
using System.Collections;

public class NejikoController : MonoBehaviour {

    CharacterController controller;
    Animator animator;
    const int DefaultLife = 5;
    const float StunDuration = 2.5f;

    Vector3 moveDirection = Vector3.zero;
    int life = DefaultLife;
    float recoverTime = 0.0f;

    public float gravity;
    public float speedZ;
    public float speedJump;
    public GameObject target;


    //
    Vector3 diff;
    Vector3 diffRotation;

    private float loLim = 0.005F;
    public float hiLim = 0.1F;//前進のしきい値
    public float Jump = 0.3F;  //ジャンプのしきい値
    private bool stateH = false;
    private float fHigh = 8.0F;
    private float curAcc = 0F;
    private float fLow = 0.2F;
    private float avgAcc;
    private int steps = 0;
    //

	public GameObject Finish;
	public GameObject StartGameText;
	public int startcountMax = 25;
	private int startcount = 0;

    public AudioSource damage;

    //縦回転
    //スピードによるフレーム削除(大きいほど削除される)
    private int h_speed = 3;
    private int h_low = 5;
    private int h_medi = 20;
    private int h_high = 100;
    //
    //スピードの大小の閾値
    private float h_lowest = 0.1f;
    private float h_low_medi = 0.4f;
    private float h_medi_high = 0.6f;
    //

    public int Life()
    {
        return life;
    }

    public bool IsStan()
    {
        return recoverTime > 0.0f || life <= 0;
    }


    // Use this for initialization
    void Start()
    {
       Finish.SetActive(false);
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        damage = GetComponent<AudioSource>();
    }

    
	// Update is called once per frame
	void LateUpdate () {
		startcount++;
		if(startcount > startcountMax){
			StartGameText.SetActive(false);
		}
        stepDetector();
        if (Application.isEditor)
        {
            if (controller.isGrounded)
            {
                if (Input.GetAxis("Vertical") > 0.0f)
                {
                    moveDirection.z = Input.GetAxis("Vertical") * speedZ;
                }
                else
                {
                    moveDirection.z = 0;
                }
                transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);


                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = speedJump;
                    animator.SetTrigger("jump");
                }
            }
        }
        else
        {


            if (controller.isGrounded)
            {
                if (steps == 1)
                {
                    moveDirection.z = 1 * speedZ;
					moveDirection.y = 0;
                }
                else
                {
                    moveDirection.z = 0;
					moveDirection.y = 0;
                }

                transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, Time.deltaTime * speedZ);

                jumpDetector();


                if (steps == 2)
                {
                    moveDirection.y = speedJump;
                    animator.SetTrigger("jump");
                }
            }
        }

        if (IsStan())
        {
            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else {

            moveDirection.y -= gravity * Time.deltaTime;
        }

        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        if (controller.isGrounded) moveDirection.y = 0;

        animator.SetBool("run", moveDirection.z > 0.0f);
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (IsStan()) return;
        if(hit.gameObject.tag == "Enemy")
        {
            Lifedecrease();
            damage.Play();
            animator.SetTrigger("damage");
            
            Destroy(hit.gameObject);
        }
		if(hit.gameObject.tag == "Goal")
		{
            //OnGUI();
            //GameObject textmessage = Instantiate<GameObject>(Finish);
		Finish.SetActive(true);

		StartCoroutine("sleep");
		}
    }

	public void SceneLoad(){ 
 		Application.LoadLevel ("DemoScene"); 
 
 	} 
	IEnumerator sleep(){
 
		yield return new WaitForSeconds(6);  //10秒待つ
		SceneLoad();
	}

/*	void OnTriggerEnter(Collider other) {

		if(other.gameObject.tag == "Goal"){
			other.gameObject.SetActive(true);
		}
    }*/

    public void Lifedecrease()
    {
        life--;
        recoverTime = StunDuration;
    }

    public void stepDetector()
    {
        curAcc = Mathf.Lerp(curAcc, Input.acceleration.magnitude, Time.deltaTime * fHigh);
        avgAcc = Mathf.Lerp(avgAcc, Input.acceleration.magnitude, Time.deltaTime * fLow);
        float delta = curAcc - avgAcc;

        if (!stateH)
        {
            if (delta > hiLim)
            {
                stateH = true;
                steps = 1;
            }
            else if (delta < loLim)
            {
                steps = 0;
                stateH = false;
            }
            stateH = false;
        }
        avgAcc = curAcc;

    }

    public void jumpDetector()
    {
        curAcc = Mathf.Lerp(curAcc, Input.acceleration.magnitude, Time.deltaTime * fHigh);
        avgAcc = Mathf.Lerp(avgAcc, Input.acceleration.magnitude, Time.deltaTime * fLow);
        float delta = curAcc - avgAcc;

        if (!stateH)
        {
            if (delta > Jump)
            {
                stateH = true;
                steps = 2;
            }

            else if (delta < loLim)
            {
                steps = 0;
                stateH = false;
            }
            stateH = false;
        }
        avgAcc = curAcc;

    }
}
