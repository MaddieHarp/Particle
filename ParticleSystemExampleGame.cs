using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParticleSystemExercise;

public class ParticleSystemExampleGame : Game, IParticleEmitter
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    ExplosionParticleSystem _explo;
    FireworkParticleSystem _firework;
    MouseState _prior;

    public Vector2 Position {get; set;}

    public Vector2 Velocity {get;set;}

    public ParticleSystemExampleGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        RainParticleSystem rain = new RainParticleSystem(this, new Rectangle(100, -20, 500, 10));
        _explo = new ExplosionParticleSystem(this, 20);
        _firework = new FireworkParticleSystem(this, 20);
        Components.Add(_firework);
        Components.Add(_explo);
        Components.Add(rain);

        PixieParticleSystem pixie = new PixieParticleSystem(this, this);
        Components.Add(pixie);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        MouseState curMouse = Mouse.GetState();
        Vector2 mousePos = new Vector2(curMouse.X, curMouse.Y);

        if(curMouse.LeftButton == ButtonState.Pressed && _prior.LeftButton == ButtonState.Released)
        {
            _explo.PlaceExplosion(mousePos);
        }

        if(curMouse.RightButton == ButtonState.Pressed && _prior.RightButton == ButtonState.Released)
        {
            _firework.PlaceFirework(mousePos);
        }
        Velocity = mousePos - Position;

        Position = mousePos;


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
