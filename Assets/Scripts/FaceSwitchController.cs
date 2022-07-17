using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FaceSwitchController : MonoBehaviour
{

    public int pips;
    public List<GameObject> walls;
    public Vector2Int thisPos;
    public Vector2Int playerPos;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponentInChildren<TextMeshPro>().text = pips.ToString();
    }

    private void Update()
    {
        playerPos = player.GetComponentInChildren<DieController>().position;
        Debug.Log(playerPos + " // "  + thisPos );
        if (thisPos == playerPos && player.GetComponentInChildren<DieController>().sides[Vector3.down] == pips)
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAA");
            foreach (GameObject w in walls) Destroy(w);
        }


    }




}
