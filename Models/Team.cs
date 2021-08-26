using System.Collections.Generic;
using Newtonsoft.Json;

public class Team : Entity
{
    public long Id { get; set; }
    public string Name { get; set; }

    [JsonProperty(PropertyName = "SubTeam")]
    public List<SubTeam> SubTeam { get; set; }

    [JsonProperty(PropertyName = "Members")]
    public List<EntityMembers> EntityMembers { get; set; }

}