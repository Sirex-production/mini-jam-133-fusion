using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Receipt
{
    [CreateAssetMenu(fileName = "Receipt", menuName = "Cards/Receipt")]
    public sealed class ReceiptConfig : ScriptableObject
    {
        [SerializeField]
        [Required] 
        private ItemConfig componentA;
        
        [SerializeField]
        [Required]  
        private ItemConfig componentB;
        
        [SerializeField]
        [Required]  
        private ItemConfig createdItem;

        public ItemConfig ComponentA => componentA;

        public ItemConfig ComponentB => componentB;

        public ItemConfig CreatedItem => createdItem;
    }
}