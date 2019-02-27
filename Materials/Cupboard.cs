﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class Cupboard
    {
        private string angleColor; /*Demander à bernard*/
        private int width;
        private int depth;
        private Bloc[] configuration = new Bloc[7]; //block lists
        private int height;
        private double price;

        public Cupboard()/*Builder*/
        {
            height = ComputeHeight();
            price = GetPrice();
            width = ComputeDepth();
            depth = ComputeWidth();
        }
        public void AddBloc(Bloc bloc)/*Allows adding a block to the list*/
        {
            int i;
            int h = 0;
            for (i = 0; i < configuration.Length; i++)/*Browse the list*/
            {
                if (configuration[i] == null)/*when there is a free slot we add*/
                {
                    configuration[i] = bloc;
                    h = i;
                    break;
                }
            }
            if (h == 0)/*If more free slot*/
            {
                Erreur();
            }
        }
        private string Erreur()/**/
        {
            return "You have a size error";
        }
        public void CheckDimension(int x, int y)
        {
            /*Demander à bernard ? */
        }
        public void RemoveBloc()/*To remove a block from the list*/
        {
            int i;
            int h = 10;
            for (i = 0; i < configuration.Length; i++)
            {
                if (configuration[i] == null && i != 0)
                {
                    configuration[i - 1] = null;
                    h = i - 1;
                    break;
                }
            }
            if (h == 10)
            {
                Erreur();
            }
        }
        private int Compute(string name)
        {
            int i;
            int var = 0;
            int h = 10;
            for (i = 0; i < configuration.Length; i++)
            {
                if (configuration[i] != null)
                {
                    Dictionary<string, Object> bibliBloc = configuration[i].GetDescription();
                    var += Convert.ToInt32(bibliBloc[name]);
                    h = i;
                }
            }
            if (i != 10)
            {
                return var;
            }
            else
            {
                return 0;
            }

        }
        private int ComputeHeight()
        {
            return Compute("height");/*Error, but for now it's okay to rectify*/
        }
        private int ComputeDepth()
        {
            return Compute("depth");/*Error, but for now it's okay to rectify*/
        }
        private int ComputeWidth()
        {
            return Compute("width");/* Error, but for now it's okay to rectify*/
        }
        public Dictionary<string, Object> GetDescription() /*Returns a dico of the whole description*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("height", height);
            Description.Add("depth", depth);
            Description.Add("width", width);
            Description.Add("price", price);
            Description.Add("angleColor", angleColor);
            return Description;
        }
        public double GetPrice()/*compute the price*/
        {
            int i;
            int h = 10;
            for (i = 0; i < configuration.Length; i++)
            {
                if (configuration[i] != null)
                {
                    Dictionary<string, Object> bibliBloc = configuration[i].GetDescription();
                    price += Convert.ToDouble(bibliBloc["price"]);
                    h = i;
                }
            }
            if (i != 10)
            {
                return price;
            }
            else
            {
                return 0;
            }
        }
    }
}
