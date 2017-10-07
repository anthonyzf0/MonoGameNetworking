using Encog.Engine.Network.Activation;
using Encog.ML;
using Encog.ML.Data;
using Encog.ML.Genetic;
using Encog.ML.Train;
using Encog.Neural.Networks;
using Encog.Neural.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonoGameNetworking.GameItself
{
    class Trainer
    {

        public static BasicNetwork train(Render render)
        {
            BasicNetwork network = CreateNetwork(6, 2, 10);

            IMLTrain train;

            train = new MLMethodGeneticAlgorithm(() =>
            {

                BasicNetwork result = CreateNetwork(6, 2, 10);
                ((IMLResettable)result).Reset();
                return result;

            }, new Tester(), 1000);

            int epoch = 1;

            for (int i = 0; i < 90; i++)
            {
                train.Iteration();
                Console.WriteLine(@"Epoch #" + epoch + @" Score:" + train.Error);
                epoch++;
            }
            
            return (BasicNetwork)train.Method;
            
        }


        public static BasicNetwork CreateNetwork(int start, int end, params int[] layers)
        {
            var pattern = new FeedForwardPattern { InputNeurons = start };

            foreach (int i in layers)
                pattern.AddHiddenLayer(i);

            pattern.OutputNeurons = end;
            pattern.ActivationFunction = new ActivationTANH();
            var network = (BasicNetwork)pattern.Generate();
            network.Reset();
            return network;
        }
    }
}

