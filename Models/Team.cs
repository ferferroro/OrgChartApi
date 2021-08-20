using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

public class Team : Entity
{
    public long Id { get; set; }
    public string Name { get; set; }

    [JsonProperty(PropertyName = "SubTeam")]
    public List<SubTeam> SubTeam { get; set; }

}