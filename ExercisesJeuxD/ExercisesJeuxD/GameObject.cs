using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExercisesJeuxD
{
    class GameObject
    {
        public Vector2 direction;
        public Rectangle position;
        public int vitesse;
        public Texture2D sprite;
        public bool estVivant;
    }
}
