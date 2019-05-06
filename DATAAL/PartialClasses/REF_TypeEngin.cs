﻿using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAAL
{
    [MetadataType(typeof(REF_TypeEnginMetadata))]
    public partial class REF_TypeEngin
    {
        public TypeEnginDTO TypeEnginToDTO()
        {
            var model = this;
            var dto = new TypeEnginDTO()
            {
                Id = model.Id,
                TypeCheckListId = model.TypeCheckListId,
                Name = model.Name,
                DureeEstimative = model.DureeEstimative,
                CreatedBy = model.CreatedBy,
                CreatedOn =  model.CreatedOn
            };
            return dto;
        }
    }
}