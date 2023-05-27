using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Secs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ingame.Recipe
{
    [CreateAssetMenu(fileName = "AllReceiptsConfig", menuName = "Cards/AllReceiptsContainer")]
    public sealed class AllRecipeContainerConfig : ScriptableObject
    {
        [FormerlySerializedAs("allReceipts")] [SerializeField] private List<Recipe> allRecipe;

        public List<Recipe> AllRecipe => new List<Recipe>(allRecipe);

        public bool TryToCombineElements(EcsWorld ecsWorld, ItemConfig componentA, ItemConfig componentB, out ItemConfig newItem)
        {
            var recipe = AllRecipe.SingleOrDefault(
                e => (e.ComponentA == componentA && e.ComponentB == componentB) || 
                           (e.ComponentB==componentA && e.ComponentA==componentB));
            
            if (recipe == null)
            {
                newItem = null;
                return false;
            }
            
            var newEntity = ecsWorld.NewEntity();
            
            ecsWorld.GetPool<DiscoverNewRecipeEvent>().AddComponent(newEntity).newRecipe = recipe;
            newItem = recipe.CreatedItem;
            
            return true;
        }
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