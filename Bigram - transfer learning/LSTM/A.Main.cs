﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MathNet.Numerics.Distributions;

namespace Program
{
    class Program
    {


        static void readidoimword()
        {
            StreamReader sr = new StreamReader("idoim.txt", Encoding.UTF8);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Global.fourword.Add(line.ToString().Trim());
            }


        }


        static void readword()
        {
            StreamReader sr = new StreamReader("word.txt", Encoding.Default);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Global.word.Add(line.ToString());
            }
        }

        static void readbigramword()
        {
            StreamReader sr = new StreamReader("bigramword.txt", Encoding.UTF8);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Global.bigramword.Add(line.ToString());
            }
        }
        static void Main(string[] args)
        {

            Console.WriteLine("Choose running mode: 1. training, 2. testing");
            string mode = Console.ReadLine();
            if (mode == "1")
            {
                Global.mode = "train";
            }
            else if (mode == "2")
            {
                Global.mode = "test";
            }

            //Console.WriteLine("Choose reading mode: 1. read saved model, 2. read model trained on MSR dataset.");
            //string read = Console.ReadLine();
            //string modes = Console.ReadLine();
            //if (modes == "1")
            //{
            //    Global.isRead = true;
            //}
            //else if (modes == "2")
            //{
            //    Global.isRead = false;
            //}

            Console.WriteLine("Choose the bigram feature mode: 1. read bigram features, 2. create bigram features");
            string bigramfeature = Console.ReadLine();
            if (bigramfeature == "1")
            {
                Global.isReadBigramfeature = true;
            }
            else if (bigramfeature == "2")
            {
                Global.isReadBigramfeature = false;
            }

            readword();
            readbigramword();
            readidoimword();

            Global.randn = new Normal();

            Global._UpLSTMLayer = LSTMLayer.readLSTM("model\\lstmmodel.txt");
            Global._UpLSTMLayerr = LSTMLayer.readLSTM("model\\lstmmodelr.txt");
            // Global._LSTMLayerr = LSTMLayer.readLSTM("model\\lstmmodelr.txt");
            Global._GRNNLayer1 = GRNNLayer.readGRNN("model\\grnnmodel1.txt");
            Global._GRNNLayer2 = GRNNLayer.readGRNN("model\\grnnmodel2.txt");
            Global._GRNNLayer3 = GRNNLayer.readGRNN("model\\grnnmodel3.txt");
            Global._GRNNLayer4 = GRNNLayer.readGRNN("model\\grnnmodel4.txt");


            Global._feedForwardLayer = FeedForwardLayer.readFF("model\\feedforwardmodel.txt");



            LSTMLayer.getSerializeWordembedding();
            LSTMLayer.getSerializeBigramWordembedding();




            DataSet X = new DataSet();

            Trainer.train(X);

            Global.swLog.Close();


        }
    }
}