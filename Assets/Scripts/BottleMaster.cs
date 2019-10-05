using System;
using System.Collections.Generic;
using UnityEngine;

public class BottleMaster : MonoBehaviour
{
    public GameObject bottlePrefab;
    private Dictionary<string, Tuple<Vector3, GameObject>> _bottles;

    private List<Vector3> _positions;

    private void OnEnable()
    {
        _positions = new List<Vector3>();
        _positions.Add(new Vector3(0.68f, -1.06f, 2.792969f));
        _positions.Add(new Vector3(-0.68f, 1.06f, 2.792969f));
        _positions.Add(new Vector3(-1.06f, 0.68f, 2.792969f));
        _positions.Add(new Vector3(1.06f, -0.68f, 2.792969f));
        
        _bottles = new Dictionary<string, Tuple<Vector3, GameObject>>();
        SpawnBottle();

        EventManager.StartListening(Events.BOTTLE_SUCCES, DeregisterBottle);
    }
    
    private void SpawnBottle()
    {
        GameObject bottle = Instantiate(bottlePrefab, _positions[0], Quaternion.identity);
        bottle.transform.parent = gameObject.transform;
        RegisterBottle(bottle);
    }

    void DeregisterBottle(string typeableWord)
    {
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
}