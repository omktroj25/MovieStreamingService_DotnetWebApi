using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Entity.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class SubscriptionDto
    { 
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid UserId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid CreatedBy { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or Sets Key
        /// </summary>

        [JsonPropertyName("key")]
        [Required(ErrorMessage = "key is required ( key )")]
        public string Key { get; set; }="";

        /// <summary>
        /// Gets or Sets Description
        /// </summary>

        [JsonPropertyName("description")]
        [Required(ErrorMessage = "description is required ( description )")]
        public string Description { get; set; }="";

    }
}
