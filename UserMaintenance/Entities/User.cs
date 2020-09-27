﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UserMaintenance.Entities
{
    public class User
    {
            public Guid ID { get; set; } = Guid.NewGuid();
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FullName
            {
                get
                {
                    return string.Format(
                        "{0} {1}",
                        LastName,
                        FirstName);
                }
            }
    }
}
