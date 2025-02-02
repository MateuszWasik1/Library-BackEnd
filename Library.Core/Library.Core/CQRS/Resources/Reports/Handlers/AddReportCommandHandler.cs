using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Resources.Reports.Commands;
using Library.Core.Exceptions.Reports;

namespace Library.Core.CQRS.Resources.Reports.Handlers
{
    public class AddReportCommandHandler : ICommandHandler<AddReportCommand>
    {
        private readonly IDataBaseContext context;
        public AddReportCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(AddReportCommand command)
        {
            if (command.Model.RName.Length < 3)
                throw new RNameMin3Characters("Nazwa raportu powinna być dłuższa niż 3 znaki!");

            if (command.Model.RName.Length > 255)
                throw new RNameMax255Characters("Nazwa raportu nie powinna przekraczać 255 znaków!");

            if (command.Model.RBase64.Length < 3)
                throw new RBase64Min3Characters("Base64 raportu powinien być dłuższy niż 3 znaki!");

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
