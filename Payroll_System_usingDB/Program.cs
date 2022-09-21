using System;
using System.Data.SqlClient;

namespace Payroll_System_usingDB
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Employee Agus = new Employee("Agus Kuncoro", "081234567891", "P2", "897654321");
            Database db = new Database();
            //db.InsertEmployee(Agus);

            Overtime ot_1 = new Overtime(2, "2022-09-12",100000);
            //db.InsertOvertime(ot_1);
            Bonus bn_1 = new Bonus(2, "2022-09-12", 500000, "Bonus Penjualan Bulanan");
            //db.InsertBonus(bn_1);
            salaryCut sc_1 = new salaryCut(2, "2022-09-12", 50000, "Terlambat");
            db.InsertSalaryCut(sc_1);

            //Console.WriteLine(db.getEmployeeID(Agus));

        }


    }

    class Employee
    {
        public string Name;
        public string Phone;
        public string Position;
        public string accountNumber;

        public Employee(string Name, string Phone, string Position, string accountNumber)
        {
            this.Name = Name;
            this.Phone = Phone;
            this.Position = Position;
            this.accountNumber = accountNumber;
        }

    }
    class Overtime
    {
        public int employeeID;
        public string date;
        public int fee;
        public Overtime(int employeeID, string date, int fee)
        {
            this.employeeID = employeeID;
            this.date = date;
            this.fee = fee;
        }
    }

        
    class Bonus
    {
        public int employeeID;
        public string date;
        public int amount;
        public string desc;
        public Bonus(int employeeID, string date, int amount, string desc)
        {
            this.employeeID = employeeID;
            this.date = date;
            this.amount = amount;
            this.desc = desc;
        }
    }
    class salaryCut
    {
        public int employeeID;
        public string date;
        public int cut;
        public string desc;
        public salaryCut(int employeeID, string date, int cut, string desc)
        {
            this.employeeID = employeeID;
            this.date = date;
            this.cut = cut;
            this.desc = desc;
        }
    }




    class Database {
        SqlConnection sqlConnection;
        string connectionString = "Data Source=TARDIS;Initial Catalog=Payroll_System;User ID=viaaprillya;Password=1234567";
       
        public void InsertEmployee(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter name = new SqlParameter();
                name.ParameterName = "@name";
                name.Value = employee.Name;

                SqlParameter position = new SqlParameter();
                position.ParameterName = "@position";
                position.Value = employee.Position;

                SqlParameter account = new SqlParameter();
                account.ParameterName = "@account";
                account.Value = employee.accountNumber;

                SqlParameter phone = new SqlParameter();
                phone.ParameterName = "@phone";
                phone.Value = employee.Phone;

                sqlCommand.Parameters.Add(name);
                sqlCommand.Parameters.Add(position);
                sqlCommand.Parameters.Add(phone);
                sqlCommand.Parameters.Add(account);

                try
                {
                    sqlCommand.CommandText = "INSERT INTO Employee " +
                        "(Employee_Name, Phone, Account_Number, PositionID) VALUES (@name, @phone, @account, @position)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }

        public int getEmployeeID(Employee employee)
        {
            int employeeID=0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter name = new SqlParameter();
                name.ParameterName = "@name";
                name.Value = employee.Name;

                sqlCommand.Parameters.Add(name);

                try
                {
                    sqlCommand.CommandText = "SELECT EmployeeID FROM Employee WHERE Employee_Name = @name ";
                    employeeID = (int)sqlCommand.ExecuteScalar();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }

            return employeeID;
        }
    

        public void getEmployee()
        {
            string query = "SELECT * FROM Employee";

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine(sqlDataReader[0] + " - " + sqlDataReader[1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        public void InsertOvertime(Overtime overtime)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter employeeID = new SqlParameter();
                employeeID.ParameterName = "@employeeID";
                employeeID.Value = overtime.employeeID;

                SqlParameter date = new SqlParameter();
                date.ParameterName = "@date";
                date.Value = overtime.date;

                SqlParameter fee = new SqlParameter();
                fee.ParameterName = "@fee";
                fee.Value = overtime.fee;

                sqlCommand.Parameters.Add(employeeID);
                sqlCommand.Parameters.Add(date);
                sqlCommand.Parameters.Add(fee);

                try
                {
                    sqlCommand.CommandText = "INSERT INTO Overtime " +
                        "(EmployeeID, Overtime_Date, Overtime_Fee) VALUES (@employeeID, @date, @fee)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }

        public void InsertBonus(Bonus bonus)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter employeeID = new SqlParameter();
                employeeID.ParameterName = "@employeeID";
                employeeID.Value = bonus.employeeID;

                SqlParameter date = new SqlParameter();
                date.ParameterName = "@date";
                date.Value = bonus.date;

                SqlParameter amount = new SqlParameter();
                amount.ParameterName = "@amount";
                amount.Value = bonus.amount;

                SqlParameter desc = new SqlParameter();
                desc.ParameterName = "@desc";
                desc.Value = bonus.desc;

                sqlCommand.Parameters.Add(employeeID);
                sqlCommand.Parameters.Add(date);
                sqlCommand.Parameters.Add(amount);
                sqlCommand.Parameters.Add(desc);

                try
                {
                    sqlCommand.CommandText = "INSERT INTO Bonus " +
                        "(EmployeeID, Bonus_Date, Bonus_Amount, Bonus_Description) VALUES (@employeeID, @date, @amount, @desc)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }

        public void InsertSalaryCut(salaryCut sc)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter employeeID = new SqlParameter();
                employeeID.ParameterName = "@employeeID";
                employeeID.Value = sc.employeeID;

                SqlParameter date = new SqlParameter();
                date.ParameterName = "@date";
                date.Value = sc.date;

                SqlParameter cut = new SqlParameter();
                cut.ParameterName = "@cut";
                cut.Value = sc.cut;

                SqlParameter desc = new SqlParameter();
                desc.ParameterName = "@desc";
                desc.Value = sc.desc;

                sqlCommand.Parameters.Add(employeeID);
                sqlCommand.Parameters.Add(date);
                sqlCommand.Parameters.Add(cut);
                sqlCommand.Parameters.Add(desc);

                try
                {
                    sqlCommand.CommandText = "INSERT INTO SalaryCut " +
                        "(EmployeeID, SalaryCut_Date, SalaryCut_Amount, SalaryCut_Description) VALUES (@employeeID, @date, @cut, @desc)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }

        public void getTotalSalary(int employeeID)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter empID = new SqlParameter();
                empID.ParameterName = "";
                empID.Value = employeeID;

                sqlCommand.Parameters.Add(empID);

                try
                {
                    sqlCommand.CommandText = "SELECT EmployeeID FROM Employee WHERE Employee_Name = @name ";
                    employeeID = (int)sqlCommand.ExecuteScalar();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }

            return employeeID;
        }

        void updateEmployee()
        {

        }


        void deleteEmployee()
        {

        }

    }

}
