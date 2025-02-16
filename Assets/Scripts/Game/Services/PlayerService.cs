using Entities;
using UnityEngine;

namespace Lessons.Lesson19_EventBus
{
    public sealed class PlayerService : MonoBehaviour
    {
        public IEntity Player => player;
        
        [SerializeField]
        private MonoEntity player;
    }
}