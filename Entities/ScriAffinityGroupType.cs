﻿using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriAffinityGroupType
    {
        public ScriAffinityGroupType()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idAffinityGroupSub { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ScriAffinityGroupSub idAffinityGroupSubNavigation { get; set; }
    }
}
