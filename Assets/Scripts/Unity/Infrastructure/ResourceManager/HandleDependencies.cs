using JetBrains.Annotations;
using UnityEngine;

namespace Common.ResourceManager
{
    public readonly struct HandleDependencies
    {
        [UsedImplicitly] public GameObject[] Links { get; }
        [UsedImplicitly] public string[] Tags { get; }

        public bool IsValid() => Links is { Length: > 0 } || Tags is { Length: > 0 };
    }
}