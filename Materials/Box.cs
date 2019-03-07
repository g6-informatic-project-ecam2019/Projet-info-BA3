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
        private double price;
        private Piece[] parts = new Piece[13];

        public Box(int height, string pannelsColor, bool hasdoor, Cupboard cupboard, string typedoor= null, string doorcolor = null) /*builder*/
        {
            this.height = height;
            this.pannelsColor = pannelsColor;
            this.hasdoor = hasdoor;
            this.doorcolor = doorcolor;
            this.Cupboard = cupboard.GetDescription();
            this.depth = Convert.ToInt32(Cupboard["depth"]);
            this.width = Convert.ToInt32(Cupboard["width"]);
            BuildPieces();
        }
        public void BuildPieces()
        {
            for (int i = 0; i < 4; i++)
            {
                this.parts[i] = new Cleat(5, this.height - 4);/*cleat1-4*/
                this.parts[i+4] = new Breadth(5, this.depth);/*breadthGD1-2-AR-AV*/
                if (i!= 3)
                {
                    this.parts[i + 8] = new Panel(5, this.height, this.pannelsColor, this.depth);/*panelGD1*/
                }
            }

            if (hasdoor == true)
            {
                if (typedoor== "ClassicDoor")
                {
                    this.parts[11] = new ClassicDoor(5, this.height, this.doorcolor, this.width);/*classicdoor1*/
                    this.parts[12] = new ClassicDoor(5, this.height, this.doorcolor, this.width);/*classicdoor2*/
                }
                else
                {
                    this.parts[11] = new GlassDoor(5, this.height, this.width);/*glassdoor1*/
                    this.parts[12] = new GlassDoor(5, this.height, this.width);/*glassdoor2*/
                }
            }
        }
        private void ComputePrice()
        {
            int i;
            for (i = 0; i < this.parts.Length; i++)
            {
                if(this.parts[i] != null)
                {
                    Dictionary<string, Object> DicDescri = this.parts[i].GetDescription();
                    this.price += Convert.ToDouble(DicDescri["price"]);
                }
            }
        }
        public double GetPrice()
        {
            ComputePrice();
            return price;
        }
        public Dictionary<string, Object> GetDescription()/*Dictionary that contains all the elements of the box*/
        {
            ComputePrice();
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("height", height);
            Description.Add("depth", depth);
            Description.Add("width", width);
            Description.Add("price", price);
            return Description;
        }
    }
}
