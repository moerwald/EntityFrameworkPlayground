namespace EntityTest
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public virtual Blog Blog { get; set; }

    }
}
