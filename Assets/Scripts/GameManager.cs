using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instanceGameManager;

    public PlayerMove playerMove;

    private void Awake()
    {
        instanceGameManager = this;
    }
}
