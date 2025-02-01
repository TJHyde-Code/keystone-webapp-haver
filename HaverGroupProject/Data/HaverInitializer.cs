using Microsoft.EntityFrameworkCore;
using HaverGroupProject.Models;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.Extensions.Hosting;

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

                #region Prepare the Database
                if (DeleteDatabase || !context.Database.CanConnect())
                {
                    context.Database.EnsureDeleted(); //Delete the existing version 
                    if (UseMigrations)
                    {
                        context.Database.Migrate(); //Create the Database and apply all migrations
                    }
                    else
                    {
                        context.Database.EnsureCreated(); //Create and update the database as per the Model
                    }
                }

                #endregion
                #region Seed Data
                //Seed Data goes here
                //Look for Customers and Vendors first, then SalesOrders
                try
                {
                    if (!context.Customers.Any())
                    {
                        context.Customers.AddRange(
                           new Customer
                           {
                               CustomerName = "Connor Company",
                               ReleaseDate = DateTime.UtcNow,
                               CustomerAddress = "404 Spruce Court",
                               CustomerContactName = "Emma Johnson",
                               CustomerEmail = "EJohnson@hotmail.com",
                               CustomerPhone = "5197890777"
                           },
                            new Customer
                            {
                                CustomerName = "Potato Shakers",
                                ReleaseDate = DateTime.UtcNow,
                                CustomerAddress = "404 Knotfound Rd.",
                                CustomerContactName = "Jesse Lopez",
                                CustomerEmail = "JLopez@hotmail.com",
                                CustomerPhone = "5197590747"
                            },
                            new Customer
                            {
                                CustomerName = "Centenial Grinding",
                                ReleaseDate = DateTime.UtcNow,
                                CustomerAddress = "1708 Pickard Rd",
                                CustomerContactName = "Jean Luc",
                                CustomerEmail = "JLuc@hotmail.com",
                                CustomerPhone = "5195890677"
                            },
                            new Customer
                            {
                                CustomerName = "Honest Jays",
                                ReleaseDate = DateTime.UtcNow,
                                CustomerAddress = "14 Palmer Crescent",
                                CustomerContactName = "Mario Mario",
                                CustomerEmail = "MMario@hotmail.com",
                                CustomerPhone = "5397830777"
                            }, new Customer
                            {
                                CustomerName = "Masher McMash",
                                ReleaseDate = DateTime.UtcNow,
                                CustomerAddress = "20 Crush St",
                                CustomerContactName = "Emma Johnson",
                                CustomerEmail = "EJohnson@hotmail.com",
                                CustomerPhone = "5197890777"
                            },
                            new Customer
                            {
                                CustomerName = "We Dig Holes",
                                ReleaseDate = DateTime.UtcNow,
                                CustomerAddress = "1952 Sunken Blvrd",
                                CustomerContactName = "Doug Dig",
                                CustomerEmail = "DDig@hotmail.com",
                                CustomerPhone = "5192890577"
                            },
                            new Customer
                            {
                                CustomerName = "Sifting Made Easy",
                                ReleaseDate = DateTime.UtcNow,
                                CustomerAddress = "456 Swish Street",
                                CustomerContactName = "Allan Swazze",
                                CustomerEmail = "ASwayze@hotmail.com",
                                CustomerPhone = "5137890677"
                            },
                            new Customer
                            {
                                CustomerName = "Pinnacle Pellet",
                                ReleaseDate = DateTime.UtcNow,
                                CustomerAddress = "22 Highland",
                                CustomerContactName = "Janet Jones",
                                CustomerEmail = "JJones@hotmail.com",
                                CustomerPhone = "5137890757"
                            },
                            new Customer
                            {
                                CustomerName = "Farland Outfitters",
                                ReleaseDate = DateTime.UtcNow,
                                CustomerAddress = "Fairway Crescent",
                                CustomerContactName = "Peter Montabelo",
                                CustomerEmail = "PMontabelo@hotmail.com",
                                CustomerPhone = "5167890775"
                            },
                            new Customer
                            {
                                CustomerName = "Windsor Contracting",
                                ReleaseDate = DateTime.UtcNow,
                                CustomerAddress = "7765 Blower Street",
                                CustomerContactName = "Hudson James",
                                CustomerEmail = "HJames@hotmail.com",
                                CustomerPhone = "5197860707"
                            }
                        );
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                }

                //Seed data for Vendors
                try
                {
                    if (!context.Vendors.Any())
                    {
                        context.Vendors.AddRange(
                         new Vendor
                         {
                             VendorName = "Thompson Machining",
                             VendorAddress = "177 Eastmarch",
                             VendorContactName = "Daniel Baker",
                             VendorPhone = "5328659874",
                             VendorEmail = "DBaker@gmail.com",
                         },
                         new Vendor
                         {
                             VendorName = "MasterCraft Machining",
                             VendorAddress = "1717 Walnut Terrace",
                             VendorContactName = "James Winston",
                             VendorPhone = "5768659874",
                             VendorEmail = "JWinston@gmail.com",
                         },
                         new Vendor
                         {
                             VendorName = "Jameson Parts",
                             VendorAddress = "7680 Norfolk",
                             VendorContactName = "John Trakt",
                             VendorPhone = "5368659674",
                             VendorEmail = "JTrakt@gmail.com",
                         },
                         new Vendor
                         {
                             VendorName = "Wilfred Machining",
                             VendorAddress = "8814 Picton Place",
                             VendorContactName = "Jeremy Armstrong",
                             VendorPhone = "5328659574",
                             VendorEmail = "JArmstrong@gmail.com",
                         },
                         new Vendor
                         {
                             VendorName = "Hudson Metal",
                             VendorAddress = "9980 Tolley Rd",
                             VendorContactName = "Tullulah Wayne",
                             VendorPhone = "5328669874",
                             VendorEmail = "TWayne.com",
                         },
                         new Vendor
                         {
                             VendorName = "Niagara Metalworks",
                             VendorAddress = "7789 Petulli",
                             VendorContactName = "Willow Morgan",
                             VendorPhone = "5328659575",
                             VendorEmail = "WMorgan@gmail.com",
                         },
                         new Vendor
                         {
                             VendorName = "Hamilton Industrial",
                             VendorAddress = "7786 Wallaby",
                             VendorContactName = "Austin Smith",
                             VendorPhone = "5323659876",
                             VendorEmail = "ASmith@gmail.com",
                         },
                         new Vendor
                         {
                             VendorName = "Forward Machining",
                             VendorAddress = "8876 Albany Place",
                             VendorContactName = "David Crocket",
                             VendorPhone = "53286598867",
                             VendorEmail = "DCrocket@gmail.com",
                         },
                         new Vendor
                         {
                             VendorName = "Princeton Processing",
                             VendorAddress = "9965 Paley Ave",
                             VendorContactName = "Chris Hall",
                             VendorPhone = "5328459874",
                             VendorEmail = "CHall@gmail.com",
                         },
                         new Vendor
                         {
                             VendorName = "Kraft Machining",
                             VendorAddress = "1452 Charleston",
                             VendorContactName = "Keegan Samuel",
                             VendorPhone = "5328657674",
                             VendorEmail = "KSamuel@gmail.com",
                         }
                         );
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                }

                //Seed Data for MachineDescription Table
                try
                {
                    if (!context.MachineDescriptions.Any())
                    {
                        context.MachineDescriptions.AddRange(
                            new MachineDescription
                            {
                                SerialNumber = "SN123456",
                                Size = "6'x20'",
                                Class = "T-330",
                                Deck = "1D",
                                NamePlateOrdered = true,
                                NameplateRecieved = false,
                                InstalledMedia = true,
                                SparePartsSpareMedia = false,
                                BaseFrame = true,
                                AirSeal = false,
                                CoatingLining = true,
                                Disassembly = true
                            }
                        );
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding machine descriptions: {ex.Message}");
                }



                //Seed Data for Engineer Table
                try
                {
                    if (!context.Engineers.Any())
                    {
                        context.Engineers.AddRange(
                            new Engineer
                            {
                                EngFirstName = "John",
                                EngMiddleName = "A",
                                EngLastName = "Doe",
                                EngPhone = "1234567890",
                                EngEmail = "johndoe@example.com"
                            }
                        );
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while checking or seeding engineers: {ex.Message}");
                }

                //Seed data for Operations Schedule
                try
                {
                    if (!context.OperationsSchedules.Any())
                    {
                        context.OperationsSchedules.AddRange(
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465896,
                                ExtSalesOrdNum = 3934999,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Connor Company").ID,
                                MachineDesc = "T-330 6'x20-1D",
                                SerialNum = "SN123456",
                                PackageReleaseName = "Package Alpha",
                                KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                PONum = "4500798688",
                                ProductionOrderNumber = "PO987654",
                                PODueDate = DateOnly.Parse("2024-12-03"),
                                DeliveryDate = DateOnly.Parse("2024-12-14"),
                                //EngineerID = 1,
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465897,
                                ExtSalesOrdNum = 3935000,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Connor Company").ID,
                                MachineDesc = "T-330 6'x20-1D",
                                SerialNum = "SN123456",
                                PackageReleaseName = "Package Alpha",
                                KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Princeton Processing").ID,
                                PONum = "4500798689",
                                ProductionOrderNumber = "PO987655",
                                PODueDate = DateOnly.Parse("2024-12-03"),
                                DeliveryDate = DateOnly.Parse("2024-12-14"),
                                //EngineerID = 1,
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465898,
                                ExtSalesOrdNum = 3935001,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Connor Company").ID,
                                MachineDesc = "T-330 6'x20-1D",
                                SerialNum = "SN123456",
                                PackageReleaseName = "Package Alpha",
                                KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                PONum = "4500798689",
                                ProductionOrderNumber = "PO987656",
                                PODueDate = DateOnly.Parse("2024-12-03"),
                                DeliveryDate = DateOnly.Parse("2024-12-14"),
                                //EngineerID = 1,
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465899,
                                ExtSalesOrdNum = 3935002,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Potato Shakers").ID,
                                MachineDesc = "T-330 6'x20-1D",
                                SerialNum = "SN123456",
                                PackageReleaseName = "Package Alpha",
                                KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Princeton Processing").ID,
                                PONum = "4500798690",
                                ProductionOrderNumber = "PO987657",
                                PODueDate = DateOnly.Parse("2024-12-03"),
                                DeliveryDate = DateOnly.Parse("2024-12-14"),
                                //EngineerID = 1,
                                //NoteID = 1
                            },
                             new OperationsSchedule
                             {
                                 SalesOrdNum = 10465900,
                                 ExtSalesOrdNum = 3935003,
                                 CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Potato Shakers").ID,
                                 MachineDesc = "T-330 6'x20-1D",
                                 SerialNum = "SN123457",
                                 PackageReleaseName = "Package Beta",
                                 KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                 ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                 VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Forward Machining").ID,
                                 PONum = "4500798691",
                                 ProductionOrderNumber = "PO987658",
                                 PODueDate = DateOnly.Parse("2024-12-03"),
                                 DeliveryDate = DateOnly.Parse("2024-12-14"),
                                 //EngineerID = 1,
                                 //NoteID = 1
                             },
                              new OperationsSchedule
                              {
                                  SalesOrdNum = 10465901,
                                  ExtSalesOrdNum = 3935004,
                                  CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Potato Shakers").ID,
                                  MachineDesc = "T-330 6'x20-1D",
                                  SerialNum = "SN123457",
                                  PackageReleaseName = "Package Beta",
                                  KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                  ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                  VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hamilton Industrial").ID,
                                  PONum = "4500798692",
                                  ProductionOrderNumber = "PO987659",
                                  PODueDate = DateOnly.Parse("2024-12-03"),
                                  DeliveryDate = DateOnly.Parse("2024-12-14"),
                                  //EngineerID = 1,
                                  //NoteID = 1
                              },
                              new OperationsSchedule
                              {
                                  SalesOrdNum = 10465902,
                                  ExtSalesOrdNum = 3935005,
                                  CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Centenial Grinding").ID,
                                  MachineDesc = "T-330 6'x20-1D",
                                  SerialNum = "SN123457",
                                  PackageReleaseName = "Package Gamma",
                                  KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                  ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                  VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Niagara Metalworks").ID,
                                  PONum = "4500798693",
                                  ProductionOrderNumber = "PO987660",
                                  PODueDate = DateOnly.Parse("2024-12-03"),
                                  DeliveryDate = DateOnly.Parse("2024-12-14"),
                                  //EngineerID = 1,
                                  //NoteID = 1
                              },
                               new OperationsSchedule
                               {
                                   SalesOrdNum = 10465903,
                                   ExtSalesOrdNum = 3935006,
                                   CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Centenial Grinding").ID,
                                   MachineDesc = "T-330 6'x20-1D",
                                   SerialNum = "SN123457",
                                   PackageReleaseName = "Package Gamma",
                                   KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                   ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                   VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                   PONum = "4500798694",
                                   ProductionOrderNumber = "PO987661",
                                   PODueDate = DateOnly.Parse("2024-12-03"),
                                   DeliveryDate = DateOnly.Parse("2024-12-14"),
                                   //EngineerID = 1,
                                   //NoteID = 1
                               },
                               new OperationsSchedule
                               {
                                   SalesOrdNum = 10465904,
                                   ExtSalesOrdNum = 3935007,
                                   CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Centenial Grinding").ID,
                                   MachineDesc = "T-330 6'x20-1D",
                                   SerialNum = "SN123786",
                                   PackageReleaseName = "Package Alpha",
                                   KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                   ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                   VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                   PONum = "4500798695",
                                   ProductionOrderNumber = "PO987662",
                                   PODueDate = DateOnly.Parse("2024-12-03"),
                                   DeliveryDate = DateOnly.Parse("2024-12-14"),
                                   //EngineerID = 1,
                                   //NoteID = 1
                               },
                               new OperationsSchedule
                               {
                                   SalesOrdNum = 10465905,
                                   ExtSalesOrdNum = 3935008,
                                   CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Honest Jays").ID,
                                   MachineDesc = "T-330 6'x20-1D",
                                   SerialNum = "SN123786",
                                   PackageReleaseName = "Package Beta",
                                   KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                   ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                   VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                   PONum = "4500798696",
                                   ProductionOrderNumber = "PO987663",
                                   PODueDate = DateOnly.Parse("2024-12-03"),
                                   DeliveryDate = DateOnly.Parse("2024-12-14"),
                                   //EngineerID = 1,
                                   //NoteID = 1
                               },
                               new OperationsSchedule
                               {
                                   SalesOrdNum = 10465906,
                                   ExtSalesOrdNum = 3935009,
                                   CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Honest Jays").ID,
                                   MachineDesc = "T-330 6'x20-1D",
                                   SerialNum = "SN123786",
                                   PackageReleaseName = "Package Beta",
                                   KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                   ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                   VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Wilfred Machining").ID,
                                   PONum = "4500798697",
                                   ProductionOrderNumber = "PO987664",
                                   PODueDate = DateOnly.Parse("2024-12-03"),
                                   DeliveryDate = DateOnly.Parse("2024-12-14"),
                                   //EngineerID = 1,
                                   //NoteID = 1
                               },
                                new OperationsSchedule
                                {
                                    SalesOrdNum = 10465908,
                                    ExtSalesOrdNum = 3935010,
                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Honest Jays").ID,
                                    MachineDesc = "T-330 6'x20-1D",
                                    SerialNum = "SN123786",
                                    PackageReleaseName = "Package Beta",
                                    KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                    ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                    PONum = "4500798699",
                                    ProductionOrderNumber = "PO987666",
                                    PODueDate = DateOnly.Parse("2024-12-03"),
                                    DeliveryDate = DateOnly.Parse("2024-12-14"),
                                    //EngineerID = 1,
                                    //NoteID = 1
                                },
                                new OperationsSchedule
                                {
                                    SalesOrdNum = 10465909,
                                    ExtSalesOrdNum = 3935011,
                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Masher McMash").ID,
                                    MachineDesc = "T-330 6'x20-1D",
                                    SerialNum = "SN123786",
                                    PackageReleaseName = "Package Beta",
                                    KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                    ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Forward Machining").ID,
                                    PONum = "4500798700",
                                    ProductionOrderNumber = "PO987667",
                                    PODueDate = DateOnly.Parse("2024-12-03"),
                                    DeliveryDate = DateOnly.Parse("2024-12-14"),
                                    //EngineerID = 1,
                                    //NoteID = 1
                                },
                                 new OperationsSchedule
                                 {
                                     SalesOrdNum = 10465910,
                                     ExtSalesOrdNum = 3935012,
                                     CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Masher McMash").ID,
                                     MachineDesc = "T-330 6'x20-1D",
                                     SerialNum = "SN123797",
                                     PackageReleaseName = "Package Beta",
                                     KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                     ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                     VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                     PONum = "4500798701",
                                     ProductionOrderNumber = "PO987668",
                                     PODueDate = DateOnly.Parse("2024-12-03"),
                                     DeliveryDate = DateOnly.Parse("2024-12-14"),
                                     //EngineerID = 1,
                                     //NoteID = 1
                                 },
                                 new OperationsSchedule
                                 {
                                     SalesOrdNum = 10465911,
                                     ExtSalesOrdNum = 3935013,
                                     CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Masher McMash").ID,
                                     MachineDesc = "T-330 6'x20-1D",
                                     SerialNum = "SN127497",
                                     PackageReleaseName = "Package Alpha",
                                     KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                     ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                     VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Princeton Processing").ID,
                                     PONum = "4500798702",
                                     ProductionOrderNumber = "PO987668",
                                     PODueDate = DateOnly.Parse("2024-12-03"),
                                     DeliveryDate = DateOnly.Parse("2024-12-14"),
                                     //EngineerID = 1,
                                     //NoteID = 1
                                 },
                                  new OperationsSchedule
                                  {
                                      SalesOrdNum = 10465912,
                                      ExtSalesOrdNum = 3935014,
                                      CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "We Dig Holes").ID,
                                      MachineDesc = "T-330 6'x20-1D",
                                      SerialNum = "SN127497",
                                      PackageReleaseName = "Package Alpha",
                                      KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                      ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                      VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Wilfred Machining").ID,
                                      PONum = "4500798703",
                                      ProductionOrderNumber = "PO987668",
                                      PODueDate = DateOnly.Parse("2024-12-03"),
                                      DeliveryDate = DateOnly.Parse("2024-12-14"),
                                      //EngineerID = 1,
                                      //NoteID = 1
                                  },
                                  new OperationsSchedule
                                  {
                                      SalesOrdNum = 10465913,
                                      ExtSalesOrdNum = 3935015,
                                      CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "We Dig Holes").ID,
                                      MachineDesc = "T-330 6'x20-1D",
                                      SerialNum = "SN127497",
                                      PackageReleaseName = "Package Alpha",
                                      KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                      ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                      VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Jameson Parts").ID,
                                      PONum = "4500798704",
                                      ProductionOrderNumber = "PO987669",
                                      PODueDate = DateOnly.Parse("2024-12-03"),
                                      DeliveryDate = DateOnly.Parse("2024-12-14"),
                                      //EngineerID = 1,
                                      //NoteID = 1
                                  },
                                   new OperationsSchedule
                                   {
                                       SalesOrdNum = 10465914,
                                       ExtSalesOrdNum = 3935016,
                                       CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "We Dig Holes").ID,
                                       MachineDesc = "T-330 6'x20-1D",
                                       SerialNum = "SN127497",
                                       PackageReleaseName = "Package Zeta",
                                       KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                       ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                       VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                       PONum = "4500798705",
                                       ProductionOrderNumber = "PO987670",
                                       PODueDate = DateOnly.Parse("2024-12-03"),
                                       DeliveryDate = DateOnly.Parse("2024-12-14"),
                                       //EngineerID = 1,
                                       //NoteID = 1
                                   },
                                    new OperationsSchedule
                                    {
                                        SalesOrdNum = 10465915,
                                        ExtSalesOrdNum = 3935017,
                                        CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Sifting Made Easy").ID,
                                        MachineDesc = "T-330 6'x20-1D",
                                        SerialNum = "SN127498",
                                        PackageReleaseName = "Package Zeta",
                                        KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                        ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                        VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                        PONum = "4500798706",
                                        ProductionOrderNumber = "PO987671",
                                        PODueDate = DateOnly.Parse("2024-12-03"),
                                        DeliveryDate = DateOnly.Parse("2024-12-14"),
                                        //EngineerID = 1,
                                        //NoteID = 1
                                    },
                                    new OperationsSchedule
                                    {
                                        SalesOrdNum = 10465916,
                                        ExtSalesOrdNum = 3935018,
                                        CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Sifting Made Easy").ID,
                                        MachineDesc = "T-330 6'x20-1D",
                                        SerialNum = "SN127498",
                                        PackageReleaseName = "Package Zeta",
                                        KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                        ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                        VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                        PONum = "4500798707",
                                        ProductionOrderNumber = "PO987672",
                                        PODueDate = DateOnly.Parse("2024-12-03"),
                                        DeliveryDate = DateOnly.Parse("2024-12-14"),
                                        //EngineerID = 1,
                                        //NoteID = 1
                                    },
                                    new OperationsSchedule
                                    {
                                        SalesOrdNum = 10465917,
                                        ExtSalesOrdNum = 3935019,
                                        CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Sifting Made Easy").ID,
                                        MachineDesc = "T-330 6'x20-1D",
                                        SerialNum = "SN127498",
                                        PackageReleaseName = "Package Zeta",
                                        KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                        ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                        VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                        PONum = "4500798708",
                                        ProductionOrderNumber = "PO987673",
                                        PODueDate = DateOnly.Parse("2024-12-03"),
                                        DeliveryDate = DateOnly.Parse("2024-12-14"),
                                        //EngineerID = 1,
                                        //NoteID = 1
                                    },
                                    new OperationsSchedule
                                    {
                                        SalesOrdNum = 10465918,
                                        ExtSalesOrdNum = 3935020,
                                        CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Pinnacle Pellet").ID,
                                        MachineDesc = "T-330 6'x20-1D",
                                        SerialNum = "SN127498",
                                        PackageReleaseName = "Package Zeta",
                                        KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                        ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                        VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                        PONum = "4500798709",
                                        ProductionOrderNumber = "PO987674",
                                        PODueDate = DateOnly.Parse("2024-12-03"),
                                        DeliveryDate = DateOnly.Parse("2024-12-14"),
                                        //EngineerID = 1,
                                        //NoteID = 1
                                    },
                                    new OperationsSchedule
                                    {
                                        SalesOrdNum = 10465919,
                                        ExtSalesOrdNum = 3935021,
                                        CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Pinnacle Pellet").ID,
                                        MachineDesc = "T-330 6'x20-1D",
                                        SerialNum = "SN127498",
                                        PackageReleaseName = "Package John",
                                        KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                        ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                        VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                        PONum = "4500798710",
                                        ProductionOrderNumber = "PO987676",
                                        PODueDate = DateOnly.Parse("2024-12-03"),
                                        DeliveryDate = DateOnly.Parse("2024-12-14"),
                                        //EngineerID = 1,
                                        //NoteID = 1
                                    },
                                    new OperationsSchedule
                                    {
                                        SalesOrdNum = 10465920,
                                        ExtSalesOrdNum = 3935022,
                                        CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Pinnacle Pellet").ID,
                                        MachineDesc = "T-330 6'x20-1D",
                                        SerialNum = "SN127498",
                                        PackageReleaseName = "Package James",
                                        KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                        ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                        VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Wilfred Machining").ID,
                                        PONum = "4500798711",
                                        ProductionOrderNumber = "PO987677",
                                        PODueDate = DateOnly.Parse("2024-12-03"),
                                        DeliveryDate = DateOnly.Parse("2024-12-14"),
                                        //EngineerID = 1,
                                        //NoteID = 1
                                    },
                                    new OperationsSchedule
                                    {
                                        SalesOrdNum = 10465921,
                                        ExtSalesOrdNum = 3935023,
                                        CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Farland Outfitters").ID,
                                        MachineDesc = "T-330 6'x20-1D",
                                        SerialNum = "SN127498",
                                        PackageReleaseName = "Package Collin",
                                        KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                        ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                        VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                        PONum = "4500798712",
                                        ProductionOrderNumber = "PO987678",
                                        PODueDate = DateOnly.Parse("2024-12-03"),
                                        DeliveryDate = DateOnly.Parse("2024-12-14"),
                                        //EngineerID = 1,
                                        //NoteID = 1
                                    },
                                    new OperationsSchedule
                                    {
                                        SalesOrdNum = 10465922,
                                        ExtSalesOrdNum = 3935024,
                                        CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Farland Outfitters").ID,
                                        MachineDesc = "T-330 6'x20-1D",
                                        SerialNum = "SN156498",
                                        PackageReleaseName = "Package Holly",
                                        KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                        ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                        VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                        PONum = "4500798713",
                                        ProductionOrderNumber = "PO987679",
                                        PODueDate = DateOnly.Parse("2024-12-03"),
                                        DeliveryDate = DateOnly.Parse("2024-12-14"),
                                        //EngineerID = 1,
                                        //NoteID = 1
                                    },
                                    new OperationsSchedule
                                    {
                                        SalesOrdNum = 10465923,
                                        ExtSalesOrdNum = 3935025,
                                        CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Farland Outfitters").ID,
                                        MachineDesc = "T-330 6'x20-1D",
                                        SerialNum = "SN156499",
                                        PackageReleaseName = "Package Holly",
                                        KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                        ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                        VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Niagara Metalworks").ID,
                                        PONum = "4500798714",
                                        ProductionOrderNumber = "PO987680",
                                        PODueDate = DateOnly.Parse("2024-12-03"),
                                        DeliveryDate = DateOnly.Parse("2024-12-14"),
                                        //EngineerID = 1,
                                        //NoteID = 1
                                    },
                                     new OperationsSchedule
                                     {
                                         SalesOrdNum = 10465924,
                                         ExtSalesOrdNum = 3935026,
                                         CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Windsor Contracting").ID,
                                         MachineDesc = "T-330 6'x20-1D",
                                         SerialNum = "SN156499",
                                         PackageReleaseName = "Package Holly",
                                         KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                         ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                         VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                         PONum = "4500798715",
                                         ProductionOrderNumber = "PO987681",
                                         PODueDate = DateOnly.Parse("2024-12-03"),
                                         DeliveryDate = DateOnly.Parse("2024-12-14"),
                                         //EngineerID = 1,
                                         //NoteID = 1
                                     },
                                     new OperationsSchedule
                                     {
                                         SalesOrdNum = 10465925,
                                         ExtSalesOrdNum = 3935027,
                                         CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Windsor Contracting").ID,
                                         MachineDesc = "T-330 6'x20-1D",
                                         SerialNum = "SN178499",
                                         PackageReleaseName = "Package Holly",
                                         KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                         ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                         VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                         PONum = "4500798716",
                                         ProductionOrderNumber = "PO987682",
                                         PODueDate = DateOnly.Parse("2024-12-03"),
                                         DeliveryDate = DateOnly.Parse("2024-12-14"),
                                         //EngineerID = 1,
                                         //NoteID = 1
                                     },
                                     new OperationsSchedule
                                     {
                                         SalesOrdNum = 10465926,
                                         ExtSalesOrdNum = 3935028,
                                         CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Windsor Contracting").ID,
                                         MachineDesc = "T-330 6'x20-1D",
                                         SerialNum = "SN178455",
                                         PackageReleaseName = "Package Thomas",
                                         KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                         ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                         VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                         PONum = "4500798717",
                                         ProductionOrderNumber = "PO987682",
                                         PODueDate = DateOnly.Parse("2024-12-03"),
                                         DeliveryDate = DateOnly.Parse("2024-12-14"),
                                         //EngineerID = 1,
                                         //NoteID = 1
                                     }
                        );
                        context.SaveChanges();

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                }

                //Seed Data for the Notes Table
                try
                {
                    if (!context.Notes.Any())
                    {
                        context.Notes.AddRange(
                            new Note
                            {
                                PreOrder = "Initial pre-order details.",
                                Scope = "Standard scope of work.",
                                BudgetAssembHrs = "120",
                                ActualAssembHours = 100,
                                ActualReworkHours = 10,
                                OtherComments = "All assembly tasks completed with minor rework.",
                            }
                        );
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding notes: {ex.Message}");
                }


                //Seed Data for OperationsScheduleVendor M:M
                try
                {
                    if (!context.OperationsScheduleVendors.Any())
                    {
                        context.OperationsScheduleVendors.AddRange(
                            new OperationsScheduleVendor
                            {
                                OperationsScheduleID = 1,
                                VendorID = 1
                            }
                        );
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding operations schedule vendors: {ex.Message}");
                }


                #endregion




            }//Using Close

        }//Initialize close

    }//HaverInitializer Class close.

}//Namespace close
