using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleSystemExercise;
public class FireworkParticleSystem : ParticleSystem
{
    Color[] colors = new Color[]
    {
        Color.Fuchsia,
        Color.Red,
        Color.Crimson,
        Color.CadetBlue,
        Color.Aqua,
        Color.HotPink,
        Color.LimeGreen,
        Color.Orange
    };

    Color color;
    
    public bool IsRaining {get; set;} = true;

    public FireworkParticleSystem(Game game, int maxExplosions) : base(game, maxExplosions * 25){
    }

    protected override void InitializeConstants()
    {
        textureFilename = "circle";
        minNumParticles = 20;
        maxNumParticles = 25;

        blendState = BlendState.Additive;
        DrawOrder = AdditiveBlendDrawOrder;
    }

    protected override void InitializeParticle(ref Particle p, Vector2 where)
    {
        var velo = RandomHelper.NextDirection() * RandomHelper.NextFloat(40,300);

        var lifetime = RandomHelper.NextFloat(.5f, 1f);

        var acc = -velo / lifetime;

        var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

        var angularVelo = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

        var scale = RandomHelper.NextFloat(4, 6);
        p.Initialize(where, velo, acc, color, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelo, scale: scale);
    }

    protected override void UpdateParticle(ref Particle particle, float dt)
    {
        base.UpdateParticle(ref particle, dt);

        float normalizedLifetime = particle.TimeSinceStart / particle.Lifetime;

        particle.Scale = .2f + .25f * normalizedLifetime;
    }

    public void PlaceFirework(Vector2 where)
    {
        color = colors[RandomHelper.Next(colors.Length)];
        AddParticles(where);

        
    }

}