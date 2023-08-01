using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace Unit
{
    [MaterialProperty("_Highlight_Color")]
    public struct SelectedColor : IComponentData
    {
        public float4 value;
    }

    public class SelectedColorAuthoring : MonoBehaviour
    {
        public class SelectedColorBaker : Baker<SelectedColorAuthoring>
        {
            public override void Bake(SelectedColorAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new SelectedColor());
            }
        }
    }
}