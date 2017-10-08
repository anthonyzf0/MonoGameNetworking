using Encog.ML.Data.Basic;
using Encog.Util.Arrayutil;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameNetworking.GameItself
{
    class SnakeGame
    {
        //Network Data
        private readonly NormalizedField location,speed;

        //Game stats
        private Random rand = new Random();

        Double velX, velY;
        Double posX = 100, posY=100;
        Double targetx=300, targety=300;
        
        public double score = 0;
        private int tics = 0;

        public SnakeGame() {

            location = new NormalizedField(NormalizationAction.Normalize, "Location", 600, 0, -0.9, 0.9);
            speed = new NormalizedField(NormalizationAction.Normalize, "Speed", 20, -20, -0.9, 0.9);
        }
        

        public BasicMLData inputs()
        {
            var input = new BasicMLData(6);

            input[0] = location.Normalize(posX);
            input[1] = location.Normalize(posY);
            input[2] = speed.Normalize(velX);
            input[3] = speed.Normalize(velY);
            input[4] = location.Normalize(targetx);
            input[5] = location.Normalize(targety);

            return input;
        }

        public void update(double x, double y)
        {
            velX += x;
            velY += y;

            posX += velX;
            posY += velY;

            if (new Rectangle((int)posX-15, (int)posY -15,30,30).Intersects(new Rectangle((int)targetx -15, (int)targety - 15, 30, 30)))
            {
                tics = 0;

                score++;
                targetx = rand.Next(400)+50;
                targety = rand.Next(400)+50;
            }

            if (posX <= 0 || posX >= 600 || posY <= 0 || posY >= 600)
                score -= 1;

            if (posX <= 0)
            {
                posX = 0;
                velX = 0;
            }
            if (posY <= 0)
            {
                posY = 0;
                velY = 0;
            }

            if (posX >= 600)
            {
                posX = 600;
                velX = 0;
            }
            if (posY >= 600)
            {
                posY = 600;
                velY = 0;
            }

            tics++;
            if (tics > 200)
            {
                tics = 0;
                score-=5;
                targetx = rand.Next(400) + 50;
                targety = rand.Next(400) + 50;
            }

        }

        public void draw(Render render)
        {
            render.draw(50, 50, 600, 600, Color.Black);
            render.draw((int)posX - 15+50, (int)posY - 15+50, 30, 30, Color.LightCoral);
            render.draw((int)targetx - 15+50, (int)targety - 15+50, 30, 30, Color.Azure);
        }

    }
}
