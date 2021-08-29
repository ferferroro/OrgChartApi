using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

public class SubTeam : Entity
{
    public long? Id { get; set; }
    public string Name { get; set; }

    
    [ForeignKey("TeamId")]
    public Team Team { get; set; }
    public long? TeamId { get; set; }


    [ForeignKey("SubTeamId")]
    public long? SubTeamId { get; set; }

    [JsonProperty(PropertyName = "SubTeam")]
    public List<SubTeam> SubSubTeam { get; set; }


    [JsonProperty(PropertyName = "Members")]
    public List<EntityMembers> EntityMembers { get; set; }
}