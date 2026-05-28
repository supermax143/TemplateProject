using System.Collections.Generic;
using UnityEngine;

namespace Unity.Infrastructure.VisualTutorial
{
    
    [CreateAssetMenu(fileName = "Assets/Data/TutorialTasksChain.asset", menuName = "Zombies/TutorialTasksChain", order = 1)]
    public class TutorialTasksChain : ScriptableObject
    {
        [SerializeField]
        private string _id;
        
        [SerializeField]
        private List<TutorialTask> _tasks;


        public List<TutorialTask> Tasks => _tasks;
        public string ID => _id;
    }
}