using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterImageScript : MonoBehaviour
{
    GameObject player;
    CharacterActions playerScript;
    public Camera mainCamera;

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<CharacterActions>();

    }

    // Update is called once per frame
    void Update()
    {

        this.GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(new Vector3(player.transform.position.x + 1f, player.transform.position.y, player.transform.position.z));

    }
}