using System;
using System.Collections.Generic;
using Ingame.Recipe;
using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Tasks 
{
    [CreateAssetMenu(fileName = "TasksConfig", menuName = "Tasks/TasksConfig")]
    public sealed class TasksConfig : ScriptableObject
    {
        [SerializeField] private List<Task> tasks;

        public List<Task> Tasks => tasks;
    }

    [Serializable]
    public sealed class Task
    {
        [SerializeField] 
        [Required]
        private ItemConfig questItem;

        [SerializeField] 
        private string description;

        [SerializeField] 
        private float money;
        
        public ItemConfig QuestItem => questItem;

        public string Description => description;

        public float Money => money;
    }
}