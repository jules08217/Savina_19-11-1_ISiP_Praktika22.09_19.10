using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public int Voz()
        {
            int[] cc = new int[4];
            int c1 = 0;
            int voz;
            string s = DATAR.Text;
            char[] ss = s.ToCharArray();
            cc[0] = ss[6] - '0';
            cc[1] = ss[7] - '0';
            cc[2] = ss[8] - '0';
            cc[3] = ss[9] - '0';
            for (int i = 0; i < 4; i++)
            {
                c1 = c1 * 10 + cc[i];
            }
            voz = 2020 - c1;
            return voz;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Добавить Студента
            string FIO = FIo.Text;
            string Groupa = GROUP.Text;
            int v = Voz();
            string DataR = v.ToString();
            string ssql = $"INSERT INTO Students (FIO,Groupa,DataR) VALUES ('{FIO}','{Groupa}','{DataR}')"; //ЗАпрос вставить данные в таблицу Table_1 - имя таблици
            string connectionString = @"Data Source=DESKTOP-CDQ6MB6\SQLEXPRESS;Initial Catalog=gnevnik;Integrated Security=True"; //prak - имя базы данных(Поменять на свою)
            SqlConnection conn = new SqlConnection(connectionString); // Подключение к БД
            conn.Open();// Открытие Соединения

            SqlCommand command = new SqlCommand(ssql, conn);// Объект вывода запросов
            MessageBox.Show("Данные добавлены");
            FIo.Clear();
            GROUP.Clear();
            DATAR.Clear();
            SqlDataReader reader = command.ExecuteReader(); // Выаолнение запроса вывод информации
            while (reader.Read()) //В цикле вывести всю информацию из таблици
            {
                InfoS.Text += reader[0] + " - Id студента " + " ФИО: " + reader[1].ToString() + "  Группа: " + reader[2].ToString() + " Возраст: " + reader[3].ToString() + "\n";
            }
            reader.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Вывод данных
            InfoS.Clear();
            InfoM.Clear();
            string ssql = $"SELECT  * FROM Students "; //ЗАпрос  
            string ssql2 = $"SELECT  * FROM Marks "; 
            string connectionString = @"Data Source=DESKTOP-CDQ6MB6\SQLEXPRESS;Initial Catalog=gnevnik;Integrated Security=True"; 
            SqlConnection conn = new SqlConnection(connectionString); // Подключение к БД
            conn.Open();// Открытие Соединения
            SqlCommand command = new SqlCommand(ssql, conn);// Объект вывода запросов
            SqlDataReader reader = command.ExecuteReader(); // Выаолнение запроса вывод информации
            while (reader.Read()) //В цикле вывести всю информацию из таблици
            {
                InfoS.Text += reader[0] + " - Id студента " + "  ФИО: " + reader[1].ToString() + "  Группа: " + reader[2].ToString() + " Возраст: " + reader[3].ToString() + "\n";
            }
            reader.Close();
            SqlCommand command2 = new SqlCommand(ssql2, conn);
            SqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                InfoM.Text += reader2[0] + " - Id студента " + " Дата: " + reader2[1].ToString() + "  Тема: " + reader2[2].ToString() + " Оценка: " + reader2[3].ToString() + "\n";
            }
            reader2.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Добавить оценку
            string Id = ID.Text;
            string Datac = DataC.Text;
            string Tema = TEMA.Text;
            string Mark = MARK.Text;
            string ssql = $"INSERT INTO Marks (Id,Datac,Tema,Mark) VALUES ('{Id}','{Datac}','{Tema}','{Mark}')"; //ЗАпрос вставить данные в таблицу Table_1 - имя таблици
            string connectionString = @"Data Source=DESKTOP-CDQ6MB6\SQLEXPRESS;Initial Catalog=gnevnik;Integrated Security=True"; //prak - имя базы данных(Поменять на свою)
            SqlConnection conn = new SqlConnection(connectionString); // Подключение к БД
            conn.Open();// Открытие Соединения
            SqlCommand command = new SqlCommand(ssql, conn);// Объект вывода запросов
            MessageBox.Show("Данные добавлены");
            ID.Clear();
            DataC.Clear();
            TEMA.Clear();
            MARK.Clear();
            SqlDataReader reader = command.ExecuteReader(); // Выаолнение запроса вывод информации
            while (reader.Read()) //В цикле вывести всю информацию из таблици
            {
                InfoM.Text += reader[0] + " - Id студента " + "Дата: " + reader[1].ToString() + "  Тема: " + reader[2].ToString() + " Оценка: " + reader[3].ToString() + "\n";
            }
            reader.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Удалить по ID
            string ssql = @"DELETE FROM Students WHERE Id = @Id";
            string ssql2 = @"DELETE FROM Marks WHERE Id = @Id";
            string connectionString = @"Data Source=DESKTOP-CDQ6MB6\SQLEXPRESS;Initial Catalog=gnevnik;Integrated Security=True"; //prak - имя базы данных(Поменять на свою)
            SqlConnection conn = new SqlConnection(connectionString); // Подключение к БД
            SqlCommand command = new SqlCommand(ssql, conn);// Объект вывода запросов
            SqlCommand command2 = new SqlCommand(ssql2, conn);// Объект вывода запросов
            command.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(ID1.Text);
            command2.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(ID1.Text);
            conn.Open();// Открытие Соединения
            command.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            MessageBox.Show("Данные удалены");
            ID1.Clear();
            InfoM.Clear();
            InfoS.Clear();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //Изменить ФИО по ID
            string ssql = @"UPDATE Students SET [FIO] = @FIO WHERE Id = @Id";
            string connectionString = @"Data Source=DESKTOP-CDQ6MB6\SQLEXPRESS;Initial Catalog=gnevnik;Integrated Security=True"; //prak - имя базы данных(Поменять на свою)
            SqlConnection conn = new SqlConnection(connectionString); // Подключение к БД
            conn.Open();
            SqlCommand command = new SqlCommand(ssql, conn);// Объект вывода запросов
            command.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(ID1.Text);
            command.Parameters.Add("@FIO", SqlDbType.NVarChar).Value = FIo.Text;
            SqlDataReader reader = command.ExecuteReader(); // Выаолнение запроса вывод информации
            while (reader.Read()) //В цикле вывести всю информацию из таблици
            {
                InfoS.Text += reader[0] + " - Id студента " + " ФИО: " + reader[1].ToString() + "  Группа: " + reader[2].ToString() + " Возраст: " + reader[3].ToString() + "\n";
            }
            reader.Close();
            MessageBox.Show("ФИО изменены");
            FIo.Clear();
            ID1.Clear();
            InfoM.Clear();
            InfoS.Clear();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //Изменить группу по ID
            string ssql = @"UPDATE Students SET [Groupa] = @GRoupa WHERE Id = @Id";
            string connectionString = @"Data Source=DESKTOP-CDQ6MB6\SQLEXPRESS;Initial Catalog=gnevnik;Integrated Security=True"; //prak - имя базы данных(Поменять на свою)
            SqlConnection conn = new SqlConnection(connectionString); // Подключение к БД
            conn.Open();
            SqlCommand command = new SqlCommand(ssql, conn);// Объект вывода запросов
            command.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(ID1.Text);
            command.Parameters.Add("@Groupa", SqlDbType.NVarChar).Value = GROUP.Text;
            SqlDataReader reader = command.ExecuteReader(); // Выаолнение запроса вывод информации
            while (reader.Read()) //В цикле вывести всю информацию из таблици
            {
                InfoS.Text += reader[0] + " - Id студента " + " ФИО: " + reader[1].ToString() + "  Группа: " + reader[2].ToString() + " Возраст: " + reader[3].ToString() + "\n";
            }
            reader.Close();
            MessageBox.Show("Группа изменена");
            GROUP.Clear();
            ID1.Clear();
            InfoM.Clear();
            InfoS.Clear();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //Изменить дату рождения по ID
            string ssql = @"UPDATE Students SET [DataR] = @DataR WHERE Id = @Id";
            string connectionString = @"Data Source=DESKTOP-CDQ6MB6\SQLEXPRESS;Initial Catalog=gnevnik;Integrated Security=True"; //prak - имя базы данных(Поменять на свою)
            SqlConnection conn = new SqlConnection(connectionString); // Подключение к БД
            conn.Open();
            SqlCommand command = new SqlCommand(ssql, conn);// Объект вывода запросов
            command.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(ID1.Text);
            int v = Voz();
            string Dt = v.ToString();
            command.Parameters.Add("@DataR", SqlDbType.NVarChar).Value = Dt;
            SqlDataReader reader = command.ExecuteReader(); // Выаолнение запроса вывод информации
            while (reader.Read()) //В цикле вывести всю информацию из таблици
            {
                InfoS.Text += reader[0] + " - Id студента " + " ФИО: " + reader[1].ToString() + "  Группа: " + reader[2].ToString() + " Возраст: " + reader[3].ToString() + "\n";
            }
            reader.Close();
            MessageBox.Show("Дата рождения изменена");
            DATAR.Clear();
            ID1.Clear();
            InfoM.Clear();
            InfoS.Clear();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            //Изменить дату оценки по ID
            string ssql = @"UPDATE Marks SET [Datac] = @Datac WHERE Id = @Id";
            string connectionString = @"Data Source=DESKTOP-CDQ6MB6\SQLEXPRESS;Initial Catalog=gnevnik;Integrated Security=True"; //prak - имя базы данных(Поменять на свою)
            SqlConnection conn = new SqlConnection(connectionString); // Подключение к БД
            conn.Open();
            SqlCommand command = new SqlCommand(ssql, conn);// Объект вывода запросов
            command.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(ID1.Text);
            command.Parameters.Add("@Datac", SqlDbType.NVarChar).Value = DataC.Text;
            SqlDataReader reader = command.ExecuteReader(); // Выаолнение запроса вывод информации
            while (reader.Read()) //В цикле вывести всю информацию из таблици
            {
                InfoM.Text += reader[0] + " - Id студента " + "Дата: " + reader[1].ToString() + "  Тема: " + reader[2].ToString() + " Оценка: " + reader[3].ToString() + "\n";
            }
            reader.Close();
            MessageBox.Show("Дата оценки изменена");
            DataC.Clear();
            ID1.Clear();
            InfoM.Clear();
            InfoS.Clear();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            //Изменить тему по ID
            string ssql = @"UPDATE Marks SET [Tema] = @Tema WHERE Id = @Id";
            string connectionString = @"Data Source=DESKTOP-CDQ6MB6\SQLEXPRESS;Initial Catalog=gnevnik;Integrated Security=True"; //prak - имя базы данных(Поменять на свою)
            SqlConnection conn = new SqlConnection(connectionString); // Подключение к БД
            conn.Open();
            SqlCommand command = new SqlCommand(ssql, conn);// Объект вывода запросов
            command.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(ID1.Text);
            command.Parameters.Add("@Tema", SqlDbType.NVarChar).Value = TEMA.Text;
            SqlDataReader reader = command.ExecuteReader(); // Выаолнение запроса вывод информации
            while (reader.Read()) //В цикле вывести всю информацию из таблици
            {
                InfoM.Text += reader[0] + " - Id студента " + "Дата: " + reader[1].ToString() + "  Тема: " + reader[2].ToString() + " Оценка: " + reader[3].ToString() + "\n";
            }
            reader.Close();
            MessageBox.Show("Тема изменена");
            TEMA.Clear();
            ID1.Clear();
            InfoM.Clear();
            InfoS.Clear();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            //Изменить оценку по ID
            string ssql = @"UPDATE Marks SET [Mark] = @Mark WHERE Id = @Id";
            string connectionString = @"Data Source=DESKTOP-CDQ6MB6\SQLEXPRESS;Initial Catalog=gnevnik;Integrated Security=True"; //prak - имя базы данных(Поменять на свою)
            SqlConnection conn = new SqlConnection(connectionString); // Подключение к БД
            conn.Open();
            SqlCommand command = new SqlCommand(ssql, conn);// Объект вывода запросов
            command.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(ID1.Text);
            command.Parameters.Add("@MArk", SqlDbType.NVarChar).Value = MARK.Text;
            SqlDataReader reader = command.ExecuteReader(); // Выаолнение запроса вывод информации
            while (reader.Read()) //В цикле вывести всю информацию из таблици
            {
                InfoM.Text += reader[0] + " - Id студента " + "Дата: " + reader[1].ToString() + "  Тема: " + reader[2].ToString() + " Оценка: " + reader[3].ToString() + "\n";
            }
            reader.Close();
            MessageBox.Show("Оценка изменена");
            MARK.Clear();
            ID1.Clear();
            InfoM.Clear();
            InfoS.Clear();
        }
    }
}