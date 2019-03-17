using System;
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
        private Bloc[] configuration;//block lists
        private int height;
        private double price;

        public Cupboard(int depth,int width,string angleColor, int number)/*Builder*/
        {
            this.depth = depth;
            this.width = width;
            this.angleColor = angleColor;
            this.configuration = new Bloc[number];
        }
        public void AddBloc(Bloc bloc)/*Allows adding a block to the list*/
        {
            int i;
            int h = 0;
            for (i = 0; i < this.configuration.Length; i++)/*Browse the list*/
            {
                if (this.configuration[i] == null)/*when there is a free slot we add*/
                {
                    this.configuration[i] = bloc;
                    h = i;
                    break;
                }
            }
            if (h == 0)/*If more free slot*/
            {
                Erreur();
            }
        }
        public Bloc[] GetBloc()
        {
            return configuration;
        }
        private string Erreur()/**/
        {
            return "You have a size error";
        }
        public void AddAngle()
        {
            
        }
        public void RemoveBloc(int number)/*To remove a block from the list*/
        {
            int i;
            int h = 10;
            for (i = 0; i < this.configuration.Length; i++)
            {
                if (this.configuration[i] == null && i != 0)
                {
                    this.configuration[i - 1] = null;
                    h = i - 1;
                    break;
                }
            }
            if (h == 10)
            {
                Erreur();
            }
        }
        private void ComputeHeight() /*Compute the height*/
        {
            int i;
            int h = 10;
            for (i = 0; i < this.configuration.Length; i++)
            {
                if (this.configuration[i] != null)
                {
                    Dictionary<string, Object> bibliBloc = this.configuration[i].GetDescription();
                    this.height += Convert.ToInt32(bibliBloc["height"]);
                    h = i;
                }
            }
            if (h == 10)
            {
                this.height = 0;
            }
            AddAngle();
        }
        
        public Dictionary<string, Object> GetDescription() /*Returns a dico of the whole description*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("height", this.height);
            Description.Add("depth", this.depth);
            Description.Add("width", this.width);
            Description.Add("price", this.price);
            Description.Add("angleColor", this.angleColor);
            return Description;
        }
        public double GetPrice()/*compute the price*/
        {
            int i;
            int h = 10;
            for (i = 0; i < this.configuration.Length; i++)
            {
                if (this.configuration[i] != null)
                {
                    Dictionary<string, Object> bibliBloc = this.configuration[i].GetDescription();
                    this.price += Convert.ToDouble(bibliBloc["price"]);
                    h = i;
                }
            }
            if (i != 10)
            {
                return this.price;
            }
            else
            {
                return 0;
            }
        }
    }
}
