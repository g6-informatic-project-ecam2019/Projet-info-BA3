using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class Box
    {
        private Dictionary<string, Object> dicCleat;
        private Dictionary<string, Object> dicBreadthGD;
        private Dictionary<string, Object> dicBreadthAR;
        private Dictionary<string, Object> dicBreadthAV;
        private Dictionary<string, Object> dicPanelGD;
        private Dictionary<string, Object> dicPanelAR;
        private Dictionary<string, Object> dicDoor;
        private string pannelsColor;
        private int height;
        private int depth;
        private int width;
        private double price;

        public Box(Cleat cleat, Breadth breadthGD, Breadth breadthAR, Breadth breadthAV, Panel panelGD, Panel panelAR, Door door) /*builder*/
        {
            this.dicCleat = cleat.GetDescription();
            this.dicBreadthGD = breadthGD.GetDescription();
            this.dicBreadthAR = breadthAR.GetDescription();
            this.dicBreadthAV = breadthAV.GetDescription();
            this.dicPanelGD = panelGD.GetDescription();
            this.dicPanelAR = panelAR.GetDescription();
            this.dicDoor = door.GetDescription();

            this.height = Convert.ToInt32(dicCleat["length"])+2*20; /*The height of a box = at the height of the cleats + 2 * the height of a breadth (2cm)*/
            this.depth = Convert.ToInt32(dicBreadthGD["length"]); /*the depth is the length of the breadthGD*/
            this.width = Convert.ToInt32(dicBreadthAR["length"]); /*the depth is the length of the breadthAR*/
            this.pannelsColor = Convert.ToString(dicPanelGD["color"]); /*Panel color*/
            this.price = Convert.ToDouble(dicCleat["price"]) * 4 + Convert.ToDouble(dicPanelGD["price"]) * 2 +
                         Convert.ToDouble(dicPanelAR["price"]) * 1 + Convert.ToDouble(dicDoor["price"]) * 1 +
                         Convert.ToDouble(dicBreadthGD["price"]) * 2 + Convert.ToDouble(dicBreadthAR["price"]) * 1 +
                         Convert.ToDouble(dicBreadthAV["price"]) * 1;
        }
        public void SetColor(string color)
        {
            this.pannelsColor = color;
            dicPanelAR["color"] = color;
            dicPanelGD["color"] = color; /*Demander a bernard l'interet*/
        }
        public void SetDimensions(int Dimention)
        {
            /*Demander à bernard a quoi ça sert dans son diagramme*/
        }
        public double GetPrice()
        {
            return price; /*Demander a bernard l'interet ? */
        }
        public Dictionary<string, Object> GetDescription()
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("height", height);
            Description.Add("depth", depth);
            Description.Add("width", width);
            Description.Add("price", price);
            return Description;
        }
    }
}
