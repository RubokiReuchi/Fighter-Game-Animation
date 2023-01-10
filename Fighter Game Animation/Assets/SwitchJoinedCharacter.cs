using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchJoinedCharacter : MonoBehaviour
{
    private PlayerInputManager im;
    public GameObject prefabB;

    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<PlayerInputManager>();
    }

    void OnPlayerJoined()
    {
        im.playerPrefab = prefabB;
    }
}
