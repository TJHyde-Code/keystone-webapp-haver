﻿using System.ComponentModel.DataAnnotations;

namespace HaverGroupProject.Models
{
    public class Customer
    {
        public int ID { get; set; }

        //Contact Person Summar
        //
        [Display(Name ="Contact")]
        public string CustConSummary
        {
            get
            {
                return CustContactFirst + " " + CustContactLastName;
            }
        }

        [Display(Name = "Customer's Name")]
        [Required(ErrorMessage = "Customer Name is required")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        [StringLength(30, ErrorMessage = "Customer's Name cannot be longer than 30 characters long. ")]
        public string? CustomerName { get; set; }

       

        [Display(Name = "Customer's Address")]
        [StringLength(255, ErrorMessage = "Customer's Address cannot be longer than 255 characters long. ")]
        public string? CustomerAddress { get; set; }

        
        [Display(Name ="Contact First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        [StringLength(50, ErrorMessage = "Customer's Contact First Name cannot be longer than 50 characters long. ")]
        public string? CustContactFirst {  get; set; }

        [Display(Name = "Contact Second Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        [StringLength(50, ErrorMessage = "Customer's Contact Name cannot be longer than 50 characters long. ")]
        public string? CustContactLastName { get; set; }

        [Display(Name = "Customer's Email")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        [StringLength(255, ErrorMessage = "Customer's Email cannot be longer than 255 characters long. ")]
        [DataType(DataType.EmailAddress)]
        public string? CustomerEmail { get; set; }

        //MT-Added Customer Phone Number
        [Display(Name = "Customer Phone Number")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10)]
        public string CustomerPhone { get; set; } = "";        

        //Inserting Archive for Customers property
        public bool CustomerArchived { get; set; }

        public ICollection<OperationsSchedule>? OperationsSchedule { get; set; } = new HashSet<OperationsSchedule>();

        public ICollection<HaverGantt> HaverGantts { get; set; } = new HashSet<HaverGantt>();

    }
}
     