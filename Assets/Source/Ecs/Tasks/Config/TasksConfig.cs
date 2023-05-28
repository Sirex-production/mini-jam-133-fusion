using System;
using System.Collections.Generic;
using Ingame.Recipe;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("questItem")]
        [SerializeField] 
        [Required]
        private List<ItemConfig> questItems;

        [SerializeField] 
        private string description;

        [SerializeField] 
        private int money;
        
        public List<ItemConfig> QuestItems => questItems;

        public string Description => description;

        public int Money => money;
    }
}