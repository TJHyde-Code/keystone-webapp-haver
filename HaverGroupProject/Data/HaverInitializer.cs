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
                               CustomerAddress = "404 Spruce Court",
                               CustContactFirst = "Emma",
                               CustContactLastName = "Johnson",                               
                               CustomerEmail = "EJohnson@hotmail.com",
                               CustomerPhone = "5197890777"
                           },
                            new Customer
                            {
                                CustomerName = "Potato Shakers",
                                CustomerAddress = "404 Knotfound Rd.",                                
                                CustContactFirst = "Jesse",
                                CustContactLastName = "Lopez",
                                CustomerEmail = "JLopez@hotmail.com",
                                CustomerPhone = "5197590747"
                            },
                            new Customer
                            {
                                CustomerName = "Centenial Grinding",
                                CustomerAddress = "1708 Pickard Rd",                                
                                CustContactFirst = "Jean",
                                CustContactLastName = "Luc",
                                CustomerEmail = "JLuc@hotmail.com",
                                CustomerPhone = "5195890677"
                            },
                            new Customer
                            {
                                CustomerName = "Honest Jays",
                                CustomerAddress = "14 Palmer Crescent",
                                CustContactFirst = "Mario",
                                CustContactLastName = "Mario",
                                CustomerEmail = "MMario@hotmail.com",
                                CustomerPhone = "5397830777"
                            }, new Customer
                            {
                                CustomerName = "Masher McMash",
                                CustomerAddress = "20 Crush St",                                
                                CustContactFirst = "Emma",
                                CustContactLastName = "Johnson",
                                CustomerEmail = "EJohnson@hotmail.com",
                                CustomerPhone = "5197890777"
                            },
                            new Customer
                            {
                                CustomerName = "We Dig Holes",
                                CustomerAddress = "1952 Sunken Blvrd",
                                CustContactFirst = "Doug",
                                CustContactLastName = "Dig",
                                CustomerEmail = "DDig@hotmail.com",
                                CustomerPhone = "5192890577"
                            },
                            new Customer
                            {
                                CustomerName = "Sifting Made Easy",
                                CustomerAddress = "456 Swish Street",
                                CustContactFirst = "Allan",
                                CustContactLastName = "Swazze",
                                CustomerEmail = "ASwayze@hotmail.com",
                                CustomerPhone = "5137890677"
                            },
                            new Customer
                            {
                                CustomerName = "Pinnacle Pellet",
                                CustomerAddress = "22 Highland",
                                CustContactFirst = "Janet",
                                CustContactLastName = "Jones",
                                CustomerEmail = "JJones@hotmail.com",
                                CustomerPhone = "5137890757"
                            },
                            new Customer
                            {
                                CustomerName = "Farland Outfitters",
                                CustomerAddress = "Fairway Crescent",
                                CustContactFirst = "Peter",
                                CustContactLastName = "Montabelo",
                                CustomerEmail = "PMontabelo@hotmail.com",
                                CustomerPhone = "5167890775"
                            },
                            new Customer
                            {
                                CustomerName = "Windsor Contracting",
                                CustomerAddress = "7765 Blower Street",
                                CustContactFirst = "Hudson",
                                CustContactLastName = "James",
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

                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Connor Company").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123456").ID,
                                KickoffMeeting = DateTime.Parse("2024-11-11"),
                                ApprovalDrawingExpected = DateTime.Now.AddDays(7),
                                ApprovalDrawingReleased = DateTime.Now.AddDays(9),
                                ProgressApprovalDrawing = 70,
                                ApprovalDrawingReturned = DateTime.Now.AddDays(10),
                                PreOrderExpected = DateTime.Now.AddDays(12),
                                PreOrderReleased = DateTime.Now.AddDays(14),
                                ProgressPreOrder = 85,
                                EngineerPackageExpected = DateTime.Now.AddDays(16),
                                EngineerPackageReleased = DateTime.Now.AddDays(20),
                                ProgressEngineerPackage = 50,
                                PurchaseOrderExpected = DateTime.Now.AddDays(22),
                                PurchaseOrderDueDate = DateTime.Now.AddDays(28),
                                ProgressPurchaseOrder = 45,
                                PUrchaseOrderRecieved = DateTime.Now.AddDays(30),
                                ReadinessToShipExpected = DateTime.Now.AddDays(38),
                                ReadinessToShipActual = DateTime.Now.AddDays(40),
                                ProgressReadinesstoShip = 30,
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                PONum = "4500798688",
                                ProductionOrderNumber = "PO987654",
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID
                                //NoteID = 1
                            },
                             new OperationsSchedule
                             {
                                 SalesOrdNum = 10465897,
                                 CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Connor Company").ID,
                                 MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123457").ID,
                                 KickoffMeeting = DateTime.Parse("2024-11-11"),
                                 ApprovalDrawingExpected = DateTime.Now.AddDays(1),
                                 ApprovalDrawingReleased = DateTime.Now.AddDays(14),                                 
                                 ApprovalDrawingReturned = DateTime.Now.AddDays(16),
                                 PreOrderExpected = DateTime.Now.AddDays(18),
                                 PreOrderReleased = DateTime.Now.AddDays(20),                                 
                                 EngineerPackageExpected = DateTime.Now.AddDays(21),
                                 EngineerPackageReleased = DateTime.Now.AddDays(25),                                 
                                 PurchaseOrderExpected = DateTime.Now.AddDays(25),
                                 PurchaseOrderDueDate = DateTime.Now.AddDays(28),                                 
                                 PUrchaseOrderRecieved = DateTime.Now.AddDays(30),
                                 ReadinessToShipExpected = DateTime.Now.AddDays(38),
                                 ReadinessToShipActual = DateTime.Now.AddDays(45),                                 
                                 VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Princeton Processing").ID,
                                 PONum = "4500798689",
                                 ProductionOrderNumber = "PO987655",
                                 EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Larry" && d.EngLastName == "Johnson").ID
                                 //NoteID = 1
                             },
                        new OperationsSchedule
                        {
                            SalesOrdNum = 10465898,

                            CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Connor Company").ID,
                            MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123458").ID,

                            KickoffMeeting = DateTime.Parse("2024-11-11"),
                            ApprovalDrawingExpected = DateTime.Now.AddDays(2),
                            ApprovalDrawingReleased = DateTime.Now.AddDays(4),
                            ApprovalDrawingReturned = DateTime.Now.AddDays(10),
                            ProgressApprovalDrawing = 30,
                            PreOrderExpected = DateTime.Now.AddDays(5),
                            PreOrderReleased = DateTime.Now.AddDays(8),
                            ProgressPreOrder = 80,
                            EngineerPackageExpected = DateTime.Now.AddDays(9),
                            EngineerPackageReleased = DateTime.Now.AddDays(13),
                            ProgressEngineerPackage = 25
                        ,
                            PurchaseOrderExpected = DateTime.Now.AddDays(10),
                            PurchaseOrderDueDate = DateTime.Now.AddDays(13),
                            ProgressPurchaseOrder = 75,
                            PUrchaseOrderRecieved = DateTime.Now.AddDays(13),
                            ReadinessToShipExpected = DateTime.Now.AddDays(14),
                            ReadinessToShipActual = DateTime.Now.AddDays(22),
                            ProgressReadinesstoShip = 25,
                            VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                            PONum = "4500798689",
                            ProductionOrderNumber = "PO987656",

                            EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID
                            //NoteID = 1
                        },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465899,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Potato Shakers").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123459").ID,

                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(5),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(9),
                                                    ProgressApprovalDrawing = 100,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(10),
                                                    PreOrderExpected = DateTime.Now.AddDays(16),
                                                    PreOrderReleased = DateTime.Now.AddDays(20),
                                                    ProgressPreOrder = 40,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(25),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(30),
                                                    ProgressEngineerPackage = 60,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(31),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(33),
                                                    ProgressPurchaseOrder = 70,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(32),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(33),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(27),
                                                    ProgressReadinesstoShip = 40,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Princeton Processing").ID,
                                                    PONum = "4500798690",
                                                    ProductionOrderNumber = "PO987657",

                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465900,
                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Potato Shakers").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123460").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(1),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(5),
                                                    ProgressApprovalDrawing = 75,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(8),
                                                    PreOrderExpected = DateTime.Now.AddDays(9),
                                                    PreOrderReleased = DateTime.Now.AddDays(13),
                                                    ProgressPreOrder = 80,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(17),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(19),
                                                    ProgressEngineerPackage = 40,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(20),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(23),
                                                    ProgressPurchaseOrder = 20,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(25),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(30),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(31),
                                                    ProgressReadinesstoShip = 100,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Forward Machining").ID,
                                                    PONum = "4500798691",
                                                    ProductionOrderNumber = "PO987658",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465901,
                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Potato Shakers").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123461").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-15),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-13),
                                                    ProgressApprovalDrawing = 100,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(10),
                                                    PreOrderExpected = DateTime.Now.AddDays(-9),
                                                    PreOrderReleased = DateTime.Now.AddDays(-4),
                                                    ProgressPreOrder = 100,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(-1),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(3),
                                                    ProgressEngineerPackage = 90,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(4),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(8),
                                                    ProgressPurchaseOrder = 55,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(10),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(14),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(15),
                                                    ProgressReadinesstoShip = 25,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hamilton Industrial").ID,
                                                    PONum = "4500798692",
                                                    ProductionOrderNumber = "PO987659",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465902,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Centenial Grinding").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123462").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-20),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-15),
                                                    ProgressApprovalDrawing = 90,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-11),
                                                    PreOrderExpected = DateTime.Now.AddDays(-9),
                                                    PreOrderReleased = DateTime.Now.AddDays(-6),
                                                    ProgressPreOrder = 90,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(-4),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(-1),
                                                    ProgressEngineerPackage = 85,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(2),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(4),
                                                    ProgressPurchaseOrder = 75,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(6),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(10),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(15),
                                                    ProgressReadinesstoShip = 25,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Niagara Metalworks").ID,
                                                    PONum = "4500798693",
                                                    ProductionOrderNumber = "PO987660",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465903,
                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Centenial Grinding").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123463").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(1),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(3),
                                                    ProgressApprovalDrawing = 15,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(7),
                                                    PreOrderExpected = DateTime.Now.AddDays(9),
                                                    PreOrderReleased = DateTime.Now.AddDays(18),
                                                    ProgressPreOrder = 90,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(20),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(27),
                                                    ProgressEngineerPackage = 70,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(28),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(30),
                                                    ProgressPurchaseOrder = 80,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(31),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(33),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(35),
                                                    ProgressReadinesstoShip = 55,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                                    PONum = "4500798694",
                                                    ProductionOrderNumber = "PO987661",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465904,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Centenial Grinding").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123464").ID,

                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(5),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(7),
                                                    ProgressApprovalDrawing = 40,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(10),
                                                    PreOrderExpected = DateTime.Now.AddDays(12),
                                                    PreOrderReleased = DateTime.Now.AddDays(15),
                                                    ProgressPreOrder = 60,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(18),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(22),
                                                    ProgressEngineerPackage = 85,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(24),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(26),
                                                    ProgressPurchaseOrder = 75,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(28),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(30),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(32),
                                                    ProgressReadinesstoShip = 50,

                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                                    PONum = "4500798695",
                                                    ProductionOrderNumber = "PO987662",

                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465905,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Honest Jays").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123465").ID,

                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(5),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(7),
                                                    ProgressApprovalDrawing = 30,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(10),
                                                    PreOrderExpected = DateTime.Now.AddDays(12),
                                                    PreOrderReleased = DateTime.Now.AddDays(16),
                                                    ProgressPreOrder = 55,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(18),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(21),
                                                    ProgressEngineerPackage = 65,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(23),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(25),
                                                    ProgressPurchaseOrder = 70,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(27),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(29),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(31),
                                                    ProgressReadinesstoShip = 60,

                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                                    PONum = "4500798696",
                                                    ProductionOrderNumber = "PO987663",

                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465906,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Honest Jays").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123466").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-30),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-28),
                                                    ProgressApprovalDrawing = 25,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-25),
                                                    PreOrderExpected = DateTime.Now.AddDays(-22),
                                                    PreOrderReleased = DateTime.Now.AddDays(-18),
                                                    ProgressPreOrder = 50,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(-15),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(-12),
                                                    ProgressEngineerPackage = 80,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(-10),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(-7),
                                                    ProgressPurchaseOrder = 65,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(-5),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(-3),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(0),
                                                    ProgressReadinesstoShip = 40,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Wilfred Machining").ID,
                                                    PONum = "4500798697",
                                                    ProductionOrderNumber = "PO987664",

                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465908,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Honest Jays").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123467").ID,

                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-30),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-27),
                                                    ProgressApprovalDrawing = 35,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-22),
                                                    PreOrderExpected = DateTime.Now.AddDays(-19),
                                                    PreOrderReleased = DateTime.Now.AddDays(-15),
                                                    ProgressPreOrder = 50,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(-12),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(-10),
                                                    ProgressEngineerPackage = 60,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(-8),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(-5),
                                                    ProgressPurchaseOrder = 70,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(-3),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(-1),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(2),
                                                    ProgressReadinesstoShip = 50,

                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                                    PONum = "4500798699",
                                                    ProductionOrderNumber = "PO987666",

                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Larry" && d.EngLastName == "Johnson").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465909,
                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Masher McMash").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123468").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-25),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-22),
                                                    ProgressApprovalDrawing = 45,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-18),
                                                    PreOrderExpected = DateTime.Now.AddDays(-15),
                                                    PreOrderReleased = DateTime.Now.AddDays(-10),
                                                    ProgressPreOrder = 60,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(-7),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(-3),
                                                    ProgressEngineerPackage = 70,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(-1),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(2),
                                                    ProgressPurchaseOrder = 80,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(4),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(6),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(8),
                                                    ProgressReadinesstoShip = 50,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Forward Machining").ID,
                                                    PONum = "4500798700",
                                                    ProductionOrderNumber = "PO987667",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465910,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Masher McMash").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123469").ID,

                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-28),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-25),
                                                    ProgressApprovalDrawing = 40,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-20),
                                                    PreOrderExpected = DateTime.Now.AddDays(-17),
                                                    PreOrderReleased = DateTime.Now.AddDays(-12),
                                                    ProgressPreOrder = 55,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(-9),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(-6),
                                                    ProgressEngineerPackage = 75,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(-3),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(1),
                                                    ProgressPurchaseOrder = 80,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(3),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(5),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(7),
                                                    ProgressReadinesstoShip = 65,

                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                                    PONum = "4500798701",
                                                    ProductionOrderNumber = "PO987668",

                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465911,
                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Masher McMash").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123470").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-22),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-19),
                                                    ProgressApprovalDrawing = 35,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-14),
                                                    PreOrderExpected = DateTime.Now.AddDays(-11),
                                                    PreOrderReleased = DateTime.Now.AddDays(-6),
                                                    ProgressPreOrder = 50,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(-3),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(0),
                                                    ProgressEngineerPackage = 70,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(2),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(5),
                                                    ProgressPurchaseOrder = 80,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(8),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(10),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(12),
                                                    ProgressReadinesstoShip = 65,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Princeton Processing").ID,
                                                    PONum = "4500798702",
                                                    ProductionOrderNumber = "PO987668",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465912,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "We Dig Holes").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123471").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-18),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-15),
                                                    ProgressApprovalDrawing = 50,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-12),
                                                    PreOrderExpected = DateTime.Now.AddDays(-9),
                                                    PreOrderReleased = DateTime.Now.AddDays(-4),
                                                    ProgressPreOrder = 60,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(-1),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(3),
                                                    ProgressEngineerPackage = 70,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(5),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(8),
                                                    ProgressPurchaseOrder = 80,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(11),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(13),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(15),
                                                    ProgressReadinesstoShip = 65,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Wilfred Machining").ID,
                                                    PONum = "4500798703",
                                                    ProductionOrderNumber = "PO987668",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465913,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "We Dig Holes").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123471").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-25),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-20),
                                                    ProgressApprovalDrawing = 40,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-15),
                                                    PreOrderExpected = DateTime.Now.AddDays(-10),
                                                    PreOrderReleased = DateTime.Now.AddDays(-4),
                                                    ProgressPreOrder = 60,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(1),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(5),
                                                    ProgressEngineerPackage = 75,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(6),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(10),
                                                    ProgressPurchaseOrder = 85,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(13),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(15),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(17),
                                                    ProgressReadinesstoShip = 70,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Jameson Parts").ID,
                                                    PONum = "4500798704",
                                                    ProductionOrderNumber = "PO987669",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Larry" && d.EngLastName == "Johnson").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465914,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "We Dig Holes").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123472").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-30),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-25),
                                                    ProgressApprovalDrawing = 35,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-20),
                                                    PreOrderExpected = DateTime.Now.AddDays(-16),
                                                    PreOrderReleased = DateTime.Now.AddDays(-10),
                                                    ProgressPreOrder = 55,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(-5),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(0),
                                                    ProgressEngineerPackage = 65,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(2),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(6),
                                                    ProgressPurchaseOrder = 75,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(9),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(12),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(14),
                                                    ProgressReadinesstoShip = 80,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                                    PONum = "4500798705",
                                                    ProductionOrderNumber = "PO987670",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465915,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Sifting Made Easy").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123473").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-20),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-15),
                                                    ProgressApprovalDrawing = 50,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-10),
                                                    PreOrderExpected = DateTime.Now.AddDays(-5),
                                                    PreOrderReleased = DateTime.Now.AddDays(0),
                                                    ProgressPreOrder = 65,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(3),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(7),
                                                    ProgressEngineerPackage = 80,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(9),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(13),
                                                    ProgressPurchaseOrder = 90,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(14),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(16),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(18),
                                                    ProgressReadinesstoShip = 85,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                                    PONum = "4500798706",
                                                    ProductionOrderNumber = "PO987671",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465916,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Sifting Made Easy").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123474").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-25),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-20),
                                                    ProgressApprovalDrawing = 40,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-15),
                                                    PreOrderExpected = DateTime.Now.AddDays(-10),
                                                    PreOrderReleased = DateTime.Now.AddDays(-5),
                                                    ProgressPreOrder = 55,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(2),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(6),
                                                    ProgressEngineerPackage = 60,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(8),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(12),
                                                    ProgressPurchaseOrder = 85,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(13),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(15),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(17),
                                                    ProgressReadinesstoShip = 90,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                                    PONum = "4500798707",
                                                    ProductionOrderNumber = "PO987672",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465917,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Sifting Made Easy").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123475").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-15),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-10),
                                                    ProgressApprovalDrawing = 50,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-5),
                                                    PreOrderExpected = DateTime.Now.AddDays(1),
                                                    PreOrderReleased = DateTime.Now.AddDays(4),
                                                    ProgressPreOrder = 75,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(7),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(10),
                                                    ProgressEngineerPackage = 60,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(12),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(15),
                                                    ProgressPurchaseOrder = 90,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(16),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(20),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(22),
                                                    ProgressReadinesstoShip = 95,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                                    PONum = "4500798708",
                                                    ProductionOrderNumber = "PO987673",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Larry" && d.EngLastName == "Johnson").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465918,
                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Pinnacle Pellet").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123476").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-15),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-10),
                                                    ProgressApprovalDrawing = 50,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-5),
                                                    PreOrderExpected = DateTime.Now.AddDays(1),
                                                    PreOrderReleased = DateTime.Now.AddDays(4),
                                                    ProgressPreOrder = 75,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(7),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(10),
                                                    ProgressEngineerPackage = 60,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(12),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(15),
                                                    ProgressPurchaseOrder = 90,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(16),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(20),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(22),
                                                    ProgressReadinesstoShip = 95,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                                    PONum = "4500798709",
                                                    ProductionOrderNumber = "PO987674",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465919,
                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Pinnacle Pellet").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123477").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-1),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(3),
                                                    ProgressApprovalDrawing = 50,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(10),
                                                    PreOrderExpected = DateTime.Now.AddDays(3),
                                                    PreOrderReleased = DateTime.Now.AddDays(6),
                                                    ProgressPreOrder = 60,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(8),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(9),
                                                    ProgressEngineerPackage = 40,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(4),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(11),
                                                    ProgressPurchaseOrder = 85,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(12),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(14),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(15),
                                                    ProgressReadinesstoShip = 90,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                                    PONum = "4500798710",
                                                    ProductionOrderNumber = "PO987676",

                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465920,

                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Pinnacle Pellet").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123478").ID,

                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-3),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(4),
                                                    ProgressApprovalDrawing = 100,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(7),
                                                    PreOrderExpected = DateTime.Now.AddDays(9),
                                                    PreOrderReleased = DateTime.Now.AddDays(13),
                                                    ProgressPreOrder = 75,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(18),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(25),
                                                    ProgressEngineerPackage = 100,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(30),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(35),
                                                    ProgressPurchaseOrder = 50,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(40),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(41),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(44),
                                                    ProgressReadinesstoShip = 25,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Wilfred Machining").ID,
                                                    PONum = "4500798711",
                                                    ProductionOrderNumber = "PO987677",

                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465921,
                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Farland Outfitters").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123479").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(1),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(6),
                                                    ProgressApprovalDrawing = 100,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(10),
                                                    PreOrderExpected = DateTime.Now.AddDays(12),
                                                    PreOrderReleased = DateTime.Now.AddDays(16),
                                                    ProgressPreOrder = 100,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(19),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(22),
                                                    ProgressEngineerPackage = 70,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(25),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(30),
                                                    ProgressPurchaseOrder = 90,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(33),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(37),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(40),
                                                    ProgressReadinesstoShip = 25,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                                    PONum = "4500798712",
                                                    ProductionOrderNumber = "PO987678",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465922,
                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Farland Outfitters").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123480").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-10),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-7),
                                                    ProgressApprovalDrawing = 100,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-5),
                                                    PreOrderExpected = DateTime.Now.AddDays(-1),
                                                    PreOrderReleased = DateTime.Now.AddDays(2),
                                                    ProgressPreOrder = 100,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(5),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(8),
                                                    ProgressEngineerPackage = 90,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(10),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(17),
                                                    ProgressPurchaseOrder = 70,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(20),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(25),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(30),
                                                    ProgressReadinesstoShip = 90,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                                    PONum = "4500798713",
                                                    ProductionOrderNumber = "PO987679",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465923,
                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Farland Outfitters").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123481").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-30),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-25),
                                                    ProgressApprovalDrawing = 100,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-15),
                                                    PreOrderExpected = DateTime.Now.AddDays(-14),
                                                    PreOrderReleased = DateTime.Now.AddDays(-11),
                                                    ProgressPreOrder = 100,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(-9),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(-1),
                                                    ProgressEngineerPackage = 90,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(1),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(8),
                                                    ProgressPurchaseOrder = 50,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(12),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(15),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(20),
                                                    ProgressReadinesstoShip = 90,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Niagara Metalworks").ID,
                                                    PONum = "4500798714",
                                                    ProductionOrderNumber = "PO987680",
                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Larry" && d.EngLastName == "Johnson").ID,
                                                    NoteID = 1
                                                },
                                                    new OperationsSchedule
                                                    {
                                                        SalesOrdNum = 10465924,
                                                        CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Windsor Contracting").ID,
                                                        MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123482").ID,
                                                        KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                        ApprovalDrawingExpected = DateTime.Now.AddDays(7),
                                                        ApprovalDrawingReleased = DateTime.Now.AddDays(10),
                                                        ProgressApprovalDrawing = 70,
                                                        ApprovalDrawingReturned = DateTime.Now.AddDays(11),
                                                        PreOrderExpected = DateTime.Now.AddDays(13),
                                                        PreOrderReleased = DateTime.Now.AddDays(17),
                                                        ProgressPreOrder = 100,
                                                        EngineerPackageExpected = DateTime.Now.AddDays(20),
                                                        EngineerPackageReleased = DateTime.Now.AddDays(25),
                                                        ProgressEngineerPackage = 30,
                                                        PurchaseOrderExpected = DateTime.Now.AddDays(28),
                                                        PurchaseOrderDueDate = DateTime.Now.AddDays(30),
                                                        ProgressPurchaseOrder = 100,
                                                        PUrchaseOrderRecieved = DateTime.Now.AddDays(31),
                                                        ReadinessToShipExpected = DateTime.Now.AddDays(32),
                                                        ReadinessToShipActual = DateTime.Now.AddDays(35),
                                                        ProgressReadinesstoShip = 100,
                                                        VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Kraft Machining").ID,
                                                        PONum = "4500798715",
                                                        ProductionOrderNumber = "PO987681",
                                                        EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID
                                                        //NoteID = 1
                                                    },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465925,
                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Windsor Contracting").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123483").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(-12),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(-8),
                                                    ProgressApprovalDrawing = 100,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(-5),
                                                    PreOrderExpected = DateTime.Now.AddDays(-1),
                                                    PreOrderReleased = DateTime.Now.AddDays(2),
                                                    ProgressPreOrder = 100,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(6),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(9),
                                                    ProgressEngineerPackage = 90,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(14),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(20),
                                                    ProgressPurchaseOrder = 50,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(25),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(30),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(35),
                                                    ProgressReadinesstoShip = 25,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                                    PONum = "4500798716",
                                                    ProductionOrderNumber = "PO987682",

                                                    EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID
                                                    //NoteID = 1
                                                },
                                                new OperationsSchedule
                                                {
                                                    SalesOrdNum = 10465926,
                                                    CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Windsor Contracting").ID,
                                                    MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123484").ID,
                                                    KickoffMeeting = DateTime.Parse("2024-11-11"),
                                                    ApprovalDrawingExpected = DateTime.Now.AddDays(5),
                                                    ApprovalDrawingReleased = DateTime.Now.AddDays(7),
                                                    ProgressApprovalDrawing = 100,
                                                    ApprovalDrawingReturned = DateTime.Now.AddDays(10),
                                                    PreOrderExpected = DateTime.Now.AddDays(15),
                                                    PreOrderReleased = DateTime.Now.AddDays(20),
                                                    ProgressPreOrder = 90,
                                                    EngineerPackageExpected = DateTime.Now.AddDays(21),
                                                    EngineerPackageReleased = DateTime.Now.AddDays(25),
                                                    ProgressEngineerPackage = 75,
                                                    PurchaseOrderExpected = DateTime.Now.AddDays(27),
                                                    PurchaseOrderDueDate = DateTime.Now.AddDays(30),
                                                    ProgressPurchaseOrder = 55,
                                                    PUrchaseOrderRecieved = DateTime.Now.AddDays(31),
                                                    ReadinessToShipExpected = DateTime.Now.AddDays(35),
                                                    ReadinessToShipActual = DateTime.Now.AddDays(40),
                                                    ProgressReadinesstoShip = 100,
                                                    VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                                    PONum = "4500798717",
                                                    ProductionOrderNumber = "PO987682",
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

                //Seed Data for OperationShecduleMachine M:M
                try
                {
                    if (!context.OperationsScheduleMachines.Any())
                    {
                        context.OperationsScheduleMachines.AddRange(
                            new OperationsScheduleMachine
                            {
                                OperationsScheduleID = 1,
                                MachineDescriptionID = 1,
                            }
                        );
                        context.SaveChanges();
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"An error occured while seeding Operation Schedule Machines: {ex.Message}");
                }

                try
                {
                    if (!context.HaverGantts.Any())
                    {

                        var today = DateTime.Today;


                        var gantts = new List<HaverGantt>
                        {
                            new HaverGantt
                            {
                                PurchaseOrderNum = "18746528",
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Windsor Contracting").ID,
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123484").ID,
                                Quantity = 2,
                                ApprvDwgRecvd = DateTime.Today.AddDays(-7),
                                StartDate = today.AddDays(0),
                                PromiseDate= today.AddDays(7),
                                Progress = 0.6m
                            },
                            new HaverGantt
                            {
                                PurchaseOrderNum = "18746537",
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Connor Company").ID,
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Thompson Machining").ID,
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123462").ID,  // Changed to SN123462
                                Quantity = 2,
                                GanttNotes = "Design changes completed. Awaiting final review.",
                                ApprvDwgRecvd = DateTime.Today.AddDays(-5),
                                StartDate = today.AddDays(2),
                                PromiseDate = today.AddDays(9),
                                Progress = 0.6m
                            },

                            new HaverGantt
                            {
                                PurchaseOrderNum = "18746538",
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Potato Shakers").ID,
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "MasterCraft Machining").ID,
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123467").ID,  // Changed to SN123467
                                Quantity = 3,
                                GanttNotes = "Approval pending for final design. Expected this week.",
                                ApprvDwgRecvd = DateTime.Today.AddDays(-7),
                                StartDate = today.AddDays(4),
                                PromiseDate = today.AddDays(12),
                                Progress = 0.8m
                            },

                            new HaverGantt
                            {
                                PurchaseOrderNum = "18746539",
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Centenial Grinding").ID,
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Jameson Parts").ID,
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123466").ID,  // Changed to SN123466
                                Quantity = 1,
                                GanttNotes = "Mechanical issues fixed. Project on track.",
                                ApprvDwgRecvd = DateTime.Today.AddDays(-10),
                                StartDate = today.AddDays(5),
                                PromiseDate = today.AddDays(15),
                                Progress = 0.5m
                            },

                            new HaverGantt
                            {
                                PurchaseOrderNum = "18746540",
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Honest Jays").ID,
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Wilfred Machining").ID,
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123463").ID,  // Changed to SN123463
                                Quantity = 4,
                                GanttNotes = "Awaiting components from supplier. Potential delay.",
                                ApprvDwgRecvd = DateTime.Today.AddDays(-3),
                                StartDate = today.AddDays(6),
                                PromiseDate = today.AddDays(13),
                                Progress = 0.4m
                            },

                            new HaverGantt
                            {
                                PurchaseOrderNum = "18746541",
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Masher McMash").ID,
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hudson Metal").ID,
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Larry" && d.EngLastName == "Johnson").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123468").ID,  // Changed to SN123468
                                Quantity = 2,
                                GanttNotes = "Design review completed, starting implementation phase.",
                                ApprvDwgRecvd = DateTime.Today.AddDays(-2),
                                StartDate = today.AddDays(3),
                                PromiseDate = today.AddDays(10),
                                Progress = 0.7m
                            },

                            new HaverGantt
                            {
                                PurchaseOrderNum = "18746542",
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "We Dig Holes").ID,
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Niagara Metalworks").ID,
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "John" && d.EngLastName == "Doe").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123464").ID,  // Changed to SN123464
                                Quantity = 6,
                                GanttNotes = "Progressing smoothly. Some minor adjustments needed.",
                                ApprvDwgRecvd = DateTime.Today.AddDays(-8),
                                StartDate = today.AddDays(2),
                                PromiseDate = today.AddDays(10),
                                Progress = 0.9m
                            },

                            new HaverGantt
                            {
                                PurchaseOrderNum = "18746543",
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Sifting Made Easy").ID,
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Hamilton Industrial").ID,
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Mike" && d.EngLastName == "Jones").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123469").ID,  // Changed to SN123469
                                Quantity = 3,
                                GanttNotes = "Design approval needed. Waiting for client's feedback.",
                                ApprvDwgRecvd = DateTime.Today.AddDays(-6),
                                StartDate = today.AddDays(3),
                                PromiseDate = today.AddDays(12),
                                Progress = 0.5m
                            },

                            new HaverGantt
                            {
                                PurchaseOrderNum = "18746544",
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Pinnacle Pellet").ID,
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Forward Machining").ID,
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Greg" && d.EngLastName == "Naismith").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123470").ID,  // Changed to SN123470
                                Quantity = 2,
                                GanttNotes = "Materials sourced, awaiting delivery.",
                                ApprvDwgRecvd = DateTime.Today.AddDays(-4),
                                StartDate = today.AddDays(7),
                                PromiseDate = today.AddDays(14),
                                Progress = 0.6m
                            },

                            new HaverGantt
                            {
                                PurchaseOrderNum = "18746545",
                                CustomerID = context.Customers.FirstOrDefault(d => d.CustomerName == "Farland Outfitters").ID,
                                VendorID = context.Vendors.FirstOrDefault(d => d.VendorName == "Princeton Processing").ID,
                                EngineerID = context.Engineers.FirstOrDefault(d => d.EngFirstName == "Robert" && d.EngLastName == "Aquilini").ID,
                                MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(d => d.SerialNumber == "SN123471").ID,  // Changed to SN123471
                                Quantity = 5,
                                GanttNotes = "Awaiting supplier confirmation for delivery dates.",
                                ApprvDwgRecvd = DateTime.Today.AddDays(-9),
                                StartDate = today.AddDays(4),
                                PromiseDate = today.AddDays(11),
                                Progress = 0.3m
                            }
                        };               
                        
                        context.HaverGantts.AddRange(gantts);  
                        context.SaveChanges();
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"An error occured while seeding Gantts: {ex.Message}");
                }


                #endregion




            }//Using Close

        }//Initialize close

    }//HaverInitializer Class close.

}//Namespace close
