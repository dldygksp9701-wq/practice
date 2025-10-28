using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NPCTriger : MonoBehaviour
{
    public Transform Player;
    public Text TextTriger;
    public Vector2 TextTransform;
    float TrigerPosition;
    public GameObject npc1;
    private bool isNearNPC = false;
    void Start()
    {

        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && npc1 != null)
        {
            
            SceneManager.LoadScene("Stack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TextTriger.gameObject.SetActive(true);
            if (collision.gameObject == npc1)
            {
                
                isNearNPC = true;


            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TextTriger.gameObject.SetActive(false);
            if (collision.gameObject == npc1)
            {
                TextTriger.gameObject.SetActive(false);
                Debug.Log("부딪침");
                isNearNPC = false;
            }
        }
    }
}
