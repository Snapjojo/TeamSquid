using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpreadsheetGUI
{
    [JsonObject(MemberSerialization.OptIn)]
    class Message
    {
        public Message()
        {

        }
        /// <summary>
        /// The type of Json message being sent. Associated with EVERY Json message.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string type { get; set; }

        /// <summary>
        /// Error codes
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int? code { get; set; }

        /// <summary>
        /// Source of error codes as cell name
        /// </summary>
        [JsonProperty(PropertyName = "source")]
        public string source { get; set; }

        /// <summary>
        /// The name of a spreadsheet requested by a client.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        /// <summary>
        /// A user's username
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string username { get; set; }

        /// <summary>
        /// A user's password
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string password { get; set; }

        /// <summary>
        /// The name of a cell
        /// </summary>
        [JsonProperty(PropertyName = "cell")]
        public string cell { get; set; }

        /// <summary>
        /// The value of a cell
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public object value { get; set; }

        /// <summary>
        /// A list of a cell's dependencies
        /// </summary>
        [JsonProperty(PropertyName = "dependencies")]
        public List<string> dependencies { get; set; }

        /// <summary>
        /// State of a spreadsheet as sent by server.
        /// </summary>
        [JsonProperty(PropertyName = "spreadsheet")]
        public Dictionary<string, string> spreadsheet { get; set; }

        /// <summary>
        /// List of spreadsheets sent by server to populate menu
        /// </summary>
        [JsonProperty(PropertyName = "spreadsheets")]
        public List<string> spreadsheets { get; set; }
    }
}
