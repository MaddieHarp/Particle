using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleSystemExercise;
public class ExplosionParticleSystem : ParticleSystem
{
    
    public bool IsRaining {get; set;} = true;

    public ExplosionParticleSystem(Game game, int maxExplosions) : base(game, maxExplosions * 25){
    }

    protected override void InitializeConstants()
    {
        textureFilename = "explosion";
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
        p.Initialize(where, velo, acc, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelo);
    }

    protected override void UpdateParticle(ref Particle particle, float dt)
    {
        base.UpdateParticle(ref particle, dt);

        float normalizedLifetime = particle.TimeSinceStart / particle.Lifetime;
        float alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);
        particle.Color = Color.White * alpha;

        particle.Scale = .2f + .25f * normalizedLifetime;
    }

    public void PlaceExplosion(Vector2 where) => AddParticles(where);




}