using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleSystemExercise;

public class PixieParticleSystem : ParticleSystem
{
    IParticleEmitter _emitter;

    public PixieParticleSystem(Game game, IParticleEmitter emitter) : base(game, 2000)
    {
        _emitter = emitter;
    }

    protected override void InitializeConstants()
    {
        textureFilename = "particle";

        minNumParticles = 2;
        maxNumParticles = 5;

        blendState = BlendState.Additive;
        DrawOrder = AdditiveBlendDrawOrder;
    }

    protected override void InitializeParticle(ref Particle p, Vector2 where)
    {
        var velo = _emitter.Velocity;
        var acc = Vector2.UnitY * 400;
        var scale = RandomHelper.NextFloat(.1f,.5f);
        var life = RandomHelper.NextFloat(.1f,1f);
        p.Initialize(where, velo, acc, Color.Goldenrod, scale:scale, lifetime: life);

        base.InitializeParticle(ref p, where);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        AddParticles(_emitter.Position);
    }
    
}