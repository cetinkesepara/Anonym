using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Dtos
{
    public class CategoryForAddDto:IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
