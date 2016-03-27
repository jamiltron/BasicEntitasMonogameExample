using Entitas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BasicEntitasMonogameExample {
  public class ViewRenderSystem : IExecuteSystem, ISetPool {
    Group group;
    Dictionary<string, Texture2D> content;
    SpriteBatch spriteBatch;

    public Dictionary<string, Texture2D> Content {
      set { content = value; }
    }

    public SpriteBatch SpriteBatch {
      set { spriteBatch = value; }
    }

    public void Execute() {
      foreach (var e in group.GetEntities()) {
        var position = e.position;
        var view = e.view;

        spriteBatch.Draw(content[view.name], new Vector2(position.x, position.y));
      }
    }
     
    public void SetPool(Pool pool) {
      group = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.View));
    }
      
  }
}

