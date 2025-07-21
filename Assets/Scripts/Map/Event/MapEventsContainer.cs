using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapEventsContainer : MonoBehaviour
{
    [SerializeField] private MapEventObject[] _eventObjects;

    private const int ChangeToActivate = 100;

    private readonly List<MapEventObject> _objets = new List<MapEventObject>();

    [Inject]
    private IInstantiator _instantitator;

    private void Awake()
    {
        CreateObjects();
    }

    private void CreateObjects()
    {
        for (int i = 0; i < _eventObjects.Length; i++)
        {
            MapEventObject obj = _instantitator.InstantiatePrefab(_eventObjects[i].gameObject).GetComponent<MapEventObject>();
            obj.gameObject.SetActive(false);
            _objets.Add(obj);
        }
    }

    public void TryActivateEvent()
    {
        int random = Random.Range(0, 100);

        if (ChangeToActivate >= random)
            GetRandomEvent().Activate();
    }

    private MapEventObject GetRandomEvent()
    {
        int randomIndex = Random.Range(0, 100);
        return _eventObjects[randomIndex];
    }
}
