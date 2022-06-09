namespace spider3auth.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
        //Allows Anonymous access to method
    }
}
