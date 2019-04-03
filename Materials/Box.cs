using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class Box : Bloc
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
        private Piece[] parts = new Piece[15];
        private Stock stock;

        public Box(int height, string pannelsColor, bool hasdoor, Cupboard cupboard, string typedoor, string doorcolor) /*builder*/
        {
            this.height = height;
            this.pannelsColor = pannelsColor;
            this.hasdoor = hasdoor;
            this.doorcolor = doorcolor;
            this.Cupboard = cupboard.GetDescription();
            this.depth = Convert.ToInt32(Cupboard["depth"]);
            this.width = Convert.ToInt32(Cupboard["width"]);
            this.stock = new Stock("Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd=");
            BuildPieces();
            ComputePrice();
        }
        public void BuildPieces() /* !!!!!!!!!!!!!!!!! CORRECTION TO BE MADE 4 LINES BELOW !!!!!!!!!!!!!!!!!!!!!!!!!! */
        {
            for (int i = 0; i < 4; i++)
            {
                this.parts[i] = new Cleat(5, this.height );/*cleat1-4*/ //add -4 when options in graphical interface is corrected 
                if (i < 2)                                  //there are only three pannels in a box (front = door)
                {
                    this.parts[i+4]  = new Breadth(5, this.depth, "GD");
                    this.parts[i+8] = new Panel(5, this.height, this.pannelsColor, this.depth,"GD");/*panelGD1*/
                    this.parts[i+10] = new Panel(5, this.height, this.pannelsColor, this.depth, "HB");
                }
            }
            this.parts[6] = new Breadth(5, this.depth, "Av");
            this.parts[7] = new Breadth(5, this.depth, "Ar");
            this.parts[12] = new Panel(5, this.height, this.pannelsColor, this.depth, "AR");

            if (hasdoor == true)
            {
                if (typedoor== "ClassicDoor")
                {
                    this.parts[13] = new ClassicDoor(5, this.height, this.doorcolor, this.width);/*classicdoor1*/
                    this.parts[14] = new ClassicDoor(5, this.height, this.doorcolor, this.width);/*classicdoor2*/
                }
                else
                {
                    this.parts[13] = new GlassDoor(5, this.height, this.width);/*glassdoor1*/
                    this.parts[14] = new GlassDoor(5, this.height, this.width);/*glassdoor2*/
                }
            }
            for (int p = 0; p < this.parts.Length; p++) //after pieces are built, 
            {
                if (parts[p] != null)
                {
                    this.parts[p].DescriptionRequest(this.stock);
                }
                else
                {
                    if ((p != 13)||(p!= 14)) //if part is null and p is 13 or 14, it just means that there is no door
                    {
                        Console.WriteLine("there seem to be a missing piece");
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
        public Piece[] GetPieces()
        {
            return parts;
        }
        public float GetPrice()
        {
            return price; 
        }
        public Dictionary<string, Object> GetDescription()/*Dictionary that contains all the elements of the box*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("height", height);
            Description.Add("depth", depth);
            Description.Add("width", width);
            Description.Add("price", price);
            Description.Add("panel", this.pannelsColor);
            if (this.hasdoor == true)
            {
                if (this.typedoor == "ClassicDoor")
                {
                    Description.Add("door", this.doorcolor);
                }
                else
                {
                    Description.Add("door","Glass door");
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
