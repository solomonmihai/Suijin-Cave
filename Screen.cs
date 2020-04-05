namespace Suijin_cave
{
    public abstract class Screen
    {
        public abstract void Load();
        public abstract void Update(float dt);
        public abstract void Draw();

        public virtual void OnEnter() {}
        public virtual void OnExit() {}
    }
}