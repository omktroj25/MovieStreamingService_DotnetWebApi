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
    public partial class ProfileDtoPaymentDto
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid UserId { get; set; }
        [JsonPropertyName("id")]
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid Id { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid CreatedBy { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or Sets PaymentType
        /// </summary>
        [JsonPropertyName("payment_type")]
        [Required(ErrorMessage = "payment type is required ( payment_type ) ( UPI , CREDIT/CARD , DEBIT/CARD )")]
        [RegularExpression("(UPI|CREDIT/CARD|DEBIT/CARD)", ErrorMessage = "The input must contain UPI, CREDIT/CARD, or DEBIT/CARD.")]
        public string PaymentType { get; set; }="";

        /// <summary>
        /// Gets or Sets CardNumber
        /// </summary>
        [RegularExpression("^[0-9]{13,16}$", ErrorMessage = "Invalid card number. The card number must be between 13 to 16 digits")]
        [JsonPropertyName("card_number")]
        //[Required(ErrorMessage = "card number is required ( card_number )")]
        public string CardNumber { get; set; }="";

        /// <summary>
        /// Gets or Sets CardHolderName
        /// </summary>

        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Invalid name. The name must not contain special characters and numbers")]
        [JsonPropertyName("card_holder_name")]
        //[Required(ErrorMessage = "card holder name is required ( card_holder_name )")]
        public string CardHolderName { get; set; }="";

        /// <summary>
        /// Gets or Sets ExpireDate
        /// </summary>
        [RegularExpression(@"^(0[1-9]|1[0-2])\/([2-9][0-9])$", ErrorMessage = "Invalid expire date format. The valid expire date format is ( MM/YY )")]
        [JsonPropertyName("expire_date")]
        //[Required(ErrorMessage = "expire date is required ( expire_date )")]
        public string ExpireDate { get; set; }="";

        /// <summary>
        /// Gets or Sets UpiId
        /// </summary>
        [RegularExpression("^[a-zA-Z0-9]+@[a-zA-Z0-9]+$", ErrorMessage = "Invalid upi id format. The valid upi id format is ( username@bankname )")]
        [JsonPropertyName("upi_id")]
        //[Required(ErrorMessage = "upi id is required ( upi_id )")]
        public string UpiId { get; set; }="";

        /// <summary>
        /// Gets or Sets UpiApp
        /// </summary>

        [JsonPropertyName("upi_app")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid upi app name. The valid upi app doesnt contains number or other character")]
        //[Required(ErrorMessage = "upi app is required ( upi_app )")]
        public string UpiApp { get; set; }="";

    }
}
