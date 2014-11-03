using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;

namespace DataLayer.DataService
{
   public abstract class SqlServerDataService : IDataService, IDisposable
   {
      bool m_disposed = false;
      private SqlConnection m_sqlConn = null;
      protected Exception m_runningException = null;

      public Exception DataException
      {
         get { return m_runningException; }
         protected set { m_runningException = value; }
      }

      protected SqlConnection SqlConnect
      {
         get
         {
            if (m_sqlConn == null)
               m_sqlConn = new SqlConnection(GetConnectionString());
            return m_sqlConn;
         }
      }

      private string ConnectionString
      {
         get
         {
            //return "Persist Security Info=false; database=SimplyLog;server=BCRS50VMSQL02;User ID=sage50wslogin;Pwd=N0m0$e9Bu5e4;Trusted_Connection=false";
            return "server=BCRS50VMSQL02;database=SimplyLog;Trusted_Connection=True";
         }
      }

      public abstract void InsertData(IData data);
      public abstract void UpdateData(IData data);
      public abstract void DeleteData(IData data);
      public abstract bool LoadData(ref IData data);


      /// <summary>
      /// Called during "disposed" to release resources
      /// </summary>
      public void Dispose()
      {
         Dispose(true);

         // We may not need this since we do not have unmanaged resources set up at this moment
         // It is still here just to follow the Dispose pattern
         GC.SuppressFinalize(this);
      }

      protected string GetConnectionString()
      {
         return ConnectionString;
      }

      protected virtual void Dispose(bool disposing)
      {
         // Execute if resources have not not already been disposed
         if (!m_disposed)
         {
            // Free managed resources
            if (disposing)
            {
               if (m_sqlConn != null)
               {
                  // Does not hur to close more than once if it has been closed already
                  m_sqlConn.Close();

                  m_sqlConn.Dispose();

                  m_sqlConn = null;
               }
            }

            // Free unmanaged resources here, if any
         }

         m_disposed = true;
      }
   }

   public class CustomerDataService : SqlServerDataService
   {
      public override void InsertData(IData data)
      {
         try
         {
            SqlConnect.Open();

            CustomerData realData = data as CustomerData;
            if (realData == null)
               return;

            using (SqlCommand command = new SqlCommand())
            {
               command.Connection = SqlConnect;
               command.CommandText = "INSERT INTO [SampleUT].[dbo].[tCustomer] VALUES (@Property)";
               command.CommandType = CommandType.Text;
               command.Parameters.Add("@Property", SqlDbType.Char).Value = realData.Property;
               command.CommandTimeout = 40;

               // Execute
               command.ExecuteNonQuery();
            }
         }
         catch (Exception exp)
         {
            DataException = exp;
         }
         finally
         {
            SqlConnect.Close();
         }
      }

      public override bool LoadData(ref IData data)
      {
         CustomerData realData = data as CustomerData;
         if (realData == null)
            return false;

         bool canLoad = false;

         try
         {
            SqlConnect.Open();

            using (SqlCommand command = new SqlCommand())
            {
               command.Connection = SqlConnect;
               command.CommandText = "SELECT * FROM [SampleUT].[dbo].[tCustomer] WHERE Id = @Id";
               command.CommandType = CommandType.Text;
               command.Parameters.Add("@Id", SqlDbType.Char).Value = realData.Id;
               command.CommandTimeout = 40;

               // Execute
               using (SqlDataReader reader = command.ExecuteReader())
               {
                  // Just get the first record
                  if (reader.Read())
                  {
                     // Parse and process to rturn
                     realData = new CustomerData
                     {
                        Id = (int)reader["Id"],
                        Property = (string)reader["Property"],
                        Email = (string)reader["Email"],
                     };
                  }

                  canLoad = true;
               }
            }

         }
         catch (Exception exp)
         {
            DataException = exp;
            canLoad = false;
         }
         finally
         {
            SqlConnect.Close();
         }

         return canLoad;
      }

      public override void UpdateData(IData data)
      {
         try
         {
            SqlConnect.Open();

            CustomerData realData = data as CustomerData;
            if (realData == null)
               return;

            using (SqlCommand command = new SqlCommand())
            {
               command.Connection = SqlConnect;
               command.CommandText = "UPDATE [SampleUT].[dbo].[tCustomer] SET property = @Property WHERE Id = @Id";
               command.CommandType = CommandType.Text;
               command.Parameters.Add("@Id", SqlDbType.Int).Value = realData.Id;
               command.Parameters.Add("@Property", SqlDbType.Char).Value = realData.Property;
               command.CommandTimeout = 40;

               // Execute
               command.ExecuteNonQuery();
            }
         }
         catch (Exception exp)
         {
            DataException = exp;
         }
         finally
         {
            SqlConnect.Close();
         }
      }

      public override void DeleteData(IData data)
      {
         try
         {
            SqlConnect.Open();

            CustomerData realData = data as CustomerData;
            if (realData == null)
               return;

            using (SqlCommand command = new SqlCommand())
            {
               command.Connection = SqlConnect;
               command.CommandText = "DELETE FROM [SampleUT].[dbo].[tCustomer] WHERE Id = @Id";
               command.CommandType = CommandType.Text;
               command.Parameters.Add("@Id", SqlDbType.Char).Value = realData.Id;
               command.CommandTimeout = 40;

               // Execute
               command.ExecuteNonQuery();
            }
         }
         catch (Exception exp)
         {
            DataException = exp;
         }
         finally
         {
            SqlConnect.Close();
         }
      }
   }
}
