using AutoMapper;
using MediatR;
using MimeKit.Text;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Employees.Commands.Create;

public sealed class CreateEmployeeCommandHandler(
    IEmployeeRepository employeeRepository,
    IPasswordGenerator passwordGenerator,
    IMailSenderService senderService,
    IMapper mapper) : IRequestHandler<CreateEmployeeCommand, int>
{
    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        const int passwordLength = 16;

        var employee = mapper.Map<Employee>(request);

        employee.Account.Login = employee.Account.Email;
        employee.Account.PasswordHash = passwordGenerator.Generate(passwordLength);

        await senderService.SendAsync(new Letter
        {
            From = "Электронное расписание",
            To = employee.Account.Email,
            Subject = "Данные для входа",
            Message = $"Логин: {employee.Account.Login}\nПароль: {employee.Account.PasswordHash}",
            Format = TextFormat.Text
        }, cancellationToken);

        return await employeeRepository.CreateAsync(employee, cancellationToken);
    }
}