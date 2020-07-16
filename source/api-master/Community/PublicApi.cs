using System;
using System.ComponentModel.DataAnnotations;

namespace Community
{
    public class PublicKnowledgeListRequest
    {
        [Required]
        public string Category { get; set; }
    }

    public class PublicKnowledgeDetailRequest
    {
        [Required]
        public int Id { get; set; }
    }


}
