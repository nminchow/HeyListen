using System;
using System.Collections.Generic;
using System.Text;

namespace HeyListen.Models
{
    public class Channel
    {
        public int ID { get; set; }
        public string DiscordId { get; set; }
        public virtual User CurrentDj { get; set; }
    }
}
