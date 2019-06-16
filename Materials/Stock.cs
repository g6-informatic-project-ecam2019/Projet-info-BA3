/*-----------------------------------------------------------*/
/* This class uses mySql library to connect and send various */
/*  requests to the database.                                */
/*                      Using tutorial :                     */
/*    https://www.youtube.com/watch?v=IcD9Jffstmw&t=184s     */
/*-----------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Materials
{

    public class Stock 
    {
        private string connString;
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public Stock(string connString) {
            this.connString = connString; //"Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd=; Connect Timeout=60"
        }

        private void Connect() {
            connection = new MySqlConnection(this.connString);
            command = connection.CreateCommand();
            try {
                connection.Open();
            }
            catch (Exception ex) { //if connection fails, shows a popup
                DialogResult dialog =  MessageBox.Show(String.Format("Unabled to connect to the database, please check your connection. \n Error is : {0}",ex), "Error", MessageBoxButtons.OK);
                if (dialog == DialogResult.OK) {
                    this.Connect(); //recursive call => as long as the connection fails, the popup will reappear
                }
            }
        }

        /***************************************************************************
         * Pre : - Receives a part as parameter                                    *
         *       - Database server is on                                           *
         * Post : - returns true if the part is available, false otherwise         *
         ***************************************************************************/
        public bool IsAvailable (Part part) {
            string dimension1 = "";
            string dimension2 = "";
            string dimension = "";
            string color = "";
            int lenght = -1;
            int width = -1;
            try {
                dimension1 = part.GetDescription()["dim1"].ToString();
                dimension2 = part.GetDescription()["dim2"].ToString();
                lenght = (int)part.GetDescription()["length"];
                width = (int)part.GetDescription()["width"];
            }
            catch (KeyNotFoundException) {
                dimension = part.GetDescription()["dim"].ToString();
                lenght = (int)part.GetDescription()["length"];
            }
            try {
                color = part.GetDescription()["color"].ToString();
            }
            catch (KeyNotFoundException) {
                if (part is GlassDoor) {
                    color = "verre";
                }
            }
            //retrieve part infos in the part table => if real quantity is null, then part is not available
            Connect(); 
            command.CommandText = String.Format("SELECT * FROM part WHERE ref='{0}'", part.GetDescription()["ref"]);
            reader = command.ExecuteReader();
            while (reader.Read()){
                if (dimension1 != ""){ //part is in two dimensions (e.g. a panel or a door)
                    if (((int)reader[dimension1] == lenght) && ((int)reader[dimension2] == width)){
                        if (color != ""){
                            if (reader["color"].ToString() == color){
                                if ((int)reader["real_quantity"] != 0){
                                    return true;
                                }
                                else{
                                    return false;
                                }}
                        }
                        else{
                            if ((int)reader["real_quantity"] != 0){
                                return true;
                            }
                            else{
                                return false;
                            } }
                    }
                }
                else{ //part is in one dimension (e.g. a cleat or an breadth)
                    if ((int)reader[dimension] == lenght){
                        if (color != ""){
                            if(reader["color"].ToString() == color){
                                if ((int)reader["real_quantity"] != 0){
                                    return true;
                                }
                                else{
                                    return false;
                                }}
                        }
                        else{
                            if ((int)reader["real_quantity"] != 0) {
                                return true;
                            }
                            else{
                                return false;
                            } }
                    }
                }   
            }
            connection.Close();
            return false;
        }

        /*--------------------------------------------------------------------------*
         * Pre : - Receives a part as a parameter                                   *
         *       - Database server is on                                            *
         * Post : - Update the database => decrease the virtual quantity of the     *
         *           ordered part                                                   *                                                        
         *--------------------------------------------------------------------------*/
        public void OrderPart(Part part){
            /*string code ="";
            int quantity = 0;
            int newQuantity = 0;
            try {
                string dimension1 = part.GetDescription()["dim1"].ToString();
                string dimension2 = part.GetDescription()["dim2"].ToString();
                if (part is GlassDoor) {
                    try {
                        code = SelectPart2D(part, dimension1, dimension2, "verre", boxDescription[countPiece], reader);
                    }
                    catch (KeyNotFoundException) {}
                }
                else {
                    try{
                        code = SelectPart2D(part, dimension1, dimension2, part.GetDescription()["color"].ToString(), boxDescription[countPiece], reader);
                    }
                    catch (KeyNotFoundException) {}
                }
            }
            catch (KeyNotFoundException) { //an error is raised if dim1-dim2 are not in part description => there is only one dim
                try {
                    string dimension = part.GetDescription()["dim"].ToString();
                }
                catch(KeyNotFoundException) {
                    Console.WriteLine(part.GetType());
                }
                try {
                    code = SelectPart(part, part.GetDescription()["dim"].ToString(), part.GetDescription()["color"].ToString(), boxDescription[countPiece], reader);
                }
                catch (KeyNotFoundException) {
                    code = SelectPart(part, part.GetDescription()["dim"].ToString(), "", boxDescription[countPiece], reader);
                }
            }
            Connect();
            this.command.CommandText = String.Format("SELECT * FROM part WHERE code='{0}'", code);
            reader = command.ExecuteReader();
            while (reader.Read()) {
                quantity = (int)reader["virtual_quantity"];
            }
            //NOTE : newQuantity CAN BE NEGATIVE
            newQuantity = quantity - 1;
            
            connection.Close();
            Connect();

            //NOTE : virtual quantity CAN BE NEGATIVE
            this.command.CommandText = String.Format("UPDATE part SET virtual_quantity='{0}' WHERE code='{1}'",  newQuantity, code);
            int l = command.ExecuteNonQuery(); 
            if (l != 1) {
                Console.WriteLine(String.Format("WARNING : database was not edited correctly : {0} lines were modified instead of one.", l));
            }
            connection.Close();*/
        }

        /*****************************************************************************
         * Pre : - Receives a cupboard as a parameter                                *
         *       - Database server is on                                             *
         * Post : Calls OrderPart for each par of the cupboard                       *
         *****************************************************************************/
        public Dictionary<string, int>  MakeOrder(Cupboard cupboard) {
            /*Maximum number of arts in a cupboard is (7 boxes * 15 parts) + 4 angles = 109*/
            Dictionary<string, int> quantities = new Dictionary<string, int>();
            for (int b = 0; b < cupboard.GetBlock().Length; b++) {
                Block block = cupboard.GetBlock()[b];
                for (int p = 0; p < block.GetParts().Length; p++) {
                    Part part = block.GetParts()[p];
                    if (part != null) {
                        string code = part.GetDescription()["code"].ToString();
                        /*Part is counted as ordered*/
                        OrderPart(part);
                        if (quantities.ContainsKey(code)) {
                            quantities[code] += 1;
                        }
                        else {
                            quantities.Add(code, 1);
                        }
                    }
                } 
            }
            Angle[] angles = cupboard.GetAngles();
            if (angles.Length == 4) {
                quantities.Add(angles[0].GetDescription()["code"].ToString(), 4);
            }
            else {
                Console.WriteLine("There are not 4 angles in the cupboard, there must have been an issue with addAngles function.");
            }
            return quantities;
        }

        /*****************************************************************************
         * Pre : - Receives the client's first and last name as parameters           *
         *       - Database server is on                                             *
         * Post : - Returns the database's client id                                 *
         *        - If the client does not exist in the database, returns an empty   *
         *           string                                                          *
         *****************************************************************************/
        public string FindClient (string clientFirstName, string clientLastName) {
            string idClient = "";
            Connect();
            command.CommandText = String.Format("SELECT * FROM client WHERE lastname='{0}'", clientLastName);
            try {
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    if (reader["firstname"].ToString() == clientFirstName) {
                        idClient = reader["idclient"].ToString();
                        Console.WriteLine("Client found ! :)");
                    }
                }
            }
            catch (MySqlException) {
                idClient = "";
                Console.WriteLine("Fail in sql command (trying to find a client. Perhaps the client did not exist");
            }
            connection.Close();
            return idClient;
        }

        /*****************************************************************************
         * Pre : - Receives order's infos as parameters                              *
         *       - Database server is on                                             *
         * Post : - Calls make order for the cupboard                                *
         *        - Creates the order in the database                                *
         *        - Creates the client if he does not exist in the database yet      *
         *****************************************************************************/
        public void ConfirmOrder (string clientFirstName, string clientLastName, string adress, string zip, string phoneNumber, Cupboard cupboard) {
            /*First handles the order's identifiers (idcom and idclient)*/
            int idCom = 0;
            Connect();
            this.command.CommandText = "SELECT MAX(idcom) FROM client_partscommand";
            try {
                string stepIDCom = command.ExecuteScalar().ToString();
                Console.WriteLine("Command ID is :");
                Console.WriteLine(stepIDCom);
                /* Command ID*/
                idCom = Int32.Parse(stepIDCom) + 1;
            }
            /*Means that there is no command yet in database*/
            catch (FormatException ex) {
                idCom = 1;
                Console.WriteLine("no command in the database yet.");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            connection.Close();
            string idClient = FindClient(clientFirstName, clientLastName);
            /*If the client was not found in the database, that means he has to be created*/
            if (idClient =="") {
                Connect();
                command.CommandText = "SELECT MAX(idclient) FROM client";
                try {
                    idClient = ((int)command.ExecuteScalar() + 1).ToString();
                }
                catch(InvalidCastException) {
                    idClient = "1";
                    Console.WriteLine("No client in the database yet.");
                }
                connection.Close();
                Connect();
                command.CommandText = String.Format("INSERT INTO client(idclient, firstname, lastname, adress, zip, phonenumber) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", idClient, clientFirstName, clientLastName, adress, zip, phoneNumber);
                command.ExecuteNonQuery();
                connection.Close();
            }
            //Orders the parts
            Dictionary<string, int> parts = MakeOrder(cupboard);
            Connect();
            command.CommandText = String.Format("INSERT INTO client_command(idcom, idclient, description) VALUES ('{0}', '{1}', '{2}');", idCom, idClient, cupboard.GetDescription().ToString());
            foreach (string code in parts.Keys) {
                command.CommandText += String.Format("INSERT INTO client_partscommand VALUES ('{0}', '{1}', '{2}');", idCom, code, parts[code]);
                command.CommandText += String.Format("UPDATE part SET real_quantity= real_quantity-{0} WHERE code='{1}';", parts[code], code);
            }
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*****************************************************************************
         * Pre : recieves a part, its main dimension and color                       *
         * Post : returns the unique code of the wanted part                         *
         * Raise : raises an error if the part has is a door or a panel              *
         *****************************************************************************/

        private string SelectPart(Part piece, string dimension, string color, Dictionary<string, Object> description, MySqlDataReader reader)
        {
            int length = (int)piece.GetDescription()["length"];
            string code = "";
            if ((int)reader[dimension] == length)
            {
                Console.WriteLine("MATCH LENGHT!");
                if (color == "")
                {
                    try
                    {
                        description.Add("code", reader["code"].ToString());
                        Console.WriteLine("Piece found!");
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Already added");
                    }
                }
                else if (reader["color"].ToString() == color)
                {
                    try
                    {
                        description.Add("code", reader["code"].ToString());
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Already added");
                    }
                }
            }

            //if (code == "")
            //{
            //    Console.WriteLine("No fitting piece found.");
            //    Console.WriteLine(piece.GetDescription()["ref"].ToString());
            //  //Console.WriteLine(piece.GetDescription()["color"].ToString());
            //    Console.WriteLine(piece.GetDescription()["length"].ToString());
            //    Console.WriteLine(piece.GetDescription()["width"].ToString());
            //    return null;
            //}
            return code;
        }

        /*same as selectPiece(), but with 2 main dimensions*/
        private string SelectPart2D(Part piece, string dimension1, string dimension2, string color, Dictionary<string, Object> description, MySqlDataReader rdr)
        {
            string code = "";
            int length = (int)piece.GetDescription()["length"];
            int width = (int)piece.GetDescription()["width"];
            command.CommandText = String.Format("SELECT * FROM part WHERE ref='{0}'", piece.GetDescription()["ref"].ToString());
            if ((int)rdr[dimension1] == length)
            {
                if ((int)rdr[dimension2] == width)
                {
                    if (color == "") //piece has no color specified
                    {
                        try
                        {
                            description.Add("code", rdr["code"].ToString());
                            Console.WriteLine("Piece found!");
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Already added");
                        }
                    }
                    else if (rdr["color"].ToString() == color)
                    {
                        try
                        {
                            description.Add("code", rdr["code"].ToString());
                            Console.WriteLine("piece found!");
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Already added");
                        }
                    }
                }
            }
            //if (code == "")
            //{
            //    Console.WriteLine("No fitting piece found.");
            //    Console.WriteLine(piece.GetDescription()["ref"].ToString());
            //    Console.WriteLine(piece.GetDescription()["color"].ToString());
            //    Console.WriteLine(piece.GetDescription()["length"].ToString());
            //    Console.WriteLine(piece.GetDescription()["width"].ToString());
            //    return null;
            //}
            return code;
        }

        /*****************************************************************************
         * Pre : recieves a part as paramater                                        *
         * Post : - Returns the part's complete description.                         *
         *          Dictionnary contains the following keys : height, depth, width   *
         *          color, code, client price, supplier price  (there may be more    *
         *          than one) and n suppliers, the number of suppliers that offer    *
         *          the part. The dimension(s) are present -or not- following the    *
         *          part's type                                                      *
         * Uses the part characteristics to find the part in the DB                  *
         *****************************************************************************/
      

        public Dictionary<string, Object>[] getBoxDescription(Block bloc, Dictionary<string, int> numberOfPieces)
        {
            Dictionary<string, Object>[] boxDescription = new Dictionary<string, Object>[bloc.GetParts().Length];
            List<string> piecesRefs = new List<string>();
            for (int i = 0; i < boxDescription.Length; i++)
            {
                boxDescription[i] = new Dictionary<string, Object>();
            }
            Connect();
            string addRefs = "";
            foreach (Part piece in bloc.GetParts())
            {
                if (!(piecesRefs.Contains(piece.GetDescription()["ref"].ToString())))
                {
                    piecesRefs.Add(piece.GetDescription()["ref"].ToString());
                    addRefs += String.Format("'{0}', ", piece.GetDescription()["ref"]);
                }

            }
            addRefs = addRefs.Substring(0, addRefs.Length - 2); //remove blank space and coma at the end of addRefs
            this.command.CommandText += String.Format("SELECT * FROM part INNER JOIN prices ON part.code=prices.code WHERE part.ref IN ({0});", addRefs);
            Console.WriteLine(command.CommandText);
            reader = command.ExecuteReader();
            int countSup = 0;
            int countPiece = 0;
            string previousCode = "";
            while (reader.Read())
            {
                string reference = reader["ref"].ToString();
                Part p = new Cleat((float)0.5, -1); //little hack to have an assigned variable p 
                foreach (Part piece in bloc.GetParts())
                {
                    if (piece.GetDescription()["ref"].ToString() == reference)
                    {
                        p = piece;
                    }
                }
                //if ((int)p.GetDescription()["length"]==-1)
                //{
                //    p = null;
                //}
                string code = "";
                try
                {
                    //Console.WriteLine(String.Format("Dim 1 : {0} \n Dim 2 : {1} \n Color : {2} \n count piece : {3}", p.GetDescription()["dim1"].ToString(), p.GetDescription()["dim2"].ToString(), p.GetDescription()["color"].ToString(), boxDescription[countPiece]));
                    code = SelectPart2D(p, p.GetDescription()["dim1"].ToString(), p.GetDescription()["dim2"].ToString(), p.GetDescription()["color"].ToString(), boxDescription[countPiece], reader);
                }
                catch (KeyNotFoundException)
                {
                    try
                    {
                        //Console.WriteLine(String.Format("Dim : {0} \n Color : {1} \n count piece : {2}", p.GetDescription()["dim"].ToString(), p.GetDescription()["color"].ToString(), boxDescription[countPiece]));
                        code = SelectPart(p, p.GetDescription()["dim"].ToString(), p.GetDescription()["color"].ToString(), boxDescription[countPiece], reader);
                    }
                    catch (KeyNotFoundException)
                    {
                        //Console.WriteLine(String.Format("Dim : {0} \n count piece : {1}", p.GetDescription()["dim"].ToString(), boxDescription[countPiece]));
                        code = SelectPart(p, p.GetDescription()["dim"].ToString(), "", boxDescription[countPiece], reader);
                    }
                }
                Console.WriteLine(code);
                int npieces = 0;
                try
                {
                    npieces = numberOfPieces[reference];
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("\n FAIL ! \n");
                    Console.WriteLine(reference);
                    npieces = 2;
                }
                if (code != "")
                {
                    if (code != previousCode)
                    {
                        int cp = 0;
                        while (cp < npieces)
                        {
                            if (countSup == 0)
                            {
                                boxDescription[countPiece].Add("client price", (float)reader["client_price"]); //code is a unique identifier
                                boxDescription[countPiece].Add("n suppliers", 0);
                            }
                            boxDescription[countPiece].Add("ref", reference);
                            boxDescription[countPiece].Add(String.Format("supplier price {0}", countSup), (float)reader["supplier_price"]);
                            countSup++;
                            boxDescription[countPiece]["n suppliers"] = countSup;
                            countPiece++;
                        }
                    }
                    previousCode = code;
                }
            }
            for (int i = 0; i < boxDescription.Length; i++)
            {
                try
                {
                    //Console.WriteLine(String.Format("Piece : {0}", boxDescription[i]["ref"]));
                    foreach (string carac in boxDescription[i].Keys)
                    {
                        Console.WriteLine(String.Format("Carac : {0}  ; Value : {1}", carac, boxDescription[i][carac]));
                    }
                    Console.WriteLine("\n");
                }
                catch (KeyNotFoundException)
                {
                    //Console.WriteLine("Key not found!");
                    //Console.WriteLine(boxDescription[i].Count);
                }
            }
            return boxDescription;
        }

        /************************************************************************
         * Pre : recieve the dimension (string) to list and the part (string)   *
         *       that sets it                                                   *
         * Post : returns available heights for boxes                           *
         * Raise : uncorrect dimension name will raise an error                 *
         *  Function will not raise any error if determiningPart is not correct *
         *  but a message will appear in the console                            *
         ************************************************************************/
        public List<int> ExistingDimension(string dim, string determiningPart)
         {
            List<int> dimensions = new List<int>();
            List<int> references = new List<int>();
            Connect();
            command.CommandText = String.Format("SELECT * FROM part WHERE ref='{0}'", determiningPart);
            reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read()){
                if (!(references.Contains((int)reader[dim]))){
                    try{
                        if ((dim == "height") && (determiningPart != "Cornieres")) {
                            dimensions.Add((int)reader[dim] + 4);
                        }
                        else {
                            dimensions.Add((int)reader[dim]);
                        }
                    }
                    catch (KeyNotFoundException) {
                        Console.WriteLine("Uncorrect specified dimension");
                    }
                    i++;
                }
                references.Add((int)reader[dim]);
            }
            connection.Close();
            return dimensions; 
         }

        /************************************************************************
         * Pre : recieve the part as a parameter                                *
         * Post : returns all available colors for that part                    *
         * Raise :                                                              *
         *  Function should be used once for each type of part ; once for panel *
         *  color, once for door color                                          *
         ************************************************************************/
        public List<string> GetExistingcolors (Part part) {
            Connect();
            string reference = part.GetDescription()["ref"].ToString();
            command.CommandText = String.Format("SELECT * FROM part WHERE ref='{0}'", reference); 
            reader = command.ExecuteReader();
            int counter = 0;
            while (reader.Read()) {
                counter++;
            }
            connection.Close();
            if (counter == 0) {
                Console.WriteLine(String.Format("No part of that name found. Is this a correct name ? {0}", reference));
            }
            List<string> colors = new List<string>();
            Connect();
            command.CommandText = String.Format("SELECT * FROM part WHERE ref='{0}'", reference);
            reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read()) {
                if (!(colors.Contains(reader["color"].ToString()))) {
                    colors.Add(reader["color"].ToString()); //each color can appear only once in the list
                } 
            }
            connection.Close();
            return colors;
        }
        /**/
        public int sizeOfTable(string table)
        {
            Connect();
            command.CommandText = String.Format("SELECT * FROM {0}", table);
            reader = command.ExecuteReader();
            int counter = 0;
            while (reader.Read())
            {
                counter++;
            }
            connection.Close();
            return counter;
        }
    }
}