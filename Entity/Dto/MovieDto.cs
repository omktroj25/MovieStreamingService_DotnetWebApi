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
    public partial class MovieDto
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
        /// Gets or Sets Title
        /// </summary>

        [JsonPropertyName("title")]
        [Required(ErrorMessage = "title is required ( title )")]
        public string Title { get; set; }="";

        /// <summary>
        /// Gets or Sets Genere
        /// </summary>

        [JsonPropertyName("genere")]
        [Required(ErrorMessage = "genere is required ( genere )")]
        public string Genere { get; set; }="";

        /// <summary>
        /// Gets or Sets Director
        /// </summary>

        [JsonPropertyName("director")]
        [Required(ErrorMessage = "director is required ( director )")]
        public string Director { get; set; }="";

        /// <summary>
        /// Gets or Sets Actor
        /// </summary>

        [JsonPropertyName("actor")]
        [Required(ErrorMessage = "actor is required ( actor )")]
        public string Actor { get; set; }="";

        /// <summary>
        /// Gets or Sets Rating
        /// </summary>
        [RegularExpression("^(?:[0-4](?:\\.[0-9]*)?|5(?:\\.0*)?)$")]
        [JsonPropertyName("rating")]
        [Required(ErrorMessage = "rating is required ( rating )")]
        public Decimal Rating { get; set; }

        /// <summary>
        /// Gets or Sets SubscriptionPlan
        /// </summary>

        [JsonPropertyName("subscription_plan")]
        [Required(ErrorMessage = "subscription plan is required ( subscription_plan )")]
        public string SubscriptionPlan { get; set; }="";

        /// <summary>
        /// Gets or Sets SubscriptionId
        /// </summary>

        [JsonPropertyName("subscription_id")]
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid SubscriptionId { get; set; }

    }
}
