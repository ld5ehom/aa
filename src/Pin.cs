using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pin : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 10f;

    private bool isPinned = false;
    private bool isLanched = false;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void FixedUpdate() //fixed deltaTime (for TargetCirgle collider)
    {
        if (isPinned == false && isLanched == true) // Pin move top && launch 
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // target hit (Pinned)
    {
        isPinned = true;
        if(other.gameObject.tag == "Target") 
        {
            GameObject childObject = transform.Find("Square").gameObject;
            //GameObject childObject = transform.GetChild(0).gmaeObject;

            SpriteRenderer childSprite = childObject.GetComponent<SpriteRenderer>();
            childSprite.enabled = true;

            transform.SetParent(other.gameObject.transform);

            GameManager.instance.DecreaseGoal(); // goal number -1
        } else if (other.gameObject.tag == "Pin")
        {
            GameManager.instance.SetGameOver(false); // Game over (false)
        }
    }

    public void Launch()
    {
        isLanched = true;
    }

}
