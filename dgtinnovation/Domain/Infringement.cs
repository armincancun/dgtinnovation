using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Abstract;

namespace Domain
{
    public class Infringement: IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Points_To_Discount { get; set; }
        public bool Status { get; set; }

        public ICollection<DriverInfringement> DriverInfringements { get; set; }

    }
}