namespace Wonderland.GamePlay.BeatRunner.Running
{
    public interface IRunnerBehavior
    {
        public Runner Runner { set; }
        void FixedUpdateBehavior();
        void UpdateBehavior();
        void LateUpdateBehavior();
        void UpSwipe();
        void LefSwipe();
        void RightSwipe();
        void DownSwipe();
    }
}