using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Waterworks.Models.View.Client
{
    public enum ContractorType
    {
        [Display(Name = "Osoba fizyczna")]
        OsobaFizyczna = 1,
        [Display(Name = "Podmiot gospodarczy")]
        PodmiotGospodarczy = 2
    }
    public class ClientCreateViewModel
    {
        public int Id { get; set; }
        public string NIP { get; set; }
        public string REGON { get; set; }
        public string KRS { get; set; }
        public string PESEL { get; set; }

        [EnumDataType(typeof(ContractorType))]
        [Display(Name = "Rodzaj klienta")]
        public ContractorType RodzajKlientaId { get; set; }

        [Required(ErrorMessage = @"Pole jest wymagane")]
        [Display(Name = "Imię*")]
        public string Imie { get; set; }
        [Display(Name = "Nazwisko*")]
        [Required(ErrorMessage = @"Pole jest wymagane")]
        public string Nazwisko { get; set; }
        [Display(Name = "Miejscowość*")]
        [Required(ErrorMessage = @"Pole jest wymagane")]
        public string Miejscowosc { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "Kod pocztowy*")]
        [Required(ErrorMessage = @"Pole jest wymagane")]
        public string KodPocztowy { get; set; }

        [Display(Name = "Poczta*")]
        [Required(ErrorMessage = @"Pole jest wymagane")]
        public string Poczta { get; set; }

        [Display(Name = "Ulica*")]
        [Required(ErrorMessage = @"Pole jest wymagane")]
        public string Ulica { get; set; }

        [Display(Name = "Nr domu*")]
        [Required(ErrorMessage = @"Pole jest wymagane")]
        public string NrDomu { get; set; }

        [Display(Name = "Nr lokalu")]
        public string NrLokalu { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = @"Pole jest wymagane")]
        [Display(Name = "Numer telefonu*")]
        public string Telefon { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Dodatkowy numer telefonu")]
        public string DodatkowyTelefon { get; set; }

        [EmailAddress]
        [Display(Name = "Email*")]
        [Required(ErrorMessage = @"Pole jest wymagane")]
        public string Email { get; set; }

        [EmailAddress]
        [Display(Name = "Dodatkowy email")]
        public string DodatkowyEmail { get; set; }

        //adres korespondencyjny
        public CorrespondenceAdressViewModel CorrespondeceAddress { get; set; }

        //aktywny odbiorca
        public ActiveReceiverViewModel ActiveReceiver { get; set; }
    }
}
