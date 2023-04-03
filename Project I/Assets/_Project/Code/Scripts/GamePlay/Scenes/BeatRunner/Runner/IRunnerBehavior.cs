namespace Wonderland.GamePlay.BeatRunner.Runner
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
