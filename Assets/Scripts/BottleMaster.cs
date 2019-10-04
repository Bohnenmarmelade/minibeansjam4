using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BottleMaster : MonoBehaviour
{
    public GameObject bottlePrefab;
    private Dictionary<string, Tuple<Vector3, GameObject>> bottles;

    private List<Vector3> _positions;

    private void OnEnable()
    {
        _positions = new List<Vector3>();
        _positions.Add(new Vector3(0.68f, -1.06f, 2.792969f));
        _positions.Add(new Vector3(-0.68f, 1.06f, 2.792969f));
        _positions.Add(new Vector3(-1.06f, 0.68f, 2.792969f));
        _positions.Add(new Vector3(1.06f, -0.68f, 2.792969f));
        
        bottles = new Dictionary<string, Tuple<Vector3, GameObject>>();
        SpawnBottle();

        EventManager.StartListening(Events.BOTTLE_SUCCES, DeregisterBottle);
    }

    private void SpawnBottle()
    {
        GameObject bottle = Instantiate(bottlePrefab, _positions[0], Quaternion.identity);
        RegisterBottle(bottle);
    }

    void DeregisterBottle(string typeableWord)
    {
        _positions.Add(bottles[typeableWord].Item1);
        Destroy(bottles[typeableWord].Item2);
        
        bottles.Remove(typeableWord);
        SpawnBottle();
    }

    void RegisterBottle(GameObject bottle)
    {
        var position = _positions[0];
        bottles[bottle.gameObject.GetComponentInChildren<TextInput>().TypeableWord.fullWord] =
            new Tuple<Vector3, GameObject>(position, bottle);
        _positions.RemoveAt(0);
    }
}