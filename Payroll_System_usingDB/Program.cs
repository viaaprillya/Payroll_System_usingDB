using System;
using System.Data.SqlClient;

namespace Payroll_System_usingDB
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Database db = new Database();

            Employee Agus = new Employee("Agus Kuncoro", "081234567891", "P2", "897654321");
            db.InsertEmployee(Agus); //Menambahkan Data Karyawan

            Overtime ot_1 = new Overtime(2, "2022-09-12", 100000);
            db.InsertOvertime(ot_1); //Menambahkan Data Lembur

            Bonus bn_1 = new Bonus(2, "2022-09-12", 500000, "Bonus Penjualan Bulanan");
            db.InsertBonus(bn_1); //Menambahkan Data Bonus

            salaryCut sc_1 = new salaryCut(2, "2022-09-12", 50000, "Terlambat");
            db.InsertSalaryCut(sc_1); //Menambahkan Data Potongan Gaji

            db.TotalSalary(); //Mengisi tabel Salary dari data tabel lain yang sudah terisi
            db.getSalary(); //Mencetak tabel Salary
            //db.deleteSalary(2); //Jika ingin menghapus data di Salary berdasarkan ID
            //db.updateEmployeePhone(1, "081208120812"); //Jika Ingin mengubah data nomer telepon Karyawan

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

        public void TotalSalary()
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                try
                {
                    sqlCommand.CommandText = "INSERT INTO Salary(EmployeeID, Basic_Salary, Allowance, Bonus_Total, SalaryCut_Total, Overtime_Total, Salary_Total) SELECT Employee.EmployeeID, Position.Basic_Salary, Position.Allowance, Bonus.Bonus_Amount, SalaryCut.SalaryCut_Amount, Overtime.Overtime_Fee, (Position.Basic_Salary+Position.Allowance+Bonus.Bonus_Amount+Overtime.Overtime_Fee-SalaryCut.SalaryCut_Amount) " + 
                        "FROM Employee JOIN Position ON Employee.PositionID=Position.PositionID "+
                        "JOIN Bonus ON Employee.EmployeeID=Bonus.EmployeeID "+
                        "JOIN Overtime ON Employee.EmployeeID=Overtime.EmployeeID " +
                        "JOIN SalaryCut ON Employee.EmployeeID=SalaryCut.EmployeeID ";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }

        public void getSalary()
        {
            string query = "SELECT Employee.Employee_Name, Salary.Salary_Total FROM Employee JOIN Salary ON Employee.EmployeeID = Salary.EmployeeID";

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


            public void deleteSalary(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = id;

                sqlCommand.Parameters.Add(sqlParameter);

                try
                {
                    sqlCommand.CommandText = "DELETE FROM Salary " +
                        "WHERE SalaryID=@id";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);

                }

            }

        }

            public void updateEmployeePhone(int id, string phone)
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.Transaction = sqlTransaction;

                    SqlParameter eid= new SqlParameter();
                    eid.ParameterName = "@id";
                    eid.Value = id;

                    SqlParameter ephone = new SqlParameter();
                    ephone.ParameterName = "@phone";
                    ephone.Value = phone;

                    sqlCommand.Parameters.Add(ephone);
                    sqlCommand.Parameters.Add(eid);

                    try
                    {
                        sqlCommand.CommandText = "UPDATE Employee SET Phone=@phone " +
                            "WHERE EmployeeID=@id";
                        sqlCommand.ExecuteNonQuery();
                        sqlTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.InnerException);

                    }

                }

            }


    }

}
