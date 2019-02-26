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
        private Dictionary<string, Object> dicPanelGD;
        private Dictionary<string, Object> dicPanelAR;
        private Dictionary<string, Object> dicDoor;
        private string pannelsColor;
        private int height;
        private int depth;
        private int width;


        public Box(Cleat cleat, Breadth breadthGD, Breadth breadthAR, Panel panelGD, Panel panelAR, Door door) /*builder*/
        {
            this.dicCleat = cleat.GetDescription();
            this.dicBreadthGD = breadthGD.GetDescription();
            this.dicBreadthAR = breadthAR.GetDescription();
            this.dicPanelGD = panelGD.GetDescription();
            this.dicPanelAR = panelAR.GetDescription();
            this.dicDoor = door.GetDescription();
            this.height = Convert.ToInt32(dicCleat["length"])+2*20; /*The height of a box = at the height of the cleats + 2 * the height of a breadth (2cm)*/
            this.depth = Convert.ToInt32(dicBreadthGD["length"]); /*the depth is the length of the breadthGD*/
            this.width = Convert.ToInt32(dicBreadthAR["length"]); /*the depth is the length of the breadthAR*/
            this.pannelsColor = Convert.ToString(dicPanelGD["color"]); /*Panel color*/
        }
        public void SetColor(string color)
        {
            this.pannelsColor = color;
            dicPanelAR["color"] = color;
            dicPanelGD["color"] = color;
        }
        public void SetDimensions(int Dimention)
        {

        }
        public double GetPrice()
        {
            return Convert.ToDouble(dicCleat["price"])+ Convert.ToDouble(dicPanelGD["price"]) + Convert.ToDouble(dicPanelAR["price"]) + Convert.ToDouble(dicDoor["price"])+ Convert.ToDouble(dicBreadthGD["price"])+ Convert.ToDouble(dicBreadthAR["price"])
        }
        public object GetDescription()
        {
            return 0;
        }
    }
}
