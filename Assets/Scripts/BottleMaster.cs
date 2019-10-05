using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BottleMaster : MonoBehaviour
{
    public GameObject bottlePrefab;
    private Dictionary<string, Tuple<Vector3, GameObject>> _bottles;

    private List<Vector3> _positions;

    private string _currentDifficulty = Difficulty.EASY;

    private void Awake()
    {
        _positions = new List<Vector3>();
        _positions.Add(new Vector3(0.68f, -1.06f, 2.792969f));
        _positions.Add(new Vector3(-0.68f, 1.06f, 2.792969f));
        _positions.Add(new Vector3(-1.06f, 0.68f, 2.792969f));
        _positions.Add(new Vector3(1.06f, -0.68f, 2.792969f));
        
        _bottles = new Dictionary<string, Tuple<Vector3, GameObject>>();

        SpawnBottle();
        
        EventManager.StartListening(Events.BOTTLE_SUCCES, DeregisterBottle);
        EventManager.StartListening(Events.INCREASE_DIFFICULTY, payload => IncreaseDifficulty());
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
        
        _positions.Add(_bottles[typeableWord].Item1);
        Destroy(_bottles[typeableWord].Item2);
        
        _bottles.Remove(typeableWord);
        SpawnBottle();
    }

    void RegisterBottle(GameObject bottle)
    {
        var position = _positions[0];
        _bottles[bottle.gameObject.GetComponentInChildren<TextInput>().TypeableWord.fullWord] =
            new Tuple<Vector3, GameObject>(position, bottle);
        _positions.RemoveAt(0);
    }

    void IncreaseDifficulty()
    {
        _currentDifficulty = Difficulty.GetNextHigherDifficulty(_currentDifficulty);
        Debug.Log($"Increased difficulty to '{_currentDifficulty}'");
    }
}