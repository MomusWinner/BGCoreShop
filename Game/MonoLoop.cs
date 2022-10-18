using Core.Locations.Model;
using Core.LoopSystem;
using GameLogic;
using UnityEngine;

namespace Core.Main.LoopSystem
{
    public class MonoLoop : Singleton<MonoLoop>
    {
        private LocationSection activeLocationSection;

        private void Start()
        {
            Loops.Initiate();
        }

        private void FixedUpdate()
        {
            CoreLoopService.Execute(Loops.FixedUpdate);
        }

        private void Update()
        {
            CoreLoopService.Execute(Loops.Timer);
            CoreLoopService.Execute(Loops.Update);
        }

        private void LateUpdate()
        {
            CoreLoopService.Execute(Loops.LateUpdate);
        }

        private void OnDrawGizmos()
        {
            CoreLoopService.Execute(Loops.Gizmos);
        }

        private void OnDrawGizmosSelected()
        {
            CoreLoopService.Execute(Loops.GizmosSelected);
        }

    }
}