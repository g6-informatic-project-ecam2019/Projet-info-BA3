using System;
using System.Collections.Generic;

namespace Materials
{
    public class Cupboard
    {
        /*Need to ask Bernard*/
        private string angleColor;
        private int width;
        private int depth;
        private Block[] configuration;//block lists
        private Angle[] angles = new Angle[4];
        private int height;
        private float price;
        //private Stock stock = new Stock("Server = localhost; Port = 3306; Database = mykitbox; Uid = root; Pwd =");

        public Cupboard(int depth,int width,string angleColor, int number)/*Builder*/
        {
            this.depth = depth;
            this.width = width;
            this.angleColor = angleColor;
            this.configuration = new Block[number];
        }

        /*Allows adding a block to the list*/
        public void AddBlock(Block block)
        {
            int i;
            int h = 0;
            /*Browses the list*/
            for (i = 0; i < this.configuration.Length; i++)
            {
                /*If there is an available slot, the element is added*/
                if (this.configuration[i] == null)
                {
                    this.configuration[i] = block;
                    h = i;
                    this.height += (int) block.GetDescription()["height"];
                    this.price += block.GetPrice();
                    break;
                }
            }
            /*If more available slots*/
            if (h == 0)
            {
                Console.WriteLine("There must be a size error");
            }
        }

        public void DeletBlock()
        {
            configuration = null;
        }

        public bool BlockStock(int num, Stock sqlstock)
        {
            Part[] parts = configuration[num - 1].GetParts();
            for(int i = 0; i < parts.Length; i++)
            {
                if (!(parts[i].IsAvailable(sqlstock)) )
                {
                    Console.WriteLine("out of stock !");
                    return false;
                }
            }
            return true;
        }

        public Block[] GetBlock()
        {
            if (configuration.Length != 0)
            {
                return configuration;
            }
            else//Test
            {
                Block[] block = new Block[7];
                return block;
            }
        }

        public void AddAngles(Stock sqlStock)
        {
            for (int i = 0; i < 4; i++)
            {
                angles[i] = new Angle(this.height, this.angleColor);
                angles[i].DescriptionRequest(sqlStock);
            }
        }

        public Angle[] GetAngles()
        {
            return angles;
        }

        /*To remove a block from the list*/
        public void RemoveBlock(int number)
        {
            int i;
            int h = 10;
            for (i = 0; i < this.configuration.Length; i++)
            {
                int blockHeight = (int)configuration[i].GetDescription()["height"];
                float blockPrice = configuration[i].GetPrice();
                if (this.configuration[i] == null && i != 0)
                {
                    this.configuration[i - 1] = null;
                    height -= blockHeight;
                    price -= blockPrice;
                    h = i - 1;
                    break;
                }
            }
            if (h == 10)
            {
                Console.WriteLine("There must have a size error");
            }
        }
        /*Compute the height*/
        /*private void ComputeHeight()
        {
            int i;
            int h = 10;
            for (i = 0; i < this.configuration.Length; i++)
            {
                if (this.configuration[i] != null)
                {
                    this.height += (int)this.configuration[i].GetDescription()["height"];
                    h = i;
                }
            }
            if (h == 10)
            {
                this.height = 0;
            }
            AddAngle();
            Console.WriteLine(String.Format("total height of cupboard is : {0}", this.height));
        }*/

        /*Returns a dictionnary of the whole description*/
        public Dictionary<string, Object> GetDescription()
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>
            {
                { "height", this.height },
                { "depth", this.depth },
                { "width", this.width },
                { "price", this.price },
                { "angleColor", this.angleColor }
            };
            return Description;
        }

        /*Computes the price*/
        public float GetPrice()
        {
            return this.price;
        }
    }
}
