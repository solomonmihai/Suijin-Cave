using Love;

namespace Suijin_cave
{
    public abstract class Screen
    {
        public abstract void Load();
        public abstract void Update(float dt);
        public abstract void Draw();

        public virtual void OnEnter() { }
        public virtual void OnExit() { }

        public virtual void MousePressed(float x, float y, int button, bool isTouch) { }

        public virtual void KeyPressed(KeyConstant key, Scancode scancode, bool isRepeat) { }
    }
}