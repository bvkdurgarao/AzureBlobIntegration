﻿using System.Collections.Generic;

namespace PropertyApi.Models
{
    public class Space
    {
        public string SpaceId { get; set; }
        public string SpaceName { get; set; }
        public List<RentRoll> RentRoll { get; set; }
    }
}