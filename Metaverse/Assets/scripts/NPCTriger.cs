using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTriger : MonoBehaviour
{
    public Transform Player;
    public Text TextTriger;
    public Vector2 TextTransform;
    float TrigerPosition;
    
    void Start()
    {

        //Vector2 TrigerPosition = new Vector2(Player.transform.position.x + 5f,0);
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("부딪침");
            TextTriger.gameObject.SetActive(true);
            transform.parent = this.transform;
            if (Input.GetKeyDown(KeyCode.F) && transform.parent.name == "tile_0186")
            {
                //실행한다
            }
            else if(Input.GetKeyDown(KeyCode.F) && transform.parent.name == "tile_0105")
            {

            }
            
        }
        
        Debug.Log(collision.gameObject.name);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TextTriger.gameObject.SetActive(false);
        }
    }
}
