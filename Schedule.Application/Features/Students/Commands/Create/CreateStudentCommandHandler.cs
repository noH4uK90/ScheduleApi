using AutoMapper;
using MediatR;
using MimeKit.Text;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Students.Commands.Create;

public sealed class CreateStudentCommandHandler(
    IStudentRepository studentRepository,
    IPasswordGenerator passwordGenerator,
    IMailSenderService senderService,
    IMapper mapper) : IRequestHandler<CreateStudentCommand, int>
{
    public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        const int passwordLength = 12;

        var student = mapper.Map<Student>(request);

        student.Account.Login = student.Account.Email;
        student.Account.PasswordHash = passwordGenerator.Generate(passwordLength);

        await senderService.SendAsync(new Letter
        {
            From = "Электронное расписание",
            To = student.Account.Email,
            Subject = "Данные для входа",
            Message = $"Логин: {student.Account.Login}\nПароль: {student.Account.PasswordHash}",
            Format = TextFormat.Text
        }, cancellationToken);

        return await studentRepository.CreateAsync(student, cancellationToken);
    }
}