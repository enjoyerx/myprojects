using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_GAME
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] field = new int[30,40];
            Player player = new Player( 100, false, 1);
            player.GetDamage(10);
            player.GetDamage(100);
        }
    }
    public class Cell
    {
        private int passability;
        protected int width;
        protected int height;
        public int Passability
        {
            get { return passability; }
            set { passability = value; }
        }
        public int Width
        {
            get { return width; }
            set { Width = value % 30; }
        }
        public int Height
        {
            get { return height; }
            set { height = value % 40; }
        }

        public Cell(int _passability, int _width, int _height)
        {
            passability = _passability;
            width = _width % 30;
            height = _height % 40;
        }
    }
    public class Bonus
    {
        public Cell Cell { get; set; }

        public int level;
        public Bonus(int _level)
        {
            level = _level;
        }
        public int Level { get; set; }
    }
    public class Apple : Bonus
    {
        private int med;
        public Apple(int _med, int _level) : base(_level)
        {
            med = _med;
        }
        public int Med
        {
            get { return med; }
            set { med = value; }
        }
    }
    public class Pineapple : Bonus
    {
        public Pineapple(int _level) : base(_level)
        {

        }
    }
    public class Monster
    {
        public Cell Cell { get; set; }
        
        private int force;
        private int algorithm;
        public Monster(int _force)
        {
            force = _force;
        }
        public int Force
        {
            get { return force; }
            set { force = value; }
        }
    }
    public class Wolf : Monster
    {
        public Wolf(int _force) : base( _force)
        {

        }
    }
    public class Griffin : Monster
    {
        public Griffin(int _force) : base( _force)
        {

        }
    }
    public class Player
    {
        public Cell Cell { get; set; }
        
        private int health;
        private bool isDeceleration;
        private int speed;
        public Player(int _health, bool _isDeceleration, int _speed)
        {
            health = _health;
            isDeceleration = _isDeceleration;
            speed = _speed;
        }
        public bool IsDeceleration
        {
            get { return isDeceleration; }
            set { isDeceleration = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public void GetDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Console.WriteLine("You give " + damage + " points of damage");
                Console.WriteLine("GameOver");
            }
            else
            {
                Console.WriteLine("You give " + damage + " points of damage");
            }
        }
    }
}