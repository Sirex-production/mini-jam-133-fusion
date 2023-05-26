using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ingame.Receipt
{
    [CreateAssetMenu(fileName = "AllReceiptsConfig", menuName = "Cards/AllReceiptsContainer")]
    public sealed class AllRecipeContainerConfig : ScriptableObject
    {
        [FormerlySerializedAs("allReceipts")] [SerializeField] private List<Recipe> allRecipe;

        public List<Recipe> AllRecipe => new List<Recipe>(allRecipe);
    }

    [Serializable]
    public sealed class Recipe
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