﻿using System.Collections.Generic;


namespace EntityTest
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }

        public virtual List<Post> Posts { get; set; }

        public virtual User User { get; set; }
    }
}
