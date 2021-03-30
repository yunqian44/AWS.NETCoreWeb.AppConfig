using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig.Models
{
    public class UserViewModel
    {
        /// <summary>
        /// No
        /// </summary>
        [DisplayName("No")]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DisplayName("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        [DisplayName("Age")]
        public int Age { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [DisplayName("Address")]
        public string Address { get; set; }
    }
}
