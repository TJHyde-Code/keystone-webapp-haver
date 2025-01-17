using Microsoft.EntityFrameworkCore;

namespace HaverGroupProject.Data
{
    public class HaverContext : DbContext
    {
        //Constructor
        public HaverContext(DbContextOptions<HaverContext> options) : base(options)
        {

        }

        //DBSets


        //ModelBuilder        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cascade Delete protection.


            //Unique Index constraints.


            base.OnModelCreating(modelBuilder);
        }
    }//Class close.
}//Namespace close.
