using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesLibrary;

public class CustomersController
{
    private SqlConnection sqlConnection { get; set; }

    public List<Customer> GetBySales(decimal low, decimal high)
    {
        var sql = "SELECT * from Customers " +
                $" Where Sales >= {low} and Sales <= {high}; ";
        var cmd = new SqlCommand(sql, sqlConnection);
        SqlDataReader reader = cmd.ExecuteReader();
        List<Customer> customers = new List<Customer>();
        while (reader.Read())
        {
            Customer customer = new Customer();
            customer.Id = Convert.ToInt32(reader["ID"]);
            customer.Name = Convert.ToString(reader["Name"])!;
            customer.City = Convert.ToString(reader["City"])!;
            customer.State = Convert.ToString(reader["State"])!;
            customer.Sales = Convert.ToDecimal(reader["Sales"]);
            customer.Active = Convert.ToBoolean(reader["active"]);
            customers.Add(customer);
        }
        reader.Close();
        return customers;
    }

    public List<Customer> GetAll()
    {
        var sql = "SELECT * from Customers;";
        var cmd = new SqlCommand(sql, sqlConnection);
        SqlDataReader reader = cmd.ExecuteReader();
        List<Customer> customers = new List<Customer>();
        while(reader.Read())
        {
            Customer customer = new Customer();
            customer.Id = Convert.ToInt32(reader["ID"]);
            customer.Name = Convert.ToString(reader["Name"])!;
            customer.City = Convert.ToString(reader["City"])!;
            customer.State = Convert.ToString(reader["State"])!;
            customer.Sales = Convert.ToDecimal(reader["Sales"]);
            customer.Active = Convert.ToBoolean(reader["active"]);
            customers.Add(customer);
        }
        reader.Close();
        return customers;
    }

    public Customer? GetById(int Id)
    {
        var sql = $"SELECT * from Customers Where Id = {Id};";
        var cmd = new SqlCommand(sql, sqlConnection);
        SqlDataReader reader = cmd.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }
        reader.Read();
        Customer customer = new Customer();
        customer.Id = Convert.ToInt32(reader["ID"]);
        customer.Name = Convert.ToString(reader["Name"])!;
        customer.City = Convert.ToString(reader["City"])!;
        customer.State = Convert.ToString(reader["State"])!;
        customer.Sales = Convert.ToDecimal(reader["Sales"]);
        customer.Active = Convert.ToBoolean(reader["active"]);
        reader.Close();
        return customer;
    }

    public bool Add(Customer cust)
    {
        var sql = "INSERT Customers (Name, city, State, Sales, Active) VALUES " +
                    $" ('{cust.Name}','{cust.City}','{cust.State}', {cust.Sales}, {(cust.Active ? 1 : 0)});";
        var cmd = new SqlCommand(sql, sqlConnection);
        var rowsAffected = cmd.ExecuteNonQuery();
        if(rowsAffected == 0)
        {
            return false;
        }
        return true;
    }

    public bool Update(Customer cust)
    {
        var sql = " UPDATE Customers SET " +
                   $" Name = '{cust.Name}', " +
                   $"City = '{cust.City}', " +
                   $"State = '{cust.State}', " +
                   $"Sales = {cust.Sales}, " +
                   $"Active = {(cust.Active ? 1 : 0)} " +
                   $"Where Id = {cust.Id}; ";
         var cmd = new SqlCommand(sql, sqlConnection);
         var rowsAffected = cmd.ExecuteNonQuery();
         if (rowsAffected == 0)
         {
                return false;
         }
            return true;
        
    }

    public bool Delete(int Id)
    {
        var sql = " DELETE Customers " +
                   $"Where Id = {Id}; ";
        var cmd = new SqlCommand(sql, sqlConnection);
        var rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected == 0)
        {
            return false;
        }
        return true;
    }

    public CustomersController (string server, string instance)
    {
        var connStr = $"server={server}\\{instance};" +
                        "database = Salesdb;" +
                        "trusted_Connection=True;" +
                        "trustservercertificate=true;";
        sqlConnection = new SqlConnection(connStr);
        sqlConnection.Open();
        if(sqlConnection.State!= ConnectionState.Open)
        {
            throw new Exception("Connection Failed!");
        }

    }

    public void CloseConnection()
    {
        if(sqlConnection.State == ConnectionState.Open)
        {
            sqlConnection.Close();
        }
    }
}

