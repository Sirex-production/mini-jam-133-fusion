using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Receipt
{
    [CreateAssetMenu(fileName = "AllReceiptsConfig", menuName = "Cards/AllReceiptsContainer")]
    public sealed class AllReceiptsContainerConfig : ScriptableObject
    {
        [SerializeField] private List<Receipt> allReceipts;

        public List<Receipt> AllReceipts => new List<Receipt>(allReceipts);
    }

    [Serializable]
    public sealed class Receipt
    {
        [SerializeField] private string receiptName;
        
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