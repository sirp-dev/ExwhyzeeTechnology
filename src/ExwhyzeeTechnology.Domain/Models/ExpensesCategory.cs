﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExwhyzeeTechnology.Domain.Models
{
    public class ExpensesCategory
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<CompayExpenses> CompayExpenses { get; set; }
    }
}
