using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    static ReferenceManager _instance;
    public static ReferenceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ReferenceManager>();
            }
            return _instance;
        }
    }

    [field: SerializeField] public GameObject Table { get; private set; }
    [field: SerializeField] public GameObject Card { get; private set; }
    [field: SerializeField] public GameObject CardShadow { get; private set; }
    [field: SerializeField] public GameObject PlayerField { get; private set; }
    [field: SerializeField] public GameObject EnemyField { get; private set; }
    [field: SerializeField] public GameObject PlayerHand { get; private set; }
    [field: SerializeField] public GameObject EnemyHand { get; private set; }
    [field: SerializeField] public GameObject CardPreviews { get; private set; }
    [field: SerializeField] public Transform PlayerSpawn { get; private set; }
    [field: SerializeField] public Transform OpponentSpawn { get; private set; }
}
