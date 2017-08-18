using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIMS
{
    class Program
    {

        static List<Physician> physicians = new List<Physician>();

        static void Main(string[] args)
        {
            HospitalMenu();
        }

        private static void HospitalMenu()
        {
            Console.Clear();
            HospitalMenuHeader();
            Console.WriteLine(" 1. Add Physician records");
            Console.WriteLine(" 2. Delete Physician records");
            Console.WriteLine(" 3. Update Physician records");
            Console.WriteLine(" 4. View all Physician records");
            Console.WriteLine(" 5. Find a Physician by ID");
            Console.WriteLine(" 6. Clear Screen");
            Console.WriteLine(" 7. Exit");
            Console.WriteLine("");
            Console.Write(" Please input a number and press enter : ");

            var option = GetUserInput("[1234567]");

            switch (option)
            {
                case "1":
                    AddPhysicianRecord();
                    break;
                case "2":
                    DeletePhysicianRecordById();
                    break;
                case "3":
                    UpdatePhysicianRecordById();
                    break;
                case "4":
                    ViewAllPhysicianRecords();
                    break;
                case "5":
                    GetPhysicianRecordById();
                    break;
                case "6":
                    Console.Clear();
                    break;
                case "7":
                    Environment.Exit(0);
                    break;
            }
        }

        private static void AddPhysicianRecord()
        {
            try
            {
                Console.Clear();
                HospitalMenuHeader();
                Console.WriteLine("|              Add Physician Form                   |");
                Console.WriteLine("=====================================================");

                Physician physician = new Physician();
                physician.ContactInfo = new ContactInfo();
                physician.Specialization = new List<Specialization>();

                Specialization specialization = new Specialization();

                if (physicians.Count > 0)
                {
                    physician.Id = physicians.Last().Id + 1;
                }
                else
                {
                    physician.Id = 1;
                }

                Console.WriteLine("|              Personal Information                 |");
                Console.WriteLine("-----------------------------------------------------");
                physician.FirstName = HospitalFormField("First Name ", "", false);
                while (!allowLettersOnly(physician.FirstName)) physician.FirstName = HospitalFormField("First Name ", "", false);
                physician.MiddleName = HospitalFormField("Middle Name ", "", false);
                while (!allowLettersOnly(physician.MiddleName)) physician.MiddleName = HospitalFormField("Middle Name ", "", false);
                physician.LastName = HospitalFormField("Last Name ", "", false);
                while (!allowLettersOnly(physician.LastName)) physician.LastName = HospitalFormField("Last Name ", "", false);
                physician.BirthDate = HospitalFormDateTimeField("Birthdate ", default(DateTime), false);
                physician.Gender = HospitalFormField("Gender ", "", false);
                while (!allowLettersOnly(physician.Gender)) physician.Gender = HospitalFormField("Gender ", "", false);
                physician.Weight = HospitalFormDecimalField("Weight ", 0.00, false);
                physician.Height = HospitalFormDecimalField("Height ", 0.00, false);
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("|              Contact Information                  |");
                Console.WriteLine("-----------------------------------------------------");
                physician.ContactInfo.HomeAddress = HospitalFormField("Home Address ", "", false);
                physician.ContactInfo.HomePhone = HospitalFormField("Home Phone ", "", false);
                while(!allowNumbersOnly(physician.ContactInfo.HomePhone, false)) physician.ContactInfo.HomePhone = HospitalFormField("Home Phone ", "", false);
                physician.ContactInfo.OfficeAddress = HospitalFormField("Office Address ", "", false);
                physician.ContactInfo.OfficePhone = HospitalFormField("Office Phone ", "", false);
                while (!allowNumbersOnly(physician.ContactInfo.OfficePhone, false)) physician.ContactInfo.OfficePhone = HospitalFormField("Office Phone ", "", false);
                physician.ContactInfo.EmailAddress = HospitalFormField("E-mail Address ", "", false);
                physician.ContactInfo.CellphoneNumber = HospitalFormField("Cellphone Number ", "", false);
                while (!allowNumbersOnly(physician.ContactInfo.CellphoneNumber, false)) physician.ContactInfo.CellphoneNumber = HospitalFormField("Cellphone Number ", "", false);
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("|              Specializations                      |");
                Console.WriteLine("-----------------------------------------------------");
                Console.Write("How many specializations? ");
                int specializationCount = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");
                for (int i = 0; i < specializationCount; i++)
                {
                    specialization = new Specialization();
                    specialization.Name = HospitalFormField("Name ", "", false);
                    specialization.Remarks = HospitalFormField("Remarks ", "", false);
                    Console.WriteLine();
                    physician.Specialization.Add(specialization);
                }

                physicians.Add(physician);
                HospitalMenu();
            }
            catch (Exception e)
            {
                defaultError();
                goBackToMainMenu();
            }
        }

        private static void DeletePhysicianRecordById()
        {
            try
            {
                Console.Clear();
                HospitalMenuHeader();
                Console.Write("Please enter employee ID to be deleted : ");
                int id = Convert.ToInt32(Console.ReadLine());

                List<Physician> physicianRecords = new List<Physician>();
                physicianRecords = physicians.Where(x => x.Id == id).ToList();

                if (physicianRecords.Count > 0)
                {
                    PhysicianRecordDetails(physicianRecords);

                    Console.Write("Are you sure you want to delete this record (Yes/No)? ");
                    var response = Console.ReadLine();
                    if (response.Trim().Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var recordToBeDeleted = physicians.Single(r => r.Id == id);
                        physicians.Remove(recordToBeDeleted);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Employee records successfully deleted.");
                        Console.ResetColor();
                    }
                    else if (response.Trim().Equals("No", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Employee records not deleted.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid input, record not deleted.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.WriteLine("|             No Available Records                  |");
                }
                HospitalMenuFooter();
            }
            catch (Exception)
            {
                defaultError();
                goBackToMainMenu();
            }
        }

        private static void UpdatePhysicianRecordById()
        {
            try
            {
                Console.Clear();
                HospitalMenuHeader();
                Console.Write("Please enter employee ID to update records : ");
                int id = Convert.ToInt32(Console.ReadLine());

                List<Physician> physicianRecords = new List<Physician>();
                physicianRecords = physicians.Where(x => x.Id == id).ToList();
                Physician newPhysicianRecord = new Physician();

                if (physicianRecords.Count > 0)
                {
                    foreach (var physician in physicianRecords)
                    {
                        Console.WriteLine("-----------------------------------------------------");
                        Console.WriteLine("                Personal Information                 ");
                        newPhysicianRecord.Id = physician.Id;
                        newPhysicianRecord.FirstName = HospitalFormField("First Name ", physician.FirstName, true);
                        while (!allowLettersOnly(newPhysicianRecord.FirstName)) newPhysicianRecord.FirstName = HospitalFormField("First Name ", physician.FirstName, true);
                        newPhysicianRecord.MiddleName = HospitalFormField("Middle Name ", physician.MiddleName, true);
                        while (!allowLettersOnly(newPhysicianRecord.MiddleName)) newPhysicianRecord.MiddleName = HospitalFormField("Middle Name ", physician.MiddleName, true);
                        newPhysicianRecord.LastName = HospitalFormField("Last Name ", physician.LastName, true);
                        while (!allowLettersOnly(newPhysicianRecord.LastName)) newPhysicianRecord.LastName = HospitalFormField("Last Name ", physician.LastName, true);
                        newPhysicianRecord.BirthDate = HospitalFormDateTimeField("Birthdate ", physician.BirthDate, true);
                        newPhysicianRecord.Gender = HospitalFormField("Gender ", physician.Gender, true);
                        while (!allowLettersOnly(newPhysicianRecord.Gender)) newPhysicianRecord.Gender = HospitalFormField("Gender ", physician.Gender, true);
                        newPhysicianRecord.Weight = HospitalFormDecimalField("Weight ", physician.Weight, true);
                        newPhysicianRecord.Height = HospitalFormDecimalField("Height ", physician.Height, true);
                        Console.WriteLine("                Contact Information                  ");
                        newPhysicianRecord.ContactInfo = new ContactInfo();
                        newPhysicianRecord.ContactInfo.HomeAddress = HospitalFormField("Home Address ", physician.ContactInfo.HomeAddress, true);
                        newPhysicianRecord.ContactInfo.HomePhone = HospitalFormField("Home Phone ", physician.ContactInfo.HomePhone, true);
                        while (!allowNumbersOnly(newPhysicianRecord.ContactInfo.HomePhone, false)) newPhysicianRecord.ContactInfo.HomePhone = HospitalFormField("Home Phone ", physician.ContactInfo.HomePhone, true);
                        newPhysicianRecord.ContactInfo.OfficeAddress = HospitalFormField("Office Address ", physician.ContactInfo.OfficeAddress, true);
                        newPhysicianRecord.ContactInfo.OfficePhone = HospitalFormField("Office Phone ", physician.ContactInfo.OfficePhone, true);
                        while (!allowNumbersOnly(newPhysicianRecord.ContactInfo.OfficePhone, false)) newPhysicianRecord.ContactInfo.OfficePhone = HospitalFormField("Office Phone ", physician.ContactInfo.OfficePhone, true);
                        newPhysicianRecord.ContactInfo.EmailAddress = HospitalFormField("E-mail Address ", physician.ContactInfo.EmailAddress, true);
                        newPhysicianRecord.ContactInfo.CellphoneNumber = HospitalFormField("Cellphone Number ", physician.ContactInfo.CellphoneNumber, true);
                        while (!allowNumbersOnly(newPhysicianRecord.ContactInfo.CellphoneNumber, false)) newPhysicianRecord.ContactInfo.CellphoneNumber = HospitalFormField("Cellphone Number ", physician.ContactInfo.CellphoneNumber, true);

                        Specialization newSpecializationRecord = new Specialization();
                        newPhysicianRecord.Specialization = new List<Specialization>();

                        Console.WriteLine("                Specializations                      ");
                        foreach (var specialization in physician.Specialization)
                        {
                            newSpecializationRecord = new Specialization();
                            newSpecializationRecord.Name = HospitalFormField("Name ", specialization.Name, true);
                            newSpecializationRecord.Remarks = HospitalFormField("Remarks ", specialization.Remarks, true);
                            newPhysicianRecord.Specialization.Add(newSpecializationRecord);
                        }

                        Console.Write("Are you sure you want to update employee records (Yes/No)? ");
                        var response = Console.ReadLine();
                        if (response.Trim().Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var prevRecordToBeDeleted = physicians.Single(r => r.Id == id);
                            bool result = physicians.Remove(prevRecordToBeDeleted);

                            if (result)
                            {
                                physicians.Add(newPhysicianRecord);
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Employee records successfully updated.");
                            Console.ResetColor();
                        }
                        else if (response.Trim().Equals("No", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Employee records not updated.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Invalid input, record not updated.");
                            Console.ResetColor();
                        }
                        Console.WriteLine("-----------------------------------------------------");

                    }
                }
                else
                {
                    Console.WriteLine("|             No Available Records                  |");
                }
                HospitalMenuFooter();
            }
            catch (Exception)
            {
                defaultError();
                goBackToMainMenu();
            }
        }

        private static void ViewAllPhysicianRecords()
        {
            try
            {
                Console.Clear();
                HospitalMenuHeader();
                Console.WriteLine("|             List of Physician Records             |");
                Console.WriteLine("=====================================================");
                if (physicians.Count > 0)
                {
                    foreach (var physician in physicians)
                    {
                        PersonalInformation(physician);
                        ContactInformation(physician);
                        Console.WriteLine("-----------------------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("|             No Available Records                  |");
                }
                HospitalMenuFooter();
            }
            catch (Exception)
            {
                defaultError();
                goBackToMainMenu();
            }
        }

        private static void GetPhysicianRecordById()
        {
            Console.Clear();
            HospitalMenuHeader();
            Console.Write("Please enter employee ID : ");
            int id = Convert.ToInt32(Console.ReadLine());

            List<Physician> physicianRecords = new List<Physician>();
            physicianRecords = physicians.Where(x => x.Id == id).ToList();

            if (physicianRecords.Count > 0)
            {
                PhysicianRecordDetails(physicianRecords);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("|             No Available Records                  |");
            }
            HospitalMenuFooter();
        }

        private static void PhysicianRecordDetails(List<Physician> physicians)
        {
            foreach (var physician in physicians)
            {
                PersonalInformation(physician);
                ContactInformation(physician);
                Console.WriteLine("                Specializations                      ");
                if (physician.Specialization.Count > 0)
                {
                    foreach (var specialization in physician.Specialization)
                    {
                        Console.WriteLine("Name     : " + specialization.Name);
                        Console.WriteLine("Remarks  : " + specialization.Remarks);
                    }
                }
                else
                {
                    Console.WriteLine("No specialization/s available.");
                }
                Console.WriteLine("-----------------------------------------------------");
            }
        }

        private static string HospitalFormField(string fieldType, string value, bool isEditable)
        {
            Console.Write(fieldType + " : ");
            if (isEditable) SendKeys.SendWait(value);
            string input = Console.ReadLine();
            return input;
        }

        private static DateTime HospitalFormDateTimeField(string fieldType, DateTime value, bool isEditable)
        {
            Console.WriteLine(fieldType);
            Console.Write("(MM/DD/YYYY) : ");
            if (isEditable) SendKeys.SendWait(value.ToShortDateString());
            DateTime input = Convert.ToDateTime(Console.ReadLine());
            return input;
        }

        private static double HospitalFormDecimalField(string fieldType, double value, bool isEditable)
        {
            var unit = string.Empty;
            if (fieldType.Trim().Equals("Height", StringComparison.InvariantCultureIgnoreCase))
            {
                unit = "in cm";
            }
            else
            {
                unit = "in kg";
            }
            Console.Write(fieldType + " ("+ unit + ") : ");
            if (isEditable) SendKeys.SendWait(Convert.ToString(value));
            double input = Convert.ToDouble(Console.ReadLine());
            return input;
        }

        private static void PersonalInformation(Physician physician)
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("                Personal Information                 ");
            Console.WriteLine("Employee ID  : " + physician.Id);
            Console.WriteLine("First Name   : " + physician.FirstName);
            Console.WriteLine("Middle Name  : " + physician.MiddleName);
            Console.WriteLine("Last Name    : " + physician.LastName);
            Console.WriteLine("Birthdate    : " + physician.BirthDate.ToShortDateString());
            Console.WriteLine("Gender       : " + physician.Gender);
            Console.WriteLine("Weight       : " + physician.Weight);
            Console.WriteLine("Height       : " + physician.Height);
        }

        private static void ContactInformation(Physician physician)
        {
            Console.WriteLine("                Contact Information                  ");
            if (physician.ContactInfo != null)
            {
                Console.WriteLine("Home Address     : " + physician.ContactInfo.HomeAddress);
                Console.WriteLine("Home Phone       : " + physician.ContactInfo.HomePhone);
                Console.WriteLine("Office Address   : " + physician.ContactInfo.OfficeAddress);
                Console.WriteLine("Office Phone     : " + physician.ContactInfo.OfficePhone);
                Console.WriteLine("E-mail Address   : " + physician.ContactInfo.EmailAddress);
                Console.WriteLine("Cellphone Number : " + physician.ContactInfo.CellphoneNumber);
            }
            else
            {
                Console.WriteLine("No contact information available.");
            }
        }

        private static void HospitalMenuHeader()
        {
            Console.WriteLine("=====================================================");
            Console.WriteLine("| Pointwest Hospital Information Management System  |");
            Console.WriteLine("=====================================================");
        }

        private static void HospitalMenuFooter()
        {
            Console.WriteLine("=====================================================");
            Console.WriteLine("|       Press enter to go back to main menu         |");
            Console.WriteLine("=====================================================");
            goBackToMainMenu();
        }

        private static void goBackToMainMenu()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                HospitalMenu();
            }
        }

        private static string GetUserInput(string validPattern = null)
        {
            var input = Console.ReadLine();
            input = input.Trim();

            if (validPattern != null && !System.Text.RegularExpressions.Regex.IsMatch(input, validPattern))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\"" + input + "\" is not valid.\n");
                Console.ResetColor();
                return null;
            }

            return input;
        }

        private static bool allowLettersOnly(string input)
        {
            bool result = false;
            input = input.Trim();

            if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^[a-zA-Z ]+$"))
            {
                result = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid input, allow only letters.");
                Console.ResetColor();
            }
            return result;
        }

        private static bool allowNumbersOnly(string input, bool isDecimalAllowed)
        {
            bool result = false;
            input = input.Trim();

            var pattern = isDecimalAllowed == true ? @"^[0-9. ]+$" : @"^[0-9 ]+$";
            var error = isDecimalAllowed == true ? "Invalid input, allow only numbers and period." : "Invalid input, allow only numbers.";

            if (System.Text.RegularExpressions.Regex.IsMatch(input, pattern))
            {
                result = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(error);
                Console.ResetColor();
            }
            return result;
        }

        private static void defaultError()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Error encountered, please press enter to go back to main menu....");
            Console.ResetColor();
        }
    }
}
