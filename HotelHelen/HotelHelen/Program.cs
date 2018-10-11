using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HotelHelen
{
    class Program
    {
        static String connectionString = ConfigurationManager.ConnectionStrings["conexionHOTELHELEN"].ConnectionString;
        static SqlConnection conexion = new SqlConnection(connectionString);
        static string cadena;
        static SqlCommand comando;

        static void Main(string[] args)
        {

           
           
        }
        public static void menu () {
            int num;
            string nameClient, surnameClient;
            do
            {
                Console.WriteLine("WELCOME to MENU.Please,choose one option.");
                Console.WriteLine("1 - To register a client.");
                Console.WriteLine("2 - To edit client´s information.");
                Console.WriteLine("3 - Check-in.");
                Console.WriteLine("4 - Check-out.");
                Console.WriteLine("5 - Exit.");

                num = Convert.ToInt32(Console.ReadLine());
                if (num == 1 || num == 2 || num == 3 || num == 4)
                    switch (num)
                    {
                        case 1:
                            RegisterClient();
                            break;
                        case 2:
                            Console.WriteLine("Introduce client´s DNI, please.");
                            string DNI = Console.ReadLine();
                            EditClient(DNI);
                            break;
                        case 3:
                            CheckIn(DateTime);
                            break;
                        case 4:
                            CheckOut(DateTime);
                            break;
                    }

            } while (num != 5);
            Console.WriteLine("You are out of MENU. See you next time.");
        }

        public static void RegisterClient() {

            string DNI, nameClient, surnameClient;

            Console.WriteLine("Welcome to Hotel Helen! Please, introduce your data. Enter your DNI.");
            DNI = Console.ReadLine();
            Console.WriteLine("Enter your Name.");
            nameClient = Console.ReadLine();
            Console.WriteLine("Enter yout surname.");
            surnameClient = Console.ReadLine();

            conexion.Open();

            cadena = "INSERT INTO CLIENTE VALUES ('" + DNI +"','" + nameClient +  "','" + surnameClient  +"')";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();

            conexion.Close();

        }

        public static void EditClient(string DNI)
        {
            string nameClient, surnameClient, answer;
            conexion.Open();

            //Console.WriteLine("Introduce client´s DNI, please.");
            //DNI = Console.ReadLine();
            cadena = "SELECT *from CLIENTE where DNI LIKE '" + DNI + "'";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();

            if (registros.Read())
            {
                Console.WriteLine("Existe el registro");

                answer = Console.ReadLine();
                if (answer.ToLower() == "n")
                {

                }

                conexion.Open();

                cadena = "UPDATE LIBRERIA SET EJEMPLARES = 15 WHERE TEMA LIKE'MECANICA'";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
            } 
            else
            {
                Console.WriteLine("El registro no existe");
            }

            Console.ReadLine();
            conexion.Close();

            Console.WriteLine("What data would you like to edit name or surname (N/S)?");

            answer = Console.ReadLine();
            if (answer.ToLower() == "n" )
            {
                
            }

            conexion.Open();

            cadena = "UPDATE LIBRERIA SET EJEMPLARES = 15 WHERE TEMA LIKE'MECANICA'";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
        }

    }
}
