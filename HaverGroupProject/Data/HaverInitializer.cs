using Microsoft.EntityFrameworkCore;
using HaverGroupProject.Models;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Linq;

namespace HaverGroupProject.Data
{
    public static class HaverInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider,
            bool DeleteDatabase = true, bool UseMigrations = true, bool SeedSampleData = true)
        {
            using (var context = new HaverContext(
                serviceProvider.GetRequiredService<DbContextOptions<HaverContext>>()))
            {
                #region Database Prep
                try
                {
                    //Seed Data goes here
                    //Look for Customers and Vendors first, then SalesOrders
                    if (!context.Customers.Any())
                    {
                        context.Customers.AddRange(
                            new Customer
                            {
                                CustomerName = "Owens Corning",
                                CustomerAddress = "123 Maple Street",
                                CustomerContactName = "Liam Smith",
                                //CustomerPhone = "519408756",
                                CustomerEmail = "LSmith@hotmail.com",
                            },
                            new Customer
                            {
                                CustomerName = "FMI",
                                CustomerAddress = "456 Oak Avenue",
                                CustomerContactName = "Olivia Brown",
                                //CustomerPhone = "519475385",
                                CustomerEmail = "OBrown@hotmail.com",
                            },
                            new Customer
                            {
                                CustomerName = "Coloured Aggregates",
                                CustomerAddress = "789 Pine Road",
                                CustomerContactName = "Noah Davis",
                                //CustomerPhone = "519230014",
                                CustomerEmail = "NDavis@hotmail.com",
                            },
                            new Customer
                            {
                                CustomerName = "Rio Tinto Sorel",
                                CustomerAddress = "101 Elm Drive",
                                CustomerContactName = "Ava Wilson",
                                //CustomerPhone = "5198874950",
                                CustomerEmail = "AWilson@hotmail.com",
                            },
                            new Customer
                            {
                                CustomerName = "Calidra La Laja",
                                CustomerAddress = "202 Birch Lane",
                                CustomerContactName = "Ethan Martinez",
                                //CustomerPhone = "5196947533",
                                CustomerEmail = "EMartinez@hotmail.com",
                            },
                            new Customer
                            {
                                CustomerName = "Intradco",
                                CustomerAddress = "302 Cedar Boulevard",
                                CustomerContactName = "Sophia Anderson",
                                //CustomerPhone = "5190081471",
                                CustomerEmail = "SAnderson@hotmail.com",
                            },
                            new Customer
                            {
                                CustomerName = "SIL Industrial Minerals",
                                CustomerAddress = "505 Willow Way",
                                CustomerContactName = "Mason Taylor",
                                //CustomerPhone = "5199630258",
                                CustomerEmail = "MTaylor@hotmail.com",
                            },
                            new Customer
                            {
                                CustomerName = "United Taconite",
                                CustomerAddress = "606 Aspen Circle",
                                CustomerContactName = "Isabella Thomas",
                                //CustomerPhone = "5197782244",
                                CustomerEmail = "IThomas@hotmail.com",
                            },
                            new Customer
                            {
                                CustomerName = "Tougher Industries",
                                CustomerAddress = "707 Redwood Terrace",
                                CustomerContactName = "Lucas Moore",
                                //CustomerPhone = "5193216540",
                                CustomerEmail = "LMoore@hotmail.com",
                            },
                            new Customer
                            {
                                CustomerName = "Connor Company",
                                CustomerAddress = "404 Spruce Court",
                                CustomerContactName = "Emma Johnson",
                                //CustomerPhone = "5197890777",
                                CustomerEmail = "EJohnson@hotmail.com",
                            });
                        context.SaveChanges();
                    }

                    //Seed data for Vendors
                    if (!context.Vendors.Any())
                    {
                        context.Vendors.AddRange(
                        new Vendor
                        {
                            VendorName = "Precision Tools Inc",
                            VendorAddress = "808 Cypress Street",
                            VendorContactName = "Mia Clark",
                            VendorPhone = "5326250989",
                            VendorEmail = "MClark@gmail.com"
                        },
                        new Vendor
                        {
                            VendorName = "Apex Machining Solutions",
                            VendorAddress = "909 Magnolia Avenue",
                            VendorContactName = "Benjamin Lewis",
                            VendorPhone = "5328884050",
                            VendorEmail = "BLewis@gmail.com"
                        },
                        new Vendor
                        {
                            VendorName = "Elite Manufacturing",
                            VendorAddress = "1010 Chestnut Road",
                            VendorContactName = "Charlotte Walker",
                            VendorPhone = "5323216877",
                            VendorEmail = "CWalker@gmail.com"
                        },
                        new Vendor
                        {
                            VendorName = "Titan Machines",
                            VendorAddress = "111 Dogwood Drive",
                            VendorContactName = "James Hall",
                            VendorPhone = "5329877115",
                            VendorEmail = "JHall@gmail.com"
                        },
                        new Vendor
                        {
                            VendorName = "Superior CNC Services",
                            VendorAddress = "1212 Fir Lane",
                            VendorContactName = "Ameila Young",
                            VendorPhone = "5327534215",
                            VendorEmail = "AYoung@gmail.com"
                        },
                        new Vendor
                        {
                            VendorName = "Prime Metal Fabricators",
                            VendorAddress = "1313 Hemlock Boulevard",
                            VendorContactName = "Alexander King",
                            VendorPhone = "5329511599",
                            VendorEmail = "AKing@hotmail.com"
                        },
                        new Vendor
                        {
                            VendorName = "AMT",
                            VendorAddress = "15 Juniper Court",
                            VendorContactName = "Harper Scott",
                            VendorPhone = "5323852140",
                            VendorEmail = "HScott@gmail.com"
                        },
                        new Vendor
                        {
                            VendorName = "ProCut Engineering",
                            VendorAddress = "1515 Poplar Way",
                            VendorContactName = "Henry Greens",
                            VendorPhone = "5325983744",
                            VendorEmail = "HGreens@gmail.com"
                        },
                        new Vendor
                        {
                            VendorName = "Dynamic Tooling Systems",
                            VendorAddress = "1616 Sycamore Circle",
                            VendorContactName = "Evelyn Adams",
                            VendorPhone = "5320964588",
                            VendorEmail = "EAdams@gmail.com"
                        },
                        new Vendor
                        {
                            VendorName = "MasterCraft Machining",
                            VendorAddress = "1717 Walnut Terrace",
                            VendorContactName = "Daniel Baker",
                            VendorPhone = "5328659874",
                            VendorEmail = "DBaker@gmail.com"
                        });
                        context.SaveChanges();
                    }

                    //Seed data for Operations Schedule
                    if (!context.OperationsSchedules.Any())
                    {
                        context.OperationsSchedules.AddRange(
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10430736,
                                ExtSalesOrdNum = 3934506,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Owens Corning").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-330" + " " + "6'x20'" + " " + "-" +"1D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "James" && e.EngLastName == "Jackson").ID,
                                KickoffMeeting = DateOnly.Parse("2024-10-01"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-10-02"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Superior CNC Services").ID,
                                PONum = "4500798658",
                                PODueDate = DateOnly.Parse("2024-11-02"),
                                DeliveryDate = DateOnly.Parse("2024-11-12"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10438456,
                                ExtSalesOrdNum = 3934701,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Owens Corning").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "S-300" + " " + "5'x9'" + " " + "-" + "2D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "John" && e.EngLastName == "Hernandez").ID,
                                KickoffMeeting = DateOnly.Parse("2024-10-06"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-10-12"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Superior CNC Services").ID,
                                PONum = "4500798660",
                                PODueDate = DateOnly.Parse("2024-12-02"),
                                DeliveryDate = DateOnly.Parse("2024-12-12"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10431478,
                                ExtSalesOrdNum = 3934251,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Owens Corning").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-330" + " " + "4'x10'" + " " + "-" + "3D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Lisa" && e.EngLastName == "Lewis").ID,
                                KickoffMeeting = DateOnly.Parse("2024-09-05"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-09-12"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "MasterCraft Machining").ID,
                                PONum = "4500798661",
                                PODueDate = DateOnly.Parse("2024-10-24"),
                                DeliveryDate = DateOnly.Parse("2024-10-30"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10439630,
                                ExtSalesOrdNum = 3934963,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "FMI").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "F-600" + " " + "5'x10'" + " " + "-" + "1D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Lisa" && e.EngLastName == "Lewis").ID,
                                KickoffMeeting = DateOnly.Parse("2024-10-11"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-10-18"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Apex Machining Solutions").ID,
                                PONum = "4500798662",
                                PODueDate = DateOnly.Parse("2024-12-18"),
                                DeliveryDate = DateOnly.Parse("2024-12-22"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10439874,
                                ExtSalesOrdNum = 3934987,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "FMI").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-1100" + " " + "6'x16'" + " " + "-" + "1D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "David" && e.EngLastName == "Brown").ID,
                                KickoffMeeting = DateOnly.Parse("2024-08-17"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-08-24"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Superior CNC Services").ID,
                                PONum = "4500798663",
                                PODueDate = DateOnly.Parse("2024-08-28"),
                                DeliveryDate = DateOnly.Parse("2024-09-03"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10436541,
                                ExtSalesOrdNum = 3934654,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Coloured Aggregates").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-1100" + " " + "6'x16'" + " " + "-" + "2D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "James" && e.EngLastName == "Jackson").ID,
                                KickoffMeeting = DateOnly.Parse("2025-01-09"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2025-01-16"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Titan Machines").ID,
                                PONum = "4500798664",
                                PODueDate = DateOnly.Parse("2025-01-18"),
                                DeliveryDate = DateOnly.Parse("2025-01-25"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10433210,
                                ExtSalesOrdNum = 3934213,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Coloured Aggregates").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "L-800" + " " + "6'x20'" + " " + "-" + "2D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Ellie" && e.EngLastName == "Thompson").ID,
                                KickoffMeeting = DateOnly.Parse("2024-09-20"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-09-27"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Apex Machining Solutions").ID,
                                PONum = "4500798665",
                                PODueDate = DateOnly.Parse("2024-10-01"),
                                DeliveryDate = DateOnly.Parse("2024-10-15"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10437894,
                                ExtSalesOrdNum = 3934789,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Coloured Aggregates").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "S-300" + " " + "5'x9'" + " " + "-" + "3D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Christopher" && e.EngLastName == "Sanchez").ID,
                                KickoffMeeting = DateOnly.Parse("2024-10-06"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-10-15"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "MasterCraft Machining").ID,
                                PONum = "4500798666",
                                PODueDate = DateOnly.Parse("2024-10-15"),
                                DeliveryDate = DateOnly.Parse("2024-10-21"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10434561,
                                ExtSalesOrdNum = 3934456,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Rio Tinto Sorel").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-880" + " " + "5'x12'" + " " + "-" + "3D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "David" && e.EngLastName == "Brown").ID,
                                KickoffMeeting = DateOnly.Parse("2024-12-13"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-12-20"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Elite Manufacturing").ID,
                                PONum = "4500798667",
                                PODueDate = DateOnly.Parse("2025-12-05"),
                                DeliveryDate = DateOnly.Parse("2025-12-11"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10431230,
                                ExtSalesOrdNum = 393123,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Rio Tinto Sorel").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-880" + " " + "5'x12'" + " " + "-" + "1D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Matthew" && e.EngLastName == "Davis").ID,
                                KickoffMeeting = DateOnly.Parse("2024-11-26"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-12-01"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Elite Manufacturing").ID,
                                PONum = "4500798668",
                                PODueDate = DateOnly.Parse("2024-12-01"),
                                DeliveryDate = DateOnly.Parse("2024-12-07"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10437532,
                                ExtSalesOrdNum = 3934753,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Rio Tinto Sorel").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "F-900" + " " + "6'x14'" + " " + "-" + "1D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "John" && e.EngLastName == "Hernandez").ID,
                                KickoffMeeting = DateOnly.Parse("2024-10-06"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-10-08"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Superior CNC Services").ID,
                                PONum = "4500798669",
                                PODueDate = DateOnly.Parse("2024-10-12"),
                                DeliveryDate = DateOnly.Parse("2024-10-14"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10433570,
                                ExtSalesOrdNum = 3934357,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Calidra La Laja").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "F-900" + " " + "6'x16'" + " " + "-" + "1D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Ellie" && e.EngLastName == "Thompson").ID,
                                KickoffMeeting = DateOnly.Parse("2024-10-18"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-10-24"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Prime Metal Fabricators").ID,
                                PONum = "4500798670",
                                PODueDate = DateOnly.Parse("2024-10-26"),
                                DeliveryDate = DateOnly.Parse("2024-11-01"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10438612,
                                ExtSalesOrdNum = 3934861,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Calidra La Laja").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "F-1100" + " " + "8'x16'" + " " + "-" + "2D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Karen" && e.EngLastName == "Lee").ID,
                                KickoffMeeting = DateOnly.Parse("2024-11-17"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-11-19"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Apex Machining Solutions").ID,
                                PONum = "4500798671",
                                PODueDate = DateOnly.Parse("2024-11-25"),
                                DeliveryDate = DateOnly.Parse("2024-11-30"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10439078,
                                ExtSalesOrdNum = 3934907,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Calidra La Laja").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "F-900" + " " + "6'x16'" + " " + "-" + "2D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Michael" && e.EngLastName == "Johnson").ID,
                                KickoffMeeting = DateOnly.Parse("2024-10-01"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-10-03"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Dynamic Tooling Systems").ID,
                                PONum = "4500798672",
                                PODueDate = DateOnly.Parse("2024-11-02"),
                                DeliveryDate = DateOnly.Parse("2024-11-07"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10437733,
                                ExtSalesOrdNum = 3938462,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Intradco").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "F-900" + " " + "6'x16'" + " " + "-" + "2D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Michael" && e.EngLastName == "Johnson").ID,
                                KickoffMeeting = DateOnly.Parse("2024-10-08"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-10-14"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Titan Machines").ID,
                                PONum = "4500798673",
                                PODueDate = DateOnly.Parse("2024-10-20"),
                                DeliveryDate = DateOnly.Parse("2024-10-22"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10431793,
                                ExtSalesOrdNum = 3934179,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Intradco").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-660" + " " + "5'x12'" + " " + "-" + "3D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "David" && e.EngLastName == "Brown").ID,
                                KickoffMeeting = DateOnly.Parse("2024-11-01"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-11-04"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Prime Metal Fabricators").ID,
                                PONum = "4500798674",
                                PODueDate = DateOnly.Parse("2024-11-08"),
                                DeliveryDate = DateOnly.Parse("2024-11-10"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10439731,
                                ExtSalesOrdNum = 3934114,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Intradco").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "S-110" + " " + "4'x8'" + " " + "-" + "3D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "James" && e.EngLastName == "Jackson").ID,
                                KickoffMeeting = DateOnly.Parse("2024-11-16"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-11-17"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "ProCut Engineering").ID,
                                PONum = "4500798675",
                                PODueDate = DateOnly.Parse("2024-11-21"),
                                DeliveryDate = DateOnly.Parse("2024-11-23"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10435426,
                                ExtSalesOrdNum = 3934426,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "SIL Industrial Minerals").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-880" + " " + "5'x12'" + " " + "-" + "3D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Lisa" && e.EngLastName == "Lewis").ID,
                                KickoffMeeting = DateOnly.Parse("2024-10-09"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-10-12"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Prime Metal Fabricators").ID,
                                PONum = "4500798676",
                                PODueDate = DateOnly.Parse("2024-10-16"),
                                DeliveryDate = DateOnly.Parse("2024-10-20"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10434865,
                                ExtSalesOrdNum = 3934684,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "SIL Industrial Minerals").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-330" + " " + "4'x10'" + " " + "-" + "1D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Anthony" && e.EngLastName == "White").ID,
                                KickoffMeeting = DateOnly.Parse("2025-01-03"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2025-01-06"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Elite Manufacturing").ID,
                                PONum = "4500798677",
                                PODueDate = DateOnly.Parse("2025-01-09"),
                                DeliveryDate = DateOnly.Parse("2025-01-11"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10430025,
                                ExtSalesOrdNum = 3934852,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "SIL Industrial Minerals").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-330" + " " + "4'x10'" + " " + "-" + "1D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Anthony" && e.EngLastName == "White").ID,
                                KickoffMeeting = DateOnly.Parse("2024-10-18"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-10-20"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Precision Tools Inc").ID,
                                PONum = "4500798678",
                                PODueDate = DateOnly.Parse("2024-11-01"),
                                DeliveryDate = DateOnly.Parse("2024-11-10"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10434159,
                                ExtSalesOrdNum = 3934951,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "United Taconite").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "F-600" + " " + "5'x10'" + " " + "-" + "1D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Anthony" && e.EngLastName == "White").ID,
                                KickoffMeeting = DateOnly.Parse("2024-10-15"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-10-20"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Apex Machining Solutions").ID,
                                PONum = "4500798679",
                                PODueDate = DateOnly.Parse("2024-11-05"),
                                DeliveryDate = DateOnly.Parse("2024-11-10"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10437532,
                                ExtSalesOrdNum = 3934357,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "United Taconite").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-1100" + " " + "6'x16'" + " " + "-" + "1D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Ellie" && e.EngLastName == "Thompson").ID,
                                KickoffMeeting = DateOnly.Parse("2024-11-01"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-11-06"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Prime Metal Fabricators").ID,
                                PONum = "4500798680",
                                PODueDate = DateOnly.Parse("2024-11-08"),
                                DeliveryDate = DateOnly.Parse("2024-11-12"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10434827,
                                ExtSalesOrdNum = 3930198,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "United Taconite").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-1100" + " " + "6'x16'" + " " + "-" + "2D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Matthew" && e.EngLastName == "Davis").ID,
                                KickoffMeeting = DateOnly.Parse("2024-09-01"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-09-04"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Elite Manufacturing").ID,
                                PONum = "4500798681",
                                PODueDate = DateOnly.Parse("2024-09-14"),
                                DeliveryDate = DateOnly.Parse("2024-09-16"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10484951,
                                ExtSalesOrdNum = 3934775,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Tougher Industries").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "L-800" + " " + "6'x20'" + " " + "-" + "2D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Christopher" && e.EngLastName == "Sanchez").ID,
                                KickoffMeeting = DateOnly.Parse("2024-09-11"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-09-14"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Apex Machining Solutions").ID,
                                PONum = "4500798682",
                                PODueDate = DateOnly.Parse("2024-09-21"),
                                DeliveryDate = DateOnly.Parse("2024-09-24"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 104369321,
                                ExtSalesOrdNum = 3934999,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Tougher Industries").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "S-300" + " " + "5'x9'" + " " + "-" + "2D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Richard" && e.EngLastName == "Smith").ID,
                                KickoffMeeting = DateOnly.Parse("2024-09-25"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-09-27"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Precision Tools Inc").ID,
                                PONum = "4500798683",
                                PODueDate = DateOnly.Parse("2024-10-02"),
                                DeliveryDate = DateOnly.Parse("2024-10-05"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10432020,
                                ExtSalesOrdNum = 3934303,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Tougher Industries").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "S-300" + " " + "5'x9'" + " " + "-" + "2D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Richard" && e.EngLastName == "Smith").ID,
                                KickoffMeeting = DateOnly.Parse("2024-10-25"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-10-28"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Prime Metal Fabricators").ID,
                                PONum = "4500798684",
                                PODueDate = DateOnly.Parse("2024-10-03"),
                                DeliveryDate = DateOnly.Parse("2024-11-02"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10436621,
                                ExtSalesOrdNum = 3930492,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Connor Company").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-880" + " " + "5'x12'" + " " + "-" + "3D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "David" && e.EngLastName == "Brown").ID,
                                KickoffMeeting = DateOnly.Parse("2024-11-06"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-11-09"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "MasterCraft Machining").ID,
                                PONum = "4500798685",
                                PODueDate = DateOnly.Parse("2024-11-13"),
                                DeliveryDate = DateOnly.Parse("2024-11-18"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10478325,
                                ExtSalesOrdNum = 3934555,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Connor Company").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "T-880" + " " + "5'x12'" + " " + "-" + "3D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Mark" && e.EngLastName == "Ramirez").ID,
                                KickoffMeeting = DateOnly.Parse("2024-11-24"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-11-28"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "AMT").ID,
                                PONum = "4500798686",
                                PODueDate = DateOnly.Parse("2024-11-30"),
                                DeliveryDate = DateOnly.Parse("2024-12-07"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10494717,
                                ExtSalesOrdNum = 3934225,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "Connor Company").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "F-900" + " " + "6'x14'" + " " + "-" + "3D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Jennifer" && e.EngLastName == "Jones").ID,
                                KickoffMeeting = DateOnly.Parse("2024-10-01"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-10-05"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "AMT").ID,
                                PONum = "4500798687",
                                PODueDate = DateOnly.Parse("2024-10-29"),
                                DeliveryDate = DateOnly.Parse("2024-11-04"),
                            },
                            new OperationsSchedule
                            {
                                SalesOrdNum = 10465896,
                                ExtSalesOrdNum = 3934999,
                                CustomerID = context.Customers.FirstOrDefault(n => n.CustomerName == "FMI").ID,
                                //MachineDescriptionID = context.MachineDescriptions.FirstOrDefault(m => m.DescriptionSummary == "F-900" + " " + "6'x19'" + " " + "-" + "3D").ID,
                                //EngineerID = context.Engineers.FirstOrDefault(e => e.EngFirstName == "Richard" && e.EngLastName == "Smith").ID,
                                KickoffMeeting = DateOnly.Parse("2024-11-11"),
                                ReleaseApprovalDrawing = DateOnly.Parse("2024-11-14"),
                                VendorID = context.Vendors.FirstOrDefault(v => v.VendorName == "Dynamic Tooling Systems").ID,
                                PONum = "4500798688",
                                PODueDate = DateOnly.Parse("2024-12-03"),
                                DeliveryDate = DateOnly.Parse("2024-12-14"),
                            });
                        context.SaveChanges();
                    }

                    //MT 30-entries for 30 customers
                    //if (!context.Additions.Any())
                    //{
                    //    context.Additions.AddRange(
                    //        new Additions
                    //        {
                    //            InstalledMedia = true,
                    //            SparePartsSpareMedia = true,
                    //            BaseFrame = true,
                    //            AirSeal = false,
                    //            CoatingLining = true,
                    //            Disassembly = false,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = true,
                    //            BaseFrame = true,
                    //            AirSeal = false,
                    //            CoatingLining = true,
                    //            Disassembly = true,
                    //        },
                    //         new Additions
                    //         {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = false,
                    //            BaseFrame = false,
                    //            AirSeal = true,
                    //            CoatingLining = false,
                    //            Disassembly = false,
                    //         },
                    //        new Additions
                    //        {
                    //            InstalledMedia = true,
                    //            SparePartsSpareMedia = true,
                    //            BaseFrame = false,
                    //            AirSeal = true,
                    //            CoatingLining = false,
                    //            Disassembly = true,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = true,
                    //            SparePartsSpareMedia = false,
                    //            BaseFrame = false,
                    //            AirSeal = true,
                    //            CoatingLining = false,
                    //            Disassembly = true,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = true,
                    //            SparePartsSpareMedia = true,
                    //            BaseFrame = false,
                    //            AirSeal = false,
                    //            CoatingLining = true,
                    //            Disassembly = true,
                    //        },
                    //         new Additions
                    //         {
                    //             InstalledMedia = true,
                    //             SparePartsSpareMedia = false,
                    //             BaseFrame = false,
                    //             AirSeal = true,
                    //             CoatingLining = false,
                    //             Disassembly = true,
                    //         },
                    //        new Additions
                    //        {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = false,
                    //            BaseFrame = true,
                    //            AirSeal = true,
                    //            CoatingLining = true,
                    //            Disassembly = false,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = true,
                    //            BaseFrame = false,
                    //            AirSeal = false,
                    //            CoatingLining = false,
                    //            Disassembly = false,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = true,
                    //            SparePartsSpareMedia = true,
                    //            BaseFrame = false,
                    //            AirSeal = false,
                    //            CoatingLining = true,
                    //            Disassembly = false,
                    //        },
                    //         new Additions
                    //         {
                    //             InstalledMedia = false,
                    //             SparePartsSpareMedia = false,
                    //             BaseFrame = false,
                    //             AirSeal = true,
                    //             CoatingLining = true,
                    //             Disassembly = false,
                    //         },
                    //        new Additions
                    //        {
                    //            InstalledMedia = true,
                    //            SparePartsSpareMedia = false,
                    //            BaseFrame = false,
                    //            AirSeal = false,
                    //            CoatingLining = true,
                    //            Disassembly = false,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = true,
                    //            BaseFrame = false,
                    //            AirSeal = true,
                    //            CoatingLining = false,
                    //            Disassembly = false,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = false,
                    //            BaseFrame = true,
                    //            AirSeal = true,
                    //            CoatingLining = false,
                    //            Disassembly = false,
                    //        },
                    //         new Additions
                    //         {
                    //             InstalledMedia = false,
                    //             SparePartsSpareMedia = true,
                    //             BaseFrame = false,
                    //             AirSeal = false,
                    //             CoatingLining = true,
                    //             Disassembly = false,
                    //         },
                    //        new Additions
                    //        {
                    //            InstalledMedia = true,
                    //            SparePartsSpareMedia = false,
                    //            BaseFrame = true,
                    //            AirSeal = true,
                    //            CoatingLining = true,
                    //            Disassembly = false,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = false,
                    //            BaseFrame = true,
                    //            AirSeal = true,
                    //            CoatingLining = false,
                    //            Disassembly = false,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = true,
                    //            SparePartsSpareMedia = false,
                    //            BaseFrame = true,
                    //            AirSeal = false,
                    //            CoatingLining = true,
                    //            Disassembly = false,
                    //        },
                    //         new Additions
                    //         {
                    //             InstalledMedia = false,
                    //             SparePartsSpareMedia = false,
                    //             BaseFrame = true,
                    //             AirSeal = true,
                    //             CoatingLining = true,
                    //             Disassembly = false,
                    //         },
                    //        new Additions
                    //        {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = false,
                    //            BaseFrame = true,
                    //            AirSeal = true,
                    //            CoatingLining = false,
                    //            Disassembly = true,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = false,
                    //            BaseFrame = true,
                    //            AirSeal = true,
                    //            CoatingLining = true,
                    //            Disassembly = false,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = true,
                    //            SparePartsSpareMedia = true,
                    //            BaseFrame = true,
                    //            AirSeal = true,
                    //            CoatingLining = true,
                    //            Disassembly = false,
                    //        },
                    //         new Additions
                    //         {
                    //             InstalledMedia = true,
                    //             SparePartsSpareMedia = true,
                    //             BaseFrame = true,
                    //             AirSeal = false,
                    //             CoatingLining = false,
                    //             Disassembly = true,
                    //         },
                    //        new Additions
                    //        {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = true,
                    //            BaseFrame = false,
                    //            AirSeal = true,
                    //            CoatingLining = false,
                    //            Disassembly = false,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = true,
                    //            SparePartsSpareMedia = true,
                    //            BaseFrame = false,
                    //            AirSeal = true,
                    //            CoatingLining = false,
                    //            Disassembly = true,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = true,
                    //            BaseFrame = false,
                    //            AirSeal = true,
                    //            CoatingLining = true,
                    //            Disassembly = true,
                    //        },
                    //         new Additions
                    //         {
                    //             InstalledMedia = false,
                    //             SparePartsSpareMedia = false,
                    //             BaseFrame = false,
                    //             AirSeal = false,
                    //             CoatingLining = false,
                    //             Disassembly = true,
                    //         },
                    //        new Additions
                    //        {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = false,
                    //            BaseFrame = true,
                    //            AirSeal = true,
                    //            CoatingLining = true,
                    //            Disassembly = true,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = false,
                    //            BaseFrame = false,
                    //            AirSeal = false,
                    //            CoatingLining = false,
                    //            Disassembly = true,
                    //        },
                    //        new Additions
                    //        {
                    //            InstalledMedia = false,
                    //            SparePartsSpareMedia = false,
                    //            BaseFrame = true,
                    //            AirSeal = true,
                    //            CoatingLining = true,
                    //            Disassembly = true,
                    //        });
                    //    context.SaveChanges();
                    //}


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.GetBaseException().Message);
                }
                #endregion


                #region Seed Required Data
                try
                {

                }
                catch (Exception)
                {
                    throw;
                }

                #endregion

                #region Seed Sample Data
                if (SeedSampleData)
                {
                    try
                    {
                        
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                #endregion
            }//Using Close

        }//Initialize close

    }//HaverInitializer Class close.
}//Namespace close
