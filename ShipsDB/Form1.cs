using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using Npgsql;
using System.Text.RegularExpressions;

namespace ShipsDB
{   
    public partial class Form1 : Form
    {
        private const int CLASSES = 0;
        private const int SHIPS = 1;
        private const int BATTLES = 2;
        private const int OUTCOMES = 3;
        private const String CLASSES_STRING = "classes";
        private const String SHIPS_STRING = "ships";
        private const String BATTLES_STRING = "battles";
        private const String OUTCOMES_STRING = "outcomes";

        String user;

        public String User
        {
            get { return user; }
            set { user = value; }
        }
        String password;

        public String Password
        {
            get { return password; }
            set { password = value; }
        }
        public String host { get; set; }

        public Form1()
        {
            
            InitializeComponent();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            ConnectForm connection = new ConnectForm(this);
            connection.Show();

        }

        private void insert(String[] fields, int table)
        {

            string queryString = "";
            switch (table)
            {
                case(CLASSES):
                    queryString = "INSERT INTO classes(class, type, country , numGuns, bore, displacement) Values" + formatVals(fields);
                    break;
                case (SHIPS):
                    queryString = "INSERT INTO ships(name, class, launched) Values" + formatVals(fields);
                    break;
                case (BATTLES):
                    queryString = "INSERT INTO battles(name, date) Values" + formatVals(fields);
                    break;
                case (OUTCOMES):
                    queryString = "INSERT INTO outcomes(ship, battle, result) Values" + formatVals(fields);
                    break;
            }
            NpgsqlCommand command = new NpgsqlCommand(queryString);

            using (NpgsqlConnection connection = Config.GetConn(user, password, host))
            {
                try
                {
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    classOutBox.Text = e.ToString();
                }

                // The connection is automatically closed at  
                // the end of the Using block.
            }
        }
        private void search(String table, String attribute, String param)
        {

            int val;
            bool isNumeric = int.TryParse(param, out val);
            string queryString = "";
            if (isNumeric)
                queryString = "SELECT * FROM " + table + " WHERE " + attribute + " = " + param;
            else
                queryString = "SELECT * FROM " + table + " WHERE " + attribute + " = '" + param +"'";

            NpgsqlCommand command = new NpgsqlCommand(queryString);

            using (NpgsqlConnection connection = Config.GetConn(user, password, host))
            {
                try
                {
                    //A dataset contains data in tabular form
                    DataSet ds = new DataSet();
                    command.Connection = connection;
                    connection.Open();
                    //The DataAdapter serves as a bridge between a DataSet and a data source for retrieving and saving data.
                    //Fill changes the data in the DataSet to match the data in the data source
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    adapter.Fill(ds);
                    //Display the contents on dataset in datagrid
                    classesDataGrid.DataSource = ds.Tables[0];
                }
                catch (Exception e)
                {
                    classOutBox.Text = e.ToString();
                }

                // The connection is automatically closed at  
                // the end of the Using block.
            }
        }
        private void showTable(String table)
        {
            String queryString = "SELECT * FROM " + table;
            NpgsqlCommand command = new NpgsqlCommand(queryString);

            using (NpgsqlConnection connection = Config.GetConn(user, password, host))
            {
                try
                {
                    //A dataset contains data in tabular form
                    DataSet ds = new DataSet();
                    command.Connection = connection;
                    connection.Open();
                    //The DataAdapter serves as a bridge between a DataSet and a data source for retrieving and saving data.
                    //Fill changes the data in the DataSet to match the data in the data source
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    adapter.Fill(ds);
                    //Display the contents on dataset in datagrid
                    classesDataGrid.DataSource = ds.Tables[0];
                }
                catch (Exception e)
                {
                    classOutBox.Text = e.ToString();
                }

                // The connection is automatically closed at  
                // the end of the Using block.
            }
        }
         private String formatVals(String[] fields)
        {
            int val;
            bool isNumeric;
            StringBuilder retval = new StringBuilder("(");
            for (int i = 0; i < fields.Length; i++)
            {
                isNumeric = int.TryParse(fields[i], out val);
                if (isNumeric)
                    retval.Append(val + ",");
                else
                    retval.Append("\'" + fields[i] + "\',");
            }
            retval.Remove(retval.Length - 1, 1); //remove final comma
            retval.Append(")");
                return retval.ToString();
        }
        //********************************INSERT BUTTONS***************************************
        private void insertClassesButton_Click(object sender, EventArgs e)
         {
            classOutBox.ResetText();
            insert(new String[] { classField1.Text, typeField1.Text, countryField1.Text, numGunsField1.Text, boreField1.Text, displacementField1.Text }, CLASSES);
            insert(new String[] { classField2.Text, typeField2.Text, countryField2.Text, numGunsField2.Text, boreField2.Text, displacementField2.Text }, CLASSES);
            showTable(CLASSES_STRING);
        }

        private void shipsInsertButton_Click(object sender, EventArgs e)
        {
            classOutBox.ResetText();
            insert(new String[] { shipsNameBox1.Text, shipsClassBox1.Text, shipsLaunchedBox1.Text }, SHIPS);
            insert(new String[] { shipsNameBox2.Text, shipsClassBox2.Text, shipsLaunchedBox2.Text }, SHIPS);
            showTable(SHIPS_STRING);
        }
        private void insertBattlesButton_Click(object sender, EventArgs e)
        {
            classOutBox.ResetText();
            insert(new String[] { battlesNameBox1.Text, battlesDateBox1.Text }, BATTLES);
            insert(new String[] { battlesNameBox2.Text, battlesDateBox2.Text }, BATTLES);
            showTable(BATTLES_STRING);
        }

        private void insertOutcomesButton_Click(object sender, EventArgs e)
        {
            classOutBox.ResetText();
            insert(new String[] { outcomesShipBox1.Text, outcomesBattleBox1.Text, outcomesResultBox1.Text }, OUTCOMES);
            insert(new String[] { outcomesShipBox2.Text, outcomesBattleBox2.Text, outcomesResultBox2.Text }, OUTCOMES);
            showTable(OUTCOMES_STRING);
        }
        //*******************************SEARCH BUTTONS***************************************
        private void countrySearchButton_Click(object sender, EventArgs e)
        {
            search("Classes", "country", countrySearchBox.Text);
        }

        private void classSearchButton_Click(object sender, EventArgs e)
        {
            search("Classes", "class", classSearchBox.Text);
        }

        private void typeSearchButton_Click(object sender, EventArgs e)
        {
            search("Classes", "type", typeSearchBox.Text);
        }

        private void numGunsSearchButton_Click(object sender, EventArgs e)
        {
            search("Classes", "numGuns", numGunsSearchBox.Text);
        }
        
        private void shipsNameSearchButton_Click(object sender, EventArgs e)
        {
            search(SHIPS_STRING, "name", shipsNameSearchBox.Text);
        }

        private void shipsClassSearchButton_Click(object sender, EventArgs e)
        {
            search(SHIPS_STRING, "class", shipsClassSearchBox.Text);
        }

        private void shipsLaunchedSearchButton_Click(object sender, EventArgs e)
        {
            search(SHIPS_STRING, "launched", shipsLaunchedSearchBox.Text);
        }

        private void searchBattlesNameButton_Click(object sender, EventArgs e)
        {
            search(BATTLES_STRING, "name", searchBattlesNameBox.Text);
        }

        private void searchBattleDatebutton_Click(object sender, EventArgs e)
        {
            search(BATTLES_STRING, "date", searchBattlesDateBox.Text);
        }

        private void searchOutcomesShiptButton_Click(object sender, EventArgs e)
        {
            search(OUTCOMES_STRING, "ship", searchOutcomesShiptBox.Text);
        }

        private void searchOutcomesBattleButton_Click(object sender, EventArgs e)
        {
            search(OUTCOMES_STRING, "battle", searchOutcomesBattleBox.Text);
        }

        private void searchResultBattleButton_Click(object sender, EventArgs e)
        {
            search(OUTCOMES_STRING, "result", searchOutcomesResultBox.Text);
        }

        private void showClassesButton_Click(object sender, EventArgs e)
        {
            showTable(CLASSES_STRING);
        }

        private void showShipsButton_Click(object sender, EventArgs e)
        {
            showTable(SHIPS_STRING);
        }

        private void showBattlesButton_Click(object sender, EventArgs e)
        {
            showTable(BATTLES_STRING);
        }

        private void showOutcomesButton_Click(object sender, EventArgs e)
        {
            showTable(OUTCOMES_STRING);
        }

        private void connectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            connectToolStripMenuItem_Click(sender, e);
        }

        private void boreSearchButton_Click(object sender, EventArgs e)
        {
            search("Classes", "bore", boreSearchBox.Text);
        }

        private void displacementSearchButton_Click(object sender, EventArgs e)
        {
            search("Classes", "displacement", displacementSearchBox.Text);
        }

    }
    /// <summary>
    /// Configuration file for the PostgreSQL database connection 
    /// </summary>
    static class Config
    {
        // Host name of Postgre, localhost if installed locally or some server name
        public static string Host = "131.252.208.122";
        // Port of Postgre server that we need to connect to
        public static string Port = "5432";
        // User of Postgre database that we need to connect
        public static string UserId = "";
        // Password for the Postgre user
        public static string Password = "";
        // Database of the Postgre that contains the neccessary tables for this application 
        public static string Database = "class7db";

        // This function will setup the connection to PostgreSQL database and return
        // the connection data. This connection data will be used to query the Postgres
        // database
        public static NpgsqlConnection GetConn(String user, String password, String host)
        {
            Password = password;
            UserId = user;
            Host = host;
            return new NpgsqlConnection("Server=" + Config.Host +
                                                        ";Port=" + Config.Port +
                                                        ";UserId=" + Config.UserId +
                                                        ";Password=" + Config.Password +
                                                        ";Database=" + Config.Database + ";");
        }
    }
}
