using System.Collections.Generic;
using Newtonsoft.Json;

public class Company : Entity
{
    public long Id { get; set; }
    
    public string Name { get; set; }

    [JsonProperty(PropertyName = "Members")]
    public List<EntityMembers> EntityMembers { get; set; }
}