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
    public partial class TokenResponseDto
    { 
        /// <summary>
        /// Gets or Sets TokenType
        /// </summary>

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }="";

        /// <summary>
        /// Gets or Sets AccessToken
        /// </summary>

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }="";

    }
}
