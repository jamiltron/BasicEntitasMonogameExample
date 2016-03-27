using Entitas;
using Microsoft.Xna.Framework.Input;

namespace BasicEntitasMonogameExample {
  public class PlayerKeyboardSystem : IExecuteSystem, ISetPool {
    private Group group;
    private const float moveSpeed = 6f;

    public void Execute() {
      System.Console.WriteLine("In execute 2!");
      foreach (var e in group.GetEntities()) {
        System.Console.WriteLine("In execute 2!");
        var playerControlled = e.playerControlled;
        var velocity = e.velocity;

        if (!playerControlled.enabled) {
          continue;
        }


        var state = Keyboard.GetState();
        var left = state.IsKeyDown(Keys.Left);
        var right = state.IsKeyDown(Keys.Right);
        var up = state.IsKeyDown(Keys.Up);
        var down = state.IsKeyDown(Keys.Down);

        if (left && !right) {
          System.Console.WriteLine("LEFT!");
          velocity.x = -moveSpeed;
        } else if (right && !left) {
          velocity.x = moveSpeed;
        } else {
          velocity.x = 0f;
        }

        if (up && !down) {
          velocity.y = -moveSpeed;
        } else if (down && !up) {
          velocity.y = moveSpeed;
        } else {
          velocity.y = 0f;
        }

        e.ReplaceVelocity(velocity.x, velocity.y);
      }
    }

    public void SetPool(Pool pool) {
      group = pool.GetGroup(Matcher.AllOf(Matcher.PlayerControlled, Matcher.Velocity));
    }
  }
}

