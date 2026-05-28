using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Unity.Infrastructure.VisualTutorial
{
    [Serializable]
    public class TutorialTask
    {
        public const string TASK_COMPLETE_EVENT = "TaskComplete";
        
        [SerializeField]
        private AssetReference _taskGraph;


        public AssetReference TaskGraph => _taskGraph;
    }
}