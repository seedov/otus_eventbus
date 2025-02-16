﻿using Entities;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class EntityInstaller : MonoBehaviour
    {
        [SerializeField]
        private MonoEntity[] entities;

        private LevelMap _levelMap;

        [Inject]
        private void Construct(LevelMap levelMap)
        {
            _levelMap = levelMap;
        }
        
        private void Start()
        {
            for (int i = 0; i < entities.Length; i++)
            {
                MonoEntity entity = entities[i];
                
                CoordinatesComponent coordinates = entity.Get<CoordinatesComponent>();
                PositionComponent position = entity.Get<PositionComponent>();
                
                coordinates.Value = _levelMap.Tiles.PositionToCoordinates(position.Value);
                position.Value = _levelMap.Tiles.CoordinatesToPosition(coordinates.Value);
                
                _levelMap.Entities.SetEntity(coordinates.Value, entity);
            }
        }
    }
}