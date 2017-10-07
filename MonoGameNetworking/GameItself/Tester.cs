using Encog.Neural.Networks.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encog.ML;
using Encog.ML.Data.Basic;
using Encog.ML.Data;
using Encog.Neural.Networks;

namespace MonoGameNetworking.GameItself
{
    class Tester : ICalculateScore
    {
        public bool RequireSingleThreaded
        {
            get { return false; }
        }

        public bool ShouldMinimize
        {
            get { return false; }
        }

        public double CalculateScore(IMLMethod network)
        {
            //1000 tics to get best score
            SnakeGame game = new SnakeGame();
            BasicNetwork net = (BasicNetwork)network;

            for (int i = 0; i < 1000; i++)
            {
                var input = game.inputs();
                IMLData output = net.Compute(input);
                game.update(output[0], output[1]);

            }

            return game.score;
            
        }
    }
}
