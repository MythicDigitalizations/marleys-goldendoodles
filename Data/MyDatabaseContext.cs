using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MarleysGoldendoodles.Models
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options)
            : base(options)
        {
        }

        public async Task<int> CreateWaitlistEntry(string FirstName, string LastName, string PhoneNumber)
        {
            var retVal = 0;

            var parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@FirstName",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = FirstName
                        },
                        new SqlParameter() {
                            ParameterName = "@LastName",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = LastName
                        },
                        new SqlParameter() {
                            ParameterName = "@PhoneNumber",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size = 10,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = PhoneNumber
                        }};

            retVal = await this.Database.ExecuteSqlRawAsync(@"
                EXEC [dbo].[CreateWaitlistEntry] 
                   @FirstName
                  ,@LastName
                  ,@PhoneNumber
            ", parameters);

            return retVal;
        }

        public async Task<int> UpdateWaitlistEntry(int WaitListId, decimal AmountPaid)
        {
            var retVal = 0;

            var parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@WaitListId",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = WaitListId
                        },
                        new SqlParameter() {
                            ParameterName = "@AmountPaid",
                            SqlDbType =  System.Data.SqlDbType.Decimal,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = AmountPaid
                        }};

            retVal = await this.Database.ExecuteSqlRawAsync(@"
                EXEC [dbo].[UpdateWaitlistEntry] 
                   @WaitListId
                  ,@AmountPaid
            ", parameters);

            return retVal;
        }

        public async Task<int> RemoveWaitlistEntry(int WaitListId)
        {
            var retVal = 0;

            var parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@WaitListId",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = WaitListId
                        }};

            retVal = await this.Database.ExecuteSqlRawAsync(@"
                EXEC [dbo].[RemoveWaitlistEntry] 
                   @WaitListId
            ", parameters);

            return retVal;
        }
    }
}
