using System.ComponentModel.DataAnnotations;

namespace PpsPlansApp.Data
{
    public class Entity 
    {
        [Key]
        public int Id { get; set; }
        public string? Note { get; set; }
    }

    public class Car : Entity
    {
        public required string CarNumber { get; set; }
        public FuelType? FuelType { get; set; }
    }

    public class Driver : Entity {
        public required string FioName { get; set; }
    }
    public class Client : Entity {
        public required string Name { get; set; }
        public string? Country { get; set; }
    }
    public class Terminal : Entity {
        /// <summary>
        /// Country City Company
        /// </summary>
        public required string Company { get; set; }
        public required string Address { get; set; }
        public FuelType? FuelType { get; set; }
        public string? GoogleMapsUrl { get; set; }
        public required ICollection<Delivery> Deliveries { get; set; }
    }
    public class Contract : Entity {
        public required string ContractNo { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public Terminal? From { get; set; }
        public Client? Client { get; set; }

        [Precision(2)]
        public decimal TotalWeight { get; set; }

        public required ICollection<Delivery> Deliveries { get; set; }
    }
    public class Delivery : Entity {
        public DateTime DeliveredDate { get; set; }

        public Contract? Contract { get; set; }

        public Driver? Driver { get; set; }
        public Car? Car { get; set; }

        [Precision(2)]
        public decimal Weight { get; set; }

        [Precision(2)]
        public decimal SellPrice { get; set; }
        public bool Paid { get; set; }

        public required DeliveryStatus Status { get; set; }

        public string? CMR_No { get; set; }
        public DateTime? CMR_Date { get; set; }

    }

    public enum DeliveryStatus
    {
        New = 1,
        Fact = 2,
        /// <summary>
        /// предварительно, по поданой заявке
        /// </summary>
        Waiting = 3,
        Cancelled = 4,
        NotSent = 5
    }

    public enum FuelType
    {
        D = 0,
        B = 1,
        BD = 2
    }
}