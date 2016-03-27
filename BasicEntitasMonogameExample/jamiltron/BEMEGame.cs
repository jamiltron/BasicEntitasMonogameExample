using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Entitas;

namespace BasicEntitasMonogameExample {
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class BEMEGame : Game {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Systems updateSystems;
    Systems renderSystems;
    Pool pool;

    public BEMEGame() {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsFixedTimeStep = true;
      graphics.SynchronizeWithVerticalRetrace = true;
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize() {
      pool = Pools.pool;
      base.Initialize();
    }

    Systems CreateUpdateSystems() {
      return new Systems().Add(pool.CreateSystem<PlayerKeyboardSystem>())
        .Add(pool.CreateSystem<MovementSystem>());
    }

    Systems CreateRenderSystems() {
      var content = new Dictionary<string, Texture2D>();
      content["Hero"] = Content.Load<Texture2D>("Hero");
      var viewRenderSystem = (ViewRenderSystem)pool.CreateSystem<ViewRenderSystem>();
      viewRenderSystem.SpriteBatch = spriteBatch;
      viewRenderSystem.Content = content;
      return new Systems().Add(viewRenderSystem);
    }

    void CreateEntities() {
      pool.CreateEntity().AddPosition(0, 0).AddVelocity(0f, 0f).AddView("Hero").AddPlayerControlled(true);
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent() {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);
      updateSystems = CreateUpdateSystems();
      renderSystems = CreateRenderSystems();
      CreateEntities();
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime) {
      // For Mobile devices, this logic will close the Game when the Back button is pressed
      // Exit() is obsolete on iOS
      #if !__IOS__ &&  !__TVOS__
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();
      #endif

      updateSystems.Execute();
      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime) {
      graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
      spriteBatch.Begin();
      renderSystems.Execute();
      spriteBatch.End();
      base.Draw(gameTime);
    }

  }
}

