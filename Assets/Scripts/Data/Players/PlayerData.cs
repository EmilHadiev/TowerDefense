using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject, IPurchasable
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public PlayerStickman Prefab { get; private set; }
    [field: SerializeField] public GameObject StickmanView { get; private set; }

    [field: SerializeField] public bool IsPurchased { get; set; }
}