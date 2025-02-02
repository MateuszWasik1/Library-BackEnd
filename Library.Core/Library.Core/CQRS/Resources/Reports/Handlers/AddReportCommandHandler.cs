using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Resources.Reports.Commands;
using Library.Core.Exceptions.Books;

namespace Library.Core.CQRS.Resources.Reports.Handlers
{
    public class AddReportCommandHandler : ICommandHandler<AddReportCommand>
    {
        private readonly IDataBaseContext context;
        public AddReportCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(AddReportCommand command)
        {
            if (command.Model.RName.Length < 3)
                throw new AuthorRequiredException("Nazwa wydawnictwa powinna być dłuższa niż 3 znaki!");

            if (command.Model.RName.Length > 255)
                throw new ReportRequiredException("Nazwa wydawnictwa nie powinien przekraczać 255 znaków!");

            if (command.Model.RBase64.Length < 3)
                throw new TitleNameMax255CharactersException("Kraj wydawnictwa powinien być krószy niż 255 znaków!");

            var Report = new Entities.Reports()
            {
                RGID = Guid.NewGuid(),
                RName = command.Model.RName,
                RGenerationDate = command.Model.RGenerationDate,
                RBase64 = command.Model.RBase64,
            };

            context.CreateOrUpdate(Report);
            context.SaveChanges();
        }
    }
}
