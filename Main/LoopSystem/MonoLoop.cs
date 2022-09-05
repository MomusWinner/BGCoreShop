using UnityEngine;

namespace Core.Main.LoopSystem
{
    public class MonoLoop : MonoBehaviour
    {
        private void Start()
        {
            Loops.Initiate();
            GEvent.Call(GlobalEvents.Start);
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