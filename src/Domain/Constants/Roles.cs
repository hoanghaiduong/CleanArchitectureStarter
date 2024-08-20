namespace MyWebApi.Domain.Constants;

public abstract class Roles
{
    public const string Administrator = nameof(Administrator);
    public const string Manager = nameof(Manager);
    public const string Receptionist = nameof(Receptionist);
    public const string Guest = nameof(Guest);
    public const string Admin = nameof(Admin);
    public const string AllRoles = $"{Administrator}, {Manager}, {Receptionist}, {Guest}, {Admin}";
   
}