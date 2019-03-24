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
        }
        public void BuildPieces()
        {
            for (int i = 0; i < 4; i++)
            {
                this.parts[i] = new Cleat(5, this.height - 4);/*cleat1-4*/
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
            for (int p = 0; p < this.parts.Length; p++)
            {
                this.parts[p].DescriptionRequest(this.stock);
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
        public Piece[] GetPieces()
        {
            return parts;
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
