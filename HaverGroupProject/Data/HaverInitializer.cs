using Microsoft.EntityFrameworkCore;
using HaverGroupProject.Models;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;

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
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123457",
                                Size = "6'x22'",
                                Class = "T-350",
                                Deck = "2D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123458",
                                Size = "8'x24'",
                                Class = "T-440",
                                Deck = "3D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123459",
                                Size = "6'x20'",
                                Class = "T-330",
                                Deck = "1D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123460",
                                Size = "6'x30'",
                                Class = "T-550",
                                Deck = "4D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123461",
                                Size = "7'x22'",
                                Class = "T-370",
                                Deck = "2D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123462",
                                Size = "6'x20'",
                                Class = "T-400",
                                Deck = "3D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123463",
                                Size = "6'x18'",
                                Class = "T-320",
                                Deck = "1D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123464",
                                Size = "7'x25'",
                                Class = "T-500",
                                Deck = "2D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123465",
                                Size = "8'x28'",
                                Class = "T-430",
                                Deck = "3D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123466",
                                Size = "6'x22'",
                                Class = "T-330",
                                Deck = "1D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123467",
                                Size = "6'x20'",
                                Class = "T-390",
                                Deck = "4D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123468",
                                Size = "7'x23'",
                                Class = "T-350",
                                Deck = "2D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123469",
                                Size = "6'x24'",
                                Class = "T-420",
                                Deck = "1D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123470",
                                Size = "6'x20'",
                                Class = "T-400",
                                Deck = "3D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123471",
                                Size = "8'x26'",
                                Class = "T-380",
                                Deck = "2D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123472",
                                Size = "7'x30'",
                                Class = "T-550",
                                Deck = "4D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123473",
                                Size = "6'x28'",
                                Class = "T-370",
                                Deck = "1D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123474",
                                Size = "6'x25'",
                                Class = "T-310",
                                Deck = "3D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123475",
                                Size = "7'x22'",
                                Class = "T-330",
                                Deck = "2D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123476",
                                Size = "6'x20'",
                                Class = "T-450",
                                Deck = "4D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123477",
                                Size = "6'x26'",
                                Class = "T-400",
                                Deck = "1D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123478",
                                Size = "8'x30'",
                                Class = "T-370",
                                Deck = "3D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123479",
                                Size = "7'x20'",
                                Class = "T-500",
                                Deck = "2D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                            new MachineDescription
                            {
                                SerialNumber = "SN123480",
                                Size = "6'x22'",
                                Class = "T-330",
                                Deck = "4D",
                                NamePlateOrdered = false,
                                NameplateRecieved = false,
                                InstalledMedia = false,
                                SparePartsSpareMedia = false,
                                BaseFrame = false,
                                AirSeal = false,
                                CoatingLining = false,
                                Disassembly = false
                            },
                             new MachineDescription
                             {
                                 SerialNumber = "SN123481",
                                 Size = "6'x22'",
                                 Class = "T-330",
                                 Deck = "4D",
                                 NamePlateOrdered = false,
                                 NameplateRecieved = false,
                                 InstalledMedia = false,
                                 SparePartsSpareMedia = false,
                                 BaseFrame = false,
                                 AirSeal = false,
                                 CoatingLining = false,
                                 Disassembly = false
                             },
                              new MachineDescription
                              {
                                  SerialNumber = "SN123482",
                                  Size = "6'x22'",
                                  Class = "T-330",
                                  Deck = "4D",
                                  NamePlateOrdered = false,
                                  NameplateRecieved = false,
                                  InstalledMedia = false,
                                  SparePartsSpareMedia = false,
                                  BaseFrame = false,
                                  AirSeal = false,
                                  CoatingLining = false,
                                  Disassembly = false
                              },
                               new MachineDescription
                               {
                                   SerialNumber = "SN123483",
                                   Size = "6'x22'",
                                   Class = "T-330",
                                   Deck = "4D",
                                   NamePlateOrdered = false,
                                   NameplateRecieved = false,
                                   InstalledMedia = false,
                                   SparePartsSpareMedia = false,
                                   BaseFrame = false,
                                   AirSeal = false,
                                   CoatingLining = false,
                                   Disassembly = false
                               },
                                new MachineDescription
                                {
                                    SerialNumber = "SN123484",
                                    Size = "6'x22'",
                                    Class = "T-330",
                                    Deck = "4D",
                                    NamePlateOrdered = false,
                                    NameplateRecieved = false,
                                    InstalledMedia = false,
                                    SparePartsSpareMedia = false,
                                    BaseFrame = false,
                                    AirSeal = false,
                                    CoatingLining = false,
                                    Disassembly = false
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
                            },
                            new Engineer
                            {
                                EngFirstName = "Mike",
                                EngMiddleName = "B",
                                EngLastName = "Jones",
                                EngPhone = "6541237890",
                                EngEmail = "mikejones@example.com"
                            },
                            new Engineer
                            {
                                EngFirstName = "Greg",
                                EngMiddleName = "D",
                                EngLastName = "Naismith",
                                EngPhone = "7538649317",
                                EngEmail = "gregnaismith@example.com"
                            },
                            new Engineer
                            {
                                EngFirstName = "Robert",
                                EngMiddleName = "P",
                                EngLastName = "Aquilini",
                                EngPhone = "9143652470",
                                EngEmail = "robaquilini@example.com"
                            },
                            new Engineer
                            {
                                EngFirstName = "Larry",
                                EngMiddleName = "T",
                                EngLastName = "Johnson",
                                EngPhone = "8527410963",
                                EngEmail = "larryjohnson@example.com"
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
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123456").ID,
                                //SerialNum = "SN123456",
                                PackageReleaseName = "Package Alpha",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                PONum = "4500798688",
                                ProductionOrderNumber = "PO987654",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465897,
                                ExtSalesOrdNum = 3935000,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Connor Company").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123457").ID,
                                //SerialNum = "SN123456",
                                PackageReleaseName = "Package Alpha",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Princeton Processing").ID,
                                PONum = "4500798689",
                                ProductionOrderNumber = "PO987655",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Larry" && d.EngLastName == "Johnson").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465898,
                                ExtSalesOrdNum = 3935001,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Connor Company").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123458").ID,
                                //SerialNum = "SN123456",
                                PackageReleaseName = "Package Alpha",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                PONum = "4500798689",
                                ProductionOrderNumber = "PO987656",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465899,
                                ExtSalesOrdNum = 3935002,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Potato Shakers").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123459").ID,
                                //SerialNum = "SN123456",
                                PackageReleaseName = "Package Alpha",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Princeton Processing").ID,
                                PONum = "4500798690",
                                ProductionOrderNumber = "PO987657",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465900,
                                ExtSalesOrdNum = 3935003,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Potato Shakers").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123460").ID,
                                //SerialNum = "SN123457",
                                PackageReleaseName = "Package Beta",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Forward Machining").ID,
                                PONum = "4500798691",
                                ProductionOrderNumber = "PO987658",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465901,
                                ExtSalesOrdNum = 3935004,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Potato Shakers").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123461").ID,
                                //SerialNum = "SN123457",
                                PackageReleaseName = "Package Beta",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hamilton Industrial").ID,
                                PONum = "4500798692",
                                ProductionOrderNumber = "PO987659",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465902,
                                ExtSalesOrdNum = 3935005,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Centenial Grinding").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123462").ID,
                                //SerialNum = "SN123457",
                                PackageReleaseName = "Package Gamma",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Niagara Metalworks").ID,
                                PONum = "4500798693",
                                ProductionOrderNumber = "PO987660",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465903,
                                ExtSalesOrdNum = 3935006,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Centenial Grinding").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123463").ID,
                                //SerialNum = "SN123457",
                                PackageReleaseName = "Package Gamma",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                PONum = "4500798694",
                                ProductionOrderNumber = "PO987661",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465904,
                                ExtSalesOrdNum = 3935007,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Centenial Grinding").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123464").ID,
                                //SerialNum = "SN123786",
                                PackageReleaseName = "Package Alpha",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                PONum = "4500798695",
                                ProductionOrderNumber = "PO987662",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465905,
                                ExtSalesOrdNum = 3935008,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Honest Jays").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123465").ID,
                                //SerialNum = "SN123786",
                                PackageReleaseName = "Package Beta",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                PONum = "4500798696",
                                ProductionOrderNumber = "PO987663",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465906,
                                ExtSalesOrdNum = 3935009,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Honest Jays").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123466").ID,
                                //SerialNum = "SN123786",
                                PackageReleaseName = "Package Beta",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Wilfred Machining").ID,
                                PONum = "4500798697",
                                ProductionOrderNumber = "PO987664",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465908,
                                ExtSalesOrdNum = 3935010,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Honest Jays").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123467").ID,
                                //SerialNum = "SN123786",
                                PackageReleaseName = "Package Beta",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                PONum = "4500798699",
                                ProductionOrderNumber = "PO987666",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Larry" && d.EngLastName == "Johnson").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465909,
                                ExtSalesOrdNum = 3935011,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Masher McMash").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123468").ID,
                                //SerialNum = "SN123786",
                                PackageReleaseName = "Package Beta",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Forward Machining").ID,
                                PONum = "4500798700",
                                ProductionOrderNumber = "PO987667",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465910,
                                ExtSalesOrdNum = 3935012,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Masher McMash").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123469").ID,
                                //SerialNum = "SN123797",
                                PackageReleaseName = "Package Beta",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                PONum = "4500798701",
                                ProductionOrderNumber = "PO987668",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465911,
                                ExtSalesOrdNum = 3935013,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Masher McMash").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123470").ID,
                                //SerialNum = "SN127497",
                                PackageReleaseName = "Package Alpha",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Princeton Processing").ID,
                                PONum = "4500798702",
                                ProductionOrderNumber = "PO987668",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465912,
                                ExtSalesOrdNum = 3935014,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "We Dig Holes").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123471").ID,
                                //SerialNum = "SN127497",
                                PackageReleaseName = "Package Alpha",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Wilfred Machining").ID,
                                PONum = "4500798703",
                                ProductionOrderNumber = "PO987668",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465913,
                                ExtSalesOrdNum = 3935015,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "We Dig Holes").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123471").ID,
                                //SerialNum = "SN127497",
                                PackageReleaseName = "Package Alpha",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Jameson Parts").ID,
                                PONum = "4500798704",
                                ProductionOrderNumber = "PO987669",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Larry" && d.EngLastName == "Johnson").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465914,
                                ExtSalesOrdNum = 3935016,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "We Dig Holes").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123472").ID,
                                //SerialNum = "SN127497",
                                PackageReleaseName = "Package Zeta",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                PONum = "4500798705",
                                ProductionOrderNumber = "PO987670",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465915,
                                ExtSalesOrdNum = 3935017,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Sifting Made Easy").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123473").ID,
                                //SerialNum = "SN127498",
                                PackageReleaseName = "Package Zeta",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                PONum = "4500798706",
                                ProductionOrderNumber = "PO987671",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465916,
                                ExtSalesOrdNum = 3935018,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Sifting Made Easy").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123474").ID,
                                //SerialNum = "SN127498",
                                PackageReleaseName = "Package Zeta",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                PONum = "4500798707",
                                ProductionOrderNumber = "PO987672",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465917,
                                ExtSalesOrdNum = 3935019,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Sifting Made Easy").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123475").ID,
                                //SerialNum = "SN127498",
                                PackageReleaseName = "Package Zeta",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                PONum = "4500798708",
                                ProductionOrderNumber = "PO987673",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Larry" && d.EngLastName == "Johnson").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465918,
                                ExtSalesOrdNum = 3935020,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Pinnacle Pellet").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123476").ID,
                                //SerialNum = "SN127498",
                                PackageReleaseName = "Package Zeta",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                PONum = "4500798709",
                                ProductionOrderNumber = "PO987674",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465919,
                                ExtSalesOrdNum = 3935021,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Pinnacle Pellet").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123477").ID,
                                //SerialNum = "SN127498",
                                PackageReleaseName = "Package John",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                PONum = "4500798710",
                                ProductionOrderNumber = "PO987676",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465920,
                                ExtSalesOrdNum = 3935022,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Pinnacle Pellet").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123478").ID,
                                //SerialNum = "SN127498",
                                PackageReleaseName = "Package James",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Wilfred Machining").ID,
                                PONum = "4500798711",
                                ProductionOrderNumber = "PO987677",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465921,
                                ExtSalesOrdNum = 3935023,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Farland Outfitters").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123479").ID,
                                //SerialNum = "SN127498",
                                PackageReleaseName = "Package Collin",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                PONum = "4500798712",
                                ProductionOrderNumber = "PO987678",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465922,
                                ExtSalesOrdNum = 3935024,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Farland Outfitters").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123480").ID,
                                //SerialNum = "SN156498",
                                PackageReleaseName = "Package Holly",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                PONum = "4500798713",
                                ProductionOrderNumber = "PO987679",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465923,
                                ExtSalesOrdNum = 3935025,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Farland Outfitters").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123481").ID,
                                //SerialNum = "SN156499",
                                PackageReleaseName = "Package Holly",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Niagara Metalworks").ID,
                                PONum = "4500798714",
                                ProductionOrderNumber = "PO987680",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Larry" && d.EngLastName == "Johnson").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465924,
                                ExtSalesOrdNum = 3935026,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Windsor Contracting").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123482").ID,
                                //SerialNum = "SN156499",
                                PackageReleaseName = "Package Holly",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                PONum = "4500798715",
                                ProductionOrderNumber = "PO987681",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465925,
                                ExtSalesOrdNum = 3935027,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Windsor Contracting").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123483").ID,
                                //SerialNum = "SN178499",
                                PackageReleaseName = "Package Holly",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                PONum = "4500798716",
                                ProductionOrderNumber = "PO987682",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID
                                //NoteID = 1
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465926,
                                ExtSalesOrdNum = 3935028,
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Windsor Contracting").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123484").ID,
                                //SerialNum = "SN178455",
                                PackageReleaseName = "Package Thomas",
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateTime.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                PONum = "4500798717",
                                ProductionOrderNumber = "PO987682",
                                PODueDate = DateTime.Parse("2024-12-03"),
                                DeliveryDate = DateTime.Parse("2024-12-14"),
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID
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
