namespace Schedule.Application.Common.Interfaces;

public interface IPasswordGenerator
{
    public string Generate(int length);
}