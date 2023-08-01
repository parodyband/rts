using Unity.Entities;
using UnityEngine;

namespace Unit
{
    public struct UnitComponent : IComponentData
    {
        
    }

    public class UnitComponentAuthoring : MonoBehaviour
    {
        public class UnitComponentBaker : Baker<UnitComponentAuthoring>
        {
            public override void Bake(UnitComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new UnitComponent());
            }
        }
    }
}