using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "GunData")]
public class GunData : ScriptableObject, ILootable
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField, Range(0, 3)] public float BaseAttackSpeed { get; private set; }
    [field: SerializeField, Range(0, 100)] public int AttackSpeedPercent { get; set; }
    [field: SerializeField, Range(0, 20)] public float BaseDamage { get; set; }
    [field: SerializeField, Range(0, 100)] public int DamagePercent { get; private set; }
    [field: SerializeField] public Gun Prefab { get; private set; }
    [field: SerializeField] public bool IsDropped { get; set; }

    [SerializeField] private LocalizedText[] _texts;

    private const float AttackSpeedFactor = 0.99f;
    private const string DamageReplacer = "_dmg_";
    private const string AttackSpeedReplacer = "_as_";

    public LocalizedText GetLocalizedText(string language)
    {
        for (int i = 0; i < _texts.Length; i++)
        {
            if (_texts[i].Language.ToString().ToLower() == language.ToLower())
            {
                LocalizedText text = new LocalizedText(_texts[i].Language, _texts[i].Name, _texts[i].ShortDescription, ChangeDescription(_texts[i].FullDescription));
                return text;
            }
        }

        throw new System.ArgumentException(nameof(language));
    }

    public float GetTotalDamage()
    {
        return BaseDamage + (BaseDamage / 100 * DamagePercent);
    }

    public float GetTotalAttackSpeed()
    {
        float attackSpeed = BaseAttackSpeed * System.MathF.Pow(AttackSpeedFactor, AttackSpeedPercent);
        return attackSpeed;
    }

    private string ChangeDescription(string description)
    {
        return description.Replace(DamageReplacer, GetTotalDamage().ToString()).Replace(AttackSpeedReplacer, GetTotalAttackSpeed().ToString());
    }
}