using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Unit.Systems
{
    public partial class UnitSelectionSystem : SystemBase
    {

        protected override void OnUpdate()
        {
            var selectionBox = CameraBoxSelection.Instance.SelectionBox();
            
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            if (!CameraBoxSelection.Instance.MakingSelection) return;
            
            Entities.WithAll<UnitComponent, SelectedColor>().ForEach((Entity unitEntity, ref SelectedColor color) =>
            {
                if (IsUnitInSelectionBox(selectionBox, unitEntity))
                {
                    if (EntityManager.HasComponent<UnitSelectedComponent>(unitEntity)) return;
                    
                    ecb.AddComponent<UnitSelectedComponent>(unitEntity);
                    color.value = new float4(0,0,1,1);
                }
                else
                {
                    if (!EntityManager.HasComponent<UnitSelectedComponent>(unitEntity)) return;
                    
                    ecb.RemoveComponent<UnitSelectedComponent>(unitEntity);
                    color.value = new float4(0,0,0,1);
                }
            }).WithoutBurst().Run();
            Dependency.Complete();
            ecb.Playback(EntityManager);
        }
        
        private bool IsUnitInSelectionBox(Rect selectionBox, Entity entity)
        {
            Vector3 position = EntityManager.GetComponentData<LocalTransform>(entity).Position;

            // Convert the position from world space to screen space
            Camera mainCamera = Camera.main; // Or however you get your main camera
            Vector2 screenPoint = mainCamera.WorldToScreenPoint(position);

            // Since selectionBox is already in screen space, we can directly check if the point is inside
            return selectionBox.Contains(screenPoint);
        }
    }
}