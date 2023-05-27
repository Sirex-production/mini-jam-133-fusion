using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Recipe
{
    [CreateAssetMenu(fileName = "Item", menuName = "Cards/Item")]
    public sealed class ItemConfig : ScriptableObject
    {
        [SerializeField] private Color cardBackgroundColor;
        [SerializeField] private Color cardDescriptionBackgroundColor;
        [SerializeField] private string itemName;
        [Required, SerializeField] private Sprite itemIcon;

        public Color CardBackgroundColor => cardBackgroundColor;
        public Color CardDescriptionBackgroundColor => cardDescriptionBackgroundColor;
        public string ItemName => itemName;
        public Sprite ItemIcon => itemIcon;
    }
}