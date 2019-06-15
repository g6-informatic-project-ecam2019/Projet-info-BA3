using System;
using System.Collections.Generic;

namespace Materials
{
    class Box : Block
    {
        private string pannelsColor;
        private int height;
        private int depth;
        private int width;
        private bool hasdoor;
        private string typedoor;
        private string doorcolor;
        private Dictionary<string, Object> Cupboard;
        private float price;
        private Part[] parts = new Part[15];
        private Stock stock;

        /*Builder*/
        public Box(int height, string pannelsColor, Cupboard cupboard, string door)
        {
            this.height = height;
            this.pannelsColor = pannelsColor;
            this.hasdoor = (door == "No door") ? false : true;
            this.doorcolor = ((door == "No door") || (door == "Glass")) ? null : door;
            this.typedoor = (door == "Glass") ? "GlassDoor" : "ClassicDoor";
            this.Cupboard = cupboard.GetDescription();
            this.depth = Convert.ToInt32(Cupboard["depth"]);
            this.width = Convert.ToInt32(Cupboard["width"]);
            this.stock = new Stock("Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd=");
            BuildParts();
            ComputePrice();
        }

        public void BuildParts()
        {
            for (int i = 0; i < 4; i++)
            {
                /*cleat1-4
                 * add -4 when options in graphical interface is corrected*/
                this.parts[i] = new Cleat(5, this.height - 4);
                //there are only three pannels in a box (front = door)
                if (i < 2)
                {
                    this.parts[i+4]  = new Breadth(5, this.depth, "GD");
                    this.parts[i+8] = new Panel(5, this.height - 4, this.pannelsColor, this.depth,"GD");/*panelGD1*/
                    this.parts[i+10] = new Panel(5, this.width, this.pannelsColor, this.depth, "HB");
                }
            }

            this.parts[6] = new Breadth(5, this.width, "Av");
            this.parts[7] = new Breadth(5, this.width, "Ar");
            this.parts[12] = new Panel(5, this.height - 4, this.pannelsColor, this.width, "Ar");

            if (hasdoor == true)
            {
                int doorWidth = (width > 62) ? ((width / 2) + 2) : 32; 
                if (typedoor == "ClassicDoor")
                {
                    /*classicdoor1*/
                    this.parts[13] = new ClassicDoor(5, doorWidth, this.doorcolor, this.height - 4);

                    /*classicdoor2*/
                    this.parts[14] = new ClassicDoor(5, doorWidth, this.doorcolor, this.height - 4);
                }
                else if (typedoor == "GlassDoor")
                {
                    /*glassdoor1*/
                    this.parts[13] = new GlassDoor(5, doorWidth, this.height - 4);

                    /*glassdoor2*/
                    this.parts[14] = new GlassDoor(5, doorWidth, this.height - 4);
                }
                else
                {
                    Console.WriteLine("Error : no such type of door");
                }
            }

            //After parts are built
            for (int p = 0; p < this.parts.Length; p++) 
            {
                if (parts[p] != null)
                {
                    this.parts[p].DescriptionRequest(this.stock);
                }
                else
                {
                    //if part is null and p is 13 or 14, it just means that there is no door
                    if ((p != 13) && (p!= 14))
                    {
                        Console.WriteLine("there seem to be a missing part");
                    }
                }
            }
            ComputePrice();
        }

        private void ComputePrice()
        {
            int i;
            this.price = 0;
            for (i = 0; i < this.parts.Length; i++)
            {
                if(this.parts[i] != null)
                {
                    this.price += (float)this.parts[i].GetPrice();
                }
            }
        }

        public Part[] GetParts()
        {
            return parts;
        }

        public float GetPrice()
        {
            return price; 
        }

        /*Dictionary that contains all the elements of the box*/
        public Dictionary<string, Object> GetDescription()
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>
            {
                { "height", height },
                { "depth", depth },
                { "width", width },
                { "price", price },
                { "panel", this.pannelsColor }
            };
            if (this.hasdoor == true)
            {
                if (this.typedoor == "ClassicDoor")
                {
                    Description.Add("door", this.doorcolor);
                }
                else if (this.typedoor == "GlassDoor")
                {
                    Description.Add("door","Glass door");
                }  
                else
                {
                    Description.Add("door", "ERROR : door type non-existent");
                    Console.WriteLine(String.Format("ERROR : WRONG TYPE OF DOOR. \n Current door type is : {0}", this.typedoor));
                }
            }
            else
            {
                Description.Add("door", "No door");
            }
            return Description;
        }
    }
}
