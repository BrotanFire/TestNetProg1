using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TestNetProg1
{
    public partial class MainForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-H9FO4E8;Initial Catalog = master; Integrated Security = True";
        long TimeToMarathon;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            TimeToMarathon = MaraphoneGetTimeToNearestMarathon();
            SetTimeToTimer();
            MaraphoneTimer.Start();
        }
        private void SetTimeToTimer()
        {
            //Пишем время до марафона
            labelwithtimetomarathone.Text = Math.Truncate((TimeToMarathon * 0.00069)) + " дней " + Math.Truncate((TimeToMarathon * 0.00069 - Math.Truncate((TimeToMarathon * 0.00069))) * 24) + " часов и " + Math.Truncate((((TimeToMarathon * 0.00069 - Math.Truncate((TimeToMarathon * 0.00069))) * 24) - Math.Truncate((TimeToMarathon * 0.00069 - Math.Truncate((TimeToMarathon * 0.00069))) * 24)) * 60) + " минут до марафона";
        }
        // каждую минуту уменьшаем время до марафона на 1 и заного пишем время
        private void MaraphoneTimer_Tick(object sender, EventArgs e)
        {
            MaraphoneTimer.Stop();
            TimeToMarathon = TimeToMarathon - 1;
            SetTimeToTimer();
            MaraphoneTimer.Start();
        }
        //Запрашиваем время до ближайшего марафона(в минутах)
        private int MaraphoneGetTimeToNearestMarathon()
        {
            // пишем название процедуры
            string sqlExpression = "[dbo].[GetTimeToNearestMarathon]";
            //создаем соединение с базой
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // открываем коннект
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var result = command.ExecuteScalar();
                connection.Close();
                return (int)result;
            }
        }

    }
}
