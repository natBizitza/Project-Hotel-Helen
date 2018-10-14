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

            menu();
           
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
                            EditClient();
                            break;
                        case CHECKIN:
                            CheckIn();
                            break;
                        case CHECKOUT:
                            CheckOut();
                            break;
                    }

            } while (num != 5);
            Console.WriteLine("You are out of MENU. See you next time.");
            Console.ReadLine();
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

            cadena = "INSERT INTO CLIENT VALUES ('" + DNI +"','" + nameClient +  "','" + surnameClient  +"')";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();

            conexion.Close();

        }

        public static void EditClient()
        {
            string DNI, nameClient, surnameClient, answer;
         
            bool IsRegistered=false;

            do
            {
                Console.WriteLine("Introduce client´s DNI, please.");
                DNI = Console.ReadLine();
                conexion.Open();
                cadena = "SELECT * from CLIENT where DNI LIKE '" + DNI + "'";
                comando = new SqlCommand(cadena, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                IsRegistered = registros.Read();
                //if (registros.Read())
                //{
                //    IsRegistered = true;
                //}
                conexion.Close();
                registros.Close();
            } while (!IsRegistered);

            //to check if it´s our client or not
            do
            {
                Console.WriteLine("What data would you like to edit name or surname (N/S)?");
                answer = Console.ReadLine();
            } while (answer.ToLower()!= "n" && answer.ToLower() != "s");

            // if yes, what we want to change 
            if (answer.ToLower() == "n")
            {
                Console.WriteLine("Introduce correct name.");
                string correctName = Console.ReadLine();

                conexion.Open();

                cadena = "UPDATE CLIENT SET Nombre='" + correctName + "' WHERE DNI LIKE  '" + DNI + "'";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            else
            {
                Console.WriteLine("Introduce correct surname.");
                string correctSurname = Console.ReadLine();

                conexion.Open();

                cadena = "UPDATE CLIENT SET Nombre='" + correctSurname + "' WHERE DNI LIKE  '" + DNI + "'";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
            }

            Console.ReadLine();
            conexion.Close();
        }

        public static void CheckIn()
        {

            string DNI;
            int roomNum, codeOfReservation;
            bool IsRegistered;
            //add hours and mins
            DateTime thisDay = DateTime.Today;
            SqlDataReader registros;
            do
            {
                Console.WriteLine("Introduce client´s DNI, please.");
                DNI = Console.ReadLine();
                conexion.Open();
                cadena = "SELECT * from CLIENT where DNI LIKE '" + DNI + "'";
                comando = new SqlCommand(cadena, conexion);
                 registros = comando.ExecuteReader();
                IsRegistered = registros.Read();
                conexion.Close();
            } while (!IsRegistered);

            Console.WriteLine("Please, choose a number of a room.");

            conexion.Open();

            cadena = "SELECT CodHabitacion FROM HABITACION WHERE Ocupacion LIKE 'L'";
            comando = new SqlCommand(cadena, conexion);
            registros = comando.ExecuteReader();

            // to show the available rooms
            while (registros.Read())
            {
                Console.WriteLine(registros["CodHabitacion"].ToString());
                Console.WriteLine();
            }
            //Console.ReadLine();
            conexion.Close();

            // Choose a room only from the list ADD HERE
            Console.WriteLine("Please, choose a room.");
            roomNum = Convert.ToInt32(Console.ReadLine());
            // roomNum = codeOfReservation
            codeOfReservation = roomNum;

            conexion.Open();

            cadena = "UPDATE HABITACION SET Ocupacion = 'O' where CodHabitacion= '" + roomNum+"'";
            //cadena = "UPDATE RESERVA SET DNI LIKE '" + DNI + "'";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();

            conexion.Close();

            conexion.Open();
            cadena = "INSERT INTO RESERVA (CodReserva, DNI, CodHabitacion, FechaIn) VALUES ('"+ codeOfReservation + "','" + DNI + "','" + roomNum + "','" + thisDay + "')";
            //cadena = "UPDATE CLIENT SET Nombre='" + correctName + "' WHERE DNI LIKE  '" + DNI + "'";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();

            conexion.Close();

        }
        public static void CheckOut()
        {
            DateTime thisDay = DateTime.Today;
            SqlDataReader registros;
            int roomNum;

            Console.WriteLine("Introduce client´s DNI, please.");
            string  DNI = Console.ReadLine();
            conexion.Open();
            cadena = "SELECT *from CLIENT where DNI LIKE '" + DNI + "'";
            comando = new SqlCommand(cadena, conexion);
            registros = comando.ExecuteReader();
            conexion.Close();

            //to know the room number of this client
            conexion.Open();
            cadena = "SELECT CodHabitacion from RESERVA where DNI LIKE '" + DNI + "'";
            comando = new SqlCommand(cadena, conexion);
            registros = comando.ExecuteReader();
            registros.Read();
            Console.WriteLine(registros["CodHabitacion"]);
            Console.WriteLine();
            conexion.Close();

            Console.WriteLine("Please, confirm the room number(Type the number)");
            roomNum =Convert.ToInt32(Console.ReadLine());


            //int roomNum = Convert.ToInt32(Console.ReadLine());

            conexion.Open();
            // CHANGE. WE need to get the Codhabitacion here too in order to change the status of the room on L
            cadena = "UPDATE HABITACION SET Ocupacion = 'L' where CodHabitacion= '" + roomNum+ "'";
            //cadena = "UPDATE RESERVA SET FechaOut LIKE  '" + thisDay + "' WHERE DNI LIKE  '" + DNI + "'";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();

            conexion.Close();

            conexion.Open();
            cadena = "UPDATE RESERVA  SET FechaOut='" + thisDay + "' where DNI ='" + DNI + "'";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();

            Console.WriteLine("The client is successfully checked out.");
            Console.ReadLine();
        }
    }
}
