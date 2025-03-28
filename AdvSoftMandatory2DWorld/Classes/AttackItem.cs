namespace AdvSoftMandatory2DWorld.Classes
{
    public class AttackItem : WorldObject
    {
        public int Damage { get; }
        public int Hit { get; set; }
        public int Range { get; set; }

        public AttackItem(string name, int x, int y, bool lootable, bool removable, int damage, int hit, int range)
            : base(name, x, y, lootable, removable)
        {
            Damage = damage;
            Hit = hit;
            Range = range;
        }
    }
}
