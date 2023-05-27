using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Recipe
{
    [CreateAssetMenu(fileName = "Item", menuName = "Cards/Item")]
    public sealed class ItemConfig : ScriptableObject
    {
        [SerializeField] private string itemName;

        [Required] 
        [SerializeField] private Texture2D icon;
        
    }
    
    
}