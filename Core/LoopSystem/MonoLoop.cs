namespace Core.LoopSystem
{
    public static class MonoLoop
    {
        public static void Initiate()
        {
            Loops.Initiate();
        }

        public static void FixedUpdate()
        {
            CoreLoopService.Execute(Loops.FixedUpdate);
        }

        public static void Update()
        {
            CoreLoopService.Execute(Loops.Timer);
            CoreLoopService.Execute(Loops.Update);
            CoreLoopService.Execute(Loops.Update1);
        }

        public static void LateUpdate()
        {
            CoreLoopService.Execute(Loops.LateUpdate);
        }

        public static void OnDrawGizmos()
        {
            CoreLoopService.Execute(Loops.Gizmos);
        }

        public static void OnDrawGizmosSelected()
        {
            CoreLoopService.Execute(Loops.GizmosSelected);
        }
    }
}