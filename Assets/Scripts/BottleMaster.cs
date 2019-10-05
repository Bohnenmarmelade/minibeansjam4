using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = System.Random;

public class BottleMaster : MonoBehaviour
{
    public GameObject bottlePrefab;
    private Dictionary<string, GameObject> _bottles;

    private List<Vector3> _positions;

    private string _currentDifficulty = Difficulty.EASY;

    private void Awake()
    {
        _positions = new List<Vector3>();
        _positions.Add(new Vector3(0.68f, 0, 2.792969f));
        _positions.Add(new Vector3(-0.68f, 0, 2.792969f));
        _positions.Add(new Vector3(-1.06f, 0, 2.792969f));
        _positions.Add(new Vector3(1.06f, 0, 2.792969f));

        _bottles = new Dictionary<string, GameObject>();

        SpawnBottle();
        
        EventManager.StartListening(Events.BOTTLE_SUCCES, DeregisterBottle);
        EventManager.StartListening(Events.INCREASE_DIFFICULTY, payload => IncreaseDifficulty());
    }

    private void OnDisable()
    {
        EventManager.StopListening(Events.BOTTLE_SUCCES, DeregisterBottle);
        EventManager.StopListening(Events.INCREASE_DIFFICULTY, payload => IncreaseDifficulty());
    }

    private void SpawnBottle()
    {
        GameObject bottle = Instantiate(bottlePrefab, _positions[0], Quaternion.identity);
        bottle.GetComponent<Bottle>().InitWordByDifficulty(_currentDifficulty);
        bottle.transform.parent = gameObject.transform;
        
        RegisterBottle(bottle);
    }

    void DeregisterBottle(string typeableWord)
    {
        EventManager.TriggerEvent(Events.INCREASE_DIFFICULTY);
        
        _positions.Add(_bottles[typeableWord].transform.position);
        Destroy(_bottles[typeableWord]);
        
        _bottles.Remove(typeableWord);
        SpawnBottle();
    }

    void RegisterBottle(GameObject bottle)
    {
        _bottles[bottle.gameObject.GetComponentInChildren<TextInput>().TypeableWord.fullWord] = bottle;
        _positions.RemoveAt(0);
    }

    void IncreaseDifficulty()
    {
        _currentDifficulty = Difficulty.GetNextHigherDifficulty(_currentDifficulty);
        Debug.Log($"Increased difficulty to '{_currentDifficulty}'");
    }
}