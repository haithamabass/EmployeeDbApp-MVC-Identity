using EmployeeDbApp.Models.Content;

namespace EmployeeDbApp.Data.Content
{
    public class SeedCitesAndDepartments
    {

        public static async Task SeedCites(AppDbContext Db)
        {
            if(!Db.Cites.Any())
            {
                var _cites = new List<City>
                {
                    new City{CityName = "Alexandria"},
                    new City{CityName = "Aswan"},
                    new City{CityName = "Asyut"},
                    new City{CityName = "Beheira"},
                    new City{CityName = "Beni Suef"},
                    new City{CityName = "Cairo"},
                    new City{CityName = "Dakahlia"},
                    new City{CityName = "Damietta"},
                    new City{CityName = "Faiyum"},
                    new City{CityName = "Gharbia"},
                    new City{CityName = "Giza"},
                    new City{CityName = "Ismailia"},
                    new City{CityName = "Kafr El Sheikh"},
                    new City{CityName = "Luxor"},
                    new City{CityName = "Matruh"},
                    new City{CityName = "Minya"},
                    new City{CityName = "Monufia"},
                    new City{CityName = " El Wadi El Gedid"},
                    new City{CityName = "North Sinai"},
                    new City{CityName = "Port Said"},
                    new City{CityName = "Qalyubia"},
                    new City{CityName = "Qena"},
                    new City{CityName = " Red Sea"},
                    new City{CityName = "Zagazig"},
                    new City{CityName = "Sohag"},
                    new City{CityName = "Suez"},
                    new City{CityName = "Ismailia"},
                };

                Db.AddRange(_cites);
                Db.SaveChanges();

            }
        }




        public static async Task SeedDepartments(AppDbContext Db)
        {
            if (!Db.Departments.Any())
            {
                var _departments = new List<Department>
                {
                    new Department{DepartmentName = "HR"},
                    new Department{DepartmentName = "IT"},
                    new Department{DepartmentName = "Sales "},
                    new Department{DepartmentName = "Marketing "},
                    new Department{DepartmentName = "Operations"},
                    new Department{DepartmentName = "Finance"},
                    new Department{DepartmentName = "Purchasing."},
                    new Department{DepartmentName = "Production"}

                 };

                Db.AddRange(_departments);
                Db.SaveChanges();

            }
        }





    }
}

