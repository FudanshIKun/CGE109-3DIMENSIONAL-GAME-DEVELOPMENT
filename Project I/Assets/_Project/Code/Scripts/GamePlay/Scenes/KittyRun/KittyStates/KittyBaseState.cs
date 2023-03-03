namespace Wonderland.GamePlay.KittyRun
{
    public abstract class KittyBaseState
    {
        public abstract void EnterState(KittyRunCat cat);
        
        public abstract void FixedUpdateState(KittyRunCat cat);
        
        public abstract void UpdateState(KittyRunCat cat);
    }
}
