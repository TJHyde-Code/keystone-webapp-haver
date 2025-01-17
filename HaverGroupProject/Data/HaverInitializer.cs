using Microsoft.EntityFrameworkCore;
using HaverGroupProject.Models;
using System.Diagnostics;
using System.Linq.Expressions;

namespace HaverGroupProject.Data
{
    public static class HaverInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider,
            bool DeleteDatabase = false, bool UseMigrations = true, bool SeedSampleData = true)
        {
            using (var context = new HaverContext(
                serviceProvider.GetRequiredService<DbContextOptions<HaverContext>>()))
            {
                #region Database Prep
                try
                {
                    
                  
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.GetBaseException().Message);
                }
                #endregion


                #region Seed Required Data


                #endregion
            }//Using Close

        }//Initialize close

    }//HaverInitializer Class close.
}//Namespace close
