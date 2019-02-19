using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

	public int numCollected;
	private int maxCollectable;
	private Rigidbody rb;
	//public Text setCollectedText;
	//public Text completeText;
    public float collectableCenterPointX = 0.30f;
    public int framesInBalloon = 0;
    public string currentTargetType = "noseTarget";
    public float forwardOffset = 0.2f;
	// Use this for initialization
	void Start () {
		numCollected = 0;
		maxCollectable = 15;
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	}
    private void OnTriggerExit(Collider other)
    {
        //reset if penciltip leaves balloon
        framesInBalloon = 0;
        Debug.Log("Too few samples collected.");
        
    }
    void OnTriggerStay(Collider other) {
       
		if (other.gameObject.CompareTag ("Collectable")) {
            Debug.Log("triggerstay");
            if (framesInBalloon > 30)
            {
                // Show balloon acquisition
                numCollected += 1;
                // Reset balloon debounce
                framesInBalloon = 0;

                if (currentTargetType == "noseTarget")
                {
                    float newLocation = Random.Range(collectableCenterPointX - 0.2f, collectableCenterPointX + 0.2f);
                    other.gameObject.transform.SetPositionAndRotation(new Vector3(newLocation, other.gameObject.transform.position.y, other.gameObject.transform.position.z - forwardOffset), other.gameObject.transform.rotation);
                    currentTargetType = "balloon";
                    // Balloon Sound 1
                    FindObjectOfType<AudioManager>().Play("balloon");
                }
                else
                {
                    //switch to balloon target
                    other.gameObject.transform.SetPositionAndRotation(new Vector3(collectableCenterPointX, other.gameObject.transform.position.y, other.gameObject.transform.position.z + forwardOffset), other.gameObject.transform.rotation);
                    currentTargetType = "noseTarget";
                    // Balloon Sound 2
                    FindObjectOfType<AudioManager>().Play("balloon");
                }
               
  
            }
            else
            {
                Debug.Log("trigger is now at " + framesInBalloon);
                framesInBalloon += 1;
            }
        }
	}
}
