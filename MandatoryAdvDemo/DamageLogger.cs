using AdvSoftMandatory2DWorld.Classes.Interfaces;
using AdvSoftMandatory2DWorld.Classes;
using AdvSoftMandatory2DWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MandatoryAdvDemo
{
    public class DamageLogger : IHitObserver
    {
        public void OnHit(Creature creature, int damageTaken)
        {
            Console.WriteLine($"{creature.Name} took {damageTaken} damage.");
        }
    }
}
    
