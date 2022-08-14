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

            retVal = await this.Database.ExecuteSqlRawAsync(@"
                EXECUTE @RC = [dbo].[CreateWaitlistEntry] 
                   @FirstName
                  ,@LastName
                  ,@PhoneNumber
                GO
            ", retVal, FirstName, LastName, PhoneNumber);

            return retVal;
        }

        public async Task<int> UpdateWaitlistEntry(int WaitListId, decimal AmountPaid)
        {
            var retVal = 0;

            retVal = await this.Database.ExecuteSqlRawAsync(@"
                EXECUTE @RC = [dbo].[CreateWaitlistEntry] 
                   @WaitListId
                  ,@AmountPaid
                GO
            ", retVal, WaitListId, AmountPaid);

            return retVal;
        }

        public async Task<int> RemoveWaitlistEntry(int WaitListId)
        {
            var retVal = 0;

            retVal = await this.Database.ExecuteSqlRawAsync(@"
                EXECUTE @RC = [dbo].[CreateWaitlistEntry] 
                   @WaitListId
                GO
            ", retVal, WaitListId);

            return retVal;
        }
    }
}
