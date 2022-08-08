﻿using System;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace CSharpAcademy
{


    public class Program
    {
        static string connectionString = @"Data Source=habit-Tracker.db";


        static void Main(string[] args)
        { 
           

            using (var connection = new SqliteConnection(connectionString))
            { 
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                                @"CREATE TABLE IF NOT EXISTS yourHabit (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Date TEXT,
                                Quantity INTEGER
                                )";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            GetUserInput();


            static void GetUserInput()
            {
                Console.Clear();
                bool closeApp = false;
                while (closeApp == false)
                {
                    Console.WriteLine("\n\nMAIN MENU");
                    Console.WriteLine("\nWhat would you like to do?");
                    Console.WriteLine("\nType 0 to Close Application");
                    Console.WriteLine("Type 1 to View All Records");
                    Console.WriteLine("Type 2 to Insert Record");
                    Console.WriteLine("Type 3 to Delete Record");
                    Console.WriteLine("Type 4 to Update Record");
                    Console.WriteLine("-----------------------------------------\n");

                    string command = Console.ReadLine();

                    switch (command)
                    {
                        case "0":
                            Console.WriteLine("\nGoodbye!\n");
                            closeApp = true;
                            break;
                        case "1":
                            GetAllRecords();
                            break;
                        case "2":
                            Insert();
                            break;
                        //case 3:
                        //    Delete();
                        //    break;
                        //case 4:
                        //    Update();
                        //    break;
                        //default:
                        //    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        //    break;
                    }


                }
            }


            static void GetAllRecords()
            {
                Console.Clear();

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    var tableCmd = connection.CreateCommand();

                    tableCmd.CommandText =
                                    $"SELECT * FROM drinking_water ";

                    List<DrinkingWater> tableDate = new();

                    SqliteDataReader reader = tableCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tableData.Add(
                            new DrinkingWater
                            {
                                Id = reader.GetInt32(0),
                                Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-US")),
                                Quantity = reader.GetInt32(2)
                            });
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found");
                    }

                    connection.Close();

                    Console.WriteLine("------------------------------------\n");
                    foreach (var dw in tableData)
                    {
                        Console.WriteLine($"{dw.Id} - {dw.Date.ToString("dd-MM-yyyy")} - Quantity: {dw.Quantity}");
                    }

                    Console.WriteLine("------------------------------------\n");
                }

            }

        

            static void Insert()
            {
                string date = GetDateInput();

                int quantity = GetNumberInput("\n\nPlease insert number of glasses or other measure of your choice (no decimals allowed)\n\n");

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    var tableCmd = connection.CreateCommand();

                    tableCmd.CommandText =
                                    $"INSERT INTO drinking_water(date, quantity) VALUES('{date}', {quantity})";


                    tableCmd.ExecuteNonQuery();

                    connection.Close();
                }

            }

            static string GetDateInput()
            {
                Console.WriteLine("\n\nPlease insert the date: (Format: dd-mm-yy). Type 0 to return to main menu.\n\n");

                string dateInput = Console.ReadLine();

                if (dateInput == "0") GetUserInput();

                return dateInput;
            }

            static int GetNumberInput(string message)
            {
                Console.WriteLine(message);

                string numberInput = Console.ReadLine();

                if (numberInput == "0") GetUserInput();

                int finalInput = Convert.ToInt32(numberInput);

                return finalInput;
            }
        }
    }

    public class DrinkingWater
    {
        public int Id { get; set; }
        public DateTime Date { get; set;}
        public int Quantity { get; set; }
        
    }
}