using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Geometries;
using TGC.MonoGame.TP.Elements;

namespace TGC.MonoGame.TP.Elements
{
    public class MovingObject : Object
    {
        private List<Vector3> Points { get; set; }

        private int movementType { get; set; } //1 = Linear, 2 = Circuit, 3 = Random
        private float speed { get; set; }

        private int direction = 1;
            
        private int lastPos { get; set; }
        private int nextPos { get; set; }

        public MovingObject(List<Vector3> Points, GraphicsDevice graphicsDevice, ContentManager content, Color color, int movementType = 1, float speed = 10f)
        {
            this.Points = Points;
            this.movementType = movementType;
            this.speed = speed;
            //Inicializacion por tipo de movimiento
            if (movementType == 1)
            {
                lastPos = 0;
                nextPos = 1;
            } else if (movementType == -1)
            {
                lastPos = Points.Count - 1;
                nextPos = Points.Count - 2;
                direction = -1;
            }
            else if (movementType == 2)
            {
                lastPos = 0;
                nextPos = 1;
            }
            else if (movementType == -2)
            {
                lastPos = Points.Count - 1;
                nextPos = Points.Count - 2;
                direction = -1;
            }
            else if (movementType == 3)
            {
                Random rnd = new Random();
                lastPos = 0;
                nextPos = rnd.Next(0, Points.Count - 1);
            }
            else if (movementType == -3)
            {
                Random rnd = new Random();
                lastPos = rnd.Next(0, Points.Count - 1);
                nextPos = rnd.Next(0, Points.Count - 1);
            }
            Position = Points[lastPos];

        }
            
        public void Move(GameTime gameTime)
        {
            if (Points.Count < 2) return;
            float deltaTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            float marginError = 2 * speed * deltaTime;
            Position = ConstantSpeedLerp(Position, Points[lastPos], Points[nextPos], speed * deltaTime);
            if (Vector3.Distance(Position, Points[nextPos]) < marginError)
            {
                if(movementType == 1 || movementType == -1) //Linear movement, el objeto va del punto 0 al ultimo y vuelve del punto 0 al ultimo, recorriendo cada punto en medio. En negativo el bloque empieza en el ultimo punto y va para el primero. 
            {
                    if (nextPos + direction > Points.Count - 1 || nextPos + direction < 0) direction *= -1;
                    lastPos = nextPos;
                    nextPos = nextPos + direction;
                }
                else if (movementType == 2 || movementType == -2) //Circuit movement, el objeto va del punto 0 al ultimo recorriendo cada punto en medio y vuelve del punto 0 al ultimo directamente. En negativo el bloque empieza en 0 pero va en sentido contrario.
                {
                    lastPos = nextPos;
                    nextPos = nextPos + direction;
                    if (nextPos > Points.Count - 1) nextPos = 0;
                    if (nextPos < 0) nextPos = Points.Count - 1;
                }
                else if (movementType == 3 || movementType == -3) //Random movement, el objeto va de un punto a otro aleatorio. En negativo el bloque empieza en un punto aleatorio tambien.
                {
                    Random rnd = new Random();
                    lastPos = nextPos;
                    nextPos = rnd.Next(0, Points.Count - 1);
                }
            }
        }

        private Vector3 ConstantSpeedLerp(Vector3 position, Vector3 start, Vector3 end, float speed) {
            return position - (start - end) / Vector3.Distance(start, end) * speed;
        }

    }

}

