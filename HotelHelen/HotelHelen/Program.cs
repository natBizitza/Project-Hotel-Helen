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
            const int REGISTER = 1, EDIT = 2, CHECKIN = 3, CHECKOUT = 4, EXIT = 5;
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
                        case REGISTER:
                            RegisterClient();
                            break;
                        case EDIT:
                            //Console.WriteLine("Introduce client´s DNI, please.");
                            //string DNI = Console.ReadLine();
                            EditClient();
                            break;
                        case CHECKIN:
                            CheckIn(DateTime);
                            break;
                        case CHECKOUT:
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

        public static void EditClient()
        {
            string DNI, nameClient, surnameClient, answer;
            conexion.Open();

            Console.WriteLine("Introduce client´s DNI, please.");
            DNI = Console.ReadLine();
            cadena = "SELECT *from CLIENTE where DNI LIKE '" + DNI + "'";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();

            if (registros.Read())
            {
                do
                {
                    Console.WriteLine("Existe el registro");
                    answer = Console.ReadLine();
                } while (answer.ToLower() != "n" || answer.ToLower() != "s");


                if (answer.ToLower() == "n")
                {
                    Console.WriteLine("Introduce correct name.");
                    string correctName = Console.ReadLine();

                    conexion.Open();

                    cadena = "UPDATE CLIENTE SET NOMBRE WHERE DNI LIKE  '" + DNI + "','" + correctName + "'";
                    comando = new SqlCommand(cadena, conexion);
                    comando.ExecuteNonQuery();

                }
                else
                {
                        Console.WriteLine("Introduce correct surname.");
                        string correctSurname = Console.ReadLine();

                        conexion.Open();

                        cadena = "UPDATE CLIENTE SET NOMBRE WHERE DNI LIKE  '" + DNI + "','" + correctSurname + "'";
                        comando = new SqlCommand(cadena, conexion);
                        comando.ExecuteNonQuery();
                }

               
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
