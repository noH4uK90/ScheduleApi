using AutoMapper;
using MediatR;
using MimeKit.Text;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Teachers.Commands.Create;

public sealed class CreateTeacherCommandHandler(
    ITeacherRepository teacherRepository,
    IPasswordGenerator passwordGenerator,
    IMailSenderService senderService,
    IMapper mapper) : IRequestHandler<CreateTeacherCommand, int>
{
    public async Task<int> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        const int passwordLength = 12;

        var teacher = mapper.Map<Teacher>(request);

        teacher.Account.Login = teacher.Account.Email;
        teacher.Account.PasswordHash = passwordGenerator.Generate(passwordLength);

        await senderService.SendAsync(new Letter
        {
            From = "Электронное расписание",
            To = teacher.Account.Email,
            Subject = "Данные для входа",
            Message = $"Логин: {teacher.Account.Login}\nПароль: {teacher.Account.PasswordHash}",
            Format = TextFormat.Text
        }, cancellationToken);

        return await teacherRepository.CreateAsync(teacher, cancellationToken);
    }
}