namespace GameProject.Entities
{
    public class Projectile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsActive { get; set; } = true;

        public Projectile(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}